using RestaurantOrderApi.Models.Enums;
using System;
using System.Linq;
using System.Text;

namespace RestaurantOrderApi.Models
{
    public abstract class MenuBase
    {
        protected const int DishesInputIndexStart = 1;
        protected const string OutputSeparator = ", ";
        protected const string OutputErrorIndicator = "error";
        protected MenuBase(string[] orderFields)
        {
            OrderDishes = ExtractDishes(orderFields);
        }

        protected MenuBase() { }

        private int[] ExtractDishes(string[] orderFields)
        {
            if(orderFields.Length - DishesInputIndexStart <= 0)
            {
                throw new InvalidOperationException("Order has to have at least one item.");
            }

            var strDishes =
                new string[orderFields.Length - DishesInputIndexStart];

            Array.Copy(
                orderFields,
                DishesInputIndexStart,
                strDishes,
                0,
                strDishes.Length
            );

            var dishes = Array.ConvertAll(
                strDishes,
                x => int.Parse(x)
            );

            Array.Sort(dishes);

            return dishes;
        }

        public abstract string TimeOfDay { get; }
        protected abstract Dish[] AvailableDishes { get; }
        protected int[] OrderDishes { get; }

        public string FormOutput()
        {
            StringBuilder output = new StringBuilder();
            var currentDishCount = 0; //Counter for dishes ordered multiple times

            for (var i = 0; i < OrderDishes.Length; i++)
            {
                if (!Enum.IsDefined(typeof(DishType), OrderDishes[i])) //Inputted DishType doesn't exist
                {
                    output.Append(
                        OutputErrorIndicator
                    );
                    break;
                }

                Dish dish =
                    AvailableDishes
                        .Where(x => x.Type == (DishType)OrderDishes[i])
                        .FirstOrDefault();

                if (dish == null)
                {
                    //Current Menu doesn't have the inputted DishType
                    output.Append(
                        OutputErrorIndicator
                    );
                    break;
                }

                if (i < OrderDishes.Length - 1 && OrderDishes[i] == OrderDishes[i + 1])
                {
                    //If next item is the same dish, increment counter and go to next interpolation
                    currentDishCount++;
                    continue;
                }

                output.Append( //Appending dish name
                    dish.Name
                );

                if (currentDishCount > 0)
                {
                    if (!dish.Multipliable) //Verifying if dish can be ordered more than once
                    {
                        // Same interpolation appends the item name and error indicator
                        output.Append(
                            OutputSeparator
                        );
                        output.Append(
                            OutputErrorIndicator
                        );
                        break;
                    }

                    //Displaying the total dish count and reseting the counter
                    output.Append(
                        $"(x{currentDishCount + 1})" //+1 to put into account current interpolation
                    );
                    currentDishCount = 0;
                }

                if (i < OrderDishes.Length - 1)
                {
                    output.Append( //Appending separator for next item
                       OutputSeparator
                    );
                }
            }

            return output.ToString();
        }
    }
}
