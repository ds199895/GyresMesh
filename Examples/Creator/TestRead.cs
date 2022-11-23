using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Flowing;
//using Lucene.Net.Support;
using Hsy.IO;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.Render;

namespace Examples.Creator
{
    public class TestRead : IApp
    {
        static void Main(String[] args)
        {
            IApp.main();


        }
        GE_Mesh m;
        I3dmImporter im;
        CamController cam;
        GE_Render render;
        public override void SetUp()
        {
            Size(1000, 800);
            cam = new CamController(this);
            render = new GE_Render(this);
            //File f = new File("E://0917.3dm");
            //FileStream f = new FileStream("E://0917.3dm", FileMode.Open, FileAccess.Read);
            //for(int i = 0; i < f.Length; i++)
            //{
            //    f.Position = i;
            //    Console.WriteLine(f.ReadByte());
            //}
            //FileStream fs = new FileStream("E://test.3dm", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream("E://0917_VERSION4.3dm", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream("E://0917_VERSION7.3dm", FileMode.Open, FileAccess.Read);
            FileStream fs = new FileStream("E://surface.3dm", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream("E://test2.3dm", FileMode.Open, FileAccess.Read);
            im = new I3dmImporter(fs);
            im.read();
            Console.WriteLine(im.file.rhinoObjects.Length);
            Rhino3dm.PolylineCurve brep = (Rhino3dm.PolylineCurve)im.file.rhinoObjects[0];
            List<HS_Vector> pts = new List<HS_Vector>();
            pts.AddRange(brep.pline);


            HS_Polygon poly = new HS_Polygon().Create(pts);
            GEC_FromPolygons gecp = new GEC_FromPolygons();
            gecp.setPolygons(new HS_Polygon[] { poly });
            m = gecp.create();



            //Rhino3dm.Brep brep = (Rhino3dm.Brep)im.file.rhinoObjects[0];
            //List<HS_Vector> pts = new List<HS_Vector>();
            //foreach (Rhino3dm.BrepVertex v in brep.vertices)
            //{
            //    pts.Add(v.point);
            //}


            //HS_Polygon poly = new HS_Polygon().Create(pts);
            //GEC_FromPolygons gecp = new GEC_FromPolygons();
            //gecp.setPolygons(new HS_Polygon[] { poly });
            //m = gecp.create();

            //Rhino3dm.Mesh immesh = ((Rhino3dm.Mesh)im.file.rhinoObjects[0]);
            //List<HS_Vector> vertices = immesh.vertices;
            //List<HS_Point> pts = new List<HS_Point>();
            //foreach (HS_Vector vertex in vertices)
            //{
            //    HS_Point pt = new HS_Point(vertex.xd, vertex.yd, vertex.zd);

            //    pts.Add(pt);
            //}
            //List<Rhino3dm.MeshFace> faces = immesh.faces;
            //List<int[]> faceList = new List<int[]>();
            //foreach (Rhino3dm.MeshFace face in faces)
            //{
            //    //HS_Vector[] iVers = face.vertexIndex;
            //    //int[] faceVers = new int[iVers.Length];
            //    //for (int i = 0; i < iVers.Length; i++)
            //    //{
            //    //    //int id = vertices.IndexOf(iVers[i]);
            //    //    faceVers[i] = id;
            //    //}
            //    faceList.Add(face.vertexIndex);
            //}
            //GEC_FromFaceList creator = new GEC_FromFaceList();
            //creator.setVertices(pts);
            //creator.setFaces(faceList);
            //m = creator.create();
            //Print(m);
            //long n = fs.Length;
            //byte[] b = new byte[n];
            //int cnt, m;
            //m = 0;
            //cnt = fs.ReadByte();
            //while (cnt != -1)
            //{
            //    b[m++] = Convert.ToByte(cnt);
            //    cnt = fs.ReadByte();
            //}
            //string Text = Encoding.Default.GetString(b);
            //Console.WriteLine(Text);
        }

        public override void Draw()
        {
            Background(255);
            cam.DrawSystem(this, 200);
            //Fill(250, 250, 240,255);
            //Stroke(0);
            //foreach (GE_Face f in m.GetFaces())
            //{
            //    render.drawFace(f);
            //}
            render.displayHeMeshWithDegree(m, cam.CurrentView);
        }

        public override void KeyReleased()
        {
            if (key == "F")
            {
                cam.Focus(m.getAABB().getLimits(), false);
            }
        }
    }
}
