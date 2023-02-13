using System;
using Hsy.Core;
using Hsy.Geo;
using Hsy.HsMath;

namespace Hsy.GyresMesh
{
  public class GE_TextureCoordinate:HS_MutableCoord
  {
    public float xf { get => (float)this.u; set => this.u=value; }
    public float yf { get => (float)this.v; set => this.v = value; }
    public float zf { get => (float)this.w; set => this.w = value; }
    public double xd { get => this.u; set => this.u = value; }
    public double yd { get => this.v; set => this.v = value; }
    public double zd { get => this.w; set => this.w = value; }

    public float uf { get => (float)this.u; set => this.u = value; }
    public float vf { get => (float)this.v; set => this.v = value; }
    public float wf { get => (float)this.w; }
    public double ud { get => this.u; set => this.u = value; }
    public double vd { get => this.v; set => this.v = value; }
    public double wd { get => this.w; }


    public static readonly GE_TextureCoordinate ZERO = new GE_TextureCoordinate();
    private double u, v, w;
    public GE_TextureCoordinate()
    {
      u = v = w = 0;
    }
    public GE_TextureCoordinate(HS_Coord uvw)
    {
      u = uvw.xd;
      v = uvw.yd;
      w = uvw.zd;
    }

    public GE_TextureCoordinate(double f, GE_TextureCoordinate uvw1, GE_TextureCoordinate uvw2)
    {
      double omf = 1.0 - f;
      u = f * uvw1.ud + omf * uvw2.ud;
      v = f * uvw1.vd + omf * uvw2.vd;
      w = f * uvw1.wd + omf * uvw2.wd;
    }

    public GE_TextureCoordinate(double u,double v)
    {
      this.u = u;
      this.v = v;
      this.w = 0;
    }
    public GE_TextureCoordinate(double u, double v,double w)
    {
      this.u = u;
      this.v = v;
      this.w = w;
    }

    public void clear()
    {
      u = v = w = 0;
    }

    /**
 *
 *
 * @param u
 * @param v
 * @param w
 */
    public void setUVW( double u,  double v,  double w)
    {
      this.u = u;
      this.v = v;
      this.w = w;
    }

    /**
     *
     *
     * @param u
     */
    public void setUVW( HS_Coord u)
    {
      this.u = u.xd;
      this.v = u.yd;
      this.w = u.zd;
    }


    public int CompareTo(HS_Coord other)
    {
      int cmp = xd.CompareTo( other.xd);

      if (cmp != 0)
      {
        return cmp;
      }
      cmp = yd.CompareTo(other.yd);
      if (cmp != 0)
      {
        return cmp;
      }
      cmp = zd.CompareTo(other.zd);
      if (cmp != 0)
      {
        return cmp;
      }
      return wd.CompareTo(other.wd);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }
      if (obj == this)
      {
        return true;
      }
      if (!(obj is HS_Coord)) {
        return false;
      }
      HS_Coord p = (HS_Coord)obj;
      if (!HS_Epsilon.isEqual(xd, p.xd))
      {
        return false;
      }
      if (!HS_Epsilon.isEqual(yd, p.yd))
      {
        return false;
      }
      if (!HS_Epsilon.isEqual(zd, p.zd))
      {
        return false;
      }
      return true;
    }
    public override int GetHashCode()
    {
      return HS_HashCode.calculateHashCode(this) ;
    }

    public double getd(int i)
    {
      if (i == 0)
      {
        return u;
      }
      if (i == 1)
      {
        return v;
      }
      if (i == 2)
      {
        return w;
      }
      return Double.NaN;
    }

    public void Set(HS_Coord z)
    {
      u = z.xd;
      v = z.yd;
      w = z.zd;
    }

    public void Set(double x, double y, double z)
    {
      u = x;
      v = y;
      w = z;
    }

    public void Set(double x, double y)
    {
      u = x;
      v = y;
    }

    public void SetX(float x)
    {
      u = x;
    }

    public void SetX(double x)
    {
      u = x;
    }

    public void SetY(float y)
    {
      v = y;
    }

    public void SetY(double y)
    {
      v = y;
    }

    public void SetZ(float z)
    {
      w = z;
    }

    public void SetZ(double z)
    {
      w = z;
    }

    public override string ToString()
    {
      return "Texture Coordinate: [u=" + ud + ", v=" + vd + ", w=" + wd + "]"; ;
    }


  }
}
