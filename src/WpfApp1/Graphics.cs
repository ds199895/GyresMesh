using _3DTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Xml.Linq;

namespace WpfApp1
{ 
    public class Graphics
    {
        ModelVisual3D visual3d=new ModelVisual3D();
        Model3DGroup group=new Model3DGroup();
        Viewport3D viewport;
        public Graphics(MainWindow mainWindow)
        {

            this.visual3d.Content = group;
            this.viewport = mainWindow.mainViewport;
            this.viewport.Children.Add(visual3d);
            // Make a timer to rotate the cube.
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += mainWindow.Draw;
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            //timer.Start();
        }
        public void DefineLights()
        {
            group.Children.Add(new AmbientLight(Colors.Gray));

            Vector3D direction = new Vector3D(1, -3, -2);
            group.Children.Add(new DirectionalLight(Colors.Gray, direction));

            Vector3D direction2 = new Vector3D(0, 0, -1);
            group.Children.Add(new DirectionalLight(Colors.Gray, direction2));
        }
        public void DrawSystem()
        {
            Point3DCollection point3Ds_X = new Point3DCollection();
            point3Ds_X.Add(new Point3D(0, 0, 0));
            point3Ds_X.Add(new Point3D(20, 0, 0));
            ScreenSpaceLines3D X_axis = new ScreenSpaceLines3D() { Points = point3Ds_X, Thickness =4, Color = Colors.Red };


            Point3DCollection point3Ds_Y = new Point3DCollection();
            point3Ds_Y.Add(new Point3D(0, 0, 0));
            point3Ds_Y.Add(new Point3D(0, 20, 0));
            ScreenSpaceLines3D Y_axis = new ScreenSpaceLines3D() { Points = point3Ds_Y, Thickness =4, Color = Colors.Green };


            Point3DCollection point3Ds_Z = new Point3DCollection();
            point3Ds_Z.Add(new Point3D(0, 0, 0));
            point3Ds_Z.Add(new Point3D(0, 0, 20));
            ScreenSpaceLines3D Z_axis = new ScreenSpaceLines3D() { Points = point3Ds_Z, Thickness = 4, Color = Colors.Blue };

            this.viewport.Children.Add(X_axis);
            this.viewport.Children.Add(Y_axis);
            this.viewport.Children.Add(Z_axis);
        }

        public void Draw(GeometryModel3D geo)
        {
            group.Children.Add(geo);
        }

        public void Draw(List<GeometryModel3D> geos)
        {
            foreach(GeometryModel3D g in geos){
                group.Children.Add(g);
            }
            
        }
    }
}
