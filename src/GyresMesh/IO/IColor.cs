using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hsy.IO
{
    public class IColor
    {
        public float[] rgba;

        public IColor()
        {
            this.rgba = new float[4];
        }

        public IColor(float[] var1)
        {
            this.rgba = var1;
        }

        public IColor(float var1, float var2, float var3, float var4):this()
        {

            this.set(var1, var2, var3, var4);
        }

        public IColor(float var1, float var2, float var3) : this(var1, var2, var3, 1.0F)
        {

        }

        public IColor(float var1, float var2): this(var1, var1, var1, var2)
        {

        }

        public IColor(float var1): this(var1, var1, var1)
        {
            
        }

        public IColor(double var1, double var3, double var5, double var7):this()
        {

            this.set(var1, var3, var5, var7);
        }

        public IColor(double var1, double var3, double var5): this(var1, var3, var5, 1.0D)
        {
            
        }

        public IColor(double var1, double var3): this(var1, var1, var1, var3)
        {
          
        }

        public IColor(double var1): this(var1, var1, var1)
        {

        }

        public IColor(int var1, int var2, int var3, int var4): this()
        {
            
            this.set((float)var1 / 255.0F, (float)var2 / 255.0F, (float)var3 / 255.0F, (float)var4 / 255.0F);
        }

        public IColor(int var1, int var2, int var3): this(var1, var2, var3, 255)
        {
            
        }

        public IColor(int var1, int var2): this(var1, var1, var1, var2)
        {
            
        }

        public IColor(int var1): this(var1, var1, var1)
        {
            
        }

        public IColor(IColor var1): this(var1.r(), var1.g(), var1.b(), var1.a())
        {
            
        }

        public IColor(IColor var1, float var2): this(var1.r(), var1.g(), var1.b(), var2)
        {
            
        }

        public IColor(IColor var1, double var2): this(var1.r(), var1.g(), var1.b(), (float)var2)
        {
            
        }

        public IColor(IColor var1, int var2): this(var1.r(), var1.g(), var1.b(), (float)var2 / 255.0F)
        {
            
        }

        public IColor(Color var1): this(var1.R, var1.G, var1.B, var1.A)
        {
            
        }

        public IColor(Color var1, float var2): this((float)var1.R / 255.0F, (float)var1.G / 255.0F, (float)var1.B / 255.0F, var2)
        {
            
        }

        public IColor(Color var1, double var2): this(var1, (float)var2)
        {
            
        }

        public IColor(Color var1, int var2): this(var1.R, var1.G, var1.B, var2)
        {
            
        }

        public IColor set(float var1, float var2, float var3, float var4)
        {
            this.rgba[0] = var1;
            this.rgba[1] = var2;
            this.rgba[2] = var3;
            this.rgba[3] = var4;
            return this;
        }

        public IColor set(double var1, double var3, double var5, double var7)
        {
            this.rgba[0] = (float)var1;
            this.rgba[1] = (float)var3;
            this.rgba[2] = (float)var5;
            this.rgba[3] = (float)var7;
            return this;
        }

        public IColor set(int var1, int var2, int var3, int var4)
        {
            this.rgba[0] = (float)var1;
            this.rgba[1] = (float)var2;
            this.rgba[2] = (float)var3;
            this.rgba[3] = (float)var4;
            return this;
        }

        public static IColor hsb(float var0, float var1, float var2, float var3)
        {
            if (var0 < 0.0F)
            {
                var0 += (float)((int)(-var0 + 1.0F));
            }
            else if (var0 > 1.0F)
            {
                var0 -= (float)((int)var0);
            }

            float var4 = var0 * 6.0F - (float)((int)(var0 * 6.0F));
            if (var0 < 0.16666667F)
            {
                return new IColor(var2, var2 * (1.0F - var1 * (1.0F - var4)), var2 * (1.0F - var1), var3);
            }
            else if (var0 < 0.33333334F)
            {
                return new IColor(var2 * (1.0F - var1 * var4), var2, var2 * (1.0F - var1), var3);
            }
            else if (var0 < 0.5F)
            {
                return new IColor(var2 * (1.0F - var1), var2, var2 * (1.0F - var1 * (1.0F - var4)), var3);
            }
            else if (var0 < 0.6666667F)
            {
                return new IColor(var2 * (1.0F - var1), var2 * (1.0F - var1 * var4), var2, var3);
            }
            else
            {
                return var0 < 0.8333333F ? new IColor(var2 * (1.0F - var1 * (1.0F - var4)), var2 * (1.0F - var1), var2, var3) : new IColor(var2, var2 * (1.0F - var1), var2 * (1.0F - var1 * var4), var3);
            }
        }

        public static IColor hsb(float var0, float var1, float var2)
        {
            return hsb(var0, var1, var2, 1.0F);
        }

        public static IColor hsb(double var0, double var2, double var4, double var6)
        {
            return hsb((float)var0, (float)var2, (float)var4, (float)var6);
        }

        public static IColor hsb(double var0, double var2, double var4)
        {
            return hsb((float)var0, (float)var2, (float)var4, 1.0F);
        }

        public float[] Rgba()
        {
            return this.rgba;
        }

        public float[] Rgba(float var1)
        {
            return new float[] { this.rgba[0], this.rgba[1], this.rgba[2], var1 };
        }

        public int argb()
        {
            int var1 = (int)(this.rgba[0] * 255.0F);
            int var2 = (int)(this.rgba[1] * 255.0F);
            int var3 = (int)(this.rgba[2] * 255.0F);
            int var4 = (int)(this.rgba[3] * 255.0F);
            if (var1 < 0)
            {
                var1 = 0;
            }
            else if (var1 > 255)
            {
                var1 = 255;
            }

            if (var2 < 0)
            {
                var2 = 0;
            }
            else if (var2 > 255)
            {
                var2 = 255;
            }

            if (var3 < 0)
            {
                var3 = 0;
            }
            else if (var3 > 255)
            {
                var3 = 255;
            }

            if (var4 < 0)
            {
                var4 = 0;
            }
            else if (var4 > 255)
            {
                var4 = 255;
            }

            return (var4 << 24) + (var1 << 16) + (var2 << 8) + var3;
        }

        public static int argb(IColor var0, float var1)
        {
            int var2 = (int)(var0.r() * 255.0F);
            int var3 = (int)(var0.g() * 255.0F);
            int var4 = (int)(var0.b() * 255.0F);
            int var5 = (int)(var1 * 255.0F);
            if (var5 < 0)
            {
                var5 = 0;
            }
            else if (var5 > 255)
            {
                var5 = 255;
            }

            return (var5 << 24) + (var2 << 16) + (var3 << 8) + var4;
        }

        public static int argb(float[] var0)
        {
            int var1 = (int)(var0[0] * 255.0F);
            int var2 = (int)(var0[1] * 255.0F);
            int var3 = (int)(var0[2] * 255.0F);
            int var4 = (int)(var0[3] * 255.0F);
            if (var1 < 0)
            {
                var1 = 0;
            }
            else if (var1 > 255)
            {
                var1 = 255;
            }

            if (var2 < 0)
            {
                var2 = 0;
            }
            else if (var2 > 255)
            {
                var2 = 255;
            }

            if (var3 < 0)
            {
                var3 = 0;
            }
            else if (var3 > 255)
            {
                var3 = 255;
            }

            if (var4 < 0)
            {
                var4 = 0;
            }
            else if (var4 > 255)
            {
                var4 = 255;
            }

            return (var4 << 24) + (var1 << 16) + (var2 << 8) + var3;
        }

        public static int argb(float var0, float var1, float var2, float var3)
        {
            int var4 = (int)(var0 * 255.0F);
            int var5 = (int)(var1 * 255.0F);
            int var6 = (int)(var2 * 255.0F);
            int var7 = (int)(var3 * 255.0F);
            if (var4 < 0)
            {
                var4 = 0;
            }
            else if (var4 > 255)
            {
                var4 = 255;
            }

            if (var5 < 0)
            {
                var5 = 0;
            }
            else if (var5 > 255)
            {
                var5 = 255;
            }

            if (var6 < 0)
            {
                var6 = 0;
            }
            else if (var6 > 255)
            {
                var6 = 255;
            }

            if (var7 < 0)
            {
                var7 = 0;
            }
            else if (var7 > 255)
            {
                var7 = 255;
            }

            return (var7 << 24) + (var4 << 16) + (var5 << 8) + var6;
        }

        public static int argb(double var0, double var2, double var4, double var6)
        {
            int var8 = (int)(var0 * 255.0D);
            int var9 = (int)(var2 * 255.0D);
            int var10 = (int)(var4 * 255.0D);
            int var11 = (int)(var6 * 255.0D);
            if (var8 < 0)
            {
                var8 = 0;
            }
            else if (var8 > 255)
            {
                var8 = 255;
            }

            if (var9 < 0)
            {
                var9 = 0;
            }
            else if (var9 > 255)
            {
                var9 = 255;
            }

            if (var10 < 0)
            {
                var10 = 0;
            }
            else if (var10 > 255)
            {
                var10 = 255;
            }

            if (var11 < 0)
            {
                var11 = 0;
            }
            else if (var11 > 255)
            {
                var11 = 255;
            }

            return (var11 << 24) + (var8 << 16) + (var9 << 8) + var10;
        }

        public static int argb(int var0, int var1, int var2, int var3)
        {
            if (var0 < 0)
            {
                var0 = 0;
            }
            else if (var0 > 255)
            {
                var0 = 255;
            }

            if (var1 < 0)
            {
                var1 = 0;
            }
            else if (var1 > 255)
            {
                var1 = 255;
            }

            if (var2 < 0)
            {
                var2 = 0;
            }
            else if (var2 > 255)
            {
                var2 = 255;
            }

            if (var3 < 0)
            {
                var3 = 0;
            }
            else if (var3 > 255)
            {
                var3 = 255;
            }

            return (var3 << 24) + (var0 << 16) + (var1 << 8) + var2;
        }

        public int getInt()
        {
            return this.argb();
        }

        public int i()
        {
            return this.argb();
        }

        public float r()
        {
            return this.rgba[0];
        }

        public float g()
        {
            return this.rgba[1];
        }

        public float b()
        {
            return this.rgba[2];
        }

        public float a()
        {
            return this.rgba[3];
        }

        public float red()
        {
            return this.r();
        }

        public float green()
        {
            return this.g();
        }

        public float blue()
        {
            return this.b();
        }

        public float alpha()
        {
            return this.a();
        }

        public float grey()
        {
            return (this.r() + this.g() + this.b()) / 3.0F;
        }

        public float gray()
        {
            return this.grey();
        }

        public IColor r(float var1)
        {
            this.rgba[0] = var1;
            return this;
        }

        public IColor g(float var1)
        {
            this.rgba[1] = var1;
            return this;
        }

        public IColor b(float var1)
        {
            this.rgba[2] = var1;
            return this;
        }

        public IColor a(float var1)
        {
            this.rgba[3] = var1;
            return this;
        }

        public IColor red(float var1)
        {
            return this.r(var1);
        }

        public IColor green(float var1)
        {
            return this.g(var1);
        }

        public IColor blue(float var1)
        {
            return this.b(var1);
        }

        public IColor alpha(float var1)
        {
            return this.a(var1);
        }

        public IColor grey(float var1)
        {
            this.r(var1);
            this.g(var1);
            this.b(var1);
            return this;
        }

        public IColor gray(float var1)
        {
            this.grey(var1);
            return this;
        }

        public int getRed()
        {
            return (int)(this.r() * 255.0F);
        }

        public int getGreen()
        {
            return (int)(this.g() * 255.0F);
        }

        public int getBlue()
        {
            return (int)(this.b() * 255.0F);
        }

        public int getAlpha()
        {
            return (int)(this.a() * 255.0F);
        }

        public int getGrey()
        {
            return (int)((this.r() + this.g() + this.b()) * 255.0F / 3.0F);
        }

        public int getGray()
        {
            return this.getGrey();
        }

        public float[] hsb()
        {
            return RGB2HSB(this.getRed(), this.getGreen(),this.getBlue());

        }
        public float[] RGB2HSB(int red, int green, int blue)
        {

            double hue; double sat; double bri;
            double r = ((double)red / 255.0);
            double g = ((double)green / 255.0);
            double b = ((double)blue / 255.0);

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            hue = 0.0;
            if (max == r && g >= b)
            {
                if (max - min == 0) hue = 0.0;
                else hue = 60 * (g - b) / (max - min);
            }
            else if (max == r && g < b)
            {
                hue = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                hue = 60 * (b - r) / (max - min) + 120;
            }
            else if (max == b)
            {
                hue = 60 * (r - g) / (max - min) + 240;
            }

            sat = (max == 0) ? 0.0 : (1.0 - ((double)min / (double)max));
            bri = max;
            return new float[] { (float)hue, (float)sat, (float)bri };
        }


        public float hue()
        {
            float[] var1 = this.hsb();
            return var1[0];
        }

        public float saturation()
        {
            float[] var1 = this.hsb();
            return var1[1];
        }

        public float brightness()
        {
            float[] var1 = this.hsb();
            return var1[2];
        }

        public Color awt()
        {
            float var1 = this.r();
            float var2 = this.g();
            float var3 = this.b();
            float var4 = this.a();
            if (var1 < 0.0F)
            {
                var1 = 0.0F;
            }
            else if (var1 > 1.0F)
            {
                var1 = 1.0F;
            }

            if (var2 < 0.0F)
            {
                var2 = 0.0F;
            }
            else if (var2 > 1.0F)
            {
                var2 = 1.0F;
            }

            if (var3 < 0.0F)
            {
                var3 = 0.0F;
            }
            else if (var3 > 1.0F)
            {
                var3 = 1.0F;
            }

            if (var4 < 0.0F)
            {
                var4 = 0.0F;
            }
            else if (var4 > 1.0F)
            {
                var4 = 1.0F;
            }

            return Color.FromArgb((byte)var4, (byte)var1, (byte)var2, (byte)var3);
        }

        public String toString()
        {
            return "(r=" + (this.rgba[0]).ToString() + ",g=" + this.rgba[1] + ",b=" + this.rgba[2] + ",a=" + this.rgba[3] + ")";
        }

        public IColor dup()
        {
            return new IColor(this);
        }

        public IColor cp()
        {
            return this.dup();
        }

        public IColor blend(IColor var1, float var2)
        {
            this.rgba[0] = this.rgba[0] * (1.0F - var2) + var1.r() * var2;
            this.rgba[1] = this.rgba[1] * (1.0F - var2) + var1.g() * var2;
            this.rgba[2] = this.rgba[2] * (1.0F - var2) + var1.b() * var2;
            this.rgba[3] = this.rgba[3] * (1.0F - var2) + var1.a() * var2;
            return this;
        }

        public IColor blend(IColor var1)
        {
            return this.blend(var1, 0.5F);
        }

        public bool eq(IColor var1)
        {
            return var1.r() == this.r() && var1.g() == this.g() && var1.b() == this.b() && var1.a() == this.a();
        }

        public bool eq(float var1, float var2, float var3, float var4)
        {
            return this.r() == var1 && this.g() == var2 && this.b() == var3 && this.a() == var4;
        }

        public bool eq(float var1, float var2, float var3)
        {
            return this.r() == var1 && this.g() == var2 && this.b() == var3;
        }
    }
}
