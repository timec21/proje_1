using System;
using System.Security.Policy;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private string textfile = @"C:\Users\Ece\Desktop\url1.txt";
        private string url1 = @"C:\Users\Ece\Desktop\url1.txt";
        private string url2 = @"C:\Users\Ece\Desktop\url2.txt";
        public int row, col;
        public Form1()
        {
            System.Windows.Forms.Control.ControlCollection control = this.Controls;

            grid g = new grid();
            g.textfile = this.textfile;
            g.TextFileReader(control);
            InitializeComponent();
            Button start = new Button();
            start.Location = new Point(370 + g.columns * 40, 100 + (g.rows * 40) / 2);
            start.Size = new Size(150, 70);
            start.Text = "Çalıştır";
            Controls.Add(start);
            start.Click += (sender, args) =>
            {
                List<Tuple<int, int>> path = new List<Tuple<int, int>>();
                List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
                Robot robot = new Robot(g.Cells, path, mainPath, g.start_X, g.start_Y, g.end_X, g.end_Y, this.Controls);
            };
            Button button = new Button();
            button.Location = new Point(200 + g.columns * 40, 100 + (g.rows * 40) / 2);
            button.Size = new Size(150, 70);
            button.Text = "URL değiştir";
            button.Click += (sender, args) =>
            {
                Controls.Clear();
                g = new grid();
                if (String.Equals(textfile, url1))
                {
                    g.textfile = string.Copy(url2);
                    textfile = string.Copy(url2);

                }
                else
                {
                    g.textfile = string.Copy(url1);
                    textfile = string.Copy(url1);
                }
                g.TextFileReader(control);
                Controls.Add(start);
                button.Location = new Point(200 + g.columns * 40, 100 + (g.rows * 40) / 2);
                start.Location = new Point(370 + g.columns * 40, 100 + (g.rows * 40) / 2);

                Controls.Add(button);
            };

            

            Controls.Add(button);



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       


    }
}