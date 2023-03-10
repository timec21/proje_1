using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3
{
    internal class Robot
    {
        private Cell[,] grid;
        private int rows;
        private int cols;
        private int start_X;
        private int start_Y;
        private int end_X;
        private int end_Y;
        private bool[,] visited;
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();

        public Robot(Cell[,] grid, List<Tuple<int, int>> path, List<Tuple<int, int>> mainPath, int start_X, int start_Y, int end_X, int end_Y)
        {
            this.grid = grid;
            this.path = path;
            this.mainPath = mainPath;
            this.start_X = start_X;
            this.start_Y = start_Y;
            this.end_X = end_X;    
            this.end_Y = end_Y;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
            visited = new bool[rows, cols];
            gridSolve();
        }

        public void gridSolve() 
        {
            List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
            mainPath.Add( Tuple.Create(start_X,start_Y));
            //ziyaretle ilgili bişey var
            Dictionary<int, int> parents = new Dictionary<int, int>();
            parents[start_X] = 0;
            parents[start_Y] = 0;
            while (mainPath.Count > 0)
            {
                Tuple<int, int> current_point = mainPath.Dequeue();
                if (current_point.SequenceEqual(e_point))
                {
                    break;
                }
                int i = current_point[0];
                int j = current_point[1];
                int[,] neighbors = new int[,] { { i - 1, j }, { i + 1, j }, { i, j - 1 }, { i, j + 1 } };
                for (int k = 0; k < 4; k++)
                {
                    int[] neighbor = { neighbors[k, 0], neighbors[k, 1] };
                    if (zero_points.Contains(neighbor) && !visited.Contains(neighbor))
                    {
                        q.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parents.Add(neighbor, current_point);
                    }
                }
            }




        }



    }
}
