using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab3
{
    public class LINQStrategy : XMLAnalyzingStrategy
    {
        List<Dish> Dishes;

        public LINQStrategy(string file)
        {
            GetXMLData(file);
        }

        public void GetXMLData(string file)
        {
            XDocument XMLData = XDocument.Load(file);
            Dishes = new List<Dish>(
                from book in XMLData.Element("menu").Elements("dish")
                select new Dish()
                {
                    Name = book.Element("name").Value,
                    MealTime = book.Element("mealTime").Value,
                    PresentationTime = book.Element("presentationTime").Value,
                    Description = book.Element("description").Value.Trim(),
                    Price = book.Element("price").Value,
                    Calories = book.Element("calories").Value,
                });
        }

        public ResultOfParsing Analyze(FilterOfDish filter)
        {
            Dish[] match = (
                from book in Dishes
                where filter.CheckDish(book)
                select book).ToArray();

            return new ResultOfParsing()
            {
                DishesList = match,
                Names = (from book in match
                         select book.Name).Distinct().ToArray(),
                MealTimes = (from book in match
                            select book.MealTime).Distinct().ToArray(),
                PresentationTimes = (from book in match
                          select book.PresentationTime)
                    .Distinct().ToArray(),
            };
        }
    }
}
