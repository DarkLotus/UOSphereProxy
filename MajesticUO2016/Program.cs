using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using UOProxy;
using UOProxy.Packets;
using UOProxy.Packets.FromClient;
using UOProxy.Packets.FromServer;

namespace MajesticUO2016
{
    class Program
    {
        static NetClient _netClient;
        static void Main(string[] args)
        {
            _netClient = new NetClient();
            _netClient.onConnected += (s, e) => {
                _netClient.SendPacket(new _0xEFClientLoginSeed(new IPAddress(new byte[] { 0xEF, 0x7F, 0x00, 0x00 }), 2, 0, 3, 0));
                _netClient.SendPacket(new _0x80LoginRequest("admin", "admin", 0));

            };

            _netClient.onEventGameServerList0xA8 += (e) => {
                _netClient.SendPacket(new _0xA0SelectServer(0)); };

            _netClient.onEventConnectToGameServer0x8c += (e) => {
                _netClient.SendPacket(new _0x91GameServerLogin(e.Key, "admin", "admin")); };

            _netClient.onEventCharStartingLocation0xA9 += (e) => {
                _netClient.SendPacket(new _0x5DLoginCharacter(e.Characters.Values.First(), e.Characters.Keys.First())); };
            _netClient.ConnectToServer("127.0.0.1", 2593);
            while (_netClient.Connected)
            {
                Thread.Sleep(500);
                lock (Logger.MsgLog)
                {
                    foreach (var msg in Logger.MsgLog)
                        Console.WriteLine(msg);
                    Logger.MsgLog.Clear();
                }
            }
        }


    

     
    }
}
