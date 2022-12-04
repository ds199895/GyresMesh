using Hsy.Geo;
using System.Windows.Media.Media3D;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Hsy.Cam;
namespace WpfApp1
{
    public class CameraController
    {
       
        public HS_Camera currentView = null;
        private ProjectionCamera camera = null;
        private Camera camPerspective = null;
        private Camera camTop = null;
        private Camera camFront = null;
        private Camera camBack = null;
        private Camera camLeft = null;
        private Camera camRight = null;
        private Camera camISO = null;
        private UIElement mainWindow = null;
        private Viewport3D viewPort = null;
        Point mouseLastPosition;
        double mouseDeltaFactor = 2;// determine the angle delta when the mouse drag the 3D view
        double keyDeltaFactor = 4;// determine the angle delta when the ddirection key pressed
        double zoomDeltaFactor = 5;
        public const double panDeltaFactor = 0.0001D;
       
        HS_Vector x;
        HS_Vector y;
        HS_Vector z;
        bool perspective;
        public Point3D cmPosition { get; set; } = new Point3D(4, 0.5, 5);
        public double cmTheta = Math.PI * 1.3;
        public double dist=0;

        public CameraController(MainWindow mainWindow, double dist)
        {

            this.mainWindow = mainWindow;
            this.viewPort = mainWindow.mainViewport;
            this.dist = dist;
            InitCamera(dist);
            this.mainWindow.MouseDown += Viewport3D_MouseDown;

            this.mainWindow.MouseMove += Viewport3D_MouseMove;
            this.mainWindow.MouseWheel += Viewport3D_MouseWheel;
            this.mainWindow.KeyDown += mainWindow_KeyDown;
            //this.viewPort.MouseDown += Viewport3D_MouseDown;

            //this.viewPort.MouseMove += Viewport3D_MouseMove;
            //this.viewPort.MouseWheel += Viewport3D_MouseWheel;
            //this.viewPort.KeyDown += mainWindow_KeyDown;
        }
        protected virtual void InitCamera(double dist)
        {
            currentView = new HS_Camera(new HS_Vector(0, 0, dist), new HS_Vector(0,0,-1));
            camera = currentView.GetCamera();

            this.viewPort.Camera = camera;
            Console.WriteLine(currentView.ToString());
            //iniCoordinateSystem(ToHS_Vector3D(camera.Position), ToHS_Vector3D(camera.LookDirection));
        }

       public void ResetCamera(double dist)
        {


        }

        private HS_Vector ToHS_Vector3D(Vector3D v)
        {
            return new HS_Vector(v.X, v.Y, v.Z);
        }
        private HS_Vector ToHS_Vector3D(Point3D p)
        {
            return new HS_Vector(p.X, p.Y, p.Z);
        }
        private Vector3D ToVector3D(HS_Vector v)
        {
            return new Vector3D(v.X(), v.Y(), v.Z());
        }

        public void Top()
        {

        }

        public void Perspective()
        {

            this.perspective = true;
        }

        public void SetCurrentViewToOrtho()
        {

        }


        // 将角度转为向量
        protected Vector3D AngleToVector(double angle, double length)
        {
            return new Vector3D(
                length * Math.Cos(angle), 0, length * Math.Sin(angle));
        }
        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point newMousePosition = e.GetPosition(mainWindow);

                if (mouseLastPosition.X != newMousePosition.X)
                {
                    HorizontalTransform(mouseLastPosition.X < newMousePosition.X, mouseDeltaFactor);
                }

                if (mouseLastPosition.Y != newMousePosition.Y)// change position in the horizontal direction
                {

                    VerticalTransform(mouseLastPosition.Y > newMousePosition.Y, mouseDeltaFactor);
                }
                mouseLastPosition = newMousePosition;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Point newMousePosition = e.GetPosition(mainWindow);


                if (mouseLastPosition != newMousePosition)
                {
                    Pan(e.GetPosition(mainWindow).X, e.GetPosition(mainWindow).Y, !currentView.perspective);
                }
                mouseLastPosition = newMousePosition;
                //Console.WriteLine("CurrentView: " + currentView.ToString());
            }

        }

        private void Pan(double x, double y,bool onXZ)
        {
            currentView.ResetCoordinateSystem();
            if (!onXZ)
            { 
                double DistToOrign = ToHS_Vector3D(camera.Position).len();
                double dx = x - mouseLastPosition.X;
                double dy = y - mouseLastPosition.Y;
                dx *= -panDeltaFactor * DistToOrign;
                dy *= panDeltaFactor * DistToOrign;

                HS_Vector movex = currentView.x.dup().mul(dx);
                HS_Vector movey = currentView.y.dup().mul(dy);

                HS_Vector move = movex.add(movey);
                camera.Position = new Point3D(camera.Position.X + move.x, camera.Position.Y + move.y, camera.Position.Z + move.z);
                camera.LookDirection = new Vector3D(camera.LookDirection.X + move.x, camera.LookDirection.Y + move.y, camera.LookDirection.Z + move.z);
                
            }
            else
            {
                Console.WriteLine("is on 2D!");
                double DistToOrign = ToHS_Vector3D(camera.Position).len();
                double dx = x - mouseLastPosition.X;
                double dy = y - mouseLastPosition.Y;
                dx *= -panDeltaFactor * DistToOrign;
                dy *= panDeltaFactor * DistToOrign;
                camera.Position = new Point3D(camera.Position.X + dx, camera.Position.Y + dy, camera.Position.Z);
            }

        }
        private void Viewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseLastPosition = e.GetPosition(mainWindow);
        }

        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z);
            Vector3D rotateAxis = Vector3D.CrossProduct(postion, camera.UpDirection);
           
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (upDown ? -1 : 1));

            RotateTransform3D rt3d = new RotateTransform3D(rotate);
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(camera.Position);
            camera.Position = newPostition;
            camera.LookDirection = matrix.Transform(camera.LookDirection);

            //update the up direction
            Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);
            newUpDirection.Normalize();
            camera.UpDirection = newUpDirection;
        }

        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D postion = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z);
            Vector3D rotateAxis = camera.UpDirection;
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (leftRight ? -1 : 1));
            RotateTransform3D rt3d = new RotateTransform3D(rotate);
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(camera.Position);
            camera.Position = newPostition;

            camera.LookDirection = matrix.Transform(camera.LookDirection);
            
        }

        private void Viewport3D_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            //120 near ,   -120 far
            //System.Diagnostics.Debug.WriteLine(e.Delta.ToString());
            //Console.WriteLine(camera.Position.ToString());
            Point3D currentPosition = camera.Position;
            Vector3D lookDirection = camera.LookDirection;//new Vector3D(camera.LookDirection.X, camera.LookDirection.Y, camera.LookDirection.Z);

            lookDirection.Normalize();

            lookDirection *= zoomDeltaFactor;
            
            if (e.Delta == 120)//getting near
            {
                if ((currentPosition.X + lookDirection.X) * currentPosition.X > 0)
                {
                    currentPosition += lookDirection;
                }
            }
            if (e.Delta == -120)//getting far
            {
                currentPosition -= lookDirection;
            }

            Point3DAnimation positionAnimation = new Point3DAnimation();
            positionAnimation.BeginTime = new TimeSpan(0, 0, 0);
            positionAnimation.Duration = TimeSpan.FromMilliseconds(100);
            
            positionAnimation.To = currentPosition;
            positionAnimation.From = camera.Position;
            positionAnimation.Completed += new EventHandler(positionAnimation_Completed);
            camera.BeginAnimation(PerspectiveCamera.PositionProperty, positionAnimation, HandoffBehavior.Compose);
        }

        void positionAnimation_Completed(object sender, EventArgs e)
        {
            //Set a Property After Animating It with a Storyboard:http://msdn.microsoft.com/en-us/library/aa970493.aspx
            Point3D position = camera.Position;
            camera.BeginAnimation(PerspectiveCamera.PositionProperty, null);
            camera.Position = position;
        }

        // 其中 上、下、Q、E代表平移
        // 左右代表旋转
        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: MoveUD(true); break;
                case Key.Down: MoveUD(false); break;
                case Key.Left: MoveLR(true); break;
                case Key.Right: MoveLR(false); break;
            }
            // 更新相机位置
            InitCamera(dist);
        }
        protected void MoveLR(bool isLeft = true)
        {
            Vector3D v = AngleToVector(cmTheta, panDeltaFactor);
            if (isLeft)
                cmPosition += new Vector3D(v.Z, 0, -v.X);
            else
                cmPosition += new Vector3D(-v.Z, 0, v.X);
        }

        //向上或者向下移动
        protected void MoveUD(bool isUp = true)
        {
            Vector3D v = AngleToVector(cmTheta, panDeltaFactor);
            if (isUp)
                cmPosition += v;
            else
                cmPosition -= v;
        }




    }

}
