using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace Lab3
{
    public partial class MainWindow : Form
    {
        // GUI elements
        RichTextBox ContentContainer;
        ComboBox AuthorFilter, TitleFilter, GenreFilter;
        TextBox DescriptionFilter, PriceFromFilter, PriceToFilter, YearFromFilter, YearToFilter;
        RadioButton LINQ, DOM, SAX;
        Button Search, Reload, Reset, Transform;
        Label L1, L2, L3, L4, L5, L6, L7, L8;

        // hardcoded paths
        string XMLSourceFile = "../../data.xml";
        string XSLTSourceFile = "../../rules.xsl";
        string HTMLTargetFile = "../../transformed.html";

        // logic elements
        FilterOfDish CurrentFilter = new FilterOfDish(); // stores filters values
        XMLAnalyzingStrategy Parser;

        public MainWindow()
        {
            InitializeComponent();

            ContentContainer = new RichTextBox
            {
                Location = new Point(30, 60),
                Size = new Size((Width - 120) / 2, ClientSize.Height - 80)
            };
            Controls.Add(ContentContainer);
            InitializeFilters();
            InitializeLabels();
            InitializeControlButtons();
            InitializeRadioButtons();

            SizeChanged += XMLDataVisualizator_SizeChanged;
            FormClosing += XMLDataVisualizator_Closing;

            Parser = new LINQStrategy(XMLSourceFile);
            FillVizualizator();
        }

        // GUI elements initializers
        private void InitializeFilters()
        {
            AuthorFilter = new ComboBox
            {
                Location = new Point(190 + ContentContainer.Width, 40),
                Size = new Size(ContentContainer.Width - 130, 100),
                Font = new Font("Verdana", 12),
                Name = "Author",
            };

            TitleFilter = new ComboBox
            {
                Location = new Point(190 + ContentContainer.Width,
                AuthorFilter.Bounds.Bottom + 20),
                Size = new Size(ContentContainer.Width - 130, 100),
                Font = new Font("Verdana", 12),
                Name = "Title",
            };

            DescriptionFilter = new TextBox
            {
                Location = new Point(190 + ContentContainer.Width,
                TitleFilter.Bounds.Bottom + 20),
                Size = new Size(ContentContainer.Width - 130, 100),
                Font = new Font("Verdana", 12),
                Name = "Description",
            };

            GenreFilter = new ComboBox
            {
                Location = new Point(190 + ContentContainer.Width,
                DescriptionFilter.Bounds.Bottom + 20),
                Size = new Size(ContentContainer.Width - 130, 100),
                Font = new Font("Verdana", 12),
                Name = "Genre",
            };

            YearFromFilter = new TextBox
            {
                Location = new Point(190 + ContentContainer.Width,
                GenreFilter.Bounds.Bottom + 20),
                Size = new Size((AuthorFilter.Width - 30) / 2, 100),
                Font = new Font("Verdana", 10),
                Name = "YearFrom",
            };

            YearToFilter = new TextBox
            {
                Location =
                new Point(YearFromFilter.Right + 30,
                GenreFilter.Bounds.Bottom + 20),
                Size = new Size((AuthorFilter.Width - 30) / 2, 100),
                Font = new Font("Verdana", 10),
                Name = "YearTo",
            };

            PriceFromFilter = new TextBox
            {
                Location =
                new Point(190 + ContentContainer.Width,
                YearFromFilter.Bounds.Bottom + 20),
                Size = new Size((AuthorFilter.Width - 30) / 2, 100),
                Font = new Font("Verdana", 10),
                Name = "PriceFrom",
            };

            PriceToFilter = new TextBox
            {
                Location = new Point(YearFromFilter.Right + 30,
                YearFromFilter.Bounds.Bottom + 20),
                Size = new Size((AuthorFilter.Width - 30) / 2, 100),
                Font = new Font("Verdana", 10),
                Name  = "PriceTo",
            };
            
            AuthorFilter.TextChanged += FilterChanged;
            GenreFilter.TextChanged += FilterChanged;
            DescriptionFilter.TextChanged += FilterChanged;
            TitleFilter.TextChanged += FilterChanged;
            YearToFilter.TextChanged += FilterChanged;
            YearFromFilter.TextChanged += FilterChanged;
            PriceFromFilter.TextChanged += FilterChanged;
            PriceToFilter.TextChanged += FilterChanged;

            Controls.Add(AuthorFilter);
            Controls.Add(TitleFilter);
            Controls.Add(DescriptionFilter);
            Controls.Add(GenreFilter);
            Controls.Add(PriceFromFilter);
            Controls.Add(PriceToFilter);
            Controls.Add(YearFromFilter);
            Controls.Add(YearToFilter);
        }

        private void InitializeRadioButtons()
        {
            SAX = new RadioButton()
            {
                Text = "SAX",
                Name = "SAX",
                Font = new Font("Verdana", 12),
                Height = 30,
                Location = new Point(Search.Left + 10, Search.Bottom + 15),
            };
            DOM = new RadioButton()
            {
                Text = "DOM",
                Name = "DOM",
                Height = 30,

                Font = new Font("Verdana", 12),
                Location = new Point(SAX.Right, SAX.Top),
            };
            LINQ = new RadioButton()
            {
                Text = "LINQ",
                Name = "LINQ",
                Checked = true,
                Height = 30,

                Font = new Font("Verdana", 12),
                Location = new Point(DOM.Right, SAX.Top),
            };
            

            LINQ.CheckedChanged += RadioButton_CheckedChanged;
            DOM.CheckedChanged += RadioButton_CheckedChanged;
            SAX.CheckedChanged += RadioButton_CheckedChanged;

            Controls.Add(LINQ);
            Controls.Add(DOM);
            Controls.Add(SAX);
        }

        private void InitializeControlButtons()
        {
            Search = new Button()
            {
                Text = "Search",
                Location = new Point(L1.Left + 20, PriceFromFilter.Bottom + 30),
                Font = new Font("Verdana", 10),
                Size = new Size(80, 40),
            };
            Reset = new Button()
            {
                Text = "Reset",
                Location = new Point(Search.Bounds.Right + 30, Search.Bounds.Y),
                Font = new Font("Verdana", 10),
                Size = new Size(80, 40),
            };
            Reload = new Button()
            {
                Text = "Reload",
                Location = new Point(Reset.Bounds.Right + 30, Reset.Bounds.Y),
                Font = new Font("Verdana", 10),
                Size = new Size(100, 40),
            };

            Search.Click += (s, e) => FillVizualizator();
            Reload.Click += (s, e) => ReloadFile();
            Reset.Click += (s, e) => ResetFilters();

            Transform = new Button()
            {
                Text = "Convert to HTML",
                Location = new Point((ContentContainer.Width - 200) / 2 + 30, 20),
                Font = new Font("Verdana", 10),
                Size = new Size(200, 30),
            };

            Transform.Click += (s, e) => TransformToHTML();

            Controls.Add(Search);
            Controls.Add(Reload);
            Controls.Add(Reset);
            Controls.Add(Transform);
        }

        private void InitializeLabels()
        {
            L1 = new Label()
            {
                Text = "Author:",
                Location = new Point(ContentContainer.Width + 50, AuthorFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L2 = new Label()
            {
                Text = "Title:",
                Location = new Point(ContentContainer.Width + 50, TitleFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L3 = new Label()
            {
                Text = "Description:",
                Location = new Point(ContentContainer.Width + 50, DescriptionFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L4 = new Label()
            {
                Text = "Genre:",
                Location = new Point(ContentContainer.Width + 50, GenreFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L5 = new Label()
            {
                Text = "Published:",
                Location = new Point(ContentContainer.Width + 50, YearFromFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L6 = new Label()
            {
                Text = "Price:",
                Location = new Point(ContentContainer.Width + 50, PriceFromFilter.Top + 2),
                Width = 140,
                Font = new Font("Verdana", 12),
            };
            L7 = new Label()
            {
                Text = "-",
                Location = new Point(YearFromFilter.Right + 5, YearFromFilter.Top),
                Font = new Font("Verdana", 12),
            };
            L8 = new Label()
            {
                Text = "-",
                Location = new Point(PriceFromFilter.Right + 5, PriceFromFilter.Top),
                Font = new Font("Verdana", 12),
            };

            Controls.Add(L1);
            Controls.Add(L2);
            Controls.Add(L3);
            Controls.Add(L4);
            Controls.Add(L5);
            Controls.Add(L6);
            Controls.Add(L7);
            Controls.Add(L8);
        }
        
        // event handlers
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedTool = sender as RadioButton;
            if (selectedTool.Name == "LINQ") Parser = new LINQStrategy(XMLSourceFile);
            else if (selectedTool.Name == "DOM") Parser = new DOMStrategy(XMLSourceFile);
            else if (selectedTool.Name == "SAX") Parser = new SAXStrategy(XMLSourceFile);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            string newValue, propName;
            if (sender is TextBox)
            {
                TextBox f = sender as TextBox;
                newValue = f.Text;
                propName = f.Name;
                CurrentFilter.ChangeFilter(propName, newValue);
            }
            else
            {
                ComboBox f = sender as ComboBox;
                newValue = f.Text;
                propName = f.Name;
                CurrentFilter.ChangeFilter(propName, newValue);
            }
        }

        private void XMLDataVisualizator_SizeChanged(object sender, EventArgs e)
        {
            ContentContainer.Size = new Size((Width - 120) / 2, ClientSize.Height - 80);

            AuthorFilter.Location = new Point(190 + ContentContainer.Width, 40);
            AuthorFilter.Size = new Size(ContentContainer.Width - 130, 100);

            TitleFilter.Location = new Point(190 + ContentContainer.Width,
                AuthorFilter.Bounds.Bottom + 20);
            TitleFilter.Size = new Size(ContentContainer.Width - 130, 100);

            DescriptionFilter.Location = new Point(190 + ContentContainer.Width,
                TitleFilter.Bounds.Bottom + 20);
             DescriptionFilter.Size = new Size(ContentContainer.Width - 130, 100);

            GenreFilter.Location = new Point(190 + ContentContainer.Width,
                DescriptionFilter.Bounds.Bottom + 20);
                GenreFilter.Size = new Size(ContentContainer.Width - 130, 100);

            YearFromFilter.Location = new Point(190 + ContentContainer.Width,
                GenreFilter.Bounds.Bottom + 20);
            YearFromFilter.Size = new Size((AuthorFilter.Width - 30) / 2, 100);

            YearToFilter.Location =
                new Point(YearFromFilter.Right + 30,
                GenreFilter.Bounds.Bottom + 20);
            YearToFilter.Size = new Size((AuthorFilter.Width - 30) / 2, 100);

            PriceFromFilter.Location =
                new Point(190 + ContentContainer.Width,
                YearFromFilter.Bounds.Bottom + 20);
            PriceFromFilter.Size = new Size((AuthorFilter.Width - 30) / 2, 100);

            PriceToFilter.Location = new Point(YearFromFilter.Right + 30,
                YearFromFilter.Bounds.Bottom + 20);
            PriceToFilter.Size = new Size((AuthorFilter.Width - 30) / 2, 100);

            L1.Location = new Point(ContentContainer.Width + 50, AuthorFilter.Top + 2);
            L2.Location = new Point(ContentContainer.Width + 50, TitleFilter.Top + 2);
            L3.Location = new Point(ContentContainer.Width + 50, DescriptionFilter.Top + 2);
            L4.Location = new Point(ContentContainer.Width + 50, GenreFilter.Top + 2);
            L5.Location = new Point(ContentContainer.Width + 50, YearFromFilter.Top + 2);
            L6.Location = new Point(ContentContainer.Width + 50, PriceFromFilter.Top + 2);
            L7.Location = new Point(YearFromFilter.Right + 5, YearFromFilter.Top);
            L8.Location = new Point(PriceFromFilter.Right + 5, PriceFromFilter.Top);

            Search.Location = new Point(AuthorFilter.Right - 320 - 
                (AuthorFilter.Right - L1.Left - 320) / 2, 
                PriceFromFilter.Bottom + 30);
            Reset.Location = new Point(Search.Bounds.Right + 30, Search.Bounds.Y);
            Reload.Location = new Point(Reset.Bounds.Right + 30, Reset.Bounds.Y);

            SAX.Location = new Point(Search.Left + 10, Search.Bottom + 15);
            DOM.Location = new Point(SAX.Right, SAX.Top);
            LINQ.Location = new Point(DOM.Right, SAX.Top);

            Transform.Location = new Point((ContentContainer.Width - 200) / 2 + 30, 20);
        }

        private void XMLDataVisualizator_Closing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to leave?", "Exit confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }


        // logic implementation
        private void FillVizualizator() // fills data depending on current filters
        {
            var res = Parser.Analyze(CurrentFilter);
            ContentContainer.Text = 
                String.IsNullOrWhiteSpace(res.DishesList)
                ? "Books not found :("
                : res.DishesList;

            AuthorFilter.Items.Clear();
            TitleFilter.Items.Clear();
            GenreFilter.Items.Clear();
            
            AuthorFilter.Items.AddRange(res.MealTime.ToArray());
            TitleFilter.Items.AddRange(res.Names.ToArray());
            GenreFilter.Items.AddRange(res.Genres.ToArray());
        }

        private void ResetFilters()
        {
            CurrentFilter = new FilterOfDish();
            AuthorFilter.Text = "";
            TitleFilter.Text = "";
            GenreFilter.Text = "";
            DescriptionFilter.Text = ""; 
            PriceFromFilter.Text = "";
            PriceToFilter.Text = "";
            YearFromFilter.Text = "";
            YearToFilter.Text = "";
            FillVizualizator();
        }

        private void ReloadFile()
        {
            Parser.GetXMLData(XMLSourceFile);
            FillVizualizator();
        }

        private void TransformToHTML()
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.GetXMLData(XSLTSourceFile);
            transform.Transform(XMLSourceFile, HTMLTargetFile);
        }
    }
}
