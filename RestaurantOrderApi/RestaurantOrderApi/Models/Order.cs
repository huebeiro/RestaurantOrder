using System;

namespace RestaurantOrderApi.Models
{
    public class Order
    {

        private const int TimeOfDayIndex = 0;
        private const string TimeOfDayMorning = "morning";
        private const string TimeOfDayNight = "night";

        public Order(string textInput)
        {
            Input = new Input(textInput);

            switch (Input.Fields[TimeOfDayIndex]) //TODO Review OCP
            {
                case TimeOfDayMorning:
                    Menu = new MorningMenu(Input.Fields);
                    break;
                case TimeOfDayNight:
                    Menu = new NightMenu(Input.Fields);
                    break;
                default: //Invalid Time of Day
                    Error = "The inserted time of day is not valid.";
                    break;
            }
        }

        private Input Input;
        private MenuBase Menu;
        public string Error { get; private set; }

        public string GetOutput()
        {
            if (Menu == null)
                throw new InvalidOperationException();

            return Menu.FormOutput();
        }

    }
}
