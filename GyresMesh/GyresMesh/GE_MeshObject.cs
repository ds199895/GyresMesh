using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public abstract class GE_MeshObject : GE_Object
    {
        private bool used;
        private bool visible;
        private int color;

        public GE_MeshObject()
        { 
            used = false;
            visible = false;
            color = -1;
        }

        public void ResetUsed()
        {
            used = false;
        }

        public void InitialUsed()
        {
            used = true;
        }

        public void InitialUsed(bool b)
        {
            used = b;
        }

        public bool isUsed()
        {
            return used;
        }

        public void ResetVisible()
        {
            visible = false;
        }

        public void InitialVisible()
        {
            visible = true;
        }

        public void InitialVisible(bool b)
        {
            visible = b;
        }

        public bool isVisible()
        {
            return visible;
        }

        override
        public bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }
            if (other == this)
            {
                return true;
            }
            if (!(other.GetType() == this.GetType()))
            {
                return false;
            }
            return ((GE_MeshObject)other).GetKey() == key;
        }

        public void Clone(GE_MeshObject ob)
        {
            base.Clone(ob);
            this.used = ob.used;
            this.visible = ob.visible;
            this.color = ob.color;
        }

        public int getColor()
        {
            return color;
        }

        public void setColor(int color)
        {
            this.color = color;
        }
    }
}
