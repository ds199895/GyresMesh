using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_CoordinateSystem
    {
        private HS_CoordinateSystem _parent;
        private HS_Point _origin;
        private HS_Vector _X;
        private HS_Vector _Y;
        private HS_Vector _Z;
        private bool _isWorld;

        public static HS_CoordinateSystem WORLD()
        {
            return new HS_CoordinateSystem(true);
        }

        public HS_CoordinateSystem(HS_Coord origin,HS_Coord x,HS_Coord y,HS_Coord z,HS_CoordinateSystem parent)
        {
            this._origin = new HS_Point(origin);
            this._X = new HS_Vector(x);
            this._Y = new HS_Vector(y);
            this._Z = new HS_Vector(z);
            this._parent = parent;
            this._isWorld = this._parent == null;
        }
        public HS_CoordinateSystem(HS_Coord origin, HS_Coord x, HS_Coord y, HS_Coord z)
        {
            this._origin = new HS_Point(origin);
            this._X = new HS_Vector(x);
            this._Y = new HS_Vector(y);
            this._Z = new HS_Vector(z);
            this._parent = WORLD();
            this._isWorld = this._parent == null;
        }
        public HS_CoordinateSystem(HS_Plane P)
        {
            new HS_CoordinateSystem(P.getOrigin(), P.getU(), P.getV(), P.getW());
        }

        public HS_CoordinateSystem(bool world)
        {
            this._origin = new HS_Point();
            this._X = new HS_Vector(HS_Vector.xaxis);
            this._Y = new HS_Vector(HS_Vector.yaxis);
            this._Z = new HS_Vector(HS_Vector.zaxis);
            this._isWorld = world;
            this._parent = world ? null : WORLD();
        }
        public HS_CoordinateSystem()
        {
            this._origin = new HS_Point();
            this._X = new HS_Vector(HS_Vector.xaxis);
            this._Y = new HS_Vector(HS_Vector.yaxis);
            this._Z = new HS_Vector(HS_Vector.zaxis);
            this._isWorld = false;
            this._parent =WORLD();
        }

        public HS_CoordinateSystem(HS_CoordinateSystem parent)
        {
            this._origin = new HS_Point();
            this._X = new HS_Vector(HS_Vector.xaxis);
            this._Y = new HS_Vector(HS_Vector.yaxis);
            this._Z = new HS_Vector(HS_Vector.zaxis);
            this._parent = parent;
            this._isWorld = this._parent == null;
        }

        public  HS_CoordinateSystem get()
        {
            return new HS_CoordinateSystem(this._origin, this. _X, this. _Y, this._Z, this._parent);
        }

        protected void set(HS_Coord origin,HS_Coord x,HS_Coord y,HS_Coord z,HS_CoordinateSystem CS)
        {
            this._origin = new HS_Point(origin);
            this._X = new HS_Vector(x);
            this._Y = new HS_Vector(y);
            this._Z = new HS_Vector(z);
            this._parent = CS;
        }

        public HS_CoordinateSystem setParent(HS_CoordinateSystem parent)
        {
            this._parent = parent;
            this._isWorld = this._parent == null;
            return this;
        }

        public HS_CoordinateSystem setOrigin(HS_Point o)
        {
            this._origin.Set(o);
            return this;
        }

        public HS_CoordinateSystem setOrigin(double ox,double oy,double oz)
        {
            this._origin.Set(ox, oy, oz);
            return this;
        }

        public HS_Vector getX()
        {
            return this._X.copy();
        }
        public HS_Vector getY()
        {
            return this._Y.copy();
        }
        public HS_Vector getZ()
        {
            return this._Z.copy();
        }
        public HS_Point getOrigin()
        {
            return this._origin.copy();
        }
        public HS_CoordinateSystem getParent()
        {
            return this._parent;
        }
        public bool isWorld()
        {
            return this._isWorld;
        }

        public HS_Transform3D getTransformToWorld()
        {
            HS_Transform3D result = new HS_Transform3D();
            Console.WriteLine("this x:  " + this._X);
            result.addFromCSToWorld(this);
            return result;
        }

    }
}
