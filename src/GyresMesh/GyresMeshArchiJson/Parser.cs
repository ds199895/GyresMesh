using System;
using System.Collections.Generic;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hsy.GyresMeshArchiJson
{
  public class Parser
  {
    public Parser()
    {

    }

    public static HS_Geometry ParseGeometryFromArchiJson(String jsonStr)
    {
      HS_Geometry geo=null;
      JObject jo = JsonConvert.DeserializeObject<JObject>(jsonStr);

      String type = jo["type"].ToString();
      switch (type)
      {
        case "Mesh":
          geo=GetMeshFromArchiJson(jsonStr);
          break;
        case "Segments":

          if (((bool)jo["closed"]))
          {
            geo = GetPolygonFromArchiJson(jsonStr);
          }
          else{
            geo = GetPolyLineFromArchiJson(jsonStr);
          }

          break;

        case "Vertices":
          geo = GetPointCloudFromArchiJson(jsonStr);
          break;
      }
      return geo;
    }


    public static GE_Mesh GetMeshFromArchiJson(String jsonStr)
    {
      GE_Mesh mesh;
      JObject jo = JsonConvert.DeserializeObject<JObject>(jsonStr);
      List<HS_Point> vertices = GetPointsFromArchiJson((JObject)jo["vertices"]);
      List<int[]> faces = GetFacesFromArchiJson(jo);

      GEC_FromFaceList gecf = new GEC_FromFaceList().setVertices(vertices).setFaces(faces).setDuplicate(new bool[0]);
      mesh = gecf.create();
      return mesh;
    }

    public static HS_PolyLine GetPolyLineFromArchiJson(String jsonStr)
    {
      HS_PolyLine poly;
      JObject jo = JsonConvert.DeserializeObject<JObject>(jsonStr);
      List<HS_Point> points = GetPointsFromArchiJson(jo);
      //Console.WriteLine("already get points!");
      //poly = new HS_PolyLine(points);
      poly = new HS_PolyLine().Create(points);
      return poly;
    }

    public static HS_Polygon GetPolygonFromArchiJson(String jsonStr)
    {
      HS_Polygon poly;
      JObject jo = JsonConvert.DeserializeObject<JObject>(jsonStr);
      List<HS_Point> points = GetPointsFromArchiJson(jo);

      poly = new HS_Polygon().Create(points);
      return poly;
    }
    public static HS_PointCloud GetPointCloudFromArchiJson(String jsonStr)
    {
      JObject json = JsonConvert.DeserializeObject<JObject>(jsonStr);
      JArray array = (JArray)json["coordinates"];
      HS_Point basePoint = new HS_Point((double)json["position"]["x"], (double)json["position"]["y"], (double)json["position"]["z"]);
      //Console.WriteLine(basePoint);
      HS_PointCloud pts = new HS_PointCloud();
      //Console.WriteLine(array.Count);
      for (int i = 0; i < array.Count; i += 3)
      {
        HS_Point pt = new HS_Point(float.Parse(array[i].ToString()), float.Parse(array[i + 1].ToString()), float.Parse(array[i + 2].ToString())) + basePoint;
        //Console.WriteLine("p: " + pt);
        pts.Add(pt);

      }
      return pts;
    }


    public static List<HS_Point> GetPointsFromArchiJson(JObject json)
    {
      JArray array = (JArray)json["coordinates"];
      HS_Point basePoint = new HS_Point((double)json["position"]["x"], (double)json["position"]["y"], (double)json["position"]["z"]);
      //Console.WriteLine(basePoint);
      List<HS_Point> pts = new List<HS_Point>();
      //Console.WriteLine(array.Count);
      for (int i = 0; i < array.Count; i += 3)
      {
        pts.Add(new HS_Point(float.Parse(array[i].ToString()), float.Parse(array[i + 1].ToString()), float.Parse(array[i + 2].ToString()))+basePoint);

      }
      return pts;
    }


    public static List<int[]> GetFacesFromArchiJson(JObject json)
    {
      List<int[]> faces = new List<int[]>();
      int typeNum = ((JArray)json["faces"]["count"]).Count;
      int pos = 0;
      for (int i = 0; i < typeNum; i++)
      {
        int end = pos + (int)json["faces"]["count"][i] * (int)json["faces"]["size"][i];

        for (int j = pos; j < end; j += (int)json["faces"]["size"][i])
        {
          int[] f = new int[(int)json["faces"]["size"][i]];
          for (int k = 0; k < (int)json["faces"]["size"][i]; k++)
          {
            f[k] = (int)json["faces"]["index"][j + k];
          }

          faces.Add(f);
        }
        pos = end;
      }

      return faces;
    }
  }

}
