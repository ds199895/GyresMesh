using System;
using System.Collections.Generic;
using Flowing;
using Hsy.Geo;

namespace Examples.Creator
{
  public class TestArea:IApp
  {



    static void Main(string[] args)
    {
      IApp.main();
    }

    List<HS_Point> polypts;
      public override void SetUp()
    {
      Size(1600, 1200);
       polypts= new List<HS_Point>();
      //hole.Add(new HS_Point(-50, 50, 0));
      //hole.Add(new HS_Point(50, 50, 0));
      //hole.Add(new HS_Point(100, 250, 0));

      //hole.Add(new HS_Point(50, 50, 0));
      //hole.Add(new HS_Point(-50, 50, 0));
      //hole.Add(new HS_Point(100, 250, 0));

      
      
      
      polypts.Add(new HS_Point(400, 300, 0));
      polypts.Add(new HS_Point(400, 500, 0));
      polypts.Add(new HS_Point(200, 500, 0));
      polypts.Add(new HS_Point(200, 300, 0));
      HS_Polygon poly = new HS_Polygon().Create(polypts);
      var area=poly.GetSignedArea();
      Print("多边形面积为： " + area);
    }
  
  }
}
