using Dane;
using Logika;

namespace Logika_Tests
{
    [TestClass]
    public class LogicUnitTests
    {
        [TestMethod]
        public void Start()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            Assert.IsNotNull(logic.GetCollection());
            Assert.AreEqual(1, logic.GetCollection().Count);
        }

        [TestMethod]
        public void Reset()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            logic.Reset(1, 1, 1, 1, 1);

            Assert.IsNotNull(logic.GetCollection());
            Assert.AreEqual(1, logic.GetCollection().Count);
        }

        [TestMethod]
        public void CreateDataTask()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            var data = logic.GetCollection()[0];

            Task task = logic.CreateDataTask(data);

            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void Move()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            var data = logic.GetCollection()[0];

            float oldX = data.X;
            float oldY = data.Y;

            logic.Move(data);

            Assert.AreEqual(data.X, oldX + data.VelX);
            Assert.AreEqual(data.Y, oldY + data.VelY);
        }

        [TestMethod]
        public void CheckForWallCollisions()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            var data = logic.GetCollection()[0];

            data.X = -1;
            float old = data.VelX;

            logic.CheckForWallCollisions(data);

            Assert.AreEqual(data.X, 0);
            Assert.AreEqual(data.VelX, -old);

            data.X = 2;
            old = data.VelX;

            logic.CheckForWallCollisions(data);

            Assert.AreEqual(data.X, data.Width - data.Size);
            Assert.AreEqual(data.VelX, -old);

            data.Y = -1;
            old = data.VelY;

            logic.CheckForWallCollisions(data);

            Assert.AreEqual(data.Y, 0);
            Assert.AreEqual(data.VelY, -old);

            data.Y = 2;
            old = data.VelY;

            logic.CheckForWallCollisions(data);

            Assert.AreEqual(data.Y, data.Height - data.Size);
            Assert.AreEqual(data.VelY, -old);

        }

        [TestMethod]
        public void CheckForBallCollisions()
        {
            Logic logic = new();

            logic.Start(2, 10, 100, 100, 5, 5);

            var data1 = logic.GetCollection()[0];

            float oldX1 = data1.X = 5;
            float oldY1 = data1.Y = 5;
            float oldVelX1 = data1.VelX;
            float oldVelY1 = data1.VelY;

            var data2 = logic.GetCollection()[1];

            float oldX2 = data2.X = 6;
            float oldY2 = data2.Y = 6;
            float oldVelX2 = data2.VelX;
            float oldVelY2 = data2.VelY;

            logic.CheckForBallCollisions(data1);

            if (data1.Mass == data2.Mass)
            {
                Assert.AreNotEqual(data1.X, oldX1);
                Assert.AreNotEqual(data1.Y, oldY1);
                Assert.AreEqual(data1.VelX, oldVelX2);
                Assert.AreEqual(data1.VelY, oldVelY2);

                Assert.AreNotEqual(data2.X, oldX2);
                Assert.AreNotEqual(data2.Y, oldY2);
                Assert.AreEqual(data2.VelX, oldVelX1);
                Assert.AreEqual(data2.VelY, oldVelY1);
            } 
            else
            {
                Assert.AreNotEqual(data1.X, oldX1);
                Assert.AreNotEqual(data1.Y, oldY1);
                Assert.AreNotEqual(data1.VelX, oldVelX1);
                Assert.AreNotEqual(data1.VelY, oldVelY1);

                Assert.AreNotEqual(data2.X, oldX2);
                Assert.AreNotEqual(data2.Y, oldY2);
                Assert.AreNotEqual(data2.VelX, oldVelX2);
                Assert.AreNotEqual(data2.VelY, oldVelY2);
            }
        }

        [TestMethod]
        public void Clear()
        {
            Logic logic = new();

            logic.Start(1, 1, 1, 1, 1, 1);

            logic.Clear();

            Assert.IsNotNull(logic.GetCollection());
            Assert.AreEqual(0, logic.GetCollection().Count);
        }

    }

}
