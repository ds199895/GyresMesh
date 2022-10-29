using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public abstract class GE_Object
    {
        private static long currentkey = 0;
        private protected long key;
        private protected int internalLabel;
        private protected int userLabelInt;
        private double userLabelDouble;

        public GE_Object()
        {
            key = currentkey++;
            internalLabel = -1;
            userLabelInt = -1;
            userLabelDouble = -1;
        }

        public long GetKey()
        {
            return key;
        }

        protected void GetInternalLabel(int label)
        {
            internalLabel = label;
        }

        public int GetInternalLabel()
        {
            return internalLabel;
        }

        public void GetUserLabel(int label)
        {
            userLabelInt = label;
        }

        public void GetUserLabel(double label)
        {
            userLabelDouble = label;
        }

        public int GetUserLabelInt()
        {
            return userLabelInt;
        }

        public double GetUserLabelDouble()
        {
            return userLabelDouble;
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
            if (!(other.GetType()==this.GetType())) {
                return false;
            }
            return ((GE_Object)other).GetKey() == key;
        }

        override
        public int GetHashCode()
        {
            return (int)(key^key>>32);
        }

        public void Clone(GE_Object ob)
        {
            internalLabel = ob.GetInternalLabel();
            userLabelInt = ob.GetUserLabelInt();
            userLabelDouble = ob.GetUserLabelDouble();
        }

        protected abstract void Clear();

        protected abstract void ClearPreComputed();
    }
}
