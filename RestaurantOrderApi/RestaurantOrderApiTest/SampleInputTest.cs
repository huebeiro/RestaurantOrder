using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApiTest
{
    [TestClass]
    public class SampleInputTest
    {
        [TestMethod]
        public void Sample1()
        {
            Assert.AreEqual(
                "eggs, toast, coffee",
                new Order("morning, 1, 2, 3").GetOutput()
            );
        }

        [TestMethod]
        public void Sample2()
        {
            Assert.AreEqual(
                "eggs, toast, coffee",
                new Order("morning, 2, 1, 3").GetOutput()
            );
        }

        [TestMethod]
        public void Sample3()
        {
            Assert.AreEqual(
                "eggs, toast, coffee, error",
                new Order("morning, 1, 2, 3, 4").GetOutput()
            );
        }

        [TestMethod]
        public void Sample4()
        {
            Assert.AreEqual(
                "eggs, toast, coffee(x3)",
                new Order("morning, 1, 2, 3, 3, 3").GetOutput()
            );
        }

        [TestMethod]
        public void Sample5()
        {
            Assert.AreEqual(
                "steak, potato, wine, cake",
                new Order("night, 1, 2, 3, 4").GetOutput()
            );
        }

        [TestMethod]
        public void Sample6()
        {
            Assert.AreEqual(
                "steak, potato(x2), cake",
                new Order("night, 1, 2, 2, 4").GetOutput()
            );
        }

        [TestMethod]
        public void Sample7()
        {
            Assert.AreEqual(
                "steak, potato, wine, error",
                new Order("night, 1, 2, 3, 5").GetOutput()
            );
        }

        [TestMethod]
        public void Sample8()
        {
            Assert.AreEqual(
                "steak, error",
                new Order("night, 1, 1, 2, 3, 5").GetOutput()
            );
        }
    }
}
