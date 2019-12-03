using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Lab3
{
    public partial class MainWindow : Form
    {
        TextBox DishesData, DescriptionFilter, FilterForMinPrice, PriceToFilter, FilterForMinCalories, FilterForMaxCalories;
        FilterOfDish RealDataOfFilters = new FilterOfDish(); 
        
        public MainWindow()
        {
            InitializeComponent();
            DishesData = new TextBox();
            DishesData.Location = new Point(30, 60);
            DishesData.Size = new Size((Width - 120) / 2, ClientSize.Height - 80);
            SizeChanged += MainWindowSizeChanged;
            FormClosing += MainWindowClosed;
            Parser = new LINQStrategy(PathToXML);
            Controls.Add(DishesData);
            AddElements();
            AddAnalyzData();
        }
        string PathToXSLT = "../../template.xsl";
        XMLAnalyzingStrategy Parser;
        Label DishName, Description, MealTime, PresentationTime, Calories, Price, L7, L8;
        ComboBox FilterForMealTime, FilterForName, FilterForPresentationTime;
        Button Analyze, LoadData, SetEmptyFilter, XSLT;
        string PathToHTML = "../../xslt.html";
        RadioButton AnalyzeLINQ, AnalyzeDOM, AnalyzeSAX;
        string PathToXML = "../../dishes.xml";

        void AddElements()
        {
            AddFilters();
            AddLabels();
            AddControlButtons();
            AddRadioButtons();
        }
        void StrategyChanged(object sender, EventArgs e)
        {
            RadioButton selectedTool = sender as RadioButton;
            if (selectedTool.Name == "AnalyzeLINQ") Parser = new LINQStrategy(PathToXML);
            else if (selectedTool.Name == "AnalyzeDOM") Parser = new DOMStrategy(PathToXML);
            else if (selectedTool.Name == "AnalyzeSAX") Parser = new SAXStrategy(PathToXML);
        }
        void AddRadioButtons()
        {
            AnalyzeDOM = new RadioButton()
            {
                Text = "DOM",
                Name = "DOM",
                Height = 30,
                Font = new Font("Times New Roman", 12),
                Location = new Point(Analyze.Left + 10, Analyze.Bottom + 15),
            };
            AnalyzeDOM.CheckedChanged += StrategyChanged;
            Controls.Add(AnalyzeDOM);
            AnalyzeSAX = new RadioButton()
            {
                Text = "SAX",
                Name = "SAX",
                Font = new Font("Times New Roman", 12),
                Height = 30,
                Location = new Point(AnalyzeDOM.Right, AnalyzeDOM.Top),
            };
            AnalyzeSAX.CheckedChanged += StrategyChanged;
            Controls.Add(AnalyzeSAX);
            AnalyzeLINQ = new RadioButton()
            {
                Text = "LINQ",
                Name = "LINQ",
                Checked = true,
                Height = 30,
                Font = new Font("Times New Roman", 12),
                Location = new Point(AnalyzeSAX.Right, AnalyzeSAX.Top),
            };
            AnalyzeLINQ.CheckedChanged += StrategyChanged;
            Controls.Add(AnalyzeLINQ);
        }
        void AddFilters()
        {
            FilterForMealTime = new ComboBox
            {
                Location = new Point(190 + DishesData.Width, 40),
                Size = new Size(DishesData.Width - 130, 100),
                Font = new Font("Times New Roman", 12),
                Name = "MealTime",
            };

            FilterForName = new ComboBox
            {
                Location = new Point(190 + DishesData.Width,
                FilterForMealTime.Bounds.Bottom + 20),
                Size = new Size(DishesData.Width - 130, 100),
                Font = new Font("Times New Roman", 12),
                Name = "Name",
            };

            DescriptionFilter = new TextBox
            {
                Location = new Point(190 + DishesData.Width,
                FilterForName.Bounds.Bottom + 20),
                Size = new Size(DishesData.Width - 130, 100),
                Font = new Font("Times New Roman", 12),
                Name = "Description",
            };

            FilterForPresentationTime = new ComboBox
            {
                Location = new Point(190 + DishesData.Width,
                DescriptionFilter.Bounds.Bottom + 20),
                Size = new Size(DishesData.Width - 130, 100),
                Font = new Font("Times New Roman", 12),
                Name = "PresentationTime",
            };

            FilterForMinCalories = new TextBox
            {
                Location = new Point(190 + DishesData.Width,
                FilterForPresentationTime.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 10),
                Name = "CaloriesFrom",
            };

            FilterForMaxCalories = new TextBox
            {
                Location =
                new Point(FilterForMinCalories.Right + 30,
                FilterForPresentationTime.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 10),
                Name = "CaloriesTo",
            };

            FilterForMinPrice = new TextBox
            {
                Location =
                new Point(190 + DishesData.Width,
                FilterForMinCalories.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 10),
                Name = "PriceFrom",
            };

            PriceToFilter = new TextBox
            {
                Location = new Point(FilterForMinCalories.Right + 30,
                FilterForMinCalories.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 10),
                Name  = "PriceTo",
            };
            
            FilterForMealTime.TextChanged += FilterChanged;
            FilterForPresentationTime.TextChanged += FilterChanged;
            DescriptionFilter.TextChanged += FilterChanged;
            FilterForName.TextChanged += FilterChanged;
            FilterForMaxCalories.TextChanged += FilterChanged;
            FilterForMinCalories.TextChanged += FilterChanged;
            FilterForMinPrice.TextChanged += FilterChanged;
            PriceToFilter.TextChanged += FilterChanged;

            Controls.Add(FilterForMealTime);
            Controls.Add(FilterForName);
            Controls.Add(DescriptionFilter);
            Controls.Add(FilterForPresentationTime);
            Controls.Add(FilterForMinPrice);
            Controls.Add(PriceToFilter);
            Controls.Add(FilterForMinCalories);
            Controls.Add(FilterForMaxCalories);
        }
        void AddLabels()
        {
            DishName = new Label()
            {
                Text = "Author:",
                Location = new Point(DishesData.Width + 50, FilterForMealTime.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Description = new Label()
            {
                Text = "Title:",
                Location = new Point(DishesData.Width + 50, FilterForName.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            MealTime = new Label()
            {
                Text = "Description:",
                Location = new Point(DishesData.Width + 50, DescriptionFilter.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            PresentationTime = new Label()
            {
                Text = "Genre:",
                Location = new Point(DishesData.Width + 50, FilterForPresentationTime.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Calories = new Label()
            {
                Text = "Published:",
                Location = new Point(DishesData.Width + 50, FilterForMinCalories.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Price = new Label()
            {
                Text = "Price:",
                Location = new Point(DishesData.Width + 50, FilterForMinPrice.Top + 2),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            L7 = new Label()
            {
                Text = "-",
                Location = new Point(FilterForMinCalories.Right + 5, FilterForMinCalories.Top),
                Font = new Font("Times New Roman", 12),
            };
            L8 = new Label()
            {
                Text = "-",
                Location = new Point(FilterForMinPrice.Right + 5, FilterForMinPrice.Top),
                Font = new Font("Times New Roman", 12),
            };

            Controls.Add(DishName);
            Controls.Add(Description);
            Controls.Add(MealTime);
            Controls.Add(PresentationTime);
            Controls.Add(Calories);
            Controls.Add(Price);
            Controls.Add(L7);
            Controls.Add(L8);
        }
        void FilterChanged(object sender, EventArgs e)
        {
            string newValue, propName;
            if (sender is TextBox)
            {
                TextBox f = sender as TextBox;
                newValue = f.Text;
                propName = f.Name;
                RealDataOfFilters.ChangeFilter(propName, newValue);
            }
            else
            {
                ComboBox f = sender as ComboBox;
                newValue = f.Text;
                propName = f.Name;
                RealDataOfFilters.ChangeFilter(propName, newValue);
            }
        }
        void AddControlButtons()
        {
            Analyze = new Button()
            {
                Text = "Analyze",
                Location = new Point(DishName.Left + 20, FilterForMinPrice.Bottom + 30),
                Font = new Font("Times New Roman", 10),
                Size = new Size(80, 40),
            };
            Analyze.Click += (s, e) => AddAnalyzData();
            Controls.Add(Analyze);
            SetEmptyFilter = new Button()
            {
                Text = "Empty",
                Location = new Point(Analyze.Bounds.Right + 30, Analyze.Bounds.Y),
                Font = new Font("Times New Roman", 10),
                Size = new Size(80, 40),
            };
            SetEmptyFilter.Click += (s, e) => SetEmptyFilterFilters();
            Controls.Add(SetEmptyFilter);
            LoadData = new Button()
            {
                Text = "Load",
                Location = new Point(SetEmptyFilter.Bounds.Right + 30, SetEmptyFilter.Bounds.Y),
                Font = new Font("Times New Roman", 10),
                Size = new Size(100, 40),
            };
            LoadData.Click += (s, e) => LoadDataFile();
            Controls.Add(LoadData);
            XSLT = new Button()
            {
                Text = "XSLT To HTML",
                Location = new Point((DishesData.Width - 200) / 2 + 30, 20),
                Font = new Font("Times New Roman", 10),
                Size = new Size(200, 30),
            };
            XSLT.Click += (s, e) => XSLTToHTML();
            Controls.Add(XSLT);
        }

        void MainWindowSizeChanged(object sender, EventArgs e)
        {
            DishesData.Size = new Size((Width - 120) / 2, ClientSize.Height - 80);

            FilterForMealTime.Location = new Point(190 + DishesData.Width, 40);
            FilterForMealTime.Size = new Size(DishesData.Width - 130, 100);

            FilterForName.Location = new Point(190 + DishesData.Width,
                FilterForMealTime.Bounds.Bottom + 20);
            FilterForName.Size = new Size(DishesData.Width - 130, 100);

            DescriptionFilter.Location = new Point(190 + DishesData.Width,
                FilterForName.Bounds.Bottom + 20);
             DescriptionFilter.Size = new Size(DishesData.Width - 130, 100);

            FilterForPresentationTime.Location = new Point(190 + DishesData.Width,
                DescriptionFilter.Bounds.Bottom + 20);
                FilterForPresentationTime.Size = new Size(DishesData.Width - 130, 100);

            FilterForMinCalories.Location = new Point(190 + DishesData.Width,
                FilterForPresentationTime.Bounds.Bottom + 20);
            FilterForMinCalories.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            FilterForMaxCalories.Location =
                new Point(FilterForMinCalories.Right + 30,
                FilterForPresentationTime.Bounds.Bottom + 20);
            FilterForMaxCalories.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            FilterForMinPrice.Location =
                new Point(190 + DishesData.Width,
                FilterForMinCalories.Bounds.Bottom + 20);
            FilterForMinPrice.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            PriceToFilter.Location = new Point(FilterForMinCalories.Right + 30,
                FilterForMinCalories.Bounds.Bottom + 20);
            PriceToFilter.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            DishName.Location = new Point(DishesData.Width + 50, FilterForMealTime.Top + 2);
            Description.Location = new Point(DishesData.Width + 50, FilterForName.Top + 2);
            MealTime.Location = new Point(DishesData.Width + 50, DescriptionFilter.Top + 2);
            PresentationTime.Location = new Point(DishesData.Width + 50, FilterForPresentationTime.Top + 2);
            Calories.Location = new Point(DishesData.Width + 50, FilterForMinCalories.Top + 2);
            Price.Location = new Point(DishesData.Width + 50, FilterForMinPrice.Top + 2);
            L7.Location = new Point(FilterForMinCalories.Right + 5, FilterForMinCalories.Top);
            L8.Location = new Point(FilterForMinPrice.Right + 5, FilterForMinPrice.Top);

            Analyze.Location = new Point(FilterForMealTime.Right - 320 - 
                (FilterForMealTime.Right - DishName.Left - 320) / 2, 
                FilterForMinPrice.Bottom + 30);
            SetEmptyFilter.Location = new Point(Analyze.Bounds.Right + 30, Analyze.Bounds.Y);
            LoadData.Location = new Point(SetEmptyFilter.Bounds.Right + 30, SetEmptyFilter.Bounds.Y);

            AnalyzeSAX.Location = new Point(Analyze.Left + 10, Analyze.Bottom + 15);
            AnalyzeDOM.Location = new Point(AnalyzeSAX.Right, AnalyzeSAX.Top);
            AnalyzeLINQ.Location = new Point(AnalyzeDOM.Right, AnalyzeSAX.Top);

            XSLT.Location = new Point((DishesData.Width - 200) / 2 + 30, 20);
        }

        void MainWindowClosed(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to leave?", "Exit confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }


        void AddAnalyzData()
        {
            var res = Parser.Analyze(RealDataOfFilters);
            string dishesStr = "";
            int i = 0;
            foreach (var dish in res.DishesList)
            {
                i++;
                dishesStr = dishesStr + dish.DishToString(i);
                dishesStr = dishesStr + "\n";
            }
            DishesData.Text = (dishesStr == "")? "No such dishes" : dishesStr;

            FilterForMealTime.Items.Clear();
            FilterForName.Items.Clear();
            FilterForPresentationTime.Items.Clear();
            
            FilterForMealTime.Items.AddRange(res.MealTimes);
            FilterForName.Items.AddRange(res.Names);
            FilterForPresentationTime.Items.AddRange(res.PresentationTimes);
        }

        void SetEmptyFilterFilters()
        {
            RealDataOfFilters = new FilterOfDish();
            FilterForMealTime.Text = "";
            FilterForName.Text = "";
            FilterForPresentationTime.Text = "";
            DescriptionFilter.Text = ""; 
            FilterForMinPrice.Text = "";
            PriceToFilter.Text = "";
            FilterForMinCalories.Text = "";
            FilterForMaxCalories.Text = "";
            AddAnalyzData();
        }

        void LoadDataFile()
        {
            Parser.GetXMLData(PathToXML);
            AddAnalyzData();
        }

        void XSLTToHTML()
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(PathToXSLT);
            transform.Transform(PathToXML, PathToHTML);
        }
    }
}
