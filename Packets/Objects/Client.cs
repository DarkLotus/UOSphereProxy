using Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UOProxy.Packets.FromClient;

namespace Packets
{
    public class Client
    {
        public static Player Player;
        public static Dictionary<int,Item> Items = new Dictionary<int, Item>();
        public static List<Mobile> Mobiles = new List<Mobile>();
        public Client(NetClient _netClient,string user, string pass)
        {
            _netClient.onConnected += (s, e) => {
                _netClient.SendPacket(new _0xEFClientLoginSeed(new IPAddress(new byte[] { 0xEF, 0x7F, 0x00, 0x00 }), 2, 0, 3, 0));
                _netClient.SendPacket(new _0x80LoginRequest(user, pass, 0));

            };

            _netClient.onEventGameServerList0xA8 += (e) => {
                _netClient.SendPacket(new _0xA0SelectServer(0));
            };

            _netClient.onEventConnectToGameServer0x8c += (e) => {
                _netClient.SendPacket(new _0x91GameServerLogin(e.Key, user, pass));
            };

            _netClient.onEventCharStartingLocation0xA9 += (e) => {
                _netClient.SendPacket(new _0x5DLoginCharacter(e.Characters.Values.First(), e.Characters.Keys.First()));
            };

            _netClient.onEventCharLocaleAndBody0x1B += (e) => {
                Client.Player = new Player(e);
            };
            _netClient.onEventDrawObject0x78 += (e) =>
            {
                if (Client.Items.ContainsKey(e.Serial))
                    Client.Items[e.Serial].Update(e);
                else
                    Client.Items.Add(e.Serial, new Packets.Item(e.Serial, e.GraphicID, 0, e.X, e.Y, 0, 0, e.Hue));
            };
        }
    }
}
