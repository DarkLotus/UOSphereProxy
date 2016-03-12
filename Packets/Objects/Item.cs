using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Packets.FromServer;

namespace Packets
{
    public class Item
    {
        private short amount;
        private int containerSerial;
        private short graphicID;
        private short hue;
        private byte index;
        private int serial;
        private short x;
        private short y;

        public Item(int serial, short graphicID, short amount, short x, short y, byte index, int containerSerial, short hue)
        {
            this.serial = serial;
            this.graphicID = graphicID;
            this.amount = amount;
            this.x = x;
            this.y = y;
            this.index = index;
            this.containerSerial = containerSerial;
            this.hue = hue;
        }

        public void Update(_0x78DrawObject e)
        {
            throw new NotImplementedException();
        }
    }
}
