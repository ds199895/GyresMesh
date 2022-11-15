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
        private double _xd;
        private double _yd;
        private double _zd;
        private float _xf;
        private float _yf;
        private float _zf;
        public float xf { get { return (float)this.pos.xf; } set => this.pos.xf = (float)(this.pos.xd = value); }
        public float yf { get { return (float)this.pos.yf; } set => this.pos.yf = (float)(this.pos.yd = value); }
        public float zf { get { return (float)this.pos.zf; } set => this.pos.zf = (float)(this.pos.zd = value); }
        public double xd { get { return this.pos.xd; } set => this.pos.xd = value; }
        public double yd { get { return this.pos.yd; } set => this.pos.yd = value; }
        public double zd { get { return this.pos.zd; } set => this.pos.zd = value; }
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

        public void SetUVW(HS_Coord uvw)
        {
            //if (uvw == null)
            //{
            //    return;
            //}
            //this.uvw = new HE_TextureCoordinate(uvw);
        }
      

        public void Set(HS_Coord c)
        {
            pos = new HS_Point(c);
        }

        public void SetX(float x)
        {
            pos.xf = x;
        }

        public void SetY(float y)
        {
            pos.yf = y;
        }

        public void SetZ(float z)
        {
            pos.zf = z;
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
            if (!HS_Epsilon.isEqual(this.xd, v.xd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.yd, v.yd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.zd, v.zd))
            {
                return false;
            }
            return true;
        }

        public void Clone(GE_Vertex ob)
        {
            base.Clone(ob);
        }

        protected internal override void Clear()
        {
            _halfedge=null;
        }

        protected internal override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }


        override
        public String ToString()
        {
            return "GE_Vertex key: " + GetKey() + "  Position: [x=" +xd + ", y=" + yd
        + ", z=" + zd + "]" + " (userLabelInt：" +GetUserLabelInt() + " userLabelDouble：" +GetUserLabelDouble() + ","+ " internalLabel："+
        + GetInternalLabel() + ")";
        }

        public int CompareTo(HS_Coord p)
        {
            int cmp = this.xd.CompareTo(p.xd);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.yd.CompareTo(p.yd);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.zd.CompareTo(p.zd);
            if (cmp != 0)
            {
                return cmp;
            }
            return 0;
        }

        public void SetX(double x)
        {
            pos.SetX(x);
        }

        public void SetY(double y)
        {
            pos.SetY(y);
        }

        public void SetZ(double z)
        {
            pos.SetZ(z);
        }

        public void Set(double x, double y, double z)
        {
            pos.Set(x, y, z);
        }

        public double getd(int i)
        {
            return pos.getd(i);
        }
    }
}
