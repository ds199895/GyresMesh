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

        public GEC_Creator()
        {
            center = new HS_Point();
            zaxis = new HS_Vector(HS_Vector.zaxis);
            Z = new HS_Vector(HS_Vector.zaxis);
            scale = 1.0;
            toModelview = false;
        }
        /**
         * Creates the base.
         *
         * @return HE_Mesh
         */
        protected internal abstract GE_Mesh createBase();

        public GE_Mesh create()
        {
            GE_Mesh based = this.createBase();
            return based;
        }
    }
}
