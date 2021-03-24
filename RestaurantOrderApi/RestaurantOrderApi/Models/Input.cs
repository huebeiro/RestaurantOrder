namespace RestaurantOrderApi.Models
{
    /// <summary>
    /// Class to process order inputs into fields
    /// </summary>
    public class Input
    {
        private const char Separator = ','; //Rule #2 does not specify input separator will have spaces
        public Input(string input)
        {
            //Normalizing and separating Input Fields
            Fields = input.ToLower().Split(Separator);
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = Fields[i].Trim();
            }
        }

        /// <summary>
        /// The fields of the inputted string
        /// </summary>
        public string[] Fields;
    }
}
