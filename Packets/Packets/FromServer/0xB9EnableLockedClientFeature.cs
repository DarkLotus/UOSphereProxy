using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xB9EnableLockedClientFeature : Packet
    {
        public short Flags;
        public _0xB9EnableLockedClientFeature(UOStream Data)
            : base(Data)
        {
            Flags = Data.ReadShort();


        }
    }
}
