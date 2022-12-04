using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.IO
{
    public class FileIO
    {
        public enum FileType
        {
            RHINO,
            OBJ,
            _3DXML,
            AI,
            SHP,
            OTHER
        }
        private I3dmImporter Import3dm;
        public List<Rhino3dm.RhinoObject> objects;
        
        public FileIO()
        {

        }

        public static List<IObject> open(string var0)
        {
            var dic = Path.GetDirectoryName(var0);
            
            Console.WriteLine(0+ " opening " + dic);

            if (!Directory.Exists(dic))
            {
                throw new Exception("the input file doesn't exist : " + dic);
                return null;
            }
            else
            {
                

                FileType var2 = GetFileType(Path.GetFileName(var0));
                Console.WriteLine(var2);
                if (var2 == FileType.OBJ)
                {
                    return null;
                    //return openObj(var0);
                }
                else if (var2 == FileType.RHINO)
                {
                    return openRhino(var0);
                    //return null;
                }
                //else if (var2 == FileType._3DXML)
                //{
                //    return open3DXML(var0, var1);
                //}
                //else if (var2 ==FileType.SHP)
                //{
                //    return openShape(var0, var1);
                //}
                else
                {
                    throw new Exception("file extension ." + Path.GetExtension(var0) + " is not supported");
                    return null;
                }
            }
        }
        public static List<Object> openObj(string var0)
        {
            return new List<object>();
        }
        public static List<IObject> openRhino(string var0)
        {
            List<IObject> var2=I3dmImporter.read(var0);

            //I3dmImporter im;
            ////FileStream fs = new FileStream("E://test.3dm", FileMode.Open, FileAccess.Read);
            ////FileStream fs = new FileStream("E://0917_VERSION4.3dm", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream("E://0917_VERSION7.3dm", FileMode.Open, FileAccess.Read);


            ////FileStream fs = new FileStream("E://surfacev7.3dm", FileMode.Open, FileAccess.Read);
            ////FileStream fs = new FileStream("E://test2.3dm", FileMode.Open, FileAccess.Read);
            //im = new I3dmImporter(fs);
            //im.read();
            //Console.WriteLine(im.file.rhinoObjects.Length);


            //return im.file.rhinoObjects.ToList();
            if (var2 != null)
            {
                Console.WriteLine(0 + " opening complete");
                return var2;
            }
            else
            {
                throw new Exception("error occured in opening file" + var0);
                return var2;
            }
        }


        public static String getExtension(String var0)
        {
            String[] var1 = var0.Split('.');
            return var1.Length < 2 ? null : var1[var1.Length - 1];
        }
        public static bool isExtension(String var0, String var1)
        {
            String var2 = getExtension(var0);
            return var2 == null ? false : var2.ToLower().Equals(var1.ToLower());
        }
        public static FileType GetFileType(string var0)
        {
            if (isExtension(var0, "3dm"))
            {
                return FileType.RHINO;
            }
            else if (isExtension(var0, "obj"))
            {
                return FileType.OBJ;
            }
            else if (isExtension(var0, "3dxml"))
            {
                return FileType._3DXML;
            }
            else if (isExtension(var0, "ai"))
            {
                return FileType.AI;
            }
            else
            {
                return isExtension(var0, "shp") ? FileType.SHP : FileType.OTHER;
            }
        }

    }
}
