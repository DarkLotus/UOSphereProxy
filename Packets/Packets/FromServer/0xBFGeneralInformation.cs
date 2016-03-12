using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xBFGeneralInformation : Packet
    {
        public short Length, SubCommand;
        public _0xBFGeneralInformation(UOStream Data)
            : base(Data)
        {
            Length = Data.ReadShort();
            SubCommand = Data.ReadShort();
            //TODO handle these
            switch(SubCommand)
            {
                case 0x08:
                    //Set cursor hue
                    var hue = Data.ReadByte();
                    break;
                default:
                    Console.WriteLine("Unhandled General Info Sub Command: " + SubCommand.ToString("x"));
                    break;
            }
            
        }
    }
}
