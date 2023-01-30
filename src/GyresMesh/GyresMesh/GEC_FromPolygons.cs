using Hsy.Core;
using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GEC_FromPolygons : GEC_Creator
    {
        private HS_Polygon[] polygons;
        private bool checkNormals;
        private List<GE_Face> _faces;
        public GEC_FromPolygons()
        {
            this.Override = true;
        }
        public GEC_FromPolygons(HS_Polygon[] qs)
        {
            new GEC_FromPolygons();
            this.polygons = qs;
        }

        public GEC_FromPolygons(List<HS_Polygon> qs)
        {
            new GEC_FromPolygons();
            this.setPolygons(qs);
        }
        public GEC_FromPolygons setPolygons(HS_Polygon[] qs)
        {
            this.polygons = qs;
            return this;
        }
        public GEC_FromPolygons setPolygons<T>(List<T> qs)where T:HS_Polygon
        {
            int n = qs.Count;
            this.polygons = new HS_Polygon[n];
            int i = 0;

            for (var var5 = qs.GetEnumerator(); var5.MoveNext(); ++i)
            {
                HS_Polygon poly = (HS_Polygon)var5.Current;
                this.polygons[i] = poly;
            }

            return this;
        }

        public GEC_FromPolygons setCheckNormals(bool b)
        {
            this.checkNormals = b;
            return this;
        }

        protected internal override GE_Mesh createBase()
        {
            if (this.polygons != null && this.polygons.Length > 0)
            {
                int nq = this.polygons.Length;
                List<HS_Coord> vertices = new List<HS_Coord>();
      
                List<int[]> faces = new List<int[]>();
                int id = 0;
                bool[] duplicate=null;
                HashSet<int> vset = new HashSet<int>();
                for (int i = 0; i < nq; i++)
                {
                    HS_Polygon poly = this.polygons[i];
                    int[] face;
                    int j;
                    if (!poly.isSimple())
                    {
                        //Console.WriteLine("check num: " + poly.getNumberOfPoints());
                        face = poly.getTriangles();
                    
                       
                        for (j = 0; j < face.Length; j += 3)
                        {
                            int[] face_ = new int[3];
                            vertices.Add(poly.GetPoint(face[j]));
                            face_[0] = id++;
                            vertices.Add(poly.GetPoint(face[j + 1]));
                            face_[1] = id++;
                            vertices.Add(poly.GetPoint(face[j + 2]));
  
                            face_[2] = id++;
                            faces.Add(face_);



                            //Console.WriteLine(face[j] + " " + face[j + 1] + " " + face[j + 2]);
                        }
                    }
                    else
                    {
                        face = new int[poly.getNumberOfPoints()];
                        for (j = 0; j < poly.getNumberOfPoints(); j++)
                        {
                            vertices.Add(poly.GetPoint(j));
                            face[j] = id++;
                        }
                        faces.Add(face);
                    }
                }
                duplicate = new bool[vertices.Count];
                for (int i=0;i<vertices.Count;i++)
                {
                    var vertex = vertices[i];
                    HS_Point pt = new HS_Point(vertex.xd, vertex.yd, vertex.zd);

                    
                    duplicate[i] = !vset.Add(HS_HashCode.calculateHashCode(vertex));

                }
                Console.WriteLine(vset.Count);
                GEC_FromFaceList fll = (new GEC_FromFaceList()).setVertices(vertices).setFaces(faces).setDuplicate(duplicate).setCheckNormals(this.checkNormals);
                return fll.createBase();
            }
            else
            {
                return new GE_Mesh();
            }
        }
    }
}
