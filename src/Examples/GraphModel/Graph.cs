using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;
using MathNet.Numerics.LinearAlgebra;
using Rhino.Geometry;
using Hsy.Geo;
using System.IO;

namespace GraphModel
{
    public class Graph
    {
        public edge[] edges = null;
        public node[] nodes = null;
        public int edgesNum = 0;
        public int vertexNum = 0;
        public HS_Vector[] positions = null;
        public static Random random = new Random();
        //    public bool isSubGraph=false;
        public Graph()
        {

        }
        public Graph(edge[] edges_, bool isSubGraph)
        {
            this.edges = edges_;
            this.edgesNum = edges_.Length;
            Dictionary<int, HS_Point> nodeHash = new Dictionary<int, HS_Point>();
            List<int> index = new List<int>();
            for (int i = 0; i < edges_.Length; i++)
            {
                if (!nodeHash.ContainsKey(edges_[i].start.fatherIndex))
                {
                    nodeHash.Add(edges_[i].start.fatherIndex, edges_[i].start.position);
                    index.Add(edges_[i].start.fatherIndex);
                }
            }
            for (int i = 0; i < edges_.Length; i++)
            {
                if (!nodeHash.ContainsKey(edges_[i].end.fatherIndex))
                {
                    nodeHash.Add(edges_[i].end.fatherIndex, edges_[i].end.position);
                    index.Add(edges_[i].end.fatherIndex);
                }
            }

            int[] ind = new int[index.Count];
            for (int i = 0; i < index.Count; i++)
            {
                ind[i] = index[i];
            }
            bubbleSort(ind);

            this.vertexNum = nodeHash.Count;
            if (isSubGraph = true)
            {
                this.nodes = new node[vertexNum];
                for (int i = 0; i < vertexNum; i++)
                {
                    this.nodes[i] = new node(i, ind[i], nodeHash[ind[i]]);
                }
            }
        }

        public List<int> adj(int index)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].start.index == index)
                {
                    ids.Add(edges[i].end.index);
                }
                else if (edges[i].end.index == index)
                {
                    ids.Add(edges[i].start.index);
                }
            }
            return ids;
        }

        public List<node> getNodesList()
        {
            List<node> nodeList = new List<node>();
            foreach (node n in nodes)
            {
                nodeList.Add(n);
            }
            return nodeList;
        }
        public static List<int[]> readTxtFile(String filePath)
        {
            List<int[]> cities = new List<int[]>();
            try
            {
                String encoding = "GBK";
                

                if (File.Exists(filePath))
                { //判断文件是否存在'
                    using(StreamReader reader=new StreamReader(filePath,Encoding.GetEncoding(encoding)))
                    {
                        String lineTxt = null;
                        while ((lineTxt = reader.ReadLine()) != null)
                        {
                            String[] points = lineTxt.Split(" ".ToCharArray());
                            int[] lineInts = new int[points.Length];
                            for (int i = 0; i < points.Length; i++)
                            {
                                lineInts[i] = int.Parse(points[i]);
                            }
                            cities.Add(lineInts);
                        }
                        reader.Close();
                    }
                    
                }
                else
                {
                    Console.WriteLine("找不到指定的文件");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace,"读取文件内容出错");
               
            }
            return cities;
        }
        public int getVertexNum()
        {
            return vertexNum;
        }

        public static void bubbleSort(int[] numbers)
        {
            int temp = 0;
            int size = numbers.Length;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])  //��������λ��
                    {
                        temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
        }
        public static Graph getWeightedGraph(String filename, int width, int height)
        {
            List<int[]> cities = readTxtFile(filename);
            edge[] edges_ = new edge[cities.Count];

            List<int> index = new List<int>();
            for (int i = 0; i < edges_.Length; i++)
            {
                if (!index.Contains(cities[i][0]))
                {
                    index.Add(cities[i][0]);
                }
                else if (!index.Contains(cities[i][1]))
                {
                    index.Add(cities[i][1]);
                }
            }
            int[] ind = new int[index.Count];
            for (int i = 0; i < index.Count; i++)
            {
                ind[i] = index[i];
            }
            bubbleSort(ind);
            node[] nodes_ = new node[ind.Length];
            
            for (int i = 0; i < ind.Length; i++)
            {
                nodes_[i] = new node(i, ind[i], new HS_Point(random.NextDouble() * width, random.NextDouble() * height));
            }
            for (int i = 0; i < edges_.Length; i++)
            {
                if (cities[i].Length <= 3)
                {
                    edges_[i] = new edge(nodes_[getNodeID(nodes_, cities[i][0])], nodes_[getNodeID(nodes_, cities[i][1])], cities[i][2], i);
                }
                else
                {
                    double[] datas = new double[cities[i].Length - 3];
                    for (int j = 0; j < cities[i].Length - 3; j++)
                    {
                        datas[j] = cities[i][j + 3];
                    }
                    edges_[i] = new edge(nodes_[getNodeID(nodes_, cities[i][0])], nodes_[getNodeID(nodes_, cities[i][1])], cities[i][2], datas, i);
                }
            }
            return new Graph(edges_, false);
        }
        public static Graph getGraph( int width, int height, int[][] edges)
        {
            //        List<int[]>cities=readTxtFile(filename);
            edge[] edges_ = new edge[edges.Length];

            List<int> index = new List<int>();
            for (int i = 0; i < edges_.Length; i++)
            {
                if (!index.Contains(edges[i][0]))
                {
                    index.Add(edges[i][0]);
                    Console.WriteLine(edges[i][0]);
                }
            }
            Console.WriteLine("size1: " + index.Count);
            for (int i = 0; i < edges_.Length; i++)
            {
                if (!index.Contains(edges[i][1]))
                {
                    index.Add(edges[i][1]);
                    Console.WriteLine(edges[i][1]);
                }
            }
            Console.WriteLine("size2: " + index.Count);

            int[] ind = new int[index.Count];
            for (int i = 0; i < index.Count; i++)
            {
                ind[i] = index[i];
            }
            bubbleSort(ind);
            node[] nodes_ = new node[ind.Length];
            for (int i = 0; i < ind.Length; i++)
            {
                nodes_[i] = new node(i, ind[i], new HS_Point(random.NextDouble() * width, random.NextDouble() * height));
            }
            for (int i = 0; i < edges_.Length; i++)
            {
                Console.WriteLine(edges[i][0]);
                Console.WriteLine("start id: " + getNodeID(nodes_, edges[i][0]));
                Console.WriteLine(edges[i][1]);
                Console.WriteLine("end id: " + getNodeID(nodes_, edges[i][1]));
                if (edges[i].Length == 2)
                {
                    Console.WriteLine("open");
                    edges_[i] = new edge(nodes_[getNodeID(nodes_, edges[i][0])], nodes_[getNodeID(nodes_, edges[i][1])], 1, i);
                }
                else if (edges[i].Length <= 3)
                {
                    edges_[i] = new edge(nodes_[getNodeID(nodes_, edges[i][0])], nodes_[getNodeID(nodes_, edges[i][1])], edges[i][2], i);
                }
                else
                {
                    double[] datas = new double[edges[i].Length - 3];
                    for (int j = 0; j < edges[i].Length - 3; j++)
                    {
                        datas[j] = edges[i][j + 3];
                    }
                    edges_[i] = new edge(nodes_[getNodeID(nodes_, edges[i][0])], nodes_[getNodeID(nodes_, edges[i][1])], edges[i][2], datas, i);
                }
            }
            return new Graph(edges_, false);
        }

        public static int getNodeID(node[] nodes_, int fatherIndex)
        {
            int index = -1;
            for (int i = 0; i < nodes_.Length; i++)
            {
                if (nodes_[i].fatherIndex == fatherIndex)
                {
                    index = nodes_[i].index;
                    //                Console.WriteLine("yes!");
                    //                Console.WriteLine("index :  "+nodes_[i].index+" is ok!");
                }
            }
            return index;
        }

        public List<int> getNeighbour(int index)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].start.index == index)
                {
                    ids.Add(edges[i].end.index);
                }
                else if (edges[i].end.index == index)
                {
                    ids.Add(edges[i].start.index);
                }
            }
            return ids;
        }

        public Graph subGraph(List<int> subset)
        {
            List<edge> edges_sub = new List<edge>();
            for (int i = 0; i < this.edges.Length; i++)
            {
                if (subset.Contains(this.edges[i].start.index) && subset.Contains(this.edges[i].end.index))
                {
                    edges_sub.Add(this.edges[i]);
                }
            }
            Graph g_sub = new Graph(edges_sub.ToArray(), true);
            return g_sub;
        }

        public int getEdgeID(int index1, int index2)
        {
            int id = -1;
            for (int i = 0; i < edges.Length; i++)
            {
                if ((edges[i].start.index == index1 && edges[i].end.index == index2) || (edges[i].start.index == index2 && edges[i].end.index == index1))
                {
                    id = i;
                }
            }
            return id;
        }
        //public void draw(PApplet app)
        //{
        //    for (int i = 0; i < edges.Length; i++)
        //    {
        //        app.pushStyle();
        //        app.stroke(150, 150, 255);
        //        app.line((float)edges[i].start.x, (float)edges[i].start.y, (float)edges[i].end.x, (float)edges[i].end.y);
        //        app.popStyle();

        //        app.fill(0);
        //        app.textSize(10);
        //        app.textAlign(PConstants.CENTER, PConstants.CENTER);
        //        String s = String.valueOf(edges[i].weight);
        //        if (edges[i].dataAttach != null)
        //        {
        //            for (int j = 0; j < edges[i].dataAttach.Length; j++)
        //            {
        //                s += " ; " + edges[i].dataAttach[j];
        //            }
        //        }

        //        HS_Point p1 = new HS_Point(edges[i].start.x, edges[i].start.y);
        //        HS_Point p2 = new HS_Point(edges[i].end.x, edges[i].end.y);
        //        HS_Vector v = new HS_Vector(p1, p2);
        //        double a = HS_Vector.getAngle(v, new HS_Point(1, 0));

        //        if (a > Math.PI / 2 && v.xf() < 0 && v.yf() < 0)
        //        {
        //            a = Math.PI - a;
        //        }
        //        else if (a > Math.PI / 2 && v.xf() < 0 && v.yf() > 0)
        //        {
        //            a = -(Math.PI - a);
        //        }
        //        else if (a <= Math.PI / 2 && v.xf() > 0 && v.yf() < 0)
        //        {
        //            a = -a;
        //        }
        //        HS_Vector mid = new HS_Point((edges[i].start.x + edges[i].end.x) / 2, (edges[i].start.y + edges[i].end.y) / 2);

        //        app.pushMatrix();
        //        app.translate(mid.xf(), mid.yf());
        //        app.rotate((float)a);
        //        app.pushStyle();
        //        app.noStroke();
        //        app.fill(255);

        //        app.rectMode(PConstants.CENTER);
        //        app.rect(0, 0, app.textWidth(s) + 2, 10);
        //        app.popStyle();

        //        app.text(s, 0, 0);
        //        app.popMatrix();

        //    }
        //    app.pushStyle();
        //    for (int i = 0; i < vertexNum; i++)
        //    {
        //        app.fill(150, 150, 255);
        //        app.ellipse((float)nodes[i].x, (float)nodes[i].y, 15, 15);
        //        app.fill(0);
        //        app.textAlign(PConstants.CENTER, PConstants.CENTER);
        //        app.textSize(10);
        //        app.text(nodes[i].fatherIndex, (float)nodes[i].x, (float)nodes[i].y);
        //    }
        //    app.popStyle();
        //}

        public class edge
        {
            public node start;
            public node end;
            public double weight;
            public int id;
            double[] dataAttach = null;
            public edge()
            {

            }

            public edge(node start, node end, int id)
            {
                this.start = start;
                this.end = end;
                this.weight = 1;
                this.id = id;
            }

            public edge(node start, node end, double weight, int id)
            {
                this.start = start;
                this.end = end;
                this.weight = weight;
                this.id = id;
            }
            public edge(node start, node end, double weight, double[] dataAttach, int id)
            {
                this.start = start;
                this.end = end;
                this.weight = weight;
                this.id = id;
                this.dataAttach = dataAttach;
            }
        }

        public class node
        {
            public int index;
            public int fatherIndex;
            public HS_Point position;
            public double x;
            public double y;
            public double z;

            public node()
            {
            }

            public node(int index, int fatherIndex, HS_Point pos)
            {
                this.index = index;
                this.fatherIndex = fatherIndex;
                this.position = pos;
                initialPos();
            }
            public void initialPos()
            {
                x = position.xf;
                y = position.yf;
                z = position.zf;
            }

            public int getIndex()
            {
                return index;
            }
            public int getFatherIndex()
            {
                return fatherIndex;
            }
            public HS_Point getPosition()
            {
                return position;
            }
        }
    }
}

