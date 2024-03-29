﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsy.Core;
using Hsy.Geo;
using Hsy.HsMath;

namespace Hsy.GyresMesh
{

    public class GE_Vertex : GE_MeshObject, HS_MutableCoord
    {
        private HS_Point pos;
        private GE_Halfedge _halfedge;
    GE_TextureCoordinate uvw = null;
        public float xf { get { return this.pos._xf; } set { this.pos._xf = value; this.pos._xd = value; } }
        public float yf { get { return this.pos._yf; } set { this.pos._yf = value; this.pos._yd = value; } }
        public float zf { get { return this.pos._zf; } set { this.pos._zf = value; this.pos._zd = value; } }
    public float wf { get => 1.0f; }
    public double xd { get { return this.pos._xd; } set { this.pos._xd = value; this.pos._xf = (float)value; } }
        public double yd { get { return this.pos._yd; } set { this.pos._yd = value; this.pos._yf = (float)value; } }
        public double zd { get { return this.pos._zd; } set { this.pos._zd = value; this.pos._zf = (float)value; } }
        public double wd { get { return 1; }  }

    

    public GE_Vertex()
        {
            pos = new HS_Point();
      uvw = null;
        }

        public GE_Vertex(HS_Coord hs_coord)
        {
            
            this.pos = new HS_Point(hs_coord);
      uvw = null;
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
    //public GE_Halfedge GetHalfedge(GE_Face f)
    //{
    //  GE_Halfedge he = _halfedge;
    //  if (he == null)
    //  {
    //    return null;
    //  }
    //  if (f == null)
    //  {
    //    do
    //    {
    //      if (he.GetFace() == null)
    //      {
    //        return he;
    //      }
    //      he = he.GetNextInVertex();
    //    } while (he != _halfedge);
    //  }
    //  else
    //  {
    //    do
    //    {
    //      if (he.GetFace() == f)
    //      {
    //        return he;
    //      }
    //      he = he.GetNextInVertex();
    //    } while (he != _halfedge);
    //  }
    //  return null;
    //}

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
            GE_Halfedge GE = _halfedge;
            if (GE == null)
            {
                return null;
            }
            if (f == null)
            {
                do
                {
                    if (GE.GetFace() == null)
                    {
                        return GE;
                    }
                    GE = GE.GetNextInVertex(); 
                } while (GE!=_halfedge);

            }
            else
            {
                do
                {
                    if (GE.GetFace() == f)
                    {
                        return GE;
                    }
                    GE = GE.GetNextInVertex();

                } while (GE!=_halfedge);
            }
            return null;
        }

        public List<GE_Halfedge> GetHalfedgeRing()
        {
            List<GE_Halfedge> edge_star = new List<GE_Halfedge>();
            GE_Halfedge GE = _halfedge;
            do
            {
                edge_star.Add(GE);
                GE = GE.GetNextInVertex();
               
            } while (GE != _halfedge);
            return edge_star;
        }

        public List<GE_Vertex> GetVertexRing()
        {
            List<GE_Vertex> vertex_star = new List<GE_Vertex>();
            if (_halfedge == null)
            {
                return vertex_star;
            }
            GE_Halfedge GE = _halfedge;
            do
            {
                GE_Halfedge GEn = GE.GetNextInFace();
                if (GEn.GetStart() != this && !vertex_star.Contains(GEn.GetStart()))
                {
                    vertex_star.Add(GE.GetStart());
                }
                
                GE=GE.GetNextInVertex();

            } while (GE != _halfedge);
            return vertex_star;
        }

        public List<GE_Face> GetFaceRing()
        {
            List<GE_Face> face_star = new List<GE_Face>();
            GE_Halfedge GE = _halfedge;
            do
            {
                face_star.Add(GE.GetFace());
                GE = GE.GetNextInVertex();
            } while (GE != _halfedge);
            return face_star;
        }
        public GE_Halfedge GetHalfedge()
        {
            return _halfedge;
        }

        public List<GE_Halfedge> GetHalfedgeStar()
        {
            List<GE_Halfedge> vGE = new FastList<GE_Halfedge>();
            if (GetHalfedge()== null)
            {
                return vGE;
            }
            GE_Halfedge GE = GetHalfedge();
            do
            {
                if (!vGE.Contains(GE))
                {
                    vGE.Add(GE);
                }
                GE = GE.GetNextInVertex();
            } while (GE != GetHalfedge());
            return vGE;
        }
        public int GetVertexDegree()
        {
            return GetVertexRing().Count;
        }

        /**
         * Get neighboring vertices.
         *
         * @return neighbors
         */
        public List<GE_Vertex> getVertexStar()
        {
             List<GE_Vertex> vv = new FastList<GE_Vertex>();
            if (GetHalfedge() == null)
            {
                return vv;
            }
            GE_Halfedge he = GetHalfedge();
            do
            {
                 GE_Halfedge hen = he.GetNextInFace();
                if (hen.GetStart() != this && !vv.Contains(hen.GetStart()))
                {
                    vv.Add(hen.GetStart());
                }
                he = he.GetNextInVertex();
            } while (he != GetHalfedge());
            return vv;
        }
        public List<GE_Vertex> getNeighborVertices()
        {
            return getVertexStar();
        }
        public bool isBoundary()
        {
            GE_Halfedge GE = _halfedge;
            do
            {
                if (GE.GetFace() == null)
                {
                    return true;
                }
                GE = GE.GetNextInVertex();
            } while (GE != _halfedge);
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
            GE_Halfedge GE = _halfedge;
            do
            {
                if (GE.GetEnd() == v)
                {
                    return true;
                }
                GE = GE.GetNextInVertex();
            } while (GE != _halfedge);
            return false;
        }
        public GE_Vertex GetNextInFace(GE_Face f)
        {
            GE_Halfedge GE  = GetHalfedge(f);
            if (GE == null)
            {
                return null;
            }
            return GE.GetNextInFace().GetStart();
        }

        public void SetUVW(HS_Coord uvw)
        {
      if (uvw == null)
      {
        return;
      }
      this.uvw = new GE_TextureCoordinate(uvw);
    }
      

        public void Set(HS_Coord c)
        {
            pos = new HS_Point(c);
        }

        public void SetX(float x)
        {
            pos._xf = x;
        }

        public void SetY(float y)
        {
            pos._yf = y;
        }

        public void SetZ(float z)
        {
            pos._zf = z;
        }
    public GE_TextureCoordinate GetVertexUVW()
    {
      if (uvw == null)
      {
        return GE_TextureCoordinate.ZERO;
      }
      return uvw;
    }
    public GE_TextureCoordinate GetHalfedgeUVW(GE_Face f)
    {
      GE_Halfedge he = GetHalfedge(f);
      if (he != null && he.HasHalfedgeUVW())
      {
        return he.GetUVW();
      }
      else
      {
        return GE_TextureCoordinate.ZERO;
      }
    }
    public GE_TextureCoordinate GetUVW(GE_Face f)
    {
      GE_Halfedge he = GetHalfedge(f);
      if (he != null)
      {
        return he.GetUVW();
      }
      return uvw == null ? GE_TextureCoordinate.ZERO : uvw;
    }
    public void CleanUVW()
    {
      if (_halfedge == null)
      {
        return;
      }
      List<GE_Halfedge> halfedges = GetHalfedgeStar();
      if (halfedges.Count == 0)
      {
        return;
      }
      int i = 0;
      while (!HasVertexUVW() && i < halfedges.Count)
      {
        if (halfedges[i].HasHalfedgeUVW())
        {
          SetUVW(halfedges[i].GetHalfedgeUVW());
        }
        i++;
      }
      if (HasVertexUVW())
      {
        foreach (GE_Halfedge he in halfedges)
        {
          if (he.HasHalfedgeUVW())
          {
            if (he.GetHalfedgeUVW().Equals(GetVertexUVW()))
            {
              he.ClearUVW();
            }
          }
        }
      }
    }
    /**
 * Clear halfedge UVW.
 */
    public void ClearUVW()
    {
      uvw = null;
    }
    /**
 * Check if this vertex has a vertex UVW.
 *
 * @return
 */
    public bool HasVertexUVW()
    {
      return uvw != null;
    }
    /**
 * Check if this vertex has a halfedge UVW for this face.
 *
 * @param f
 * @return
 */
    public bool HasHalfedgeUVW(GE_Face f)
    {
     GE_Halfedge he = GetHalfedge(f);
      if (he != null && he.HasHalfedgeUVW())
      {
        return true;
      }
      return false;
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
            if (!HS_Epsilon.isEqual(this.pos._xd, v.xd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.pos._yd, v.yd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(this.pos._zd, v.zd))
            {
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return (int)HS_HashCode.calculateHashCode(this.xd, this.yd, this.zd);
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

        public void Set(double x, double y)
        {
            this.pos.Set(x, y);
        }
    }
}
