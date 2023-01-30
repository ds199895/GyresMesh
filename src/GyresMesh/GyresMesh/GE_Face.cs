using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    
    public class GE_Face:GE_MeshObject
    {
        private List<GE_Vertex> _faceVertices;
        private List<GE_Halfedge> _faceHalfedges;
        private List<GE_Face> _adjacentFaces;
        private GE_Halfedge _halfedge;

        public GE_Face()
        {
            _faceHalfedges = null;
            _faceVertices = null;
            _adjacentFaces = null;
            _halfedge = null;

        }

        public GE_Halfedge GetHalfedge()
        {
            return _halfedge;
        }

        public List<GE_Halfedge> GetFaceHalfedges()
        {
            _faceHalfedges = new List<GE_Halfedge>();
            if(_halfedge == null)
            {
                return _faceHalfedges;
            }
            GE_Halfedge he = _halfedge;

            do
            {
                if (!_faceHalfedges.Contains(he))
                {
                    _faceHalfedges.Add(he);
                }
                he = he.GetNextInFace();
            } while (he != _halfedge);

            return _faceHalfedges;

        }

        public List<GE_Face> GetFacesRing()
        {
           _adjacentFaces= new List<GE_Face>();
            if (_halfedge == null)
            {
                return _adjacentFaces;
            }
            GE_Halfedge he = _halfedge;
            do
            {
                GE_Halfedge hep = he.Pair();
                if (hep != null && hep.GetFace() != null)
                {
                    
                    if (!_adjacentFaces.Contains(hep.GetFace()))
                    {
                        _adjacentFaces.Add(hep.GetFace());
                    }
                }
                he = he.GetNextInFace();
            } while (he != _halfedge);

            return _adjacentFaces;
        }

        public int GetFaceDegree()
        {
            return GetFacesRing().Count;
        }
        public List<GE_Vertex> GetFaceVertices()
        {
            _faceVertices = new List<GE_Vertex>();
            if (_halfedge == null)
            {
                return _faceVertices;
            }
            GE_Halfedge he = _halfedge;
           
            do
            {
                _faceVertices.Add(he.GetStart());
                he = he.GetNextInFace();
            } while (he != _halfedge);

            return _faceVertices;

        }

        public HS_Coord getFaceNormal()
        {
            return GE_MeshOp.getFaceNormal(this);
        }

        public HS_Coord getFaceCenter()
        {
            return GE_MeshOp.GetFaceCenter(this);
        }
        public void SetHalfedge(GE_Halfedge he)
        {
            _halfedge = he;
        }


        override
        public String ToString()
        {
            String s = "GE_Face key: " + GetKey() + ". Connects " + GetFaceVertices().Count
                    + " vertices: ";
            GE_Halfedge he = _halfedge;
            for (int i = 0; i < GetFaceVertices().Count - 1; i++)
            {
                s += he.GetStart().GetKey() + "-";
                he = he.GetNextInFace();
            }
            s += he.GetStart().GetKey()+" Half Edge Circulation:";
            he = _halfedge;
            for (int i = 0; i < GetFaceVertices().Count - 1; i++)
            {
                s += he.GetKey() + "-";
                he = he.GetNextInFace();
            }
            s += he.GetKey()+ "." + "(userLabelInt：" +GetUserLabelInt() + " userLabelDouble：" +GetUserLabelDouble() + ","+ " internalLabel："+
        + GetInternalLabel() + ")";
            return s;
        }

        protected internal  override void Clear()
        {
            throw new NotImplementedException();
        }

        protected internal  override void ClearPreComputed()
        {
            throw new NotImplementedException();
        }
    }
}
