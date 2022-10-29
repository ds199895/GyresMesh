using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public abstract class GEC_Creator
    {
        private List<GE_Face> _faces;

        public abstract List<GE_Face> GetFaces();

        /**
         * Creates the base.
         *
         * @return HE_Mesh
         */
        protected abstract GE_Mesh createBase();
    }
}
