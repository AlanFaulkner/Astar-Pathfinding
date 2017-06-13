using MazeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar_Pathfinding
{
    public struct Node
    {
        public int[] Location;
        public int[] Parent;
        public int G;
        public int H;
        public int F;
    };

    static class Program
    {

        public static void PrintDataTable(List<List<int>> Data)
        {
            for (int row = 0; row < Data.Count; row++)
            {
                for (int col = 0; col < Data[row].Count; col++)
                {
                    //Console.Write(Data[row][col] + " ");
                    if(Data[row][col] == 0) { Console.Write(" "); }
                    if (Data[row][col] == 1) { Console.Write("#"); }
                    if (Data[row][col] == 2) { Console.Write("."); }
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Algorithm Astar = new Algorithm();

            List<List<int>> Maze = new List<List<int>>
            {
                new List<int> {0,0,0,0,0},
                new List<int> {0,0,0,0,0},
                new List<int> {0,0,0,1,0},
                new List<int> {0,1,0,1,0},
                new List<int> {0,0,0,0,0}
            };

            MazeGenerator Mazegen = new MazeGenerator(20, 20);
            var maze = Mazegen.GenerateMaze();
            Mazegen.PrintMaze();

            var path = Astar.ShortestPath(maze, 1, 0, 6, 20);
            foreach (var step in path) { maze[step.Location[1]][step.Location[0]] = 2; }
            Console.WriteLine();
            PrintDataTable(maze);
        }
    }
}
