using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_Coord : IComparable<HS_Coord>
    {

        public float xf
        {
            get; set;
        }

        public float yf
        {
            get;set;
        }

        public float zf
        {
            get;set;
        }

        public double xd
        {
            get; set;
        }

        public double yd
        {
            get;set;
        }

        public double zd
        {
            get;set;
        }
        public double wd
        {
            get;
        }
        public double getd(int i);
         
    }
}
