using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Hsy.Geo;

namespace Hsy.IO
{
    public class IObject
    {
    
    public IAttribute attribute;
        public Object[] userData;

        public IObject()
        {
            this.initObject();
        }

        public IObject(IObject var1)
        {
            if (var1.attribute != null)
            {
                this.attribute = var1.attribute.dup();
            }
        }


        public IObject dup()
        {
            return new IObject(this);
        }

        public IObject cp()
        {
            return this.dup();
        }

        public virtual String Type
        {
            get{ return this.attribute != null ? this.attribute.name : null; }
        }

        public IObject name(String var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();

            }
            this.attribute.name = var1;
            return this;

        }
        public ILayer layer()
        {
            return this.attribute != null ? this.attribute.layer : null;
        }

        public IObject layer(ILayer var1)
        {
            if (var1 == null)
            {
                if (this.attribute != null)
                {
                    this.attribute.layer = null;
                }
            }else if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
                if (!var1.contains(this))
                {
                    var1.add(this);
                }
                this.attribute.layer = var1;
            }
            return this;
        }

        //public IObject layer(String var1)
        //{
        //    return 
        //}
        public IAttribute attr()
        {
            return this.attribute;
        }

        public IObject attr(IAttribute var1)
        {
            this.attribute = var1;
            return this;
        }

        public IObject attr(IObject var1)
        {
            if (var1 != null && var1.attr() != null)
            {
                this.attr(var1.attr().dup());
            }

            return this;
        }


        public IAttribute defaultAttribute()
        {
            return new IAttribute();
        }

        public Object[] UserData()
        {
            return this.userData;
        }

        public Object UserData(int var1)
        {
            return this.userData != null && var1 < this.userData.Length  ? this.userData[var1] : null;
        }

        public int userDataNum()
        {
            return this.userData == null ? 0 : this.userData.Length ;
        }

        public IObject UserData(Object[] var1)
        {
            this.userData = var1;
            return this;
        }

        public IObject addUserData(Object var1)
        {
            if (this.userData == null)
            {
                this.userData = new Object[1];
                this.userData[0] = var1;
            }
            else
            {
                Object[] var2 = new Object[this.userData.Length  + 1];

                for (int var3 = 0; var3 < this.userData.Length ; ++var3)
                {
                    var2[var3] = this.userData[var3];
                }

                var2[this.userData.Length ] = var1;
                this.userData = var2;
            }

            return this;
        }

        //    public IObject addUserData(String var1, String var2)
        //    {
        //        Dictionary<String,String> var3 = null;
        //        if (this.userData == null)
        //        {
        //            this.userData = new Object[1];
        //            var3 = new Dictionary<String, String>();
        //            this.userData[0] = var3;
        //        }
        //        else
        //        {
        //            for (int var4 = 0; var4 < this.userData.Length  && var3 == null; ++var4)
        //            {
        //                if (this.userData[var4] is Dictionary<String, String>) {
        //                    Dictionary<String, String> var5 = (Dictionary<String, String>)this.userData[var4];
        //                    HashSet<String> var6 = var5.
        //                    if (var6 != null)
        //                {
        //                    Iterator var7 = var6.iterator();
        //                    if (var7 != null)
        //                    {
        //                        Object var8 = var7.next();
        //                        if (var8 is String) {
        //                            Object var9 = var5.get(var8);
        //                            if (var9 is String) {
        //                                var3 = castStringHashMap(var5);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (var3 == null)
        //        {
        //            Object[] var10 = new Object[this.userData.Length  + 1];

        //            for (int var11 = 0; var11 < this.userData.Length ; ++var11)
        //            {
        //                var10[var11] = this.userData[var11];
        //            }

        //            var3 = new HashMap();
        //            var10[this.userData.Length ] = var3;
        //            this.userData = var10;
        //        }
        //    }

        //    var3.put(var1, var2);
        //    return this;
        //}
        public bool visible()
        {
            if (this.attribute != null)
            {
                return this.attribute.Visible();
            }
            else
            {
                //if (this.graphics != null)
                //{
                //    Iterator var1 = this.graphics.iterator();

                //    while (var1.hasNext())
                //    {
                //        IGraphicObject var2 = (IGraphicObject)var1.next();
                //        if (var2.visible())
                //        {
                //            return true;
                //        }
                //    }
                //}

                return false;
            }
        }

        public bool isVisible()
        {
            return this.visible();
        }

        public IObject hide()
        {
            //if (this.graphics != null)
            //{
            //    Iterator var1 = this.graphics.iterator();

            //    while (var1.hasNext())
            //    {
            //        IGraphicObject var2 = (IGraphicObject)var1.next();
            //        var2.hide();
            //    }
            //}

            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.hide();
            return this;
        }

        public IObject show()
        {
            //if (this.graphics != null)
            //{
            //    Iterator var1 = this.graphics.iterator();

            //    while (var1.hasNext())
            //    {
            //        IGraphicObject var2 = (IGraphicObject)var1.next();
            //        var2.show();
            //    }
            //}

            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.show();
            return this;
        }

        public void initObject()
        {
            this.attribute = new IAttribute();
        }
        public IColor clr()
        {
            if (this.attribute != null && this.attribute.clr() != null)
            {
                return this.attribute.clr();
            }
            else
            {
                //if (this.graphics != null)
                //{
                //    Iterator var1 = this.graphics.iterator();

                //    while (var1.hasNext())
                //    {
                //        IGraphicObject var2 = (IGraphicObject)var1.next();
                //        if (var2.getColor() != null)
                //        {
                //            return var2.getColor();
                //        }
                //    }
                //}

                return IConfig.objectColor;
            }
        }
        public void syncColor()
        {
            this.setColor(this.attribute.clr());

        }
        //public void syncWeight()
        //{
        //    this.

        //}

        public float weight()
        {
            if (this.attribute != null)
            {
                return this.attribute.weight;
            }
            else
            {
                //if (this.graphics != null)
                //{
                //    Iterator var1 = this.graphics.iterator();

                //    while (var1.hasNext())
                //    {
                //        IGraphicObject var2 = (IGraphicObject)var1.next();
                //        if (var2.getWeight() >= 0.0F)
                //        {
                //            return var2.getWeight();
                //        }
                //    }
                //}

                return IConfig.strokeWeight;
            }
        }

        public int redInt()
        {
            return this.clr().getRed();
        }

        public int greenInt()
        {
            return this.clr().getGreen();
        }

        public int blueInt()
        {
            return this.clr().getBlue();
        }

        public int alphaInt()
        {
            return this.clr().getAlpha();
        }

        public int greyInt()
        {
            return this.clr().getGrey();
        }

        public int grayInt()
        {
            return this.greyInt();
        }

        public float red()
        {
            return this.clr().red();
        }

        public float green()
        {
            return this.clr().green();
        }

        public float blue()
        {
            return this.clr().blue();
        }

        public float alpha()
        {
            return this.clr().alpha();
        }

        public float grey()
        {
            return this.clr().grey();
        }

        public float gray()
        {
            return this.clr().gray();
        }

        public float hue()
        {
            return this.clr().hue();
        }

        public float saturation()
        {
            return this.clr().saturation();
        }

        public float brightness()
        {
            return this.clr().brightness();
        }

        public IObject clr(IColor var1)
        {
            if (var1 == null)
            {
                return this;
            }
            else
            {
                if (this.attribute == null)
                {
                    this.attribute = this.defaultAttribute();
                }

                this.attribute.clr(var1);
                this.syncColor();
                return this;
            }
        }

        public IObject clr(IColor var1, int var2)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2);
            this.syncColor();
            return this;
        }

        public IObject clr(IColor var1, float var2)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2);
            this.syncColor();
            return this;
        }

        public IObject clr(IColor var1, double var2)
        {
            return this.clr(var1, (float)var2);
        }

        public IObject clr(IObject var1)
        {
            this.clr(var1.clr());
            return this;
        }

        public Color awtColor()
        {
            if (this.attribute != null)
            {
                return this.attribute.color.awt();
            }
            else
            {
                //if (this.graphics != null)
                //{
                //    Iterator var1 = this.graphics.iterator();

                //    while (var1.hasNext())
                //    {
                //        IGraphicObject var2 = (IGraphicObject)var1.next();
                //        if (var2.getColor() != null)
                //        {
                //            return var2.getColor().awt();
                //        }
                //    }
                //}

                return IConfig.objectColor.awt();
            }
        }

        public Color getAWTColor()
        {
            return this.awtColor();
        }

        public IObject clr(Color var1)
        {
            return this.clr(new IColor(var1));
        }

        public IObject clr(Color var1, int var2)
        {
            return this.clr(new IColor(var1), var2);
        }

        public IObject clr(Color var1, float var2)
        {
            return this.clr(new IColor(var1), var2);
        }

        public IObject clr(Color var1, double var2)
        {
            return this.clr(new IColor(var1), var2);
        }

        public IColor getColor()
        {
            return this.clr();
        }

        public IObject clr(int var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1);
            this.syncColor();
            return this;
        }

        public IObject clr(double var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1);
            this.syncColor();
            return this;
        }

        public IObject clr(float var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1);
            this.syncColor();
            return this;
        }

        public IObject clr(int var1, int var2)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2);
            this.syncColor();
            return this;
        }

        public IObject clr(double var1, double var3)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var3);
            this.syncColor();
            return this;
        }

        public IObject clr(float var1, float var2)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2);
            this.syncColor();
            return this;
        }

        public IObject clr(int var1, int var2, int var3)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2, var3);
            this.syncColor();
            return this;
        }

        public IObject clr(double var1, double var3, double var5)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var3, var5);
            this.syncColor();
            return this;
        }

        public IObject clr(float var1, float var2, float var3)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2, var3);
            this.syncColor();
            return this;
        }

        public IObject clr(int var1, int var2, int var3, int var4)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2, var3, var4);
            this.syncColor();
            return this;
        }

        public IObject clr(double var1, double var3, double var5, double var7)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var3, var5, var7);
            this.syncColor();
            return this;
        }

        public IObject clr(float var1, float var2, float var3, float var4)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.clr(var1, var2, var3, var4);
            this.syncColor();
            return this;
        }

        public IObject hsb(double var1, double var3, double var5, double var7)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.hsb(var1, var3, var5, var7);
            this.syncColor();
            return this;
        }

        public IObject hsb(float var1, float var2, float var3, float var4)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.hsb(var1, var2, var3, var4);
            this.syncColor();
            return this;
        }

        public IObject hsb(double var1, double var3, double var5)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.hsb(var1, var3, var5);
            this.syncColor();
            return this;
        }

        public IObject hsb(float var1, float var2, float var3)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.hsb(var1, var2, var3);
            this.syncColor();
            return this;
        }

        public IObject weight(double var1)
        {
            return this.weight((float)var1);
        }

        public IObject weight(float var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.Weight(var1);
            //this.syncWeight();
            return this;
        }

        public IObject setColor(IColor var1)
        {
            return this.clr(var1);
        }

        public IObject setColor(IColor var1, int var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(IColor var1, float var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(IColor var1, double var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(Color var1)
        {
            return this.clr(var1);
        }

        public IObject setColor(Color var1, int var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(Color var1, float var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(Color var1, double var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(int var1)
        {
            return this.clr(var1);
        }

        public IObject setColor(float var1)
        {
            return this.clr(var1);
        }

        public IObject setColor(double var1)
        {
            return this.clr(var1);
        }

        public IObject setColor(int var1, int var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(float var1, float var2)
        {
            return this.clr(var1, var2);
        }

        public IObject setColor(double var1, double var3)
        {
            return this.clr(var1, var3);
        }

        public IObject setColor(int var1, int var2, int var3)
        {
            return this.clr(var1, var2, var3);
        }

        public IObject setColor(float var1, float var2, float var3)
        {
            return this.clr(var1, var2, var3);
        }

        public IObject setColor(double var1, double var3, double var5)
        {
            return this.clr(var1, var3, var5);
        }

        public IObject setColor(int var1, int var2, int var3, int var4)
        {
            return this.clr(var1, var2, var3, var4);
        }

        public IObject setColor(float var1, float var2, float var3, float var4)
        {
            return this.clr(var1, var2, var3, var4);
        }

        public IObject setColor(double var1, double var3, double var5, double var7)
        {
            return this.clr(var1, var3, var5, var7);
        }

        public IObject setHSBColor(float var1, float var2, float var3, float var4)
        {
            return this.hsb(var1, var2, var3, var4);
        }

        public IObject setHSBColor(double var1, double var3, double var5, double var7)
        {
            return this.hsb(var1, var3, var5, var7);
        }

        public IObject setHSBColor(float var1, float var2, float var3)
        {
            return this.hsb(var1, var2, var3);
        }

        public IObject setHSBColor(double var1, double var3, double var5)
        {
            return this.hsb(var1, var3, var5);
        }

        public IObject texture(ITexture var1)
        {
            if (this.attribute == null)
            {
                this.attribute = this.defaultAttribute();
            }

            this.attribute.texture = var1;
            //if (this.graphics != null)
            //{
            //    Iterator var2 = this.graphics.iterator();

            //    while (var2.hasNext())
            //    {
            //        IGraphicObject var3 = (IGraphicObject)var2.next();
            //        var3.setAttribute(this.attribute);
            //    }
            //}

            return this;
        }

        public IObject setTexture(ITexture var1)
        {
            return this.texture(var1);
        }
    }
}
