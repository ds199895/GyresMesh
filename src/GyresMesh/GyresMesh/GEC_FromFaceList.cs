﻿using Hsy.Core;
using Hsy.Geo;
using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hsy.Geo.HS_KDTreeInteger<Hsy.Geo.HS_Coord>;

namespace Hsy.GyresMesh
{
    public class GEC_FromFaceList : GEC_Creator
    {
        private List<GE_Face> _faceList;
        private HS_Coord[] vertices;
        private HS_Coord[] uvws;
        private HS_Coord[] vertexuvws;
        private int[] vertexColors;
        private bool[] vertexVisibility;
        private int[] vertexLabels;
        private int[] vertexInternalLabels;
        private int[] faceColors;
        private int[] faceTextures;
        private int[] faceTextureIds;
        private bool[] faceVisibility;
        private int[] faceLabels;
        private int[] faceInternalLabels;

        private int[][] faces;
        private int[][] faceuvws;


        private bool duplicate = false;
        /** Check face normal consistency?. */
        private bool normalcheck;
        private bool manifoldcheck;
        private bool cleanunused;
        private bool useFaceInfo;
        private bool useVertexInfo;
        private bool[] duplicated;
        public GEC_FromFaceList()
        {
            _faceList = null;
            Override = true;
            duplicate = true;
            normalcheck = false;
            cleanunused = true;

        }

        public GEC_FromFaceList(List<GE_Face> faces)
        {
            _faceList = faces;
        }

        public GEC_FromFaceList setVertices(HS_Coord[] vs)
        {
            vertices = vs;
            return this;
        }
        public GEC_FromFaceList setVertices<T>(List<T> vs) where T : HS_Coord
        {
            int n = vs.Count;
            var Etr = vs.GetEnumerator();
            this.vertices = new HS_Coord[n];
            for (int i = 0; Etr.MoveNext(); i++)
            {
                this.vertices[i] = (HS_Coord)Etr.Current;
            }
            return this;
        }

        public GEC_FromFaceList setFaces(int[][] fs)
        {
            faces = fs;
            return this;
        }

        public GEC_FromFaceList setFaces(List<int[]> fs)
        {
            this.faces = new int[fs.Count][];
            int i = 0;
            for (var var4 = fs.GetEnumerator(); var4.MoveNext(); i++)
            {
                int[] indices = (int[])var4.Current;
                this.faces[i] = indices;
            }
            return this;
        }
        public GEC_FromFaceList setDuplicate(bool[] b)
        {
            this.duplicated = b;
            if (b.Length > 0)
            {
                this.duplicate = true;
            }
            else
            {
                this.duplicate = false;
            }


            return this;
        }
        //public GEC_FromFaceList setVertexDuplicate(bool[] b)
        //{
        //    this.duplicate = b;
        //    return this;
        //}
        public GEC_FromFaceList setCheckNormals(bool b)
        {
            this.normalcheck = b;
            return this;
        }
        //protected boolean getCheckDuplicateVertices()
        //{
        //    return parameters.get("duplicate", true);
        //}

        //protected boolean getUseFaceInfo()
        //{
        //    return parameters.get("usefaceinfo", false);
        //}
        //protected boolean getUseVertexInfo()
        //{
        //    return parameters.get("usevertexinfo", false);
        //}

        public void SetFaces(List<GE_Face> faces)
        {
            _faceList = faces;
        }

        protected internal override GE_Mesh createBase()
        {
            GE_Mesh mesh = new GE_Mesh();
            double lasttime;
            if (faces != null && vertices != null)
            {
                if (faces.Length == 0)
                {
                    return mesh;
                }
                bool useVertexUVW = vertexuvws != null && vertexuvws.Length == vertices.Length;
                bool useFaceUVW = uvws != null && faceuvws != null && faceuvws.Length == faces.Length;
                GE_Vertex[] uniqueVertices = new GE_Vertex[vertices.Length];
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                lasttime = stopwatch.ElapsedMilliseconds;
                stopwatch.Start();
                if (duplicate)
                {
                    HS_KDTreeInteger<HS_Coord> kdtree = new HS_KDTreeInteger<HS_Coord>();
                    HS_KDEntryInteger<HS_Coord>[] neighbors;

                    //for (int i = 0; i < vertices.Length; i++)
                    //{
                    //    if (duplicated[i])
                    //    {
                    //        long kvalue = -1 ;
                    //        pkey.TryGetValue(vertices[i], out kvalue);
                    //        uniqueVertices[i] = vdic[kvalue];
                    //    }
                    //    else
                    //    {
                    //        v = new GE_Vertex(vertices[i]);
                    //        if (useVertexInfo)
                    //        {

                    //        }
                    //        else
                    //        {
                    //            v.SetInternalLabel(i);
                    //        }
                    //        if (useVertexUVW)
                    //        {
                    //            v.SetUVW(vertexuvws[i]);
                    //        }
                    //        uniqueVertices[i] = v;
                    //        pkey.Add(vertices[i], v.GetKey());
                    //        vdic.Add(v.GetKey(), v);
                    //    }


                    //    mesh.Add(uniqueVertices[i]);
                    //}

                    //base on faces;
                    //Dictionary<HS_Coord, long> pkey;
                    Dictionary<int, long> phkey;
                    Dictionary<HS_Coord, long> pvkey;
                    Dictionary<long, GE_Vertex> vdic;
                    phkey = new Dictionary<int, long>();
                    pvkey = new Dictionary<HS_Coord, long>();
                    vdic = new Dictionary<long, GE_Vertex>();
                    for (int i = 0; i < faces.Length; i++)
                    {

                        GE_Vertex v;
                        //HS_Coord ve = null;

                        for (int j = 0; j < faces[i].Length; j++)
                        {
                            int vid = faces[i][j];
                            if (duplicated[vid])
                            {
                                long kvalue = -1;
                                var ve = vertices[vid];
                                int hashcode = HS_HashCode.calculateHashCode(ve);
                                //Console.WriteLine("vertex: "+ve);
                                //pkey.ContainsKey(ve);
                                //foreach(HS_Coord coord in pkey.Keys)
                                //{
                                //    if (HS_Epsilon.isZero(HS_CoordOp3D.getSqDistance3D(coord, ve))){
                                //        Console.WriteLine(coord);
                                //        kvalue = pkey[coord];
                                //    }

                                //}

                                //if (kvalue == 0)
                                //{
                                //    kvalue = pkey.Last().Value;
                                //}
                                //Console.WriteLine(" in or not:   " + pkey.ContainsKey(hashcode));
                                //pkey.TryGetValue(ve, out kvalue);
                                if (pvkey.ContainsKey(ve))
                                {
                                    pvkey.TryGetValue(ve, out kvalue);
                                }
                                else
                                {
                                    phkey.TryGetValue(hashcode, out kvalue);
                                }
                                
                                //Console.WriteLine(" kvalue:   " + kvalue);
                                uniqueVertices[vid] = vdic[kvalue];

                            }
                            else
                            {
                                v = new GE_Vertex(vertices[vid]);
                                if (useVertexInfo)
                                {

                                }
                                else
                                {
                                    v.SetInternalLabel(vid);
                                }
                                if (useVertexUVW)
                                {
                                    v.SetUVW(vertexuvws[vid]);
                                }
                                uniqueVertices[vid] = v;
                                int hashcode = HS_HashCode.calculateHashCode(vertices[vid]);

                                if (!phkey.ContainsKey(hashcode))
                                {

                                    phkey.Add(hashcode, v.GetKey());
                                    //pkey.Add(vertices[vid], v.GetKey());
                                }
                                pvkey.Add(vertices[vid], v.GetKey());
                                vdic.Add(v.GetKey(), v);

                            }
                            mesh.Add(uniqueVertices[vid]);




                        }
                    }




                    //    //neighbors = kdtree.getNearestNeighbors(v, 1);
                    //    //if (neighbors[0].d2 < HS_Epsilon.SQEPSILON)
                    //    //{
                    //    //    uniqueVertices[i] = uniqueVertices[neighbors[0].value];
                    //    //    duplicated[i] = true;
                    //    //}
                    //    //else
                    //    //{
                    //    //    kdtree.add(v, i);
                    //    //    uniqueVertices[i] = v;
                    //    //    mesh.Add(uniqueVertices[i]);
                    //    //    duplicated[i] = false;
                    //    //}
                    //}
                }
                else
                {
                    duplicated = new bool[vertices.Length];
                    GE_Vertex v;
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        v = new GE_Vertex(vertices[i]);
                        if (useVertexInfo)
                        {

                        }
                        else
                        {
                            v.SetInternalLabel(i);
                        }
                        if (useVertexUVW)
                        {
                            v.SetUVW(vertexuvws[i]);
                        }
                        v.SetInternalLabel(i);
                        uniqueVertices[i] = v;
                        duplicated[i] = false;
                        mesh.Add(uniqueVertices[i]);
                    }
                }




                Console.WriteLine("checkduplicate总用   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
                lasttime = stopwatch.ElapsedMilliseconds;
                int id = 0;
                GE_Halfedge he;
                List<long> nmedges = new List<long>();
                if (normalcheck)
                {
                    Dictionary<long, int[]> edges = new Dictionary<long, int[]>();
                    for (int i = 0; i < faces.Length; i++)
                    {
                        int[] face = faces[i];
                        int fl = face.Length;
                        for (int j = 0; j < fl; j++)
                        {
                            long ohash = Ohash(face[j], face[(j + 1) % fl]);
                            int[] faces = edges[ohash];
                            if (faces == null)
                            {
                                edges.Add(ohash, new int[] { i, -1 });
                            }
                            else
                            {
                                if (faces[i] > -1)
                                {
                                    nmedges.Add(ohash);
                                }
                                face[1] = i;
                            }
                        }
                    }
                    bool[] visited = new bool[faces.Length];
                    LinkedList<int> queue = new LinkedList<int>();
                    bool faceleft = false;
                    int starti = 0;
                    do
                    {
                        queue.AddLast(starti);
                        int temp;
                        while (queue.Count != 0)
                        {
                            int index = queue.First();
                            int[] face = faces[index];
                            int fl = face.Length;
                            visited[index] = true;
                            for (int j = 0; j < fl; j++)
                            {
                                long ohash = Ohash(face[j], face[(j + 1) % fl]);
                                int[] ns = edges[ohash];
                                if (ns != null)
                                {
                                    edges.Remove(ohash);
                                    int neighbor;
                                    if (ns[0] == index)
                                    {
                                        neighbor = ns[1];
                                    }
                                    else
                                    {
                                        neighbor = ns[0];
                                    }
                                    if (neighbor > -1)
                                    {
                                        if (visited[neighbor] == false)
                                        {
                                            if (!queue.Contains(neighbor))
                                            {
                                                queue.AddLast(neighbor);
                                            }
                                            if (consistentOrder(j, (j + 1) % fl, face, faces[neighbor]) == -1)
                                            {
                                                int fln = faces[neighbor].Length;
                                                for (int k = 0; k < fln / 2; k++)
                                                {
                                                    temp = faces[neighbor][k];
                                                    faces[neighbor][k] = faces[neighbor][fln - k - 1];
                                                    faces[neighbor][fln - k - 1] = temp;
                                                }
                                            }
                                            visited[neighbor] = true;
                                        }
                                    }
                                }
                            }
                        }
                        faceleft = false;
                        for (; starti < faces.Length; starti++)
                        {
                            if (!visited[starti])
                            {
                                faceleft = true;
                                break;
                            }
                        }

                    }
                    while (faceleft);

                }
                bool useFaceTextures = faceTextures != null && faceTextureIds.Length == faces.Length;
                int faceid = 0;
                //Dictionary<HS_Point, GE_Halfedge> centers = new Dictionary<HS_Point, GE_Halfedge>();
                ParallelOptions options = new ParallelOptions();
                options.MaxDegreeOfParallelism = 3;
                //System.Threading.Tasks.Parallel.ForEach(faces, options, face => {
                //    int[] faceuvw = null;
                //    if (useFaceUVW)
                //    {
                //        faceuvw = faceuvws[faceid];
                //    }
                //    if (face != null)
                //    {
                //        List<GE_Halfedge> faceEdges = new List<GE_Halfedge>();
                //        GE_Face hef = new GE_Face();
                //        hef.SetInternalLabel(id);
                //        if (useFaceInfo)
                //        {
                //            //hef.setColor(faceColors[faceid]);
                //            //hef.setTextureId(faceTextureIds[id]);
                //            //hef.setVisible(faceVisibility[id]);
                //            //hef.setUserLabel(faceLabels[id]);
                //            //hef.setInternalLabel(faceInternalLabels[id]);
                //        }
                //        if (useFaceTextures)
                //        {
                //            //hef.setTextureId(faceTextureIds[id]);
                //        }
                //        id++;
                //        int fl = face.Length;
                //        int[] locface = new int[fl];
                //        int[] locfaceuvw = new int[fl];
                //        int li = 0;
                //        locface[li] = face[0];
                //        if (useFaceUVW)
                //        {
                //            locfaceuvw[li] = faceuvw[0];
                //        }
                //        li++;
                //        for (int i = 1; i < fl - 1; i++)
                //        {
                //            if (uniqueVertices[face[i]] != uniqueVertices[face[i - 1]])
                //            {
                //                locface[li] = face[i];
                //                if (useFaceUVW)
                //                {
                //                    locfaceuvw[li] = faceuvw[i];
                //                }
                //                li++;
                //            }
                //        }
                //        if (uniqueVertices[face[fl - 1]] != uniqueVertices[face[fl - 2]] && uniqueVertices[face[fl - 1]] != uniqueVertices[face[0]])
                //        {
                //            locface[li] = face[fl - 1];
                //            if (useFaceUVW)
                //            {
                //                locfaceuvw[li] = faceuvw[fl - 1];
                //            }
                //            li++;
                //        }
                //        if (li > 2)
                //        {
                //            for (int i = 0; i < li; i++)
                //            {
                //                he = new GE_Halfedge();
                //                faceEdges.Add(he);
                //                mesh.SetFace(he, hef);
                //                if (hef.GetHalfedge() == null)
                //                {
                //                    mesh.SetHalfedge(hef, he);

                //                }
                //                mesh.SetVertex(he, uniqueVertices[locface[i]]);
                //                if (useFaceUVW)
                //                {
                //                    he.SetUVW(uvws[locfaceuvw[i]]);
                //                }
                //                if (useVertexUVW)
                //                {
                //                    if (duplicated[locface[i]])
                //                    {

                //                    }
                //                }
                //                mesh.SetHalfedge(he.GetStart(), he);
                //            }

                //            mesh.Add(hef);
                //            GE_MeshOp.cycleHalfedges(mesh, faceEdges);
                //            mesh.addHalfedges(faceEdges);

                //        }
                //    }
                //    faceid++;
                //});
                //{
                foreach (int[] face in faces)
                {
                    int[] faceuvw = null;
                    if (useFaceUVW)
                    {
                        faceuvw = faceuvws[faceid];
                    }
                    if (face != null)
                    {
                        List<GE_Halfedge> faceEdges = new List<GE_Halfedge>();
                        GE_Face hef = new GE_Face();
                        hef.SetInternalLabel(id);
                        if (useFaceInfo)
                        {
                            //hef.setColor(faceColors[faceid]);
                            //hef.setTextureId(faceTextureIds[id]);
                            //hef.setVisible(faceVisibility[id]);
                            //hef.setUserLabel(faceLabels[id]);
                            //hef.setInternalLabel(faceInternalLabels[id]);
                        }
                        if (useFaceTextures)
                        {
                            //hef.setTextureId(faceTextureIds[id]);
                        }
                        id++;
                        int fl = face.Length;
                        int[] locface = new int[fl];
                        int[] locfaceuvw = new int[fl];
                        int li = 0;
                        locface[li] = face[0];
                        if (useFaceUVW)
                        {
                            locfaceuvw[li] = faceuvw[0];
                        }
                        li++;
                        for (int i = 1; i < fl - 1; i++)
                        {
                            if (uniqueVertices[face[i]] != uniqueVertices[face[i - 1]])
                            {
                                locface[li] = face[i];
                                if (useFaceUVW)
                                {
                                    locfaceuvw[li] = faceuvw[i];
                                }
                                li++;
                            }
                        }
                        if (uniqueVertices[face[fl - 1]] != uniqueVertices[face[fl - 2]] && uniqueVertices[face[fl - 1]] != uniqueVertices[face[0]])
                        {
                            locface[li] = face[fl - 1];
                            if (useFaceUVW)
                            {
                                locfaceuvw[li] = faceuvw[fl - 1];
                            }
                            li++;
                        }
                        if (li > 2)
                        {
                            for (int i = 0; i < li; i++)
                            {
                                he = new GE_Halfedge();
                                faceEdges.Add(he);
                                mesh.SetFace(he, hef);
                                if (hef.GetHalfedge() == null)
                                {
                                    mesh.SetHalfedge(hef, he);

                                }
                                mesh.SetVertex(he, uniqueVertices[locface[i]]);
                                if (useFaceUVW)
                                {
                                    he.SetUVW(uvws[locfaceuvw[i]]);
                                }
                                if (useVertexUVW)
                                {
                                    if (duplicated[locface[i]])
                                    {

                                    }
                                }
                                mesh.SetHalfedge(he.GetStart(), he);
                            }

                            mesh.Add(hef);
                            GE_MeshOp.cycleHalfedges(mesh, faceEdges);
                            mesh.addHalfedges(faceEdges);

                        }
                    }
                    faceid++;
                }

                Console.WriteLine("baseset总用   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
                lasttime = stopwatch.ElapsedMilliseconds;
                //Dictionary<long, GE_Halfedge> centers = new Dictionary<long, GE_Halfedge>();
                HS_KDTreeInteger<HS_Coord> cen = new HS_KDTreeInteger<HS_Coord>();
                HS_KDEntryInteger<HS_Coord>[] nei;
                //var hes = mesh.GetHalfedges();
                //cen.add(hes[0].GetCenter(), (int)hes[0].GetKey());
                //centers.Add(hes[0].GetKey(), hes[0]);

                //for (int i = 0; i < hes.Count; i++)
                //{
                //    GE_Halfedge e = hes[i];
                //    HS_Point mid = (HS_Point)e.GetCenter();
                //    nei = cen.getNearestNeighbors(mid, 1);

                //    if (nei[0].d2 < HS_Epsilon.SQEPSILON)
                //    {
                //        mesh.SetPair(centers[(long)nei[0].value], e);
                //    }
                //    else
                //    {
                //        cen.add(mid, (int)e.GetKey());
                //        centers.Add(e.GetKey(), e);
                //    }

                //}
                var hes = mesh.GetHalfedges();
                cen.add(hes[0].GetCenter(), 0);
                GE_Halfedge[] pairs = new GE_Halfedge[hes.Count];
                //centers.Add(0, hes[0]);
                //mids.Add(hes[0].GetCenter());

                //for(int i=0;i<hes.Count;i++)
                //{
                //    GE_Halfedge e = hes[i];
                //    HS_Point mid = (HS_Point)e.GetCenter();
                //    nei = cen.getNearestNeighbors(mid, 1);

                //    if (!(nei[0].d2 < HS_Epsilon.SQEPSILON))
                //    {
                //        cen.add(mid, i);
                //        //pairs[i] =null;
                //        //centers.Add(e.GetKey(), e);
                //    }
                //    else
                //    {
                //        //mesh.SetPair(hes[nei[0].value], e);
                //        //pairs[i] = hes[nei[0].value];
                //        mesh.SetPair(e, hes[nei[0].value]);
                //    }
                //}

                //System.Threading.Tasks.Parallel.For(1, hes.Count, options, i =>
                //{
                //    GE_Halfedge e = hes[i];
                //    HS_Point mid = (HS_Point)e.GetCenter();
                //    nei = cen.getNearestNeighbors(mid, 1);

                //    if (!(nei[0].d2 < HS_Epsilon.SQEPSILON))
                //    {
                //        cen.add(mid, i);
                //        pairs[i] =null;
                //        //centers.Add(e.GetKey(), e);
                //    }
                //    else
                //    {
                //        //mesh.SetPair(hes[nei[0].value], e);
                //        pairs[i] = hes[nei[0].value];
                //        //mesh.SetPair(e, hes[nei[0].value]);
                //    }
                //    //mids.Add(mid);
                //});

                //options.MaxDegreeOfParallelism = 1;
                //System.Threading.Tasks.Parallel.For(1, hes.Count, options, i =>
                //{
                //    GE_Halfedge e = hes[i];

                //    if (pairs[i] != null)
                //    {
                //        mesh.SetPair(e, pairs[i]);
                //        //centers.Add(e.GetKey(), e);
                //    }
                //    //mids.Add(mid);
                //});
                GE_MeshOp.pairHalfedges(mesh);

                Console.WriteLine("pairing线程总用   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
                lasttime = stopwatch.ElapsedMilliseconds;

                if (this.cleanunused)
                {
                    mesh.cleanUnusedElementsByface();
                    GE_MeshOp.capHalfedges(mesh);
                }
                if (this.manifoldcheck)
                {

                }
                if (this.normalcheck)
                {
                    GE_FaceEnumerator fEtr = mesh.fEtr();
                    GE_Face left = null;
                    Object fcleft = new HS_Point(1.7976931348623157E308D, 1.7976931348623157E308D, 1.7976931348623157E308D);
                    while (fEtr.MoveNext())
                    {
                        GE_Face f = fEtr.Current;
                        if (GE_MeshOp.getFaceCenter(f).xd < ((HS_Coord)fcleft).xd)
                        {
                            left = f;
                            fcleft = GE_MeshOp.getFaceCenter(f);
                        }
                    }
                    HS_Coord leftn = GE_MeshOp.getFaceNormal(left);
                    if (leftn.xd > 0.0D)
                    {
                        GE_MeshOp.ReverseFaces(mesh);
                    }
                }

                Console.WriteLine("cap总用   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
            }
            return mesh;
        }
        private long Ohash(int u, int v)
        {
            int lu = u;
            int lv = v;
            if (u > v)
            {
                lu = v;
                lv = u;
            }
            long A = lu >= 0 ? 2 * lu : -2 * lu - 1;
            long B = lv >= 0 ? 2 * lv : -2 * lv - 1;
            return A >= B ? A * A + A + B : A + B * B;
        }
        private int consistentOrder(int i, int j, int[] face,
        int[] neighbor)
        {
            for (int k = 0; k < neighbor.Length; k++)
            {
                if (neighbor[k] == face[i]
                        && neighbor[(k + 1) % neighbor.Length] == face[j])
                {
                    return -1;
                }
                if (neighbor[k] == face[j]
                        && neighbor[(k + 1) % neighbor.Length] == face[i])
                {
                    return 1;
                }
            }
            return 0;
        }
        //protected override HE_Mesh createBase()
        //{
        //    GE_Mesh mesh = new GE_Mesh();
        //    if (faces != null && vertices != null)
        //    {
        //        if (faces.Length == 0)
        //        {
        //            return mesh;
        //        }
        //        bool useVertexUVM = vertexuvws != null && vertexuvws.Length == vertices.Length;
        //        bool useFaceUVW = uvws != null && faceuvws != null && faceuvws.Length == faces.Length;
        //        GE_Vertex[] uniqueVertices = new GE_Vertex[vertices.Length];
        //        bool[] duplicated = new bool[vertices.Length];
        //        bool useVertexInfo = getUseVertexInfo();

        //    }
        //}
    }
}