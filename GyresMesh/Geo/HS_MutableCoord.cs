using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_MutableCoord :HS_Coord
    {

        public void SetX(float x);

        void SetY(float y);

        void SetZ(float z);
        void SetX(double x);
        void SetY(double y);
        void SetZ(double z);
        void Set(HS_Coord z);

    }
}
