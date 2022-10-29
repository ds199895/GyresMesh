using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Point:HS_Vector
    {
        public HS_Point(HS_Coord hs_coord)
        {
            
            this.x = hs_coord.X();
            this.y = hs_coord.Y();
            this.z = hs_coord.Z();
        }
        public HS_Point()
        {
            this.x = this.y = this.z = 0;
        }
        public HS_Point(float x,float y,float z)
        {

            this.x = x;
            this.y = y;
            this.z = z;
        }


        public HS_Point(double x, double y, double z)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
        }
        //public float X()
        //{
        //    return (float)this.x;
        //}

        //public float Y()
        //{
        //    return (float)this.y;
        //}

        //public float Z()
        //{
        //    return (float)this.z;
        //}


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
            return "HS_Point "  + " [x=" + X() + ", y=" + Y()
        + ", z=" + Z() + "]";
        }
    }
}
