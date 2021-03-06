﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace UOProxy
{
    public class UOStream : MemoryStream
    {
        public override string ToString()
        {
            var curPos = this.Position;
            this.Position = 0;
            var buf = new byte[this.Length];
            var results = "";
            this.Read(buf, 0, (int)this.Length);
            foreach (var b in buf)
                results += b + " ";
            return results;
        }
        public UOStream(byte[] Data) : base(Data)
        {
            this.Position = 0;

        }
        public UOStream() : base()
        {

        }
        public void WriteInt(int Value)
        {
            byte[] data = new byte[4];
            data = BitConverter.GetBytes(Value);
            this.Write(data, 0, 4);
        }

        public void WriteBit(byte Value)
        {
            this.Write(new byte[] { Value }, 0, 1);
        }
        public void WriteShort(short Value)
        {
            byte[] data = new byte[2];
            data = BitConverter.GetBytes(Value);
            this.Write(data, 0, 2);
        }

        internal byte[] ReadBytes(int v)
        {
            throw new NotImplementedException();
        }

        public void WriteString(string Value,int RequiredLength)
        {
            //RequiredLength 0 if you dont need padding.
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            if (RequiredLength == 0)
            {
                byte[] Data = encoding.GetBytes(Value);
                this.Write(Data, 0, Data.Length);
            }
            else
            {
                byte[] Data = new byte[RequiredLength];
                for (int i = 0; i < Data.Length;i++)
                {
                    Data[i] = 0x00;
                }
                if(Data.Length <= RequiredLength)
                     encoding.GetBytes(Value).CopyTo(Data,0);

                this.Write(Data, 0, Data.Length);
            }
        }

        

        internal void WriteUInt(uint v)
        {
            throw new NotImplementedException();
        }

        public int ReadInt()
        {
            byte[] results = new byte[4]; this.Read(results, 0, 4);
            return (int)((results[0] << 24 | results[1] << 16) | (results[2] << 8) | results[3]);
        }

        public byte ReadBit()
        {
            byte[] results = new byte[1]; this.Read(results, 0, 1);
            return results[0];
        }

        public short ReadShort()
        {
            byte[] results = new byte[2]; this.Read(results, 0, 2);
            return (short)(results[0] << 8 | results[1]);
        }

        public string Read30CharString()
        {
            char[] mystring = new char[30];
            for (int i = 0; i < 30; i++)
            {
                mystring[i] = (char)this.ReadBit();
            }
            return new string(mystring);
        }
        public string ReadString(int bytesToRead)
        {
            char[] mystring = new char[bytesToRead];
            for (int i = 0; i < bytesToRead; i++)
            {
                var charr = (char)this.ReadBit();
                if (charr != 0x00)
                mystring[i] = (char)charr;
            }
            return new string(mystring);
        }
        public string ReadNullTermString()
        {
            byte current;
            byte previous = 1;
            List<char> ms = new List<char>();
            while (true)
            {
                current = this.ReadBit();
                if (current == 0x00 && previous == 0x00) // Reads untill null terminated
                    break;
                ms.Add((char)current);
                previous = (byte)(current + 0);

            }
            return new string(ms.ToArray());
            
        }
    }
}
