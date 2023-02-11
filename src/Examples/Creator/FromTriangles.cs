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

namespace Examples.Creator
{
    class FromTriangles : IApp
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
        double CollisionDistance = 100;     //最小碰撞距离
        double MaxSplitDistance = 90;    //分裂距离，线段长度大于分裂距离时会分裂出新节点
        double MinSplitDistance = 20;
        double FinalLength = 100;

        double CollisionWeight = 20000;     //碰撞因子权重
        double EdgeLengthConstraintWeight = 1000;      //线段收缩力权重
        double BendingResistanceWeight = 300000;    //抗弯力因子权重
        double ControlEdgeAngleWeight = 600000;  //边缘角度控制权重

        int count = 0;
        List<HS_Vector> totalWeightedMoves;             //总移动向量
        List<Double> totalWeights;        //碰撞次数
        List<Double> GrowthIntensity;        //生长因子
        List<GE_Vertex> vertices;        //生长因子
        HS_KDTreeInteger<HS_Point> tree;     //KD树形结构
        HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point>[] inRange;
        public override void SetUp()
        {
            Size(800, 600);
            cam = new CamController(this);
            render = new GE_Render(this);

            int count = 80;
            HS_Point[] points = new HS_Point[count * count];
            int index = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            starttime = stopwatch.ElapsedMilliseconds;
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
            Console.WriteLine("获取aabb总用时   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");


            totalWeightedMoves = new List<HS_Vector>();        //记录总移动向量
            totalWeights = new List<Double>();          //记录碰撞次数
            GrowthIntensity = new List<Double>();
            vertices = new List<GE_Vertex>();     //记录顶点数组

            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                totalWeightedMoves.Add(new HS_Vector(0, 0, 0));
                totalWeights.Add(0.0);
                GrowthIntensity.Add(0.0);
                vertices.Add(mesh.getVertexWithIndex(i));
            }
            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                if (mesh.getVertexWithIndex(i).isBoundary())
                {
                    GrowthIntensity[i]= 1.0;
                }
            }
            getGrowthIntensity();
            Print("halfedges num: " + mesh.GetHalfedges().Count);
            Print("edges num: " + mesh.GetEdges().Count);
            Print("edges array test: " + mesh.GetEdgesAsArray().Length);
            Print("test: "+mesh.getVertexWithIndex(1));
            Print("index: " + mesh.GetIndex(mesh.getVertexWithIndex(1)));
        }

        public override void Draw()
        {
            Background(255);
            cam.DrawSystem(this, 200);

            render.displayHeMeshWithDegree(mesh, cam.CurrentView, detail);
            //for (int i = 0; i < 1; i++)
            //{
                if (growth)
                {
                    update();
                }
            //}

        }
        HS_AABB aabb;

        HS_Point cen;
        Vector3 normal;
        double targetLength;
        double dis = 0;
        bool detail = false;
        bool growth = false;
        bool record = false;

        public override void KeyReleased()
        {
            Print(key);
            if (key == "F")
            {
                cam.Focus(aabb.getLimits(), false);
            }
            if (key == "Q")
            {
                double length = targetLength;
                Print("Length: " + targetLength);

                double times = (cam.CurrentView.Position - cam.CurrentView.target).X / normal.X;
                //Print("height times pro: "+(double)this.window.Height /times);
                Print("width: " + this.window.Width + " height: " + this.window.Height);
                Print("width times pro: " + (double)this.window.Width / times);
                Print("aspectRatio: " + (double)this.window.Width / (double)this.window.Height);
                Print("length pro: " + targetLength / times);

                Print("times: " + times);
                Print("target: " + cam.CurrentView.target);
            }
            else if (key == "D")
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
            if (key == "S")
            {
                growth = !growth;
            }

        }
        public void update()
        {

            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {                   //更新生长因子
                                //            System.out.println("....第" + i + "个顶点生长因子是" + GrowthIntensity.get(i));
                render.drawPoint(mesh.getVertexWithIndex(i), (float)(5 * GrowthIntensity[i]));
            }

            SplitAllLongEdges(20);   //分裂较长的边缘
                                     //        adaptiveSubdivision();
            flipEdgeControl();

            totalWeightedMoves = new List<HS_Vector>();        //记录总移动向量
            totalWeights = new List<Double>();          //记录碰撞次数
            vertices = new List<GE_Vertex>();     //记录顶点数组
            GrowthIntensity = new List<Double>();  //记录生长因子


            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                totalWeightedMoves.Add(new HS_Vector(0, 0, 0));
                totalWeights.Add(0.0);
                vertices.Add(mesh.getVertexWithIndex(i));
            }

            getKDTree();//运用KD树形结构计算

            for (int i = 0; i < 5; i++)
            {
                getGrowthIntensity();    //获取生长因子,扩散次数为5
            }

            ProcessCollisionUsingKDTree();     //计算碰撞
            EdgeLengthConstraintUsingKDTree();      //计算边长收缩，控制每条边长尽量相同
            BendingResistanceUsingKDTree();     //计算抗弯折力
            ControlEdgeAngleUsingKDTree();     //边缘角度控制


            updateVertexPosition();
        }

        public void getKDTree()
        {
            tree = new HS_KDTreeInteger<HS_Point>();
            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                tree.add(mesh.getPositionWithIndex(i), i);
            }
        }


        public void ProcessCollisionUsingKDTree()
        {
            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                //将每个顶点碰撞范围内的点记录到inRange数组中
                inRange = tree.getRange(mesh.getPositionWithIndex(i), CollisionDistance);
                //            System.out.println("......................."+inRange.length);

                //对于每一个顶点遍历其inRange数组中的每个点
                foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> j in inRange)
                {
                    if (i > j.value || i == j.value) continue;
                    //                System.out.println(j.value+"   i="+i);

                    HS_Vector move = HS_Vector.sub(mesh.getPositionWithIndex(i), mesh.getPositionWithIndex(j.value));
                    double currentDistance = move.len();
                    move = HS_Vector.mul(move, 0.5 * (CollisionDistance - currentDistance) / currentDistance);

                    totalWeightedMoves[i].Set(HS_Vector.add(totalWeightedMoves[i], (HS_Vector.mul(move, CollisionWeight))));
                    totalWeightedMoves[j.value].Set(HS_Vector.sub(totalWeightedMoves[j.value], (HS_Vector.mul(move, CollisionWeight))));

                    totalWeights[i] = totalWeights[i] + CollisionWeight;

                    totalWeights[j.value] = totalWeights[j.value] + CollisionWeight;
                }
            }

        }

        public void getGrowthIntensity()
        {
            List<Double> next = new List<Double>();          //记录下次迭代的生长因子

            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                next.Add(0.0);
                if (i > GrowthIntensity.Count || i == GrowthIntensity.Count)
                {   //新分裂出的顶点加入数组后面，生长因子为0
                    GrowthIntensity.Add(0.0);
                }
            }

            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {                       //计算出next的生长因子
                                    //            System.out.println("第"+i+"个顶点生长因子是"+GrowthIntensity.get(i));
                if (mesh.getVertexWithIndex(i).isBoundary())
                {
                    next[i] = 1.0;
                }
                else
                {
                    GE_Vertex v1 = mesh.getVertexWithIndex(i);

                    double all = GrowthIntensity[i];

                    foreach (GE_Vertex ver in v1.getNeighborVertices())
                    {
                        int index = vertices.IndexOf(ver);   //获取周围点的序号
                        all = GrowthIntensity[index] + all;
                    }
                    next[i] = all / (v1.getNeighborVertices().Count + 1);
                }
            }

            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {                   //更新生长因子
                double n1 = (double)next[i];
                GrowthIntensity[i] = n1;
                //            System.out.println("....第"+i+"个顶点生长因子是"+GrowthIntensity.get(i));
                //            System.out.println(GrowthIntensity.Count);
                //            System.out.println(mesh.GetNumberOfVertices());
            }
        }

        public double getSplitDistance(GE_Halfedge he)
        {
            double splitDistance_;

            GE_Vertex v1 = he.GetStart();
            GE_Vertex v2 = he.GetEnd();

            int index1 = vertices.IndexOf(v1);
            int index2 = vertices.IndexOf(v2);

            if (index1 >= 0 && index2 >= 0)
            {

                //        System.out.println("index1="+index1  +"       index2="+index2  );

                double n1 = (float)((GrowthIntensity[index1] + GrowthIntensity[index2]) * 0.5);      //两个端点的测地线平均值
                double n2 = map((float)n1, 0, 1.0F, 1.0F, 0);
                splitDistance_ = n2 * (MaxSplitDistance - MinSplitDistance) + MinSplitDistance;
                //        System.out.println("n1="+n1);
                //            System.out.println(n2);
                //            if (vertices.Count < 500) {
                //                splitDistance_ = 5f * (float) SplitDistance;
                //            } else {
                //                splitDistance_ = map((float) n1, 0, 1.0F, 5f * (float) SplitDistance, (float) SplitDistance);
                //            }
            }
            else
            {
                splitDistance_ = Double.MaxValue;
            }
            return splitDistance_;
        }



        private void adaptiveSubdivision()
        {

            bool done = false;
            while (done == false)
            {      //一次性细分到规定距离以内
                done = true;
                Print("11111111111111111111111111111111111111");
                foreach (GE_Halfedge he in mesh.GetHalfedges())
                {
                    double MySplitDistance = getSplitDistance(he);
                    //                double MySplitDistance = SplitDistance;

                    if (mesh.GetNumberOfVertices() < MaxVertexCount && he.GetLength() > MySplitDistance)
                    {
                        done = false;

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

                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

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

                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

                                GE_MeshOp.splitFace(mesh, f1, v1, v1_opp);
                            }
                            Print("222222222222he=" + "");

                        }
                        else if (he.IsOuterBoundary())
                        {
                            Print(he.GetLength());
                            Print(MySplitDistance);
                            Print("1111");
                            Print(done);
                            done = true;
                            Print(done);
                            continue;


                        }
                    }
                }

            }
        }


        //public void getVertex5CP()
        //{
        //    GE_Vertex he = mesh.getVertexWithIndex(35);
        //    mesh.Add(new GE_Vertex(300, 300, 0));
        //    Print(getClosetLengthToBoundary(mesh, he));
        //}


        //public double getClosetLengthToBoundary(GE_Mesh mesh, GE_Vertex v_start)
        //{
        //    double MinDistance = Double.MaxValue;
        //    foreach (GE_Vertex v in  mesh.getAllBoundaryVertices())
        //    {
        //        if (!v.Equals(v_start))
        //        {
        //            GE_Path path_temp = GE_Path.getShortestPath(v_start, v, mesh);
        //            double path_tempLength = path_temp.getPathLength();
        //            if (path_tempLength < MinDistance)
        //            {
        //                MinDistance = path_tempLength;
        //            }
        //        }
        //    }

        //    return MinDistance;
        //}


        public void EdgeLengthConstraintUsingKDTree()
        {
            foreach (GE_Halfedge he in mesh.GetHalfedges())
            {
                HS_Vector move = HS_Vector.sub(he.GetStart(), he.GetEnd());

                if (move.GetLength() > FinalLength || move.GetLength() < FinalLength)
                {
                    move = HS_Vector.mul(move, 0.5 * (move.GetLength() - FinalLength) / move.GetLength());    //如果两点距离小于碰撞距离则扩张到规定距离

                    HS_Point v1 = he.GetPosition();      //半边起点位
                    HS_Point v2 = he.GetEndPosition();      //半边起点位

                    inRange = tree.getRange(v1, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(v1))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.sub(totalWeightedMoves[ver.value], (HS_Vector.mul(move, EdgeLengthConstraintWeight))));

                            totalWeights[ver.value] = totalWeights[ver.value] + EdgeLengthConstraintWeight;
                        }
                    }

                    inRange = tree.getRange(v2, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(v2))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move, EdgeLengthConstraintWeight))));

                            totalWeights[ver.value] = totalWeights[ver.value] + EdgeLengthConstraintWeight;
                        }
                    }

                }
                else
                {
                    continue;
                }
            }
        }


        public void BendingResistanceUsingKDTree()
        {
            foreach (GE_Halfedge he in mesh.GetHalfedges())
            {
                if (!he.IsOuterBoundary() && !he.IsInnerBoundary())
                {

                    HS_Vector vI = he.GetStart().GetPosition();
                    HS_Vector vJ = he.GetEnd().GetPosition();
                    HS_Vector vP = he.GetPrevInFace().GetStart().GetPosition();
                    HS_Vector vQ = he.Pair().GetPrevInFace().GetStart().GetPosition();

                    HS_Vector nP = (HS_Vector)he.GetFace().getFaceNormal();
                    HS_Vector nQ = (HS_Vector)he.Pair().GetFace().getFaceNormal();

                    //                HS_Vector vIJ=HS_Vector.sub(vJ,vI);
                    //                HS_Vector vIP=HS_Vector.sub(vP,vI);
                    //                HS_Vector vIQ=HS_Vector.sub(vQ,vI);
                    //
                    //                HS_Vector nP = HS_Vector.cross(vIJ, vIP);
                    //                HS_Vector nQ = HS_Vector.sub(vIQ, vIJ);

                    HS_Vector planNormal = HS_Vector.add(nP, nQ);
                    HS_Vector planOrigin = vI.add(vJ).add(vP).add(vQ).mul(0.25f);

                    HS_Plane plane = new HS_Plane(planOrigin, planNormal);

                    HS_Vector vI2 = HS_GeometryOp.getClosestPoint3D(vI, plane);
                    HS_Vector vJ2 = HS_GeometryOp.getClosestPoint3D(vJ, plane);
                    HS_Vector vP2 = HS_GeometryOp.getClosestPoint3D(vP, plane);
                    HS_Vector vQ2 = HS_GeometryOp.getClosestPoint3D(vQ, plane);

                    HS_Vector move1 = HS_Vector.sub(vI2, vI);
                    HS_Vector move2 = HS_Vector.sub(vJ2, vJ);
                    HS_Vector move3 = HS_Vector.sub(vP2, vP);
                    HS_Vector move4 = HS_Vector.sub(vQ2, vQ);

                    inRange = tree.getRange(vI, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(vI))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move1, BendingResistanceWeight))));

                            //totalWeights.RemoveAt(ver.value);
                            totalWeights[ver.value]= totalWeights[ver.value] + BendingResistanceWeight;
                        }
                    }

                    inRange = tree.getRange(vJ, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(vJ))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move2, BendingResistanceWeight))));

                            //totalWeights.RemoveAt(ver.value);
                            totalWeights[ver.value]= totalWeights[ver.value] + BendingResistanceWeight;
                        }
                    }

                    inRange = tree.getRange(vP, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(vP))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move3, BendingResistanceWeight))));
                            //totalWeights.RemoveAt(ver.value);
                            totalWeights[ver.value]=totalWeights[ver.value] + BendingResistanceWeight;
                        }
                    }

                    inRange = tree.getRange(vQ, 1);
                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(vQ))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move4, BendingResistanceWeight))));

                            totalWeights[ver.value] = totalWeights[ver.value] + BendingResistanceWeight;
                        }
                    }
                }
            }
        }

        public void ControlEdgeAngleUsingKDTree()
        {
            foreach (GE_Halfedge he in mesh.getAllBoundaryHalfedges())
            {
                GE_Halfedge henext = he.GetNextInVertex();
                while (!henext.IsInnerBoundary() && !henext.IsOuterBoundary())
                {
                    henext = henext.GetNextInVertex();
                }  //循环找出边缘共顶点的半边

                HS_Vector v1 = he.GetPosition();  //半边顶点
                HS_Vector v2 = he.GetEndPosition();  //半边终点

                HS_Vector v3 = henext.GetEndPosition();  //边界相邻共点半边终点

                HS_Vector vP = HS_Vector.sub(v2, v1);
                HS_Vector vQ = HS_Vector.sub(v3, v1);
                double angle1 = HS_Vector.angle(vP, vQ);

                if (angle1 < PI)
                {
                    HS_Vector move = HS_Vector.add(vP, vQ);
                    move = HS_Vector.mul(move, 0.5 * (PI - angle1) / angle1);

                    inRange = tree.getRange(v1, 1);

                    foreach (HS_KDTreeInteger<HS_Point>.HS_KDEntryInteger<HS_Point> ver in inRange)
                    {
                        if (mesh.getPositionWithIndex(ver.value).Equals(v1))
                        {
                            totalWeightedMoves[ver.value].Set(HS_Vector.add(totalWeightedMoves[ver.value], (HS_Vector.mul(move, ControlEdgeAngleWeight))));

                            totalWeights[ver.value] = totalWeights[ver.value] + ControlEdgeAngleWeight;
                        }
                    }
                }
            }
        }

        public void updateVertexPosition()
        {
            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                if (totalWeights[i] == 0.0) continue;

                HS_Vector move = totalWeightedMoves[i].div(totalWeights[i]);    //每次更新需要移动的向量大小
                                                                                //            double g1 = GrowthIntensity.get(i);
                                                                                //            float n1 = map((float) g1, 0, 1, 0.1f, 1);
                                                                                //            HS_Vector.mul(move, n1);
                HS_Point newPosition = mesh.getPositionWithIndex(i).add(move);

                GE_Vertex v = mesh.getVertexWithIndex(i);   //更新最终的点位置
                v.Set(newPosition);

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
                    if (mesh.GetNumberOfVertices() < MaxVertexCount && he.GetLength() > 4 / 3f * target)
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

                                //println("mesh vertices size: " + mesh.getVertices().Count + ", vertices size: " + vertices.Count);
                                int index1 = vertices.IndexOf(he.GetStart());
                                int index2 = vertices.IndexOf(he.GetEnd());
                                //                        System.out.println("index1="+index1+".............index2="+index2);
                                double newVgrowthIntensity = (GrowthIntensity[index1] + GrowthIntensity[index2]) / 2;
                                //                        println(newVgrowthIntensity);

                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

                                vertices.Add(v1);
                                GrowthIntensity.Add(newVgrowthIntensity);

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
                                                                                               //                        println("mesh vertices size: "+mesh.getVertices().Count+", vertices size: "+vertices.Count);
                                int index1 = vertices.IndexOf(he.GetStart());
                                int index2 = vertices.IndexOf(he.GetEnd());
                                ////                        System.out.println("index1="+index1+".............index2="+index2);
                                double newVgrowthIntensity = (GrowthIntensity[index1] + GrowthIntensity[index2]) / 2;
                                ////                        println("new vertex growthIntensity: "+newVgrowthIntensity);
                                GE_MeshOp.splitEdge(mesh, he);        //长边从中点分割一半
                                GE_Vertex v1 = he.GetEnd();           //新的端点（半边的中点）位置更新,注意不能写成简单 GE_Vertex v1=new GE_Vertex(mid);得把网格中的vertex赋予v1

                                vertices.Add(v1);
                                GrowthIntensity.Add(newVgrowthIntensity);

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
                    if (he.GetLength() > 4 / 3f * target)
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
                        GE_MeshOp.FlipEdge(mesh, he);
                        //GE_MeshOp.flipEdge(mesh, he);
                    }
                    else if (angle1 > 0.6 * PI && angle2 < 0.5 * PI && angle3 < 0.5 * PI && angle4 < 0.5 * PI)
                    {
                        GE_MeshOp.FlipEdge(mesh, he);
                        //GE_MeshOp.flipEdge(mesh, he);
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
