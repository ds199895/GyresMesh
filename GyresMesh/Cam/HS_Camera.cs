using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;

namespace Hsy.Cam
{
    
    public class HS_Camera
    {
        private PerspectiveCamera camPerspective = new PerspectiveCamera();
        private OrthographicCamera camOrtho = new OrthographicCamera();
        public HS_Vector x;
        public HS_Vector y;
        public HS_Vector z;
        public Point3D cmPosition { get; set; } = new Point3D(4, 5, 0.5);
        public double cmTheta = Math.PI * 1.3;
        public bool perspective = true;
        //public ProjectionCamera cam;
        private PerspectiveCamera cam = new PerspectiveCamera();
        public HS_Camera(HS_Vector pos,HS_Vector lookAt)
        {
            this.cam.Position = pos.ToPoint3D();
            this.cam.LookDirection = lookAt.ToVector3D();

            this.cam.Position = cmPosition;
            this.cam.LookDirection = AngleToVector(cmTheta, 1);
            //this.cam.LookDirection = new Vector3D(cmPosition.X, 0, cmPosition.Z);

            this.cam.UpDirection = new Vector3D(0, 0, 1);
            HS_Vector RightDirection = HS_Vector.ToHS_Vector3D(cam.LookDirection).cross(HS_Vector.ToHS_Vector3D(cam.UpDirection)).unit();

            this.x = RightDirection;
            this.y = HS_Vector.ToHS_Vector3D(cam.UpDirection);
            this.z =HS_Vector.ToHS_Vector3D(cam.LookDirection);
        }
        //public void applyCamera()
        //{
        //    if (this.perspective)
        //    {
        //        this.cam = new PerspectiveCamera();
               
        //    }
        //    else
        //    {
        //        this.cam = new OrthographicCamera();

        //    }
        //}

        // 将角度转为向量
        protected Vector3D AngleToVector(double angle, double length)
        {
            return new Vector3D(
                length * Math.Cos(angle), length * Math.Sin(angle), 0);
        }


        public ProjectionCamera GetCamera()
        {
            return this.cam;
        }


        public void ResetCoordinateSystem()
        {
            HS_Vector RightDirection = HS_Vector.ToHS_Vector3D(cam.LookDirection).cross(HS_Vector.ToHS_Vector3D(cam.UpDirection)).unit();
            this.y = HS_Vector.ToHS_Vector3D(cam.UpDirection);
            this.z = HS_Vector.ToHS_Vector3D(cam.LookDirection);
            this.x = RightDirection;
        }

        override
        public string ToString()
        {
            string s = "HS_Camera: " + "  pos:  " + cam.Position.ToString() + "  lookDirection: " + cam.LookDirection.ToString()+" xAxis: "+this.x.ToString()+" yAxis: "+this.y.ToString() + " zAxis: " + this.z.ToString();
            return s;
        }


    }
}
