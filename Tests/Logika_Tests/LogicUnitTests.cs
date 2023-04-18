using Logika;

namespace Logika_Tests
{
    [TestClass]
    public class LogicUnitTests
    {
        [TestMethod]
        public void Start()
        {
            LogicController logic = new(new Logic());

            logic.Start(1, 1, 1, 1, 1, 1);

            Assert.AreEqual(1, logic.GetCollection().Count());
        }

        [TestMethod]
        public void Reset()
        {
            LogicController logic = new(new Logic());

            logic.Start(1, 1, 1, 1, 1, 1);

            logic.Reset(1, 1, 1, 1, 1);

            Assert.AreEqual(1, logic.GetCollection().Count());
        }

        [TestMethod]
        public void Clear()
        {
            LogicController logic = new(new Logic());

            logic.Start(1, 1, 1, 1, 1, 1);

            logic.Clear();

            Assert.AreEqual(0, logic.GetCollection().Count());
        }

    }

}
