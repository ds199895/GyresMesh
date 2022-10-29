using _3DTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        // 相机
        private PerspectiveCamera TheCamera = null;

        // 相机遥控器
        private CameraController cam = null;
        public GeometryModel3D groundMesh;
        public List<GeometryModel3D> cubeMeshes;
        public int row = 3;
        public int column = 3;
        public double width = 0.1D;
        Graphics g;
        public void SetUp(object sender, EventArgs e)
        {
            Size(1200, 1000);

            cam = new CameraController(this, 20);
            g = new Graphics(this);
            groundMesh=createGround();

            cubeMeshes = CreateCubes();
            g.DrawSystem();
            g.DefineLights();
            //g.Draw(groundMesh);
            g.Draw(cubeMeshes);
            //cam = new CameraController(this, mainViewport,20);
            //DefineLights(group3d);
            //DefineModel(group3d);
        }


        // Rotate the cube.
        public void Draw(object sender, EventArgs e)
        {
            //width += 0.1;


        }

        public void Size(double width,double height)
        {
            this.Width = width;
            this.Height = height;
            this.mainViewport.Width = this.Width;
            this.mainViewport.Height = this.Height;
        }

        // 定义光线
        public GeometryModel3D createGround()
        {
            // Make the ground.
            MeshGeometry3D groundMesh = new MeshGeometry3D();
            const double wid = 10;
            groundMesh.Positions.Add(new Point3D(-wid, -wid, 0));
            groundMesh.Positions.Add(new Point3D(-wid, +wid, 0));
            groundMesh.Positions.Add(new Point3D(+wid, +wid, 0));
            groundMesh.Positions.Add(new Point3D(+wid, -wid, 0));
            groundMesh.TriangleIndices.Add(0);
            groundMesh.TriangleIndices.Add(2);
            groundMesh.TriangleIndices.Add(3);
            groundMesh.TriangleIndices.Add(0);
            groundMesh.TriangleIndices.Add(1);
            groundMesh.TriangleIndices.Add(2);
            DiffuseMaterial groundMaterial = new DiffuseMaterial(Brushes.DarkGray);
            GeometryModel3D groundModel = new GeometryModel3D(groundMesh, groundMaterial);
            return groundModel;
            //group.Children.Add(groundModel);
        }


        // 定义模型
        private List<GeometryModel3D> CreateCubes()
        {
            List<GeometryModel3D> cubes =new List<GeometryModel3D>();
             // Make some cubes.
            for (int x = -(row+1)/2; x <=(row+1)/2; x += 2)
            {
                for (int y = -(column + 1) / 2; y <= (column + 1) / 2; y += 2)
                {
                    MeshGeometry3D mesh = MakeCubeMesh(x, y,0.5, 1);

                    byte r = (byte)(128 + x * 50);
                    byte g = (byte)(128 + y * 50);
                    byte b = (byte)(128 + x * 50);
                    Color color = Color.FromArgb(255, r, g, b);
                    DiffuseMaterial material = new DiffuseMaterial(
                        new SolidColorBrush(color));

                    GeometryModel3D model = new GeometryModel3D(mesh, material);
                    cubes.Add(model);
                    //group.Children.Add(model);
                }
            }
            return cubes;
        }

        // 根据中心位置生成立方体
        private MeshGeometry3D MakeCubeMesh(double x, double y, double z, double w)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            // Define the positions.
            w /= 2;
            Point3D[] points =
            {
                new Point3D(x - w, y - w, z - w),
                new Point3D(x + w, y - w, z - w),
                new Point3D(x + w, y - w, z + w),
                new Point3D(x - w, y - w, z + w),
                new Point3D(x - w, y - w, z + w),
                new Point3D(x + w, y - w, z + w),
                new Point3D(x + w, y + w, z + w),
                new Point3D(x - w, y + w, z + w),
                new Point3D(x + w, y - w, z + w),
                new Point3D(x + w, y - w, z - w),
                new Point3D(x + w, y + w, z - w),
                new Point3D(x + w, y + w, z + w),
                new Point3D(x + w, y + w, z + w),
                new Point3D(x + w, y + w, z - w),
                new Point3D(x - w, y + w, z - w),
                new Point3D(x - w, y + w, z + w),
                new Point3D(x - w, y - w, z + w),
                new Point3D(x - w, y + w, z + w),
                new Point3D(x - w, y + w, z - w),
                new Point3D(x - w, y - w, z - w),
                new Point3D(x - w, y - w, z - w),
                new Point3D(x - w, y + w, z - w),
                new Point3D(x + w, y + w, z - w),
                new Point3D(x + w, y - w, z - w),
            };
            foreach (Point3D point in points) mesh.Positions.Add(point);

            // Define the triangles.
            Tuple<int, int, int>[] triangles = new Tuple<int, int, int>[12];
            for (int i = 0; i < triangles.Length; i++)
            {
                int tmp = i % 2 == 0 ? 2 : -2;
                triangles[i] = new Tuple<int, int, int>(
                    2 * i, 2 * i + 1, 2 * i + tmp);
            }
            foreach (Tuple<int, int, int> tuple in triangles)
            {
                mesh.TriangleIndices.Add(tuple.Item1);
                mesh.TriangleIndices.Add(tuple.Item2);
                mesh.TriangleIndices.Add(tuple.Item3);
            }

            return mesh;
        }
    }
}
