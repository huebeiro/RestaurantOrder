using RestaurantOrderApi.Models;
using Xunit;

namespace RestaurantOrderApiTest
{
    public class SampleInputTest
    {
        [Theory]
        [InlineData("morning, 1, 2, 3", "eggs, toast, coffee")]
        [InlineData("morning, 2, 1, 3", "eggs, toast, coffee")]
        [InlineData("morning, 1, 2, 3, 4", "eggs, toast, coffee, error")]
        [InlineData("morning, 1, 2, 3, 3, 3", "eggs, toast, coffee(x3)")]
        [InlineData("night, 1, 2, 3, 4", "steak, potato, wine, cake")]
        [InlineData("night, 1, 2, 2, 4", "steak, potato(x2), cake")]
        [InlineData("night, 1, 2, 3, 5", "steak, potato, wine, error")]
        [InlineData("night, 1, 1, 2, 3, 5", "steak, error")]
        public void SampleInputTheory(string input, string expectedOutput)
        {
            Assert.Equal(
                expectedOutput,
                new Order(input).Output
            );
        }
    }
}
