using Projekt;

namespace Projekt_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test test = new();

            Assert.AreEqual(1, test.TestFunction(), "Poprawnie");
        }
    }
}