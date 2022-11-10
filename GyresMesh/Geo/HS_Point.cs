using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Point:HS_Vector
    {
        public HS_Point(HS_Coord hs_coord):base(hs_coord)
        {
        }
        public HS_Point()
        {
            this.xd = this.yd = this.zd = 0;
        }
        public HS_Point(float x, float y, float z):base(x,y,z)
        {
        }


        public HS_Point(double x, double y, double z):base(x,y,z)
        {
        }

        //public void Set(HS_Coord c)
        //{
        //    this.x = c.X();
        //    this.y = c.Y();
        //    this.z = c.Z();
        //}

        //public void SetX(float x)
        //{
        //    this.x = x;
        //}

        //public void SetY(float y)
        //{
        //    this.y = y;
        //}

        //public void SetZ(float z)
        //{
        //    this.z = z;
        //}

        //public int CompareTo(HS_Coord p)
        //{
        //    int cmp = this.X().CompareTo(p.X());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    cmp = this.Y().CompareTo(p.Y());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    cmp = this.Z().CompareTo(p.Z());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    return 0;
        //}

        override
        public String ToString()
        {
            return "HS_Point "  + " [x=" + xd + ", y=" + yd
        + ", z=" + zd+ "]";
        }


    }
}
