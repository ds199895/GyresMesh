using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_Map
    {
        public void mapPoint3D(HS_Coord p, HS_MutableCoord result);

        public void mapPoint3D(double x, double y, double z, HS_MutableCoord result);

        public void unmapPoint3D(HS_Coord p,  HS_MutableCoord result);

        public void unmapPoint3D(double x, double y, double z,  HS_MutableCoord result);

        public void mapVector3D(HS_Coord p,  HS_MutableCoord result);

        public void mapVector3D(double x, double y, double z, HS_MutableCoord result);

        public void unmapVector3D(HS_Coord p,  HS_MutableCoord result);

        public void unmapVector3D(double x, double y, double z,  HS_MutableCoord result);

        public HS_Coord mapPoint3D(HS_Coord p);

        public HS_Coord mapPoint3D(double x, double y, double z);

        public HS_Coord unmapPoint3D(HS_Coord p);

        public HS_Coord unmapPoint3D(double x, double y, double z);

        public HS_Coord mapVector3D(HS_Coord p);

        public HS_Coord mapVector3D(double x, double y, double z);

        public HS_Coord unmapVector3D(HS_Coord p);

        public HS_Coord unmapVector3D(double x, double y, double z);

    }
}
