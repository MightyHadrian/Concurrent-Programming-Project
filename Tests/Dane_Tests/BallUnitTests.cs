using Dane;

namespace Dane_Tests
{
    [TestClass]
    public class BallUnitTests
    {
        [TestMethod]
        public void Move()
        {
            DataController data = new(new Ball())
            {
                X = 1.0f,
                Y = 1.0f,

                VelX = 1.0f,
                VelY = 1.0f
            };

            data.Move();

            Assert.AreEqual(data.X, 2.0f);
            Assert.AreEqual(data.Y, 2.0f);
        }

    }

}
