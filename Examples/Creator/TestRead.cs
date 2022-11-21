using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Flowing;
using Lucene.Net.Support;
using Hsy.IO;

namespace Examples.Creator
{
    public class TestRead:IApp
    {
        static void Main(String[] args)
        {
            IApp.main();
            

        }

        I3dmImporter im;
        public override void SetUp()
        {
            //File f = new File("E://0917.3dm");
            //FileStream f = new FileStream("E://0917.3dm", FileMode.Open, FileAccess.Read);
            //for(int i = 0; i < f.Length; i++)
            //{
            //    f.Position = i;
            //    Console.WriteLine(f.ReadByte());
            //}
            //FileStream fs = new FileStream("E://test.3dm", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream("E://0917_VERSION4.3dm", FileMode.Open, FileAccess.Read);
            FileStream fs = new FileStream("E://0917v4.3dm", FileMode.Open, FileAccess.Read);
            im = new I3dmImporter(fs);
            im.read();

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




    }
}
