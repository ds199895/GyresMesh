using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_Mesh:GE_MeshObject
    {
        public static void helloWorld()
        {
            System.Diagnostics.Debug.Print("Hello world!");
        }

        private List<GE_Halfedge> _halfedges;
        private List<GE_Vertex> _vertices;
        private List<GE_Face> _faces;
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
            _halfedges.Add(he);
        }

        public void Add(GE_Face f)
        {
            _faces.Add(f);
        }

        public void SetVertex(GE_Halfedge he, GE_Vertex v)
        {
            he.SetVertex(v);
        }

        public void SetHalfedge( GE_Vertex v, GE_Halfedge he)
        {
            v.SetHalfedge(he);
        }

        public void SetPair(GE_Halfedge he,GE_Halfedge pair)
        {
            he.SetPair(pair);
        }

        public void SetNext(GE_Halfedge he, GE_Halfedge nxt)
        {
            he.SetNext(nxt);
        }

        public void SetFace(GE_Halfedge he, GE_Face f)
        {
            he.SetFace(f);
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

        override
        public String ToString()
        {
            String s = "GE_Mesh key: " + GetKey() + ". ( vertices num: " + GetNumberOfVertices()+" Halfedges num: "+GetNumberOfHalfedges()
                    + ", faces num: " + GetNumberOfFaces() + ")";
            return s;
        }
        protected override void Clear()
        {
            throw new NotImplementedException();
        }

        protected override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }
    }
}
