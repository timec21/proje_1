namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            System.Windows.Forms.Control.ControlCollection control = this.Controls;

            grid g = new grid();
            g.TextFileReader(control);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.ControlCollection control = this.Controls;

            grid gb = new grid();
            gb.UrlDegistir();
            gb.TextFileReader(control);
            InitializeComponent();

        }
    }
}