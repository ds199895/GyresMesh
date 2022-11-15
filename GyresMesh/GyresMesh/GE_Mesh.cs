using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_Mesh : GE_MeshObject
    {
        public static void helloWorld()
        {
            System.Diagnostics.Debug.Print("Hello world!");
        }

        private List<GE_Halfedge> _halfedges;
        private List<GE_Vertex> _vertices;
        private List<GE_Face> _faces;
        private List<GE_Halfedge> unpairedHalfedges;
        private List<GE_Halfedge> edges;
        /**
         * Instantiates a new HE_Mesh.
         *
         */
        public GE_Mesh()
        {
            _halfedges = new List<GE_Halfedge>();
            _vertices = new List<GE_Vertex>();
            _faces = new List<GE_Face>();
        }

        public GE_Mesh(GE_Mesh mesh)
        {
            this._vertices = mesh._vertices;
            this._halfedges = mesh._halfedges;
            this._faces = mesh._faces;
        }

        public GE_Mesh(GEC_Creator creator)
        {
            Create(creator);
        }

        public void Create(GEC_Creator creator)
        {
            this._vertices = new List<GE_Vertex>();
            this._halfedges = new List<GE_Halfedge>();
            this._faces = creator.GetFaces();
            foreach (GE_Face f in _faces)
            {
                this._vertices.AddRange(f.GetFaceVertices());
                this._halfedges.AddRange(f.GetFaceHalfedges());
            }
        }

        public void Set(GE_Mesh mesh)
        {
            this._vertices = mesh._vertices;
            this._halfedges = mesh._halfedges;
            this._faces = mesh._faces;
        }

        public void Add(GE_Vertex v)
        {
            _vertices.Add(v);
        }

        public void Add(GE_Halfedge he)
        {
            if (he.Pair() == null)
            {
                unpairedHalfedges.Add(he);
            }else if (he.isEdge())
            {
                edges.Add(he);
            }
            else
            {
                _halfedges.Add(he);
            }
            
        }

        public void Add(GE_Face f)
        {
            _faces.Add(f);
        }

        public void addHalfedges<T>(List<T>halfedges)where T:GE_Halfedge
        {
            foreach(GE_Halfedge halfedge in halfedges)
            {
                Add(halfedge);
            }
        }

        public void addDerivedElement(GE_Halfedge he,params GE_Object[] el)
        {
            Add(he);
            //for()
        }
        public void SetVertex(GE_Halfedge he, GE_Vertex v)
        {
            he.SetVertex(v);
        }

        public void SetHalfedge(GE_Vertex v, GE_Halfedge he)
        {
            v.SetHalfedge(he);
        }
        public void SetHalfedge( GE_Face f, GE_Halfedge he)
        {
            f.SetHalfedge(he);
        }
        public void SetPair(GE_Halfedge he, GE_Halfedge pair)
        {
            he.SetPair(pair);
        }
        public void ClearPair(GE_Halfedge he)
        {
            if (he.Pair() == null)
            {
                return;

            }
            GE_Halfedge hep = he.Pair();
            removeNoSelectionCheck(he);
            removeNoSelectionCheck(hep);
            he.ClearPair();
            hep.ClearPair();
            Add(he);
            Add(hep);
        }
        public void removeVertices<T>(List<T> vertices)where T:GE_Vertex
        {
            foreach(GE_Vertex v in vertices)
            {
                remove(v);
            }
        }
        public void remove(GE_Vertex v)
        {
            _vertices.Remove(v);
            //for (HE_Selection sel : selections.values())
            //{
            //    sel.remove(v);
            //}
        }
        public void removeNoSelectionCheck(GE_Halfedge he)
        {
            edges.Remove(he);
            _halfedges.Remove(he);
            unpairedHalfedges.Remove(he);
        }
        public void SetNext(GE_Halfedge he, GE_Halfedge nxt)
        {
            he.SetNext(nxt);
        }

        public void SetFace(GE_Halfedge he, GE_Face f)
        {
            he.SetFace(f);
        }

        public GE_FaceEnumerator fEtr()
        {
            List<GE_Face> fs = new List<GE_Face>(GetFaces());
            return new GE_FaceEnumerator(fs);
        }

        public GE_HalfedgeEnumerator heEtr()
        {
            List<GE_Halfedge> hes = new List<GE_Halfedge>(GetHalfedges());
            return new GE_HalfedgeEnumerator(hes);
        }
        public GE_VertexEnumerator vEtr()
        {
            List<GE_Vertex> vs = new List<GE_Vertex>(GetVertices());
            return new GE_VertexEnumerator(vs);
        }

        public List<GE_Face> GetFaces()
        {
            return this._faces;
        }

        public List<GE_Vertex> GetVertices()
        {
            return this._vertices;
        }
        public List<GE_Halfedge> GetHalfedges()
        {
            return this._halfedges;
        }
        public int GetNumberOfVertices()
        {
            return _vertices.Count;
        }


        public int GetNumberOfFaces()
        {
            return _faces.Count;
        }

        public int GetNumberOfHalfedges()
        {
            return _halfedges.Count;
        }

        public List<GE_Halfedge> getUnpairedHalfedges()
        {
            List<GE_Halfedge> halfedges = new List<GE_Halfedge>();
            halfedges.AddRange(this.unpairedHalfedges);
            return halfedges;
        }

        override
        public String ToString()
        {
            String s = "GE_Mesh key: " + GetKey() + ". ( vertices num: " + GetNumberOfVertices() + " Halfedges num: " + GetNumberOfHalfedges()
                    + ", faces num: " + GetNumberOfFaces() + ")";
            return s;
        }
        protected internal override void Clear()
        {
            throw new NotImplementedException();
        }

        protected internal override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }
    }
}
