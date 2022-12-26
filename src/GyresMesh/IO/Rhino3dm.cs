using Hsy.Core;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.HsMath;
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
                Console.WriteLine(this.attributes);
            }

            public virtual IObject createIObject(Rhino3dmFile var1)
            {
                return null;
            }

            public void setAttributesToIObject(Rhino3dmFile var1, IObject var2)
            {
                if (this.attributes == null)
                {
                    throw new Exception("no attributes is set");
                }
                else
                {
                    ILayer var3 = null;
                    //if (this.attributes.name != null)
                    //{
                    //    var2.name(this.attributes.name);
                    //    Console.WriteLine(10 + " object name : " + this.attributes.name);
                    //}

                    if (var1 != null && var1.layers != null && this.attributes.layerIndex >= 0 && this.attributes.layerIndex < var1.layers.Length)
                    {
                        var3 = var1.layers[this.attributes.layerIndex].ilayer;
                        var3.add(var2);
                        Console.WriteLine(10 + " layer name : " + var3.name());
                        if (!var3.isVisible())
                        {
                            var2.hide();
                        }
                    }

                    if (this.attributes.colorSource == 1 && this.attributes.color != null)
                    {
                        var2.setColor(this.attributes.color);
                        Console.WriteLine(10 + " set object color : " + this.attributes.color);
                        Console.WriteLine(10 + " set object color : <" + this.attributes.color.getRed() + "," + this.attributes.color.getGreen() + "," + this.attributes.color.getBlue() + "," + this.attributes.color.getAlpha() + ">");
                    }
                    else if (var3 != null)
                    {
                        var2.setColor(var3.getColor());
                        Console.WriteLine(10 + " set layer color : " + var3.getColor());
                    }

                    if (!this.attributes.visible)
                    {
                        var2.hide();
                    }

                    Console.WriteLine(10 + " object color : " + var2.getColor());
                }
            }
        }
        public class MappingChannel
        {
            public UUID mappingId;
            public int mappingIndex;
            public int mappingChannelId;
            public Xform objectXform;

            public MappingChannel()
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.content == null)
                {
                    throw new Exception("chunk content is null");
                    throw new IOException("chunk content is null");
                }
                else
                {
                    MemoryStream var4 = new MemoryStream(var3.content);
                    int var5 = I3dmImporter.readInt(var4);
                    int var6 = I3dmImporter.readInt(var4);
                    if (var5 != 1)
                    {
                        throw new Exception("invalid majorVersion : " + var5);
                        throw new IOException("invalid major version" + var5.ToString());
                    }
                    else
                    {
                        this.mappingChannelId = I3dmImporter.readInt(var4);
                        this.mappingId = I3dmImporter.readUUID(var4);
                        if (var6 >= 1)
                        {
                            this.objectXform = I3dmImporter.readXform(var4);
                            if (var1.openNurbsVersion < 200610030 && this.objectXform.isZero())
                            {
                                this.objectXform.identity();
                            }
                        }

                    }
                }
            }
        }
        public class MaterialRef
        {
            public UUID pluginId;
            public UUID materialId;
            public UUID materialBackfaceId;
            public int materialSource = 0;
            public char reserved1;
            public char reserved2;
            public char reserved3;
            public int materialIndex = -1;
            public int materialBackfaceIndex = -1;

            public MaterialRef()
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.content == null)
                {
                    throw new Exception("chunk content is null");
                    throw new IOException("chunk content is null");
                }
                else
                {
                    MemoryStream var4 = new MemoryStream(var3.content);
                    int var5 = I3dmImporter.readInt(var4);
                    int var6 = I3dmImporter.readInt(var4);
                    if (var5 != 1)
                    {
                        throw new Exception("invalid major version : " + var5);
                        throw new IOException("invalid major version" + var5.ToString());
                    }
                    else
                    {
                        this.pluginId = I3dmImporter.readUUID(var4);
                        this.materialId = I3dmImporter.readUUID(var4);
                        int var7 = I3dmImporter.readInt(var4);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            MappingChannel var9 = new MappingChannel();
                            var9.read(var1, var4);
                        }

                        if (var6 >= 1)
                        {
                            this.materialBackfaceId = I3dmImporter.readUUID(var4);
                            this.materialSource = I3dmImporter.readInt(var4);
                        }

                    }
                }
            }

            //        public void write(Rhino3dmFile var1, Stream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592, 1, 1);
            //IRhino3dmExporter.writeUUID(var4, this.pluginId, var4.getCRC());
            //IRhino3dmExporter.writeUUID(var4, this.materialId, var4.getCRC());
            //IRhino3dmExporter.writeInt32(var4, 0, var4.getCRC());
            //IRhino3dmExporter.writeUUID(var4, this.materialBackfaceId, var4.getCRC());
            //IRhino3dmExporter.writeInt32(var4, this.materialSource, var4.getCRC());
            //IRhino3dmExporter.writeChunk(var2, var4.getChunk());
            //        }
        }


        public class RenderingAttributes
        {
            public List<MaterialRef> materials = new List<MaterialRef>();

            public RenderingAttributes()
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3 != null && var3.content != null)
                {
                    MemoryStream var4 = new MemoryStream(var3.content);
                    int var5 = I3dmImporter.readInt(var4);
                    int var6 = I3dmImporter.readInt(var4);
                    if (var5 != 1)
                    {
                        throw new IOException("invalid major version : " + var5.ToString());
                    }
                    else
                    {
                        int var7 = I3dmImporter.readInt(var4);
                        //var7 = I3dmImporter.readInt(var4);
                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            MaterialRef var9 = new MaterialRef();
                            var9.read(var1, var4);
                            this.materials.Add(var9);
                        }

                    }
                }
                else
                {
                    throw new Exception("no chunk content");
                    throw new IOException("no chunk content");
                }
            }

            //public void write(Rhino3dmFile var1, Stream var2, CRC32 var3)
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592, 1, 0);
            //    int var5 = 0;
            //    if (this.materials != null)
            //    {
            //        var5 = this.materials.size();
            //    }

            //    IRhino3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //    for (int var6 = 0; var6 < var5; ++var6)
            //    {
            //        ((MaterialRef)this.materials.get(var6)).write(var1, var4, var4.getCRC());
            //    }

            //    IRhino3dmExporter.writeChunk(var2, var4.getChunk());
            //}
        }
        public class DisplayMaterialRef
        {
            public UUID viewportId;
            public UUID displayMaterialId;

            public DisplayMaterialRef()
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                this.viewportId = I3dmImporter.readUUID(var2);
                this.displayMaterialId = I3dmImporter.readUUID(var2);
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    IRhino3dmExporter.writeUUID(var2, this.viewportId, var3);
            //IRhino3dmExporter.writeUUID(var2, this.displayMaterialId, var3);
            //}
        }
        public class ObjectAttributes : RhinoObject
        {
            public new static readonly String uuid = "A828C015-09F5-477c-8665-F0482F5D6996";
            public static readonly UUID obsoletePageSpaceObjectId = new UUID(-1682229271, (short)8497, (short)20408, IntArrToByteArr(new int[] { -71, -58, 85, 36, -123, -101, -104, -72 }));
            public UUID objectUUID;
            public String name;
            public String url;
            public int layerIndex;
            public int linetypeIndex;
            public int materialIndex;
            public RenderingAttributes renderingAttributes;
            public IColor color;
            public IColor plotColor;
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
            public List<DisplayMaterialRef> dmref;

            public new UUID getClassUUID()
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
                this.renderingAttributes = new RenderingAttributes();
                this.color = new IColor(0, 0, 0);
                this.plotColor = new IColor(0, 0, 0);
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
                this.dmref = null;
            }

            public ObjectAttributes(IObject var1, Rhino3dmFile var2)
            {
                this.objectUUID = UUID.randomUUID();
                this.name = var1.name();
                this.url = null;
                //if (var1.layer() != null && var1.server != null && var1.server.layers != null)
                //{
                //    this.layerIndex = var1.server.layers.indexOf(var1.layer());
                //}
                //else
                //{
                //    this.layerIndex = 0;
                //}

                this.linetypeIndex = -1;
                //if (var1.attr() != null && var1.attr().material != null && var2.imaterials != null)
                //{
                //    this.materialIndex = var2.imaterials.indexOf(var1.attr().material);
                //}
                //else
                //{
                //    this.materialIndex = -1;
                //}

                this.renderingAttributes = new RenderingAttributes();
                this.color = var1.getColor();
                this.plotColor = var1.getColor();
                this.displayOrder = 0;
                this.plotWeightMm = 0.0D;
                this.objectDecoration = 0;
                this.wireDensity = 1;
                this.viewportId = null;
                this.activeSpace = 1;
                this.visible = var1.visible();
                this.mode = 0;
                if (this.color != null)
                {
                    this.colorSource = 1;
                }
                else
                {
                    this.colorSource = 0;
                }

                this.plotColorSource = 0;
                this.plotWeightSource = 0;
                if (this.materialIndex >= 0)
                {
                    this.materialSource = 1;
                }
                else
                {
                    this.materialSource = 0;
                }

                this.linetypeSource = 0;
                this.group = null;
                this.dmref = null;
            }

            public void readV5(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                bool var6 = true;
                this.objectUUID = I3dmImporter.readUUID(var2);
                this.layerIndex = I3dmImporter.readInt(var2);
                byte var10 = I3dmImporter.readByte(var2);
                if (var10 != 0)
                {
                    if (var10 == 1)
                    {
                        this.name = I3dmImporter.readString(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 2)
                    {
                        this.url = I3dmImporter.readString(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 3)
                    {
                        this.linetypeIndex = I3dmImporter.readInt(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 4)
                    {
                        this.materialIndex = I3dmImporter.readInt(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 5)
                    {
                        this.renderingAttributes.read(var1, var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 6)
                    {
                        this.color = I3dmImporter.readColor(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 7)
                    {
                        this.plotColor = I3dmImporter.readColor(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 8)
                    {
                        this.plotWeightMm = I3dmImporter.readDouble(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 9)
                    {
                        this.objectDecoration = (short)I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 10)
                    {
                        this.wireDensity = I3dmImporter.readInt(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 11)
                    {
                        this.visible = I3dmImporter.readBool(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 12)
                    {
                        this.mode = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 13)
                    {
                        this.colorSource = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 14)
                    {
                        this.plotColorSource = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 15)
                    {
                        this.plotWeightSource = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 16)
                    {
                        this.materialSource = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 17)
                    {
                        this.linetypeSource = I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 18)
                    {
                        this.group = I3dmImporter.readArrayInt(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 19)
                    {
                        this.activeSpace = (short)I3dmImporter.readByte(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 20)
                    {
                        this.viewportId = I3dmImporter.readUUID(var2);
                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var10 == 21)
                    {
                        int var7 = I3dmImporter.readInt(var2);
                        this.dmref = new List<DisplayMaterialRef>(var7);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            DisplayMaterialRef var9 = new DisplayMaterialRef();
                            var9.read(var1, var2);
                            this.dmref.Add(var9);
                        }

                        var10 = I3dmImporter.readByte(var2);
                    }

                    if (var5 >= 1)
                    {
                        if (var10 == 22)
                        {
                            this.displayOrder = I3dmImporter.readInt(var2);
                            var10 = I3dmImporter.readByte(var2);
                        }

                        if (var5 >= 2)
                        {
                        }
                    }

                }
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {

                if (var1.version >= 5 && var1.openNurbsVersion >= 200712190)
                {
                    this.readV5(var1, var2);
                    Console.WriteLine(5);
                }
                //if (var1.version >= 6 && var1.openNurbsVersion <= -1912309923)
                //{
                //    this.readV5(var1, var2);
                //}
                else
                {
                    //var2.Position += 4;
                    int[] var3 = I3dmImporter.readChunkVersion(var2);
                    int var4 = var3[0];
                    int var5 = var3[1];
                    if (var4 != 1)
                    {
                        throw new Exception("wrong major version " + var4);
                        throw new IOException("wrong major version " + var4);
                    }
                    else
                    {
                        this.objectUUID = I3dmImporter.readUUID(var2);
                        this.layerIndex = I3dmImporter.readInt(var2);
                        this.materialIndex = I3dmImporter.readInt(var2);
                        this.color = I3dmImporter.readColor(var2);
                        short var6 = I3dmImporter.readShort(var2);
                        if (var1.version < 4 || var1.openNurbsVersion < 200503170)
                        {
                            this.objectDecoration = (short)(var6 & 24);
                        }

                        I3dmImporter.readShort(var2);
                        I3dmImporter.readDouble(var2);
                        I3dmImporter.readDouble(var2);
                        this.wireDensity = I3dmImporter.readInt(var2);
                        this.mode = I3dmImporter.readByte(var2);
                        this.colorSource = I3dmImporter.readByte(var2);
                        this.linetypeSource = I3dmImporter.readByte(var2);
                        this.materialSource = I3dmImporter.readByte(var2);
                        this.name = I3dmImporter.readString(var2);
                        this.url = I3dmImporter.readString(var2);
                        this.visible = (this.mode & 15) != 1;
                        if (var5 >= 1)
                        {
                            this.group = I3dmImporter.readArrayInt(var2);
                            if (var5 >= 2)
                            {
                                this.visible = I3dmImporter.readBool(var2);
                                if (var5 >= 3)
                                {
                                    int var7 = I3dmImporter.readInt(var2);
                                    this.dmref = new List<DisplayMaterialRef>(var7);

                                    for (int var8 = 0; var8 < var7; ++var8)
                                    {
                                        DisplayMaterialRef var9 = new DisplayMaterialRef();
                                        var9.read(var1, var2);
                                        this.dmref.Add(var9);
                                    }

                                    if (var5 >= 4)
                                    {
                                        this.objectDecoration = (short)(I3dmImporter.readInt(var2) & 255);
                                        this.plotColorSource = I3dmImporter.readByte(var2);
                                        this.plotColor = I3dmImporter.readColor(var2);
                                        this.plotWeightSource = I3dmImporter.readByte(var2);
                                        this.plotWeightMm = I3dmImporter.readDouble(var2);
                                        if (var5 >= 5)
                                        {
                                            this.linetypeIndex = I3dmImporter.readInt(var2);
                                            if (var5 >= 6)
                                            {
                                                byte var11 = I3dmImporter.readByte(var2);
                                                this.activeSpace = (short)(var11 == 1 ? 2 : 1);
                                                this.dmref.Clear();
                                                var7 = I3dmImporter.readInt(var2);

                                                for (int var12 = 0; var12 < var7; ++var12)
                                                {
                                                    DisplayMaterialRef var10 = new DisplayMaterialRef();
                                                    var10.viewportId = I3dmImporter.readUUID(var2);
                                                    if (obsoletePageSpaceObjectId.Equals(var10.viewportId))
                                                    {
                                                        this.viewportId = var10.viewportId;
                                                    }
                                                    else
                                                    {
                                                        this.dmref.Add(var10);
                                                    }
                                                }

                                                if (var5 >= 7)
                                                {
                                                    this.renderingAttributes.read(var1, var2);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

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
            //        Rhino3dmExporter.writedouble(var2, this.plotWeightMm, var3);
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

            //            if (this.group != null && this.group.Count > 0) {
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

            //            if (this.dmref != null && this.dmref.Count > 0) {
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
            //        Rhino3dmExporter.writedouble(var2, 0.0D, var3);
            //        Rhino3dmExporter.writedouble(var2, 1.0D, var3);
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
            //        Rhino3dmExporter.writedouble(var2, this.plotWeightMm, var3);
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
            //            var6 = this.dmref.Count;
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
                put("4ED7D4D7-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Curve));
                put("4ED7D4E1-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Surface));
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
                put("60B5DBC0-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepVertex));
                put("60B5DBC1-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepEdge));
                put("60B5DBC2-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepTrim));
                put("60B5DBC3-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepLoop));
                put("60B5DBC4-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.BrepFace));
                put("60B5DBC5-E660-11d3-BFE4-0010830122F0", typeof(Rhino3dm.Brep));
                //put("7FE23D63-E536-43f1-98E2-C807A2625AFF", typeof(Rhino3dm.BrepRegionTopologyUserData));
                //put("30930370-0D5B-4ee4-8083-BD635C7398A4", typeof(Rhino3dm.BrepFaceSide));
                //put("CA7A0092-7EE6-4f99-B9D2-E1D6AA798AA1", typeof(Rhino3dm.BrepRegion));
                put("4ED7D4D8-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.CurveOnSurface));
                //put("C8C66EFA-B3CB-4e00-9440-2AD66203379E", typeof(Rhino3dm.DetailView));
                //put("513FDE53-7284-4065-8601-06CEA8B28D6F", typeof(Rhino3dm.DimStyleExtra));
                //put("3FF7007C-3D04-463f-84E3-132ACEB91062", typeof(Rhino3dm.HatchExtra));
                //put("0559733B-5332-49d1-A936-0532AC76ADE5", typeof(Rhino3dm.Hatch));
                //put("F9CFB638-B9D4-4340-87E3-C56E7865D96A", typeof(Rhino3dm.InstanceRef));
                //put("3E4904E6-E930-4fbc-AA42-EBD407AEFE3B", typeof(Rhino3dm.LayerExtensions));
                put("4ED7D4D9-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.CurveProxy));
                put("4ED7D4DB-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.LineCurve));
                put("11D3E947-E5BF-1000-8301-22F0F49BE923", typeof(Rhino3dm.LineCurve));
                put("4ED7D4E4-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Mesh));
                //put("4ED7D4E4-FFFFE947-000011D3-BFE50010830122F0", typeof(Rhino3dm.Mesh));
                //put("C547B4BD-BDCD-49b6-A983-0C4A7F02E31A", typeof(Rhino3dm.MeshVertexRef));
                //put("ED727872-463A-4424-851F-9EC02CB0F155", typeof(Rhino3dm.MeshEdgeRef));
                //put("4F529AA5-EF8D-4c25-BCBB-162D510AA280", typeof(Rhino3dm.MeshFaceRef));
                //put("31F55AA3-71FB-49f5-A975-757584D937FF", typeof(Rhino3dm.MeshNgonUserData));
                put("4ED7D4DD-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                put("4ED7D4DE-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                //put("06936AFB-3D3C-41ac-BF70-C9319FA480A1", typeof(Rhino3dm.NurbsCage));
                //put("D379E6D8-7C31-4407-A913-E3B7040D034A", typeof(Rhino3dm.MorphControl));
                //put("00C61749-D430-4ecc-83A8-29130A20CF9C", typeof(Rhino3dm.OffsetSurface));
                put("4ED7D4DF-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PlaneSurface));
                put("11D3E947-E5BF-1000-8301-22F073D7D8F7", typeof(Rhino3dm.PlaneSurface));
                //put("DBC5A584-CE3F-4170-98A8-497069CA5C36", typeof(Rhino3dm.ClippingPlaneSurface));
                //put("2488F347-F8FA-11d3-BFEC-0010830122F0", typeof(Rhino3dm.PointCloud));
                put("C3101A1D-F157-11d3-BFE7-0010830122F0", typeof(Rhino3dm.Point));
                //put("4ED7D4E5-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PointGrid));
                put("4ED7D4E0-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PolyCurve));
                //put("42F47A87-5B1B-4e31-AB87-4639D78325D6", typeof(Rhino3dm.PolyEdgeSegment));
                //put("39FF3DD3-FE0F-4807-9D59-185F0D73C0E4", typeof(Rhino3dm.PolyEdgeCurve));
                put("4ED7D4E6-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.PolylineCurve));
                //put("A16220D3-163B-11d4-8000-0010830122F0", typeof(Rhino3dm.RevSurface));
                put("C4CD5359-446D-4690-9FF5-29059732472B", typeof(Rhino3dm.SumSurface));
                put("4ED7D4E2-E947-11d3-BFE5-0010830122F0", typeof(Rhino3dm.SurfaceProxy));
                //put("850324A8-050E-11d4-BFFA-0010830122F0", typeof(Rhino3dm.UnknownUserData));
                //put("D66E5CCF-EA39-11d3-BFE5-0010830122F0", typeof(Rhino3dm.Viewport));
                put("5EAF1119-0B51-11d4-BFFE-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                put("76A709D5-1550-11d4-8000-0010830122F0", typeof(Rhino3dm.NurbsCurve));
                put("11D40B51-FEBF-1000-8301-22F04A1A7917", typeof(Rhino3dm.NurbsCurve));

                put("4760C817-0BE3-11d4-BFFE-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                put("FA4FD4B5-1613-11d4-8000-0010830122F0", typeof(Rhino3dm.NurbsSurface));
                put("11D40BE3-FEBF-1000-8301-22F0AB288EBE", typeof(Rhino3dm.NurbsSurface));
                put("EF638317-154B-11d4-8000-0010830122F0", typeof(Rhino3dm.PolyCurve));
                put("0705FDEF-3E2A-11d4-800E-0010830122F0", typeof(Rhino3dm.Brep));
                put("2D4CFEDB-3E2A-11d4-800E-0010830122F0", typeof(Rhino3dm.Brep));
                put("F06FC243-A32A-4608-9DD8-A7D2C4CE2A36", typeof(Rhino3dm.Brep));
                //put("0A8401B6-4D34-4b99-8615-1B4E723DC4E5", typeof(Rhino3dm.RevSurface));
                put("665F6331-2A66-4cce-81D0-B5EEBD9B5417", typeof(Rhino3dm.SumSurface));
                put("4CCE2A66-D081-EEB5-BD9B-541757BB6668", typeof(Rhino3dm.SumSurface));
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

                //Console.WriteLine(map.Keys.ToArray()[4]);
                Type t = null;
                foreach (var key in map.Keys.ToArray())
                {
                    Console.WriteLine("key:   " + key);
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
            public new static readonly String uuid = "4ED7D4DA-E947-11d3-BFE5-0010830122F0";

            public Geometry()
            {
            }

            public new UUID getClassUUID()
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
                for (int i = 0; i < xform.Length; i++)
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
        public class TextureMapping : RhinoObject
        {
            public new static readonly String uuid = "32EC997A-C3BF-4ae5-AB19-FD572B8AD554";

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

            public new  UUID getClassUUID()
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

            public void read(Rhino3dmFile var1, Stream var2)
            {

                Chunk var3 = I3dmImporter.readChunk(var2);
                MemoryStream var6 = new MemoryStream(var3.content);
                int var4 = I3dmImporter.readInt(var6);
                int var5 = I3dmImporter.readInt(var6);
                if (var4 == 1)
                {
                    this.mappingId = I3dmImporter.readUUID(var6);
                    this.mappingCRC = I3dmImporter.readInt(var6);
                    this.meshXform = I3dmImporter.readXform(var6);
                    if (var5 >= 1)
                    {
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
            public new static String uuid = "4ED7D4E4-E947-11d3-BFE5-0010830122F0";
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

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4E4-E947-11d3-BFE5-0010830122F0");
            }
            public new int getType()
            {
                return 32;
            }
            public override IObject createIObject(Rhino3dmFile var1)
            {

                List<HS_Vector> vertices = this.vertices;
                List<HS_Point> pts = new List<HS_Point>();
                //bool[] duplicate = new bool[vertices.Count];
                //HashSet<HS_Coord> vset = new HashSet<HS_Coord>();

                foreach (HS_Vector vertex in vertices)
                {
                    HS_Point pt = new HS_Point(vertex.xd, vertex.yd, vertex.zd);

                    pts.Add(pt);
                    //duplicate[i] = !vset.Add(vertex);

                }
                List<Rhino3dm.MeshFace> faces = this.faces;
                List<int[]> faceList = new List<int[]>();
                foreach (Rhino3dm.MeshFace face in faces)
                {
                    //HS_Vector[] iVers = face.vertexIndex;
                    //int[] faceVers = new int[iVers.Length];
                    //for (int i = 0; i < iVers.Length; i++)
                    //{
                    //    //int id = vertices.IndexOf(iVers[i]);
                    //    faceVers[i] = id;
                    //}

                    faceList.Add(face.vertexIndex);
                }
                GEC_FromFaceList creator = new GEC_FromFaceList();
                creator.setVertices(pts);
                creator.setFaces(faceList);
                creator.setDuplicate(new bool[0]);
                GE_Mesh m = creator.create();
                Console.WriteLine(m);
                return m;
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
                    Console.WriteLine("Faces:  " + this.faces.Count);
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

                    if (this.surfaceParameter == null && this.vertices != null && this.vertices.Count > 0)
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
                Endian var4 = Endian.BigEndian;
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


        public abstract class Curve : Rhino3dm.Geometry
        {
            public new static readonly String uuid = "4ED7D4D7-E947-11d3-BFE5-0010830122F0";
            //public ICurveGeo icurve;
            public HS_Vector[] cv;
            public Curve()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4D7-E947-11d3-BFE5-0010830122F0");
            }

            public new int getType()
            {
                return 4;
            }

            public abstract Interval domain();

            public abstract bool isValid();

            //public IObject createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    return null;
            //}

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    return null;
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    return null;
            //}
        }
        public class LineCurve : Curve
        {
            public new static readonly String uuid = "4ED7D4DB-E947-11d3-BFE5-0010830122F0";
            public Line line;

            public Interval t;
            public int dim;

            public LineCurve()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4DB-E947-11d3-BFE5-0010830122F0");
            }

            public override Interval domain()
            {
                return this.t;
            }

            public override bool isValid()
            {
                return this.t.isIncreasing() && this.line.length() > 0.0D;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                var2.Position -= 4;
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.cv = new HS_Vector[2];
                    this.line = I3dmImporter.readLine(var2);
                    this.cv[0] = this.line.from;
                    this.cv[1] = this.line.to;
                    this.t = I3dmImporter.readInterval(var2);
                    this.dim = I3dmImporter.readInt32(var2);
                    Console.WriteLine(100 + "line = " + this.line.from + ", " + this.line.to);
                }
                else
                {
                    throw new Exception("wrong chunk major version :" + var4);
                }

            }

            //public ICurve createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.line == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        ICurve var3 = new ICurve(var2, this.line.from, this.line.to);
            //        return var3;
            //    }
            //}

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    return this.line == null ? null : new ICurveGeo(this.line.from, this.line.to);
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    return this.line == null ? null : new ITrimCurve(this.line.from, this.line.to);
            //}
        }
        public class Line
        {
            public HS_Vector from;
            public HS_Vector to;

            public Line()
            {
            }

            public double length()
            {
                return this.from.dist(this.to);
            }

            public override String ToString()
            {
                return "< " + this.from.ToString() + ", " + this.to.ToString() + " >";
            }
        }

        public class CurveOnSurface : Curve
        {
            public new static readonly String uuid = "4ED7D4D8-E947-11d3-BFE5-0010830122F0";
            public Curve curve2;
            public Curve curve3;
            public Surface surface;

            public CurveOnSurface()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4D8-E947-11d3-BFE5-0010830122F0");
            }

            public override bool isValid()
            {
                if (this.curve2 == null)
                {
                    return false;
                }
                else if (this.surface == null)
                {
                    return false;
                }
                else if (!this.curve2.isValid())
                {
                    return false;
                }
                else
                {
                    return this.curve3 == null || this.curve3.isValid();
                }
            }

            public override Interval domain()
            {
                return this.curve2.domain();
            }
        }
        public class PolylineCurve : Curve
        {
            public new static readonly String uuid = "4ED7D4E6-E947-11d3-BFE5-0010830122F0";
            public Polyline pline;
            public List<double> t;
            public int dim;

            public PolylineCurve()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4E6-E947-11d3-BFE5-0010830122F0");
            }

            public override bool isValid()
            {
                return this.pline.isValid();
            }

            public override Interval domain()
            {
                int var1 = this.pline.Count;
                return var1 >= 2 && (double)this.t[0] < (double)this.t[var1 - 1] ? new Interval((double)this.t[0], (double)this.t[var1 - 1]) : null;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.pline = I3dmImporter.readPolyline(var2);
                    this.t = I3dmImporter.readArrayDouble(var2);
                    this.dim = I3dmImporter.readInt(var2);
                }

            }

            public override IObject createIObject(Rhino3dmFile var1)
            {
                //HS_PolyLine var3 = new ICurve(var2, (HS_VectorI[])this.pline.toArray(new HS_Vector[this.pline.Count]));
                //return var3;
                return null;
            }

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    return new ICurveGeo((HS_VectorI[])this.pline.toArray(new HS_Vector[this.pline.Count]));
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    return new ITrimCurve((HS_VectorI[])this.pline.toArray(new HS_Vector[this.pline.Count]));
            //}
        }

        public class Polyline : PointArray
        {
            public bool isValid()
            {
                return true;
            }

            public Polyline()
            {
            }

            public Polyline(int var1) : base(var1)
            {

            }
        }

        public class Point2Array : List<HS_Vector>
        {
            public Point2Array()
            {
            }

            public Point2Array(int var1) : base(var1)
            {

            }
        }

        public class PointArray : List<HS_Vector>
        {
            public PointArray()
            {
            }

            public PointArray(int var1) : base(var1)
            {

            }
        }

        public class ArcCurve : Curve
        {
            public new static readonly String uuid = "CF33BE2A-09B4-11d4-BFFB-0010830122F0";
            public Arc arc;
            public Interval t;
            public int dim;

            public ArcCurve()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("CF33BE2A-09B4-11d4-BFFB-0010830122F0");
            }

            public override Interval domain()
            {
                return this.t;
            }

            public override bool isValid()
            {
                if (!this.t.isIncreasing())
                {
                    return false;
                }
                else
                {
                    return this.arc.isValid();
                }
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.arc = I3dmImporter.readArc(var2);
                    this.t = I3dmImporter.readInterval(var2);
                    this.dim = I3dmImporter.readInt(var2);
                    if (this.dim != 2 && this.dim != 3)
                    {
                        this.dim = 3;
                    }
                }

            }

            //public ICurve createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    HS_Vector var3 = this.arc.plane.origin;
            //    HS_Vector var4 = this.arc.plane.zdaxis;
            //    HS_Vector var5;
            //    if (Math.abs(this.arc.angle.Length()) < 6.283185307179586D - IConfig.angleTolerance)
            //    {
            //        var5 = this.arc.plane.xdaxis.dup().len(this.arc.radius).add(var3);
            //        var5.rot(var3, var4, this.arc.angle.v1);
            //        double var9 = this.arc.angle.v2 - this.arc.angle.v1;
            //        IArc var8 = new IArc(var2, var3, var4, var5, var9);
            //        return var8;
            //    }
            //    else
            //    {
            //        var5 = this.arc.plane.xdaxis;
            //        ICircle var6 = new ICircle(var2, var3, var4, var5, this.arc.radius);
            //        return var6;
            //    }
            //}

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    HS_Vector var3 = this.arc.plane.origin;
            //    HS_Vector var4 = this.arc.plane.zdaxis;
            //    HS_Vector var5;
            //    if (Math.abs(this.arc.angle.Length()) < 6.283185307179586D - IConfig.angleTolerance)
            //    {
            //        var5 = this.arc.plane.xdaxis.dup().len(this.arc.radius).add(var3);
            //        var5.rot(var3, var4, this.arc.angle.v1);
            //        double var9 = this.arc.angle.v2 - this.arc.angle.v1;
            //        IArcGeo var8 = new IArcGeo(var3, var4, var5, var9);
            //        return var8;
            //    }
            //    else
            //    {
            //        var5 = this.arc.plane.xdaxis;
            //        ICircleGeo var6 = new ICircleGeo(var3, var4, var5, this.arc.radius);
            //        return var6;
            //    }
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    HS_Vector var4 = this.arc.plane.origin;
            //    HS_Vector var5 = this.arc.plane.zdaxis;
            //    HS_Vector var6;
            //    if (Math.abs(this.arc.angle.Length()) < 6.283185307179586D - IConfig.angleTolerance)
            //    {
            //        var6 = this.arc.plane.xdaxis.dup().len(this.arc.radius).add(var4);
            //        var6.rot(var4, var5, this.arc.angle.v1);
            //        double var10 = this.arc.angle.v2 - this.arc.angle.v1;
            //        IArcGeo var9 = new IArcGeo(var4, var5, var6, var10);
            //        return new ITrimCurve(var9);
            //    }
            //    else
            //    {
            //        var6 = this.arc.plane.xdaxis;
            //        ICircleGeo var7 = new ICircleGeo(var4, var5, var6, this.arc.radius);
            //        return new ITrimCurve(var7);
            //    }
            //}
        }

        public class Arc : Circle
        {
            public Interval angle;

            public Arc()
            {
            }

            public new bool isValid()
            {
                return base.isValid() && this.angle.isValid() && this.angleRadians() >= 0.0D && this.angleRadians() <= 6.283185307179586D;
            }

            public double angleRadians()
            {
                return this.angle.v2 - this.angle.v1;
            }

            public Interval domain()
            {
                return this.angle;
            }
        }

        public class Circle
        {
            public Plane plane;
            public double radius;

            public Circle()
            {
            }

            public bool isValid()
            {
                return this.radius > 0.0D && this.plane.isValid();
            }
        }



        public class CurveProxy : Curve
        {
            public new static readonly String uuid = "4ED7D4D9-E947-11d3-BFE5-0010830122F0";
            public Curve realCurve;
            public bool reversed;
            public Interval realCurveDomain = new Interval();
            public Interval thisDomain = new Interval();

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4D9-E947-11d3-BFE5-0010830122F0");
            }

            public CurveProxy()
            {
            }

            public void setProxyCurve(Curve var1, Interval var2)
            {
                if (var1 != this)
                {
                    var1 = null;
                    this.realCurveDomain = null;
                    this.thisDomain = null;
                    this.reversed = false;
                }
                else
                {
                    if (this.isValid() && this.thisDomain.includes(var2))
                    {
                        var1 = this.realCurve;
                        double var3 = this.realCurveParameter(var2.v1);
                        double var5 = this.realCurveParameter(var2.v2);
                        var2.set(var3, var5);
                    }
                    else
                    {
                        var1 = null;
                    }

                    this.realCurve = null;
                }

                if (this.realCurve != null)
                {
                    this.setProxyCurveDomain(var2);
                }
                else
                {
                    this.realCurveDomain = var2;
                }

                this.thisDomain = this.realCurveDomain;
            }

            public bool setProxyCurveDomain(Interval var1)
            {
                if (!var1.isIncreasing())
                {
                    return false;
                }
                else
                {
                    if (this.realCurve != null)
                    {
                        Interval var2 = this.realCurve.domain();
                        var2.intersection(var1);
                        if (var2.isIncreasing())
                        {
                            this.realCurveDomain = var2;
                        }
                    }
                    else
                    {
                        this.realCurveDomain = var1;
                    }

                    return true;
                }
            }

            public Interval proxyCurveDomain()
            {
                return this.realCurveDomain;
            }

            public double realCurveParameter(double var1)
            {
                if (this.reversed || this.realCurveDomain.equals(this.thisDomain))
                {
                    double var3 = this.thisDomain.normalizedParameterAt(var1);
                    if (this.reversed)
                    {
                        var3 = 1.0D - var3;
                    }

                    var1 = this.realCurveDomain.parameterAt(var3);
                }

                return var1;
            }

            public void setDomain(Interval var1)
            {
                if (var1.isIncreasing())
                {
                    this.thisDomain.set(var1);
                }

            }


            public override Interval domain()
            {
                return this.thisDomain;
            }
            public bool reverse()
            {
                if (this.thisDomain.isIncreasing())
                {
                    this.reversed = !this.reversed;
                    this.thisDomain.reverse();
                }

                return true;
            }

            public bool proxyCurveIsReversed()
            {
                return this.reversed;
            }
            public override bool isValid()
            {
                throw new NotImplementedException();
            }
        }
        public class PolyCurve : Curve
        {
            public new static readonly String uuid = "4ED7D4E0-E947-11d3-BFE5-0010830122F0";
            public static readonly double sqrtEpsilon = 1.490116119385E-8D;
            public CurveArray segment;
            public List<double> t;

            public PolyCurve()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4E0-E947-11d3-BFE5-0010830122F0");
            }

            public override Interval domain()
            {
                int var1 = this.segment.Count;
                return var1 > 0 && (double)this.t[0] < (double)this.t[var1] ? new Interval((double)this.t[0], (double)this.t[var1]) : null;
            }

            public Interval segmentDomain(int var1)
            {
                Interval var2 = new Interval();
                if (var1 >= 0 && var1 < this.count())
                {
                    var2.v1 = (double)this.t[var1];
                    var2.v2 = (double)this.t[var1 + 1];
                }

                return var2;
            }

            public override bool isValid()
            {
                return this.isValid(false);
            }

            public bool isValid(bool var1)
            {
                for (int var2 = 0; var2 < this.segment.Count; ++var2)
                {
                    if (!((Curve)this.segment[var2]).isValid())
                    {
                        return false;
                    }
                }

                return true;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                int var6 = I3dmImporter.readInt(var2);
                int var7 = I3dmImporter.readInt(var2);
                int var8 = I3dmImporter.readInt(var2);
                BoundingBox var9 = I3dmImporter.readBoundingBox(var2);
                this.t = I3dmImporter.readArrayDouble(var2);
                this.segment = new CurveArray(var6);

                for (int var10 = 0; var10 < var6; ++var10)
                {
                    RhinoObject var11 = I3dmImporter.readObject(var1, var2);
                    if (var11 != null)
                    {
                        if (!(var11 is Curve))
                        {
                            throw new IOException("invalid class of instance: " + var11);
                        }

                        Curve var12 = (Curve)var11;
                        this.segment.Add(var12);
                    }
                }

                if (this.segment.Count == 0)
                {
                    throw new IOException("number of segemnts is zero");
                }
                else if (this.segment.Count != var6)
                {
                    throw new IOException("number of segemnts doesn't match with count");
                }
                else if (this.t.Count != var6 + 1)
                {
                    throw new IOException("number of domain doesn't match with number of segment");
                }
                else
                {
                    Interval var23 = null;
                    Interval var24 = ((Curve)this.segment[0]).domain();
                    double var25 = 0.0D;
                    double var14 = var24.length();
                    double var16 = 0.0D;
                    double var18 = 0.0D;
                    double var20 = 0.0D;

                    for (int var22 = 1; var22 < var6; ++var22)
                    {
                        var18 = (double)this.t[var22];
                        var23 = var24;
                        var25 = var14;
                        var24 = ((Curve)this.segment[var22]).domain();
                        if (var24 == null)
                        {
                        }

                        var14 = var24.length();
                        var20 = var23.v2;
                        if (var20 != var18 && var20 == var24.v1 && var18 > var23.v1 && var18 < var24.v2)
                        {
                            var16 = (var25 <= var14 ? var25 : var14) * 1.490116119385E-8D;
                            if (Math.Abs(var18 - var20) <= var16)
                            {
                                //this.t.set(var22, var20);
                                this.t[var22] = var20;
                            }
                        }
                    }

                    var16 = var14 * 1.490116119385E-8D;
                    var18 = (double)this.t[var6];
                    var20 = var24.v2;
                    if (var20 != var18 && var18 > var24.v1 && Math.Abs(var20 - var18) <= var16)
                    {
                        //this.t.set(var6, var20);
                        this.t[var6] = var20;
                    }

                    if (var1.openNurbsVersion < 200304080)
                    {
                        this.removeNesting();
                    }

                }
            }

            public int count()
            {
                return this.segment == null ? 0 : this.segment.Count;
            }

            public Curve segmentCurve(int var1)
            {
                return (Curve)this.segment[var1];
            }

            public void removeNesting()
            {
                int var1 = this.count();
                List<double> var2 = this.t;
                CurveArray var3 = this.segment;
                this.t = new List<double>();
                this.t.Add(var2[0]);
                this.segment = new CurveArray();

                for (int var4 = 0; var4 < var1; ++var4)
                {
                    if (var3[var4] is PolyCurve)
                    {
                        PolyCurve var5 = (PolyCurve)var3[var4];
                        this.flatten(var5, new Interval((double)var2[var4], (double)var2[var4 + 1]), this.t, this.segment);
                    }
                    else
                    {
                        this.t.Add(var2[var4 + 1]);
                        this.segment.Add(var3[var4]);
                    }
                }

            }

            public void flatten(PolyCurve var1, Interval var2, List<double> var3, CurveArray var4)
            {
                int var5 = var1.count();
                double var6 = var2.v1;
                Interval var8 = var1.domain();

                for (int var9 = 0; var9 < var5; ++var9)
                {
                    double var10 = var1.segmentDomain(var9).v1;
                    double var12 = var8.normalizedParameterAt(var10);
                    double var14 = var2.parameterAt(var12);
                    Curve var16 = var1.segmentCurve(var9);
                    if (var16 is PolyCurve)
                    {
                        this.flatten((PolyCurve)var16, new Interval(var6, var14), var3, var4);
                        var1.harvestSegment(var9);
                    }
                    else
                    {
                        var3.Add(var14);
                        var4.Add(var16);
                        var1.harvestSegment(var9);
                    }

                    var6 = var14;
                }

            }

            public Curve harvestSegment(int var1)
            {
                Curve var2 = null;
                if (var1 >= 0 && var1 < this.segment.Count)
                {
                    var2 = (Curve)this.segment[var1];
                    this.segment[var1] = (Curve)null;
                    //this.segment.set(var1, (Object)null);
                }

                return var2;
            }

            public override IObject createIObject(Rhino3dmFile var1)
            {
                //FastList<> var3 = newList();
                //bool var4 = true;

                //for (int var5 = 0; var5 < this.segment.Count; ++var5)
                //{
                //    IObject var6 = ((Curve)this.segment[var5]).createIObject(var1);
                //    if (var6 is ICurve)
                //    {
                //        ICurve var7 = (ICurve)var6;
                //        var3.Add(var7);
                //        if (var7.deg() > 1)
                //        {
                //            var4 = false;
                //        }
                //    }
                //}

                //if (!var4)
                //{
                //    return new IPolycurve(var3);
                //}
                //else
                //{
                //    List var9 = newList();

                //    for (int var10 = 0; var10 < var3.Count; ++var10)
                //    {
                //        for (int var11 = 0; var11 < ((ICurve)var3.get(var10)).num(); ++var11)
                //        {
                //            HS_Vector var8 = ((ICurve)var3.get(var10)).cp(var11);
                //            if (var11 == 0 && (var10 == 0 || !((HS_Vector)var9[var9.Count - 1]).eq(var8,1)) || var11 > 0)
                //            {
                //                var9.Add(var8);
                //            }
                //        }

                //        ((ICurve)var3.get(var10)).del();
                //    }

                //    return new ICurve(var2, (HS_VectorI[])var9.toArray(new HS_Vector[var9.Count]));
                //}
                return null;
            }

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.segment != null && this.segment.Count != 0)
            //    {
            //        List var3 = newList();
            //        bool var4 = true;

            //        for (int var5 = 0; var5 < this.segment.Count; ++var5)
            //        {
            //            ICurveGeo var6 = ((Curve)this.segment.get(var5)).createIGeometry(var1, var2);
            //            var3.Add(var6);
            //            if (var6.deg() > 1)
            //            {
            //                var4 = false;
            //            }
            //        }

            //        if (!var4)
            //        {
            //            return (ICurveGeo)var3.get(0);
            //        }
            //        else
            //        {
            //            List var9 = newList();

            //            for (int var10 = 0; var10 < var3.Count; ++var10)
            //            {
            //                for (int var7 = 0; var7 < ((ICurveGeo)var3.get(var10)).num(); ++var7)
            //                {
            //                    HS_VectorI var8 = ((ICurveGeo)var3.get(var10)).cp(var7);
            //                    if (var7 == 0 && (var10 == 0 || !((HS_VectorI)var9.get(var9.Count - 1)).eq(var8)) || var7 > 0)
            //                    {
            //                        var9.Add(var8);
            //                    }
            //                }
            //            }

            //            return new ICurveGeo((HS_VectorI[])var9.toArray(new HS_Vector[var9.Count]));
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    if (this.segment != null && this.segment.Count != 0)
            //    {
            //        List var4 = newList();
            //        bool var5 = true;

            //        for (int var6 = 0; var6 < this.segment.Count; ++var6)
            //        {
            //            ITrimCurve var7 = ((Curve)this.segment.get(var6)).createTrimCurve(var1, var2, var3);
            //            var4.Add(var7);
            //            if (var7.deg() > 1)
            //            {
            //                var5 = false;
            //            }
            //        }

            //        List var10 = newList();

            //        for (int var11 = 0; var11 < var4.Count; ++var11)
            //        {
            //            for (int var8 = 0; var8 < ((ITrimCurve)var4.get(var11)).num(); ++var8)
            //            {
            //                HS_VectorI var9 = ((ITrimCurve)var4.get(var11)).cp(var8);
            //                if (var8 == 0 && (var11 == 0 || !((HS_VectorI)var10.get(var10.Count - 1)).eq(var9)) || var8 > 0)
            //                {
            //                    var10.Add(var9);
            //                }
            //            }
            //        }

            //        return new ITrimCurve((HS_VectorI[])var10.toArray(new HS_Vector[var10.Count]));
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }



        public class CurveArray : List<Curve>
        {
            public CurveArray()
            {
            }

            public CurveArray(int var1) : base(var1)
            {

            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var11 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var11);
                    int var5 = var4[0];
                    int var6 = var4[1];
                    if (var5 == 1)
                    {
                        int var7 = I3dmImporter.readInt(var11);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            int var9 = I3dmImporter.readInt(var11);
                            if (var9 == 1)
                            {
                                RhinoObject var10 = I3dmImporter.readObject(var1, var11);
                                if (var10 == null)
                                {
                                    throw new IOException("instance is null");
                                }

                                if (!(var10 is Curve))
                                {
                                    throw new IOException("invalid class of instance: " + var10);
                                }

                                this.Add((Curve)var10);
                            }
                        }
                    }

                }
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //int var5 = this.Count;
            //I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //for (int var6 = 0; var6 < var5; ++var6)
            //{
            //    if (this.get(var6) != null)
            //    {
            //        I3dmExporter.writeInt32(var4, 1, var4.getCRC());
            //        Chunk var7 = I3dmExporter.getObjectChunk(var1, (RhinoObject)this.get(var6));
            //        I3dmExporter.writeChunk(var4, var7);
            //    }
            //    else
            //    {
            //        I3dmExporter.writeInt32(var4, 0, var4.getCRC());
            //    }
            //}

            //I3dmExporter.writeChunk(var2, var4.getChunk());
            //        }
        }

        public class Surface : Rhino3dm.Geometry
        {
            public new static readonly String uuid = "4ED7D4E1-E947-11d3-BFE5-0010830122F0";

            public Surface()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4E1-E947-11d3-BFE5-0010830122F0");
            }

            public new int getType()
            {
                return 8;
            }

            public static Surface.ISO readIso(int var0)
            {
                switch (var0)
                {
                    case 0:
                        return Surface.ISO.NotIso;
                    case 1:
                        return Surface.ISO.XIso;
                    case 2:
                        return Surface.ISO.YIso;
                    case 3:
                        return Surface.ISO.WIso;
                    case 4:
                        return Surface.ISO.SIso;
                    case 5:
                        return Surface.ISO.EIso;
                    case 6:
                        return Surface.ISO.NIso;
                    default:
                        return Surface.ISO.NotIso;
                }
            }

            public static int getInt(Surface.ISO var0)
            {
                switch (var0)
                {
                    case ISO.NotIso:
                        return 0;
                    case ISO.XIso:
                        return 1;
                    case ISO.YIso:
                        return 2;
                    case ISO.WIso:
                        return 3;
                    case ISO.SIso:
                        return 4;
                    case ISO.EIso:
                        return 5;
                    case ISO.NIso:
                        return 6;
                    default:
                        return 0;
                }
            }

            public Interval domain(int var1)
            {
                return new Interval(0.0D, 1.0D);
            }

            //public ISurface createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    return null;
            //}

            //public ISurfaceGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    return null;
            //}

            public enum ISO
            {
                NotIso,
                XIso,
                YIso,
                WIso,
                SIso,
                EIso,
                NIso,
                IsoCount
            }
        }

        public class PlaneSurface : Surface
        {
            public new static readonly String uuid = "4ED7D4DF-E947-11d3-BFE5-0010830122F0";
            public Plane plane;
            public Interval[] Domain;
            public Interval[] extents;

            public PlaneSurface()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4DF-E947-11d3-BFE5-0010830122F0");
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.plane = I3dmImporter.readPlane(var2);
                    this.Domain = new Interval[2];
                    this.Domain[0] = I3dmImporter.readInterval(var2);
                    this.Domain[1] = I3dmImporter.readInterval(var2);
                    this.extents = new Interval[2];
                    this.extents[0] = this.Domain[0];
                    this.extents[1] = this.Domain[1];
                    if (var5 >= 1)
                    {
                        this.extents[0] = I3dmImporter.readInterval(var2);
                        this.extents[1] = I3dmImporter.readInterval(var2);
                    }
                }

            }

            //public ISurface createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.plane == null)
            //    {
            //        throw new Exception("plane is null");
            //        return null;
            //    }
            //    else if (this.extents == null)
            //    {
            //        throw new Exception("extent is null");
            //        return null;
            //    }
            //    else
            //    {
            //        HS_Vector var3 = this.plane.origin;
            //        HS_Vector[][] var4 = new HS_Vector[2][2];
            //        var4[0][0] = var3.dup().add(this.plane.xdaxis, this.extents[0].v1).add(this.plane.ydaxis, this.extents[1].v1);
            //        var4[1][0] = var3.dup().add(this.plane.xdaxis, this.extents[0].v2).add(this.plane.ydaxis, this.extents[1].v1);
            //        var4[0][1] = var3.dup().add(this.plane.xdaxis, this.extents[0].v1).add(this.plane.ydaxis, this.extents[1].v2);
            //        var4[1][1] = var3.dup().add(this.plane.xdaxis, this.extents[0].v2).add(this.plane.ydaxis, this.extents[1].v2);
            //        double[] var5 = new double[] { this.Domain[0].v1, this.Domain[0].v1, this.Domain[0].v2, this.Domain[0].v2 };
            //        double[] var6 = new double[] { this.Domain[1].v1, this.Domain[1].v1, this.Domain[1].v2, this.Domain[1].v2 };
            //        return new ISurface(var2, var4, 1, 1, var5, var6, this.Domain[0].v1, this.Domain[0].v2, this.Domain[1].v1, this.Domain[1].v2);
            //    }
            //}

            //public ISurfaceGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.plane == null)
            //    {
            //        throw new Exception("plane is null");
            //        return null;
            //    }
            //    else if (this.extents == null)
            //    {
            //        throw new Exception("extent is null");
            //        return null;
            //    }
            //    else
            //    {
            //        HS_Vector var3 = this.plane.origin;
            //        HS_Vector[][] var4 = new HS_Vector[2][2];
            //        var4[0][0] = var3.dup().add(this.plane.xdaxis, this.extents[0].v1).add(this.plane.ydaxis, this.extents[1].v1);
            //        var4[1][0] = var3.dup().add(this.plane.xdaxis, this.extents[0].v2).add(this.plane.ydaxis, this.extents[1].v1);
            //        var4[0][1] = var3.dup().add(this.plane.xdaxis, this.extents[0].v1).add(this.plane.ydaxis, this.extents[1].v2);
            //        var4[1][1] = var3.dup().add(this.plane.xdaxis, this.extents[0].v2).add(this.plane.ydaxis, this.extents[1].v2);
            //        double[] var5 = new double[] { this.Domain[0].v1, this.Domain[0].v1, this.Domain[0].v2, this.Domain[0].v2 };
            //        double[] var6 = new double[] { this.Domain[1].v1, this.Domain[1].v1, this.Domain[1].v2, this.Domain[1].v2 };
            //        return new ISurfaceGeo(var4, 1, 1, var5, var6, this.Domain[0].v1, this.Domain[0].v2, this.Domain[1].v1, this.Domain[1].v2);
            //    }
            //}

            public new Interval domain(int var1)
            {
                return var1 != 0 ? this.Domain[1] : this.Domain[0];
            }

            public bool isValid()
            {
                return this.plane.isValid() && this.Domain[0].isIncreasing() && this.Domain[1].isIncreasing() && this.extents[0].isIncreasing() && this.extents[1].isIncreasing();
            }
        }

        public class Plane
        {
            public HS_Vector origin;
            public HS_Vector xaxis;
            public HS_Vector yaxis;
            public HS_Vector zaxis;
            public PlaneEquation planeEquation;

            public Plane()
            {
            }

            public bool isValid()
            {
                return true;
            }

            public override String ToString()
            {
                return "orig=" + this.origin + ", x=" + this.xaxis + ", y=" + this.yaxis + ", z=" + this.zaxis + ", eq=" + this.planeEquation;
            }
        }

        public class PlaneEquation
        {
            public double x;
            public double y;
            public double z;
            public double d;

            public PlaneEquation()
            {
            }

            public PlaneEquation(double var1, double var3, double var5, double var7)
            {
                this.x = var1;
                this.y = var3;
                this.z = var5;
                this.d = var7;
            }

            public PlaneEquation(HS_Vector var1, HS_Vector var2)
            {
                this.x = var1.xd;
                this.y = var1.yd;
                this.z = var1.zd;
                this.d = -var1.dot(var2);
            }

            public override String ToString()
            {
                return "plane equation {" + this.x + "," + this.y + "," + this.z + "," + this.d + "}";
            }
        }

        public class SurfaceArray : List<Surface>
        {
            public SurfaceArray()
            {
            }

            public SurfaceArray(int var1) : base(var1)
            {

            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var11 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var11);
                    int var5 = var4[0];
                    int var6 = var4[1];
                    if (var5 == 1)
                    {
                        int var7 = I3dmImporter.readInt(var11);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            int var9 = I3dmImporter.readInt(var11);
                            if (var9 == 1)
                            {
                                RhinoObject var10 = I3dmImporter.readObject(var1, var11);
                                if (var10 == null)
                                {
                                    throw new IOException("instance is null");
                                }

                                if (!(var10 is Surface))
                                {
                                    throw new IOException("invalid class of instance: " + var10);
                                }

                                this.Add((Surface)var10);
                            }
                        }
                    }

                }
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //int var5 = this.Count;
            //I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //for (int var6 = 0; var6 < var5; ++var6)
            //{
            //    if (this.get(var6) != null)
            //    {
            //        I3dmExporter.writeInt32(var4, 1, var4.getCRC());
            //        Chunk var7 = I3dmExporter.getObjectChunk(var1, (RhinoObject)this.get(var6));
            //        I3dmExporter.writeChunk(var4, var7);
            //    }
            //    else
            //    {
            //        I3dmExporter.writeInt32(var4, 0, var4.getCRC());
            //    }
            //}

            //I3dmExporter.writeChunk(var2, var4.getChunk());
            //        }
        }

        public class SurfaceProxy : Surface
        {
            public new static readonly String uuid = "4ED7D4E2-E947-11d3-BFE5-0010830122F0";
            public Surface surface;
            public bool transposed;

            public SurfaceProxy()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4E2-E947-11d3-BFE5-0010830122F0");
            }

            public void setProxySurface(Surface var1)
            {
                if (var1 == this)
                {
                    var1 = null;
                }

                this.surface = var1;
                this.transposed = false;
            }
        }
        public class BrepEdge : CurveProxy
        {
            public new static readonly String uuid = "60B5DBC1-E660-11d3-BFE4-0010830122F0";
            public int edgeIndex;
            public int curve3Index;
            public int[] vertexIndex;
            public List<int> trimIndex;
            public double tolerance;
            public Brep brep;

            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC1-E660-11d3-BFE4-0010830122F0");
            }

            public new int getType()
            {
                return 4;
            }

            public BrepEdge()
            {
            }

            public BrepEdge(int var1, int var2, int var3, int var4, Interval var5, Interval var6, Brep var7)
            {
                this.edgeIndex = var1;
                this.curve3Index = var2;
                this.vertexIndex = new int[2];
                this.vertexIndex[0] = var3;
                this.vertexIndex[1] = var4;
                this.brep = var7;
                this.trimIndex = new List<int>();
                this.tolerance = 0.0D;
                this.setProxyCurveDomain(var5);
                this.setDomain(var6);
            }

            public void addTrimIndex(int var1)
            {
                this.trimIndex.Add(var1);
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                this.edgeIndex = I3dmImporter.readInt(var2);
                this.curve3Index = I3dmImporter.readInt(var2);
                int var3 = I3dmImporter.readInt(var2);
                Interval var4 = I3dmImporter.readInterval(var2);
                this.vertexIndex = new int[2];
                this.vertexIndex[0] = I3dmImporter.readInt(var2);
                this.vertexIndex[1] = I3dmImporter.readInt(var2);
                this.trimIndex = I3dmImporter.readArrayInt(var2);
                this.tolerance = I3dmImporter.readDouble(var2);
                Interval var5 = var4;
                if (var1.version > 4)
                {
                    if (var1.version >= 3 && var1.openNurbsVersion >= 200206180)
                    {
                        try
                        {
                            var5 = I3dmImporter.readInterval(var2);
                        }
                        catch (IOException var7)
                        {
                            var5 = var4;
                        }
                    }
                }
                else
                {
                    if (var1.version >= 3 && var1.openNurbsVersion >= 200206180)
                    {
                        try
                        {
                            var5 = I3dmImporter.readInterval(var2);
                        }
                        catch (IOException var7)
                        {
                            var5 = var4;
                        }
                    }
                }


                this.setProxyCurve((Curve)null, var4);
                if (var3 != 0)
                {
                    this.reverse();
                }

                this.setDomain(var5);
                Console.WriteLine(200 + " edgeIndex = " + this.edgeIndex);
                Console.WriteLine(200 + " curve3Index = " + this.curve3Index);
                Console.WriteLine(200 + " vertexIndex = " + this.vertexIndex[0] + ", " + this.vertexIndex[1]);
                Console.WriteLine(200 + " trimIndex = " + this.trimIndex);
                Console.WriteLine(200 + " tolerance = " + this.tolerance);
                Console.WriteLine(200 + " domain = " + this.domain());
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //    I3dmExporter.writeInt32(var2, this.edgeIndex, var3);
            //    I3dmExporter.writeInt32(var2, this.curve3Index, var3);
            //    int var4 = this.proxyCurveIsReversed() ? 1 : 0;
            //    I3dmExporter.writeInt32(var2, var4, var3);
            //    I3dmExporter.writeInterval(var2, this.proxyCurveDomain(), var3);
            //    I3dmExporter.writeInt32(var2, this.vertexIndex[0], var3);
            //    I3dmExporter.writeInt32(var2, this.vertexIndex[1], var3);
            //    I3dmExporter.writeArrayInt(var2, this.trimIndex, var3);
            //    I3dmExporter.writedouble(var2, this.tolerance, var3);
            //    if (var1.version >= 4)
            //    {
            //        I3dmExporter.writeInterval(var2, this.domain(), var3);
            //    }

            //}
        }

        public class BrepEdgeArray : List<BrepEdge>
        {
            public BrepEdgeArray()
            {
            }

            public BrepEdgeArray(int var1) : base(var1)
            {

            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var10 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var10);
                    int var5 = var4[0];
                    int var6 = var4[1];
                    if (var5 == 1)
                    {
                        int var7 = I3dmImporter.readInt(var10);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            BrepEdge var9 = new BrepEdge();
                            var9.read(var1, var10);
                            this.Add(var9);
                        }
                    }

                }
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //int var5 = this.Count;
            //I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //for (int var6 = 0; var6 < var5; ++var6)
            //{
            //    ((BrepEdge)this.get(var6)).write(var1, var4, var4.getCRC());
            //}

            //I3dmExporter.writeChunk(var2, var4.getChunk());
            //        }
        }



        public class Point : Rhino3dm.Geometry
        {
            public new static readonly String uuid = "C3101A1D-F157-11d3-BFE7-0010830122F0";
            public HS_Vector point;

            public new UUID getClassUUID()
            {
                return new UUID("C3101A1D-F157-11d3-BFE7-0010830122F0");
            }

            public new int getType()
            {
                return 1;
            }

            public Point()
            {
            }

            public Point(HS_Vector var1)
            {
                this.point = var1;
            }

            public override IObject createIObject(Rhino3dmFile var1)
            {
                HS_Vector p = new HS_Vector();
                p = this.point;
                p.attribute.name = "Point";
                return p;
            }


            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                if (var3[0] != 1)
                {
                    throw new IOException("invalid major version : " + (var3[0]).ToString());
                }
                else
                {
                    this.point = new HS_Vector();
                    this.point.xd = I3dmImporter.readDouble(var2);
                    this.point.yd = I3dmImporter.readDouble(var2);
                    this.point.zd = I3dmImporter.readDouble(var2);
                }
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeChunkVersion(var2, 1, 0, var3);
            //            if (this.point == null) {
            //        throw new Exception("point is null");
            //        this.point = new HS_Vector();
            //    }

            //    I3dmExporter.writedouble(var2, this.point.xd, var3);
            //    I3dmExporter.writedouble(var2, this.point.yd, var3);
            //    I3dmExporter.writedouble(var2, this.point.zd, var3);
            //}

            //public IPoint createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    IPoint var3 = new IPoint(var2, this.point);
            //    return var3;
            //}

            //public HS_Vector createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    return this.point.dup();
            //}
        }

        public class NurbsCurve : Curve
        {
            public new static readonly String uuid = "4ED7D4DD-E947-11d3-BFE5-0010830122F0";
            public int dim;
            public int isRat;
            public int order;
            public int cvCount;
            public int knotCapacity;
            public double[] knot;
            //public HS_Vector[] cv;

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4DD-E947-11d3-BFE5-0010830122F0");
            }

            public NurbsCurve()
            {
            }

            //public NurbsCurve(ICurveGeo var1)
            //{
            //    this.dim = 3;
            //    this.isRat = var1.isRational() ? 1 : 0;
            //    this.order = var1.deg() + 1;
            //    this.cvCount = var1.num();
            //    double[] var2 = new double[var1.knotNum()];

            //    int var3;
            //    for (var3 = 0; var3 < var1.knotNum(); ++var3)
            //    {
            //        var2[var3] = var1.knot(var3);
            //    }

            //    this.knot = getRhinoKnots(var2);
            //    this.cv = new HS_Vector[var1.num()];

            //    for (var3 = 0; var3 < var1.num(); ++var3)
            //    {
            //        this.cv[var3] = var1.cp(var3).get();
            //    }

            //    this.icurve = var1;
            //}

            //public NurbsCurve(ITrimCurve var1)
            //{
            //    this.dim = 2;
            //    this.isRat = var1.isRational() ? 1 : 0;
            //    this.order = var1.deg() + 1;
            //    this.cvCount = var1.num();
            //    double[] var2 = new double[var1.knotNum()];

            //    int var3;
            //    for (var3 = 0; var3 < var1.knotNum(); ++var3)
            //    {
            //        var2[var3] = var1.knot(var3);
            //    }

            //    this.knot = getRhinoKnots(var2);
            //    this.cv = new HS_Vector[var1.num()];

            //    for (var3 = 0; var3 < var1.num(); ++var3)
            //    {
            //        this.cv[var3] = var1.cp(var3).get();
            //    }

            //}

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                var2.Position-=4;
                int[] var3 = I3dmImporter.readChunkVersion(var2);

                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.dim = I3dmImporter.readInt(var2);
                    this.isRat = I3dmImporter.readInt(var2);
                    this.order = I3dmImporter.readInt(var2);
                    this.cvCount = I3dmImporter.readInt(var2);
                    int var6 = I3dmImporter.readInt(var2);
                    int var7 = I3dmImporter.readInt(var2);
                    BoundingBox var8 = I3dmImporter.readBoundingBox(var2);
                    int var9 = I3dmImporter.readInt(var2);
                    this.knot = new double[var9];

                    int var10;
                    for (var10 = 0; var10 < var9; ++var10)
                    {
                        this.knot[var10] = I3dmImporter.readDouble(var2);
                        Console.WriteLine(100 + "knot[" + var10 + "] = " + this.knot[var10]);
                    }

                    var10 = I3dmImporter.readInt(var2);
                    int var10000;
                    if (this.isRat != 0)
                    {
                        var10000 = this.dim + 1;
                    }
                    else
                    {
                        var10000 = this.dim;
                    }

                    this.cv = new HS_Vector[var10];

                    for (int var12 = 0; var12 < var10; ++var12)
                    {
                        if (this.isRat != 0)
                        {
                            this.cv[var12] = new HS_Vector();
                        }
                        else
                        {
                            this.cv[var12] = new HS_Vector();
                        }

                        if (this.dim >= 1)
                        {
                            this.cv[var12].xd = I3dmImporter.readDouble(var2);
                        }

                        if (this.dim >= 2)
                        {
                            this.cv[var12].yd = I3dmImporter.readDouble(var2);
                        }

                        if (this.dim >= 3)
                        {
                            this.cv[var12].zd = I3dmImporter.readDouble(var2);
                        }

                        if (this.dim >= 4)
                        {
                            throw new Exception(this.dim + " dimension point cannot be read");

                            for (int var13 = 0; var13 < this.dim - 3; ++var13)
                            {
                                I3dmImporter.readDouble(var2);
                            }
                        }

                        if (this.isRat != 0)
                        {
                            double var15 = I3dmImporter.readDouble(var2);
                            //((HS_Vector)this.cv[var12]).w = var15;
                            HS_Vector var16 = this.cv[var12];
                            var16.xd /= var15;
                            var16 = this.cv[var12];
                            var16.yd /= var15;
                            var16 = this.cv[var12];
                            var16.zd /= var15;
                        }

                        Console.WriteLine(100 + "cv[" + var12 + "] = " + this.cv[var12]);
                    }
                }

            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeChunkVersion(var2, 1, 0, var3);
            //    I3dmExporter.writeInt32(var2, this.dim, var3);
            //    I3dmExporter.writeInt32(var2, this.isRat, var3);
            //    I3dmExporter.writeInt32(var2, this.order, var3);
            //    I3dmExporter.writeInt32(var2, this.cvCount, var3);
            //    I3dmExporter.writeInt32(var2, 0, var3);
            //    I3dmExporter.writeInt32(var2, 0, var3);
            //    I3dmExporter.writeBoundingBox(var2, new BoundingBox(), var3);
            //int var4 = this.knot == null ? 0 : this.knot.Length;
            //I3dmExporter.writeInt32(var2, var4, var3);

            //int var5;
            //for (var5 = 0; var5 < var4; ++var5)
            //{
            //    I3dmExporter.writeDouble(var2, this.knot[var5], var3);
            //}

            //var5 = this.cv == null || this.dim != 2 && this.dim != 3 ? 0 : this.cv.Length;
            //I3dmExporter.writeInt32(var2, var5, var3);

            //for (int var6 = 0; var6 < var5; ++var6)
            //{
            //    double var7;
            //    if (this.dim == 2)
            //    {
            //        if (this.isRat == 0)
            //        {
            //            I3dmExporter.writeDouble(var2, this.cv[var6].xd, var3);
            //            I3dmExporter.writeDouble(var2, this.cv[var6].yd, var3);
            //        }
            //        else
            //        {
            //            var7 = 1.0D;
            //            if (this.cv[var6] is HS_Vector4) {
            //    var7 = ((HS_Vector4)this.cv[var6]).w;
            //}

            //I3dmExporter.writeDouble(var2, this.cv[var6].xd * var7, var3);
            //I3dmExporter.writeDouble(var2, this.cv[var6].yd * var7, var3);
            //I3dmExporter.writeDouble(var2, var7, var3);
            //                    }
            //        } else if (this.dim == 3)
            //{
            //            if (this.isRat == 0)
            //            {
            //                I3dmExporter.writeDouble(var2, this.cv[var6].xd, var3);
            //                I3dmExporter.writeDouble(var2, this.cv[var6].yd, var3);
            //                I3dmExporter.writeDouble(var2, this.cv[var6].zd, var3);
            //            }
            //            else
            //            {
            //                var7 = 1.0D;
            //                if (this.cv[var6] is HS_Vector4) {
            //                    var7 = ((HS_Vector4)this.cv[var6]).w;
            //                }

            //                I3dmExporter.writeDouble(var2, this.cv[var6].xd * var7, var3);
            //                I3dmExporter.writeDouble(var2, this.cv[var6].yd * var7, var3);
            //                I3dmExporter.writeDouble(var2, this.cv[var6].zd * var7, var3);
            //                I3dmExporter.writeDouble(var2, var7, var3);
            //            }
            //        }
            //        }

            //}

            public double[] getIGKnots()
            {
                double[] var1 = new double[this.knot.Length + 2];
                int var2;
                if (this.order == 2)
                {
                    var1[0] = this.knot[0];

                    for (var2 = 0; var2 < this.knot.Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[var2];
                    }

                    var1[this.knot.Length + 1] = this.knot[this.knot.Length - 1];
                }
                else if (this.order > 2 && this.knot.Length > 2)
                {
                    var1[0] = this.knot[0] - (this.knot[1] - this.knot[0]);

                    for (var2 = 0; var2 < this.knot.Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[var2];
                    }

                    var1[this.knot.Length + 1] = this.knot[this.knot.Length - 1] + (this.knot[this.knot.Length - 1] - this.knot[this.knot.Length - 2]);
                }
                else
                {
                    throw new Exception("wrong knot length (" + this.knot.Length + ") at order = " + this.order + ", and cv count = " + this.cvCount);
                }

                return var1;
            }

            public static double[] getRhinoKnots(double[] var0)
            {
                if (var0.Length <= 2)
                {
                    throw new Exception("knot is too short");
                }

                double[] var1 = new double[var0.Length - 2];

                for (int var2 = 0; var2 < var1.Length; ++var2)
                {
                    var1[var2] = var0[var2 + 1];
                }

                return var1;
            }

            //public ICurve createIObject(Rhino3dmFile var1, IServerI var2)
            //{
            //    double[] var3 = this.getIGKnots();
            //    double var4 = 0.0D;
            //    double var6 = 1.0D;
            //    if (this.knot.Length > this.order - 2)
            //    {
            //        var4 = this.knot[this.order - 2];
            //        var6 = this.knot[this.knot.Length - 1 - (this.order - 2)];
            //        ICurve var8 = new ICurve(var2, this.cv, this.order - 1, var3, var4, var6);
            //        return var8;
            //    }
            //    else
            //    {
            //        throw new Exception("knot is too short: knot.Length=" + this.knot.Length + ", order=" + this.order);
            //        return null;
            //    }
            //}

            //public ICurveGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    double[] var3 = this.getIGKnots();
            //    double var4 = 0.0D;
            //    double var6 = 1.0D;
            //    if (this.knot.Length > this.order - 2)
            //    {
            //        var4 = this.knot[this.order - 2];
            //        var6 = this.knot[this.knot.Length - 1 - (this.order - 2)];
            //        return new ICurveGeo(this.cv, this.order - 1, var3, var4, var6);
            //    }
            //    else
            //    {
            //        throw new Exception("knot is too short: knot.Length=" + this.knot.Length + ", order=" + this.order);
            //        return null;
            //    }
            //}

            //public ITrimCurve createTrimCurve(Rhino3dmFile var1, IServerI var2, ISurfaceI var3)
            //{
            //    double[] var4 = this.getIGKnots();
            //    double var5 = 0.0D;
            //    double var7 = 1.0D;
            //    if (this.knot.Length > this.order - 2)
            //    {
            //        var5 = this.knot[this.order - 2];
            //        var7 = this.knot[this.knot.Length - 1 - (this.order - 2)];
            //        return new ITrimCurve(this.cv, this.order - 1, var4, var5, var7);
            //    }
            //    else
            //    {
            //        throw new Exception("knot is too short: knot.Length=" + this.knot.Length + ", order=" + this.order);
            //        return null;
            //    }
            //}

            public int cvSize()
            {
                return this.isRat != 0 ? this.dim + 1 : this.dim;
            }

            public static Interval getKnotVectorDomain(int var0, int var1, double[] var2)
            {
                if (var0 >= 2 && var1 >= var0 && var2 != null)
                {
                    Interval var3 = new Interval();
                    var3.v1 = var2[var0 - 2];
                    var3.v2 = var2[var1 - 1];
                    return var3;
                }
                else
                {
                    return null;
                }
            }

            public static bool isValidKnotVector(int var0, int var1, double[] var2)
            {
                if (var0 >= 2 && var1 >= var0 && var2 != null && !(var2[var0 - 2] >= var2[var0 - 1]) && !(var2[var1 - 2] >= var2[var1 - 1]))
                {
                    int var3;
                    for (var3 = 0; var3 < var2.Length - 1; ++var3)
                    {
                        if (var2[var3] > var2[var3 + 1])
                        {
                            return false;
                        }
                    }

                    for (var3 = 0; var3 < var2.Length - var0 + 1; ++var3)
                    {
                        if (var2[var3] >= var2[var3 + var0 - 1])
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

            public override Interval domain()
            {
                return getKnotVectorDomain(this.order, this.cvCount, this.knot);
            }

            public override bool isValid()
            {
                if (this.dim > 0 && this.order >= 2 && this.cvCount >= this.order && this.cv != null && this.knot != null && !isValidKnotVector(this.order, this.cvCount, this.knot))
                {
                    if (this.isRat != 0)
                    {
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class NurbsSurface : Surface
        {
            public new static readonly String uuid = "4ED7D4DE-E947-11d3-BFE5-0010830122F0";
            public int dim;
            public int isRat;
            public int[] order;
            public int[] cvCount;
            public int[] knotCapacity;
            public double[][] knot;
            public HS_Vector[][] cv;

            public new UUID getClassUUID()
            {
                return new UUID("4ED7D4DE-E947-11d3-BFE5-0010830122F0");
            }

            public NurbsSurface()
            {
            }

            //public NurbsSurface(ISurfaceGeo var1)
            //{
            //    this.dim = 3;
            //    this.isRat = var1.isRational() ? 1 : 0;
            //    this.order = new int[2];
            //    this.order[0] = var1.udeg() + 1;
            //    this.order[1] = var1.vdeg() + 1;
            //    this.cvCount = new int[2];
            //    this.cvCount[0] = var1.unum();
            //    this.cvCount[1] = var1.vnum();
            //    this.knot = new double[2][];
            //    double[] var2 = new double[var1.uknotNum()];

            //    for (int var3 = 0; var3 < var2.Length; ++var3)
            //    {
            //        var2[var3] = var1.uknot(var3);
            //    }

            //    this.knot[0] = NurbsCurve.getRhinoKnots(var2);
            //    double[] var6 = new double[var1.vknotNum()];

            //    int var4;
            //    for (var4 = 0; var4 < var6.Length; ++var4)
            //    {
            //        var6[var4] = var1.vknot(var4);
            //    }

            //    this.knot[1] = NurbsCurve.getRhinoKnots(var6);
            //    this.cv = new HS_Vector[this.cvCount[0]][this.cvCount[1]];

            //    for (var4 = 0; var4 < this.cvCount[0]; ++var4)
            //    {
            //        for (int var5 = 0; var5 < this.cvCount[1]; ++var5)
            //        {
            //            this.cv[var4][var5] = var1.cp(var4, var5).get();
            //        }
            //    }

            //}

            public new Interval domain(int var1)
            {
                Interval var2 = new Interval();
                var2.v1 = this.knot[var1][this.order[var1] - 2];
                var2.v2 = this.knot[var1][this.cvCount[var1] - 1];
                return var2;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.order = new int[2];
                    this.cvCount = new int[2];
                    this.knotCapacity = new int[2];
                    this.knot = new double[2][];
                    this.dim = I3dmImporter.readInt(var2);
                    this.isRat = I3dmImporter.readInt(var2);
                    this.order[0] = I3dmImporter.readInt(var2);
                    this.order[1] = I3dmImporter.readInt(var2);
                    this.cvCount[0] = I3dmImporter.readInt(var2);
                    this.cvCount[1] = I3dmImporter.readInt(var2);
                    int var6 = I3dmImporter.readInt(var2);
                    int var7 = I3dmImporter.readInt(var2);
                    BoundingBox var8 = I3dmImporter.readBoundingBox(var2);
                    int var9 = I3dmImporter.readInt(var2);
                    this.knot[0] = new double[var9];

                    int var10;
                    for (var10 = 0; var10 < var9; ++var10)
                    {
                        this.knot[0][var10] = I3dmImporter.readDouble(var2);
                        Console.WriteLine(100 + " uknot[" + var10 + "]=" + this.knot[0][var10]);
                    }

                    var9 = I3dmImporter.readInt(var2);
                    this.knot[1] = new double[var9];

                    for (var10 = 0; var10 < var9; ++var10)
                    {
                        this.knot[1][var10] = I3dmImporter.readDouble(var2);
                        Console.WriteLine(100 + " vknot[" + var10 + "]=" + this.knot[1][var10]);
                    }

                    var10 = I3dmImporter.readInt(var2);
                    int var10000;
                    if (this.isRat != 0)
                    {
                        var10000 = this.dim + 1;
                    }
                    else
                    {
                        var10000 = this.dim;
                    }

                    if (var10 != this.cvCount[0] * this.cvCount[1])
                    {
                        throw new Exception("cv count (" + this.cvCount + ") doesn't match with cvCount[0](" + this.cvCount[0] + ") * cvCount[1](" + this.cvCount[1] + ")");
                    }

                    this.cv = new HS_Vector[this.cvCount[0]][];
                    for (int i = 0; i < this.cv.Length; i++)
                    {
                        this.cv[i] = new HS_Vector[this.cvCount[1]];
                    }


                    for (int var12 = 0; var12 < this.cvCount[0]; ++var12)
                    {
                        for (int var13 = 0; var13 < this.cvCount[1]; ++var13)
                        {
                            if (this.isRat != 0)
                            {
                                this.cv[var12][var13] = new HS_Vector();
                            }
                            else
                            {
                                this.cv[var12][var13] = new HS_Vector();
                            }

                            if (this.dim >= 1)
                            {
                                this.cv[var12][var13].xd = I3dmImporter.readDouble(var2);
                            }

                            if (this.dim >= 2)
                            {
                                this.cv[var12][var13].yd = I3dmImporter.readDouble(var2);
                            }

                            if (this.dim >= 3)
                            {
                                this.cv[var12][var13].zd = I3dmImporter.readDouble(var2);
                            }

                            if (this.dim >= 4)
                            {
                                throw new Exception(this.dim + " dimension point cannot be read");

                                for (int var14 = 0; var14 < this.dim - 3; ++var14)
                                {
                                    I3dmImporter.readDouble(var2);
                                }
                            }

                            if (this.isRat != 0)
                            {
                                double var16 = I3dmImporter.readDouble(var2);
                                //((HS_Vector)this.cv[var12][var13]).w = var16;
                                HS_Vector var17 = this.cv[var12][var13];
                                var17.xd /= var16;
                                var17 = this.cv[var12][var13];
                                var17.yd /= var16;
                                var17 = this.cv[var12][var13];
                                var17.zd /= var16;
                            }
                        }
                    }
                }

            }

            //public void write(Rhino3dmFile var1, Stream var2, CRC32 var3)
            //{
            //    IRhino3dmExporter.writeChunkVersion(var2, 1, 0, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.dim, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.isRat, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.order[0], var3);
            //    IRhino3dmExporter.writeInt32(var2, this.order[1], var3);
            //    IRhino3dmExporter.writeInt32(var2, this.cvCount[0], var3);
            //    IRhino3dmExporter.writeInt32(var2, this.cvCount[1], var3);
            //    IRhino3dmExporter.writeInt32(var2, 0, var3);
            //    IRhino3dmExporter.writeInt32(var2, 0, var3);
            //    IRhino3dmExporter.writeBoundingBox(var2, new BoundingBox(), var3);
            //    int var4 = this.knot[0] != null ? this.knot[0].Length : 0;
            //    IRhino3dmExporter.writeInt32(var2, var4, var3);

            //    int var5;
            //    for (var5 = 0; var5 < var4; ++var5)
            //    {
            //        IRhino3dmExporter.writeDouble(var2, this.knot[0][var5], var3);
            //    }

            //    var5 = this.knot[1] != null ? this.knot[1].Length : 0;
            //    IRhino3dmExporter.writeInt32(var2, var5, var3);

            //    int var6;
            //    for (var6 = 0; var6 < var5; ++var6)
            //    {
            //        IRhino3dmExporter.writeDouble(var2, this.knot[1][var6], var3);
            //    }

            //    var6 = this.cv != null && this.dim == 3 && this.cvCount[0] > 0 && this.cvCount[1] > 0 ? this.cvCount[0] * this.cvCount[1] : 0;
            //    IRhino3dmExporter.writeInt32(var2, var6, var3);
            //    if (var6 > 0)
            //    {
            //        for (int var7 = 0; var7 < this.cvCount[0]; ++var7)
            //        {
            //            for (int var8 = 0; var8 < this.cvCount[1]; ++var8)
            //            {
            //                if (this.isRat == 0)
            //                {
            //                    IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].xd, var3);
            //                    IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].yd, var3);
            //                    IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].zd, var3);
            //                }
            //                else
            //                {
            //                    double var9 = 1.0D;
            //                    if (this.cv[var7][var8] is HS_Vector4) {
            //            var9 = ((HS_Vector4)this.cv[var7][var8]).w;
            //        }

            //        IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].xd * var9, var3);
            //        IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].yd * var9, var3);
            //        IRhino3dmExporter.writeDouble(var2, this.cv[var7][var8].zd * var9, var3);
            //        IRhino3dmExporter.writeDouble(var2, var9, var3);
            //    }
            //}

            public int cvSize()
            {
                return this.isRat != 0 ? this.dim + 1 : this.dim;
            }

            public double[] getIGUKnots()
            {
                double[] var1 = new double[this.knot[0].Length + 2];
                int var2;
                if (this.order[0] == 2)
                {
                    var1[0] = this.knot[0][0];

                    for (var2 = 0; var2 < this.knot[0].Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[0][var2];
                    }

                    var1[this.knot[0].Length + 1] = this.knot[0][this.knot[0].Length - 1];
                }
                else if (this.order[0] > 2 && this.knot[0].Length > 2)
                {
                    var1[0] = this.knot[0][0] - (this.knot[0][1] - this.knot[0][0]);

                    for (var2 = 0; var2 < this.knot[0].Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[0][var2];
                    }

                    var1[this.knot[0].Length + 1] = this.knot[0][this.knot[0].Length - 1] + (this.knot[0][this.knot[0].Length - 1] - this.knot[0][this.knot[0].Length - 2]);
                }
                else
                {
                    throw new Exception("wrong knot length (" + this.knot[0].Length + ") at order = " + this.order[0] + ", and cv count = " + this.cvCount[0]);
                }

                return var1;
            }

            public double[] getIGVKnots()
            {
                double[] var1 = new double[this.knot[1].Length + 2];
                int var2;
                if (this.order[1] == 2)
                {
                    var1[0] = this.knot[1][0];

                    for (var2 = 0; var2 < this.knot[1].Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[1][var2];
                    }

                    var1[this.knot[1].Length + 1] = this.knot[1][this.knot[1].Length - 1];
                }
                else if (this.order[1] > 2 && this.knot[1].Length > 2)
                {
                    var1[0] = this.knot[1][0] - (this.knot[1][1] - this.knot[1][0]);

                    for (var2 = 0; var2 < this.knot[1].Length; ++var2)
                    {
                        var1[var2 + 1] = this.knot[1][var2];
                    }

                    var1[this.knot[1].Length + 1] = this.knot[1][this.knot[1].Length - 1] + (this.knot[1][this.knot[1].Length - 1] - this.knot[1][this.knot[1].Length - 2]);
                }
                else
                {
                    throw new Exception("wrong knot length (" + this.knot[1].Length + ") at order = " + this.order[1] + ", and cv count = " + this.cvCount[1]);
                }

                return var1;
            }

            //public ISurface createIObject(Rhino3dmFile var1)
            //{
            //    if (this.order == null)
            //    {
            //        throw new Exception("order is null");
            //        return null;
            //    }
            //    else if (this.knot == null)
            //    {
            //        throw new Exception("knot is null");
            //        return null;
            //    }
            //    else if (this.cv == null)
            //    {
            //        throw new Exception("cv is null");
            //        return null;
            //    }
            //    else
            //    {
            //        double[] var3 = this.getIGUKnots();
            //        double[] var4 = this.getIGVKnots();
            //        double var5 = 0.0D;
            //        double var7 = 1.0D;
            //        if (this.knot[0].Length > this.order[0] - 2)
            //        {
            //            var5 = this.knot[0][this.order[0] - 2];
            //            var7 = this.knot[0][this.knot[0].Length - 1 - (this.order[0] - 2)];
            //        }

            //        double var9 = 0.0D;
            //        double var11 = 1.0D;
            //        if (this.knot[1].Length > this.order[1] - 2)
            //        {
            //            var9 = this.knot[1][this.order[1] - 2];
            //            var11 = this.knot[1][this.knot[1].Length - 1 - (this.order[1] - 2)];
            //        }

            //        return new ISurface(var2, this.cv, this.order[0] - 1, this.order[1] - 1, var3, var4, var5, var7, var9, var11);
            //    }
            //}

            //public ISurfaceGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.order == null)
            //    {
            //        throw new Exception("order is null");
            //        return null;
            //    }
            //    else if (this.knot == null)
            //    {
            //        throw new Exception("knot is null");
            //        return null;
            //    }
            //    else if (this.cv == null)
            //    {
            //        throw new Exception("cv is null");
            //        return null;
            //    }
            //    else
            //    {
            //        double[] var3 = this.getIGUKnots();
            //        double[] var4 = this.getIGVKnots();
            //        double var5 = 0.0D;
            //        double var7 = 1.0D;
            //        if (this.knot[0].Length > this.order[0] - 2)
            //        {
            //            var5 = this.knot[0][this.order[0] - 2];
            //            var7 = this.knot[0][this.knot[0].Length - 1 - (this.order[0] - 2)];
            //        }

            //        double var9 = 0.0D;
            //        double var11 = 1.0D;
            //        if (this.knot[1].Length > this.order[1] - 2)
            //        {
            //            var9 = this.knot[1][this.order[1] - 2];
            //            var11 = this.knot[1][this.knot[1].Length - 1 - (this.order[1] - 2)];
            //        }

            //        return new ISurfaceGeo(this.cv, this.order[0] - 1, this.order[1] - 1, var3, var4, var5, var7, var9, var11);
            //    }
            //}
        }
        public class SumSurface : Surface
        {
            public new static readonly String uuid = "C4CD5359-446D-4690-9FF5-29059732472B";
            public Curve[] curve;
            public HS_Vector basepoint;
            public BoundingBox bbox;

            public SumSurface()
            {
            }

            public new UUID getClassUUID()
            {
                return new UUID("C4CD5359-446D-4690-9FF5-29059732472B");
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 1)
                {
                    this.basepoint = I3dmImporter.readPoint3(var2);
                    this.bbox = I3dmImporter.readBoundingBox(var2);
                    this.curve = new Curve[2];
                    RhinoObject var6 = I3dmImporter.readObject(var1, var2);
                    if (var6 != null)
                    {
                        if (var6 is Curve)
                        {
                            this.curve[0] = (Curve)var6;
                        }
                        else
                        {
                            throw new Exception("wrong instance of class : " + var6);
                        }
                    }

                    var6 = I3dmImporter.readObject(var1, var2);
                    if (var6 != null)
                    {
                        if (var6 is Curve)
                        {
                            this.curve[1] = (Curve)var6;
                        }
                        else
                        {
                            throw new Exception("wrong instance of class : " + var6);
                        }
                    }
                }

            }

            public override IObject createIObject(Rhino3dmFile var1)
            {
                return null;
                //ISurfaceGeo var3 = this.createIGeometry(var1, var2);
                //return new ISurface(var2, var3);

                //List<BrepVertex> vertices = this.vertices;
                //List<HS_Point> pts = new List<HS_Point>();
                //foreach (BrepVertex vertex in vertices)
                //{
                //    HS_Point pt = new HS_Point(vertex.point);

                //    pts.Add(pt);
                //}


                //List<int[]> faceList = new List<int[]>();

                //foreach (Rhino3dm.BrepLoop l in this.loops)
                //{
                //    int[] vindex = new int[l.trimIndex.Count + 1];
                //    vindex[0] = this.trims[l.trimIndex[0]].vertexIndex[0];
                //    for (int i = 0; i < l.trimIndex.Count; i++)
                //    {
                //        //foreach(int vid in this.edges[index].vertexIndex) {
                //        vindex[i + 1] = this.trims[l.trimIndex[i]].vertexIndex[1];
                //        //}

                //    }
                //    faceList.Add(vindex);
                //}


                //GEC_FromFaceList creator = new GEC_FromFaceList();
                //creator.setVertices(pts);
                //creator.setFaces(faceList);
                //GE_Mesh m = creator.create();
                //Console.WriteLine(m);
                //return m;

            }

            //public ISurfaceGeo createIGeometry(Rhino3dmFile var1, IServerI var2)
            //{
            //    if (this.curve[0] != null && this.curve[1] != null && this.basepoint != null)
            //    {
            //        ICurveGeo var3 = this.curve[0].createIGeometry(var1, var2);
            //        ICurveGeo var4 = this.curve[1].createIGeometry(var1, var2);
            //        if (var3 != null && var4 != null)
            //        {
            //            int var5 = var3.deg();
            //            int var6 = var4.deg();
            //            double[] var7 = var3.knots;
            //            double[] var8 = var4.knots;
            //            int var9 = var3.num();
            //            int var10 = var4.num();
            //            Interval var11 = this.curve[0].domain();
            //            Interval var12 = this.curve[1].domain();
            //            HS_VectorI[][] var13 = new HS_VectorI[var9][var10];

            //            int var15;
            //            for (int var14 = 0; var14 < var9; ++var14)
            //            {
            //                for (var15 = 0; var15 < var10; ++var15)
            //                {
            //                    var13[var14][var15] = this.basepoint.dup().add(var3.cp(var14)).add(var4.cp(var15));
            //                }
            //            }

            //            ISurfaceGeo var16 = new ISurfaceGeo(var13, var5, var6, var7, var8, 0.0D, 1.0D, 0.0D, 1.0D);
            //            var16.ustart = var11.v1;
            //            var16.uend = var11.v2;
            //            var16.vstart = var12.v1;
            //            var16.vend = var12.v2;
            //            if (IOut.debugLevel() >= 20)
            //            {
            //                for (var15 = 0; var15 < var7.length; ++var15)
            //                {
            //                    IOut.debug(20, "uknots[" + var15 + "]=" + var7[var15]);
            //                }

            //                for (var15 = 0; var15 < var8.length; ++var15)
            //                {
            //                    IOut.debug(20, "vknots[" + var15 + "]=" + var8[var15]);
            //                }

            //                IOut.debug(20, "ustart=" + var16.ustart);
            //                IOut.debug(20, "uend=" + var16.uend);
            //                IOut.debug(20, "vstart=" + var16.vstart);
            //                IOut.debug(20, "vend=" + var16.vend);
            //            }

            //            return var16;
            //        }
            //        else
            //        {
            //            return null;
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }
        public class BoundingBox
        {
            public HS_Vector min = new HS_Vector();
            public HS_Vector max = new HS_Vector();

            public BoundingBox()
            {
            }
        }

        public class BrepVertex : Point
        {
            public new static readonly String uuid = "60B5DBC0-E660-11d3-BFE4-0010830122F0";
            public int vertexIndex;
            public List<int> edgeIndex;
            public double tolerance;

            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC0-E660-11d3-BFE4-0010830122F0");
            }

            public BrepVertex()
            {
            }

            public BrepVertex(int var1, HS_Vector var2) : base(var2)
            {

                this.vertexIndex = var1;
                this.edgeIndex = new List<int>();
                this.tolerance = 0.0D;
            }

            public void addEdgeIndex(int var1)
            {
                this.edgeIndex.Add(var1);
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                this.vertexIndex = I3dmImporter.readInt(var2);
                this.point = I3dmImporter.readPoint3(var2);
                this.edgeIndex = I3dmImporter.readArrayInt(var2);
                this.tolerance = I3dmImporter.readDouble(var2);
                Console.WriteLine(200 + "vertexIndex = " + this.vertexIndex);
                Console.WriteLine(200 + "point = " + this.point);
                String var3 = "";

                for (int var4 = 0; var4 < this.edgeIndex.Count; ++var4)
                {
                    var3 = var3 + this.edgeIndex[var4] + ", ";
                }

                Console.WriteLine(200 + "edgeIndex = " + var3);
                Console.WriteLine(200 + "tolerance = " + this.tolerance);
            }

            //public void write(Rhino3dmFile var1, Stream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeInt32(var2, this.vertexIndex, var3);
            //    I3dmExporter.writePoint(var2, this.point, var3);
            //    I3dmExporter.writeArrayInt(var2, this.edgeIndex, var3);
            //    I3dmExporter.writedouble(var2, this.tolerance, var3);
            //}
        }
        public class BrepVertexArray : List<BrepVertex>
        {

            public BrepVertexArray()
            {
            }

            public BrepVertexArray(int var1) : base(var1)
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var10 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var10);
                    int var5 = var4[0];
                    int var6 = var4[1];
                    if (var5 == 1)
                    {
                        int var7 = I3dmImporter.readInt(var10);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            BrepVertex var9 = new BrepVertex();
                            var9.read(var1, var10);
                            this.Add(var9);
                        }
                    }

                }
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //int var5 = this.Count;
            //I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //for (int var6 = 0; var6 < var5; ++var6)
            //{
            //    ((BrepVertex)this.get(var6)).write(var1, var4, var4.getCRC());
            //}

            //I3dmExporter.writeChunk(var2, var4.getChunk());
            //        }

        }

        public class BrepTrim : CurveProxy
        {
            public new static readonly String uuid = "60B5DBC2-E660-11d3-BFE4-0010830122F0";
            public static readonly int force32BitTrimType = -1;
            public int trimIndex;
            public int curve2Index;
            public int edgeIndex;
            public int[] vertexIndex;
            public bool rev3d;
            public BrepTrim.Type type;
            public Surface.ISO iso;
            public int loopIndex;
            public double[] tolerance;
            public double legacy2dTol;
            public double legacy3dTol;
            public Brep brep;

            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC2-E660-11d3-BFE4-0010830122F0");
            }

            public BrepTrim()
            {
            }

            //public BrepTrim(int var1, int var2, int var3, int var4, int var5, int var6, Interval var7, Interval var8, Brep var9, ITrimCurve var10)
            //{
            //    this.trimIndex = var1;
            //    this.curve2Index = var2;
            //    this.edgeIndex = var3;
            //    this.vertexIndex = new int[2];
            //    this.vertexIndex[0] = var4;
            //    this.vertexIndex[1] = var5;
            //    this.rev3d = false;
            //    this.type = Type.Boundary;
            //    this.loopIndex = var6;
            //    this.tolerance = new double[2];
            //    this.tolerance[0] = 0.0D;
            //    this.tolerance[1] = 0.0D;
            //    this.legacy2dTol = 0.0D;
            //    this.legacy3dTol = 0.0D;
            //    this.iso = this.getISOType(var10);
            //    this.setProxyCurveDomain(var7);
            //    this.setDomain(var8);
            //    this.brep = var9;
            //}

            //public Surface.ISO getISOType(ITrimCurve var1)
            //{
            //    if (this.isStraightOnX(var1))
            //    {
            //        if (var1.num() == 2)
            //        {
            //            if (var1.cp(0).yd() == 0.0D)
            //            {
            //                return Surface.ISO.SIso;
            //            }

            //            if (var1.cp(0).yd() == 1.0D)
            //            {
            //                return Surface.ISO.NIso;
            //            }
            //        }

            //        return Surface.ISO.xIso;
            //    }
            //    else if (this.isStraightOnY(var1))
            //    {
            //        if (var1.num() == 2)
            //        {
            //            if (var1.cp(0).xd() == 0.0D)
            //            {
            //                return Surface.ISO.WIso;
            //            }

            //            if (var1.cp(0).xd() == 1.0D)
            //            {
            //                return Surface.ISO.EIso;
            //            }
            //        }

            //        return Surface.ISO.yIso;
            //    }
            //    else
            //    {
            //        return Surface.ISO.NotIso;
            //    }
            //}

            //public bool isStraightOnX(ITrimCurve var1)
            //{
            //    for (int var2 = 1; var2 < var1.num(); ++var2)
            //    {
            //        if (!var1.cp(0).eqY(var1.cp(var2)))
            //        {
            //            return false;
            //        }
            //    }

            //    return true;
            //}

            //public bool isStraightOnY(ITrimCurve var1)
            //{
            //    for (int var2 = 1; var2 < var1.num(); ++var2)
            //    {
            //        if (!var1.cp(0).eqX(var1.cp(var2)))
            //        {
            //            return false;
            //        }
            //    }

            //    return true;
            //}

            public static Type readType(int var0)
            {
                switch (var0)
                {
                    case 0:
                        return Type.Unknown;
                    case 1:
                        return Type.Boundary;
                    case 2:
                        return Type.Mated;
                    case 3:
                        return Type.Seam;
                    case 4:
                        return Type.Singular;
                    case 5:
                        return Type.CrvOnSrf;
                    case 6:
                        return Type.PtOnSrf;
                    case 7:
                        return Type.Slit;
                    default:
                        return Type.Unknown;
                }
            }

            public static int getInt(Type var0)
            {
                switch (var0)
                {
                    case Type.Unknown:
                        return 0;
                    case Type.Boundary:
                        return 1;
                    case Type.Mated:
                        return 2;
                    case Type.Seam:
                        return 3;
                    case Type.Singular:
                        return 4;
                    case Type.CrvOnSrf:
                        return 5;
                    case Type.PtOnSrf:
                        return 6;
                    case Type.Slit:
                        return 7;
                    default:
                        return 0;
                }
            }

            public new bool reverse()
            {
                this.rev3d = !this.rev3d;
                return true;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                this.trimIndex = I3dmImporter.readInt(var2);
                this.curve2Index = I3dmImporter.readInt(var2);
                Interval var3 = I3dmImporter.readInterval(var2);
                this.setProxyCurveDomain(var3);
                this.setDomain(var3);
                this.edgeIndex = I3dmImporter.readInt(var2);
                this.vertexIndex = new int[2];
                this.vertexIndex[0] = I3dmImporter.readInt(var2);
                this.vertexIndex[1] = I3dmImporter.readInt(var2);
                this.rev3d = I3dmImporter.readInt(var2) != 0;
                this.type = readType(I3dmImporter.readInt(var2));
                this.iso = Surface.readIso(I3dmImporter.readInt(var2));
                this.loopIndex = I3dmImporter.readInt(var2);
                this.tolerance = new double[2];
                this.tolerance[0] = I3dmImporter.readDouble(var2);
                this.tolerance[1] = I3dmImporter.readDouble(var2);
                if (var1.version >= 3 && var1.openNurbsVersion >= 200206180)
                {
                    Interval var8 = this.proxyCurveDomain();

                    try
                    {
                        var8 = I3dmImporter.readInterval(var2);
                    }
                    catch (IOException var7)
                    {
                        var8 = this.proxyCurveDomain();
                    }

                    bool var9 = false;
                    byte[] var6 = I3dmImporter.readBytes(var2, 8);
                    if (var6[0] == 1)
                    {
                        var9 = true;
                    }

                    var6 = I3dmImporter.readBytes(var2, 24);
                    if (var9)
                    {
                        this.reverse();
                    }

                    this.setDomain(var8);
                }
                else
                {
                    HS_Vector var4 = I3dmImporter.readPoint3(var2);
                    HS_Vector var5 = I3dmImporter.readPoint3(var2);
                }

                this.legacy2dTol = I3dmImporter.readDouble(var2);
                this.legacy3dTol = I3dmImporter.readDouble(var2);
                Console.WriteLine(200 + " trimIndex = " + this.trimIndex);
                Console.WriteLine(200 + " curve2Index = " + this.curve2Index);
                Console.WriteLine(200 + " thisDomain = " + this.thisDomain);
                Console.WriteLine(200 + " realCurveDomain = " + this.realCurveDomain);
                Console.WriteLine(200 + " edgeIndex = " + this.edgeIndex);
                Console.WriteLine(200 + " vertexIndex = " + this.vertexIndex[0] + ", " + this.vertexIndex[1]);
                Console.WriteLine(200 + " rev3d = " + this.rev3d);
                Console.WriteLine(200 + " type = " + this.type);
                Console.WriteLine(200 + " iso = " + this.iso);
                Console.WriteLine(200 + " loopIndex = " + this.loopIndex);
                Console.WriteLine(200 + " tolerance = " + this.tolerance[0] + ", " + this.tolerance[1]);
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeInt32(var2, this.trimIndex, var3);
            //    I3dmExporter.writeInt32(var2, this.curve2Index, var3);
            //    I3dmExporter.writeInterval(var2, this.proxyCurveDomain(), var3);
            //    I3dmExporter.writeInt32(var2, this.edgeIndex, var3);
            //    I3dmExporter.writeInt32(var2, this.vertexIndex [0], var3);
            //    I3dmExporter.writeInt32(var2, this.vertexIndex [1], var3);
            //    I3dmExporter.writeInt32(var2, this.rev3d ? 1 : 0, var3);
            //    I3dmExporter.writeInt32(var2, getInt(this.type), var3);
            //    I3dmExporter.writeInt32(var2, Surface.getInt(this.iso), var3);
            //    I3dmExporter.writeInt32(var2, this.loopIndex, var3);
            //    I3dmExporter.writedouble(var2, this.tolerance [0], var3);
            //    I3dmExporter.writedouble(var2, this.tolerance [1], var3);
            //            if (var1.version < 3) {
            //        HS_Vector var4 = new HS_Vector(0.0D, 0.0D, 0.0D);
            //        I3dmExporter.writePoint(var2, var4, var3);
            //        I3dmExporter.writePoint(var2, var4, var3);
            //    } else {
            //        I3dmExporter.writeInterval(var2, this.domain(), var3);
            //        int var6 = this.proxyCurveIsReversed() ? 1 : 0;
            //        I3dmExporter.writeByte(var2, (byte)var6, var3);

            //        int var5;
            //        for (var5 = 0; var5 < 7; ++var5)
            //        {
            //            I3dmExporter.writeByte(var2, (byte)0, var3);
            //        }

            //        for (var5 = 0; var5 < 24; ++var5)
            //        {
            //            I3dmExporter.writeByte(var2, (byte)0, var3);
            //        }
            //    }

            //    I3dmExporter.writedouble(var2, this.legacy2dTol, var3);
            //    I3dmExporter.writedouble(var2, this.legacy3dTol, var3);
            //}

            public override String ToString()
            {
                return "trimIndex = " + this.trimIndex + "\ncurve2Index = " + this.curve2Index + "\nedgeIndex = " + this.edgeIndex + "\nvertexIndex[0] = " + this.vertexIndex[0] + "\nvertexIndex[1] = " + this.vertexIndex[1] + "\nrev3d = " + this.rev3d + "\ntype = " + this.type + "\niso = " + this.iso + "\nloopIndex = " + this.loopIndex + "\ntolerance[0] = " + this.tolerance[0] + "\ntolerance[1] = " + this.tolerance[1] + "\nlegacy2dTol = " + this.legacy2dTol + "\nlegacy3dTol = " + this.legacy3dTol + "\nbrep = " + this.brep;
            }

            public enum Type
            {
                Unknown,
                Boundary,
                Mated,
                Seam,
                Singular,
                CrvOnSrf,
                PtOnSrf,
                Slit,
                TrimTypeCount
            }
        }

        public class BrepTrimArray : List<BrepTrim>
        {
            public BrepTrimArray()
            {
            }

            public BrepTrimArray(int var1) : base(var1)
            {

            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var10 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var10);
                    int var5 = var4[0];
                    int var6 = var4[1];
                    if (var5 == 1)
                    {
                        int var7 = I3dmImporter.readInt(var10);

                        for (int var8 = 0; var8 < var7; ++var8)
                        {
                            BrepTrim var9 = new BrepTrim();
                            var9.read(var1, var10);
                            this.Add(var9);
                        }
                    }

                }
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)  {
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //    I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //    int var5 = this.Count;
            //    I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //    for (int var6 = 0; var6 < var5; ++var6)
            //    {
            //        ((BrepTrim)this.get(var6)).write(var1, var4, var4.getCRC());
            //    }

            //    I3dmExporter.writeChunk(var2, var4.getChunk());
            //}
        }

        public class BrepLoop : Geometry
        {
            public new static readonly String uuid = "60B5DBC3-E660-11d3-BFE4-0010830122F0";
            public int loopIndex;
            public List<int> trimIndex;
            public Type type;
            public int faceIndex;
            public BoundingBox pbox;
            public Brep brep;

            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC3-E660-11d3-BFE4-0010830122F0");
            }

            public BrepLoop()
            {
            }

            public BrepLoop(int var1, int var2, Type var3, Brep var4)
            {
                this.loopIndex = var1;
                this.faceIndex = var2;
                this.trimIndex = new List<int>();
                this.type = var3;
                this.brep = var4;
            }

            public void addTrimIndex(int var1)
            {
                this.trimIndex.Add(var1);
            }

            public static Type readType(int var0)
            {
                switch (var0)
                {
                    case 0:
                        return Type.Unknown;
                    case 1:
                        return Type.Outer;
                    case 2:
                        return Type.Inner;
                    case 3:
                        return Type.Slit;
                    case 4:
                        return Type.CrvOnSrf;
                    case 5:
                        return Type.PtOnSrf;
                    default:
                        return Type.Unknown;
                }
            }

            public static int getInt(Type var0)
            {
                switch (var0)
                {
                    case Type.Unknown:
                        return 0;
                    case Type.Outer:
                        return 1;
                    case Type.Inner:
                        return 2;
                    case Type.Slit:
                        return 3;
                    case Type.CrvOnSrf:
                        return 4;
                    case Type.PtOnSrf:
                        return 5;
                    default:
                        return 0;
                }
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                this.loopIndex = I3dmImporter.readInt(var2);
                this.trimIndex = I3dmImporter.readArrayInt(var2);
                this.type = readType(I3dmImporter.readInt(var2));
                this.faceIndex = I3dmImporter.readInt(var2);
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeInt32(var2, this.loopIndex, var3);
            //    I3dmExporter.writeArrayInt(var2, this.trimIndex, var3);
            //    I3dmExporter.writeInt32(var2, getInt(this.type), var3);
            //    I3dmExporter.writeInt32(var2, this.faceIndex, var3);
            //}

            public enum Type
            {
                Unknown,
                Outer,
                Inner,
                Slit,
                CrvOnSrf,
                PtOnSrf,
                TypeCount
            }
        }

        public class BrepLoopArray : List<BrepLoop>
        {
            public BrepLoopArray()
            {
            }

            public BrepLoopArray(int var1) : base(var1)
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var9 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var9);
                    int var5 = var4[0];
                    if (var5 == 1)
                    {
                        int var6 = I3dmImporter.readInt(var9);

                        for (int var7 = 0; var7 < var6; ++var7)
                        {
                            BrepLoop var8 = new BrepLoop();
                            var8.read(var1, var9);
                            this.Add(var8);
                        }
                    }

                }
            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3)
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //    I3dmExporter.writeChunkVersion(var4, 1, 0, var4.getCRC());
            //    int var5 = this.Count;
            //    I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //    for (int var6 = 0; var6 < var5; ++var6)
            //    {
            //        ((BrepLoop)this.get(var6)).write(var1, var4, var4.getCRC());
            //    }

            //    I3dmExporter.writeChunk(var2, var4.getChunk());
            //}
        }

        public class BrepFace : SurfaceProxy
        {
            public new static readonly String uuid = "60B5DBC4-E660-11d3-BFE4-0010830122F0";
            public int faceIndex;
            public List<int> loopIndex;
            public int surfaceIndex;
            public bool rev;
            public int faceMaterialChannel;
            public UUID faceUUID;
            public BoundingBox bbox;
            public Interval[] Domain;
            public Mesh renderMesh;
            public Mesh analysisMesh;
            public Mesh previewMesh;
            public Brep brep;

            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC4-E660-11d3-BFE4-0010830122F0");
            }

            public BrepFace()
            {
            }

            public BrepFace(int var1, int var2, Surface var3, Brep var4)
            {
                this.faceIndex = var1;
                this.surfaceIndex = var2;
                this.brep = var4;
                this.loopIndex = new List<int>();
                this.faceUUID = var3.getClassUUID();
                this.Domain = new Interval[2];
                this.Domain[0] = var3.domain(0);
                this.Domain[1] = var3.domain(1);
                this.rev = false;
                this.faceMaterialChannel = 0;
            }

            public void addLoopIndex(int var1)
            {
                this.loopIndex.Add(var1);
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                this.faceIndex = I3dmImporter.readInt(var2);
                this.loopIndex = I3dmImporter.readArrayInt(var2);
                this.surfaceIndex = I3dmImporter.readInt(var2);
                this.rev = I3dmImporter.readInt(var2) != 0;
                this.faceMaterialChannel = I3dmImporter.readInt(var2);
                if (this.faceMaterialChannel < 0)
                {
                    this.faceMaterialChannel = 0;
                }

            }

            //public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    I3dmExporter.writeInt32(var2, this.faceIndex, var3);
            //I3dmExporter.writeArrayInt(var2, this.loopIndex, var3);
            //I3dmExporter.writeInt32(var2, this.surfaceIndex, var3);
            //I3dmExporter.writeInt32(var2, this.rev? 1 : 0, var3);
            //I3dmExporter.writeInt32(var2, this.faceMaterialChannel, var3);
            //}
        }

        public class BrepFaceArray : List<BrepFace>
        {
            public BrepFaceArray()
            {
            }

            public BrepFaceArray(int var1) : base(var1)
            {
            }

            public void read(Rhino3dmFile var1, Stream var2)
            {
                Chunk var3 = I3dmImporter.readChunk(var2);
                if (var3.header != 1073774592)
                {
                    throw new IOException("invalid type code = " + hex(var3.header));
                }
                else
                {
                    MemoryStream var9 = new MemoryStream(var3.content);
                    int[] var4 = I3dmImporter.readChunkVersion(var9);
                    int var5 = var4[0];
                    if (var5 == 1)
                    {
                        int var6 = I3dmImporter.readInt(var9);

                        for (int var7 = 0; var7 < var6; ++var7)
                        {
                            BrepFace var8 = new BrepFace();
                            var8.read(var1, var9);
                            this.Add(var8);
                        }
                    }

                }
            }

            //        public void write(Rhino3dmFile var1, OutputStream var2, CRC32 var3) 
            //{
            //    ChunkOutputStream var4 = new ChunkOutputStream(1073774592);
            //I3dmExporter.writeChunkVersion(var4, 1, 1, var4.getCRC());
            //int var5 = this.Count;
            //I3dmExporter.writeInt32(var4, var5, var4.getCRC());

            //int var6;
            //for (var6 = 0; var6 < var5; ++var6)
            //{
            //    ((BrepFace)this.get(var6)).write(var1, var4, var4.getCRC());
            //}

            //for (var6 = 0; var6 < var5; ++var6)
            //{
            //    I3dmExporter.writeUUID(var4, ((BrepFace)this.get(var6)).faceUUID, var4.getCRC());
            //}

            //I3dmExporter.writeChunk(var2, var4.getChunk());
            //        }
        }
        public class Brep : Geometry
        {
            public new static readonly String uuid = "60B5DBC5-E660-11d3-BFE4-0010830122F0";
            public CurveArray curves2;
            public CurveArray curves3;
            public SurfaceArray surfaces;
            public BrepVertexArray vertices;
            public BrepEdgeArray edges;
            public BrepTrimArray trims;
            public BrepLoopArray loops;
            public BrepFaceArray faces;
            public BoundingBox bbox;
            public new UUID getClassUUID()
            {
                return new UUID("60B5DBC5-E660-11d3-BFE4-0010830122F0");
            }

            public new int getType()
            {
                return 16;
            }

            public Brep()
            {

            }
            public override IObject createIObject(Rhino3dmFile var0)
            {
                List<BrepVertex> vertices = this.vertices;

                List<int[]> faceList = new List<int[]>();
                List<HS_Polygon> polys = new List<HS_Polygon>();

                for (int i = 0; i < this.faces.Count; i++)
                {
                    BrepFace f = this.faces[i];

                    HS_Polygon poly;
                    BrepLoop lshell = this.loops[f.loopIndex[0]];

                    List<HS_Vector> shellpts = new List<HS_Vector>();

                    for (int j = 0; j < lshell.trimIndex.Count; j++)
                    {

                        var cl = this.trims[lshell.trimIndex[j]].cv.ToList();
                        cl.RemoveAt(0);
                        shellpts.AddRange(cl);
                    }
                    if (f.rev)
                    {
                        shellpts.Reverse();
                    }

                    if (f.loopIndex.Count == 1)
                    {
                        poly = new HS_Polygon().Create(shellpts);
                    }
                    else
                    {
                        List<List<HS_Vector>> holes = new List<List<HS_Vector>>();

                        for (int j = 1; j < f.loopIndex.Count; j++)
                        {
                            BrepLoop lhole = this.loops[f.loopIndex[j]];
                            List<HS_Vector> holepts = new List<HS_Vector>();
                            for (int k = 0; k < lhole.trimIndex.Count; k++)
                            {
                                var cl=this.trims[lhole.trimIndex[k]].cv.ToList();
                                cl.RemoveAt(0);
                                holepts.AddRange(cl);
                            }
                            if (!f.rev)
                            {
                                holepts.Reverse();
                            }

                            holes.Add(holepts);
                        }

                        poly = new HS_Polygon().Create(shellpts, holes.ToArray());
                    }

                    polys.Add(poly);
                }

                GEC_FromPolygons gecp = new GEC_FromPolygons();

                gecp.setPolygons(polys);
                GE_Mesh m = gecp.create();

                Console.WriteLine(m);
                m.attribute.name = "Mesh";
                return m;
            }
            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                if (var4 == 2)
                {
                    this.readOld200(var1, var2);
                }
                else if (var4 == 3)
                {
                    byte var6 = 100;
                    Console.WriteLine(var6 + " read curves2");
                    this.curves2 = new CurveArray();
                    this.curves2.read(var1, var2);
                    int var7 = this.curves2.Count;
                    Console.WriteLine(var6 + " curves2Count = " + var7);
                    Console.WriteLine(var6 + " read curves3");
                    this.curves3 = new CurveArray();
                    this.curves3.read(var1, var2);
                    int var8 = this.curves3.Count;
                    Console.WriteLine(var6 + " curves3Count = " + var8);
                    Console.WriteLine(var6 + " read surface");
                    this.surfaces = new SurfaceArray();
                    this.surfaces.read(var1, var2);
                    int var9 = this.surfaces.Count;
                    Console.WriteLine(var6 + " surfacesCount = " + var9);
                    Console.WriteLine(var6 + " read vertices");
                    this.vertices = new BrepVertexArray();
                    this.vertices.read(var1, var2);
                    Console.WriteLine(var6 + " verticesCount = " + this.vertices.Count);
                    Console.WriteLine(var6 + " read edges");
                    this.edges = new BrepEdgeArray();
                    this.edges.read(var1, var2);
                    Console.WriteLine(var6 + " edgeCount = " + this.edges.Count);

                    int var10;
                    bool var12;
                    Interval var13;
                    Interval var14;
                    for (var10 = 0; var10 < this.edges.Count; ++var10)
                    {
                        BrepEdge var11 = (BrepEdge)this.edges[var10];
                        var11.brep = this;
                        if (var11.curve3Index >= 0 && var11.curve3Index < var8)
                        {
                            var12 = var11.proxyCurveIsReversed();
                            var13 = var11.proxyCurveDomain();
                            var14 = var11.domain();
                            var11.setProxyCurve((Curve)this.curves3[var11.curve3Index], var13);
                            if (var12)
                            {
                                var11.reverse();
                            }

                            var11.setDomain(var14);
                        }
                    }

                    Console.WriteLine(var6 + " read trims");
                    this.trims = new BrepTrimArray();
                    this.trims.read(var1, var2);

                    foreach (BrepTrim t in this.trims)
                    {

                        t.cv = new HS_Vector[this.curves3[t.edgeIndex].cv.Length];
                        this.curves3[t.edgeIndex].cv.CopyTo(t.cv, 0);
                        if (t.rev3d)
                        {
                            Array.Reverse(t.cv);
                        }
                    }


                    Console.WriteLine(var6 + " trimCount = " + this.trims.Count);

                    for (var10 = 0; var10 < this.trims.Count; ++var10)
                    {
                        BrepTrim var15 = (BrepTrim)this.trims[var10];
                        var15.brep = this;
                        if (var15.curve2Index >= 0 && var15.curve2Index < var7)
                        {
                            var12 = var15.proxyCurveIsReversed();
                            var13 = var15.proxyCurveDomain();
                            var14 = var15.domain();
                            var15.setProxyCurve((Curve)this.curves2[var15.curve2Index], var13);
                            if (var12)
                            {
                                var15.reverse();
                            }

                            var15.setDomain(var14);
                        }
                    }

                    Console.WriteLine(var6 + " read loops");
                    this.loops = new BrepLoopArray();
                    this.loops.read(var1, var2);
                    Console.WriteLine(var6 + " loopCount=" + this.loops.Count);

                    for (var10 = 0; var10 < this.loops.Count; ++var10)
                    {
                        ((BrepLoop)this.loops[var10]).brep = this;
                    }

                    Console.WriteLine(var6 + " read faces");
                    this.faces = new BrepFaceArray();
                    this.faces.read(var1, var2);
                    Console.WriteLine(var6 + " facesCount=" + this.faces.Count);

                    for (var10 = 0; var10 < this.faces.Count; ++var10)
                    {
                        BrepFace var16 = (BrepFace)this.faces[var10];
                        var16.brep = this;
                        if (var16.surfaceIndex >= 0 && var16.surfaceIndex < var9)
                        {
                            var16.setProxySurface((Surface)this.surfaces[var16.surfaceIndex]);
                        }
                    }

                    this.bbox = new BoundingBox();
                    this.bbox.min = I3dmImporter.readPoint3(var2);
                    this.bbox.max = I3dmImporter.readPoint3(var2);
                }
            }

            public void readOld200(Rhino3dmFile var1, Stream var2)
            {
                int var3 = I3dmImporter.readInt(var2);
                int var4 = I3dmImporter.readInt(var2);
                int var5 = I3dmImporter.readInt(var2);
                int var6 = I3dmImporter.readInt(var2);
                int var7 = I3dmImporter.readInt(var2);
                this.bbox.min = I3dmImporter.readPoint3(var2);
                this.bbox.max = I3dmImporter.readPoint3(var2);
                this.curves2 = new CurveArray(var6);

                for (int var8 = 0; var8 < var6; ++var8)
                {
                    PolyCurve var9 = new PolyCurve();
                    var9.read(var1, var2);
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

            public override int GetHashCode()
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
                    var1[var2] = hexStringToByte(var0.Substring(var2 * 2, 2));
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
        public class Layer : RhinoObject
        {
            public new static readonly String uuid = "95809813-E985-11d3-BFE5-0010830122F0";
            public int layerIndex;
            public UUID layerId;
            public UUID parentLayerId;
            public int igesLevel;
            public int materialIndex;
            public int linetypeIndex;
            public IColor color;
            public UUID displayMaterialId;
            public IColor plotColor;
            public double plotWeightMm;
            public String name;
            public bool visible;
            public bool locked;
            public bool expanded;
            //public RenderingAttributes renderingAttributes;
            public ILayer ilayer;

            public new UUID getClassUUID()
            {
                return new UUID("95809813-E985-11d3-BFE5-0010830122F0");
            }

            public new int getType()
            {
                return 64;
            }

            public Layer()
            {
                this.layerId = null;
                this.parentLayerId = null;
                this.layerIndex = -1;
                this.igesLevel = -1;
                this.materialIndex = -1;
                this.linetypeIndex = -1;
                this.color = new IColor(0, 0, 0);
                this.displayMaterialId = null;
                this.plotColor = new IColor(255, 255, 255);
                this.plotWeightMm = 0.0D;
                this.visible = true;
                this.locked = false;
                this.expanded = true;
                //this.renderingAttributes = new RenderingAttributes();
            }

            public Layer(ILayer var1, int var2)
            {
                this.layerId = UUID.randomUUID();
                this.parentLayerId = null;
                this.layerIndex = var2;
                this.igesLevel = -1;
                this.materialIndex = -1;
                this.linetypeIndex = -1;
                this.color = var1.clr();
                this.displayMaterialId = null;
                this.plotColor = new IColor(255, 255, 255, 0);
                this.plotWeightMm = 0.0D;
                this.visible = var1.visible();
                this.locked = false;
                this.expanded = true;
                //this.renderingAttributes = new RenderingAttributes();
                this.name = var1.name();
                this.ilayer = var1;
            }

            public override void read(Rhino3dmFile var1, Stream var2)
            {
                int[] var3 = I3dmImporter.readChunkVersion(var2);
                int var4 = var3[0];
                int var5 = var3[1];
                int var6;
                if (var4 == 1)
                {
                    var6 = I3dmImporter.readInt(var2);
                    switch (var6)
                    {
                        case 0:
                            this.visible = true;
                            this.locked = false;
                            break;
                        case 1:
                            this.visible = false;
                            this.locked = false;
                            break;
                        case 2:
                            this.visible = true;
                            this.locked = true;
                            break;
                        default:
                            this.visible = true;
                            this.locked = false;
                            break;
                    }
                }

                this.layerIndex = I3dmImporter.readInt(var2);
                this.igesLevel = I3dmImporter.readInt(var2);
                this.materialIndex = I3dmImporter.readInt(var2);
                var6 = I3dmImporter.readInt(var2);
                this.color = I3dmImporter.readColor(var2);
                I3dmImporter.readShort(var2);
                I3dmImporter.readShort(var2);
                I3dmImporter.readDouble(var2);
                I3dmImporter.readDouble(var2);
                this.name = I3dmImporter.readString(var2);
                if (var5 >= 1)
                {
                    this.visible = I3dmImporter.readBool(var2);
                    if (var5 >= 2)
                    {
                        this.linetypeIndex = I3dmImporter.readInt(var2);
                        if (var5 >= 3)
                        {
                            this.plotColor = I3dmImporter.readColor(var2);
                            this.plotWeightMm = I3dmImporter.readDouble(var2);
                            if (var5 >= 4)
                            {
                                this.locked = I3dmImporter.readBool(var2);
                                if (var5 >= 5)
                                {
                                    this.layerId = I3dmImporter.readUUID(var2);
                                    if (var5 >= 6 && var1.openNurbsVersion > 200505110)
                                    {
                                        this.parentLayerId = I3dmImporter.readUUID(var2);
                                        this.expanded = I3dmImporter.readBool(var2);
                                    }

                                    if (var5 >= 7)
                                    {
                                        //this.renderingAttributes = new RenderingAttributes();
                                        //this.renderingAttributes.read(var1, var2);
                                        if (var5 >= 8)
                                        {
                                            this.displayMaterialId = I3dmImporter.readUUID(var2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            //public void write(Rhino3dmFile var1, Stream var2, CRC32 var3)
            //{
            //    IRhino3dmExporter.writeChunkVersion(var2, 1, 8, var3);
            //    bool var4 = false;
            //    byte var6;
            //    if (this.visible)
            //    {
            //        var6 = 0;
            //    }
            //    else if (this.locked)
            //    {
            //        var6 = 2;
            //    }
            //    else
            //    {
            //        var6 = 1;
            //    }

            //    IRhino3dmExporter.writeInt32(var2, var6, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.layerIndex, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.igesLevel, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.materialIndex, var3);
            //    var6 = 0;
            //    IRhino3dmExporter.writeInt32(var2, var6, var3);
            //    IRhino3dmExporter.writeColor(var2, this.color, var3);
            //    byte var5 = 0;
            //    IRhino3dmExporter.writeInt16(var2, var5, var3);
            //    IRhino3dmExporter.writeInt16(var2, var5, var3);
            //    IRhino3dmExporter.writeDouble(var2, 0.0D, var3);
            //    IRhino3dmExporter.writeDouble(var2, 1.0D, var3);
            //    IRhino3dmExporter.writeString(var2, this.name, var3);
            //    IRhino3dmExporter.writeBool(var2, this.visible, var3);
            //    IRhino3dmExporter.writeInt32(var2, this.linetypeIndex, var3);
            //    IRhino3dmExporter.writeColor(var2, this.plotColor, var3);
            //    IRhino3dmExporter.writeDouble(var2, this.plotWeightMm, var3);
            //    IRhino3dmExporter.writeBool(var2, this.locked, var3);
            //    IRhino3dmExporter.writeUUID(var2, this.layerId, var3);
            //    IRhino3dmExporter.writeUUID(var2, this.parentLayerId, var3);
            //    IRhino3dmExporter.writeBool(var2, this.expanded, var3);
            //    this.renderingAttributes.write(var1, var2, var3);
            //    IRhino3dmExporter.writeUUID(var2, this.displayMaterialId, var3);
            //}

            public new ILayer createIObject(Rhino3dmFile var1)
            {
                if (this.name == null)
                {
                    throw new Exception("layer name is null. no layer is instantiated.");
                    return null;
                }
                else
                {
                    //this.ilayer = var2.server().getLayer(this.name);
                    if (this.color != null)
                    {
                        this.ilayer.setColor(this.color);
                    }

                    this.ilayer.setVisible(this.visible);
                    return this.ilayer;
                }
            }

            public String toString()
            {
                return "Layer : name " + this.name + "\nlayerIndex = " + this.layerIndex + "\nlayerId = " + this.layerId + "\nparentLayerId = " + this.parentLayerId + "\nigesLevel = " + this.igesLevel + "\nmaterialIndex = " + this.materialIndex + "\nlinetypeIndex = " + this.linetypeIndex + "\ncolor = " + this.color + "\ndisplayMaterialId = " + this.displayMaterialId + "\nplotColor = " + this.plotColor + "\nplotWeightMm = " + this.plotWeightMm + "\nvisible = " + this.visible + "\nlocked = " + this.locked + "\nexpanded = " + this.expanded + "\nRenderingAttributes = " +
                    //this.renderingAttributes 
                    //+
                    "\n";
            }
        }
        public class Rhino3dmFile
        {
            public int version;
            public uint openNurbsVersion;
            //public Rhino3dm.StartSection startSection;
            public Properties properties;
            //public Rhino3dm.Settings settings;
            //public Rhino3dm.Bitmap[] bitmaps;
            //public Rhino3dm.TextureMapping[] textureMappings;
            //public List<Rhino3dm.Material> materials;
            //public List<IMaterial> imaterials;
            //public Rhino3dm.Linetype[] linetypes;
            public Rhino3dm.Layer[] layers;
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
            public Rhino3dmFile(int var1, uint var2)
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
        public class Properties
        {
            public uint openNurbsVersion = 0;
            public byte[] notes;
            public byte[] previewImage;
            public byte[] application;
            public byte[] compressedPreviewImage;
            public byte[] revisionHistory;

            public Properties()
            {
            }

            public void setOpenNurbsVersion(Chunk var1)
            {
                var s = var1.body.ToString("X");
                var ver = Convert.ToUInt32(s, 16);
                this.openNurbsVersion = ver;
                Console.WriteLine(this.openNurbsVersion);
            }

            public void setNotes(Chunk var1)
            {
                this.notes = var1.content;
            }

            public void setPreviewImage(Chunk var1)
            {
                this.previewImage = var1.content;
            }

            public void setApplication(Chunk var1)
            {
                this.application = var1.content;
            }

            public void setCompressedPreviewImage(Chunk var1)
            {
                this.compressedPreviewImage = var1.content;
            }

            public void setRevisionHistory(Chunk var1)
            {
                this.revisionHistory = var1.content;
            }
        }


        public class UserData : RhinoObject
        {
            public new static readonly String uuid = "850324A7-050E-11d4-BFFA-0010830122F0";

            public UserData()
            {
            }

            public new UUID getClassUUID()
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
