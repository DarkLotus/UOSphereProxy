using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UOProxy.Packets;
using UOProxy.Packets.FromClient;
using UOProxy.Packets.FromServer;

namespace UOProxy
{
    public partial class UOProxy
    {
        public event GumpMenuSelectionEventHandler _0xB1GumpMenuSelection;
        public delegate void GumpMenuSelectionEventHandler(Packets.FromClient._0xB1GumpMenuSelection e,TcpClient serverCon);

        public event TalkRequestEventHandler _0x03TalkRequest;
        public delegate void TalkRequestEventHandler(Packets.FromClient._0x03TalkRequest e);

        public event AttackRequestEventHandler _0x05AttackRequest;
        public delegate bool AttackRequestEventHandler(Packets.FromClient._0x05RequestAttack e);

        private bool HandleClientPacket(byte[] data, int bytesRead,TcpClient server)
        {
            UOStream Data = new UOStream(data);
            switch(data[0])
            {
                case OpCode.CMSG_GumpMenuSelection:
                    var p = new Packets.FromClient._0xB1GumpMenuSelection(Data);
                    if (_0xB1GumpMenuSelection != null)
                        _0xB1GumpMenuSelection((Packets.FromClient._0xB1GumpMenuSelection)p,server);
                    break;
                case OpCode.CMSG_TalkRequest:
                    var ptalk = new Packets.FromClient._0x03TalkRequest(Data);
                    if (ptalk.Message.StartsWith("~"))
                    {
                        HandleProxyCommand(ptalk);
                        Console.WriteLine("Command accepted: " + ptalk.Message);
                        //dont forward to server
                        return false;
                    }
                    if (_0x03TalkRequest != null)
                        _0x03TalkRequest(ptalk);
                    break;
                case OpCode.CMSG_RequestAttack:
                    var p05 = new Packets.FromClient._0x05RequestAttack(Data);
                    if (_0x05AttackRequest != null)
                        return _0x05AttackRequest((_0x05RequestAttack)p05);
                    break;
            }

            return true;
        }

        public Dictionary<int,int> GumpsWaitedFor = new Dictionary<int, int>();
        private void HandleProxyCommand(_0x03TalkRequest ptalk)
        {
            var commands = ptalk.Message.Remove(0,1).Split(new char[] { '#' });
            if(commands[0].Equals("recall"))
            {
                int gumpid = int.Parse(commands[1]);
                int Clicked = int.Parse(commands[2]);
                GumpsWaitedFor.Add(gumpid, Clicked);
                this._0xB0SendGumpMenuDialog += UOProxy__0xB0SendGumpMenuDialog;
            }
        }

        private void UOProxy__0xB0SendGumpMenuDialog(_0xB0SendGumpMenuDialog e)
        {
            throw new NotImplementedException();
        }
    }
}
