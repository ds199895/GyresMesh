using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_Halfedge:GE_MeshObject
    {
        private GE_Vertex _vertex;
        private GE_Halfedge _pair;
        private GE_Halfedge _nxt;
        private GE_Halfedge _pre;
        private GE_Face _face;
        //private GE_TextureCoordinate uvw;
        public GE_Halfedge()
        {
            _vertex = null;
            _pair = null;
            _nxt = null;
            _pre = null;
            _face = null;
        }

        public GE_Halfedge(GE_Vertex Start,GE_Vertex End)
        {
            _pair = null;
            _pre = null;
            _face = null;

            _vertex = Start;
            _nxt._vertex= End;
        }

        public GE_Vertex GetStart()
        {
            return _vertex;
        }

        public GE_Halfedge Get()
        {
            return this;
        }

        public GE_Halfedge Pair()
        {
            return _pair;
        }
        public void ClearPair()
        {
            _pair = null;
        }

        public GE_Halfedge GetPrevInFace()
        {
            return _pre;
        }

        public GE_Halfedge GetPrevNInFace(int n)
        {
            GE_Halfedge he = this;
            for(int i = 0; i < n; i++)
            {
                he = he.GetPrevInFace();
            }
            return he;
        }
        public GE_Halfedge GetNextInFace()
        {
            return _nxt;
        }

        public GE_Halfedge GetNextNInFace(int n)
        {
            GE_Halfedge he = this;
            for (int i = 0; i < n; i++)
            {
                he = he.GetNextInFace();
            }
            return he;
        }

        public GE_Halfedge GetNextInVertex()
        {
            if (_pair == null)
            {
                return null;
            }
            return _pair.GetNextInFace();
        }
        public GE_Halfedge GetNextNInVertex(int n)
        {
            GE_Halfedge he = this;
            for(int i = 0; i < n; i++)
            {
                he = he.GetNextInVertex();
            }
            return he;
        }

        public GE_Halfedge GetPrevInVertex()
        {
            if (_pair == null)
            {
                return null;
            }
            return _pair.GetPrevInFace();
        }
        public GE_Halfedge GetPrevNInVertex(int n)
        {
            GE_Halfedge he = this;
            for (int i = 0; i < n; i++)
            {
                he = he.GetPrevInVertex();
            }
            return he;
        }

        public void SetVertex(GE_Vertex v)
        {
            _vertex = v;
        }

        public void SetNext(GE_Halfedge he)
        {
            this._nxt = he;
           he._pre = this;
        }

        public void SetPrev(GE_Halfedge he)
        {
            this._pre = he;
            he._nxt = this;
        }

        public void SetPair(GE_Halfedge he)
        {
            this._pair = he;
            //he._pair = this;
        }
        public void SetUVW(HS_Coord uvw)
        {
            if (uvw == null)
            {
                return;
            }
            //this.uvw = new HE_TextureCoordinate(uvw);
        }

        public GE_Face GetFace()
        {
            return _face;
        }

        public void SetFace(GE_Face face)
        {
            _face = face;
        }

        public HS_Coord GetCenter()
        {
            return GE_MeshOp.getEdgeCenter(this);
        }

        public GE_Vertex GetEnd()
        {
            if(_pair != null)
            {
                return _pair._vertex;
            }
            if (_nxt != null)
            {
                return _nxt._vertex;
            }
            return null;
        }

        public HS_Point GetEndPosition()
        {
            if (_pair != null)
            {
                return _pair._vertex.GetPosition();
            }
            if (_nxt != null)
            {
                return _nxt._vertex.GetPosition();
            }
            return null;
        }
        public HS_Point GetPosition()
        {
            return _vertex.GetPosition();
        }
        public HS_Point GetStartPosition()
        {
            return _vertex.GetPosition();
        }


        public HS_Coord getHalfedgeDirection()
        {
            return GE_MeshOp.getHalfedgeTangent(this);
        }
        public double GetLength()
        {
            return HS_GeometryOp.GetDistance3D(this.GetStart(), this.GetEnd());
        }
        public bool IsOuterBoundary()
        {
            if (_face == null)
            {
                return true;
            }
            return false;
        }
        public bool IsInnerBoundary()
        {
            if (_face == null || _pair == null)
            {
                return false;
            }
            if (_pair._face == null)
            {
                return true;
            }
            return false;
        }

        public bool isEdge()
        {
            if (_pair == null)
            {
                return false;
            }
            if (_face == null)
            {
                if (_pair._face == null)
                {// both halfedges are faceless
                    return GetKey() < _pair.GetKey();
                }
                else
                {
                    return false;
                }
            }
            else if (_pair._face == null)
            {
                return true;
            }
            return GetKey() < _pair.GetKey();
        }
        protected internal override void Clear()
        {
            throw new NotImplementedException();
        }

        protected internal override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }

        public void Clone(GE_Vertex ob)
        {
            base.Clone(ob);

        }

        override
        public String ToString()
        {
            string face = null;
            if (_face == null)
            {
                face = "null";
            }
            else
            {
                face = _face.GetKey().ToString();
            }
            return "GE_Halfedge key: " + GetKey() +" in Face "+face+ ", paired with halfedge "
                + Pair().GetKey() + " next halfedge "
                 + GetNextInFace().GetKey()+ " previous halfedge "
                 + GetPrevInFace().GetKey() + ". Vertex: " +GetStart().GetKey()
                + ". Is this an edge: " + isEdge() + "." + " ( userLabelInt：" + GetUserLabelInt() + " userLabelDouble：" + GetUserLabelDouble() + "," + GetInternalLabel() + ")";
        }
    }
}
