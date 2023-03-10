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
        public string textfile = @"C:\Users\Ece\Desktop\url1.txt";
        public string url1 = @"C:\Users\Ece\Desktop\url1.txt";
        public string url2 = @"C:\Users\Ece\Desktop\url2.txt";
 
        public void UrlDegistir()
        {
            if(String.Equals(textfile, url1))
            {
                textfile = string.Copy(url2);
            }
            if (String.Equals(textfile, url2))
            {
                textfile = string.Copy(url1);
            }

        }

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
                            Cells[i, j] = new Block2();

                            if (txt[a] == '3')
                            Cells[i, j] = new Block3();

                           a++;
                        }
                         

                       

                      

                    }
                }
            }
            GridGenerate(control, satir, sutun);
            
        }

        public async void GridGenerate(System.Windows.Forms.Control.ControlCollection control, int satir, int sutun)
        {
            int start_X = 0, start_Y = 0, end_X = 0, end_Y = 0;
            Random rand = new Random();
            int x = 0;
            while (x < 2)
            {
                int rows = Cells.GetLength(0);
                int columns = Cells.GetLength(1);

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

            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
            Robot robot = new Robot(Cells, path, mainPath, start_X, start_Y, end_X, end_Y);
            //mainPath = robot.gridSolve();
            // robot.gridSolve(start_X, start_Y, end_X, end_Y);
            mainPath.Reverse();
            foreach (Tuple<int, int> pair in mainPath)
            {
                Cells[start_X, start_Y].Button.BackColor = Color.Blue;
                Cells[pair.Item1, pair.Item2].Button.BackColor = Color.Yellow;
                await Task.Delay(250);
            }
         
                    
        }
    }
}
