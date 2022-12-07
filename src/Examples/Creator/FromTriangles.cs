using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.Render;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class FromTriangles:IApp
    {
        static void Main(string[] args)
        {
            IApp.main();
        }
        CamController cam;
        GE_Render render;
        GE_Mesh mesh;
        double starttime;
        double lasttime;
        int MaxVertexCount = 10000;
        public override void SetUp()
        {
            Size(800, 600);
            cam = new CamController(this);
            render = new GE_Render(this);

            int count =4;
            HS_Point[] points = new HS_Point[count * count];
            int index = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            starttime= stopwatch.ElapsedMilliseconds; 
            lasttime = stopwatch.ElapsedMilliseconds;
            stopwatch.Start();
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    //                points[index] = new HS_Point(i * 40, j * 40, sin(TWO_PI / 20 * i) * 40 + cos(TWO_PI / 10 * j) * 40);
                    points[index] = new HS_Point(-200 + i * 40 + (((i != 0) && (i != 10)) ? random(-20, 20) : 0), -200 + j * 40 + (((j != 0) && (j != 10)) ? random(-20, 20) : 0), Sin(TWO_PI / 20 * i) * 100 + Cos(TWO_PI / 10 * j) * 100);
                    index++;
                }
            }

            Console.WriteLine("创建points总用时   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
            lasttime = stopwatch.ElapsedMilliseconds;
            //create triangles from point grid
            HS_Triangle[] tris = new HS_Triangle[2 * (count - 1) * (count - 1)];

            for (int i = 0; i < (count - 1); i++)
            {
                for (int j = 0; j < (count - 1); j++)
                {
                    tris[2 * (i + (count - 1) * j)] = new HS_Triangle(points[i + count * j], points[i + 1 + count * j], points[i + count * j + count]);
                    tris[2 * (i + (count - 1) * j) + 1] = new HS_Triangle(points[i + 1 + count * j], points[i + count * j + count + 1], points[i + count * j + count]);
                }
            }
            Console.WriteLine("创建tri总用时   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
            lasttime = stopwatch.ElapsedMilliseconds;
            GEC_FromTriangles creator = new GEC_FromTriangles();

            creator.setTriangles(tris);
            //alternatively tris can be any Collection<HS_Triangle>
            mesh = new GE_Mesh(creator);
            Console.WriteLine("create mesh in:   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
            lasttime = stopwatch.ElapsedMilliseconds;
            Print(mesh);
            //foreach (GE_Vertex v in mesh.GetVertices())
            //{
            //    Print(v);
            //}
            //foreach (GE_Halfedge he in mesh.GetHalfedges())
            //{
            //    Print(he);
            //}
           
            aabb = mesh.getAABB();
            Console.WriteLine("获取aabb总用时   " + (stopwatch.ElapsedMilliseconds -lasttime) + "ms");
        }

        public override void Draw()
        {
            Background(255);
            cam.DrawSystem(this, 200);

            render.displayHeMeshWithDegree(mesh, cam.CurrentView,detail);
        }
        HS_AABB aabb;
        
        HS_Point cen;
        Vector3 normal;
        double targetLength;
        double dis = 0;
        bool detail = false;
        public override void KeyReleased()
        {
            if (key == "F")
            {
                cam.Focus(aabb.getLimits(), false);
            }
            if (key == "Q")
            {
                double length = targetLength;
                Print("Length: "+targetLength);
                
                double times = (cam.CurrentView.Position - cam.CurrentView.target).X / normal.X;
                //Print("height times pro: "+(double)this.window.Height /times);
                Print("width: " +this.window.Width+" height: "+this.window.Height);
                Print("width times pro: " + (double)this.window.Width / times);
                Print("aspectRatio: "+(double)this.window.Width / (double)this.window.Height);
                Print("length pro: " + targetLength / times);

                Print("times: " + times);
                Print("target: " + cam.CurrentView.target);
            }else if (key == "D")
            {
                detail = !detail;
            }
            else if (key == "P")
            {
                Print("SplitPreMesh=............" + mesh.GetNumberOfFaces());
                SplitAllLongEdges(20);
                Print("SplitAfterMesh=............" + mesh.GetNumberOfFaces());
            }
            else if (key == "R")
            {
                Print("FlipPreMesh=............" + mesh.GetNumberOfFaces());
                flipEdgeControl();
                Print("FlipAfterMesh=............" + mesh.GetNumberOfFaces());
            }

        }

        public void SplitAllLongEdges(double target)
        {
            bool done = false;
            while (!done)
            {
                done = true;
                foreach (GE_Halfedge he in mesh.GetHalfedges())
                {
                    if (mesh.GetNumberOfVertices() < MaxVertexCount && he.GetLength() > 4 / 3f* target)
                    {
                        if (!he.IsOuterBoundary() && !he.IsInnerBoundary())
                        {      //判断半边是否是边界
                            if (he.GetLength() > he.GetNextInFace().GetLength()
                                    && he.GetLength() > he.GetPrevInFace().GetLength()
                                    && he.Pair().GetLength() > he.Pair().GetNextInFace().GetLength()
                                    && he.Pair().GetLength() > he.Pair().GetPrevInFace().GetLength())
                            {

                                GE_Face f1 = he.GetFace();             //f1：半边的相邻面
                                GE_Face f2 = he.Pair().GetFace();             //f2：相邻半边的相邻面

                                GE_Vertex v1_opp = he.GetPrevInFace().GetStart();              // v1_opp：半边相邻三角面分裂边的另一个顶点
                                GE_Vertex v2_opp = he.Pair().GetPrevInFace().GetStart();      // v2_opp：半边相邻三角面分裂边的另一个顶点

                                //                        println("mesh vertices size: "+mesh.getVertices().size()+", vertices size: "+vertices.size());
                                //int index1 = vertices.indexOf(he.GetStart());
                                //int index2 = vertices.indexOf(he.GetEnd());
                                ////                        System.out.println("index1="+index1+".............index2="+index2);
                                //double newVgrowthIntensity = (GrowthIntensity.get(index1) + GrowthIntensity.get(index2)) / 2;
                                ////                        println(newVgrowthIntensity);

                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

                                //vertices.add(v1);
                                //GrowthIntensity.add(newVgrowthIntensity);

                                GE_MeshOp.splitFace(mesh, f1, v1, v1_opp);
                                GE_MeshOp.splitFace(mesh, f2, v1, v2_opp);
                            }

                        }
                        else if (he.IsInnerBoundary())
                        {
                            if (he.GetLength() > he.GetNextInFace().GetLength() && he.GetLength() > he.GetPrevInFace().GetLength())
                            {
                                GE_Face f1 = he.GetFace();             //f1：半边的相邻面


                                GE_Vertex v1_opp = he.GetPrevInFace().GetStart();              // v1_opp：半边相邻三角面分裂边的另一个顶点
                                                                                                //                        println("mesh vertices size: "+mesh.getVertices().size()+", vertices size: "+vertices.size());
                                //int index1 = vertices.indexOf(he.GetStart());
                                //int index2 = vertices.indexOf(he.GetEnd());
                                ////                        System.out.println("index1="+index1+".............index2="+index2);
                                //double newVgrowthIntensity = (GrowthIntensity.get(index1) + GrowthIntensity.get(index2)) / 2;
                                ////                        println("new vertex growthIntensity: "+newVgrowthIntensity);
                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

                                //vertices.add(v1);
                                //GrowthIntensity.add(newVgrowthIntensity);

                                GE_MeshOp.splitFace(mesh, f1, v1, v1_opp);
                            }

                        }
                        else if (he.IsOuterBoundary())
                        {
                            continue;
                        }

                    }
                }
                foreach (GE_Halfedge he in mesh.GetHalfedges())
                {
                    if (he.GetLength() > 4 / 3f* target)
                    {
                        done = false;
                    }
                }
            }
        }

        public void flipEdgeControl()
        {
            foreach (GE_Halfedge he in mesh.GetHalfedges())
            {
                double angle1 = getHalfEdgeAngle(he);    //半边对应角度
                double angle2 = getHalfEdgeAngle(he.Pair());     //半边对边对应角度
                double angle3 = getHalfEdgeAngle(he.Pair().GetNextInFace());
                double angle4 = getHalfEdgeAngle(he.Pair().GetNextInFace().GetNextInFace());
                if (!he.IsOuterBoundary() && !he.IsInnerBoundary())
                {      //判断半边是否是边界
                    if (angle1 > 0.5 * PI && angle2 > 0.5 * PI)
                    {
                        //FlipEdge(mesh, he);
                        GE_MeshOp.flipEdge(mesh, he);
                    }
                    else if (angle1 > 0.6 * PI && angle2 < 0.5 * PI && angle3 < 0.5 * PI && angle4 < 0.5 * PI)
                    {
                        GE_MeshOp.flipEdge(mesh, he);
                    }
                }
            }
        }

        public double getHalfEdgeAngle(GE_Halfedge _he)
        {
            HS_Vector v1 = _he.GetNextInFace().GetStartPosition();  //下一条半边顶点
            HS_Vector v2 = _he.GetNextInFace().GetEndPosition();  //下一条半边终点

            HS_Vector v3 = _he.GetNextInFace().GetNextInFace().GetStartPosition();  //下下一条半边顶点
            HS_Vector v4 = _he.GetNextInFace().GetNextInFace().GetEndPosition();  //下下一条半边终点

            HS_Vector vP = HS_Vector.sub(v1, v2);
            HS_Vector vQ = HS_Vector.sub(v4, v3);
            double angle = HS_Vector.angle(vP, vQ);    //半边对应角度

            return angle;
        }


    }
}
