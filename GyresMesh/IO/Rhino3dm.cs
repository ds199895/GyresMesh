//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hsy.IO
//{
//    public class Rhino3dm
//    {


//        public class RhinoObject
//        {
//            public static readonly String uuid = "60B5DBBD-E660-11d3-BFE4-0010830122F0";
//        static readonly int objectTypeUnknown = 0;
//            static readonly int objectTypePoint = 1;
//            static readonly int objectTypePointset = 2;
//            static readonly int objectTypeCurve = 4;
//            static readonly int objectTypeSurface = 8;
//            static readonly int objectTypeBrep = 16;
//            static readonly int objectTypeMesh = 32;
//            static readonly int objectTypeLayer = 64;
//            static readonly int objectTypeMaterial = 128;
//            static readonly int objectTypeLight = 256;
//            static readonly int objectTypeAnnotation = 512;
//            static readonly int objectTypeUserData = 1024;
//            static readonly int objectTypeInstanceDefinition = 2048;
//            static readonly int objectTypeInstanceReference = 4096;
//            static readonly int objectTypeTextDot = 8192;
//            static readonly int objectTypeGrip = 16384;
//            static readonly int objectTypeDetail = 32768;
//            static readonly int objectTypeHatch = 65536;
//            static readonly int objectTypeMorphControl = 131072;
//            static readonly int objectTypeLoop = 524288;
//            static readonly int objectTypePolysrfFilter = 2097152;
//            static readonly int objectTypeEdgeFilter = 4194304;
//            static readonly int objectTypePolyledgeFilter = 8388608;
//            static readonly int objectTypeMeshVertex = 16777216;
//            static readonly int objectTypeMeshEdge = 33554432;
//            static readonly int objectTypeMeshFace = 67108864;
//            static readonly int objectTypeCage = 134217728;
//            static readonly int objectTypePhantom = 268435456;
//            static readonly int objectTypeClipPlane = 536870912;
//            static readonly int objectTypeBeam = 1073741824;
//            static readonly int objectTypeExtrusion = 1073741824;
//            static readonly int objectTypeAny = -1;
//            public Rhino3dm.ObjectAttributes attributes = null;
//            public Rhino3dm.UserData[] userDataList;

//            public RhinoObject()
//            {
//            }

//            public Rhino3dm.UUID getClassUUID()
//            {
//                return new Rhino3dm.UUID("60B5DBBD-E660-11d3-BFE4-0010830122F0");
//            }

//            public int getType()
//            {
//                return 0;
//            }

//            public void read(Rhino3dm.Rhino3dmFile var1, byte[] var2) throws IOException
//            {
//            this.read(var1, (InputStream)(new ByteArrayInputStream(var2)));
//        }

//        public void read(Rhino3dm.Rhino3dmFile var1, InputStream var2) throws IOException
//        {
//        }

//        public void write(Rhino3dm.Rhino3dmFile var1, OutputStream var2, CRC32 var3) throws IOException
//        {
//        }

//        public void setAttributes(Rhino3dm.ObjectAttributes var1)
//        {
//            this.attributes = var1;
//        }

//        public IObject createIObject(Rhino3dm.Rhino3dmFile var1, IServerI var2)
//        {
//            return null;
//        }

//        public void setAttributesToIObject(Rhino3dm.Rhino3dmFile var1, IObject var2)
//        {
//            if (this.attributes == null)
//            {
//                IOut.err("no attributes is set");
//            }
//            else
//            {
//                ILayer var3 = null;
//                if (this.attributes.name != null)
//                {
//                    var2.name(this.attributes.name);
//                    Console.WriteLine(10, "object name : " + this.attributes.name);
//                }

//                if (var1 != null && var1.layers != null && this.attributes.layerIndex >= 0 && this.attributes.layerIndex < var1.layers.length)
//                {
//                    var3 = var1.layers[this.attributes.layerIndex].ilayer;
//                    var3.add(var2);
//                    Console.WriteLine(10, "layer name : " + var3.name());
//                    if (!var3.isVisible())
//                    {
//                        var2.hide();
//                    }
//                }

//                if (this.attributes.colorSource == 1 && this.attributes.color != null)
//                {
//                    var2.setColor(this.attributes.color);
//                    Console.WriteLine(10, "set object color : " + this.attributes.color);
//                    Console.WriteLine(10, "set object color : <" + this.attributes.color.getRed() + "," + this.attributes.color.getGreen() + "," + this.attributes.color.getBlue() + "," + this.attributes.color.getAlpha() + ">");
//                }
//                else if (var3 != null)
//                {
//                    var2.setColor(var3.getColor());
//                    Console.WriteLine(10, "set layer color : " + var3.getColor());
//                }

//                if (!this.attributes.visible)
//                {
//                    var2.hide();
//                }

//                Console.WriteLine(10, "object color : " + var2.getColor());
//            }
//        }
//    }


//}
//}
