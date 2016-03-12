using Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MajesticUO2016
{
    public static class Client
    {
        public static Player Player;
        public static Dictionary<int,Item> Items = new Dictionary<int, Item>();
        public static List<Mobile> Mobiles = new List<Mobile>();
    }
}
