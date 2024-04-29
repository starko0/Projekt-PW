using NUnit.Framework;
using TPW_Project;

namespace ProgramTests
{
    public class ProgramTests
    {
        [Test]
        public void TestSum()
        {
            Assert.AreEqual(10, Program.sum(5, 5));
        }
    }
}