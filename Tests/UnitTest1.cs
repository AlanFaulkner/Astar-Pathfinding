using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Astar_Pathfinding;
using System.Collections.Generic;

namespace Tests
{
    

    [TestClass]
    public class UnitTest1
    {       

        [TestMethod]
        public void SortandRemoveDuplicatesFromTable()
        {
            // test to see that a list of nodes is sorted based on the F value  
            // nodes with the same location but different F values are pruned so only the one iwht the lowest f value is left

            //Arrange
            Algorithm Astar = new Algorithm();            
            List<Node> Data = new List<Node> { };
            Node a, b, c;
            a.Location = new int[] { 2, 2 };
            a.Parent = new int[] { 1, 1 };
            a.G = 0;
            a.H = 0;
            a.F = 1;
            Data.Add(a);

            b.Location = new int[] { 1, 1 };
            b.Parent = new int[] { 1, 1 };
            b.G = 0;
            b.H = 0;
            b.F = 1;
            Data.Add(b);

            c.Location = new int[] { 1, 1 };
            c.Parent = new int[] { 1, 1 };
            c.G = 0;
            c.H = 0;
            c.F = 3;
            Data.Add(c);

            List < Node > expected = new List<Node> { b, a };

            //Act
            Data = Astar.OrderandRemoveDuplicates(Data);

            //asert
            PrintNodes(Data);
            CollectionAssert.AreEqual(expected, Data);
        }

        [TestMethod]
        public void AnalyiseLocalEnviroment()
        {
            // for a given maze check that function returns all possible nodes
            Algorithm Astar = new Algorithm();
            List<List<int>> Maze = new List<List<int>>
            {
                new List<int> {0,0,0,0,0},
                new List<int> {0,0,0,0,0},
                new List<int> {0,0,0,1,0},
                new List<int> {0,1,0,1,0},
                new List<int> {0,0,0,0,0}
            };
            Node Parent;
            Parent.Location = new int[] { 1, 1 };
            Parent.Parent = new int[] { 0, 0 };
            Parent.G = 0;
            Parent.H = 0;
            Parent.F = 0;

            int TargetX = 4;
            int TargetY = 4;

        List<Node> Expected = new List<Node> { };
            Node a, b, c, d;
            a.Location = new int[] { 1, 0 };
            a.Parent = new int[] { 1, 1 };
            a.G = 1;
            a.H = 7;
            a.F = 8;
            Expected.Add(a);

            b.Location = new int[] { 0, 1 };
            b.Parent = new int[] { 1, 1 };
            b.G = 1;
            b.H = 7;
            b.F = 8;
            Expected.Add(b);

            c.Location = new int[] { 2, 1 };
            c.Parent = new int[] { 1, 1 };
            c.G = 1;
            c.H = 5;
            c.F = 6;
            Expected.Add(c);

            d.Location = new int[] { 1, 2 };
            d.Parent = new int[] { 1, 1 };
            d.G = 1;
            d.H = 5;
            d.F = 6;
            Expected.Add(d);

            // Act
            List<Node> Result = Astar.AnalyiseLocalNodes(Maze, Parent, TargetX, TargetY);

            // Asert
            PrintNodes(Expected);
            PrintNodes(Result);
            CollectionAssert.AreEqual(Expected, Result);
        }



        private void PrintNodes(List<Node> data)
        {
            foreach(var row in data)
            {
                Console.Write("Location: " + row.Location[0] + " " +row.Location[1] + " Parent: "+row.Parent[0]+" "+row.Parent[1]+" G: " + row.G + " H: " + row.H + " F: " + row.F+"\n");
            }
            Console.WriteLine();
        }
    }
}
