using System;
using System.Drawing;
using Hsy.GyresMesh;
using Hsy.Geo;
//using Bitmap;
//using Graphics;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
           
            for(int i = 0; i < 10; i++)
            {
                float y = 0;
                if (i % 2 == 0)
                {
                    y = 20;
                }
                int nxt = (i + 1) % 10;
                GE_Vertex vs = new GE_Vertex(new HS_Point((float)Math.Sin(i*Math.PI*2/10), (float)Math.Cos(i * Math.PI * 2 / 10), 0));
                GE_Vertex ve = new GE_Vertex(new HS_Point((float)Math.Sin(nxt * Math.PI * 2 / 10), (float)Math.Cos(nxt* Math.PI * 2 / 10), 0));

                GE_Halfedge he = new GE_Halfedge();
                GE_Halfedge hen = new GE_Halfedge();
                GE_Halfedge hep = new GE_Halfedge();
                
                he.SetVertex(vs);
                hep.SetVertex(ve);
                he.SetNext(hen);
                he.SetPair(hep);


                GE_Mesh.helloWorld();
                Console.WriteLine(vs);
                Console.WriteLine(he);
                Console.WriteLine(he.GetLength());
            }

        }
    }
}
