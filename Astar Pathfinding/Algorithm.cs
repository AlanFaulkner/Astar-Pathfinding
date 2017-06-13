using System;
using System.Collections.Generic;
using System.Linq;

namespace Astar_Pathfinding
{
    public class Algorithm
    {
        public List<Node> ShortestPath(List<List<int>> Maze, int StartX, int StartY, int TargetX, int TargetY)
        {
            List<Node> OpenList = new List<Node> { };
            List<Node> ClosedList = new List<Node> { };

            // create starting node and add to open list
            Node Start;
            Start.Location = new int[] { StartX, StartY };
            Start.Parent = new int[] { StartX, StartY };
            Start.G = 0;
            Start.H = Math.Abs(TargetY - StartY) + Math.Abs(TargetX - StartX);
            Start.F = Start.G + Start.H;
            OpenList.Add(Start);

            while (OpenList.Any())
            {
                OpenList = OrderandRemoveDuplicates(OpenList);
                // remove best node from open list and put in closed list
                ClosedList.Add(OpenList[0]);
                OpenList.RemoveAt(0);
                var Moves = AnalyiseLocalNodes(Maze, ClosedList[ClosedList.Count-1], TargetX, TargetY);
                OpenList.AddRange(Moves);
                // terminate search if target is found else continue
                if (Moves[0].Location[0] == TargetX && Moves[0].Location[1] == TargetY) {
                    ClosedList.AddRange(Moves);
                    break; }
            }

            // build and return path
            return ReconstructPath(ClosedList);
        }

        public List<Node> ReconstructPath(List<Node> closedList)
        {
            List<Node> Path = new List<Node> { };
            Node CurrentNode = closedList[closedList.Count - 1];

            while (CurrentNode.G != 0)
            {               
                Path.Add(CurrentNode);
                CurrentNode = closedList.Find(x => x.Location[0] == CurrentNode.Parent[0] && x.Location[1]==CurrentNode.Parent[1]);
            }
            Path.Add(closedList[0]);

            return Path;
        }

        public List<Node> OrderandRemoveDuplicates(List<Node> Data)
        {
            // order list by  5 element
            // keep only on element that match 1,2 and delete the rest

            Data = Data.OrderBy(a => a.F).ToList();
            List<Node> SortedNodeList = new List<Node> { };

            for (int row = 0; row < Data.Count; row++)
            {
               if (!SortedNodeList.Exists(node => node.Location[0] == Data[row].Location[0] && node.Location[1]==Data[row].Location[1])) { SortedNodeList.Add(Data[row]); }
            }

            return SortedNodeList;
        }

        public List<Node> AnalyiseLocalNodes(List<List<int>> Maze, Node Parent, int TargetX, int TargetY)
        {
            // loop through the 9 squares centered on the location of the parent.
            // if the location is horizantal or vertical check to see if that square is clear - if yes generate new node - else ignore
            // return all accissable nodes

            List<Node> Moves = new List<Node> { };

            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = -1; Col < 2; Col++)
                {
                    int X = Clamp(Parent.Location[0] + Col, 0, Maze[0].Count - 1);
                    int Y = Clamp(Parent.Location[1] + Row, 0, Maze.Count - 1);
                    if (Maze[Y][X] == 0 && Math.Abs(Row) != Math.Abs(Col) && !(X != Parent.Location[0] && Y != Parent.Location[1]))
                    {
                        Node Move;
                        Move.Location = new int[] { X, Y };
                        Move.Parent = Parent.Location;
                        Move.G = Parent.G + 1;
                        Move.H = Math.Abs(TargetY - Y) + Math.Abs(TargetX - X);
                        Move.F = Move.G + Move.H;
                        Moves.Add(Move);
                        if (Move.H == 0)
                        {
                            Moves.Clear();
                            Moves.Add(Move);
                            return Moves;
                        }
                    }
                }
            }

            return Moves;
        }

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}