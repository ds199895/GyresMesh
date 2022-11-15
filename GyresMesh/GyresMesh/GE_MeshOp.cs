using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_MeshOp
    {
        private static HS_GeometryFactory gf = new HS_GeometryFactory();
        protected GE_MeshOp()
        {

        }

        public static HS_Coord getFaceNormal(GE_Face face)
        {
            GE_Halfedge he = face.GetHalfedge();
            if (he == null)
            {
                return null;
            }
            else
            {
                HS_Vector _normal = new HS_Vector();
                do
                {
                    GE_Vertex p0 = he.GetStart();
                    GE_Vertex p1 = he.GetNextInFace().GetStart();
                    _normal += new HS_Vector((p0.yd - p1.yd) * (p0.zd + p1.zd), (p0.zd - p1.zd) * (p0.xd + p1.xd), (p0.xd - p1.xd) * (p0.yd + p1.yd));
                    he = he.GetNextInFace();
                } while (he != face.GetHalfedge());
                _normal.united();
                return _normal;
            }
        }

        public static void cycleHalfedges(GE_Mesh mesh, List<GE_Halfedge> halfedges)
        {
            orderHalfedges(mesh, halfedges, true);
        }
        public static void cycleHalfedgesReverse(GE_Mesh mesh, List<GE_Halfedge> halfedges)
        {
            orderHalfedgesReverse(mesh, halfedges, true);
        }


        public static void orderHalfedges(GE_Mesh mesh, List<GE_Halfedge> halfedges, bool loop)
        {
            GE_Halfedge he;
            int n = halfedges.Count;
            if (n > 0)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    he = halfedges[j];
                    mesh.SetNext(he, halfedges[j + 1]);
                }
                if (loop)
                {
                    he = halfedges[n - 1];
                    mesh.SetNext(he, halfedges[0]);
                }
            }
        }

        public static void orderHalfedgesReverse(GE_Mesh mesh, List<GE_Halfedge> halfedges, bool loop)
        {
            GE_Halfedge he;
            int n = halfedges.Count;
            if (n > 0)
            {
                if (loop)
                {
                    he = halfedges[0];
                    mesh.SetNext(he, halfedges[n - 1]);
                }
                for (int j = 1; j < n; j++)
                {
                    he = halfedges[j];
                    mesh.SetNext(he, halfedges[j - 1]);
                }
            }
        }

        public static void capHalfedges(GE_Mesh mesh)
        {
            List<GE_Halfedge> unpairedHalfedges = mesh.getUnpairedHalfedges();
            int nuh = unpairedHalfedges.Count;
            GE_Halfedge[] newHalfedges = new GE_Halfedge[nuh];
            GE_Halfedge he1, he2;
            for (int i = 0; i < nuh; i++)
            {
                he1 = unpairedHalfedges[i];
                he2 = new GE_Halfedge();
                mesh.SetVertex(he2, he1.GetNextInFace().GetStart());
                mesh.SetPair(he1, he2);
                newHalfedges[i] = he2;
                mesh.addDerivedElement(he2);

            }
            for (int i = 0; i < nuh; i++)
            {
                he1 = newHalfedges[i];
                if (he1.GetNextInFace() == null)
                {
                    for (int j = 0; j < nuh; j++)
                    {
                        he2 = newHalfedges[j];
                        if (!he2.isUsed())
                        {
                            if (he2.GetStart() == he1.Pair().GetStart())
                            {
                                mesh.SetNext(he1, he2);
                                he2.SetUsed();
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static void pairHalfedges(GE_Mesh mesh)
        {
            pairHalfedgesOnePass(mesh);
        }
        public static List<GE_Halfedge> pairHalfedgesOnePass(GE_Mesh mesh)
        {
            Dictionary<long, VertexInfo> vertexLists = new Dictionary<long, VertexInfo>();
            List<GE_Halfedge> unpairedHalfedges = mesh.getUnpairedHalfedges();
            GE_Vertex v;
            VertexInfo vi;
            foreach (GE_Halfedge he in unpairedHalfedges)
            {
                v = he.GetStart();
                vi = vertexLists[v.GetKey()];
                if (vi == null)
                {
                    vi = new VertexInfo();
                    vertexLists.Add(v.GetKey(), vi);
                }
                vi.Out.Add(he);
                v = he.GetNextInFace().GetStart();
                vi = vertexLists[v.GetKey()];
                if (vi == null)
                {
                    vi = new VertexInfo();
                    vertexLists.Add(v.GetKey(), vi);
                }
                vi.In.Add(he);

            }
            GE_Halfedge he1;
            GE_Halfedge he2;
            var vitr = vertexLists.GetEnumerator();
            VertexInfo vInfo;
            List<GE_Halfedge> mismatchedHalfedges = new List<GE_Halfedge>();
            while (vitr.MoveNext())
            {
                vInfo = vitr.Current.Value;
                for (int i = 0; i < vInfo.Out.Count; i++)
                {
                    he1 = vInfo.Out[i];
                    if (he1.Pair() == null)
                    {
                        for (int j = 0; j < vInfo.In.Count; j++)
                        {
                            he2 = vInfo.In[j];
                            if (he2.Pair() == null)
                            {
                                if (he1.GetStart() == he2.GetNextInFace().GetStart() && he2.GetStart() == he1.GetNextInFace().GetStart())
                                {
                                    mesh.SetPair(he1, he2);
                                    break;
                                }
                            }
                        }
                        for (int j = 0; j < vInfo.Out.Count; j++)
                        {
                            he2 = vInfo.Out[j];
                            if (he2 != he1 && he2.Pair() == null)
                            {
                                if (he1.GetNextInFace().GetStart() == he2.GetNextInFace().GetStart())
                                {
                                    mismatchedHalfedges.Add(he1);
                                    mismatchedHalfedges.Add(he2);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return mismatchedHalfedges;
        }

        public static GE_Mesh cleanUnusedElementsByFace(GE_Mesh mesh)
        {
            List<GE_Vertex> cleanedVertices = new List<GE_Vertex>();
            List<GE_Halfedge> cleanedHalfedges = new List<GE_Halfedge>();
            GE_Halfedge he;
            GE_Face f;
            IEnumerator<GE_Face> fetr = mesh.fEtr();
            while (fetr.MoveNext())
            {
                f = fetr.Current;
                he = f.GetHalfedge();
                do
                {
                    if (!cleanedVertices.Contains(he.GetStart()))
                    {
                        cleanedVertices.Add(he.GetStart());
                        mesh.SetHalfedge(he.GetStart(), he);
                    }
                    if (!cleanedHalfedges.Contains(he)){
                        cleanedHalfedges.Add(he);
                    }
                    he = he.GetNextInFace();
                } while (he != f.GetHalfedge());
            }
            int n = cleanedHalfedges.Count;
            for(int i = 0; i < n; i++)
            {
                he = cleanedHalfedges[i];
                if (!cleanedHalfedges.Contains(he.Pair()))
                {
                    mesh.ClearPair(he);
                    mesh.SetHalfedge(he.GetStart(), he);
                }
            }
            List<GE_Vertex> removev = new List<GE_Vertex>();
            foreach(GE_Vertex v in mesh.GetVertices())
            {
                if (!cleanedVertices.Contains(v))
                {
                    removev.Add(v);
                }
            }
            mesh.removeVertices(removev);
            GE_HalfedgeEnumerator heEtr = mesh.heEtr();
            List<GE_Halfedge> remove = new List<GE_Halfedge>();
            while (heEtr.MoveNext())
            {
                he = heEtr.Current;
                if (!cleanedHalfedges.Contains(he))
                {
                    remove.Add(he);
                }
            }
            mesh.remove
        }


        //public static HS_Coord GetFaceCenter(GE_Face f)
        //{
        //    HS_Point center = new HS_Point();
        //    int c = 0;
        //    foreach(GE_Vertex v in f.GetFaceVertices()){

        //        c++;
        //    }
        //}

        public class VertexInfo
        {
            public List<GE_Halfedge> In;
            public List<GE_Halfedge> Out;
            public VertexInfo()
            {
                this.Out = new List<GE_Halfedge>();
                this.In = new List<GE_Halfedge>();
            }
        }
    }
}
