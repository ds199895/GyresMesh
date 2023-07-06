using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Rhino.Geometry;

namespace Spatial_Printing
{
	public class Graph
	{
        private int vertices; // 图中顶点的数量
        private List<int>[] adjacencyList; // 图的邻接表表示

        public Graph(int v)
        {
            vertices = v;
            adjacencyList = new List<int>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                adjacencyList[i] = new List<int>();
            }
        }

        public void AddEdge(int v, int w)
        {
            adjacencyList[v].Add(w);
            adjacencyList[w].Add(v);
        }

        public int CountConnectedComponents()
        {
            bool[] visited = new bool[vertices];
            int count = 0;

            for (int i = 0; i < vertices; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited);
                    count++;
                }
            }

            return count;
        }

        private void DFS(int v, bool[] visited)
        {
            visited[v] = true;

            foreach (int neighbor in adjacencyList[v])
            {
                if (!visited[neighbor])
                {
                    DFS(neighbor, visited);
                }
            }
        }


    }
	}


