using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.IO
{
    public class ILayer:IObject
    {
        public List<IObject> objects;
        public ILayer()
        {
            //this.attribute=new I
            this.objects = new List<IObject>();
        }

        public ILayer(String var1) : base()
        {
            this.attribute.name = var1;

        }

        public int num()
        {
            return this.objects.Count;
        }

        public IObject get(int var1)
        {
            return (IObject)this.objects[var1];
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ILayer add(IObject var1)
        {
            if (!this.objects.Contains(var1))
            {
                this.objects.Add(var1);
                if (var1.layer() != this)
                {
                    var1.layer(this);
                }
            }

            return this;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ILayer remove(int var1)
        {
            this.objects.RemoveAt(var1);
            return this;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ILayer remove(IObject var1)
        {
            this.objects.Remove(var1);
            return this;
        }

        public bool contains(IObject var1)
        {
            return this.objects.Contains(var1);
        }

        public List<IObject> allObjects()
        {
            return this.objects;
        }

        public List<IObject> getAllObjects()
        {
            return this.allObjects();
        }

        //        public IPoint[] points()
        //        {
        //            List var1 = new List();
        //            synchronized(this.server) {
        //                for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //                {
        //                    if (this.objects.get(var3) is IPoint) {
        //                    var1.Add((IPoint)this.objects.get(var3));
        //                }
        //            }

        //            return (IPoint[])var1.toArray(new IPoint[var1.Count]);
        //        }
        //    }

        //    public IPoint[] getPoints()
        //    {
        //        return this.points();
        //    }

        //    public IPoint[] pts()
        //    {
        //        return this.points();
        //    }

        //    public ICurve[] curves()
        //    {
        //        List var1 = new List();
        //        synchronized(this.server) {
        //            for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //            {
        //                if (this.objects.get(var3) is ICurve) {
        //                var1.Add((ICurve)this.objects.get(var3));
        //            }
        //        }

        //        return (ICurve[])var1.toArray(new ICurve[var1.Count]);
        //    }
        //}

        //public ICurve[] getCurves()
        //{
        //    return this.curves();
        //}

        //public ICurve[] crvs()
        //{
        //    return this.curves();
        //}

        //public ISurface[] surfaces()
        //{
        //    List var1 = new List();
        //    synchronized(this.server) {
        //        for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //        {
        //            if (this.objects.get(var3) is ISurface) {
        //    var1.Add((ISurface)this.objects.get(var3));
        //}
        //            }

        //            return (ISurface[])var1.toArray(new ISurface[var1.Count]);
        //        }
        //    }

        //    public ISurface[] getSurfaces()
        //{
        //    return this.surfaces();
        //}

        //public ISurface[] srfs()
        //{
        //    return this.surfaces();
        //}

        //public IMesh[] meshes()
        //{
        //    List var1 = new List();
        //    synchronized(this.server) {
        //        for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //        {
        //            if (this.objects.get(var3) is IMesh) {
        //    var1.Add((IMesh)this.objects.get(var3));
        //}
        //            }

        //            return (IMesh[])var1.toArray(new IMesh[var1.Count]);
        //        }
        //    }

        //    public IMesh[] getMeshes()
        //{
        //    return this.meshes();
        //}

        //public IBrep[] breps()
        //{
        //    List var1 = new List();
        //    synchronized(this.server) {
        //        for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //        {
        //            if (this.objects.get(var3) is IBrep) {
        //    var1.Add((IBrep)this.objects.get(var3));
        //}
        //            }

        //            return (IBrep[])var1.toArray(new IBrep[var1.Count]);
        //        }
        //    }

        //    public IBrep[] getBreps()
        //{
        //    return this.breps();
        //}

        //public IGeometry[] geometries()
        //{
        //    List var1 = new List();
        //    synchronized(this.server) {
        //        for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //        {
        //            if (this.objects.get(var3) is IGeometry) {
        //    var1.Add((IGeometry)this.objects.get(var3));
        //}
        //            }

        //            return (IGeometry[])var1.toArray(new IGeometry[var1.Count]);
        //        }
        //    }

        //    public IGeometry[] getGeometries()
        //{
        //    return this.geometries();
        //}

        //public IGeometry[] geos()
        //{
        //    return this.geometries();
        //}

        //public IObject[] objects(Class var1)
        //{
        //    List var2 = new List();
        //    synchronized(this.server) {
        //        for (int var4 = 0; var4 < var2.Count; ++var4)
        //        {
        //            if (var1.isInstance(this.objects.get(var4)))
        //            {
        //                var2.Add(this.objects.get(var4));
        //            }
        //        }

        //        return (IObject[])var2.toArray(new IObject[var2.Count]);
        //    }
        //}

        //public IObject[] getObjects(Class var1)
        //{
        //    return this.objects(var1);
        //}

        //public IObject[] objs(Class var1)
        //{
        //    return this.objects(var1);
        //}

        //public IObject[] objects()
        //{
        //    return (IObject[])this.objects.toArray(new IObject[this.objects.Count]);
        //}

        //public IObject[] getObjects()
        //{
        //    return this.objects();
        //}

        //public IObject[] objs()
        //{
        //    return this.objects();
        //}

        //public synchronized IPoint point(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is IPoint && var1 == var2++) {
        //        return (IPoint)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public IPoint getPoint(int var1)
        //{
        //    return this.point(var1);
        //}

        //public IPoint pt(int var1)
        //{
        //    return this.point(var1);
        //}

        //public synchronized ICurve curve(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is ICurve && var1 == var2++) {
        //        return (ICurve)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public ICurve getCurve(int var1)
        //{
        //    return this.curve(var1);
        //}

        //public ICurve crv(int var1)
        //{
        //    return this.curve(var1);
        //}

        //public synchronized ISurface surface(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is ISurface && var1 == var2++) {
        //        return (ISurface)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public ISurface getSurface(int var1)
        //{
        //    return this.surface(var1);
        //}

        //public ISurface srf(int var1)
        //{
        //    return this.surface(var1);
        //}

        //public synchronized IMesh mesh(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is IMesh && var1 == var2++) {
        //        return (IMesh)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public IMesh getMesh(int var1)
        //{
        //    return this.mesh(var1);
        //}

        //public synchronized IBrep brep(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is IBrep && var1 == var2++) {
        //        return (IBrep)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public IBrep getBrep(int var1)
        //{
        //    return this.brep(var1);
        //}

        //public synchronized IGeometry geometry(int var1) {
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (this.objects.get(var3) is IGeometry && var1 == var2++) {
        //        return (IGeometry)this.objects.get(var3);
        //    }
        //}

        //return null;
        //    }

        //    public IGeometry getGeometry(int var1)
        //{
        //    return this.geometry(var1);
        //}

        //public IGeometry geo(int var1)
        //{
        //    return this.geometry(var1);
        //}

        //public synchronized IObject object(Class var1, int var2) {
        //    int var3 = 0;

        //    for (int var4 = 0; var4 < this.objects.Count; ++var4)
        //    {
        //        if (var1.isInstance(this.objects.get(var4)) && var2 == var3++)
        //        {
        //            return (IObject)this.objects.get(var4);
        //        }
        //    }

        //    return null;
        //}

        //public IObject getObject(Class var1, int var2)
        //{
        //    return this.object(var1, var2);
        //}

        //public IObject obj(Class var1, int var2)
        //{
        //    return this.object(var1, var2);
        //}

        //public synchronized IObject object(int var1) {
        //    return var1 >= 0 && var1 < this.objects.Count ? (IObject)this.objects.get(var1) : null;
        //}

        //public IObject getObject(int var1)
        //{
        //    return this.object(var1);
        //}

        //public IObject obj(int var1)
        //{
        //    return this.object(var1);
        //}

        //public synchronized int pointNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is IPoint) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getPointNum()
        //{
        //    return this.pointNum();
        //}

        //public int ptNum()
        //{
        //    return this.pointNum();
        //}

        //public synchronized int curveNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is ICurve) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getCurveNum()
        //{
        //    return this.curveNum();
        //}

        //public int crvNum()
        //{
        //    return this.curveNum();
        //}

        //public synchronized int surfaceNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is ISurface) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getSurfaceNum()
        //{
        //    return this.surfaceNum();
        //}

        //public int srfNum()
        //{
        //    return this.surfaceNum();
        //}

        //public synchronized int meshNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is IMesh) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getMeshNum()
        //{
        //    return this.meshNum();
        //}

        //public synchronized int brepNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is IBrep) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getBrepNum()
        //{
        //    return this.brepNum();
        //}

        //public synchronized int geometryNum()
        //{
        //    int var1 = 0;

        //    for (int var2 = 0; var2 < this.objects.Count; ++var2)
        //    {
        //        if (this.objects.get(var2) is IGeometry) {
        //    ++var1;
        //}
        //        }

        //        return var1;
        //    }

        //    public int getGeometryNum()
        //{
        //    return this.geometryNum();
        //}

        //public int geoNum()
        //{
        //    return this.geometryNum();
        //}

        //public synchronized int objectNum(Class var1)
        //{
        //    int var2 = 0;

        //    for (int var3 = 0; var3 < this.objects.Count; ++var3)
        //    {
        //        if (var1.isInstance(this.objects.get(var3)))
        //        {
        //            ++var2;
        //        }
        //    }

        //    return var2;
        //}

        //public int getObjectNum(Class var1)
        //{
        //    return this.objectNum(var1);
        //}

        //public int objNum(Class var1)
        //{
        //    return this.objectNum(var1);
        //}

        //public synchronized int objectNum()
        //{
        //    return this.objects.Count;
        //}

        //public int getObjectNum()
        //{
        //    return this.objectNum();
        //}

        //public int objNum()
        //{
        //    return this.objectNum();
        //}

        //public ILayer name(String var1)
        //{
        //    this.attribute.name = var1;
        //    return this;
        //}

        //public String toString()
        //{
        //    return this.name();
        //}

        //public bool visible()
        //{
        //    return this.attribute.visible();
        //}

        public ILayer setVisible(bool var1)
        {
            return this.visible(var1);
        }

        public ILayer visible(bool var1)
        {
            this.attribute.Visible(var1);
            if (this.attribute.Visible())
            {
                this.show();
            }
            else
            {
                this.hide();
            }

            return this;
        }

        //public ILayer hide()
        //{
        //    base.hide();
        //    this.attribute.hide();
        //    Iterator var1 = this.objects.iterator();

        //    while (var1.hasNext())
        //    {
        //        IObject var2 = (IObject)var1.next();
        //        var2.hide();
        //    }

        //    return this;
        //}

        //public ILayer show()
        //{
        //    base.show();
        //    this.attribute.show();
        //    Iterator var1 = this.objects.iterator();

        //    while (var1.hasNext())
        //    {
        //        IObject var2 = (IObject)var1.next();
        //        var2.show();
        //    }

        //    return this;
        //}

        //public IColor clr()
        //{
        //    return this.attribute.clr();
        //}

        //public Color color()
        //{
        //    return this.attribute.color();
        //}

        //public Color awtColor()
        //{
        //    return this.attribute.awtColor();
        //}

        //public ILayer clr(IColor var1)
        //{
        //    this.attribute.clr(var1);
        //    return this;
        //}

        //public ILayer clr(IColor var1, int var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(IColor var1, float var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(IColor var1, double var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(IObject var1)
        //{
        //    base.clr(var1);
        //    return this;
        //}

        //public ILayer clr(Color var1)
        //{
        //    this.attribute.clr(var1);
        //    return this;
        //}

        //public ILayer clr(Color var1, int var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(Color var1, float var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(Color var1, double var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(int var1)
        //{
        //    this.attribute.clr(var1);
        //    return this;
        //}

        //public ILayer clr(float var1)
        //{
        //    this.attribute.clr(var1);
        //    return this;
        //}

        //public ILayer clr(double var1)
        //{
        //    this.attribute.clr(var1);
        //    return this;
        //}

        //public ILayer clr(int var1, int var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(float var1, float var2)
        //{
        //    this.attribute.clr(var1, var2);
        //    return this;
        //}

        //public ILayer clr(double var1, double var3)
        //{
        //    this.attribute.clr(var1, var3);
        //    return this;
        //}

        //public ILayer clr(int var1, int var2, int var3)
        //{
        //    this.attribute.clr(var1, var2, var3);
        //    return this;
        //}

        //public ILayer clr(float var1, float var2, float var3)
        //{
        //    this.attribute.clr(var1, var2, var3);
        //    return this;
        //}

        //public ILayer clr(double var1, double var3, double var5)
        //{
        //    this.attribute.clr(var1, var3, var5);
        //    return this;
        //}

        //public ILayer clr(int var1, int var2, int var3, int var4)
        //{
        //    this.attribute.clr(var1, var2, var3, var4);
        //    return this;
        //}

        //public ILayer clr(float var1, float var2, float var3, float var4)
        //{
        //    this.attribute.clr(var1, var2, var3, var4);
        //    return this;
        //}

        //public ILayer clr(double var1, double var3, double var5, double var7)
        //{
        //    this.attribute.clr(var1, var3, var5, var7);
        //    return this;
        //}

        //public ILayer hsb(float var1, float var2, float var3, float var4)
        //{
        //    this.attribute.hsb(var1, var2, var3, var4);
        //    return this;
        //}

        //public ILayer hsb(double var1, double var3, double var5, double var7)
        //{
        //    this.attribute.hsb(var1, var3, var5, var7);
        //    return this;
        //}

        //public ILayer hsb(float var1, float var2, float var3)
        //{
        //    this.attribute.hsb(var1, var2, var3);
        //    return this;
        //}

        //public ILayer hsb(double var1, double var3, double var5)
        //{
        //    this.attribute.hsb(var1, var3, var5);
        //    return this;
        //}

        //public IColor getColor()
        //{
        //    return this.clr();
        //}

        //public Color getAWTColor()
        //{
        //    return this.color();
        //}

        //public ILayer setColor(IColor var1)
        //{
        //    return this.clr(var1);
        //}

        //public ILayer setColor(IColor var1, int var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(IColor var1, float var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(IColor var1, double var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(Color var1)
        //{
        //    return this.clr(var1);
        //}

        //public ILayer setColor(Color var1, int var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(Color var1, float var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(Color var1, double var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(int var1)
        //{
        //    return this.clr(var1);
        //}

        //public ILayer setColor(float var1)
        //{
        //    return this.clr(var1);
        //}

        //public ILayer setColor(double var1)
        //{
        //    return this.clr(var1);
        //}

        //public ILayer setColor(int var1, int var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(float var1, float var2)
        //{
        //    return this.clr(var1, var2);
        //}

        //public ILayer setColor(double var1, double var3)
        //{
        //    return this.clr(var1, var3);
        //}

        //public ILayer setColor(int var1, int var2, int var3)
        //{
        //    return this.clr(var1, var2, var3);
        //}

        //public ILayer setColor(float var1, float var2, float var3)
        //{
        //    return this.clr(var1, var2, var3);
        //}

        //public ILayer setColor(double var1, double var3, double var5)
        //{
        //    return this.clr(var1, var3, var5);
        //}

        //public ILayer setColor(int var1, int var2, int var3, int var4)
        //{
        //    return this.clr(var1, var2, var3, var4);
        //}

        //public ILayer setColor(float var1, float var2, float var3, float var4)
        //{
        //    return this.clr(var1, var2, var3, var4);
        //}

        //public ILayer setColor(double var1, double var3, double var5, double var7)
        //{
        //    return this.clr(var1, var3, var5, var7);
        //}

        //public ILayer setHSBColor(float var1, float var2, float var3, float var4)
        //{
        //    return this.hsb(var1, var2, var3, var4);
        //}

        //public ILayer setHSBColor(double var1, double var3, double var5, double var7)
        //{
        //    return this.hsb(var1, var3, var5, var7);
        //}

        //public ILayer setHSBColor(float var1, float var2, float var3)
        //{
        //    return this.hsb(var1, var2, var3);
        //}

        //public ILayer setHSBColor(double var1, double var3, double var5)
        //{
        //    return this.hsb(var1, var3, var5);
        //}

        //public ILayer weight(double var1)
        //{
        //    base.weight(var1);
        //    return this;
        //}

        //public ILayer weight(float var1)
        //{
        //    base.weight(var1);
        //    return this;
        //}

        //public ILayer setMaterial(IMaterial var1)
        //{
        //    this.attribute.material = var1;
        //    return this;
        //}

    }
}
