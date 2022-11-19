using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_Mesh : GE_MeshObject
    {
        private List<GE_Halfedge> _halfedges;
        private List<GE_Vertex> _vertices;
        private List<GE_Face> _faces;
        private List<GE_Halfedge> unpairedHalfedges;
        private List<GE_Halfedge> edges;
        int[] triangles;
        /**
         * Instantiates a new HE_Mesh.
         *
         */
        public GE_Mesh()
        {
            _halfedges = new List<GE_Halfedge>();
            _vertices = new List<GE_Vertex>();
            _faces = new List<GE_Face>();
            unpairedHalfedges = new List<GE_Halfedge>();
            edges = new List<GE_Halfedge>();
        }

        public GE_Mesh(GE_Mesh mesh)
        {
            this._vertices = mesh._vertices;
            this._halfedges = mesh._halfedges;
            this._faces = mesh._faces;
        }

        public GE_Mesh(GEC_Creator creator)
        {
            _halfedges = new List<GE_Halfedge>();
            _vertices = new List<GE_Vertex>();
            _faces = new List<GE_Face>();
            unpairedHalfedges = new List<GE_Halfedge>();
            edges = new List<GE_Halfedge>();
            this.setNoCopy(creator.create());
            this.triangles = null;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setNoCopy(GE_Mesh target)
        {
            this.replaceVertices(target);
            this.replaceFaces(target);
            this.replaceHalfedges(target);

        }
        private void replaceVertices(GE_Mesh mesh)
        {
            if (this._vertices!=null)
            {
                this._vertices.Clear();
            }
            
            this.addVertices(mesh._vertices);
        }

        private void replaceFaces(GE_Mesh mesh)
        {
            this._faces.Clear();
            this.addFaces(mesh._faces);
        }
        private void replaceHalfedges(GE_Mesh mesh)
        {
            clearHalfedges();
            GE_HalfedgeEnumerator heEtr = mesh.heEtr();
            while (heEtr.MoveNext())
            {
                this.Add(heEtr.Current);
            }
        }

        public void clearHalfedges()
        {
            this._halfedges = new List<GE_Halfedge>();
            this.edges = new List<GE_Halfedge>();
            this.unpairedHalfedges = new List<GE_Halfedge>();
        }
        public HS_AABB getAABB()
        {
            return GE_MeshOp.getAABB(this);
        }
        //public void clearVertices()
        //{
        //    this._vertices = new List<GE_Vertex>();
        //    var var2 = _vertices.GetEnumerator();

        //    while (var2.MoveNext())
        //    {
        //        GE_Vertex v = var2.Current;
        //        sel.clearVertices();
        //    }

        //}

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
            var var3 = halfedges.GetEnumerator();
            while (var3.MoveNext())
            {
                GE_Halfedge he = (GE_Halfedge)var3.Current;
                this.Add(he);
            }
        }
        public void addVertices<T>(List<T>vertices)where T:GE_Vertex
        {
            var var3 = vertices.GetEnumerator();

            while (var3.MoveNext())
            {
                GE_Vertex v = (GE_Vertex)var3.Current;
                this.Add(v);
            }

        }

        public void addFaces<T>(List<T>faces)where T : GE_Face
        {
            var var3 = faces.GetEnumerator();
            while (var3.MoveNext())
            {
                GE_Face f = (GE_Face)var3.Current;
                this.Add(f);
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
        public void SetPair(GE_Halfedge he1, GE_Halfedge he2)
        {
            this.removeNoSelectionCheck(he1);
            this.removeNoSelectionCheck(he2);

            he1.SetPair(he2);
            he2.SetPair(he1);
            this.addDerivedElement(he1, he2);
            this.addDerivedElement(he2, he1);
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
        public void clearUsedElements()
        {
            GE_FaceEnumerator fEtr = this.fEtr();
            while (fEtr.MoveNext())
            {
                fEtr.Current.ResetUsed();
            }
            GE_VertexEnumerator vEtr = this.vEtr();
            while (vEtr.MoveNext())
            {
                vEtr.Current.ResetUsed();
            }
            GE_HalfedgeEnumerator heEtr = this.heEtr();
            while (heEtr.MoveNext())
            {
                heEtr.Current.ResetUsed();
            }
        }
        public GE_Mesh cleanUnusedElementsByface()
        {
            return GE_MeshOp.cleanUnusedElementsByFace(this);
        }

        public void removeVertices<T>(List<T> vertices)where T:GE_Vertex
        {
            var var3 = vertices.GetEnumerator();
            while (var3.MoveNext())
            {
                GE_Vertex v = var3.Current;
                this.remove(v);
            }
            //foreach(GE_Vertex v in vertices)
            //{
            //    remove(v);
            //}
        }

        public void removeHalfedges<T>(List<T> halfedges) where T : GE_Halfedge
        {
            var var3 = halfedges.GetEnumerator();
            while (var3.MoveNext())
            {
                GE_Halfedge v = var3.Current;
                this.remove(v);
            }
            //foreach(GE_Vertex v in vertices)
            //{
            //    remove(v);
            //}
        }

        public void remove(GE_Vertex v)
        {
            _vertices.Remove(v);
            //for (HE_Selection sel : selections.values())
            //{
            //    sel.remove(v);
            //}
        }
        public void remove(GE_Halfedge he)
        {
            _halfedges.Remove(he);
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
        public GE_EdgeEnumerator eEtr()
        {
            List<GE_Halfedge> hes = new List<GE_Halfedge>(GetHalfedges());
            return new GE_EdgeEnumerator(hes);
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
            List<GE_Halfedge> hes = new List<GE_Halfedge>();
            hes.AddRange(_halfedges);
            hes.AddRange(edges);
            hes.AddRange(unpairedHalfedges);
            return hes;
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
            return _halfedges.Count+edges.Count+unpairedHalfedges.Count;
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
