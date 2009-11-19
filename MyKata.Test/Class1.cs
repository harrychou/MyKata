using MbUnit.Framework;

namespace MyKata.Test
{
    public class Fibonacci
    {
        public static int Calculate(int x)
        {
            if (x <= 0) return 0;
            if (x == 1) return 1;
            return Calculate(x - 1) + Calculate(x - 2);
        }
    }

    [TestFixture]
    public class FibonacciTest
    {
        [Test]
        public void FibonacciOfNumberGreaterThanOne()
        {
            Assert.AreEqual(Fibonacci.Calculate(6), 8);
        }
    }
}