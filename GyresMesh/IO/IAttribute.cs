using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hsy.IO
{
    public class IAttribute
    {
        public int id = -1;
        public String name;
        public ILayer layer;
        public IColor color;
        public IColor stroke;
        public float size = 1.0F;
        public float weight = 1.0F;
        public IMaterial material;
        public ITexture texture;
        public bool visible = true;

        public IAttribute()
        {
        }

        public IAttribute(IAttribute var1)
        {
            if (var1.name != null)
            {
                this.name = new String(var1.name.ToArray());
            }

            this.layer = var1.layer;
            if (var1.color != null)
            {
                this.color = var1.color.dup();
            }

            this.size = var1.size;
            this.weight = var1.weight;
            this.material = var1.material;
            this.visible = var1.visible;
        }

        public IAttribute dup()
        {
            return new IAttribute(this);
        }

        public IAttribute cp()
        {
            return this.dup();
        }

        public IAttribute set(IAttribute var1)
        {
            this.name = new String(var1.name.ToArray());
            this.layer = var1.layer;
            this.color = var1.color.dup();
            this.size = var1.size;
            this.weight = var1.weight;
            this.material = var1.material;
            this.visible = var1.visible;
            return this;
        }

        public IAttribute merge(IAttribute var1)
        {
            if (this.layer == null)
            {
                this.layer = var1.layer;
            }

            if (this.color == null)
            {
                this.color = var1.color;
            }

            if (this.size != 1.0F)
            {
                this.size = var1.size;
            }

            if (this.weight != 1.0F)
            {
                this.weight = var1.weight;
            }

            if (this.material == null)
            {
                this.material = var1.material;
            }

            if (!this.visible)
            {
                this.visible = var1.visible;
            }

            return this;
        }

        public void show()
        {
            this.visible = true;
        }

        public void hide()
        {
            this.visible = false;
        }

        public IAttribute Visible(bool var1)
        {
            this.visible = var1;
            return this;
        }

        public bool Visible()
        {
            return this.visible;
        }

        public bool isVisible()
        {
            return this.Visible();
        }

        public Color Color()
        {
            return this.color.awt();
        }

        public Color awtColor()
        {
            return this.Color();
        }

        public IColor clr()
        {
            return this.color;
        }

        public IAttribute clr(IColor var1)
        {
            this.color = var1;
            return this;
        }

        public IAttribute clr(IColor var1, int var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(IColor var1, float var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(IColor var1, double var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(Color var1)
        {
            this.color = new IColor(var1);
            return this;
        }

        public IAttribute clr(Color var1, int var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(Color var1, float var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(Color var1, double var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(int var1)
        {
            this.color = new IColor(var1);
            return this;
        }

        public IAttribute clr(float var1)
        {
            this.color = new IColor(var1);
            return this;
        }

        public IAttribute clr(double var1)
        {
            this.clr((float)var1);
            return this;
        }

        public IAttribute clr(int var1, int var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(float var1, float var2)
        {
            this.color = new IColor(var1, var2);
            return this;
        }

        public IAttribute clr(double var1, double var3)
        {
            this.clr((float)var1, (float)var3);
            return this;
        }

        public IAttribute clr(int var1, int var2, int var3)
        {
            this.color = new IColor(var1, var2, var3);
            return this;
        }

        public IAttribute clr(float var1, float var2, float var3)
        {
            this.color = new IColor(var1, var2, var3);
            return this;
        }

        public IAttribute clr(double var1, double var3, double var5)
        {
            this.clr((float)var1, (float)var3, (float)var5);
            return this;
        }

        public IAttribute clr(int var1, int var2, int var3, int var4)
        {
            this.color = new IColor(var1, var2, var3, var4);
            return this;
        }

        public IAttribute clr(float var1, float var2, float var3, float var4)
        {
            this.color = new IColor(var1, var2, var3, var4);
            return this;
        }

        public IAttribute clr(double var1, double var3, double var5, double var7)
        {
            this.clr((float)var1, (float)var3, (float)var5, (float)var7);
            return this;
        }

        public IAttribute hsb(float var1, float var2, float var3, float var4)
        {
            this.color = IColor.hsb(var1, var2, var3, var4);
            return this;
        }

        public IAttribute hsb(double var1, double var3, double var5, double var7)
        {
            return this.hsb((float)var1, (float)var3, (float)var5, (float)var7);
        }

        public IAttribute hsb(float var1, float var2, float var3)
        {
            this.color = IColor.hsb(var1, var2, var3);
            return this;
        }

        public IAttribute hsb(double var1, double var3, double var5)
        {
            return this.hsb((float)var1, (float)var3, (float)var5);
        }

        public float Weight()
        {
            return this.weight;
        }

        public IAttribute Weight(float var1)
        {
            this.weight = var1;
            return this;
        }

        public IAttribute Weight(double var1)
        {
            this.weight = (float)var1;
            return this;
        }

        public Color getAWTColor()
        {
            return this.Color();
        }

        public IColor getColor()
        {
            return this.clr();
        }

        public IAttribute setColor(Color var1)
        {
            return this.clr(var1);
        }

        public IAttribute setColor(int var1)
        {
            return this.clr(var1);
        }

        public IAttribute setColor(float var1)
        {
            return this.clr(var1);
        }

        public IAttribute setColor(double var1)
        {
            return this.clr(var1);
        }

        public IAttribute setColor(int var1, int var2)
        {
            return this.clr(var1, var2);
        }

        public IAttribute setColor(float var1, float var2)
        {
            return this.clr(var1, var2);
        }

        public IAttribute setColor(double var1, double var3)
        {
            return this.clr(var1, var3);
        }

        public IAttribute setColor(int var1, int var2, int var3)
        {
            return this.clr(var1, var2, var3);
        }

        public IAttribute setColor(float var1, float var2, float var3)
        {
            return this.clr(var1, var2, var3);
        }

        public IAttribute setColor(double var1, double var3, double var5)
        {
            return this.clr(var1, var3, var5);
        }

        public IAttribute setColor(int var1, int var2, int var3, int var4)
        {
            return this.clr(var1, var2, var3, var4);
        }

        public IAttribute setColor(float var1, float var2, float var3, float var4)
        {
            return this.clr(var1, var2, var3, var4);
        }

        public IAttribute setColor(double var1, double var3, double var5, double var7)
        {
            return this.clr(var1, var3, var5, var7);
        }

        public IAttribute setHSBColor(float var1, float var2, float var3, float var4)
        {
            return this.hsb(var1, var2, var3, var4);
        }

        public IAttribute setHSBColor(double var1, double var3, double var5, double var7)
        {
            return this.hsb(var1, var3, var5, var7);
        }

        public IAttribute setHSBColor(float var1, float var2, float var3)
        {
            return this.hsb(var1, var2, var3);
        }

        public IAttribute setHSBColor(double var1, double var3, double var5)
        {
            return this.hsb(var1, var3, var5);
        }

        public IAttribute setWeight(float var1)
        {
            return this.Weight(var1);
        }

        public String toString()
        {
            return "id=" + this.id + ", name=" + this.name + ", layer=" + this.layer + ", color=" + this.color + ", stroke=" + this.stroke + ", size=" + this.size + ", weight=" + this.weight + ", material=" + this.material + ", visible=" + this.visible;
        }
    }
}
