using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryFactory:HS_GeometryFactory3D
    {
        public HS_GeometryFactory()
        {

        }

        public static HS_GeometryFactory instance()
        {
            return new HS_GeometryFactory();
        }


    }
}
