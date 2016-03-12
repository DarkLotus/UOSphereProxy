using UOProxy.Packets.FromServer;

namespace MajesticUO2016
{
    public class Player
    {
        public int ID;
        public short GraphicID, X, Y;
        public byte Z, Facing;
        public Player(_0x1BCharLocaleBody e)
        {
            this.ID = e.ID;
            this.GraphicID = e.GraphicID;
            this.X = e.X;
            this.Y = e.Y;
            this.Z = e.Z;
            this.Facing = e.Facing;
        }
    }
}