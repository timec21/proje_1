using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    internal class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Location_X { get; set; }
        public int Location_Y { get; set; }
        public bool Visited { get; set; }
        public Button Button { get; set; }
        public bool Wall { get; set; }

        public Cell()
        {
            Button button = new Button();
            button.Size = new Size(40, 40);
            button.Text = "0";
            button.BackColor = Color.White;
            Button = button;
        }

        public Cell(int row, int col)
        {

            Row = row;
            Col = col;
            Visited = false;
            Wall = true;
            Button button = new Button();
            button.Size = new Size(40, 40);
            button.Text = "0";
            button.BackColor = Color.White;
            Button = button;
        }

        
    }
      
}

