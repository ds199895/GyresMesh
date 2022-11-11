using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Triangulate2D
    {
        private int[] triangles;
        private int[] edges;
        private int[][] neighbors;
        private int high;

        public HS_Triangulate2D()
        {


        }

        public HS_Triangulate2D(int[] T, int[] E)
        {
            triangles = T;
            edges = E;
        }

        public HS_Triangulate2D(int[] T)
        {
            triangles = T;
            if (triangles.Length == 0)
            {
                edges = new int[0];
                neighbors = new int[0][0];
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

        private void extractEdges(int[] tris)
        {
            high = -1;
            int f = tris.Length;
            Dictionary<long, int[]> map = new Dictionary<long, int[]>();
            for(int i = 0; i < tris.Length; i++)
            {
                int v0 = tris[i];
                high = Math.Max(high, v0);
                int v1 = tris[i + 1];
                high = Math.Max(high, v1);
                int v2 = tris[i + 2];
                high = Math.Max(high, v2);
                long index = getIndex(v0, v1, f);
            }
        }

    }
}
