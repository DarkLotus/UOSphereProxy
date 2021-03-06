﻿using Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UOProxy.Helpers;

namespace UOProxy.Packets.FromServer
{
    public class _0x25AddItemToContainer : Packet
    {
        public int Serial;
        public ushort GraphicID;
        byte OffSetGraphicID; // unknown??
        public short Amount, X, Y;
        public byte Index;
        public int ContainerSerial;
        public short Hue;
        public Item Item;
        public _0x25AddItemToContainer(UOStream Data)
            : base(Data)
        {
            Serial = Data.ReadInt();
            GraphicID = (ushort)Data.ReadShort();
            OffSetGraphicID = Data.ReadBit();
            Amount = Data.ReadShort();
            X = Data.ReadShort();
            Y = Data.ReadShort();
            Index = Data.ReadBit();
            ContainerSerial = Data.ReadInt();
            Hue = Data.ReadShort();
            this.Item = new Item(Serial,GraphicID,Amount,X,Y,Index,ContainerSerial,Hue);
        }
    }
}
