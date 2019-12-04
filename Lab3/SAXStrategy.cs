using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab3
{
    public class SAXStrategy : XMLAnalyzingStrategy
    {
        string FileName;
        XmlReader Reader;

        public SAXStrategy(string file)
        {
            FileName = file;
        }

        public ResultOfParsing Analyze(FilterOfDish filter)
        {
            GetXMLData(FileName);
            Dish dish = null;
            int i = 0;
            List<Dish> match = new List<Dish>();
            List<string> names = new List<string>();
            List<string> mealtimes = new List<string>();
            List<string> presentationtimes = new List<string>();

            while (Reader.Read())
            {
                switch (Reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (Reader.Name)
                        {
                            case "dish":
                                dish = new Dish();
                                break;
                            case "name":
                                Reader.Read();
                                dish.Name = Reader.Value;
                                break;
                            case "price":
                                Reader.Read();
                                dish.Price = Reader.Value;
                                break;
                            case "description":
                                Reader.Read();
                                dish.Description = Reader.Value.Trim();
                                break;
                            case "calories":
                                Reader.Read();
                                dish.Calories = Reader.Value;
                                break;
                            case "mealTime":
                                Reader.Read();
                                dish.MealTime = Reader.Value;
                                break;
                            case "presentationTime":
                                Reader.Read();
                                dish.PresentationTime = Reader.Value;
                                break;
                            default:
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (Reader.Name == "dish")
                        {
                            if (filter.CheckDish(dish))
                            {
                                mealtimes.Add(dish.MealTime);
                                names.Add(dish.Name);
                                presentationtimes.Add(dish.PresentationTime);
                                i++;
                                match.Add(dish);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return new ResultOfParsing()
            {
                DishesList = match.ToArray(),
                MealTimes = mealtimes.Distinct().ToArray(),
                Names = names.Distinct().ToArray(),
                PresentationTimes = presentationtimes.Distinct().ToArray(),
            };
        }

        public void GetXMLData(string file)
        {
            Reader = XmlReader.Create(file);
        }
    }
}
