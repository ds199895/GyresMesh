using Hsy.GyresMesh;
using Hsy.Geo;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMeshGH
{
    public static class RhinoSupport
    {
        public static GE_Mesh ToGyresMesh(this Mesh source)
        {
          var vertices = source.Vertices;
          List<HS_Point> pts = new List<HS_Point>();

          foreach (var vertex in vertices)
          {
            HS_Point pt = new HS_Point(vertex.X, vertex.Y, vertex.Z);

            pts.Add(pt);

          }
          var faces = source.Faces;
          List<int[]> faceList = new List<int[]>();
          foreach (var face in faces)
          {
            int[] id = new int[4];
            id[0] = face.A;
            id[1] = face.B;
            id[2] = face.C;
            id[3] = face.D;

            faceList.Add(id);
          }
      var tc = source.TextureCoordinates;
      List<GE_TextureCoordinate> uvs = new List<GE_TextureCoordinate>();
      foreach(var p in tc)
      {
        uvs.Add(new GE_TextureCoordinate(p.X, p.Y, 0));
      }
          GEC_FromFaceList creator = new GEC_FromFaceList();
          creator.setVertices(pts);
          creator.setFaces(faceList);
          creator.setDuplicate(new bool[0]);
          creator.SetVertexUVW(uvs);
          GE_Mesh geMesh= creator.create(); 
          return geMesh;
        }


        public static Mesh ToRhinoMesh(this GE_Mesh source)
        {
            // could add different options for triangulating ngons later
            Mesh rMesh = new Mesh();
   
            foreach (GE_Vertex v in source.GetVertices())
            {
                rMesh.Vertices.Add(v.xf, v.yf, v.zf);
        rMesh.TextureCoordinates.Add(new Point2f(v.GetVertexUVW().uf, v.GetVertexUVW().vf));
            }
            for (int i = 0; i < source.GetFaces().Count; i++)
            {
                GE_Vertex[] fvs = source.GetFaces()[i].GetFaceVertices().ToArray();

                if (fvs.Length == 3)
                {
                    rMesh.Faces.AddFace(source.GetIndex(fvs[0]), source.GetIndex(fvs[1]), source.GetIndex(fvs[2]));
                }
                else if (fvs.Length == 4)
                {
                    rMesh.Faces.AddFace(source.GetIndex(fvs[0]), source.GetIndex(fvs[1]), source.GetIndex(fvs[2]), source.GetIndex(fvs[3]));
                }
                else if (fvs.Length > 4)
                {
                    // triangulate about face center (fan)
                    var fc = source.GetFaces()[i].getFaceCenter();
                    rMesh.Vertices.Add(fc.xf, fc.yf, fc.zf);
                    for (int j = 0; j < fvs.Length; j++)
                    {
                        rMesh.Faces.AddFace(source.GetIndex(fvs[j]), source.GetIndex(fvs[(j + 1) % fvs.Length]), rMesh.Vertices.Count - 1);
                    }
                }
            }
            rMesh.Normals.ComputeNormals();

            


            return rMesh;
        }
    }
}
