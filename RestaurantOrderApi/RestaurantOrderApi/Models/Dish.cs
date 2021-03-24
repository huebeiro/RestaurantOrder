using RestaurantOrderApi.Models.Enums;

namespace RestaurantOrderApi.Models
{
    public class Dish
    {

        public Dish(DishType type, string name, bool multipliable = false)
        {
            Type = type;
            Name = name;
            IsMultipliable = multipliable;
        }

        /// <summary>
        /// The type of the dish
        /// </summary>
        public DishType Type { get; }
        /// <summary>
        /// The name of the dish
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Flag to indicate whether or not a dish can be ordered more than once
        /// </summary>
        public bool IsMultipliable { get; } 
    }
}
