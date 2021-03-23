using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace RestaurantOrderApi.Models
{
    public class Order
    {

        private const int TimeOfDayIndex = 0;

        public Order(string textInput)
        {
            Input = new Input(textInput);

            var menuType = GetMenuFromTimeOfDay(Input.Fields[TimeOfDayIndex]);

            if(menuType == null) //Invalid Time of Day
            {
                Error = "The selected time of day is not valid.";
                return;
            }

            try
            {
                Menu = (MenuBase)Activator.CreateInstance(menuType, new object[] { Input.Fields }); 
            }
            catch (TargetInvocationException ex) 
            {
                if (ex.InnerException.GetType() == typeof(FormatException))
                {
                    Error = "One of the selected dishes is not valid.";
                } 
                else if(ex.InnerException.GetType() == typeof(InvalidOperationException))
                {
                    Error = "Order must have at least one item.";
                }
                else
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }
            }
            
        }

        private Input Input;
        private MenuBase Menu;
        public string Error { get; private set; } = string.Empty;
        public Type[] AvailableMenus => new Type[]
        {
            typeof(MorningMenu),
            typeof(NightMenu)
        };

        public Type GetMenuFromTimeOfDay(string timeOfDay)
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

        public string GetOutput()
        {
            if (Menu == null)
                throw new InvalidOperationException();

            return Menu.FormOutput();
        }

    }
}
