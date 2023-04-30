using Logika;
using Moq;

namespace Logika_Tests
{
    [TestClass]
    public class LogicControllerUnitTests
    {
        [TestMethod]
        public void Start()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.Start(1, 2, 3, 4, 5, 6)).Verifiable();

            logic.Start(1, 2, 3, 4, 5, 6);

            mockLogic.Verify();
        }

        [TestMethod]
        public void Restart()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.Restart()).Verifiable();

            logic.Restart();

            mockLogic.Verify();
        }

        [TestMethod]
        public void Reset()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.Reset(1, 2, 3, 4, 5)).Verifiable();

            logic.Reset(1, 2, 3, 4, 5);

            mockLogic.Verify();
        }

        [TestMethod]
        public void Stop()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.Stop()).Verifiable();

            logic.Stop();

            mockLogic.Verify();
        }

        [TestMethod]
        public void Clear()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.Clear()).Verifiable();

            logic.Clear();

            mockLogic.Verify();
        }

        [TestMethod]
        public void GetCollection()
        {
            Mock<ILogic> mockLogic = new();

            LogicController logic = new(mockLogic.Object);

            mockLogic.Setup(logic => logic.GetCollection()).Verifiable();

            logic.GetCollection();

            mockLogic.Verify();
        }

    }

}
