using RestaurantOrderApi.Models.Enums;

namespace RestaurantOrderApi.Models
{
    public class MorningMenu : MenuBase
    {
        public MorningMenu(string[] orderFields) : base(orderFields)
        { }

        public override string TimeOfDay => "morning";

        protected override Dish[] AvailableDishes => new Dish[]
        {
            new Dish(DishType.Entree, "eggs"),
            new Dish(DishType.Side, "toast"),
            new Dish(DishType.Drink, "coffee", true)
        };
    }
}
