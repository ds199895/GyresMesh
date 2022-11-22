using Hsy.Geo;
using Lucene.Net.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.IO
{
    public class Rhino3dm
    {


        public class RhinoObject
        {
            public static readonly String uuid = "60B5DBBD-E660-11d3-BFE4-0010830122F0";
            static readonly int objectTypeUnknown = 0;
            static readonly int objectTypePoint = 1;
            static readonly int objectTypePointset = 2;
            static readonly int objectTypeCurve = 4;
            static readonly int objectTypeSurface = 8;
            static readonly int objectTypeBrep = 16;
            static readonly int objectTypeMesh = 32;
            static readonly int objectTypeLayer = 64;
            static readonly int objectTypeMaterial = 128;
            static readonly int objectTypeLight = 256;
            static readonly int objectTypeAnnotation = 512;
            static readonly int objectTypeUserData = 1024;
            static readonly int objectTypeInstanceDefinition = 2048;
            static readonly int objectTypeInstanceReference = 4096;
            static readonly int objectTypeTextDot = 8192;
            static readonly int objectTypeGrip = 16384;
            static readonly int objectTypeDetail = 32768;
            static readonly int objectTypeHatch = 65536;
            static readonly int objectTypeMorphControl = 131072;
            static readonly int objectTypeLoop = 524288;
            static readonly int objectTypePolysrfFilter = 2097152;
            static readonly int objectTypeEdgeFilter = 4194304;
            static readonly int objectTypePolyledgeFilter = 8388608;
            static readonly int objectTypeMeshVertex = 16777216;
            static readonly int objectTypeMeshEdge = 33554432;
            static readonly int objectTypeMeshFace = 67108864;
            static readonly int objectTypeCage = 134217728;
            static readonly int objectTypePhantom = 268435456;
            static readonly int objectTypeClipPlane = 536870912;
            static readonly int objectTypeBeam = 1073741824;
            static readonly int objectTypeExtrusion = 1073741824;
            static readonly int objectTypeAny = -1;
            public ObjectAttributes attributes = null;
            public UserData[] userDataList;

            public RhinoObject()
            {
            }

            public UUID getClassUUID()
            {
                return new UUID("60B5DBBD-E660-11d3-BFE4-0010830122F0");
            }

            public int getType()
            {
                return 0;
            }

            public virtual void read(Rhino3dmFile var1, byte[] var2)
            {

                
                this.read(var1, new MemoryStream(var2));
            }

            public virtual void read(Rhino3dmFile var1, Stream var2)
            {
            }
            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //}

            public void setAttributes(ObjectAttributes var1)
            {
                this.attributes = var1;
            }

            //public IObject createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    return null;
            //}

            //public void setAttributesToIObject(Rhino3dmFile var1, IObject var2)
            //{
            //    if (this.attributes == null)
            //    {
            //        throw new Exception("no attributes is set");
            //    }
            //    else
            //    {
            //        ILayer var3 = null;
            //        if (this.attributes.name != null)
            //        {
            //            var2.name(this.attributes.name);
            //            Console.WriteLine(10, "object name : " + this.attributes.name);
            //        }

            //        if (var1 != null && var1.layers != null && this.attributes.layerIndex >= 0 && this.attributes.layerIndex < var1.layers.Length)
            //        {
            //            var3 = var1.layers[this.attributes.layerIndex].ilayer;
            //            var3.add(var2);
            //            Console.WriteLine(10, "layer name : " + var3.name());
            //            if (!var3.isVisible())
            //            {
            //                var2.hide();
            //            }
            //        }

            //        if (this.attributes.colorSource == 1 && this.attributes.color != null)
            //        {
            //            var2.setColor(this.attributes.color);
            //            Console.WriteLine(10, "set object color : " + this.attributes.color);
            //            Console.WriteLine(10, "set object color : <" + this.attributes.color.getRed() + "," + this.attributes.color.getGreen() + "," + this.attributes.color.getBlue() + "," + this.attributes.color.getAlpha() + ">");
            //        }
            //        else if (var3 != null)
            //        {
            //            var2.setColor(var3.getColor());
            //            Console.WriteLine(10, "set layer color : " + var3.getColor());
            //        }

            //        if (!this.attributes.visible)
            //        {
            //            var2.hide();
            //        }

            //        Console.WriteLine(10, "object color : " + var2.getColor());
            //    }
            //}
        }

        public class ObjectAttributes : RhinoObject
        {
            public static readonly String uuid = "A828C015-09F5-477c-8665-F0482F5D6996";
            public static readonly UUID obsoletePageSpaceObjectId = new UUID(-1682229271, (short)8497, (short)20408, IntArrToByteArr(new int[] { -71, -58, 85, 36, -123, -101, -104, -72 }));
            public UUID objectUUID;
            public String name;
            public String url;
            public int layerIndex;
            public int linetypeIndex;
            public int materialIndex;
            //public RenderingAttributes renderingAttributes;
            //public IColor color;
            //public IColor plotColor;
            public int displayOrder;
            public double plotWeightMm;
            public short objectDecoration;
            public int wireDensity;
            public UUID viewportId;
            public short activeSpace;
            public bool visible;
            public byte mode;
            public byte colorSource;
            public byte plotColorSource;
            public byte plotWeightSource;
            public byte materialSource;
            public byte linetypeSource;
            public List<int> group;
            //public List<DisplayMaterialRef> dmref;

            public UUID getClassUUID()
            {
                return new UUID("A828C015-09F5-477c-8665-F0482F5D6996");
            }
            public static byte[] IntArrToByteArr(int[] intArr)
            {
                int intSize = sizeof(int) * intArr.Length;
                byte[] bytArr = new byte[intSize];
                //申请一块非托管内存
                IntPtr ptr = Marshal.AllocHGlobal(intSize);
                //复制int数组到该内存块
                Marshal.Copy(intArr, 0, ptr, intArr.Length);
                //复制回byte数组
                Marshal.Copy(ptr, bytArr, 0, bytArr.Length);
                //释放申请的非托管内存
                Marshal.FreeHGlobal(ptr);
                return bytArr;
            }
            public ObjectAttributes()
            {
                this.objectUUID = null;
                this.name = null;
                this.url = null;
                this.layerIndex = 0;
                this.linetypeIndex = -1;
                this.materialIndex = -1;
                //this.renderingAttributes = new RenderingAttributes();
                //this.color = new IColor(0, 0, 0);
                //this.plotColor = new IColor(0, 0, 0);
                this.displayOrder = 0;
                this.plotWeightMm = 0.0D;
                this.objectDecoration = 0;
                this.wireDensity = 1;
                this.viewportId = null;
                this.activeSpace = 1;
                this.visible = true;
                this.mode = 0;
                this.colorSource = 0;
                this.plotColorSource = 0;
                this.plotWeightSource = 0;
                this.materialSource = 0;
                this.linetypeSource = 0;
                this.group = null;
                //this.dmref = null;
            }

            //public ObjectAttributes(IObject var1, Rhino3dmFile var2)
            //{
            //    this.objectUUID = UUID.randomUUID();
            //    this.name = var1.name();
            //    this.url = null;
            //    if (var1.layer() != null && var1.server != null && var1.server.layers != null)
            //    {
            //        this.layerIndex = var1.server.layers.indexOf(var1.layer());
            //    }
            //    else
            //    {
            //        this.layerIndex = 0;
            //    }

            //    this.linetypeIndex = -1;
            //    //if (var1.attr() != null && var1.attr().material != null && var2.imaterials != null)
            //    //{
            //    //    this.materialIndex = var2.imaterials.indexOf(var1.attr().material);
            //    //}
            //    //else
            //    //{
            //    //    this.materialIndex = -1;
            //    //}

            //    //this.renderingAttributes = new RenderingAttributes();
            //    //this.color = var1.getColor();
            //    //this.plotColor = var1.getColor();
            //    this.displayOrder = 0;
            //    this.plotWeightMm = 0.0D;
            //    this.objectDecoration = 0;
            //    this.wireDensity = 1;
            //    this.viewportId = null;
            //    this.activeSpace = 1;
            //    this.visible = var1.visible();
            //    this.mode = 0;
            //    //if (this.color != null)
            //    //{
            //    //    this.colorSource = 1;
            //    //}
            //    else
            //    {
            //        this.colorSource = 0;
            //    }

            //    this.plotColorSource = 0;
            //    this.plotWeightSource = 0;
            //    if (this.materialIndex >= 0)
            //    {
            //        this.materialSource = 1;
            //    }
            //    else
            //    {
            //        this.materialSource = 0;
            //    }

            //    this.linetypeSource = 0;
            //    this.group = null;
            //    //this.dmref = null;
            //}

            //        public void readV5(Rhino3dmFile var1, Stream var2) 
            //        {
            //        int[] var3 = I3dmImporter.readChunkVersion(var2);
            //        int var4 = var3[0];
            //        int var5 = var3[1];
            //        bool var6 = true;
            //        this.objectUUID = I3dmImporter.readUUID(var2);
            //        this.layerIndex = I3dmImporter.readInt(var2);
            //        byte var10 = I3dmImporter.readByte(var2);
            //        if (var10 != 0) {
            //            if (var10 == 1) {
            //                this.name = I3dmImporter.readString(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 2) {
            //                this.url = I3dmImporter.readString(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 3) {
            //                this.linetypeIndex = I3dmImporter.readInt(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 4) {
            //                this.materialIndex = I3dmImporter.readInt(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 5) {
            //                this.renderingAttributes.read(var1, var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 6) {
            //                this.color = I3dmImporter.readColor(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 7) {
            //                this.plotColor = I3dmImporter.readColor(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 8) {
            //                this.plotWeightMm = I3dmImporter.readDouble(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 9) {
            //                this.objectDecoration = (short)I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 10) {
            //                this.wireDensity = I3dmImporter.readInt(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 11) {
            //                this.visible = I3dmImporter.readBool(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 12) {
            //                this.mode = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 13) {
            //                this.colorSource = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 14) {
            //                this.plotColorSource = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 15) {
            //                this.plotWeightSource = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 16) {
            //                this.materialSource = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 17) {
            //                this.linetypeSource = I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 18) {
            //                this.group = I3dmImporter.readArrayInt(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 19) {
            //                this.activeSpace = (short)I3dmImporter.readByte(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 20) {
            //                this.viewportId = I3dmImporter.readUUID(var2);
            //                var10 = I3dmImporter.readByte(var2);
            //            }

            //            if (var10 == 21) {
            //                int var7 = I3dmImporter.readInt(var2);
            //                this.dmref = new List(var7);

            //                for(int var8 = 0; var8<var7; ++var8) {
            //    DisplayMaterialRef var9 = new DisplayMaterialRef();
            //    var9.read(var1, var2);
            //    this.dmref.add(var9);
            //}

            //var10 = I3dmImporter.readByte(var2);
            //}

            //            if (var5 >= 1) {
            //                if (var10 == 22) {
            //                    this.displayOrder = I3dmImporter.readInt(var2);
            //                    var10 = I3dmImporter.readByte(var2);
            //                }

            //                if (var5 >= 2) {
            //                }
            //            }

            //        }
            //    }

            //        public void read(Rhino3dmFile var1, Stream var2) 
            //{
            //            if (var1.version >= 5 && var1.openNurbsVersion >= 200712190) {
            //        this.readV5(var1, var2);
            //    } else {
            //        int[] var3 = I3dmImporter.readChunkVersion(var2);
            //        int var4 = var3[0];
            //        int var5 = var3[1];
            //        if (var4 != 1)
            //        {
            //            throw new Exception("wrong major version " + var4);
            //            throw new IOException("wrong major version " + var4);
            //        }
            //        else
            //        {
            //            this.objectUUID = I3dmImporter.readUUID(var2);
            //            this.layerIndex = I3dmImporter.readInt(var2);
            //            this.materialIndex = I3dmImporter.readInt(var2);
            //            this.color = I3dmImporter.readColor(var2);
            //            short var6 = I3dmImporter.readShort(var2);
            //            if (var1.version < 4 || var1.openNurbsVersion < 200503170)
            //            {
            //                this.objectDecoration = (short)(var6 & 24);
            //            }

            //            I3dmImporter.readShort(var2);
            //            I3dmImporter.readDouble(var2);
            //            I3dmImporter.readDouble(var2);
            //            this.wireDensity = I3dmImporter.readInt(var2);
            //            this.mode = I3dmImporter.readByte(var2);
            //            this.colorSource = I3dmImporter.readByte(var2);
            //            this.linetypeSource = I3dmImporter.readByte(var2);
            //            this.materialSource = I3dmImporter.readByte(var2);
            //            this.name = I3dmImporter.readString(var2);
            //            this.url = I3dmImporter.readString(var2);
            //            this.visible = (this.mode & 15) != 1;
            //            if (var5 >= 1)
            //            {
            //                this.group = I3dmImporter.readArrayInt(var2);
            //                if (var5 >= 2)
            //                {
            //                    this.visible = I3dmImporter.readBool(var2);
            //                    if (var5 >= 3)
            //                    {
            //                        int var7 = I3dmImporter.readInt(var2);
            //                        this.dmref = new List(var7);

            //                        for (int var8 = 0; var8 < var7; ++var8)
            //                        {
            //                            DisplayMaterialRef var9 = new DisplayMaterialRef();
            //                            var9.read(var1, var2);
            //                            this.dmref.add(var9);
            //                        }

            //                        if (var5 >= 4)
            //                        {
            //                            this.objectDecoration = (short)(I3dmImporter.readInt(var2) & 255);
            //                            this.plotColorSource = I3dmImporter.readByte(var2);
            //                            this.plotColor = I3dmImporter.readColor(var2);
            //                            this.plotWeightSource = I3dmImporter.readByte(var2);
            //                            this.plotWeightMm = I3dmImporter.readDouble(var2);
            //                            if (var5 >= 5)
            //                            {
            //                                this.linetypeIndex = I3dmImporter.readInt(var2);
            //                                if (var5 >= 6)
            //                                {
            //                                    byte var11 = I3dmImporter.readByte(var2);
            //                                    this.activeSpace = (short)(var11 == 1 ? 2 : 1);
            //                                    this.dmref.clear();
            //                                    var7 = I3dmImporter.readInt(var2);

            //                                    for (int var12 = 0; var12 < var7; ++var12)
            //                                    {
            //                                        DisplayMaterialRef var10 = new DisplayMaterialRef();
            //                                        var10.viewportId = I3dmImporter.readUUID(var2);
            //                                        if (obsoletePageSpaceObjectId.equals(var10.viewportId))
            //                                        {
            //                                            this.viewportId = var10.viewportId;
            //                                        }
            //                                        else
            //                                        {
            //                                            this.dmref.add(var10);
            //                                        }
            //                                    }

            //                                    if (var5 >= 7)
            //                                    {
            //                                        this.renderingAttributes.read(var1, var2);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}

            public void writeReferencedComponentIndex(int var1, int var2)
            {
            }

            //public void writeV5(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    Rhino3dmExporter.writeChunkVersion(var2, 2, 1, var3);
            //    Rhino3dmExporter.writeUUID(var2, this.objectUUID, var3);
            //    Rhino3dmExporter.writeInt32(var2, this.layerIndex, var3);
            //            byte var4;
            //            if (this.name != null && this.name.Length > 0) {
            //        var4 = 1;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeString(var2, this.name, var3);
            //    }

            //            if (this.url != null && this.url.Length > 0) {
            //        var4 = 2;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeString(var2, this.url, var3);
            //    }

            //            if (this.linetypeIndex >= 0) {
            //        var4 = 3;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.linetypeIndex, var3);
            //    }

            //            if (this.materialIndex >= 0 && this.materialSource == 1) {
            //        var4 = 4;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.materialIndex, var3);
            //    }

            //            if (this.color != null) {
            //        var4 = 6;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeColor(var2, this.color, var3);
            //    }

            //            if (this.plotColor != null) {
            //        var4 = 7;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeColor(var2, this.plotColor, var3);
            //    }

            //            if (this.plotWeightMm > 0.0D) {
            //        var4 = 8;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeDouble(var2, this.plotWeightMm, var3);
            //    }

            //            if (this.objectDecoration != 0) {
            //        var4 = 9;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, (byte)(this.objectDecoration & 255), var3);
            //    }

            //            if (this.wireDensity != 1) {
            //        var4 = 10;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.wireDensity, var3);
            //    }

            //            if (!this.visible) {
            //        var4 = 11;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeBool(var2, this.visible, var3);
            //    }

            //            if (this.mode != 0) {
            //        var4 = 12;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.mode, var3);
            //    }

            //            if (this.colorSource != 0) {
            //        var4 = 13;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.colorSource, var3);
            //    }

            //            if (this.plotColorSource != 0) {
            //        var4 = 14;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.plotColorSource, var3);
            //    }

            //            if (this.plotWeightSource != 0) {
            //        var4 = 15;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.plotWeightSource, var3);
            //    }

            //            if (this.materialSource != 0) {
            //        var4 = 16;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.materialSource, var3);
            //    }

            //            if (this.linetypeSource != 0) {
            //        var4 = 17;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, this.linetypeSource, var3);
            //    }

            //            if (this.group != null && this.group.size() > 0) {
            //        var4 = 18;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeArrayInt(var2, this.group, var3);
            //    }

            //            if (this.activeSpace != 1) {
            //        var4 = 19;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeByte(var2, (byte)(this.activeSpace & 255), var3);
            //    }

            //            if (this.viewportId != null && this.viewportId != UUID.nilValue) {
            //        var4 = 20;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeUUID(var2, this.viewportId, var3);
            //    }

            //            if (this.dmref != null && this.dmref.size() > 0) {
            //        var4 = 21;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeArrayDisplayMaterialRef(var1, var2, this.dmref, var3);
            //    }

            //            if (this.displayOrder != 0) {
            //        var4 = 22;
            //        Rhino3dmExporter.writeByte(var2, var4, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.displayOrder, var3);
            //    }

            //    var4 = 0;
            //    Rhino3dmExporter.writeByte(var2, var4, var3);
            //}

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //            if (var1.version >= 5) {
            //        this.writeV5(var1, var2, var3);
            //    } else {
            //        Rhino3dmExporter.writeChunkVersion(var2, 1, 6, var3);
            //        Rhino3dmExporter.writeUUID(var2, this.objectUUID, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.layerIndex, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.materialIndex, var3);
            //        Rhino3dmExporter.writeColor(var2, this.color, var3);
            //        short var4 = this.objectDecoration;
            //        Rhino3dmExporter.writeInt16(var2, var4, var3);
            //        byte var9 = 0;
            //        Rhino3dmExporter.writeInt16(var2, var9, var3);
            //        Rhino3dmExporter.writeDouble(var2, 0.0D, var3);
            //        Rhino3dmExporter.writeDouble(var2, 1.0D, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.wireDensity, var3);
            //        Rhino3dmExporter.writeByte(var2, this.mode, var3);
            //        Rhino3dmExporter.writeByte(var2, this.colorSource, var3);
            //        Rhino3dmExporter.writeByte(var2, this.linetypeSource, var3);
            //        Rhino3dmExporter.writeByte(var2, this.materialSource, var3);
            //        Rhino3dmExporter.writeString(var2, this.name, var3);
            //        Rhino3dmExporter.writeString(var2, this.url, var3);
            //        Rhino3dmExporter.writeArrayInt(var2, this.group, var3);
            //        Rhino3dmExporter.writeBool(var2, this.visible, var3);
            //        Rhino3dmExporter.writeArrayDisplayMaterialRef(var1, var2, this.dmref, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.objectDecoration & '\uffff', var3);
            //        Rhino3dmExporter.writeByte(var2, this.plotColorSource, var3);
            //        Rhino3dmExporter.writeColor(var2, this.plotColor, var3);
            //        Rhino3dmExporter.writeByte(var2, this.plotWeightSource, var3);
            //        Rhino3dmExporter.writeDouble(var2, this.plotWeightMm, var3);
            //        Rhino3dmExporter.writeInt32(var2, this.linetypeIndex, var3);
            //        byte var5 = 0;
            //        switch (this.activeSpace)
            //        {
            //            case 0:
            //                var5 = 0;
            //                break;
            //            case 1:
            //                var5 = 0;
            //                break;
            //            case 2:
            //                var5 = 1;
            //        }

            //        Rhino3dmExporter.writeByte(var2, var5, var3);
            //        int var6 = 0;
            //        if (this.dmref != null)
            //        {
            //            var6 = this.dmref.size();
            //        }

            //        bool var7 = this.activeSpace == 2 && this.viewportId != null && this.viewportId != UUID.nilValue;
            //        Rhino3dmExporter.writeInt32(var2, var7 ? var6 + 1 : var6, var3);
            //        if (var7)
            //        {
            //            Rhino3dmExporter.writeUUID(var2, this.viewportId, var3);
            //            UUID var8 = new UUID(-1682229271, (short)8497, (short)20408, new byte[] { -71, -58, 85, 36, -123, -101, -104, -72 });
            //            Rhino3dmExporter.writeUUID(var2, var8, var3);
            //        }

            //        for (int var10 = 0; this.dmref != null && var10 < var6; ++var10)
            //        {
            //            Rhino3dmExporter.writeUUID(var2, ((DisplayMaterialRef)this.dmref.get(var10)).viewportId, var3);
            //            Rhino3dmExporter.writeUUID(var2, ((DisplayMaterialRef)this.dmref.get(var10)).displayMaterialId, var3);
            //        }

            //    }
            //}

            //public String toString()
            //{
            //    return "objectUUID = " + this.objectUUID + "\nname = " + this.name + "\nurl = " + this.url + "\nlayerIndex = " + this.layerIndex + "\nlinetypeIndex = " + this.linetypeIndex + "\nmaterialIndex = " + this.materialIndex + "\nrenderingAttributes = " + this.renderingAttributes + "\ncolor = " + this.color + "\nplotColor = " + this.plotColor + "\ndisplayOrder = " + this.displayOrder + "\nplotWeightMm = " + this.plotWeightMm + "\nobjectDecoration = " + this.objectDecoration + "\nwireDensity = " + this.wireDensity + "\nviewportId = " + this.viewportId + "\nactiveSpace = " + this.activeSpace + "\nvisible = " + this.visible + "\nmode = " + this.mode + "\ncolorSource = " + this.colorSource + "\nplotColorSource = " + this.plotColorSource + "\nplotWeightSource = " + this.plotWeightSource + "\nmaterialSource = " + this.materialSource + "\nlinetypeSource = " + this.linetypeSource + "\ngroup = " + this.group + "\ndmref = " 
            //        //+ this.dmref
            //        + "\n";
            //}
        }
        public class ClassRegistry
        {
            public static HashMap<UUID, Type> map;

            public ClassRegistry()
            {
            }

            public static void init()
            {
                map = new HashMap<UUID, Type>();
                put("60B5DBBD-E660-11d3-BFE4-0010830122F0", typeof(RhinoObject));
                put("A828C015-09F5-477c-8665-F0482F5D6996", typeof(Rhino3dm.ObjectAttributes));
                //put("60B5DBBC-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.Material));
                //put("D6FF106D-329B-4f29-97E2-FD282A618020", typeof(Rhino3dm.Texture));
                //put("390465E9-3721-11d4-800B-0010830122F0", typeof(Rhino3dm.Bitmap));
                //put("32EC997A-C3BF-4ae5-AB19-FD572B8AD554", typeof(Rhino3dm.TextureMapping));
                //put("26F10A24-7D13-4f05-8FDA-8E364DAF8EA6", typeof(Rhino3dm.Linetype));
                //put("95809813-E985-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Layer));
                //put("721D9F97-3645-44c4-8BE6-B2CF697D25CE", typeof(Rhino3dm.Group));
                //put("4F0F51FB-35D0-4865-9998-6D2C6A99721D", typeof(Rhino3dm.Font));
                //put("81BD83D5-7120-41c4-9A57-C449336FF12C", typeof(Rhino3dm.DimStyle）);
                //put("85A08513-F383-11d3-BFE7-0010830122F0", typeof(Rhino3dm.Light）);
                //put("064E7C91-35F6-4734-A446-79FF7CD659E1", typeof(Rhino3dm.HatchPattern）);
                //put("26F8BFF6-2618-417f-A158-153D64A94989", typeof(Rhino3dm.InstanceDefinition）);
                put("4ED7D4DA-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Geometry));
                //put("ECD0FD2F-2088-49dc-9641-9CF7A28FFA6B", typeof(Rhino3dm.HistoryRecord));
                put("850324A7-050E-11d4-BFFA-0010830122F0", typeof(Rhino3dm.UserData));
                //put("ABAF5873-4145-11d4-800F-0010830122F0", typeof(Rhino3dm.Annotation));
                //put("8D820224-BC6C-46b4-9066-BF39CC13AEFB", typeof(Rhino3dm.Annotation2));
                //put("4ED7D4D7-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Curve));
                //put("4ED7D4E1-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Surface));
                //put("5DE6B20D-486B-11d4-8014-0010830122F0", typeof(Rhino3dm.LinearDimension));
                //put("5DE6B20E-486B-11d4-8014-0010830122F0", typeof(Rhino3dm.RadialDimension));
                //put("5DE6B20F-486B-11d4-8014-0010830122F0", typeof(Rhino3dm.AngularDimension));
                //put("5DE6B210-486B-11d4-8014-0010830122F0", typeof(Rhino3dm.TextEntity));
                //put("5DE6B211-486B-11d4-8014-0010830122F0", typeof(Rhino3dm.Leader));
                //put("8BD94E19-59E1-11d4-8018-0010830122F0", typeof(Rhino3dm.AnnotationTextDot));
                //put("8BD94E1A-59E1-11d4-8018-0010830122F0", typeof(Rhino3dm.AnnotationArrow));
                //put("D90490A5-DB86-49f8-BDA1-9080B1F4E976", typeof(Rhino3dm.TextExtra));
                //put("8AD5B9FC-0D5C-47fb-ADFD-74C28B6F661E", typeof(Rhino3dm.DimensionExtra));
                //put("BD57F33B-A1B2-46e9-9C6E-AF09D30FFDDE", typeof(Rhino3dm.LinearDimension2));
                //put("B2B683FC-7964-4e96-B1F9-9B356A76B08B", typeof(Rhino3dm.RadialDimension2));
                //put("841BC40B-A971-4a8e-94E5-BBA26D67348E", typeof(Rhino3dm.AngularDimension2));
                //put("46F75541-F46B-48be-AA7E-B353BBE068A7", typeof(Rhino3dm.TextEntity2));
                //put("14922B7A-5B65-4f11-8345-D415A9637129", typeof(Rhino3dm.Leader2));
                //put("74198302-CDF4-4f95-9609-6D684F22AB37", typeof(Rhino3dm.TextDot));
                //put("C8288D69-5BD8-4f50-9BAF-525A0086B0C3", typeof(Rhino3dm.OrdinateDimension2.));
                //put("A68B151F-C778-4a6e-BCB4-23DDD1835677", typeof(Rhino3dm.AngularDimension2Extra));
                //put("CF33BE2A-09B4-11d4-BFFB-0010830122F0", typeof(Rhino3dm.ArcCurve));
                //put("36F53175-72B8-4d47-BF1F-B4E6FC24F4B9", typeof(Rhino3dm.Extrusion));
                //put("390465EB-3721-11d4-800B-0010830122F0", typeof(Rhino3dm.WindowsBitmap));
                //put("772E6FC1-B17B-4fc4-8F54-5FDA511D76D2", typeof(Rhino3dm.EmbeddedBitmap));
                //put("203AFC17-BCC9-44fb-A07B-7F5C31BD5ED9", typeof(Rhino3dm.WindowsBitmapEx));
                //put("60B5DBC0-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepVertex));
                //put("60B5DBC1-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepEdge));
                //put("60B5DBC2-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepTrim));
                //put("60B5DBC3-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepLoop));
                //put("60B5DBC4-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepFace));
                //put("60B5DBC5-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.Brep));
                //put("7FE23D63-E536-43f1-98E2-C807A2625AFF", typeof(Rhino3dm.BrepRegionTopologyUserData));
                //put("30930370-0D5B-4ee4-8083-BD635C7398A4", typeof(Rhino3dm.BrepFaceSide));
                //put("CA7A0092-7EE6-4f99-B9D2-E1D6AA798AA1", typeof(Rhino3dm.BrepRegion));
                //put("4ED7D4D8-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.CurveOnSurface));
                //put("C8C66EFA-B3CB-4e00-9440-2AD66203379E", typeof(Rhino3dm.DetailView));
                //put("513FDE53-7284-4065-8601-06CEA8B28D6F", typeof(Rhino3dm.DimStyleExtra));
                //put("3FF7007C-3D04-463f-84E3-132ACEB91062", typeof(Rhino3dm.HatchExtra));
                //put("0559733B-5332-49d1-A936-0532AC76ADE5", typeof(Rhino3dm.Hatch));
                //put("F9CFB638-B9D4-4340-87E3-C56E7865D96A", typeof(Rhino3dm.InstanceRef));
                //put("3E4904E6-E930-4fbc-AA42-EBD407AEFE3B", typeof(Rhino3dm.LayerExtensions));
                //put("4ED7D4D9-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.CurveProxy));
                //put("4ED7D4DB-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.LineCurve));
                put("4ED7D4E4-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Mesh));
                //put("4ED7D4E4-FFFFE947-000011D3-BFE50010830122F0", typeof(Rhino3dm.Mesh));
                //put("C547B4BD-BDCD-49b6-A983-0C4A7F02E31A", typeof(Rhino3dm.MeshVertexRef));
                //put("ED727872-463A-4424-851F-9EC02CB0F155", typeof(Rhino3dm.MeshEdgeRef));
                //put("4F529AA5-EF8D-4c25-BCBB-162D510AA280", typeof(Rhino3dm.MeshFaceRef));
                //put("31F55AA3-71FB-49f5-A975-757584D937FF", typeof(Rhino3dm.MeshNgonUserData));
                //put("4ED7D4DD-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                //put("4ED7D4DE-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                //put("06936AFB-3D3C-41ac-BF70-C9319FA480A1", typeof(Rhino3dm.NurbsCage));
                //put("D379E6D8-7C31-4407-A913-E3B7040D034A", typeof(Rhino3dm.MorphControl));
                //put("00C61749-D430-4ecc-83A8-29130A20CF9C", typeof(Rhino3dm.OffsetSurface));
                //put("4ED7D4DF-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PlaneSurface));
                //put("DBC5A584-CE3F-4170-98A8-497069CA5C36", typeof(Rhino3dm.ClippingPlaneSurface));
                //put("2488F347-F8FA-11d3-BFEC-0010830122F0", typeof(Rhino3dm.PointCloud));
                //put("C3101A1D-F157-11d3-BFE7-0010830122F0", typeof(Rhino3dm.Point));
                //put("4ED7D4E5-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PointGrid));
                //put("4ED7D4E0-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PolyCurve));
                //put("42F47A87-5B1B-4e31-AB87-4639D78325D6", typeof(Rhino3dm.PolyEdgeSegment));
                //put("39FF3DD3-FE0F-4807-9D59-185F0D73C0E4", typeof(Rhino3dm.PolyEdgeCurve));
                //put("4ED7D4E6-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PolylineCurve));
                //put("A16220D3-163B-11d4-8000-0010830122F0", typeof(Rhino3dm.RevSurface));
                //put("C4CD5359-446D-4690-9FF5-29059732472B", typeof(Rhino3dm.SumSurface));
                //put("4ED7D4E2-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.SurfaceProxy));
                //put("850324A8-050E-11d4-BFFA-0010830122F0", typeof(Rhino3dm.UnknownUserData));
                //put("D66E5CCF-EA39-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Viewport));
                //put("5EAF1119-0B51-11d4-BFFE-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                //put("76A709D5-1550-11d4-8000-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                //put("4760C817-0BE3-11d4-BFFE-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                //put("FA4FD4B5-1613-11d4-8000-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                //put("EF638317-154B-11d4-8000-0010830122F0", typeof(Rhino3dm.PolyCurve));
                //put("0705FDEF-3E2A-11d4-800E-0010830122F0", typeof(Rhino3dm.Brep));
                //put("2D4CFEDB-3E2A-11d4-800E-0010830122F0", typeof(Rhino3dm.Brep));
                //put("F06FC243-A32A-4608-9DD8-A7D2C4CE2A36", typeof(Rhino3dm.Brep));
                //put("0A8401B6-4D34-4b99-8615-1B4E723DC4E5", typeof(Rhino3dm.RevSurface));
                //put("665F6331-2A66-4cce-81D0-B5EEBD9B5417", typeof(Rhino3dm.SumSurface));
            }

            public static void put(String var0, Type var1)
            {
                put(new UUID(var0), var1);
            }

            public static void put(UUID var0, Type var1)
            {
                if (map.ContainsKey(var0))
                {
                    throw new Exception("UUID " + var0 + " is already set @" + var1);
                }

                map.Add(var0, var1);
            }

            public static Type get(UUID var0)
            {
                
                Console.WriteLine(map.Keys.ToArray()[4]);
                Type t = null;
                foreach (var key in map.Keys.ToArray())
                {
                    if (key.Equals(var0))
                    {
                        map.TryGetValue(key, out t);
                    }
                }
                
                Console.WriteLine(t);
                return t;
            }
        }
        public class Geometry : Rhino3dm.RhinoObject
        {
            public static readonly String uuid = "4ED7D4DA-E947-11d3-BFE5-0010830122F0";

            public Geometry()
            {
            }

            public UUID getClassUUID()
            {
                return new UUID("4ED7D4DA-E947-11d3-BFE5-0010830122F0");
            }

        }
        public class Interval
        {
            public double v1;
            public double v2;

            public Interval()
            {
            }

            public Interval(double var1, double var3)
            {
                this.v1 = var1;
                this.v2 = var3;
            }

            public Interval(Interval var1)
            {
                this.v1 = var1.v1;
                this.v2 = var1.v2;
            }

            public bool isIncreasing()
            {
                return this.v1 < this.v2;
            }

            public bool isValid()
            {
                return true;
            }

            public bool includes(double var1)
            {
                return this.includes(var1, false);
            }

            public bool includes(double var1, bool var3)
            {
                if (var3)
                {
                    return Math.Min(this.v1, this.v2) < var1 && var1 < Math.Max(this.v1, this.v2);
                }
                else
                {
                    return Math.Min(this.v1, this.v2) <= var1 && var1 <= Math.Max(this.v1, this.v2);
                }
            }

            public bool includes(Interval var1)
            {
                return this.includes(var1, false);
            }

            public bool includes(Interval var1, bool var2)
            {
                if (this.includes(var1.v1) && this.includes(var1.v2))
                {
                    return !var2 || this.includes(var1.v1, true) || this.includes(var1.v2, true);
                }
                else
                {
                    return false;
                }
            }

            public void set(double var1, double var3)
            {
                this.v1 = var1;
                this.v2 = var3;
            }

            public void set(Interval var1)
            {
                this.v1 = var1.v1;
                this.v2 = var1.v2;
            }

            public bool equals(Interval var1)
            {
                if (this.v1 != var1.v1)
                {
                    return false;
                }
                else
                {
                    return this.v2 == var1.v2;
                }
            }

            public void reverse()
            {
                double var1 = this.v1;
                this.v1 = this.v2;
                this.v2 = var1;
            }

            public double normalizedParameterAt(double var1)
            {
                double var3 = this.v1;
                if (this.v1 != this.v2)
                {
                    var3 = var1 == this.v2 ? 1.0D : (var1 - this.v1) / (this.v2 - this.v1);
                }

                return var3;
            }

            public double parameterAt(double var1)
            {
                return (1.0D - var1) * this.v1 + var1 * this.v2;
            }

            public double min()
            {
                return Math.Min(this.v1, this.v2);
            }

            public double max()
            {
                return Math.Max(this.v1, this.v2);
            }

            public double length()
            {
                return this.v2 - this.v1;
            }

            public void intersection(Interval var1)
            {
                double var2 = this.min();
                double var4 = var1.min();
                double var6 = var2 >= var4 ? var2 : var4;
                var2 = this.max();
                var4 = var1.max();
                double var8 = var2 <= var4 ? var2 : var4;
                if (var6 <= var8)
                {
                    this.set(var6, var8);
                }

            }

            public override String ToString()
            {
                return "[" + this.v1 + "," + this.v2 + "]";
            }
        }
        public class Xform
        {
            public double[][] xform;

            public Xform()
            {
                this.xform = new double[4][];
                for(int i = 0; i < xform.Length; i++)
                {
                    this.xform[i] = new double[4];
                }
            }

            public Xform(double[][] var1)
            {
                this.xform = var1;
            }

            public bool isZero()
            {
                for (int var1 = 0; var1 < 4; ++var1)
                {
                    for (int var2 = 0; var2 < 4; ++var2)
                    {
                        if (this.xform[var1][var2] != 0.0D)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public void identity()
            {
                for (int var1 = 0; var1 < 4; ++var1)
                {
                    for (int var2 = 0; var2 < 4; ++var2)
                    {
                        if (var1 == var2)
                        {
                            this.xform[var1][var2] = 1.0D;
                        }
                        else
                        {
                            this.xform[var1][var2] = 0.0D;
                        }
                    }
                }

            }

            public String toString()
            {
                String var1 = "[";

                for (int var2 = 0; var2 < 4; ++var2)
                {
                    for (int var3 = 0; var3 < 4; ++var3)
                    {
                        var1 = var1 + (this.xform[var2][var3]).ToString();
                        if (var2 < 3 || var3 < 3)
                        {
                            var1 = var1 + ",";
                        }
                    }
                }

                var1 = var1 + "]";
                return var1;
            }
        }
        public  class TextureMapping : RhinoObject
        {
        public static readonly String uuid = "32EC997A-C3BF-4ae5-AB19-FD572B8AD554";

        public TextureMapping()
        {
        }

        public static TextureMapping.Type_ type(int var0)
        {
            switch (var0)
            {
                case 0:
                    return TextureMapping.Type_.NoMapping;
                case 1:
                    return TextureMapping.Type_.SrfpMapping;
                case 2:
                    return TextureMapping.Type_.PlaneMapping;
                case 3:
                    return TextureMapping.Type_.CylinderMapping;
                case 4:
                    return TextureMapping.Type_.SphereMapping;
                case 5:
                    return TextureMapping.Type_.BoxMapping;
                case 6:
                    return TextureMapping.Type_.MeshMappingPrimitive;
                case 7:
                    return TextureMapping.Type_.SrfMappingPrimitive;
                case 8:
                    return TextureMapping.Type_.BrepMappingPrimitive;
                default:
                    return TextureMapping.Type_.NoMapping;
            }
        }

        public UUID getClassUUID()
        {
            return new UUID("32EC997A-C3BF-4ae5-AB19-FD572B8AD554");
        }

        public enum Type_
        {
            NoMapping,
            SrfpMapping,
            PlaneMapping,
            CylinderMapping,
            SphereMapping,
            BoxMapping,
            MeshMappingPrimitive,
            SrfMappingPrimitive,
            BrepMappingPrimitive

    }
}




public class MappingTag
        {
            public UUID mappingId;
            public TextureMapping.Type_ mappingType;
            public int mappingCRC;
            public Xform meshXform;

            public MappingTag()
            {
            }

            public void read(Rhino3dmFile var1,Stream var2)
            {

                Chunk var3 = I3dmImporter.readChunk(var2);
           MemoryStream var6 = new  MemoryStream(var3.content);
            int var4 = I3dmImporter.readInt(var6);
            int var5 = I3dmImporter.readInt(var6);
            if (var4 == 1) {
                this.mappingId = I3dmImporter.readUUID(var6);
                this.mappingCRC = I3dmImporter.readInt(var6);
                this.meshXform = I3dmImporter.readXform(var6);
                if (var5 >= 1) {
                    this.mappingType = TextureMapping.type(I3dmImporter.readInt(var6));
                }
    }

}
    }


        public class MeshParameters
        {
            public bool customSetting = false;
            public bool computeCurvature = false;
            public bool simplePlanes = false;
            public bool refine = true;
            public bool jaggedSeams = false;
            public byte reserved1 = 0;
            public byte reserved2 = 0;
            public byte mesher = 0;
            public int textureRange = 2;
            public double tolerance = 0.0D;
            public double relativeTolerance = 0.0D;
            public double minTolerance = 0.0D;
            public double minEdgeLength = 1.0E-4D;
            public double maxEdgeLength = 0.0D;
            public double gridAspectRatio = 6.0D;
            public int gridMinCount = 0;
            public int gridMaxCount = 0;
            public double gridAngle = 0.3490658503988659D;
            public double gridAmplification = 1.0D;
            public double refineAngle = 0.3490658503988659D;
            public int faceType = 0;

            public MeshParameters()
            {
            }

            public static bool readIntAsbool(Stream var0)
            {
                return I3dmImporter.readInt(var0) != 0;
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    bool var6 = false;
                    this.computeCurvature = readIntAsbool(var2);
                    this.simplePlanes = readIntAsbool(var2);
                    this.refine = readIntAsbool(var2);
                    this.jaggedSeams = readIntAsbool(var2);
                    int var7 = I3dmImporter.readInt(var2);
                    this.tolerance = I3dmImporter.readDouble(var2);
                    this.minEdgeLength = I3dmImporter.readDouble(var2);
                    this.maxEdgeLength = I3dmImporter.readDouble(var2);
                    this.gridAspectRatio = I3dmImporter.readDouble(var2);
                    this.gridMinCount = I3dmImporter.readInt(var2);
                    this.gridMaxCount = I3dmImporter.readInt(var2);
                    this.gridAngle = I3dmImporter.readDouble(var2);
                    this.gridAmplification = I3dmImporter.readDouble(var2);
                    this.refineAngle = I3dmImporter.readDouble(var2);
                    double var8 = I3dmImporter.readDouble(var2);
                    this.faceType = I3dmImporter.readInt(var2);
                    if (this.faceType < 0 || this.faceType > 2)
                    {
                        this.faceType = 0;
                        throw new IOException("faceType out of range");
                    }

                    if (var5 >= 1)
                    {
                        this.textureRange = I3dmImporter.readInt(var2);
                        if (var5 >= 2)
                        {
                            this.customSetting = I3dmImporter.readBool(var2);
                            this.relativeTolerance = I3dmImporter.readDouble(var2);
                            if (var5 >= 3)
                            {
                                this.mesher = I3dmImporter.readByte(var2);
                            }
                        }
                    }
                }

            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //}
        }
        public class MeshCurvatureStats
        {
            public MeshCurvatureStats.CurvatureStyle style;
            public double infinity;
            public int countInfinite;
            public int count;
            public double mode;
            public double average;
            public double adev;
            public Interval range;

            public MeshCurvatureStats()
            {
            }

            public static MeshCurvatureStats.CurvatureStyle Style(int var0)
            {
                switch (var0)
                {
                    case 1:
                        return MeshCurvatureStats.CurvatureStyle.GaussianCurvature;
                    case 2:
                        return MeshCurvatureStats.CurvatureStyle.MeanCurvature;
                    case 3:
                        return MeshCurvatureStats.CurvatureStyle.MinCurvature;
                    case 4:
                        return MeshCurvatureStats.CurvatureStyle.MaxCurvature;
                    default:
                        return MeshCurvatureStats.CurvatureStyle.UnknownCurvature;
                }
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    bool var6 = false;
                    this.style = Style(I3dmImporter.readInt(var2));
                    this.infinity = I3dmImporter.readDouble(var2);
                    this.countInfinite = I3dmImporter.readInt(var2);
                    this.count = I3dmImporter.readInt(var2);
                    this.mode = I3dmImporter.readDouble(var2);
                    this.average = I3dmImporter.readDouble(var2);
                    this.adev = I3dmImporter.readDouble(var2);
                    this.range = I3dmImporter.readInterval(var2);
                }

            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //}

            public enum CurvatureStyle
            {
                UnknownCurvature,
                GaussianCurvature,
                MeanCurvature,
                MinCurvature,
                MaxCurvature
            }
            //    private CurvatureStyle()
            //    {
            //    }
            //}
        }
        public class SurfaceCurvature
        {
            public double k1;
            public double k2;

            public SurfaceCurvature()
            {
            }
        }

        public class MeshFace
        {
            public int[] vertexIndex;

            public MeshFace()
            {
            }

            public MeshFace(int[] var1)
            {
                if (var1 == null)
                {
                    throw new Exception("input array is null");
                }

                if (var1.Length == 4)
                {
                    this.vertexIndex = var1;
                }
                else if (var1.Length == 3)
                {
                    this.vertexIndex = new int[4];
                    this.vertexIndex[0] = var1[0];
                    this.vertexIndex[1] = var1[1];
                    this.vertexIndex[2] = var1[2];
                    this.vertexIndex[3] = var1[2];
                }
                else
                {
                    throw new Exception("wrong input array size (" + var1.Length + "). vertex number should be 3 or 4.");
                }

            }

            public MeshFace(int var1, int var2, int var3, int var4)
            {
                this.vertexIndex = new int[4];
                this.vertexIndex[0] = var1;
                this.vertexIndex[1] = var2;
                this.vertexIndex[2] = var3;
                this.vertexIndex[3] = var4;
            }

            public MeshFace(int var1, int var2, int var3)
            {
                this.vertexIndex = new int[4];
                this.vertexIndex[0] = var1;
                this.vertexIndex[1] = var2;
                this.vertexIndex[2] = var3;
                this.vertexIndex[3] = var3;
            }

            public override String ToString()
            {
                return this.vertexIndex[0] + "-" + this.vertexIndex[1] + "-" + this.vertexIndex[2] + "-" + this.vertexIndex[3];
            }
        }




        public class Mesh : Rhino3dm.Geometry
        {
            public static String uuid = "4ED7D4E4-E947-11d3-BFE5-0010830122F0";
            public List<HS_Vector> vertices;
            public int invalidCount;
            public int quadCount;
            public int triangleCount;
            public int closed;
            public float[][] vbox;
            public float[][] nbox;
            public float[][] tbox;
            public RhinoObject parent;
            public MeshParameters meshParameters;
            public Interval[] packedTextureDomain;
            public Interval[] surfaceDomain;
            public double[] surfaceScale;
            public MeshCurvatureStats[] curvatureStat;
            public List<MeshFace> faces;
            public List<HS_Vector> normals;
            public List<HS_Vector> unitNormals;
            public List<HS_Vector> texture;
            public List<SurfaceCurvature> surfaceCurvature;
            public List<IColor> color;
            public List<HS_Vector> surfaceParameter;
            public bool packedTextureRotate;
            public MappingTag ttag;

            public UUID getClassUUID()
            {
                return new UUID("4ED7D4E4-E947-11d3-BFE5-0010830122F0");
            }
            public int getType()
            {
                return 32;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {

                //var2.Position += 4;
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1 || var4 == 3)
                {
                    int var6 = I3dmImporter.readInt(var2);
                    int var7 = I3dmImporter.readInt(var2);
                    this.packedTextureDomain = new Rhino3dm.Interval[2];
                    this.packedTextureDomain[0] = I3dmImporter.readInterval(var2);
                    this.packedTextureDomain[1] = I3dmImporter.readInterval(var2);
                    this.surfaceDomain = new Rhino3dm.Interval[2];
                    this.surfaceDomain[0] = I3dmImporter.readInterval(var2);
                    this.surfaceDomain[1] = I3dmImporter.readInterval(var2);
                    this.surfaceScale = new double[2];
                    this.surfaceScale[0] = I3dmImporter.readDouble(var2);
                    this.surfaceScale[1] = I3dmImporter.readDouble(var2);
                    this.vbox = new float[2][];

                    int var8;
                    int var9;
                    for (var8 = 0; var8 < this.vbox.Length; ++var8)
                    {
                        this.vbox[var8] = new float[3];
                        for (var9 = 0; var9 < this.vbox[var8].Length; ++var9)
                        {
                            this.vbox[var8][var9] = I3dmImporter.readFloat(var2);
                        }
                    }

                    this.nbox = new float[2][];

                    for (var8 = 0; var8 < this.nbox.Length; ++var8)
                    {
                        this.nbox[var8] = new float[3];
                        for (var9 = 0; var9 < this.nbox[var8].Length; ++var9)
                        {
                            this.nbox[var8][var9] = I3dmImporter.readFloat(var2);
                        }
                    }

                    this.tbox = new float[2][];

                    for (var8 = 0; var8 < this.tbox.Length; ++var8)
                    {
                        this.tbox[var8] = new float[2];
                        for (var9 = 0; var9 < this.tbox[var8].Length; ++var9)
                        {
                            this.tbox[var8][var9] = I3dmImporter.readFloat(var2);
                        }
                    }

                    this.closed = I3dmImporter.readInt(var2);
                    bool var12 = false;
                    byte var13 = I3dmImporter.readByte(var2);
                    if (var13 > 0)
                    {
                        Rhino3dm.Chunk var16 = I3dmImporter.readChunk(var2);
                       
                        if (var16.header != 1073774592)
                        {
                            throw new IOException("invalid type code = " + Rhino3dm.hex(var16.header));
                        }

                        MemoryStream var10 = new MemoryStream(var16.content);
                        this.meshParameters = new Rhino3dm.MeshParameters();
                        this.meshParameters.read(var1, var10);
                    }

                    this.curvatureStat = new Rhino3dm.MeshCurvatureStats[4];

                    for (var9 = 0; var9 < 4; ++var9)
                    {
                        var13 = I3dmImporter.readByte(var2);
                        if (var13 > 0)
                        {
                            Rhino3dm.Chunk var14 = I3dmImporter.readChunk(var2);
                            if (var14.header != 1073774592)
                            {
                                throw new IOException("invalid type code = " + Rhino3dm.hex(var14.header));
                            }

                            MemoryStream var11 = new MemoryStream(var14.content);
                            this.curvatureStat[var9] = new Rhino3dm.MeshCurvatureStats();
                            this.curvatureStat[var9].read(var1, var11);
                        }
                    }

                    this.readFaceArray(var1, var2, var6, var7);
                    Console.WriteLine("var4:  " + var4);
                    Console.WriteLine("Faces:  " + this.faces.Count) ;
                    if (var4 == 1)
                    {
                        this.read1(var1, var2);
                    }
                    else if (var4 == 3)
                    {
                        this.read2(var1, var2, var6);
                    }

                    if (var5 >= 2)
                    {
                        var9 = I3dmImporter.readInt(var2);
                        this.packedTextureRotate = var9 != 0;
                    }

                    if (var4 == 3 && var5 >= 3)
                    {
                        this.ttag = new Rhino3dm.MappingTag();
                        this.ttag.mappingId = I3dmImporter.readUUID(var2);
                        if (var6 > 0)
                        {
                            var9 = I3dmImporter.readCompressedBufferSize(var2);
                            byte var15 = 16;
                            if (var9 > 0)
                            {
                                if (var9 != var15 * var6)
                                {
                                    throw new IOException("buffer size (" + var9 + ") doesn't match with vcount(" + var6 + ")*surface_parameter_data_size(" + var15 + ")");
                                }

                                this.surfaceParameter = I3dmImporter.readArrayPoint2(I3dmImporter.readCompressedBuffer(var2, var9), var6);
                                //if (Rhino3dm.endian() == Rhino3dm.Endian.BigEndian)
                                //{
                                //}

                                if (var5 >= 4 && var1.openNurbsVersion >= 200606010)
                                {
                                    this.ttag.read(var1, var2);
                                }
                            }
                        }
                    }

                    if (this.surfaceParameter == null && this.vertices != null && this.vertices.Count> 0)
                    {
                    }
                }

            }
            public void read1(Rhino3dmFile var1, Stream var2)
            {
                this.vertices = I3dmImporter.readArrayPoint3f(var2);
                this.normals = I3dmImporter.readArrayPoint3f(var2);
                this.texture = I3dmImporter.readArrayPoint2f(var2);
                this.surfaceCurvature = I3dmImporter.readArraySurfaceCurvature(var2);
                this.color = I3dmImporter.readArrayColor(var2);
            }

            public void read2(Rhino3dmFile var1, Stream var2, int var3)
            {
                Endian var4=Endian.BigEndian;
                if (var3 > 0)
                {
                    byte var6 = 12;
                    int var7 = I3dmImporter.readCompressedBufferSize(var2);
                    if (var7 > 0)
                    {
                        if (var7 != var3 * var6)
                        {
                            throw new IOException("buffer size (" + var7 + ") doesn't match with vcount(" + var3 + ")*vertex_data_size(" + var6 + ")");
                        }

                        this.vertices = I3dmImporter.readArrayPoint3f(I3dmImporter.readCompressedBuffer(var2, var7), var3);
                    }

                    var7 = I3dmImporter.readCompressedBufferSize(var2);
                    if (var7 > 0)
                    {
                        if (var7 != var3 * var6)
                        {
                            throw new IOException("buffer size (" + var7 + ") doesn't match with vcount(" + var3 + ")*normal_data_size(" + var6 + ")");
                        }

                        this.normals = I3dmImporter.readArrayPoint3f(I3dmImporter.readCompressedBuffer(var2, var7), var3);
                    }

                    var6 = 8;
                    var7 = I3dmImporter.readCompressedBufferSize(var2);
                    if (var7 > 0)
                    {
                        if (var7 != var3 * var6)
                        {
                            throw new IOException("buffer size (" + var7 + ") doesn't match with vcount(" + var3 + ")*texture_data_size(" + var6 + ")");
                        }

                        this.texture = I3dmImporter.readArrayPoint2f(I3dmImporter.readCompressedBuffer(var2, var7), var3);
                    }

                    var6 = 16;
                    var7 = I3dmImporter.readCompressedBufferSize(var2);
                    if (var7 > 0)
                    {
                        if (var7 != var3 * var6)
                        {
                            throw new IOException("buffer size (" + var7 + ") doesn't match with vcount(" + var3 + ")*curvature_data_size(" + var6 + ")");
                        }

                        this.surfaceCurvature = I3dmImporter.readArraySurfaceCurvature(I3dmImporter.readCompressedBuffer(var2, var7), var3);
                    }

                    var6 = 4;
                    var7 = I3dmImporter.readCompressedBufferSize(var2);
                    if (var7 > 0)
                    {
                        if (var7 != var3 * var6)
                        {
                            throw new IOException("buffer size (" + var7 + ") doesn't match with vcount(" + var3 + ")*color_data_size(" + var6 + ")");
                        }

                        this.color = I3dmImporter.readArrayColor(I3dmImporter.readCompressedBuffer(var2, var7), var3);
                    }

                    if (var4 == Endian.BigEndian)
                    {
                    }
                }

            }
            public void readFaceArray(Rhino3dmFile var1, Stream var2, int var3, int var4)
            {
                this.faces = new List<MeshFace>(var4);
                bool var5 = false;
                int var10 = I3dmImporter.readInt(var2);
                switch (var10)
                {
                    case 1:
                        byte[] var6 = new byte[4];

                        for (int var11 = 0; var11 < var4; ++var11)
                        {
                            var6[0] = I3dmImporter.readByte(var2);
                            var6[1] = I3dmImporter.readByte(var2);
                            var6[2] = I3dmImporter.readByte(var2);
                            var6[3] = I3dmImporter.readByte(var2);
                            this.faces.Add(new MeshFace(var6[0] & 255, var6[1] & 255, var6[2] & 255, var6[3] & 255));
                        }

                        return;

                    case 2:
                        short[] var7 = new short[4];

                        for (int var12 = 0; var12 < var4; ++var12)
                        {
                            var7[0] = I3dmImporter.readShort(var2);
                            var7[1] = I3dmImporter.readShort(var2);
                            var7[2] = I3dmImporter.readShort(var2);
                            var7[3] = I3dmImporter.readShort(var2);
                            this.faces.Add(new MeshFace(var7[0] & '\uffff', var7[1] & '\uffff', var7[2] & '\uffff', var7[3] & '\uffff'));
                        }
                        break;
                    case 3:
                    default:
                        break;
                    case 4:
                        int[] var8 = new int[4];

                        for (int var9 = 0; var9 < var4; ++var9)
                        {
                            var8[0] = I3dmImporter.readInt(var2);
                            var8[1] = I3dmImporter.readInt(var2);
                            var8[2] = I3dmImporter.readInt(var2);
                            var8[3] = I3dmImporter.readInt(var2);
                            this.faces.Add(new MeshFace(var8[0], var8[1], var8[2], var8[3]));
                        }
                        break;
                }

            }




        }



        public class UUID
        {
            public static UUID nilValue = new UUID(0, (short)0, (short)0, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            public int data1;
            public short data2;
            public short data3;
            public byte[] data4;

            public static UUID randomUUID()
            {
                Guid var0 = Guid.NewGuid();
                String str = var0.ToString("X");
                byte[] bts = Encoding.Default.GetBytes(str.Substring(8, 8));
                return new UUID(int.Parse(str.Substring(0, 4)), short.Parse(str.Substring(4, 2)), short.Parse(str.Substring(6, 2)), bts);
            }

            public UUID()
            {
            }

            public UUID(String var1)
            {
                this.initWithString(var1);
            }

            public UUID(int var1, short var2, short var3, byte[] var4)
            {
                this.data1 = var1;
                this.data2 = var2;
                this.data3 = var3;
                this.data4 = var4;
            }

            public UUID(long var1, long var3)
            {
                this.data1 = (int)(var1 >> 32 & -1L);
                this.data2 = (short)((int)(var1 >> 16 & 65535L));
                this.data3 = (short)((int)(var1 & 65535L));
                this.data4 = new byte[8];

                for (int var5 = 0; var5 < 8; ++var5)
                {
                    this.data4[var5] = (byte)((int)(var3 >> 8 * (7 - var5) & 255L));
                }

            }


            public override bool Equals(Object var1)
            {
                if (!(var1 is UUID))
                {
                    return base.Equals(var1);
                }
                else
                {
                    UUID var2 = (UUID)var1;
                    if (this.data1 == var2.data1 && this.data2 == var2.data2 && this.data3 == var2.data3)
                    {
                        for (int var3 = 0; var3 < this.data4.Length; ++var3)
                        {
                            if (this.data4[var3] != var2.data4[var3])
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public int hashCode()
            {
                return this.data1;
            }

            public override String ToString()
            {
                return "UUID: " + hex(this.data1) + "-" + hex(this.data2) + "-" + hex(this.data3) + "-" + hex(this.data4);
            }

            public void initWithString(String var1)
            {
                var1 = var1.Replace("-", "");
                if (var1.Length != 32)
                {
                    throw new Exception("wrong input string for UUID : " + var1);
                }
                else
                {
                    String var2 = var1.Substring(0, 8);
                    String var3 = var1.Substring(8, 4);
                    String var4 = var1.Substring(12, 4);
                    String var5 = var1.Substring(16, 16);

                    try
                    {
                        this.data1 = hexStringToInt(var2);
                        this.data2 = hexStringToShort(var3);
                        this.data3 = hexStringToShort(var4);
                        this.data4 = hexStringToBytes(var5);
                        Console.WriteLine(this.data4);
                    }
                    catch (FormatException var7)
                    {
                        Console.WriteLine(var7.StackTrace);
                    }

                }
            }

            //public void write(OutputStream var1, CRC32 var2) 
            //{
            //    Rhino3dmExporter.writeInt32(var1, this.data1, var2);
            //Rhino3dmExporter.writeInt16(var1, this.data2, var2);
            //Rhino3dmExporter.writeInt16(var1, this.data3, var2);
            //Rhino3dmExporter.writeBytes(var1, this.data4, var2);
            //}

            public static byte[] hexStringToBytes(String var0)
            {
                byte[] var1 = new byte[var0.Length / 2];

                for (int var2 = 0; var2 < var0.Length / 2; ++var2)
                {
                    var1[var2] = hexStringToByte(var0.Substring(var2 * 2,2));
                }

                return var1;
            }

            public static int hexStringToInt(String var0)
            {
                if (var0.Length != 8)
                {
                    throw new FormatException("invalid string \"" + var0 + "\"");
                }
                else
                {
                    int var1 = 0;

                    for (int var2 = 0; var2 < var0.Length; ++var2)
                    {
                        var1 <<= 4;
                        var1 |= charToByte(var0[var2]);
                    }

                    return var1;
                }
            }

            public static short hexStringToShort(String var0)
            {
                if (var0.Length != 4)
                {
                    throw new FormatException("invalid string \"" + var0 + "\"");
                }
                else
                {
                    short var1 = 0;

                    for (int var2 = 0; var2 < var0.Length; ++var2)
                    {
                        var1 = (short)(var1 << 4);
                        var1 = (short)(var1 | charToByte(var0[var2]));
                    }

                    return var1;
                }
            }

            public static byte hexStringToByte(String var0)
            {
                if (var0.Length != 2)
                {
                    throw new FormatException("invalid string \"" + var0 + "\"");
                }
                else
                {
                    return (byte)(charToByte(var0[0]) << 4 | charToByte(var0[1]));
                }
            }

            public static byte charToByte(char var0)
            {
                if (var0 >= '0' && var0 <= '9')
                {
                    return (byte)(var0 - 48);
                }
                else if (var0 >= 'a' && var0 <= 'f')
                {
                    return (byte)(var0 - 97 + 10);
                }
                else if (var0 >= 'A' && var0 <= 'F')
                {
                    return (byte)(var0 - 65 + 10);
                }
                else
                {
                    throw new FormatException("invalid character \"" + var0 + "\"");
                }
            }
        }

        public class Rhino3dmFile
        {
            public int version;
            public int openNurbsVersion;
            //public Rhino3dm.StartSection startSection;
            //public Rhino3dm.Properties properties;
            //public Rhino3dm.Settings settings;
            //public Rhino3dm.Bitmap[] bitmaps;
            //public Rhino3dm.TextureMapping[] textureMappings;
            //public List<Rhino3dm.Material> materials;
            //public List<IMaterial> imaterials;
            //public Rhino3dm.Linetype[] linetypes;
            //public Rhino3dm.Layer[] layers;
            //public Rhino3dm.Group[] groups;
            //public Rhino3dm.Font[] fonts;
            //public Rhino3dm.DimStyle[] dimStyles;
            //public Rhino3dm.Light[] lights;
            //public Rhino3dm.HatchPattern[] hatchPatterns;
            //public Rhino3dm.InstanceDefinition[] instanceDefinitions;
            public Rhino3dm.RhinoObject[] rhinoObjects;
            //public Rhino3dm.HistoryRecord[] historyRecords;
            //public Rhino3dm.UserData[] userData;
            //public IServerI server;

            //public void setServer(IServerI var1)
            //{
            //    this.server = var1;
            //}

            public Rhino3dmFile()
            {

            }
            public Rhino3dmFile(int var1, int var2)
            {
                this.version = var1;
                this.openNurbsVersion = var2;
                //this.server = var3;
                //this.settings = new Rhino3dm.Settings(var3);
            }
            //public Rhino3dmFile(int var1, int var2, IServerI var3)
            //{
            //    this.version = var1;
            //    this.openNurbsVersion = var2;
            //    this.server = var3;
            //    this.settings = new Rhino3dm.Settings(var3);
            //}

            public int sizeOfChunkLength()
            {
                return this.version < 50 ? 4 : 8;
            }
        }



        public class UserData : RhinoObject
        {
            public static readonly String uuid = "850324A7-050E-11d4-BFFA-0010830122F0";

            public UserData()
            {
            }

            public UUID getClassUUID()
            {
                return new UUID("850324A7-050E-11d4-BFFA-0010830122F0");
            }

            //public void writeBody(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //}

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //    Rhino3dmExporter.writeChunkVersion(var2, 2, 2, var3);
            //    ChunkOutputStream var4 = new ChunkOutputStream(196601);
            //    Rhino3dmExporter.writeUUID(var4, this.getClassUUID(), var4.getCRC());
            //    UUID var5 = this.getClassUUID();
            //    Rhino3dmExporter.writeUUID(var4, var5, var4.getCRC());
            //    byte var6 = 1;
            //    Rhino3dmExporter.writeInt(var4, var6, var4.getCRC());
            //    Xform var7 = new Xform();
            //    var7.identity();
            //    Rhino3dmExporter.writeXform(var4, var7, var4.getCRC());
            //    UUID var8 = UUID.randomUUID();
            //    Rhino3dmExporter.writeUUID(var4, var8, var4.getCRC());
            //    bool var9 = false;
            //    Rhino3dmExporter.writeBool(var4, var9, var4.getCRC());
            //    int var10 = var1.version;
            //    Rhino3dmExporter.writeInt(var4, var10, var4.getCRC());
            //    int var11 = var1.openNurbsVersion;
            //    Rhino3dmExporter.writeInt(var4, var11, var4.getCRC());
            //    Rhino3dmExporter.writeChunk(var2, var4.getChunk());
            //    ChunkOutputStream var12 = new ChunkOutputStream(1073774592);
            //    this.writeBody(var1, var12, var12.getCRC());
            //    Rhino3dmExporter.writeChunk(var2, var12.getChunk());
            //}
        }

        public class Chunk
        {
            public int header;
            public int body;
            public byte[] content = null;
            public List<byte[]> contents = null;
            public CRC32 crc;

            public Chunk(int var1, int var2)
            {
                this.header = var1;
                this.body = var2;
            }

            public Chunk(int var1, int var2, byte[] var3, CRC32 var4)
            {
                this.header = var1;
                this.body = var2;
                this.content = var3;
                this.crc = var4;
            }

            public Chunk(int var1, int var2, byte[] var3)
            {
                this.header = var1;
                this.body = var2;
                this.content = var3;
                if (this.doCRC())
                {
                    this.crc = new CRC32();
                    this.crc.Update(this.content);
                }

            }

            public Chunk(int var1, byte[] var2, CRC32 var3)
            {
                this.header = var1;
                this.content = var2;
                this.body = this.content.Length;
                this.crc = var3;
            }

            public Chunk(int var1, byte[] var2)
            {
                this.header = var1;
                this.content = var2;
                this.body = this.content.Length;
                if (this.doCRC())
                {
                    this.crc = new CRC32();
                    this.crc.Update(this.content);
                }

            }

            public Chunk(int var1, String var2)
            {
                this.header = var1;
                this.content = Encoding.Default.GetBytes(var2);
                this.body = this.content.Length;
                if (this.doCRC())
                {
                    this.crc = new CRC32();
                    this.crc.Update(this.content);
                }

            }

            public Chunk(int var1, int var2, int var3)
            {
                this.header = var1;
                this.body = var2;
                this.content = System.BitConverter.GetBytes(var3);
                if (this.doCRC())
                {
                    this.crc = new CRC32();
                    this.crc.Update(this.content);
                }

            }

            public Chunk(int var1, int var2, List<byte[]> var3, CRC32 var4)
            {
                this.header = var1;
                this.body = var2;
                this.contents = var3;
                this.crc = var4;
            }

            public Chunk(int var1, int var2, List<byte[]> var3)
            {
                this.header = var1;
                this.body = var2;
                this.contents = var3;
                if (this.doCRC())
                {
                    this.crc = new CRC32();

                    for (int var4 = 0; var3 != null && var4 < var3.Count; ++var4)
                    {
                        this.crc.Update((byte[])var3[var4]);
                    }
                }

            }

            public Chunk(int var1, List<byte[]> var2, CRC32 var3)
            {
                this.header = var1;
                this.contents = var2;
                this.body = 0;

                for (int var4 = 0; var2 != null && var4 < var2.Count; ++var4)
                {
                    this.body += ((byte[])var2[var4]).Length;
                }

                this.crc = var3;
            }

            public Chunk(int var1, List<byte[]> var2)
            {
                this.header = var1;
                this.contents = var2;
                this.body = 0;

                int var3;
                for (var3 = 0; var2 != null && var3 < var2.Count; ++var3)
                {
                    this.body += ((byte[])var2[var3]).Length;
                }

                if (this.doCRC())
                {
                    this.crc = new CRC32();

                    for (var3 = 0; var2 != null && var3 < var2.Count; ++var3)
                    {
                        this.crc.Update((byte[])var2[var3]);
                    }
                }

            }

            public void setContentLength()
            {
                if (this.content != null)
                {
                    this.body = this.content.Length;
                }
                else
                {
                    this.body = 0;
                }

            }

            public int getHeader()
            {
                return this.header;
            }

            public int getBody()
            {
                return this.body;
            }

            public byte[] getContent()
            {
                return this.content;
            }

            public int contentLength()
            {
                if (this.contents == null)
                {
                    return this.content != null ? this.content.Length : 0;
                }
                else
                {
                    int var1 = 0;

                    for (int var2 = 0; var2 < this.contents.Count; ++var2)
                    {
                        var1 += ((byte[])this.contents[var2]).Length;
                    }

                    return var1;
                }
            }

            public List<byte[]> getContents()
            {
                return this.contents;
            }

            public bool isShort()
            {
                return (this.header & -2147483648) != 0;
            }

            public bool doCRC()
            {
                return (this.header & '耀') != 0;
            }

            public bool isEndOfTable()
            {
                return this.header == -1;
            }

            public void setCRC(CRC32 var1)
            {
                this.crc = var1;
            }

            public int getCRC()
            {
                return (int)this.crc.Value;
            }

            public override String ToString()
            {
                String var1 = "header: " + hex(this.header) + "\nbody: " + hex(this.body) + "\n";
                if (this.content != null)
                {
                    var1 = var1 + "length: " + this.content.Length + "\ncontent: " + hex(this.content) + "\ncontent: " + asciiOrHex(this.content) + "\n";
                }

                return var1;
            }

            public void clear()
            {
                this.content = null;
            }
        }


        public enum Endian
        {
            LittleEndian,
            BigEndian
        }

        public static String hex(int var0)
        {
            String var1 = "";

            for (int var2 = 0; var2 < 4; ++var2)
            {
                var1 = var1 + hex((byte)(var0 >> (3 - var2) * 8 & 255));
            }

            return var1;
        }
        public static String asciiOrHex(byte[] var0)
        {
            String var1 = "";

            for (int var2 = 0; var2 < var0.Length; ++var2)
            {
                if ((var0[var2] <= 32 || var0[var2] >= 127) && var0[var2] != 9 && var0[var2] != 10 && var0[var2] != 13)
                {
                    var1 = var1 + hex(var0[var2]);
                }
                else
                {
                    var1 = var1 + (char)var0[var2];
                }
            }

            return var1;
        }
        public static String hex(byte[] var0)
        {
            String var1 = "";

            for (int var2 = 0; var2 < var0.Length; ++var2)
            {
                var1 = var1 + hex(var0[var2]);
            }

            return var1;
        }
        public static String hex(byte var0)
        {
            String var1 = "";

            for (int var2 = 1; var2 >= 0; --var2)
            {
                int var3 = var0 >> var2 * 4 & 15;
                if (var3 < 10)
                {
                    var1 = var1 + ((char)(48 + var3)).ToString();
                }
                else
                {
                    var1 = var1 + ((char)(65 + var3 - 10)).ToString();
                }
            }

            return var1;
        }

    }
}
