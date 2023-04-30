using Dane;

namespace Dane_Tests
{
    [TestClass]
    public class BallUnitTests
    {
        [TestMethod]
        public void Constructor()
        {
            Ball data = new(1, 50, 50, 1, 1);

            Assert.AreNotEqual(data.Size, 1);
            Assert.AreNotEqual(data.Size, 12);
            Assert.AreEqual(data.Width, 50);
            Assert.AreEqual(data.Height, 50);
            Assert.AreNotEqual(data.Mass, 0.0f);
            Assert.AreNotEqual(data.Mass, 12.0f);
        }

        [TestMethod]
        public void GetWidth()
        {
            Ball data = new(1, 50, 50, 1, 1);

            Assert.AreEqual(data.Width, 50);
        }

        [TestMethod]
        public void GetHeight()
        {
            Ball data = new(1, 50, 50, 1, 1);

            Assert.AreEqual(data.Height, 50);
        }

        [TestMethod]
        public void GetSize()
        {
            Ball data = new(1, 50, 50, 1, 1);

            Assert.AreNotEqual(data.Size, 1);
            Assert.AreNotEqual(data.Size, 12);
        }

        [TestMethod]
        public void GetMass()
        {
            Ball data = new(1, 50, 50, 1, 1);

            Assert.AreNotEqual(data.Mass, 0.0f);
            Assert.AreNotEqual(data.Mass, 12.0f);
        }

        [TestMethod]
        public void GetSetX()
        {
            Ball data = new(1, 50, 50, 1, 1)
            {
                X = 1.0f
            };

            Assert.AreEqual(data.X, 1.0f);
        }

        [TestMethod]
        public void GetSetY()
        {
            Ball data = new(1, 50, 50, 1, 1)
            {
                Y = 1.0f
            };

            Assert.AreEqual(data.Y, 1.0f);
        }

        [TestMethod]
        public void GetSetVelX()
        {
            Ball data = new(1, 50, 50, 1, 1)
            {
                VelX = 1.0f
            };

            Assert.AreEqual(data.VelX, 1.0f);
        }

        [TestMethod]
        public void GetSetVelY()
        {
            Ball data = new(1, 50, 50, 1, 1)
            {
                VelY = 1.0f
            };

            Assert.AreEqual(data.VelY, 1.0f);
        }

    }

}
