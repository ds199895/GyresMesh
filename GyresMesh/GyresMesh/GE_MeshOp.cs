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
        //public static HS_Coord GetFaceCenter(GE_Face f)
        //{
        //    HS_Point center = new HS_Point();
        //    int c = 0;
        //    foreach(GE_Vertex v in f.GetFaceVertices()){
                
        //        c++;
        //    }
        //}
    }
}
