using Lucene.Net.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hsy.Geo;
using ICSharpCode.SharpZipLib.Zip.Compression;
using Inflater = ICSharpCode.SharpZipLib.Zip.Compression.Inflater;
using System.Text.RegularExpressions;

namespace Hsy.IO
{
    public class I3dmImporter : Rhino3dm
    {
        private Stream InputStream;
        private static int version;
        public Rhino3dmFile file;
        public List<IObject> objects;
        public I3dmImporter(Stream var1)
        {
            this.InputStream = var1;
        }


        public void readFileHeader()
        {
            byte[] var3 = new byte[24];
            byte[] var4 = new byte[8];
            this.InputStream.Read(var3, 0, 24);
            this.InputStream.Read(var4, 0, 8);
            //int version = 0;
            try
            {
                String var5 = Encoding.Default.GetString(var4);
                Console.WriteLine(var5);
                String[] var6 = var5.Split('0');
                var5 = var6[0];

                if (var5.Equals("X"))
                {
                    version = 2;
                }
                else
                {
                    //Console.WriteLine("3DM FILE VERSION = " + var5.ToCharArray()[0].ToString());
                    version = int.Parse(var5);
                    this.file.version = version;


                }

                Console.WriteLine("3DM FILE VERSION = " + version);
            }
            catch (FormatException var7)
            {
                throw new Exception(var7.StackTrace);
            }
        }
        public static List<IObject> read(string var0)
        {
            try
            {
                Stream var2 = new FileStream(var0, FileMode.Open, FileAccess.Read);
                List<IObject> var3 = read(var2);
                if (var2 != null)
                {
                    var2.Close();
                }
                return var3;
            }
            catch (IOException var4)
            {
                Console.WriteLine(var4.StackTrace);
                return null;
            }
        }

        public static List<IObject> read(Stream var0)
        {
            I3dmImporter var2 = new I3dmImporter(var0);
            var2.read();
            return var2.objects;
        }
        public void read()
        {
            this.file = new Rhino3dmFile();
            ClassRegistry.init();
            this.objects = new List<IObject>();
            try
            {
                this.readFileHeader();
                Chunk var1 = null;
                int var2 = 0;
                bool end = false;
                do
                {
                    var2++;
                    Console.WriteLine("\n" + 10 + " chunk #" + var2);
                    Console.WriteLine("pre position:  " + this.InputStream.Position);
                    Console.WriteLine("pre length:    " + this.InputStream.Length);
                    try
                    {
                        var1 = readChunk(this.InputStream);
                        if (var1 == null)
                        {
                            throw new Exception("chunk is null");

                        }
                        else
                        {
                            switch (var1.header)
                            {
                                case 1:
                                    this.readStartSection(var1);
                                    break;
                                case 268435476:
                                    this.readPropertiesTable(var1);
                                    break;
                                case 268435475:
                                    this.readObjectTable(var1);
                                    break;
                                case 32767:
                                    this.readEndMark(var1);
                                    break;
                                    //default:
                                    //    throw new Exception("unknown type code: " + hex(var1.header));
                                    //    break;

                            }
                            Console.WriteLine("post position:  " + this.InputStream.Position);
                            Console.WriteLine("post length:    " + this.InputStream.Length);
                            if (this.InputStream.Position == this.InputStream.Length)
                            {
                                var1 = null;
                            }

                        }
                    }
                    catch (IOException var5)
                    {
                        Console.WriteLine(var5.StackTrace);
                        var1 = null;
                    }
                } while (var1 != null);
            }
            catch (IOException var6)
            {
                Console.WriteLine(var6.StackTrace);
            }
        }

        public void readPropertiesTable(Chunk var1)
        {
            Console.WriteLine(10 + " Rhino3dmImporter.readProperties");
            Chunk[] var2 = readChunkTable(var1);
            if (var2 == null)
            {
                throw new Exception("no table item is found");
            }
            else
            {
                Properties var3 = new Properties();
                Chunk[] var4 = var2;
                int var5 = var2.Length;

                for (int var6 = 0; var6 < var5; ++var6)
                {
                    Chunk var7 = var4[var6];
                    switch (var7.header)
                    {
                        case -1610612698:
                            var3.setOpenNurbsVersion(var7);
                            this.file.openNurbsVersion = var3.openNurbsVersion;
                            break;
                        case -1:
                            var7 = null;
                            break;
                        case 536903713:
                            var3.setRevisionHistory(var7);
                            break;
                        case 536903714:
                            var3.setNotes(var7);
                            break;
                        case 536903715:
                            var3.setPreviewImage(var7);
                            break;
                        case 536903716:
                            var3.setApplication(var7);
                            break;
                        case 536903717:
                            var3.setCompressedPreviewImage(var7);
                            break;
                            //default:
                            //    throw new Exception("unknown type code: " + hex(var7.header));
                            //    break;
                    }
                }

                this.file.properties = var3;
            }
        }


        public void readObjectTable(Chunk var1)
        {
            Console.WriteLine(10 + " Rhino3dmImporter.readObjectTable");
            Chunk[] var2 = readChunkTable(var1);
            if (var2 != null)
            {
                List<RhinoObject> var3 = new List<RhinoObject>();
                Console.WriteLine(10 + "num of rhino objects : " + var2.Length);
                int var4 = 0;
                Chunk[] var5 = var2;
                int var6 = var2.Length;

                for (int var7 = 0; var7 < var6; ++var7)
                {
                    Chunk var8 = var5[var7];
                    if (var4 > 0 && var4 % 100 == 0)
                    {
                        StringBuilder var10001 = new StringBuilder();
                        ++var4;
                        Console.WriteLine(1 + var10001.Append(var4).Append("/").Append(var2.Length).ToString());
                    }

                    RhinoObject var9 = this.readRhinoObject(var8);
                    if (var9 != null)
                    {
                        var3.Add(var9);
                    }
                }

                if (var3.Count > 0)
                {
                    this.file.rhinoObjects = (RhinoObject[])var3.ToArray();
                }

            }
        }

        public void readEndMark(Chunk var1)
        {
            Console.WriteLine(10 + " Rhino3dmImporter.readEndMark");

            try
            {
                MemoryStream var2 = new MemoryStream(var1.content);
                if (version > 4)
                {
                    this.InputStream.Position += 4;
                    Console.WriteLine("corrected position: " + this.InputStream.Position);
                    var2.Position += 4;
                }

                if (this.file.sizeOfChunkLength() == 4)
                {
                    int var3 = readInt(var2);
                    Console.WriteLine(var3);
                }
                else
                {
                    long var6 = readLong(var2);
                    Console.WriteLine(var6);
                }
            }
            catch (IOException var5)
            {
                Console.WriteLine(var5.StackTrace);
            }

        }


        public RhinoObject readRhinoObject(Chunk var1)
        {
            if (version == 1)
            {
                return null;
            }
            else if (var1.header != 536903792)
            {
                throw new Exception("chunk header is not tcodeObjectRecord\n" + var1);
                return null;
            }
            else
            {
                Chunk[] var2 = readChunkTable(var1, -2113929089);
                if (var2 == null)
                {
                    throw new Exception("no table item is found");
                    return null;
                }
                else if (var2[0].header != -2113929103)
                {
                    throw new Exception("chunk header is not tcodeObjectRecordType\n" + var2[0]);
                    return null;
                }
                else if (var2.Length < 2)
                {
                    throw new Exception("object data is missing");
                    return null;
                }
                else
                {
                    RhinoObject var3 = this.readObject(var2[1]);
                    if (var3 == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (var2.Length >= 3)
                        {
                            for (int var4 = 2; var4 < var2.Length; ++var4)
                            {
                                if (var2[var4].header == 33587314)
                                {
                                    ObjectAttributes var5 = new ObjectAttributes();

                                    try
                                    {
                                        var5.read(this.file, var2[var4].content);
                                    }
                                    catch (IOException var7)
                                    {
                                        Console.WriteLine(var7.StackTrace);
                                    }

                                    var3.setAttributes(var5);
                                }
                                else if (var2[var4].header == 33554547)
                                {
                                    this.readObjectUserData(var2[var4]);
                                }
                            }
                        }

                        IObject var8 = var3.createIObject(this.file);
                        if (var8 != null)
                        {
                            var3.setAttributesToIObject(this.file, var8);
                            this.objects.Add(var8);
                        }

                        return var3;
                    }
                }
            }
        }
        public void readObjectUserData(Chunk var1)
        {
            try
            {
                MemoryStream var2 = new MemoryStream(var1.content);
                Chunk var3 = readChunk(var2);
                MemoryStream var4 = new MemoryStream(var3.content);
                int[] var5 = readChunkVersion(var4);
                int var6 = var5[0];
                int var7 = var5[1];
                MemoryStream var8 = var4;
                if (var6 == 2)
                {
                    Chunk var9 = readChunk(var4);
                    var8 = new MemoryStream(var9.content);
                }

                UUID var18 = readUUID((Stream)var8);
                UUID var10 = readUUID((Stream)var8);
                int var11 = readInt(var8);
                Xform var12 = readXform(var8);
                throw new Exception("classID = " + var18);
                throw new Exception("itemID = " + var10);
                throw new Exception("copyCount = " + var11);
                throw new Exception("xform = " + var12);
                if (var6 == 2 && var7 >= 1)
                {
                    UUID var13 = readUUID((Stream)var8);
                    throw new Exception("appID = " + var13);
                    if (var7 >= 2)
                    {
                        bool var14 = readBool(var8);
                        int var15 = readInt(var8);
                        int var16 = readInt(var8);
                        throw new Exception("unknown data = " + var14);
                        throw new Exception("3dm version = " + var15);
                        throw new Exception("ON version = " + var16);
                    }
                }

                Chunk var19 = readChunk(var4);
                throw new Exception("anonymousDataChunk : " + var19);
            }
            catch (IOException var17)
            {
                Console.WriteLine(var17.StackTrace);
            }

        }
        public RhinoObject readObject(Stream var1)
        {
            return readObject(this.file, readChunk(var1));
        }

        public RhinoObject readObject(Chunk var1)
        {
            return readObject(this.file, var1);
        }

        public static RhinoObject readObject(Rhino3dmFile var0, Stream var1)
        {
            Chunk temp = readChunk(var1);
            return readObject(var0, temp);
        }

        public static RhinoObject readObject(Rhino3dmFile var0, Chunk var1)
        {
            if (var1 != null && var1.header == 163834)
            {
                Chunk[] var2 = readChunkTable(var1, -2147319809);
                if (var2 == null)
                {
                    throw new Exception("chunk table is null");
                    return null;
                }
                else
                {
                    try
                    {
                        for (int var3 = 0; var3 < var2.Length - 1; var3++)
                        {
                            if (var2[var3].header == 196603)
                            {
                                UUID var4 = readUUID(var2[var3]);
                                Console.WriteLine("uuid= " + var4);
                                Type var5 = ClassRegistry.get(var4);
                                Console.WriteLine("uuid Class= " + var5);
                                if (var5 != null)
                                {
                                    RhinoObject var6 = null;
                                    try
                                    {
                                        var6 = (RhinoObject)System.Activator.CreateInstance(var5);
                                    }
                                    catch (Exception var8)
                                    {
                                        Console.WriteLine(var8.StackTrace);
                                    }
                                    if (var6 != null && var2[var3 + 1].header == 196604)
                                    {
                                        byte[] var7 = var2[var3 + 1].content;
                                        MemoryStream m = new MemoryStream(var7);
                                        if (version > 4)
                                        {
                                            m.Position += 4;
                                        }
                                        var6.read(var0, m);
                                        //var6.read(var0, var7);
                                        return var6;
                                    }
                                }
                            }
                            else if (var2[var3].header == 163837)
                            {
                                Console.WriteLine(20 + "user data = \n" + var2[var3]);
                            }
                        }
                    }
                    catch (EndOfStreamException var9)
                    {

                    }
                    catch (IOException var10)
                    {
                        Console.WriteLine(var10.StackTrace);
                    }
                    throw new Exception("no UUID or ClassData is found");
                    return null;
                }
            }
            else
            {
                throw new Exception("no tcodeOpenNurbsClass block found");
                return null;
            }
        }
        public static int[] readChunkVersion(Stream var0)
        {
            return readChunkVersion(readByte(var0));
        }

        public static int[] readChunkVersion(byte var0)
        {
            int var1 = var0 & 15;
            int var2 = (var0 & 240) >> 4;
            return new int[] { var2, var1 };
        }


        public static Interval readInterval(Stream var0)
        {

            Interval var1 = new Interval();
            var1.v1 = readDouble(var0);
            var1.v2 = readDouble(var0);
            return var1;
        }

        public static Xform readXform(Stream var0)
        {
            double[][] var1 = new double[4][];

            for (int var2 = 0; var2 < 4; ++var2)
            {
                var1[var2] = new double[4];
                for (int var3 = 0; var3 < 4; ++var3)
                {
                    var1[var2][var3] = readDouble(var0);
                }
            }

            return new Xform(var1);
        }
        public static Line readLine(Stream var0)
        {
            Line var1 = new Line();
            var1.from = readPoint3(var0);
            var1.to = readPoint3(var0);
            return var1;
        }
        public static Plane readPlane(Stream var0)
        {
            Plane var1 = new Plane();
            var1.origin = readPoint3(var0);
            var1.xaxis = readPoint3(var0);
            var1.yaxis = readPoint3(var0);
            var1.zaxis = readPoint3(var0);
            PlaneEquation var2 = new PlaneEquation();
            var2.x = readDouble(var0);
            var2.y = readDouble(var0);
            var2.z = readDouble(var0);
            var2.d = readDouble(var0);
            var1.planeEquation = var2;
            return var1;
        }

        public static PointArray readPointArray(Stream var0)
        {
            int var1 = readInt(var0);
            PointArray var2 = new PointArray(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readPoint3(var0));
            }

            return var2;
        }

        public static Polyline readPolyline(Stream var0)
        {
            int var1 = readInt(var0);
            Polyline var2 = new Polyline(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readPoint3(var0));
            }

            return var2;
        }

        public static Circle readCircle(Stream var0)
        {
            Circle var1 = new Circle();
            var1.plane = readPlane(var0);
            var1.radius = readDouble(var0);
            HS_Vector var2 = readPoint3(var0);
            var2 = readPoint3(var0);
            var2 = readPoint3(var0);
            return var1;
        }

        public static Arc readArc(Stream var0)
        {
            Arc var1 = new Arc();
            var1.plane = readPlane(var0);
            var1.radius = readDouble(var0);
            HS_Vector var2 = readPoint3(var0);
            var2 = readPoint3(var0);
            var2 = readPoint3(var0);
            var1.angle = readInterval(var0);
            return var1;
        }


        public static BoundingBox readBoundingBox(Stream var0)
        {
            BoundingBox var1 = new BoundingBox();
            var1.min.xd = readDouble(var0);
            var1.min.yd = readDouble(var0);
            var1.min.zd = readDouble(var0);
            var1.max.xd = readDouble(var0);
            var1.max.yd = readDouble(var0);
            var1.max.zd = readDouble(var0);
            return var1;
        }

        public static byte[] readCompressedBuffer(Stream var0, int var1)
        {
            bool var2 = false;
            bool var3 = false;
            bool var4 = false;
            int var5 = readInt(var0);
            byte var6 = readByte(var0);
            if (var6 == 0)
            {
                return readBytes(var0, var1);
            }
            else if (var6 == 1)
            {
                return readInflate(var0, var1);
            }
            else
            {
                throw new IOException("method is invalid value: " + var6.ToString());
            }
        }

        public static byte[] readInflate(Stream var0, int var1)
        {
            //var0.Position += 8; 
            Chunk var2 = readChunk(var0);
            if (var2 == null)
            {
                throw new Exception("no chunk is read");
                return null;
            }
            else if (var2.header != 1073774592)
            {
                throw new IOException("invalid type code = " + hex(var2.header));
            }
            else if (var2.body <= 4)
            {
                throw new IOException("chunk length is too short = " + var2.body);
            }
            else
            {

                int var3 = var2.body - 4;
                Inflater var4 = new Inflater();
                var4.SetInput(var2.content, 0, var3);
                byte[] var5 = new byte[var1];

                try
                {
                    var4.Inflate(var5, 0, var1);
                    return var5;
                }
                catch (FormatException var7)
                {
                    throw new IOException("DataFormatException:" + var7.Message);
                }
            }
        }

        public static int readCompressedBufferSize(Stream var0)
        {
            return readSize(var0);
        }

        public static int readSize(Stream var0)
        {
            return readInt(var0);
        }

        public static HS_Vector readPoint2(Stream var0)
        {
            HS_Vector var1 = new HS_Vector();
            var1.xd = readDouble(var0);
            var1.yd = readDouble(var0);
            return var1;
        }

        public static HS_Vector readPoint2f(Stream var0)
        {
            HS_Vector var1 = new HS_Vector();
            var1.xd = (double)readFloat(var0);
            var1.yd = (double)readFloat(var0);
            return var1;
        }

        public static HS_Vector readPoint3(Stream var0)
        {
            HS_Vector var1 = new HS_Vector();
            var1.xd = readDouble(var0);
            var1.yd = readDouble(var0);
            var1.zd = readDouble(var0);
            return var1;
        }
        public static float Hex2Float()
        {
            string s = "43DD7D00";
            MatchCollection matches = Regex.Matches(s, @"[0-9A-Fa-f]{2}");
            byte[] bytes = new byte[matches.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(matches[i].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
            float m = BitConverter.ToSingle(bytes.Reverse().ToArray(), 0);
            Console.WriteLine(m);
            return m;
        }

        public static HS_Vector readPoint3f(Stream var0)
        {
            HS_Vector var1 = new HS_Vector();
            //byte[] xb = new byte[4];
            //var0.Read(xb, 0, 4);
            //float fTemp = BitConverter.ToSingle(xb, 0);

            //int i=readInt(var0);
            ////float f = xb[0] | xb[1] << 8 | xb[2] << 16 | xb[3] << 24;
            //Hex2Float();
            var1.xd = (double)readFloat(var0);
            var1.yd = (double)readFloat(var0);
            var1.zd = (double)readFloat(var0);
            return var1;
        }
        public static SurfaceCurvature readSurfaceCurvature(Stream var0)
        {
            SurfaceCurvature var1 = new SurfaceCurvature();
            var1.k1 = readDouble(var0);
            var1.k2 = readDouble(var0);
            return var1;
        }
        public static List<SurfaceCurvature> readArraySurfaceCurvature(byte[] var0, int var1)
        {
            MemoryStream var2 = new MemoryStream(var0);
            List<SurfaceCurvature> var3 = new List<SurfaceCurvature>(var1);

            for (int var4 = 0; var4 < var1; ++var4)
            {
                var3.Add(readSurfaceCurvature(var2));
            }

            return var3;
        }

        public static List<HS_Vector> readArrayPoint2f(Stream var0)
        {
            int var1 = readInt(var0);
            List<HS_Vector> var2 = new List<HS_Vector>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readPoint2f(var0));
            }

            return var2;
        }

        public static List<HS_Vector> readArrayPoint2f(byte[] var0, int var1)
        {
            MemoryStream var2 = new MemoryStream(var0);
            List<HS_Vector> var3 = new List<HS_Vector>(var1);

            for (int var4 = 0; var4 < var1; ++var4)
            {
                var3.Add(readPoint2f(var2));
            }

            return var3;
        }

        public static List<HS_Vector> readArrayPoint3f(Stream var0)
        {
            int var1 = readInt(var0);
            List<HS_Vector> var2 = new List<HS_Vector>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readPoint3f(var0));
            }

            return var2;
        }

        public static List<HS_Vector> readArrayPoint3f(byte[] var0, int var1)
        {
            MemoryStream var2 = new MemoryStream(var0);
            List<HS_Vector> var3 = new List<HS_Vector>(var1);

            for (int var4 = 0; var4 < var1; ++var4)
            {
                var3.Add(readPoint3f(var2));
            }

            return var3;
        }

        public static List<HS_Vector> readArrayPoint2(byte[] var0, int var1)
        {
            MemoryStream var2 = new MemoryStream(var0);
            List<HS_Vector> var3 = new List<HS_Vector>(var1);

            for (int var4 = 0; var4 < var1; ++var4)
            {
                var3.Add(readPoint2(var2));
            }

            return var3;
        }

        public static List<SurfaceCurvature> readArraySurfaceCurvature(Stream var0)
        {
            int var1 = readInt(var0);
            List<SurfaceCurvature> var2 = new List<SurfaceCurvature>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readSurfaceCurvature(var0));
            }

            return var2;
        }
        public static List<IColor> readArrayColor(byte[] var0, int var1)
        {
            MemoryStream var2 = new MemoryStream(var0);
            List<IColor> var3 = new List<IColor>(var1);

            for (int var4 = 0; var4 < var1; ++var4)
            {
                var3.Add(readColor(var2));
            }

            return var3;
        }
        public static List<IColor> readArrayColor(Stream var0)
        {
            int var1 = readInt(var0);
            List<IColor> var2 = new List<IColor>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readColor(var0));
            }

            return var2;
        }
        public static IColor readColor(int var0)
        {
            int var1 = var0 & 255;
            int var2 = var0 >> 8 & 255;
            int var3 = var0 >> 16 & 255;
            int var4 = var0 >> 24 & 255;
            return new IColor(var1, var2, var3, 255 - var4);
        }
        public static IColor readColor(Stream var0)
        {
            return readColor(readInt32(var0));
        }

        public unsafe static String readString(Stream var0)
        {
            int var2 = readInt(var0);
            if (var2 == 0)
            {
                return "";
            }
            else if (var2 < 65535)
            {
                byte[] var3 = read(var0, var2 * 2);
                sbyte* ptr_sbyte;
                // The "fixed" statement tells the runtime to keep the array in the same
                // place in memory (relocating it would make the pointer invalid)
                fixed (byte* ptr_byte = &var3[0])
                {
                    // Cast the pointer to sbyte*
                    ptr_sbyte = (sbyte*)ptr_byte;

                    // Do your stuff here
                }

                // The end of the "fixed" block tells the runtime that the original array
                // is available for relocation and/or garbage collection again

                return new String(ptr_sbyte, 0, var2 * 2 - 2, Encoding.UTF32);
            }
            else
            {
                throw new Exception("invalid string length: len = " + var2);
                return null;
            }
        }
        public static byte readByte(Stream var0)
        {

            int var1 = var0.ReadByte();
            if (var1 < 0)
            {
                throw new EndOfStreamException();
            }
            else
            {
                return (byte)var1;
            }
        }


        public static byte readByte(MemoryStream var0)
        {
            int var1 = var0.ReadByte();
            if (var1 < 0)
            {
                throw new EndOfStreamException();
            }
            else
            {
                return (byte)var1;
            }
        }

        public static byte[] readBytes(Stream var0, int var1)
        {
            return read(var0, var1);
        }
        public static bool readBool(Stream var0)
        {
            return readByte(var0) != 0;
        }
        public static long readLong(Stream var0)
        {
            return readInt64(var0);
        }

        public static int readInt(Stream var0)
        {
            return readInt32(var0);
        }
        public static short readInt16(Stream var0)
        {
            return readInt16(read(var0, 2));
        }
        public static short readInt16(byte[] var0)
        {
            return (short)((var0[1] & 255) << 8 | var0[0] & 255);
        }


        public static int readInt32(Stream var0)
        {
            return readInt32(read(var0, 4));
        }

        public unsafe static int readInt32(byte[] var0)
        {
            return BitConverter.ToInt32(var0,0);
            //return Convert.ToInt32((byte)ptr_sbyte);
            //return var0[3] << 24 | (var0[2] & 255) << 16 | (var0[1] & 255) << 8 | var0[0] & 255;
        }


        public static long readInt64(Stream var0)
        {
            return readInt64(read(var0, 8));
        }

        public static long readInt64(byte[] var0)
        {
            long var1 = 0L;

            for (int var3 = 7; var3 >= 0; --var3)
            {
                var1 = var1 << 8 | (long)var0[var3] & 255L;
            }

            return var1;
        }
        public static float readFloat(Stream var0)
        {


            return readFloat(read(var0, 4));
        }

        public static float readFloat(byte[] var0)
        {
            //float f = var0[0] | var0[1] << 8 | var0[2] << 16 | var0[3] << 24;
            return BitConverter.ToSingle(var0, 0);
            //return Convert.ToSingle(readInt32(var0));
            //return f;
        }

        public static double readDouble(Stream var0)
        {
            return readDouble(read(var0, 8));
        }

        public static double readDouble(byte[] var0)
        {

            return BitConverter.ToDouble(var0, 0);
        }
        public static List<int> readArrayInt(Stream var0)
        {
            int var1 = readInt(var0);
            //var1 = readInt(var0);
            List<int> var2 = new List<int>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readInt(var0));
            }

            return var2;
        }

        public static List<double> readArrayDouble(Stream var0)
        {
            int var1 = readInt(var0);
            List<double> var2 = new List<double>(var1);

            for (int var3 = 0; var3 < var1; ++var3)
            {
                var2.Add(readDouble(var0));
            }

            return var2;
        }

        public static UUID readUUID(Chunk var0)
        {

            MemoryStream var1 = new MemoryStream(var0.content);
            if (version > 4)
            {
                var1.Position += 4;
            }
            return readUUID((MemoryStream)var1);
        }

        public static UUID readUUID(Stream var0)
        {
            UUID var1 = new UUID();
            var1.data1 = readInt(var0);
            var1.data2 = readShort(var0);
            var1.data3 = readShort(var0);
            byte[] var2 = read(var0, 8);
            var1.data4 = new byte[8];

            for (int var3 = 0; var3 < 8; ++var3)
            {
                var1.data4[var3] = var2[var3];
            }

            return var1;
        }
        public static Chunk[] readChunkTable(Chunk var0)
        {
            return readChunkTable(var0, -1);
        }

        public static Chunk[] readChunkTable(Chunk var0, int var1)
        {
            if (var0 == null)
            {
                throw new Exception("input chunk is null");
                return null;
            }
            else if (var0.content == null)
            {
                throw new Exception("no content in the input chunk");
                return null;
            }
            else
            {

                MemoryStream var2 = new MemoryStream(var0.content);
                Chunk var3 = null;
                List<Chunk> var4 = new List<Chunk>();
                int var5 = 1;

                do
                {
                    try
                    {

                        var3 = readChunk(var2);

                        if (var3 == null)
                        {
                            throw new Exception("no chunk is read");
                            return null;
                        }

                        if (var3.header == var1)
                        {
                            var3 = null;
                        }

                        ++var5;
                    }
                    catch (EndOfStreamException var7)
                    {
                        throw new Exception("unexpected EOF of chunk");
                        var3 = null;
                    }
                    catch (IOException var8)
                    {
                        Console.WriteLine(var8.StackTrace);
                        var3 = null;
                    }

                    if (var3 != null)
                    {
                        var4.Add(var3);
                    }
                } while (var3 != null);

                if (var4.Count == 0)
                {
                    return null;
                }
                else
                {
                    return (Chunk[])var4.ToArray();
                }
            }
        }

        public static Chunk readChunkV4(Stream var0)
        {
            Console.WriteLine("***********************Read   chunk*************");

            var var_temp = readInt32(var0);
            Console.WriteLine("temp_header:  " + var_temp);
            int var1;
            int var2;

            var1 = var_temp;
            var2 = readInt32(var0);


            Console.WriteLine("header:  " + var1);
            Console.WriteLine("length:  " + var2);

            if (!isShortChunk(var1) && var2 != 0)
            {
                if (var2 < 0)
                {
                    throw new Exception("length of content isn't positive: " + var2);
                    return null;
                }
                else if (var2 > 1000000000)
                {
                    throw new Exception("length of content seems too big: " + var2);
                    return null;
                }
                else
                {
                    Console.WriteLine("this");
                    byte[] var3 = read(var0, var2);

                    return new Chunk(var1, var2, var3);
                }
            }
            else
            {

                return new Chunk(var1, var2);
            }
        }
        public static Chunk readChunk(Stream var0)
        {
            //bool shift = false;

            //if (version > 4)
            //{
            //    int temph = readInt32(var0);
            //    int templ= readInt32(var0);
            //    if (temph <= 0 || temph == 1706537||templ<0)
            //    {

            //        shift = true;

            //    }
            //    var0.Position -= 8;
            //    if (shift)
            //    {
            return readChunkO(var0);
            //    }
            //    else
            //    {
            //        return readChunkV4(var0);
            //    }
            //}
            //else
            //{
            //    return readChunkV4(var0);
            //}


        }

        public static Chunk readChunkO(Stream var0)
        {
            Console.WriteLine("***********************Read   chunk*************");

            var var_temp = readInt32(var0);
            Console.WriteLine("temp_header:  " + var_temp);
            int var1;
            int var2;

            if (version > 4 && var_temp != 1)
            {
                if (var_temp == -2147319809 || var_temp == 1073774592 || var_temp == 163834 || var_temp == 196603 || var_temp == 196604)
                {
                    var1 = var_temp;
                    var2 = readInt32(var0);
                    var0.Position += 4;
                    
                }
                else
                {
                    var1 = readInt32(var0);
                    var2 = readInt32(var0);
                }
                if (var2 == 196603)
                {
                    var0.Position -= 16;
                    var1 = readInt32(var0);
                    var2 = readInt32(var0);
                }
            }
            else
            {
                var1 = var_temp;
                var2 = readInt32(var0);
            }


            Console.WriteLine("header:  " + var1);
            Console.WriteLine("length:  " + var2);

            if (!isShortChunk(var1) && var2 != 0)
            {
                if (var2 < 0)
                {
                    throw new Exception("length of content isn't positive: " + var2);
                    return null;
                }
                else if (var2 > 1000000000)
                {
                    throw new Exception("length of content seems too big: " + var2);
                    return null;
                }
                else
                {
                    Console.WriteLine("this");
                    byte[] var3 = read(var0, var2);

                    return new Chunk(var1, var2, var3);
                }
            }
            else
            {

                return new Chunk(var1, var2);
            }
        }

        public void readStartSection(Chunk var1)
        {
            Console.WriteLine(10 + " Rhino3dmImporter.readStartSection");

            int var2;
            Console.WriteLine(var1.content.Length);
            for (var2 = var1.content.Length; var2 > 0 && (var1.content[var2 - 1] == 0 || var1.content[var2 - 1] == 26); --var2)
            {
            }
            char[] chars = Encoding.Default.GetChars(var1.content);
            Console.WriteLine(chars.Length);
            String var3 = new String(chars);
            Console.WriteLine(var3);
            //this.file.startSection = new StartSection(var3);
        }


        public static bool isShortChunk(int var0)
        {
            return (var0 & -2147483648) != 0;
        }
        public static short readShort(Stream var0)
        {
            return readInt16(var0);
        }

        public static byte[] read(Stream var0, int var1)
        {
            byte[] var2 = new byte[var1];
            int var3 = 0;

            int var6;
            for (bool var4 = false; var3 < var1; var3 += var6)
            {
                var6 = var0.Read(var2, var3, var1 - var3);
                if (var6 < 0)
                {
                    if (var3 > 0)
                    {
                        byte[] var5 = new byte[var3];
                        //System.arraycopy(var2, 0, var5, 0, var3);
                        Array.Copy(var2, 0, var5, 0, var3);
                        throw new Exception("unexpected end of stream : len=" + var1 + ", ptr=" + var3 + ", res=" + var6);
                        //printAsciiOrHex(var5, throw new Exception);
                        throw new IOException();
                    }

                    throw new EndOfStreamException();
                }
            }

            return var2;
        }



    }
}
