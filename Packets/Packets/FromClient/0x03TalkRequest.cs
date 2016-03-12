using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromClient
{
    public class _0x03TalkRequest : Packet
    {
        public short Length;
        public byte SpeechType;
        public short Color;
        public short SpeechFont;
        public string Message;
        public _0x03TalkRequest(UOStream data) : base(data)
        {
            try
            {
                this.Length = data.ReadShort();
                this.SpeechType = (byte)data.ReadByte();
                this.Color = data.ReadShort();
                this.SpeechFont = data.ReadShort();
                this.Message = data.ReadNullTermString();
            }
            catch
            {

            }

        }

        public _0x03TalkRequest(short len, byte type, short color, short font, string msg) : base(0x03)
        {
            Data.WriteShort(len);
            Data.WriteByte(type);
            Data.WriteShort(color);
            Data.WriteShort(font);
            //pad to block of 8???
            Data.WriteString(msg,msg.Length + 8 - (msg.Length % 8));
        }
    }
}


