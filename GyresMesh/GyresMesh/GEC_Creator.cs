using Flowing;
using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hsy.Core;
using static Hsy.Core.HS_ProgressReporter;

namespace Hsy.GyresMesh
{
    public abstract class GEC_Creator
    {
        //public static HS_ProgressReporter reporter=new HS_ProgressReporter("E://log.txt");
        //public static HS_ProgressReporter.HS_ProgressTracker tracker = reporter.tracker;
        public static HS_ProgressReporter.HS_ProgressTracker tracker = HS_ProgressTracker.instance();

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
            tracker.setStartStatus(this, "Creating base mesh.");

            GE_Mesh based = this.createBase();
            

            based.attribute = new IO.IAttribute();
            based.attribute.name = "Mesh";
            tracker.setStopStatus(this, "Base mesh created.");
            return based;
        }
    }
}
