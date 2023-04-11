using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dane
{
    public class DataController
    {
        private readonly IData _idata;

        public DataController(IData idata)
        {
            _idata = idata;
        }

        static public DataController Create(IData data)
        {
            return new DataController(data);
        }

        public int Size
        {
            get => _idata.Size;
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

    }

}
