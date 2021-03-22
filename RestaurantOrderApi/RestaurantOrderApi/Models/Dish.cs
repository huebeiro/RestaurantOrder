using RestaurantOrderApi.Models.Enums;

namespace RestaurantOrderApi.Models
{
    public class Dish
    {

        public Dish(DishType type, string name, bool multipliable = false)
        {
            Type = type;
            Name = name;
            Multipliable = multipliable;
        }

        public DishType Type { get; }
        public string Name { get; }
        public bool Multipliable { get; }
    }
}
