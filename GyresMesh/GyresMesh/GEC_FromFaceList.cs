using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GEC_FromFaceList:GEC_Creator
    {
        private List<GE_Face> _faceList;
        private HS_Coord[] vertices;
        private HS_Coord[] uvws;
        private HS_Coord[] vertexuvws;
        private int[] vertexColors;
        private bool[] vertexVisibility;
        private int[] vertexLabels;
        private int[] vertexInternalLabels;
        private int[] faceColors;
        private int[] faceTextures;
        private bool[] faceVisibility;
        private int[] faceLabels;
        private int[] faceInternalLabels;

        private int[,] faces;
        private int[,] faceuvws;



        public GEC_FromFaceList()
        {
            _faceList = null;
        }

        public GEC_FromFaceList(List<GE_Face> faces)
        {
            _faceList = faces;
        }

        //protected boolean getCheckDuplicateVertices()
        //{
        //    return parameters.get("duplicate", true);
        //}

        //protected boolean getUseFaceInfo()
        //{
        //    return parameters.get("usefaceinfo", false);
        //}
        //protected boolean getUseVertexInfo()
        //{
        //    return parameters.get("usevertexinfo", false);
        //}

        public override List<GE_Face> GetFaces()
        {
            return _faceList;
        }

        public void SetFaces(List<GE_Face> faces)
        {
            _faceList = faces;
        }
        public void SetFaces(int[,] fs)
        {
            faces = fs;
        }
        public GEC_FromFaceList setVertices(HS_Coord[] vs)
        {
            vertices = vs;
            return this;
        }

        protected override GE_Mesh createBase()
        {
            throw new NotImplementedException();
        }

        //protected override HE_Mesh createBase()
        //{
        //    GE_Mesh mesh = new GE_Mesh();
        //    if (faces != null && vertices != null)
        //    {
        //        if (faces.Length == 0)
        //        {
        //            return mesh;
        //        }
        //        bool useVertexUVM = vertexuvws != null && vertexuvws.Length == vertices.Length;
        //        bool useFaceUVW = uvws != null && faceuvws != null && faceuvws.Length == faces.Length;
        //        GE_Vertex[] uniqueVertices = new GE_Vertex[vertices.Length];
        //        bool[] duplicated = new bool[vertices.Length];
        //        bool useVertexInfo = getUseVertexInfo();

        //    }
        //}
    }
}
