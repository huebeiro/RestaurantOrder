using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApiTest
{
    [TestClass]
    public class OrderBusinessRulesTest
    {
        [TestMethod]
        public void CaseSensitivityTest()
        {
            Assert.AreEqual(
                "eggs, toast, coffee",
                new Order("morning, 1, 2, 3").GetOutput()
            );
        }

        [TestMethod]
        public void TrimmingTest()
        {
            Assert.AreEqual(
                "eggs, toast, coffee",
                new Order("morning,1,2    ,3").GetOutput()
            );
        }

        [TestMethod]
        public void MalformedDishesTest()
        {
            Assert.AreNotEqual(
                string.Empty,
                new Order("morning, 1, 2,").Error
            );
        }

        [TestMethod]
        public void NoDishesTest()
        {
            Assert.AreNotEqual(
                string.Empty,
                new Order("morning").Error
            );
        }

        [TestMethod]
        public void InvalidTimeOfDayTest()
        {
            Assert.AreNotEqual(
                string.Empty,
                new Order("afternoon, 1, 2, 3").Error
            );
        }
    }
}
