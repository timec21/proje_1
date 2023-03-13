using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    internal class grid
    {
        public Cell[,] Cells;
        public string text = "";
        public string txt = "";
        public string textfile;
        public string url1 = @"C:\Users\Ece\Desktop\url1.txt";
        public string url2 = @"C:\Users\Ece\Desktop\url2.txt";
        public int rows, columns;
        public int start_X = 0, start_Y = 0, end_X = 0, end_Y = 0;


        public void TextFileReader(System.Windows.Forms.Control.ControlCollection control)
        {

    

            StreamReader streamReader = File.OpenText(textfile);


            while ((text = streamReader.ReadLine()) != null)
            {
                txt = text + txt;
            }
            int satir = File.ReadAllLines(textfile).Length;
           
            streamReader.Close();
            int a = 0;
            int sutun = (txt.Length) / satir;
            satir = satir + 2;
            sutun = sutun + 2;
            Cells = new Cell[satir, sutun];
            
            

            while (a < txt.Length)
            {
                for (int i = satir - 1; i >= 0; i--)
                {
                    for (int j = 0; j < sutun; j++)
                    {
                        if (i == 0 || i == satir-1 || j == 0 || j == sutun-1)
                            Cells[i, j] = new Wall();
                        else
                        {
                            if (txt[a] == '0')
                            Cells[i, j] = new Cell();
                      
                            if (txt[a] == '1')
                            Cells[i, j] = new Block1();
                     
                            if (txt[a] == '2')
                                if (Cells[i, j] == null)
                                {
                                    Cells[i, j] = new Block2();
                                    Cells[i, j+1] = new Block_2();
                                }
                           
                           
                            if (txt[a] == '3')
                                if (Cells[i, j] == null)
                                { 
                                    Cells[i, j] = new Block3();
                                    Cells[i-1, j] = new Block_3();
                                    Cells[i-2, j] = new Block_3();                        
                                }
                                                                     

                            a++;
                        }
                         

                                      
                    }
                }
            }
            GridGenerate(control, satir, sutun);
            
        }

        public async void GridGenerate(System.Windows.Forms.Control.ControlCollection control, int satir, int sutun)
        {

            Random rand = new Random();
            int x = 0;
            while (x < 2)
            {
                rows = Cells.GetLength(0);
                columns = Cells.GetLength(1);

                int randomRow = rand.Next(rows);
                int randomColumn = rand.Next(columns);

                if (Cells[randomRow, randomColumn].Button.Text == "0" && x == 1)
                {
                    Cells[randomRow, randomColumn].Button.BackColor = Color.Red;
                    Cells[randomRow, randomColumn].Button.Text = "Finish";
                    end_X = randomRow;
                    end_Y = randomColumn;
                    x++;
                }

                if (Cells[randomRow, randomColumn].Button.Text == "0" && x == 0)
                {
                    Cells[randomRow, randomColumn].Button.BackColor = Color.Blue;              
                    Cells[randomRow, randomColumn].Button.Text = "Start";
                    start_X = randomRow;
                    start_Y = randomColumn;
                    x++;
                }
            }


            for (int i = 0; i < satir; i++)
            {
                for (int j = 0; j < sutun; j++)
                {
                    Cells[i, j].Button.Location = new Point(100 + j * 40, 100 + i * 40);
                    control.Add(Cells[i, j].Button);
                }

            }

          
                    
        }
    }
}
