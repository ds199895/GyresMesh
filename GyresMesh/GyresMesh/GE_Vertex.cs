using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsy.Geo;
using Hsy.HsMath;

namespace Hsy.GyresMesh
{

    public class GE_Vertex : GE_MeshObject, HS_MutableCoord
    {
        private HS_Point pos;
        private GE_Halfedge _halfedge;

        public GE_Vertex()
        {
            pos = new HS_Point();
        }

        public GE_Vertex(HS_Coord hs_coord)
        {
            this.pos = new HS_Point(hs_coord);
        }

        public GE_Vertex(float x,float y,float z)
        {
            pos = new HS_Point(x, y, z);
        }



        public GE_Vertex Get()
        {
            GE_Vertex copy = new GE_Vertex(GetPosition());
            copy.Clone(this);
            return copy;
        }

        public HS_Point GetPosition()
        {
            return pos;
        }

        public GE_Halfedge GetHalfEdge()
        {
            return _halfedge;
        }

        internal void SetHalfedge(GE_Halfedge halfedge)
        {
            _halfedge = halfedge;
        }

        protected void ClearHalfedge()
        {
            _halfedge = null;
        }
        public GE_Halfedge GetHalfedge(GE_Face f)
        {
            GE_Halfedge he = _halfedge;
            if (he == null)
            {
                return null;
            }
            if (f == null)
            {
                do
                {
                    if (he.GetFace() == null)
                    {
                        return he;
                    }
                    he = he.GetNextInVertex(); 
                } while (he!=_halfedge);

            }
            else
            {
                do
                {
                    if (he.GetFace() == f)
                    {
                        return he;
                    }
                    he = he.GetNextInVertex();

                } while (he!=_halfedge);
            }
            return null;
        }

        public List<GE_Halfedge> GetHalfedgeRing()
        {
            List<GE_Halfedge> edge_star = new List<GE_Halfedge>();
            GE_Halfedge he = _halfedge;
            do
            {
                edge_star.Add(he);
                he = he.GetNextInVertex();
               
            } while (he != _halfedge);
            return edge_star;
        }

        public List<GE_Vertex> GetVertexRing()
        {
            List<GE_Vertex> vertex_star = new List<GE_Vertex>();
            if (_halfedge == null)
            {
                return vertex_star;
            }
            GE_Halfedge he = _halfedge;
            do
            {
                GE_Halfedge hen = he.GetNextInFace();
                if (hen.GetStart() != this && !vertex_star.Contains(hen.GetStart()))
                {
                    vertex_star.Add(he.GetStart());
                }
                
                he=he.GetNextInVertex();

            } while (he != _halfedge);
            return vertex_star;
        }

        public List<GE_Face> GetFaceRing()
        {
            List<GE_Face> face_star = new List<GE_Face>();
            GE_Halfedge he = _halfedge;
            do
            {
                face_star.Add(he.GetFace());
                he = he.GetNextInVertex();
            } while (he != _halfedge);
            return face_star;
        }
       
        public int GetVertexDegree()
        {
            return GetVertexRing().Count;
        }

        public bool isBoundary()
        {
            GE_Halfedge he = _halfedge;
            do
            {
                if (he.GetFace() == null)
                {
                    return true;
                }
                he = he.GetNextInVertex();
            } while (he != _halfedge);
            return false;
        }
        public bool isIsolated()
        {
            return _halfedge == null;
        }

        public bool isNeighbor(GE_Vertex v)
        {
            if (_halfedge == null)
            {
                return false;
            }
            GE_Halfedge he = _halfedge;
            do
            {
                if (he.GetEnd() == v)
                {
                    return true;
                }
                he = he.GetNextInVertex();
            } while (he != _halfedge);
            return false;
        }
        public GE_Vertex GetNextInFace(GE_Face f)
        {
            GE_Halfedge he  = GetHalfedge(f);
            if (he == null)
            {
                return null;
            }
            return he.GetNextInFace().GetStart();
        }


        public float X()
        {
            return (float)pos.x;
        }

        public float Y()
        {
            return (float)pos.y;
        }

        public float Z()
        {
            return (float)pos.z;
        }


        public void Set(HS_Coord c)
        {
            pos = new HS_Point(c);
        }

        public void SetX(float x)
        {
            pos.x = x;
        }

        public void SetY(float y)
        {
            pos.y = y;
        }

        public void SetZ(float z)
        {
            pos.z = z;
        }
        override
        public bool Equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o == this)
            {
                return true;
            }
            if (!(o.GetType() == this.GetType()))
            {
                return false;
            }
            GE_Vertex v = (GE_Vertex)o;
            if (!HS_Epsilon.isEqual(this.X(), v.X()))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.Y(), v.Y()))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.Z(), v.Z()))
            {
                return false;
            }
            return true;
        }

        public void Clone(GE_Vertex ob)
        {
            base.Clone(ob);
        }

        protected override void Clear()
        {
            _halfedge=null;
        }

        protected override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }


        override
        public String ToString()
        {
            return "GE_Vertex key: " + GetKey() + " [x=" +X() + ", y=" + Y()
        + ", z=" + Z() + "]" + " (userLabelInt：" +GetUserLabelInt() + " userLabelDouble：" +GetUserLabelDouble() + ","+ " internalLabel："+
        + GetInternalLabel() + ")";
        }

        public int CompareTo(HS_Coord p)
        {
            int cmp = this.X().CompareTo(p.X());
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.Y().CompareTo(p.Y());
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.Z().CompareTo(p.Z());
            if (cmp != 0)
            {
                return cmp;
            }
            return 0;
        }
    }
}
