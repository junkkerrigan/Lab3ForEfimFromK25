using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class FilterOfDish
    {
        public string Name { get; set; } = "";
        public string MealTime { get; set; } = "";
        public string PresentationTime { get; set; } = "";
        public string Description { get; set; } = "";
        public float CaloriesFrom { get; set; } = -1e9F;
        public float CaloriesTo { get; set; } = 1e9F;
        public float PriceFrom { get; set; } = -1e9F;
        public float PriceTo { get; set; } = 1e9F;

        float GetFilterFloatValue(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (name == "CaloriesFrom") return -1e9F;
                else if (name == "CaloriesTo") return 1e9F;
                else if (name == "PriceFrom") return -1e9F;
                else return 1e9F;
            }
            try
            {
                var nfi = new NumberFormatInfo()
                {
                    NumberDecimalSeparator = "."
                };
                return Convert.ToSingle(value, nfi);
            }
            catch
            {
                if (name == "CaloriesFrom") return 1e9F;
                else if (name == "CaloriesTo") return -1e9F;
                else if (name == "PriceFrom") return 1e9F;
                else return -1e9F;
            }
        }

        public void ChangeFilter(string name, string value)
        {
            PropertyInfo filterToSet = GetType().GetProperty(name);
            Type filterType = filterToSet.PropertyType;
            if (filterType == typeof(string))
            {
                filterToSet.SetValue(this, value.Trim().ToLower());
            }
            else
            {
                float converted = GetFilterFloatValue(name, value);
                filterToSet.SetValue(this, converted);
            }
        }

        public bool CheckDish(Dish possible)
        {
            var nfi = new NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            };
            Console.WriteLine(Convert.ToSingle(possible.Price, nfi) >= PriceFrom);
            Console.WriteLine(Convert.ToSingle(possible.Price, nfi) <= PriceTo);
            Console.WriteLine(Convert.ToSingle(possible.Calories, nfi) >= CaloriesFrom);
            Console.WriteLine(Convert.ToSingle(possible.Calories, nfi) <= CaloriesTo);
            var match = (possible.Name.ToLower().Contains(Name.ToLower())
                && possible.MealTime.ToLower().Contains(MealTime.ToLower())
                && possible.PresentationTime.ToLower().Contains(PresentationTime.ToLower())
                && possible.Description.ToLower().Contains(Description.ToLower())
                && (Convert.ToSingle(possible.Price, nfi) >= PriceFrom)
                && (Convert.ToSingle(possible.Price, nfi) <= PriceTo)
                && (Convert.ToSingle(possible.Calories, nfi) >= CaloriesFrom)
                && (Convert.ToSingle(possible.Calories, nfi) <= CaloriesTo));
            return match;
        }
    }
}
