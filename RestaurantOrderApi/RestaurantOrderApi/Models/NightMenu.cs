using RestaurantOrderApi.Models.Enums;

namespace RestaurantOrderApi.Models
{
    public class NightMenu : MenuBase
    {
        public NightMenu(string[] orderFields) : base(orderFields)
        { }

        public override string TimeOfDay => "night";

        protected override Dish[] AvailableDishes => new Dish[]
        {
            new Dish(DishType.Entree, "steak"),
            new Dish(DishType.Side, "potato", true),
            new Dish(DishType.Drink, "wine"),
            new Dish(DishType.Dessert, "cake")
        };
    }
}
