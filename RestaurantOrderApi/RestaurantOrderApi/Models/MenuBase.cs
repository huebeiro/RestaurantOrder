using RestaurantOrderApi.Models.Enums;
using System;
using System.Linq;
using System.Text;

namespace RestaurantOrderApi.Models
{
    /// <summary>
    /// The base class to represent an available menu
    /// </summary>
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

        /// <summary>
        /// The time of day that the dish will be operating
        /// </summary>
        public abstract string TimeOfDay { get; }

        /// <summary>
        /// The list of available dishes of the menu
        /// </summary>
        protected abstract Dish[] AvailableDishes { get; }

        /// <summary>
        /// The list of dishes in the current order
        /// </summary>
        protected int[] OrderDishes { get; }

        /// <summary>
        /// Forms an formatted output based on the inputted order data
        /// </summary>
        /// <returns>The formatted order output</returns>
        public string FormOutput()
        {
            StringBuilder output = new StringBuilder();
            var currentDishCount = 0; //Counter for dishes ordered multiple times

            for (var i = 0; i < OrderDishes.Length; i++)
            {
                if (!Enum.IsDefined(typeof(DishType), OrderDishes[i])) //Inputted DishType doesn't exist
                {
                    AppendError(output);
                    break;
                }

                Dish dish = GetDishByDishType((DishType)OrderDishes[i]);

                if (dish == null)
                {
                    //Current Menu doesn't have the inputted DishType
                    AppendError(output);
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
                    if (!dish.IsMultipliable) //Verifying if dish can be ordered more than once
                    {
                        // Same interpolation appends the item name and error indicator
                        AppendSeparator(output);
                        AppendError(output);
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
                    AppendSeparator(output);//Appending separator for next item
                }
            }

            return output.ToString();
        }

        private static void AppendSeparator(StringBuilder output)
        {
            output.Append(
                OutputSeparator
            );
        }

        private static void AppendError(StringBuilder output)
        {
            output.Append(
                OutputErrorIndicator
            );
        }

        private Dish GetDishByDishType(DishType type)
        {
            return AvailableDishes
                    .Where(x => x.Type == type)
                    .FirstOrDefault();
        }
    }
}
