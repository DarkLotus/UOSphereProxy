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
using UOProxy.Packets.FromServer;

namespace Packets
{
    public class NetClient
    {

        public event DamageEventHandler onEventDamageReceived0x0B;
        public delegate void DamageEventHandler(_0x0BDamage e);

        public event LoginCompleteEventHandler onEventLoginComplete0x55;
        public delegate void LoginCompleteEventHandler(_0x55LoginComplete e);

        public event GameServerListEventHandler onEventGameServerList0xA8;
        public delegate void GameServerListEventHandler(_0xA8GameServerList e);
        public event CharStartingLocationEventHandler onEventCharStartingLocation0xA9;
        public delegate void CharStartingLocationEventHandler(_0xA9CharStartingLocation e);
        

        public event StatusBarInfoEventHandler _0x11StatusBarInfo;
        public delegate void StatusBarInfoEventHandler(_0x11StatusBarInfo e);

        public event StatusBarUpdateEventHandler _0x16StatusBarUpdate;
        public delegate void StatusBarUpdateEventHandler(_0x16StatusBarUpdate e);

        public event ObjectInfoEventHandler _0x1AObjectInfo;
        public delegate void ObjectInfoEventHandler(_0x1AObjectInfo e);

        public event CharLocaleEventHandler onEventCharLocaleAndBody0x1B;
        public delegate void CharLocaleEventHandler(_0x1BCharLocaleBody e);

        public event SendSpeechEventHandler _0x1CSendSpeech;
        public delegate void SendSpeechEventHandler(_0x1CSendSpeech e);

        public event DeleteObjectEventHandler _0x1DDeleteObject;
        public delegate void DeleteObjectEventHandler(_0x1DDeleteObject e);

        public event MobAttributeEventHandler _0x2DMobAttributes;
        public delegate void MobAttributeEventHandler(_0x2DMobAttributes e);

        public event UpdatePlayerEventHandler onEventUpdatePlayer0x77;
        public delegate void UpdatePlayerEventHandler(_0x77UpdatePlayer e);

        public event _0x78DrawObjectEventHandler onEventDrawObject0x78;
        public delegate void _0x78DrawObjectEventHandler(_0x78DrawObject e);

        public event ConnectToGameServerEventHandler onEventConnectToGameServer0x8c;
        public delegate void ConnectToGameServerEventHandler(_0x8CConnectToGameServer e);

        public event SendGumpMenuDialogEventHandler _0xB0SendGumpMenuDialog;
        public delegate void SendGumpMenuDialogEventHandler(_0xB0SendGumpMenuDialog e);

        public event CompressedGumpEventHandler _0xDDCompressedGump;
        public delegate void CompressedGumpEventHandler(_0xDDCompressedGump e);

        public event UpdateCurrentHealthEventHandler onEventUpdateCurrentHealth0xA1;
        public delegate void UpdateCurrentHealthEventHandler(_0xA1UpdateCurrentHealth e);

        public event RequestWarModeEventHandler onEventRequestWarMode0x72;
        public delegate void RequestWarModeEventHandler(_0x72RequestWarMode e);
        

        public event EventHandler onConnected;

        //public EventDictionary HandlersEvents = new EventDictionary();
        private Dictionary<byte, Tuple<Type, Delegate>> Handlers = new Dictionary<byte, Tuple<Type, Delegate>>();
        private bool _useHuffman = false;
        private byte[] TempdataBuffer = new byte[8096];
        public bool Connected { get { return _server.Connected; } }

        private TcpClient _server;


        public NetClient()
        {
            


        }

        public TcpClient ConnectToServer(string ip, int port)
        {
            SetupHandlers();
            _server = new TcpClient();
            _server.Connect(IPAddress.Parse(ip), port);
            Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
            ServerComThread.Start(_server);
            Thread.Sleep(1000);
            return _server;

        }
        public bool SendPacket(Packet p)
        {
            Logger.Log("Sending Len: " + p.PacketData.Length + " packet: " + p);
            if (_server.Connected)
            {
                _server.GetStream().Write(p.PacketData, 0, p.PacketData.Length);
            }
            return true;
        }
        private void SetupHandlers()
        {
            Handlers.Clear();
            Handlers.Add(0x0B, new Tuple<Type, Delegate>(typeof(_0x0BDamage), onEventDamageReceived0x0B));
            Handlers.Add(0x11, new Tuple<Type, Delegate>(typeof(_0x11StatusBarInfo), _0x11StatusBarInfo));
            Handlers.Add(0x16, new Tuple<Type, Delegate>(typeof(_0x16StatusBarUpdate), _0x16StatusBarUpdate));
            Handlers.Add(0x17, new Tuple<Type, Delegate>(typeof(_0x16StatusBarUpdate), _0x16StatusBarUpdate)); //KR / SA versions
            Handlers.Add(0x1A, new Tuple<Type, Delegate>(typeof(_0x1AObjectInfo), _0x1AObjectInfo));
            Handlers.Add(0x1B, new Tuple<Type, Delegate>(typeof(_0x1BCharLocaleBody), onEventCharLocaleAndBody0x1B));
            Handlers.Add(0x1C, new Tuple<Type, Delegate>(typeof(_0x1CSendSpeech), _0x1CSendSpeech));
            Handlers.Add(0x1D, new Tuple<Type, Delegate>(typeof(_0x1DDeleteObject), _0x1DDeleteObject));
            Handlers.Add(0x20, new Tuple<Type, Delegate>(typeof(_0x20DrawGamePlayer), null));
            Handlers.Add(0x21, new Tuple<Type, Delegate>(typeof(_0x21CharMoveRejection), null));
            Handlers.Add(0x24, new Tuple<Type, Delegate>(typeof(_0x24DrawContainer), null));
            Handlers.Add(0x25, new Tuple<Type, Delegate>(typeof(_0x25AddItemToContainer), null));
            Handlers.Add(0x2D, new Tuple<Type, Delegate>(typeof(_0x2DMobAttributes), _0x2DMobAttributes));
            Handlers.Add(0x2E, new Tuple<Type, Delegate>(typeof(_0x2EWornItem), null));
            Handlers.Add(0x55, new Tuple<Type, Delegate>(typeof(_0x55LoginComplete), onEventLoginComplete0x55));
            Handlers.Add(0x72, new Tuple<Type, Delegate>(typeof(_0x72RequestWarMode), onEventRequestWarMode0x72));
            Handlers.Add(0x77, new Tuple<Type, Delegate>(typeof(_0x77UpdatePlayer), onEventUpdatePlayer0x77));
            Handlers.Add(0x78, new Tuple<Type, Delegate>(typeof(_0x78DrawObject), onEventDrawObject0x78));
            Handlers.Add(0x8c, new Tuple<Type, Delegate>(typeof(_0x8CConnectToGameServer), onEventConnectToGameServer0x8c));
            Handlers.Add(0xA1, new Tuple<Type, Delegate>(typeof(_0xA1UpdateCurrentHealth), onEventUpdateCurrentHealth0xA1));
            Handlers.Add(0xA8, new Tuple<Type, Delegate>(typeof(_0xA8GameServerList), onEventGameServerList0xA8));
            Handlers.Add(0xA9, new Tuple<Type, Delegate>(typeof(_0xA9CharStartingLocation), onEventCharStartingLocation0xA9));
            Handlers.Add(0xB0, new Tuple<Type, Delegate>(typeof(_0xB0SendGumpMenuDialog), _0xB0SendGumpMenuDialog));
            Handlers.Add(0xB9, new Tuple<Type, Delegate>(typeof(_0xB9EnableLockedClientFeature), null));
            Handlers.Add(0xBF, new Tuple<Type, Delegate>(typeof(_0xBFGeneralInformation), null));
            Handlers.Add(0xDD, new Tuple<Type, Delegate>(typeof(_0xDDCompressedGump), _0xDDCompressedGump));
            
        }
        private void HandleServerCom(object server)
        {
            List<byte> IncomingQueue = new List<byte>();
            HuffmanDecompression Decompressor = new HuffmanDecompression();
            NetworkStream ServerStream = _server.GetStream();

            Thread.Sleep(1000);
            if (onConnected != null)
                onConnected(null,null);

            while (_server.Connected)
            {
                //MessagePump from/to Server
                Thread.Sleep(5);
                if (_server.Available <= 0)
                    continue;
                int bytesRead = ServerStream.Read(TempdataBuffer, 0, _server.Available);

                byte[] data = new byte[bytesRead];
                Array.Copy(TempdataBuffer, 0, data, 0, bytesRead);

                // if (TcpClients.client != null && UseHuffman && TcpClients.client.Connected)
                //     TcpClients.client.GetStream().Write(data, 0, bytesRead);// this is here so we send uncompressed for now, No compress method

                if (IncomingQueue.Count > 0)
                {
                    IncomingQueue.AddRange(data);
                    data = IncomingQueue.ToArray();
                    IncomingQueue = new List<byte>();
                    bytesRead = data.Length;
                }
                if (_useHuffman)
                {

                    byte[] dest = new byte[4096];
                    int destSize = 0;
                    int datalen = data.Length;
                    while (Decompressor.DecompressOnePacket(ref data, bytesRead, ref dest, ref destSize))
                    {
                        byte[] destTrimmed = new byte[destSize];
                        Array.Copy(dest, 0, destTrimmed, 0, destSize);
                        Logger.Log("From Server DeHuffed: " + BitConverter.ToString(destTrimmed, 0, destSize));
                        HandlePacketFromServer(destTrimmed);
                        bytesRead = data.Length;
                    }
                    if (data.Length > 0)
                    {
                        //Logger.Log("NoFull Packets adding " + data.Count() + " to queue");
                        IncomingQueue.AddRange(data);
                    }
                }
                else
                {
                    HandlePacketFromServer(data);
                    Logger.Log("From Server NoHuff: " + BitConverter.ToString(data, 0, bytesRead));

                    // if (TcpClients.client != null)
                    //    TcpClients.client.GetStream().Write(data, 0, bytesRead);
                   /* if (data[0] == 0x8c)
                    {
                        _server.Close();
                    }*/
                }

            }
        }




        private void HandlePacketFromServer(byte[] data)
        {
            //uncompressed packets may arrive with more than one in data;
            //HandlersEvents.Add(0x8c, this._0x8CConnectToGameServer);
            UOStream Data = new UOStream(data);
            Packet packet = new Packet();
            if (data == null || data.Length < 1)
                return;


            Data.Position = 0;
            if (Handlers.ContainsKey(data[0]))
            {
                packet = (Packet)Activator.CreateInstance(Handlers[data[0]].Item1, new object[] { Data });
                var eventDelegate = Handlers[data[0]].Item2;
                if(eventDelegate != null)
                eventDelegate.DynamicInvoke(new object[] { packet });
                //Logger.Log(packet.ToString() + "Handled");
               /* var eventinfo = this.GetType().GetField(packet.GetType().Name, BindingFlags.Instance
                    | BindingFlags.NonPublic);

                if (eventinfo != null)
                {
                    var member = eventinfo.GetValue(this);
                    if (member != null)
                    {
                        Logger.Log(member.ToString());

                        member.GetType().GetMethod("Invoke").Invoke(member, new object[] { packet });
                    }
                    else
                    {
                        //Logger.Log("MEMBER WAS NULL FOR EVENT: " + eventinfo.Name);
                    }

                }
                else
                {
                    Logger.Log("EVENTFIELD WAS NULL FOR PACKET : " + packet.ToString());
                }*/
                if (data[0] == 0x8c)
                { _useHuffman = true; }
                return;
            }
            else
            {
                if (data[0] == 0xBC) // Seasonal Info
                    return;
                if (data[0] == 0x76) // New subserver
                    return;
                if (data[0] == 0x4F || data[0] == 0x4E) // lightlevel
                    return;
                Logger.Log(data[0].ToString("x") + "No Handler");
            }
            return;

        }


    }
}
