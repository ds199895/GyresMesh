using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GEC_FromTriangles : GEC_Creator
    {
        List<HS_Triangle> triangles;

        public GEC_FromTriangles() : base()
        {
            Override = true;
        }

        public GEC_FromTriangles setTriangles(HS_Triangle[] ts)
        {
            triangles = new List<HS_Triangle>();
            foreach(HS_Triangle tri in ts)
            {
                triangles.Add(tri);
            }
            return this;
        }

        public GEC_FromTriangles setTriangles<T>(List<T> ts)where T:HS_Triangle
        {
            triangles = new List<HS_Triangle>();
            foreach (HS_Triangle tri in ts)
            {
                triangles.Add(tri);
            }
            return this;
        }

        protected internal override GE_Mesh createBase()
        {
            if (triangles != null)
            {
                HS_Coord[] vertices = new HS_Point[triangles.Count * 3];
                int[][] faces = new int[triangles.Count][];
                for(int i = 0; i < triangles.Count; i++)
                {
                    faces[i] = new int[3];
                    vertices[3 * i] = triangles[i].p1;
                    vertices[3 * i + 1] = triangles[i].p2;
                    vertices[3 * i + 2] = triangles[i].p3;
                    faces[i][0] = 3 * i;
                    faces[i][1] = 3 * i + 1;
                    faces[i][2] = 3 * i + 2;
                }
                GEC_FromFaceList ffl = new GEC_FromFaceList().setVertices(vertices).setFaces(faces).setDuplicate(true);
                return ffl.createBase();
            }
            return null;
        }
    }
}
