using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GEC_FromPolygons : GEC_Creator
    {
        private List<GE_Face> _faces;
        public override List<GE_Face> GetFaces()
        {
            return _faces;
        }

        protected override GE_Mesh createBase()
        {
            throw new NotImplementedException();
        }
    }
}
