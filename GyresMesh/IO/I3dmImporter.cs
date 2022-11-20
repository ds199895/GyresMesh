using Lucene.Net.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Hsy.IO
{
    public class I3dmImporter
    {
        private FileStream InputStream;

        public I3dmImporter(FileStream var1)
        {
            this.InputStream = var1;

        }

        public void readFileHeader()
        {
            byte[] var3 = new byte[24];
            byte[] var4 = new byte[8];
            this.InputStream.Read(var3, 0, 24);
            this.InputStream.Read(var4, 0, 8);
            int version = 0;
            try
            {
                String var5 = Encoding.Default.GetString(var4);
                Console.WriteLine(var5);
                String[] var6 = var5.Split('s');
                var5 = var6[var6.Length - 1];

                if (var5.Equals("X"))
                {
                    version = 2;
                }
                else
                {
                    version = int.Parse(var5);

                }

                Console.WriteLine("3DM FILE VERSION = " + version);
            }
            catch (FormatException var7)
            {
                throw new Exception(var7.StackTrace);
            }
        }
        public void read()
        {
            try
            {
                this.readFileHeader();
                Chunk var1 = null;
                int var2 = 0;
                do
                {
                    var2++;
                    Console.WriteLine(10+" chunk #" + var2);
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
                                case 268435475:
                                    this.readObjectTable(var1);
                                    break;
                                case 1706537:
                                    readChunkTable(var1);
                                    break;
                            }
                        }
                    }
                    catch (EndOfStreamException var4)
                    {
                        var1 = null;
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
        public void readObjectTable(Chunk var1)
        {
            Console.WriteLine(10+ " Rhino3dmImporter.readObjectTable");
            Chunk[] var2 = readChunkTable(var1);
            if (var2 != null)
            {
                //List<> var3 = new ArrayList();
                Console.WriteLine(10+ "num of rhino objects : " + var2.Length);
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
                        Console.WriteLine(1+ var10001.Append(var4).Append("/").Append(var2.Length).ToString());
                    }

                    //RhinoObject var9 = this.readRhinoObject(var8);
                    //if (var9 != null)
                    //{
                    //    var3.Add(var9);
                    //}
                }

                //if (var3.Count > 0)
                //{
                //    //this.file.rhinoObjects = (RhinoObject[])var3.toArray(new RhinoObject[var3.Count]);
                //}

            }
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
                
                MemoryStream var2 =new MemoryStream(var0.content);
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

        public static Chunk readChunk(MemoryStream var0)
        {
            int var1 = readInt32(var0);
            int var2 = readInt32(var0);
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
                    byte[] var3 = read(var0, var2);
                    return new Chunk(var1, var2, var3);
                }
            }
            else
            {
                return new Chunk(var1, var2);
            }
        }

        public Chunk readChunk(FileStream var0)
        {
            Console.WriteLine("***********************Read   chunk*************");
            int var1 = readInt32(var0);
            int var2 = readInt32(var0);
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
            Console.WriteLine(10+" Rhino3dmImporter.readStartSection");

            int var2;
            for (var2 = var1.content.Length; var2 > 0 && (var1.content[var2 - 1] == 0 || var1.content[var2 - 1] == 26); --var2)
            {
            }
            char[] chars = Encoding.Default.GetChars(var1.content);
            String var3 = new String(chars);
            Console.WriteLine(var3);
            //this.file.startSection = new StartSection(var3);
        }


        public static bool isShortChunk(int var0)
        {
            return (var0 & -2147483648) != 0;
        }
        public static int readInt32(MemoryStream var0)
        {
            return readInt32(read(var0, 4));
        }

        public static int readInt32(FileStream var0) 
        {
        return readInt32(read(var0, 4));
    }
        public static int readInt32(byte[] var0)
        {
            return var0[3] << 24 | (var0[2] & 255) << 16 | (var0[1] & 255) << 8 | var0[0] & 255;
        }
        public static byte[] read(FileStream var0, int var1)
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
                        Array.Copy(var2, 0, var5, 0,var3);
                        throw new Exception("unexpected end of stream : len=" + var1 + ", ptr=" + var3 + ", res=" + var6);
                        //printAsciiOrHex(var5, IOut.err);
                        throw new IOException();
                    }

                    throw new EndOfStreamException();
                }
            }

            return var2;
        }
        public static byte[] read(MemoryStream var0, int var1)
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
                        //printAsciiOrHex(var5, IOut.err);
                        throw new IOException();
                    }

                    throw new EndOfStreamException();
                }
            }

            return var2;
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

            public String toString()
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
