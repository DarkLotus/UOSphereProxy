using System;
using UOProxy.Packets.FromServer;

namespace Packets
{
    public class Mobile
    {
        public byte Flags;
        public ushort GraphicID;
        public int ID;
        public byte Notoriety;
        public short X;
        public short Y;
        public byte Z;

        public Mobile(_0x78DrawObject e)
        {
            this.Flags = e.Flags;
            this.GraphicID = e.GraphicID;
            this.ID = e.Serial;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Notoriety = e.Notoriety;
        }

        internal void Update(_0x78DrawObject e)
        {

            //throw new NotImplementedException();
        }
    }
}