using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Triangulation2D
    {
        private int[] triangles;
        private int[] edges;
        private int[][] neighbors;
        private int high;

        public HS_Triangulation2D()
        {


        }

        public HS_Triangulation2D(int[] T, int[] E)
        {
            this.triangles = T;
            this.edges = E;
        }

        public HS_Triangulation2D(int[] T)
        {
            this.triangles= T;
            if (triangles.Length == 0)
            {
                this.edges = new int[0];
                this.neighbors = new int[0][];
            }
            else
            {
                extractEdges(triangles);
            }
        }
        public int[] getTriangles()
        {
            return this.triangles;
        }
        public int[] getEdges()
        {
            return this.edges;
        }

        public int[][] getNeighbors()
        {
            return neighbors;
        }


        private void extractEdges(int[] tris)
        {
            high = -1;
            int f = tris.Length;
            Dictionary<long, int[]> map = new Dictionary<long, int[]>();
            for(int i = 0; i < tris.Length; i+=3)
            {
                int v0 = tris[i];
                high = Math.Max(high, v0);
                int v1 = tris[i + 1];
                high = Math.Max(high, v1);
                int v2 = tris[i + 2];
                high = Math.Max(high, v2);
                long index = getIndex(v0, v1, f);
                map.Add(index, new int[] { v0, v1 });
                index = getIndex(v1, v2, f);
                map.Add(index, new int[] { v1, v2 });
                index = getIndex(v2, v0, f);
                map.Add(index, new int[] { v2, v0 });

            }
            edges = new int[2 * map.Count];
            var values = map.Values;
            //int i = 0;
            List<int>[] nn = new List<int>[high + 1];
            for(int j = 0; j <= high; j++)
            {
                nn[j] = new List<int>();
            }
            int id = 0;
            foreach (int[] value in values)
            {
                edges[2 * id] = value[0];
                edges[2 * id + 1] = value[1];
                nn[value[0]].Add(value[1]);
                nn[value[1]].Add(value[0]);
                id++;
            }
            neighbors = new int[high + 1][];
            for(int j = 0; j <= high; j++)
            {
                neighbors[j] = new int[nn[j].Count];
                for(int k = 0; k < nn[j].Count; k++)
                {
                    neighbors[j][k] = (int)nn[j][k];
                }
            }
        }

        private long getIndex(int i,int j,int f)
        {
            return i > j ? j + i * f : i + j * f;
        }

    }
}
