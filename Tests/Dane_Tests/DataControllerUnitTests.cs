using Dane;
using Moq;
using System.ComponentModel;

namespace Dane_Tests
{
    [TestClass]
    public class DataControllerUnitTests
    {
        [TestMethod]
        public void Move()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.Setup(data => data.Move()).Raises(data => data.PropertyChanged += null, new PropertyChangedEventArgs("Move")).Verifiable();

            data.Move();

            mockData.Verify();
        }

        [TestMethod]
        public void GetSize()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            int temp = data.Size;

            mockData.VerifyGet(data => data.Size, Times.Once);
        }

        [TestMethod]
        public void GetWidth()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            int temp = data.Width;

            mockData.VerifyGet(data => data.Width, Times.Once);
        }

        [TestMethod]
        public void GetHeight()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            int temp = data.Height;

            mockData.VerifyGet(data => data.Height, Times.Once);
        }

        [TestMethod]
        public void GetX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            float temp = data.X;

            mockData.VerifyGet(data => data.X, Times.Once);
        }

        [TestMethod]
        public void SetX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object)
            {
                X = 1.0f
            };

            mockData.VerifySet(data => data.X = 1.0f);
        }

        [TestMethod]
        public void GetY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            float temp = data.Y;

            mockData.VerifyGet(data => data.Y, Times.Once);
        }

        [TestMethod]
        public void SetY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object)
            {
                Y = 1.0f
            };

            mockData.VerifySet(data => data.Y = 1.0f);
        }

        [TestMethod]
        public void GetVelX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            float temp = data.VelX;

            mockData.VerifyGet(data => data.VelX, Times.Once);
        }

        [TestMethod]
        public void SetVelX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object)
            {
                VelX = 1.0f
            };

            mockData.VerifySet(data => data.VelX = 1.0f);
        }

        [TestMethod]
        public void GetVelY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            float temp = data.VelY;

            mockData.VerifyGet(data => data.VelY, Times.Once);
        }

        [TestMethod]
        public void SetVelY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object)
            {
                VelY = 1.0f
            };

            mockData.VerifySet(data => data.VelY = 1.0f);
        }

    }

}
