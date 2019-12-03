using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Lab3
{
    public partial class MainWindow : Form
    {
        TextBox FilterForDescription, FilterForMinPrice, FilterForMaxPrice, FilterForMinCalories, FilterForMaxCalories;
        RichTextBox DishesData;
        FilterOfDish RealDataOfFilters = new FilterOfDish(); 
        
        public MainWindow()
        {
            InitializeComponent();
            AddElements();
            Parser = new DOMStrategy(PathToXML);
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
            DishesData = new RichTextBox();
            DishesData.Location = new Point(FilterForMealTime.Right + 50, 30);
            DishesData.Size = new Size((Width - 120) / 2, ClientSize.Height - 60);
            SizeChanged += MainWindowSizeChanged;
            FormClosing += MainWindowClosed;
            Parser = new LINQStrategy(PathToXML);
            Controls.Add(DishesData);
            AddLabels();
            AddControlButtons();
            AddRadioButtons();
        }
        void StrategyChanged(object sender, EventArgs e)
        {
            RadioButton selectedTool = sender as RadioButton;
            if (selectedTool.Name == "LINQ") Parser = new LINQStrategy(PathToXML);
            else if (selectedTool.Name == "DOM") Parser = new DOMStrategy(PathToXML);
            else if (selectedTool.Name == "SAX") Parser = new SAXStrategy(PathToXML);
        }
        void AddRadioButtons()
        {
            AnalyzeDOM = new RadioButton()
            {
                Text = "DOM",
                Name = "DOM",
                Height = 30,
                Font = new Font("Times New Roman", 12),
                Checked = true,
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
                Height = 30,
                Font = new Font("Times New Roman", 12),
                Location = new Point(AnalyzeSAX.Right, AnalyzeSAX.Top),
            };
            AnalyzeLINQ.CheckedChanged += StrategyChanged;
            Controls.Add(AnalyzeLINQ);
        }
        void AddFilters()
        {
            FilterForName = new ComboBox
            {
                Location = new Point(150, 50),
                Size = new Size((Width - 120) / 2 - 130, 100),
                Font = new Font("Times New Roman", 14),
                Name = "Name",
            };
            FilterForName.TextChanged += FilterChanged;
            FilterForMealTime = new ComboBox
            {
                Location = new Point(150, FilterForName.Bounds.Bottom + 20),
                Size = new Size((Width - 120) / 2 - 130, 100),
                Font = new Font("Times New Roman", 14),
                Name = "MealTime",
            };
            FilterForMealTime.TextChanged += FilterChanged;
            FilterForDescription = new TextBox
            {
                Location = new Point(150,
                FilterForMealTime.Bounds.Bottom + 20),
                Size = new Size((Width - 120) / 2 - 130, 100),
                Font = new Font("Times New Roman", 14),
                Name = "Description",
            };
            FilterForDescription.TextChanged += FilterChanged;
            FilterForPresentationTime = new ComboBox
            {
                Location = new Point(150,
                FilterForDescription.Bounds.Bottom + 10),
                Size = new Size((Width - 120) / 2 - 130, 100),
                Font = new Font("Times New Roman", 14),
                Name = "PresentationTime",
            };
            FilterForPresentationTime.TextChanged += FilterChanged;
            FilterForMinCalories = new TextBox
            {
                Location = new Point(150,
                FilterForPresentationTime.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 12),
                Name = "CaloriesFrom",
            };
            FilterForMinCalories.TextChanged += FilterChanged;
            FilterForMaxCalories = new TextBox
            {
                Location =
                new Point(FilterForMinCalories.Right + 30,
                FilterForPresentationTime.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 12),
                Name = "CaloriesTo",
            };
            FilterForMaxCalories.TextChanged += FilterChanged;
            FilterForMinPrice = new TextBox
            {
                Location =
                new Point(FilterForMinCalories.Left,
                FilterForMinCalories.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 12),
                Name = "PriceFrom",
            };
            FilterForMinPrice.TextChanged += FilterChanged;
            FilterForMaxPrice = new TextBox
            {
                Location = new Point(FilterForMinCalories.Right + 30,
                FilterForMinCalories.Bounds.Bottom + 20),
                Size = new Size((FilterForMealTime.Width - 30) / 2, 100),
                Font = new Font("Times New Roman", 12),
                Name  = "PriceTo",
            };
            FilterForMaxPrice.TextChanged += FilterChanged;

            Controls.Add(FilterForMealTime);
            Controls.Add(FilterForName);
            Controls.Add(FilterForDescription);
            Controls.Add(FilterForPresentationTime);
            Controls.Add(FilterForMinPrice);
            Controls.Add(FilterForMaxPrice);
            Controls.Add(FilterForMinCalories);
            Controls.Add(FilterForMaxCalories);
        }
        void AddLabels()
        {
            DishName = new Label()
            {
                Text = "Name:",
                Location = new Point(10, FilterForName.Top + 6),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Description = new Label()
            {
                Text = "Description:",
                Location = new Point(10, FilterForDescription.Top + 4),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            MealTime = new Label()
            {
                Text = "Mealtime:",
                Location = new Point(10, FilterForMealTime.Top + 4),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            PresentationTime = new Label()
            {
                Text = "Time of presentation:",
                Location = new Point(10, FilterForPresentationTime.Top + 4),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Calories = new Label()
            {
                Text = "Calories:           from",
                Location = new Point(10, FilterForMinCalories.Top + 4),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            Price = new Label()
            {
                Text = "Price:                from",
                Location = new Point(10, FilterForMinPrice.Top + 4),
                Width = 140,
                Font = new Font("Times New Roman", 12),
            };
            L7 = new Label()
            {
                Text = "to",
                Location = new Point(FilterForMinCalories.Right + 5, FilterForMinCalories.Top + 4),
                Font = new Font("Times New Roman", 12),
            };
            L8 = new Label()
            {
                Text = "to",
                Location = new Point(FilterForMinPrice.Right + 5, FilterForMinPrice.Top + 4),
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
                Location = new Point(DishName.Left + 40, FilterForMinPrice.Bottom + 30),
                Font = new Font("Times New Roman", 10),
                Size = new Size(60, 40),
            };
            Analyze.Click += (s, e) => AddAnalyzData();
            Controls.Add(Analyze);
            SetEmptyFilter = new Button()
            {
                Text = "Empty",
                Location = new Point(Analyze.Bounds.Right + 10, Analyze.Bounds.Y),
                Font = new Font("Times New Roman", 10),
                Size = new Size(60, 40),
            };
            SetEmptyFilter.Click += (s, e) => SetEmptyFilterFilters();
            Controls.Add(SetEmptyFilter);
            LoadData = new Button()
            {
                Text = "Load",
                Location = new Point(SetEmptyFilter.Bounds.Right + 10, SetEmptyFilter.Bounds.Y),
                Font = new Font("Times New Roman", 10),
                Size = new Size(60, 40),
            };
            LoadData.Click += (s, e) => LoadDataFile();
            Controls.Add(LoadData);
            XSLT = new Button()
            {
                Text = "HTML",
                Location = new Point(LoadData.Right + 10, LoadData.Bounds.Y),
                Font = new Font("Times New Roman", 10),
                Size = new Size(60, 40),
            };
            XSLT.Click += (s, e) => XSLTToHTML();
            Controls.Add(XSLT);
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
        void LoadDataFile()
        {
            Parser.GetXMLData(PathToXML);
            AddAnalyzData();
        }
        void SetEmptyFilterFilters()
        {
            RealDataOfFilters = new FilterOfDish();
            FilterForMealTime.Text = "";
            FilterForName.Text = "";
            FilterForPresentationTime.Text = "";
            FilterForDescription.Text = ""; 
            FilterForMinPrice.Text = "";
            FilterForMaxPrice.Text = "";
            FilterForMinCalories.Text = "";
            FilterForMaxCalories.Text = "";
            AddAnalyzData();
        }
        void MainWindowSizeChanged(object sender, EventArgs e)
        {

            FilterForMealTime.Size = new Size((Width - 120) / 2 - 130, 100);

            DishesData.Size = new Size((Width - 120) / 2, ClientSize.Height - 60);
            DishesData.Location = new Point(FilterForMealTime.Right + 50, 30);

            FilterForName.Location = new Point(150,
                FilterForMealTime.Bounds.Bottom + 20);
            FilterForName.Size = new Size((Width - 120) / 2 - 130, 100);

            FilterForDescription.Location = new Point(150,
                FilterForName.Bounds.Bottom + 20);
             FilterForDescription.Size = new Size((Width - 120) / 2 - 130, 100);

            FilterForPresentationTime.Location = new Point(150,
                FilterForDescription.Bounds.Bottom + 20);
                FilterForPresentationTime.Size = new Size((Width - 120) / 2 - 130, 100);

            FilterForMinCalories.Location = new Point(150,
                FilterForPresentationTime.Bounds.Bottom + 20);
            FilterForMinCalories.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            FilterForMaxCalories.Location =
                new Point(FilterForMinCalories.Right + 30,
                FilterForPresentationTime.Bounds.Bottom + 20);
            FilterForMaxCalories.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            FilterForMinPrice.Location =
                new Point(FilterForMinCalories.Left,
                FilterForMinCalories.Bounds.Bottom + 20);
            FilterForMinPrice.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            FilterForMaxPrice.Location = new Point(FilterForMinCalories.Right + 30,
                FilterForMinCalories.Bounds.Bottom + 20);
            FilterForMaxPrice.Size = new Size((FilterForMealTime.Width - 30) / 2, 100);

            DishName.Location = new Point(10, FilterForMealTime.Top + 6);
            Description.Location = new Point(10, FilterForName.Top + 4);
            MealTime.Location = new Point(10, FilterForDescription.Top + 4);
            PresentationTime.Location = new Point(10, FilterForPresentationTime.Top + 4);
            Calories.Location = new Point(10, FilterForMinCalories.Top + 4);
            Price.Location = new Point(10, FilterForMinPrice.Top + 4);
            L7.Location = new Point(FilterForMinCalories.Right + 5, FilterForMinCalories.Top + 4);
            L8.Location = new Point(FilterForMinPrice.Right + 5, FilterForMinPrice.Top + 4);

            Analyze.Location = new Point(FilterForMealTime.Right - 300 - 
                (FilterForMealTime.Right - DishName.Left - 320) / 2, 
                FilterForMinPrice.Bottom + 30);
            SetEmptyFilter.Location = new Point(Analyze.Bounds.Right + 10, Analyze.Bounds.Y);
            LoadData.Location = new Point(SetEmptyFilter.Bounds.Right + 10, SetEmptyFilter.Bounds.Y);

            AnalyzeSAX.Location = new Point(Analyze.Left + 10, Analyze.Bottom + 15);
            AnalyzeDOM.Location = new Point(AnalyzeSAX.Right, AnalyzeSAX.Top);
            AnalyzeLINQ.Location = new Point(AnalyzeDOM.Right, AnalyzeSAX.Top);

            XSLT.Location = new Point(LoadData.Right + 10, LoadData.Bounds.Y);
        }
        void XSLTToHTML()
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(PathToXSLT);
            transform.Transform(PathToXML, PathToHTML);
        }
        void MainWindowClosed(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to leave?", "Exit confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
