using Hsy.Geo;
using Hsy.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hsy.Core;
using System.Threading;

namespace Hsy.GyresMesh
{
    public class GE_Mesh : GE_MeshObject
    {
        protected internal readonly static HS_ProgressReporter.HS_ProgressTracker tracker = HS_ProgressReporter.HS_ProgressTracker.instance();

        class CreatorThread : HS_Thread
        {

            GEC_Creator creator;
            GE_Mesh result;
            public CreatorThread(GEC_Creator creator)
            {
                this.creator = creator;
                Thread newThread = new Thread(new ThreadStart(run));
                newThread.Start();
            }

            public override void run()
            {
                this.result = creator.create();
            }

        }


        private GE_RAS<GE_Halfedge> _halfedges;
        private GE_RAS<GE_Vertex> _vertices;
        private GE_RAS<GE_Face> _faces;
        private GE_RAS<GE_Halfedge> unpairedHalfedges;
        private GE_RAS<GE_Halfedge> edges;
        int[] triangles;
        Task executor;
        LinkedList<HS_Thread> tasks;


        /**
         * Instantiates a new GE_Mesh.
         *
         */
        public GE_Mesh() : base()
        {
            _halfedges = new GE_RAS<GE_Halfedge>();
            _vertices = new GE_RAS<GE_Vertex>();
            _faces = new GE_RAS<GE_Face>();
            unpairedHalfedges = new GE_RAS<GE_Halfedge>();
            edges = new GE_RAS<GE_Halfedge>();
            tasks = new LinkedList<HS_Thread>();
            executor = null;
            this.attribute.name = "Mesh";
        }

        public GE_Mesh(GE_Mesh mesh) : this()
        {
            this._vertices = mesh._vertices;
            this._halfedges = mesh._halfedges;
            this._faces = mesh._faces;
            this.attribute.name = mesh.attribute.name;
        }

        public GE_Mesh(GEC_Creator creator) : this()
        {
            this.setNoCopy(creator.create());
            this.triangles = null;
            this.attribute.name = "Mesh";
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void setNoCopy(GE_Mesh target)
        {
            this.replaceVertices(target);
            this.replaceFaces(target);
            this.replaceHalfedges(target);
        }
        public void createThreaded(GEC_Creator creator)
        {
            tasks.Append(new CreatorThread(creator));
        }
        private void replaceVertices(GE_Mesh mesh)
        {
            clearVertices();
            this.addVertices(mesh._vertices);
        }
        private void replaceHalfedges(GE_Mesh mesh)
        {
            clearHalfedges();
            this.addHalfedges(mesh.GetHalfedges());
        }

        private void replaceFaces(GE_Mesh mesh)
        {
            clearFaces();
            this.addFaces(mesh._faces);
        }
        public void clearFaces()
        { 
            _faces = new GE_RAS<GE_Face>();
        }
        public void clearHalfedges()
        {
            this._halfedges = new GE_RAS<GE_Halfedge>();
            this.edges = new GE_RAS<GE_Halfedge>();
            this.unpairedHalfedges = new GE_RAS<GE_Halfedge>();
        }
        public void clearVertices()
        {
            _vertices = new GE_RAS<GE_Vertex>();
        }



        public HS_AABB getAABB()
        {
            return GE_MeshOp.getAABB(this);
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
            }
            else if (he.isEdge())
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

        public void addHalfedges<T>(List<T> halfedges) where T : GE_Halfedge
        {
            //var var3 = halfedges.GetEnumerator();
            //while (var3.MoveNext())
            //{
            //    GE_Halfedge he = var3.Current;
            //    this.Add(he);
            //}
            //System.Threading.Tasks.Parallel.ForEach(halfedges, e => {  });
            foreach (GE_Halfedge e in halfedges)
            {
                this.Add(e);
            }
        }
        public void addHalfedges<T>(FastList<T> halfedges) where T : GE_Halfedge
        {
            //var var3 = halfedges.GetEnumerator();
            //while (var3.MoveNext())
            //{
            //    GE_Halfedge he = var3.Current;
            //    this.Add(he);
            //}
            //System.Threading.Tasks.Parallel.ForEach(halfedges, e => {  });
            foreach (GE_Halfedge e in halfedges.ToArray())
            {

                this.Add(e);
                                
            }
        }
        public void addVertices<T>(ICollection<T> vertices) where T : GE_Vertex
        {

            //var var3 = vertices.GetEnumerator();

            //while (var3.MoveNext())
            //{
            //    GE_Vertex v = (GE_Vertex)var3.Current;
            //    this.Add(v);
            //}
            foreach(GE_Vertex v in vertices)
            {
                this.Add(v);
            }

        }

        public void addFaces<T>(ICollection<T> faces) where T : GE_Face
        {
            //var var3 = faces.GetEnumerator();
            //while (var3.MoveNext())
            //{
            //    GE_Face f = (GE_Face)var3.Current;
            //    this.Add(f);
            //}
            foreach(GE_Face f in faces)
            {
                this.Add(f);
            }
        }
        public void addDerivedElement(GE_Vertex v, params GE_Object[] el)
        {
            Add(v);
            //for (GE_Selection sel : selections.values())
            //{
            //    boolean contains = false;
            //    for (int i = 0; i < el.length; i++)
            //    {
            //        contains |= sel.contains(el[i]);
            //        if (contains)
            //        {
            //            break;
            //        }
            //    }
            //    if (contains)
            //    {
            //        sel.add(v);
            //    }
            //}
        }

        public void addDerivedElement(GE_Halfedge he, params GE_Object[] el)
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
        public void SetHalfedge(GE_Face f, GE_Halfedge he)
        {
            f.SetHalfedge(he);
        }
        public void SetPair(GE_Halfedge he1, GE_Halfedge he2)
        {

            he1.SetPair(he2);
            he2.SetPair(he1);
            this.removeNoSelectionCheck(he1);
            this.removeNoSelectionCheck(he2);

            this.addDerivedElement(he1, he2);
            this.addDerivedElement(he2, he1);
            // he1.SetPair(he2);
            // he2.SetPair(he1);
            //unpairedHalfedges.Remove(he1);
            // unpairedHalfedges.Remove(he2);

            // if (he1.isEdge())
            // {
            //     edges.Add(he1);
            // }
            // else
            // {
            //     _halfedges.Add(he1);
            // }
            // if (he2.isEdge())
            // {
            //     edges.Add(he2);
            // }
            // else
            // {
            //     _halfedges.Add(he2);
            // }
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

        public void removeVertices<T>(List<T> vertices) where T : GE_Vertex
        {
            //var var3 = vertices.GetEnumerator();
            //while (var3.MoveNext())
            //{
            //    GE_Vertex v = var3.Current;
            //    this.remove(v);
            //}
            foreach (GE_Vertex v in vertices)
            {
                remove(v);
            }
        }

        public void removeHalfedges<T>(List<T> halfedges) where T : GE_Halfedge
        {
            //var var3 = halfedges.GetEnumerator();
            //while (var3.MoveNext())
            //{
            //    GE_Halfedge v = var3.Current;
            //    this.remove(v);
            //}
            foreach (GE_Halfedge e in halfedges)
            {
                remove(e);
                
            }
        }

        public void remove(GE_Vertex v)
        {
            _vertices.Remove(v);
            v.Dispose();
            //for (GE_Selection sel : selections.values())
            //{
            //    sel.remove(v);
            //}
        }
        public void remove(GE_Halfedge he)
        {
            _halfedges.Remove(he);
            he.Dispose();
            //for (GE_Selection sel : selections.values())
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
        public GE_Vertex getVertexWithIndex(int i)
        {
            if (i < 0 || i >= _vertices.size())
            {
                throw new IndexOutOfRangeException(
                        "Requested vertex index " + i + "not in range.");
            }
            return _vertices.getWithIndex(i);
        }
        public GE_Halfedge getHalfedgeWithIndex(int i)
        {
            if (i < 0 || i >= edges.size() + _halfedges.size()
                    + unpairedHalfedges.size())
            {
                throw new IndexOutOfRangeException(
                        "Requested halfedge index " + i + "not in range.");
            }
            if (i >= edges.size() + _halfedges.size())
            {
                return unpairedHalfedges
                        .getWithIndex(i - edges.size() - _halfedges.size());
            }
            else if (i >= edges.size())
            {
                return _halfedges.getWithIndex(i - edges.size());
            }
            return edges.getWithIndex(i);
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
            var faces = this._faces.getObjects();
            return new List<GE_Face>(faces);
        }

        public List<GE_Vertex> GetVertices()
        {
            var objs = this._vertices.getObjects();
            return new List<GE_Vertex>(objs);
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
            return _halfedges.Count + edges.Count + unpairedHalfedges.Count;
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
