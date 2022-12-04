using Hsy.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public abstract class GE_Object :IObject
    {
        private static long currentkey = 0;
        private protected long key;
        private protected int _internalLabel;
        private protected int _userLabelInt;
        private double _userLabelDouble;
        //public  int internalLabel { get{ return this._internalLabel; }private set { this._internalLabel = value; }}
        //private protected int userLabelInt { get { return this._userLabelInt; } set { this._userLabelInt= value; } }
        //private double userLabelDouble { get { return this._userLabelDouble; } set { this._userLabelDouble = value; } }
        
        public GE_Object()
        {
            key = currentkey++;
            _internalLabel = -1;
            _userLabelInt = -1;
            _userLabelDouble = -1;
        }

        public long GetKey()
        {
            return key;
        }


        protected void GetInternalLabel(int label)
        {
            _internalLabel = label;
        }
        protected internal void SetInternalLabel(int label)
        {
            _internalLabel = label;
        }


        public int GetInternalLabel()
        {
            return _internalLabel;
        }



        public int GetUserLabelInt()
        {
            return _userLabelInt;
        }
        public void SetUserLabel(int label)
        {
            _userLabelInt = label;
        }



        public double GetUserLabelDouble()
        {
            return _userLabelDouble;
        }
        public void SetUserLabel(double label)
        {
            _userLabelDouble = label;
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
            _internalLabel = ob.GetInternalLabel();
            _userLabelInt = ob.GetUserLabelInt();
            _userLabelDouble = ob.GetUserLabelDouble();
        }

        protected internal abstract void Clear();

        protected internal abstract void ClearPreComputed();
    }
}
