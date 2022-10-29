using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_MutableCoord :HS_Coord
    {

        void SetX(float x);

        void SetY(float y);

        void SetZ(float z);

        void Set(HS_Coord z);

    }
}
