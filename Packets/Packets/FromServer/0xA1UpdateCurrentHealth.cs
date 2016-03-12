using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UOProxy.Packets.FromServer
{
    public class _0xA1UpdateCurrentHealth : Packet
    {
        int playerID;
        short maxHealth;
        short curHealth;
        public _0xA1UpdateCurrentHealth(UOStream Data)
            : base(Data)
        {
            playerID = Data.ReadInt();
            maxHealth = Data.ReadShort();
            curHealth = Data.ReadShort();
        }
    }
}
