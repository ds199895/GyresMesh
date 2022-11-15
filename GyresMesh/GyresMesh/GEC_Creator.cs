using Flowing;
using Hsy.Geo;
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
        public IApp home;
        /** Center. */
        protected HS_Point center;
        /** Rotation angle about Z-axis. */
        protected double zangle;
        /** Z-axis. */
        protected HS_Vector zaxis;
        /** Override. */
        protected bool Override;
	    protected bool override2D;
        /** Use applet model coordinates. */
        protected bool toModelview;
        /** Base Z-axis. */
        protected HS_Vector Z;
        protected bool manifoldCheck;
        protected double scale;


        public abstract List<GE_Face> GetFaces();

        /**
         * Creates the base.
         *
         * @return HE_Mesh
         */
        protected abstract GE_Mesh createBase();

        //public GE_Mesh create()
        //{

        //}
    }
}
