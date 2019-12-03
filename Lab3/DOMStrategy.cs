using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab3
{
    public class DOMStrategy : XMLAnalyzingStrategy
    {
        XmlDocument xmlDoc = new XmlDocument();

        public DOMStrategy(string file)
        {
            GetXMLData(file);
        }

        public ResultOfParsing Analyze(FilterOfDish filter)
        {
            var dishNodes = xmlDoc.SelectNodes("/menu/dish");
            Console.WriteLine(dishNodes.Count);
            List<Dish> match = new List<Dish>();
            List<string> names = new List<string>();
            List<string> mealtimes = new List<string>();
            List<string> presentationtimes = new List<string>();
            foreach (XmlNode node in dishNodes)
            {
                Dish dish = new Dish()
                {
                    Name = node.SelectSingleNode("name").InnerText,
                    MealTime = node.SelectSingleNode("mealTime").InnerText,
                    PresentationTime = node.SelectSingleNode("presentationTime").InnerText,
                    Description = node.SelectSingleNode("description").InnerText.Trim(),
                    Price = node.SelectSingleNode("price").InnerText,
                    Calories = node.SelectSingleNode("calories").InnerText,
                };
                if (filter.CheckDish(dish))
                {
                    Console.WriteLine(dish.DishToString(1));
                    match.Add(dish);
                    names.Add(dish.Name);
                    mealtimes.Add(dish.MealTime);
                    presentationtimes.Add(dish.PresentationTime);
                }
            }

            return new ResultOfParsing()
            {
                DishesList = match.ToArray(),
                Names = names.Distinct().ToArray(),
                MealTimes = mealtimes.Distinct().ToArray(),
                PresentationTimes = presentationtimes.Distinct().ToArray(),
            };
        }

        public void GetXMLData(string file)
        {
            xmlDoc.Load(file);
        }
    }
}
