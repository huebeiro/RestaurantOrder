using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace RestaurantOrderApi.Models
{
    /// <summary>
    /// The class that represents the order based on an inputted string
    /// </summary>
    public class Order
    {
        private const int TimeOfDayIndex = 0;

        public Order(string textInput)
        {
            Input input = new Input(textInput);

            Type menuType = GetMenuTypeFromTimeOfDay(input.Fields[TimeOfDayIndex]);

            if(menuType == null) //Invalid Time of Day
            {
                Error = "The selected time of day is not valid.";
                return;
            }

            try
            {
                Menu = (MenuBase) Activator.CreateInstance(
                    menuType, 
                    new object[] { input.Fields }
                ); 
            }
            catch (TargetInvocationException ex)
            {
                HandleActivatorExceptions(ex);
            }

        }
        
        private void HandleActivatorExceptions(TargetInvocationException ex)
        {
            if (ex.InnerException.GetType() == typeof(FormatException))
            {
                Error = "One of the selected dishes is not valid.";
            }
            else if (ex.InnerException.GetType() == typeof(InvalidOperationException))
            {
                Error = "Order must have at least one item.";
            }
            else
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            }
        }

        private Type GetMenuTypeFromTimeOfDay(string timeOfDay)
        {
            foreach (var menu in AvailableMenus)
            {
                if (((MenuBase)Activator.CreateInstance(menu)).TimeOfDay.Equals(timeOfDay))
                {
                    return menu;
                }
            }
            return null;
        }

        //Private fields
        private MenuBase Menu;
        private Type[] AvailableMenus => new Type[]
        {
            typeof(MorningMenu),
            typeof(NightMenu)
        };

        //Public fields
        public string Error { get; private set; } = string.Empty;
        public string Output => Menu == null ? null : Menu.FormOutput();

    }
}
