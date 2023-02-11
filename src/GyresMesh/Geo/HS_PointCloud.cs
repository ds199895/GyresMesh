using System;
using System.Collections;
using System.Collections.Generic;
using Hsy.Core;

namespace Hsy.Geo
{
  public class HS_PointCloud : HS_Geometry, ICollection<HS_Coord>
  {
    private FastList<HS_Coord> pts=new FastList<HS_Coord>();
    public override string Type => "PointCloud";

    public HS_PointCloud()
    {
    }
    public HS_PointCloud(ICollection<HS_Coord> points)
    {
      foreach (HS_Coord p in points)
      {
        Console.WriteLine("p: " + p);
        this.Add(p);
      }
    }

    public int Count => this.pts.Count;

    public bool IsReadOnly => false;

    public void Add(HS_Coord item)
    {
      this.pts.Add(item);
    }
    public HS_Coord this[int index]
    {
      get
      {
        if (index >= this.Count)
        {
          throw new ArgumentOutOfRangeException("index不能大于等于数组的长度");
        }
        return this.pts[index];
      }
      set
      {
        if (index >= this.Count)
        {
          throw new ArgumentOutOfRangeException("index不能大于等于数组的长度");
        }
        this.pts[index] = value;
      }
    }
   

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(HS_Coord item)
    {
      throw new NotImplementedException();
    }

    public void CopyTo(HS_Coord[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public IEnumerator<HS_Coord> GetEnumerator()
    {
      return this.pts.GetEnumerator();
    }

    public bool Remove(HS_Coord item)
    {
      throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.pts.GetEnumerator();
    }
  }
}
