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
        int pathTime = 0;
       // private bool[,] visited;
        private int delay = 300;
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        List<Tuple<int, int>> mainPath = new List<Tuple<int, int>>();
        List<Tuple<int, int>> bfsPath = new List<Tuple<int, int>>();
        HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
        Queue<(int, int)> q = new Queue<(int, int)>();
        Dictionary<Tuple<int, int>, Tuple<int, int>> parent = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
        public System.Windows.Forms.Control.ControlCollection Controls;

        public Robot(Cell[,] grid, List<Tuple<int, int>> path, List<Tuple<int, int>> mainPath, int start_X, int start_Y, int end_X, int end_Y, System.Windows.Forms.Control.ControlCollection control)
        {
            this.Controls = control;
            this.grid = grid;
            this.path = path;
            this.mainPath = mainPath;
            this.start_X = start_X;
            this.start_Y = start_Y;
            this.end_X = end_X;
            this.end_Y = end_Y;
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);
            // visited = new bool[rows, cols];
            pathTime = 0;
            gridSolve();
        }
        public void gridSolve()
        {

            // başlangıç ve bitiş noktalarını belirleyelim
            Tuple<int, int> start = Tuple.Create(start_X, start_Y);
            Tuple<int, int> end = Tuple.Create(end_X, end_Y);
            parent[start] = null;
            // BFS algoritması için gereken kuyruk (queue) ve ziyaret edilen (visited) noktaları tanımlayalım
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
         

            // başlangıç noktasını kuyruğa ekleyelim ve ziyaret edildi olarak işaretleyelim
            queue.Enqueue(start);
            visited.Add(start);

            // ana döngü
            while (queue.Count > 0)
            {
                // kuyruktan bir sonraki noktayı alalım
                Tuple<int, int> currentPoint = queue.Dequeue();

                // eğer bitiş noktasına ulaşılmışsa döngüyü sonlandıralım
                if (currentPoint.Equals(end))
                {
                    break;
                }

                // mevcut noktanın komşularını bulalım
                int i = currentPoint.Item1;
                int j = currentPoint.Item2;
                List<Tuple<int, int>> neighbors = new List<Tuple<int, int>> {
                Tuple.Create(i-1, j),
                Tuple.Create(i+1, j),
                Tuple.Create(i, j-1),
                Tuple.Create(i, j+1)
            };

                // add unvisited neighbors to queue and mark as visited
                
                foreach (Tuple<int, int> neighbor in neighbors)
                {
                    if (IsValid(neighbor) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parent[neighbor] = currentPoint;
                        //mainPath.Add(currentPoint);
                        


                       
                    }
                }


            }

            // print shortest path
            List<Tuple<int, int>> path = GetShortestPath(start, end);
            Console.Write("Shortest path: ");
            foreach (Tuple<int, int> point in path)
            {
                Console.Write("({0}, {1}) ", point.Item1, point.Item2);

            }
            foreach(Tuple<int, int> addneighbors in path)
            {
                int i = addneighbors.Item1;
                int j = addneighbors.Item2;
                if(addneighbors == end || addneighbors == start)
                {
                    bfsPath.Add(addneighbors);
                }
                else
                {
                    bfsPath = new List<Tuple<int, int>>(){
                    Tuple.Create(i, j),   
                    Tuple.Create(i - 1, j),
                    Tuple.Create(i + 1, j),
                    Tuple.Create(i, j - 1),
                    Tuple.Create(i, j + 1)
                };
                }
               
                foreach(Tuple<int, int> tuple in bfsPath)
            {
                if(visited.Contains(tuple))
                {
                    mainPath.Add(tuple);
                    pathTime = pathTime + 1;
                    visited.Remove(tuple);
                }
            }
            }

            



            Button result = new Button();
            result.Text = "Sonuç Göster";
            result.Size = new Size(150, 70);
            result.Location = new Point(200 + cols * 40, 20 + (rows * 40) / 2);
            Controls.Add(result);

            result.Click += (sender, args) =>
            {
                delay = 0;
            };

            showPath();
                
        }

        // helper function to get shortest path as a list of points
        public List<Tuple<int, int>> GetShortestPath(Tuple<int, int> start, Tuple<int, int> end)
        {
            //List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            Tuple<int, int> current = end;
            while (!current.Equals(start))
            {
                path.Add(current);
                // break;
                current = parent[current];
            }
            path.Add(start);
            path.Reverse();
            return path;
        }


        private bool IsValid(Tuple<int, int> point)
        {
            int i = point.Item1;
            int j = point.Item2;
           /* if (grid[i, j].Button.BackColor == Color.Green)
            {
                return true;
            } */
            return i >= 0 && i < rows && j >= 0 && j < cols && (string.Equals(grid[i, j].Button.Text, "0") || grid[i, j].Button.BackColor == Color.Green || string.Equals(grid[i, j].Button.Text, "Finish") || string.Equals(grid[i, j].Button.Text, "Start"));
        }

        private async void showPath()
        {
            Button time = new Button();
            time.Location = new Point(370 + cols * 40, 20 + (rows * 40) / 2);
            time.Text = "Süre: ";
            time.Size = new Size(150, 70);
            Controls.Add(time);


            foreach (Tuple<int, int> pair in mainPath)
            {
                grid[start_X, start_Y].Button.BackColor = Color.Blue;
                grid[pair.Item1, pair.Item2].Button.BackColor = Color.Yellow;
                grid[end_X, end_Y].Button.BackColor = Color.Red;



                await Task.Delay(delay);
            }
            foreach (Tuple<int, int> pair in path)
            {
                grid[start_X, start_Y].Button.BackColor = Color.Blue;
                grid[pair.Item1, pair.Item2].Button.BackColor = Color.Purple;
                grid[end_X, end_Y].Button.BackColor = Color.Red;



                await Task.Delay(delay/2);
            }

            time.Text = "Süre: " + pathTime + " sn";
        }





    }
}
