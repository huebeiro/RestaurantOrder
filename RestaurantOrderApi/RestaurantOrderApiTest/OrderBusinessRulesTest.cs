using RestaurantOrderApi.Models;
using Xunit;

namespace RestaurantOrderApiTest
{
    public class OrderBusinessRulesTest
    {
        [Theory]
        [InlineData("MORNING, 1, 2, 3", "eggs, toast, coffee")] //Case sensitivity test
        [InlineData("morning,1,2   ,3", "eggs, toast, coffee")] //Input trimming test
        public void OrderOutputTheory(string input, string expectedOutput)
        {
            Assert.Equal(
                expectedOutput,
                new Order(input).GetOutput()
            );
        }

        [Theory]
        [InlineData("morning")] //No dishes test
        [InlineData("afternoon, 1, 2, 3")] //Invalid time of day test
        [InlineData("")] //Empty input test
        public void OrderErrorTheory(string input)
        {
            Assert.NotEmpty(
                new Order(input).Error
            );
        }
    }
}
