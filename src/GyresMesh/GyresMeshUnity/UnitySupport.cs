using System;
using System.Collections;
using System.Collections.Generic;
using Hsy.Geo;
using Hsy.GyresMesh;
using UnityEngine;
namespace Hsy.GyresMeshUnity
{

  public class UnitySupport
  {

    public UnitySupport()
    {

    }

    public static GE_Mesh ToGyresMesh(Mesh umesh)
    {
      GE_Mesh geMesh;

      var vertices = umesh.vertices;
      List<HS_Point> pts = new List<HS_Point>();
      List<GE_TextureCoordinate> vertexUVW = new List<GE_TextureCoordinate>();
      for (int i = 0; i < vertices.Length; i++)
      {
        Vector3 vertex = vertices[i];
        HS_Point pt = new HS_Point(vertex.x, vertex.y, vertex.z);

        pts.Add(pt);
        GE_TextureCoordinate tc = new GE_TextureCoordinate(umesh.uv[i].x, umesh.uv[i].y, 0);
        vertexUVW.Add(tc);
      }
      var faces = umesh.triangles;
      List<int[]> faceList = new List<int[]>();
      for (int i = 0; i < faces.Length; i += 3)
      {
        int[] id = new int[3];
        id[0] = faces[i];
        id[1] = faces[i + 1];
        id[2] = faces[i + 2];
        faceList.Add(id);
      }
      GEC_FromFaceList creator = new GEC_FromFaceList();
      creator.setVertices(pts);
      creator.setFaces(faceList);
      creator.setDuplicate(new bool[0]);
      creator.SetVertexUVW(vertexUVW);
      geMesh = creator.create();

      return geMesh;
    }

    public static Mesh ToUnityMesh(GE_Mesh gmesh)
    {

      // could add different options for triangulating ngons later
      Mesh rMesh = new Mesh();
      List<Vector3> vers = new List<Vector3>();
      List<int> tris = new List<int>();
      List<Vector2> uv = new List<Vector2>();
      for (int i = 0; i < gmesh.GetVertices().Count; i++)
      {
        var p = gmesh.getVertexWithIndex(i);
        vers.Add(new Vector3(p.xf, p.yf, p.zf));
        if (p.HasVertexUVW())
        {
          uv.Add(new Vector2(p.GetVertexUVW().xf, p.GetVertexUVW().yf));
        }

      }
      for (int i = 0; i < gmesh.GetFaces().Count; i++)
      {
        GE_Vertex[] fvs = gmesh.GetFaces()[i].GetFaceVertices().ToArray();

        if (fvs.Length == 3)
        {
          tris.Add(gmesh.GetIndex(fvs[0]));
          tris.Add(gmesh.GetIndex(fvs[1]));
          tris.Add(gmesh.GetIndex(fvs[2]));
        }
        else if (fvs.Length > 4)
        {
          // triangulate about face center (fan)
          var fc = gmesh.GetFaces()[i].getFaceCenter();
          vers.Add(new Vector3(fc.xf, fc.yf, fc.zf));
          for (int j = 0; j < fvs.Length; j++)
          {
            tris.Add(gmesh.GetIndex(fvs[j]));
            tris.Add(gmesh.GetIndex(fvs[(j + 1) % fvs.Length]));
            tris.Add(vers.Count - 1);
          }
        }
      }
      Vector4[] tangents = new Vector4[vers.Count];
      Vector2[] uvs = new Vector2[vers.Count];
      Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
      for (int i = 0; i < vers.Count; i++)
      {
        tangents[i] = tangent;

      }
      rMesh.vertices = vers.ToArray();
      rMesh.triangles = tris.ToArray();

      rMesh.tangents = tangents;

      rMesh.RecalculateNormals();

      Vector3[] normals = rMesh.normals;

      if (uv != null && uv.Count == gmesh.GetNumberOfVertices())
      {
        uvs = uv.ToArray();
      }
      else
      {
        //var aabb = gmesh.getAABB();
        //float ax = (float)((aabb.getMinX()+ aabb.getMaxX())/2);
        //float ay = (float)((aabb.getMinY() + aabb.getMaxY()) / 2);
        //float az = (float)((aabb.getMinZ() + aabb.getMaxZ()) / 2);

        //float a = (ax + ay + az) / 3;


        float a = 2;
        for (int i = 0; i < normals.Length; i++)
        {
          //X-Plane
          if (Mathf.Abs(normals[i].x) > Mathf.Abs(normals[i].y) && Mathf.Abs(normals[i].x) > Mathf.Abs(normals[i].z))
          {
            //float ave= (vers[i].y + vers[i].z)/ 2;
            uvs[i] = new Vector2(vers[i].y / a, vers[i].z / a);
          }
          //Y-Plane
          if (Mathf.Abs(normals[i].y) > Mathf.Abs(normals[i].x) && Mathf.Abs(normals[i].y) > Mathf.Abs(normals[i].z))
          {
            //float ave = (vers[i].x + vers[i].z) / 2;
            uvs[i] = new Vector2(vers[i].x / a, vers[i].z / a);
          }
          //Z-Plane
          if (Mathf.Abs(normals[i].z) > Mathf.Abs(normals[i].x) && Mathf.Abs(normals[i].z) > Mathf.Abs(normals[i].y))
          {
            //float ave = (vers[i].x + vers[i].y) / 2;
            uvs[i] = new Vector2(vers[i].x / a, vers[i].y / a);
          }
        }
      }


      rMesh.uv = uvs;

      return rMesh;

    }
  }

}
