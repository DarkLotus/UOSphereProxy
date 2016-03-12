using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0x72RequestWarMode : Packet
    {
        public int flag;
        public _0x72RequestWarMode(UOStream Data)
            : base(Data)
        {
            this.flag = Data.ReadByte();
            Data.ReadByte(); Data.ReadByte(); Data.ReadByte();
        }
    }
}
