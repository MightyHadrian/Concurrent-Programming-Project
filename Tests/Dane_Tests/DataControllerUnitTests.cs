using Dane;
using Moq;

namespace Dane_Tests
{
    [TestClass]
    public class DataControllerUnitTests
    {
        [TestMethod]
        public void GetWidth()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.Width);

            int temp = data.Width;

            mockData.VerifyGet(data => data.Width, Times.Once);
        }

        [TestMethod]
        public void GetHeight()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.Height);

            int temp = data.Height;

            mockData.VerifyGet(data => data.Height, Times.Once);
        }


        [TestMethod]
        public void GetSize()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.Size);

            int temp = data.Size;

            mockData.VerifyGet(data => data.Size, Times.Once);
        }


        [TestMethod]
        public void GetMass()
        {
            Mock<IData> mockData = new();
            
            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.Mass);

            float temp = data.Mass;

            mockData.VerifyGet(data => data.Mass, Times.Once);
        }

        [TestMethod]
        public void GetX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.X);

            float temp = data.X;

            mockData.VerifyGet(data => data.X, Times.Once);
        }

        [TestMethod]
        public void SetX()
        {
            Mock<IData> mockData = new();

            mockData.SetupSet(d => d.X = 1.0f);

            DataController data = new(mockData.Object);

            mockData.SetupSet(d => d.X = 1.0f);

            data.X = 1.0f;

            mockData.VerifySet(data => data.X = 1.0f);
        }

        [TestMethod]
        public void GetY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.Y);

            float temp = data.Y;

            mockData.VerifyGet(data => data.Y, Times.Once);
        }

        [TestMethod]
        public void SetY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupSet(d => d.Y = 1.0f);

            data.Y = 1.0f;

            mockData.VerifySet(data => data.Y = 1.0f);
        }

        [TestMethod]
        public void GetVelX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.VelX);

            float temp = data.VelX;

            mockData.VerifyGet(data => data.VelX, Times.Once);
        }

        [TestMethod]
        public void SetVelX()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupSet(d => d.VelX = 1.0f);

            data.VelX = 1.0f;

            mockData.VerifySet(data => data.VelX = 1.0f);
        }

        [TestMethod]
        public void GetVelY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupGet(d => d.VelY);

            float temp = data.VelY;

            mockData.VerifyGet(data => data.VelY, Times.Once);
        }

        [TestMethod]
        public void SetVelY()
        {
            Mock<IData> mockData = new();

            DataController data = new(mockData.Object);

            mockData.SetupSet(d => d.VelY = 1.0f);

            data.VelY = 1.0f;

            mockData.VerifySet(data => data.VelY = 1.0f);
        }

    }

}
