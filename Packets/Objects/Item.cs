using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Packets.FromServer;

namespace Packets
{
    public class Item
    {
        public short amount;
        public int containerSerial;
        public ushort GraphicID;
        public short hue;
        public byte index;
        public int serial;
        public short X;
        public short Y;

        public Item(int serial, ushort graphicID, short amount, short x, short y, byte index, int containerSerial, short hue)
        {
            this.serial = serial;
            this.GraphicID = graphicID;
            this.amount = amount;
            this.X = x;
            this.Y = y;
            this.index = index;
            this.containerSerial = containerSerial;
            this.hue = hue;
        }

       
    }
}
