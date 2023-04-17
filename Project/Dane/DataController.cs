using System.ComponentModel;

namespace Dane
{
    public class DataController : INotifyPropertyChanged
    {
        private readonly IData _idata;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DataController(IData idata)
        {
            _idata = idata;
        }

        static public DataController Create(IData data)
        {
            return new DataController(data);
        }

        public void Move()
        {
            _idata.Move();
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
        }

        public int Size
        {
            get => _idata.Size;
        }

        public int Width
        {
            get => _idata.Width;
        }

        public int Height
        {
            get => _idata.Height;
        }

        public float X
        {
            get => _idata.X;

            set => _idata.X = value;
        }

        public float Y
        {
            get => _idata.Y;

            set => _idata.Y = value;
        }

        public float VelX
        {
            get => _idata.VelX;

            set => _idata.VelX = value;
        }

        public float VelY
        {
            get => _idata.VelY;

            set => _idata.VelY = value;
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
