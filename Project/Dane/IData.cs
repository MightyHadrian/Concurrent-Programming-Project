using System.ComponentModel;

namespace Dane
{
    public interface IData
    {
        public int Width { get; }
        public int Height { get; }
        public int Size { get; }
        public float Mass { get; }
        public float X { get; set; }
        public float Y { get; set; }
        public float VelX { get; set; }
        public float VelY { get; set; }
    }
}
