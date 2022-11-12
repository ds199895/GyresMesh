using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public abstract class HS_Swizzle
    {
        public static HS_Swizzle x = new X();
        public static HS_Swizzle y = new Y();
        public static HS_Swizzle z = new Z();
        public static HS_Swizzle xx = new XX();
        public static HS_Swizzle xy = new XY();
        public static HS_Swizzle xz = new XZ();
        public static HS_Swizzle yx = new YX();
        public static HS_Swizzle yy = new YY();
        public static HS_Swizzle yz = new YZ();
        public static HS_Swizzle zx = new ZX();
        public static HS_Swizzle zy = new ZY();
        public static HS_Swizzle zz = new ZZ();
        public static HS_Swizzle xxx = new XXX();
        public static HS_Swizzle xxy = new XXY();
        public static HS_Swizzle xxz = new XXZ();
        public static HS_Swizzle xyx = new XYX();
        public static HS_Swizzle xyy = new XYY();
        public static HS_Swizzle xyz = new XYZ();
        public static HS_Swizzle xzx = new XZX();
        public static HS_Swizzle xzy = new XZY();
        public static HS_Swizzle xzz = new XZZ();
        public static HS_Swizzle yxx = new YXX();
        public static HS_Swizzle yxy = new YXY();
        public static HS_Swizzle yxz = new YXZ();
        public static HS_Swizzle yyx = new YYX();
        public static HS_Swizzle yyy = new YYY();
        public static HS_Swizzle yyz = new YYZ();
        public static HS_Swizzle yzx = new YZX();
        public static HS_Swizzle yzy = new YZY();
        public static HS_Swizzle yzz = new YZZ();
        public static HS_Swizzle zxx = new ZXX();
        public static HS_Swizzle zxy = new ZXY();
        public static HS_Swizzle zxz = new ZXZ();
        public static HS_Swizzle zyx = new ZYX();
        public static HS_Swizzle zyy = new ZYY();
        public static HS_Swizzle zyz = new ZYZ();
        public static HS_Swizzle zzx = new ZZX();
        public static HS_Swizzle zzy = new ZZY();
        public static HS_Swizzle zzz = new ZZZ();

        public abstract double xd(HS_Coord p);

        public abstract double yd(HS_Coord p);

        public abstract double zd(HS_Coord p);

        public abstract float xf(HS_Coord p);

        public abstract float yf(HS_Coord p);

        public abstract float zf(HS_Coord p);

        public abstract void swizzleSelf(HS_MutableCoord p);

        public HS_Swizzle()
        {
        }

        public class Closest : HS_Swizzle
        {

            HS_Swizzle inter = null;

            public Closest(HS_Coord p)
            {
                if (Math.Abs(p.xd) > Math.Abs(p.yd))
                {
                    inter = Math.Abs(p.xd) > Math.Abs(p.zd) ? yz : xy;
                }
                else
                {
                    inter = Math.Abs(p.yd) > Math.Abs(p.zd) ? xz :xy;
                }
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#xd(HSlut.geom.HS_Coord)
             */
            override
            public double xd(HS_Coord p)
            {

                return inter.xd(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#yd(HSlut.geom.HS_Coord)
             */
            override
            public double yd(HS_Coord p)
            {
                return inter.yd(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#zd(HSlut.geom.HS_Coord)
             */
            override
            public double zd(HS_Coord p)
            {
                return inter.zd(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#xf(HSlut.geom.HS_Coord)
             */
            override
            public float xf(HS_Coord p)
            {
                return inter.xf(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#yf(HSlut.geom.HS_Coord)
             */
            override
            public float yf(HS_Coord p)
            {
                return inter.yf(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#zf(HSlut.geom.HS_Coord)
             */
            override
            public float zf(HS_Coord p)
            {
                return inter.zf(p);
            }

            /*
             * (non-Javadoc)
             *
             * @see HSlut.geom.HS_Swizzle#swizzle(HSlut.geom.HS_MutableCoord)
             */
            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                inter.swizzleSelf(p);

            }

        }

        public class XXX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }


            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.xd, p.xd);
            }

        }

        public class XXY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.xd, p.yd);
            }

        }

        public class XXZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }
            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.xd, p.zd);
            }

        }

        public class XYX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.yd, p.xd);
            }

        }

        public class XYY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.yd, p.yd);
            }

        }

        public class XYZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }
            override

            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {

            }

        }

        public class XZX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }
            override

            public double zd(HS_Coord p)
            {
                return p.xd;
            }
            override

            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.zd, p.xd);
            }

        }

        public class XZY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.zd, p.yd);

            }

        }

        public class XZZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.zd, p.zd);
            }

        }

        public class YXX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }
            override

            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }
            override

            public float xf(HS_Coord p)
            {
                return p.yf;
            }
            override

            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.xd, p.xd);
            }

        }

        public class YXY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.xd, p.yd);
            }

        }

        public class YXZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.xd, p.zd);
            }

        }

        public class YYX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }
            override

            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.yd, p.xd);
            }

        }

        public class YYY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.yd, p.yd);
            }

        }

        public class YYZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.yd, p.zd);
            }

        }

        public class YZX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.zd, p.xd);
            }

        }

        public class YZY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.zd, p.yd);

            }

        }

        public class YZZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.zd, p.zd);
            }

        }

        public class ZXX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.xd, p.xd);
            }

        }

        public class ZXY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.xd, p.yd);
            }

        }

        public class ZXZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.xd, p.zd);
            }

        }

        public class ZYX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }
            override

            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.yd, p.xd);
            }

        }

        public class ZYY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.yd, p.yd);
            }

        }

        public class ZYZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }
            override

            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.yd, p.zd);
            }

        }

        public class ZZX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.zd, p.xd);
            }

        }

        public class ZZY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.zd, p.yd);

            }

        }

        public class ZZZ : HS_Swizzle
        {
            override

            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }
            override

            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.zd, p.zd);
            }

        }

        public class XX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.xd, 0);
            }

        }

        public class XY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.yd, 0);
            }

        }

        public class XZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, p.zd, 0);
            }

        }

        public class YX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.xd, 0);
            }

        }

        public class YY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }
            override

            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.yd, 0);
            }

        }

        public class YZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, p.zd, 0);
            }

        }

        public class ZX : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.xd, 0);
            }

        }

        public class ZY : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.yd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.yd, 0);
            }

        }

        public class ZZ : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float yf(HS_Coord p)
            {
                return p.zf;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, p.zd, 0);
            }

        }

        public class X : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.xd;
            }

            override
            public double yd(HS_Coord p)
            {
                return 0;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.xf;
            }

            override
            public float yf(HS_Coord p)
            {
                return 0;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.xd, 0, 0);
            }

        }

        public class Y : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.yd;
            }
            override

            public double yd(HS_Coord p)
            {
                return 0;
            }
            override

            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.yf;
            }

            override
            public float yf(HS_Coord p)
            {
                return 0;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.yd, 0, 0);
            }

        }

        public class Z : HS_Swizzle
        {

            override
            public double xd(HS_Coord p)
            {
                return p.zd;
            }

            override
            public double yd(HS_Coord p)
            {
                return 0;
            }

            override
            public double zd(HS_Coord p)
            {
                return 0;
            }

            override
            public float xf(HS_Coord p)
            {
                return p.zf;
            }
            override

            public float yf(HS_Coord p)
            {
                return 0;
            }

            override
            public float zf(HS_Coord p)
            {
                return 0.0f;
            }

            override
            public void swizzleSelf(HS_MutableCoord p)
            {
                p.Set(p.zd, 0, 0);
            }

        }



    }




}
