using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_Coord: IComparable<HS_Coord>
    {
        float X();

        float Y();

        float Z();

    }
}
