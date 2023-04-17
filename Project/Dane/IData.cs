using System.ComponentModel;

namespace Dane
{
    public interface IData : INotifyPropertyChanged
    {
        public IData Create(int size, int width, int height, float velX, float velY);
        public void Move();
        public int Size { get; }
        public int Width { get; }
        public int Height { get; }
        public float X { get; set; }
        public float Y { get; set; }
        public float VelX { get; set; }
        public float VelY { get; set; }
        public void RaisePropertyChanged(string name);
    }
}
