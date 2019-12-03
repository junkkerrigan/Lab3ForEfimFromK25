using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Dish
    {
        public string DishToString(int number)
        {
            return
                $"#{number}\n" +
                $"Name: {Name}\n" +
                $"Description: {Description}\n" +
                $"Calories: {Calories}\n" +
                $"Preffered mealtime: {MealTime}\n" +
                $"Time of presentaton: {PresentationTime}\n" +
                $"Price: {Price}\n";
        }
        public string Name { get; set; } = "";
        public string Price { get; set; } = "";
        public string Description { get; set; } = "";
        public string Calories { get; set; } = "";
        public string MealTime { get; set; } = "";
        public string PresentationTime { get; set; } = "";
    }
}
