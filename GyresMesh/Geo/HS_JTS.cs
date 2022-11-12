using GeoAPI.Geometries;
using Hsy.GyresMesh;
using Hsy.HsMath;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Collections.Generic.Dictionary<TKey, TValue>;

namespace Hsy.Geo
{
    public class HS_JTS
    {
        private static GeometryFactory JTSgf = new GeometryFactory(new PrecisionModel(HS_Epsilon.SCALE));
        //private static HS_Map2D XY=(new HS_GeometryFactoryd).CreateSimplePolygon;

        private HS_JTS()
        {
        }
        public static Coordinate toJTSCoordinate2D(HS_Coord point, int i)
        {
            return new Coordinate(point.xd, point.yd, i);
        }
        public static Polygon toJTSPolygon2D(HS_Polygon poly)
        {
            int[] npc = poly.GetNumberOfPointsPerContour();
            Coordinate[] coords = new Coordinate[npc[0] + 1];
            int i = 0;
            for (i = 0; i < npc[0]; ++i)
            {
                coords[i] = toJTSCoordinate2D(poly.GetPoint(i), i);
            }

            coords[i] = toJTSCoordinate2D(poly.GetPoint(0), 0);
            LinearRing shell = JTSgf.CreateLinearRing(coords) as LinearRing;
            //LinearRing shell = new LinearRing(coords);
            LinearRing[] holes = new LinearRing[poly.getNumberOfHoles()];
            int index = poly.getNumberOfShellPoints();

            for (i = 0; i < poly.getNumberOfHoles(); ++i)
            {
                coords = new Coordinate[npc[i + 1] + 1];
                coords[npc[i + 1]] = toJTSCoordinate2D(poly.GetPoint(index), index);

                for (int j = 0; j < npc[i + 1]; ++j)
                {
                    coords[j] = toJTSCoordinate2D(poly.GetPoint(index), index);
                    ++index;
                }


                holes[i] = JTSgf.CreateLinearRing(coords) as LinearRing;
                //holes[i] = new LinearRing(coords);
            }

            return JTSgf.CreatePolygon(shell, holes) as Polygon;
            //return new Polygon(shell, holes) ;
        }

        static HS_Polygon CreatePolygonFromJTSPolygon2D( Polygon JTSpoly)
        {
             LineString shell = (LineString)JTSpoly.ExteriorRing;
            Coordinate[] coords = shell.Coordinates;
             HS_Coord[] points = new HS_Coord[coords.Length - 1];
            for (int i = 0; i < coords.Length - 1; i++)
            {
                points[i] = createPoint2D(coords[i]);
            }
             int numholes = JTSpoly.NumInteriorRings;
            if (numholes > 0)
            {
                 HS_Coord[][] holecoords = new HS_Coord[numholes][];
                for (int i = 0; i < numholes; i++)
                {
                     LineString hole = (LineString)JTSpoly.GetInteriorRingN(i);
                    coords = hole.Coordinates;
                    holecoords[i] = new HS_Coord[coords.Length - 1];
                    for (int j = 0; j < coords.Length - 1; j++)
                    {
                        holecoords[i][j] = createPoint2D(coords[j]);
                    }
                }
                return new HS_GeometryFactory2D().createPolygonWithHoles(points,
                        holecoords);
            }
            else
            {
                return new HS_GeometryFactory2D().createSimplePolygon(points);
            }
        }
        public static HS_Point createPoint2D(Coordinate coord)
        {
            return new HS_Point(coord.X, coord.Y);
        }
        //public class PlanarPathTriangulator
        //{
        //    //static  HS_ProgressTracker tracker = HS_ProgressTracker.instance();
        //    private static HS_GeometryFactory geometryfactory = new HS_GeometryFactoryd;

        //    public PlanarPathTriangulator()
        //    {
        //    }

        //    public static long[] getTriangleKeys(List<GE_Path> paths, HS_Plane P)
        //    {
        //        tracker.setStartStatus("GET_PlanarPathTriangulator", "Starting planar path triangulation.");
        //        HS_Map2D emb = new HS_OrthoProject(P);
        //        HS_JTS.PlanarPathTriangulator.RingTree ringtree = new HS_JTS.PlanarPathTriangulator.RingTree();
        //        HS_KDTree<HS_Point, Long> vertextree = new HS_KDTree();
        //        tracker.setDuringStatus("GET_PlanarPathTriangulator", "Building contours tree.");

        //        for (int i = 0; i < paths.Count; ++i)
        //        {
        //            GE_Path path = (GE_Path)paths.Get(i);
        //            if (path.isLoop() && path.getPathOrder() > 2)
        //            {
        //                List<GE_Vertex> vertices = path.getPathVertices();
        //                Coordinate[] pts = new Coordinate[vertices.Count + 1];

        //                for (int j = 0; j < vertices.Count; ++j)
        //                {
        //                    HS_Point proj = new HS_Point();
        //                    emb.mapPoint3D((HS_Coord)vertices.Get(j), proj);
        //                    vertextree.Add(proj, ((GE_Vertex)vertices.Get(j)).getKeyd);
        //                    pts[vertices.Count - j] = new Coordinate(proj.xd, proj.yd, 0.0D);
        //                }

        //                HS_Point proj = new HS_Point();
        //                emb.mapPoint3D((HS_Coord)vertices.Get(0), proj);
        //                pts[0] = new Coordinate(proj.xd, proj.yd, 0.0D);
        //                ringtree.Add(HS_JTS.JTSgf.CreateLinearRing(pts));
        //            }
        //        }

        //        tracker.setDuringStatus("GET_PlanarPathTriangulator", "Extracting polygons from contours tree.");
        //        List<HS_Polygon> polygons = ringtree.extractPolygons();
        //        List<HS_Coord[]> triangles = new List<HS_Point>();
        //        HS_ProgressCounter counter = new HS_ProgressCounter(polygons.Count, 10);
        //        tracker.setCounterStatus("GET_PlanarPathTriangulator", "Triangulating polygons.", counter);
        //        Iterator var11 = polygons.iterator();

        //        while (var11.hasNext())
        //        {
        //            HS_Polygon poly = (HS_Polygon)var11.next();
        //            int[] tris = poly.getTriangles();

        //            for (int i = 0; i < tris.Length; i += 3)
        //            {
        //                triangles.Add(new HS_Coord[] { poly.GetPoint(tris[i]), poly.GetPoint(tris[i + 1]), poly.GetPoint(tris[i + 2]) });
        //            }

        //            counter.increment();
        //        }

        //        long[] trianglekeys = new long[3 * triangles.Count];

        //        for (int i = 0; i < triangles.Count; ++i)
        //        {
        //            HS_Coord[] tri = (HS_Coord[])triangles.Get(i);
        //            long key0 = (Long)vertextree.getNearestNeighbor(tri[0]).value;
        //            long key1 = (Long)vertextree.getNearestNeighbor(tri[1]).value;
        //            long key2 = (Long)vertextree.getNearestNeighbor(tri[2]).value;
        //            trianglekeys[3 * i] = key0;
        //            trianglekeys[3 * i + 1] = key2;
        //            trianglekeys[3 * i + 2] = key1;
        //        }

        //        tracker.setStopStatus("GET_PlanarPathTriangulator", "All paths triangulated.");
        //        return trianglekeys;
        //    }

        //    public static GE_Mesh toMesh(HS_Triangulation2DWithPoints tri)
        //    {
        //        GEC_FromFacelist ffl = (new GEC_FromFacelist()).setFaces(tri.getTriangles()).setVertices(tri.GetPoints());
        //        return new GE_Mesh(ffl);
        //    }

        //    private static class RingNode
        //    {
        //        HS_JTS.PlanarPathTriangulator.RingNode parent;
        //        List<HS_JTS.PlanarPathTriangulator.RingNode> children;
        //        LinearRing ring;
        //        Polygon poly;
        //        bool hole;

        //        RingNode()
        //        {
        //            this.parent = null;
        //            this.ring = null;
        //            this.children = new ArrayList();
        //            this.hole = true;
        //        }

        //        RingNode(HS_JTS.PlanarPathTriangulator.RingNode parent, LinearRing ring)
        //        {
        //            this.parent = parent;
        //            this.ring = ring;
        //            Coordinate[] coords = ring.Coordinates;
        //            this.poly = HS_JTS.JTSgf.CreatePolygon(coords);
        //            this.hole = !CGAlgorithms.isCCW(coords);
        //            this.children = new ArrayList();
        //        }
        //    }

        //    private static class RingTree
        //    {
        //        HS_JTS.PlanarPathTriangulator.RingNode root = new HS_JTS.PlanarPathTriangulator.RingNode();

        //        RingTree()
        //        {
        //        }

        //        void add(LinearRing ring)
        //        {
        //            Polygon poly = HS_JTS.JTSgf.CreatePolygon(ring);
        //            HS_JTS.PlanarPathTriangulator.RingNode currentParent = this.root;

        //            HS_JTS.PlanarPathTriangulator.RingNode foundParent;
        //            do
        //            {
        //                foundParent = null;

        //                for (int i = 0; i < currentParent.children.Count; ++i)
        //                {
        //                    HS_JTS.PlanarPathTriangulator.RingNode node = (HS_JTS.PlanarPathTriangulator.RingNode)currentParent.children.Get(i);
        //                    Polygon otGEr = node.poly;
        //                    if (poly.within(otGEr))
        //                    {
        //                        foundParent = node;
        //                        currentParent = node;
        //                        break;
        //                    }
        //                }
        //            } while (foundParent != null);

        //            HS_JTS.PlanarPathTriangulator.RingNode newNode = new HS_JTS.PlanarPathTriangulator.RingNode(currentParent, ring);
        //            List<HS_JTS.PlanarPathTriangulator.RingNode> nodesToRemove = new ArrayList();

        //            for (int i = 0; i < currentParent.children.Count; ++i)
        //            {
        //                HS_JTS.PlanarPathTriangulator.RingNode node = (HS_JTS.PlanarPathTriangulator.RingNode)currentParent.children.Get(i);
        //                Polygon otGEr = node.poly;
        //                if (otGEr.within(poly))
        //                {
        //                    newNode.children.Add(node);
        //                    nodesToRemove.Add(node);
        //                }
        //            }

        //            currentParent.children.removeAll(nodesToRemove);
        //            currentParent.children.Add(newNode);
        //        }

        //        List<HS_Polygon> extractPolygons()
        //        {
        //            List<HS_Polygon> polygons = new ArrayList();
        //            List<HS_JTS.PlanarPathTriangulator.RingNode> shellNodes = new ArrayList();
        //            this.addExteriorNodes(this.root, shellNodes);
        //            Iterator var4 = shellNodes.iterator();

        //            while (true)
        //            {
        //                while (var4.hasNext())
        //                {
        //                    HS_JTS.PlanarPathTriangulator.RingNode node = (HS_JTS.PlanarPathTriangulator.RingNode)var4.next();
        //                    int count = 0;

        //                    for (int i = 0; i < node.children.Count; ++i)
        //                    {
        //                        if (((HS_JTS.PlanarPathTriangulator.RingNode)node.children.Get(i)).hole)
        //                        {
        //                            ++count;
        //                        }
        //                    }

        //                    LinearRing[] holes = new LinearRing[count];
        //                    int index = 0;

        //                    for (int i = 0; i < node.children.Count; ++i)
        //                    {
        //                        if (((HS_JTS.PlanarPathTriangulator.RingNode)node.children.Get(i)).hole)
        //                        {
        //                            holes[index++] = ((HS_JTS.PlanarPathTriangulator.RingNode)node.children.Get(i)).ring;
        //                        }
        //                    }

        //                    Geometry result = HS_JTS.JTSgf.CreatePolygon(node.ring, holes);
        //                    if (result.getGeometryType().equals("Polygon"))
        //                    {
        //                        polygons.Add(HS_JTS.CreatePolygonFromJTSPolygon2D((Polygon)result));
        //                    }
        //                    else if (result.getGeometryType().equals("MultiPolygon"))
        //                    {
        //                        for (int j = 0; j < result.getNumGeometries(); ++j)
        //                        {
        //                            Geometry ggeo = result.getGeometryN(j);
        //                            polygons.Add(HS_JTS.CreatePolygonFromJTSPolygon2D((Polygon)ggeo));
        //                        }
        //                    }
        //                }

        //                return polygons;
        //            }
        //        }

        //        void addExteriorNodes(HS_JTS.PlanarPathTriangulator.RingNode parent, List<HS_JTS.PlanarPathTriangulator.RingNode> shellNodes)
        //        {
        //            HS_JTS.PlanarPathTriangulator.RingNode node;
        //            for (Iterator var4 = parent.children.iterator(); var4.hasNext(); this.addExteriorNodes(node, shellNodes))
        //            {
        //                node = (HS_JTS.PlanarPathTriangulator.RingNode)var4.next();
        //                if (!node.hole)
        //                {
        //                    shellNodes.Add(node);
        //                }
        //            }

        //        }
        //    }
        //}

        public class PolygonTriangulatorJTS
        {
            private List<Coordinate> shellCoords;
            private bool[] shellCoordAvailable;

            public PolygonTriangulatorJTS()
            {
            }

            public static int[] triangulateQuad(HS_Coord p0, HS_Coord p1, HS_Coord p2, HS_Coord p3)
            {
                bool p0inside = HS_GeometryOp3D.pointInTriangleBary3D(p0, p1, p2, p3);
                if (p0inside)
                {
                    return new int[] { 0, 1, 2, 0, 2, 3 };
                }
                else
                {
                    bool p2inside = HS_GeometryOp3D.pointInTriangleBary3D(p2, p1, p0, p3);
                    return p2inside ? new int[] { 0, 1, 2, 0, 2, 3 } : new int[] { 0, 1, 3, 1, 2, 3 };
                }
            }

            public HS_Triangulation2D triangulatePolygon2D(HS_Polygon polygon, bool optimize)
            {
                List<HS_Point> pts = new List<HS_Point>();

                int index;
                for (index = 0; index < polygon.numberOfShellPoints; ++index)
                {
                    pts.Add(polygon.GetPoint(index));
                }

                index = polygon.numberOfShellPoints;
                List<HS_Point>[] hpts = new List<HS_Point>[polygon.numberOfContours - 1];

                for (int i = 0; i < polygon.numberOfContours - 1; ++i)
                {
                    hpts[i] = new List<HS_Point>();

                    for (int j = 0; j < polygon.numberOfPointsPerContour[i + 1]; ++j)
                    {
                        hpts[i].Add((HS_Point)polygon.points[index++]);
                    }
                }

                HS_Plane P = polygon.GetPlane(0.0D);
                HS_Triangulation2DWithPoints triangulation = this.triangulatePolygon2D(pts,hpts, optimize, (new HS_GeometryFactory3D()).createEmbeddedPlane(P));
                HS_KDTreeInteger<HS_Point> pointmap = new HS_KDTreeInteger();

                for (int i = 0; i < polygon.numberOfPoints; ++i)
                {
                    pointmap.Add(polygon.GetPoint(i), i);
                }

                int[] triangles = triangulation.getTriangles();
                int[] edges = triangulation.getEdges();
                //HS_CoordCollection tripoints = triangulation.GetPoints();
                List<HS_Point> tripoints = triangulation.GetPoints();
                int[] intmap = new int[tripoints.Count];
                index = 0;

                //int i;
                for (int i = 0; i < tripoints.Count; ++i)
                {
                    int found = pointmap.getNearestNeighbor(tripoints[i]).value;
                    intmap[index++] = found;
                }

                for (int i = 0; i < triangles.Length; ++i)
                {
                    triangles[i] = intmap[triangles[i]];
                }

                for (int i = 0; i < edges.Length; ++i)
                {
                    edges[i] = intmap[edges[i]];
                }

                return new HS_Triangulation2D(triangles, edges);
            }

            public HS_Triangulation2DWithPoints triangulatePolygon2D(List<HS_Coord> outerPolygon, List<HS_Coord>[] innerPolygons, bool optimize, HS_Map2D context)
            {
                Coordinate[] coords = new Coordinate[outerPolygon.Count + 1];
                HS_Point point = new HS_Point();

                for (int i = 0; i < outerPolygon.Count; ++i)
                {
                    context.mapPoint3D((HS_Coord)outerPolygon[i], point);
                    coords[i] = new Coordinate(point.xd, point.yd, 0.0D);
                }

                context.mapPoint3D((HS_Coord)outerPolygon[0],point);
                coords[outerPolygon.Count] = new Coordinate(point.xd, point.yd, 0.0D);
                LinearRing[] holes = null;
                if (innerPolygons != null)
                {
                    holes = new LinearRing[innerPolygons.Length];

                    for (int j = 0; j < innerPolygons.Length; ++j)
                    {
                        Coordinate[] icoords = new Coordinate[innerPolygons[j].Count + 1];

                        for (int i = 0; i < innerPolygons[j].Count; ++i)
                        {
                            context.mapPoint3D((HS_Coord)innerPolygons[j][i], point);
                            icoords[i] = new Coordinate(point.xd, point.yd, 0.0D);
                        }

                        context.mapPoint3D((HS_Coord)innerPolygons[j][0], point);
                        icoords[innerPolygons[j].Count] = new Coordinate(point.xd, point.yd, 0.0D);
                        LinearRing hole = HS_JTS.JTSgf.CreateLinearRing(icoords) as LinearRing;
                        holes[j] = hole;
                    }
                }

                LinearRing shell = (LinearRing)HS_JTS.JTSgf.CreateLinearRing(coords);
                Polygon inputPolygon = (Polygon)HS_JTS.JTSgf.CreatePolygon(shell, holes);
                int[] ears = this.triangulate(inputPolygon, optimize);
                int[] E = extractEdgesTri(ears);
                List<HS_Point> Points = new List<HS_Point>();

                for (int i = 0; i < this.shellCoords.Count - 1; ++i)
                {
                    point = new HS_Point();
                    context.unmapPoint2D(((Coordinate)this.shellCoords[i]).X, ((Coordinate)this.shellCoords[i]).Y, point);

                    Points.Add(point);
                }

                return new HS_Triangulation2DWithPoints(ears, E, Points);
            }

            public HS_Triangulation2DWithPoints triangulatePolygon2D(int[] polygon, HS_Coord[] points, bool optimize, HS_Map2D context)
            {
                Coordinate[] coords = new Coordinate[polygon.Length + 1];
                HS_Point point = new HS_Point();

                for (int i = 0; i < polygon.Length; ++i)
                {
                    context.mapPoint3D(points[polygon[i]], point);
                    coords[i] = new Coordinate(point.xd, point.yd, (double)i);
                }

                context.mapPoint3D(points[polygon[0]], point);
                coords[polygon.Length] = new Coordinate(point.xd, point.yd, 0.0D);
                Polygon inputPolygon = (Polygon)HS_JTS.JTSgf.CreatePolygon(coords);

                int[] ears = this.triangulate(inputPolygon, optimize);

                for (int i = 0; i < ears.Length; ++i)
                {
                    ears[i] = polygon[ears[i]];
                }

                int[] E = extractEdgesTri(ears);
                List<HS_Point> Points = new List<HS_Point>();

                for (int i = 0; i < this.shellCoords.Count - 1; ++i)
                {
                    point = new HS_Point();
                    context.unmapPoint2D(((Coordinate)this.shellCoords[i]).x, ((Coordinate)this.shellCoords[i]).y, point);
                    Points.Add(point);
                }

                return new HS_Triangulation2DWithPoints(ears, E, Points);
            }

            public HS_Triangulation2D triangulatePolygon2D(int[] polygon, List<HS_Coord> points, bool optimize, HS_Map2D context)
            {
                Coordinate[] coords = new Coordinate[polygon.Length + 1];
                HS_Point point = new HS_Point();

                for (int i = 0; i < polygon.Length; ++i)
                {
                    context.mapPoint3D((HS_Coord)points[polygon[i]], point);
                    coords[i] = new Coordinate(point.xd, point.yd, (double)polygon[i]);
                }

                context.mapPoint3D((HS_Coord)points[polygon[0]], point);
                coords[polygon.Length] = new Coordinate(point.xd, point.yd, (double)polygon[0]);
                Polygon inputPolygon = HS_JTS.JTSgf.CreatePolygon(coords) as Polygon;
                int[] ears = this.triangulate(inputPolygon, optimize);

                for (int i = 0; i < ears.Length; ++i)
                {
                    ears[i] = (int)((Coordinate)this.shellCoords[ears[i]]).Z;
                }

                int[] E = extractEdgesTri(ears);
                return new HS_Triangulation2D(ears, E);
            }

            public int[] triangulateFace(GE_Face face, bool optimize)
            {
                int fo = face.GetFaceDegree();
                Coordinate[] coords = new Coordinate[fo + 1];
                HS_Coord normal = GE_MeshOp.getFaceNormal(face);
                HS_Swizzle coordViewer;
                if (Math.Abs(normal.xd) > Math.Abs(normal.yd))
                {
                    coordViewer = Math.Abs(normal.xd) > Math.Abs(normal.zd) ? HS_Swizzle.YZ : HS_Swizzle.XY;
                }
                else
                {
                    coordViewer = Math.Abs(normal.yd) > Math.Abs(normal.zd) ? HS_Swizzle.ZX : HS_Swizzle.XY;
                }

                HS_KDTreeInteger<HS_Point> pointmap = new HS_KDTreeInteger();
                int i = 0;
                GE_Halfedge GE = face.GetHalfedge();

                do
                {
                    coords[i] = new Coordinate(coordViewer.xd(GE.GetStart()), coordViewer.yd(GE.GetStart()), 0.0D);
                    pointmap.Add(new HS_Point(coordViewer.xd(GE.GetStart()), coordViewer.yd(GE.GetStart())), i);
                    ++i;
                    GE = GE.GetNextInFace();
                } while (GE != face.GetHalfedge());

                coords[i] = new Coordinate(coordViewer.xd(GE.GetStart()), coordViewer.yd(GE.GetStart()), 0.0D);
                LinearRing shell = HS_JTS.JTSgf.CreateLinearRing(coords) as LinearRing;
                Polygon inputPolygon = HS_JTS.JTSgf.CreatePolygon(shell) as Polygon;
                int[] ears = this.triangulate(inputPolygon, optimize);
                List<HS_Point> tripoints = new List<HS_Point>();

                for (int j = 0; j < this.shellCoords.Count - 1; ++j)
                {
                    tripoints.Add(new HS_Point(((Coordinate)this.shellCoords[j]).X, ((Coordinate)this.shellCoords[j]).Y));
                }

                int[] intmap = new int[tripoints.Count];
                i = 0;

                //int found;
                //for (Iterator var16 = tripoints.iterator(); var16.hasNext(); intmap[i++] = found)
                //{
                //    HS_Coord point = (HS_Coord)var16.next();
                //    found = pointmap.getNearestNeighbor(point).value;
                //}
                foreach (HS_Coord point in tripoints)
                {
                    int found = pointmap.getNearestNeighbor(point).value;
                    intmap[i++] = found;
                }

                for (int j = 0; j < ears.Length; ++j)
                {
                    ears[j] = intmap[ears[j]];
                }

                return ears;
            }

            public int[] triangulateSimplePolygon(HS_Polygon polygon, bool optimize)
            {
                int fo = polygon.getNumberOfShellPoints();
                Coordinate[] coords = new Coordinate[fo + 1];
                HS_Coord normal = polygon.GetNormal();
                HS_Swizzle coordViewer;
                if (Math.Abs(normal.xd) > Math.Abs(normal.yd))
                {
                    coordViewer = Math.Abs(normal.xd) > Math.Abs(normal.zd) ? HS_Swizzle.YZ : HS_Swizzle.XY;
                }
                else
                {
                    coordViewer = Math.Abs(normal.yd) > Math.Abs(normal.zd) ? HS_Swizzle.ZX : HS_Swizzle.XY;
                }

                HS_KDTreeInteger<HS_Point> pointmap = new HS_KDTreeInteger();

                for (int i = 0; i <= polygon.getNumberOfShellPoints(); ++i)
                {
                    HS_Coord c = polygon.GetPoint(i);
                    coords[i] = new Coordinate(coordViewer.xd(c), coordViewer.yd(c), 0.0D);
                    pointmap.Add(new HS_Point(coordViewer.xd(c), coordViewer.yd(c)), i);
                    ++i;
                }

                LinearRing shell = HS_JTS.JTSgf.CreateLinearRing(coords) as LinearRing;
                Polygon inputPolygon = HS_JTS.JTSgf.CreatePolygon(shell) as Polygon;
                int[] ears = this.triangulate(inputPolygon, optimize);
                List<HS_Point> tripoints = new List<HS_Point>();

                for (int j = 0; j < this.shellCoords.Count - 1; ++j)
                {
                    tripoints.Add(new HS_Point(((Coordinate)this.shellCoords[j]).X, ((Coordinate)this.shellCoords[j]).Y));
                }

                int[] intmap = new int[tripoints.Count];
                int i = 0;

                //int found;
                //for (Iterator var16 = tripoints.iterator(); var16.hasNext(); intmap[i++] = found)
                //{
                //    HS_Coord point = (HS_Coord)var16.next();
                //    found = pointmap.getNearestNeighbor(point).value;
                //}
                foreach (HS_Coord point in tripoints)
                {
                    //HS_Coord point = (HS_Coord)var16.next();
                    //found = pointmap.getNearestNeighbor(point).value;
                    int found = pointmap.getNearestNeighbor(point).value;
                    intmap[i++] = found;

                }
                for (int j = 0; j < ears.Length; ++j)
                {
                    ears[j] = intmap[ears[j]];
                }

                return ears;
            }

            private static int[] extractEdgesTri(int[] ears)
            {
                int f = ears.Length;
                //UnifiedMap<Long, int[]> map = new UnifiedMap();
                Dictionary<long, int[]> map = new Dictionary<long, int[]>();
                //int i;
                for (int i = 0; i < ears.Length; i += 3)
                {
                    int v0 = ears[i];
                    i = ears[i + 1];
                    int v2 = ears[i + 2];
                    long index = getIndex(v0, i, f);
                    //map.put(index, new int[] { v0, i });
                    map.Add(index, new int[] { v0, i });
                    index = getIndex(i, v2, f);
                    map.Add(index, new int[] { i, v2 });
                    //map.put(index, new int[] { i, v2 });
                    index = getIndex(v2, v0, f);
                    map.Add(index, new int[] { v2, v0 });
                    //map.put(index, new int[] { v2, v0 });
                }

                int[] edges = new int[2 * map.Count];
                //Collection<int[]> values = map.values();

                var values =map.Values;

                int j= 0;

                //for (Iterator var12 = values.iterator(); var12.hasNext(); ++i)
                //{
                //    int[] value = (int[])var12.next();
                //    edges[2 * i] = value[0];
                //    edges[2 * i + 1] = value[1];
                //}
                foreach (int[] value in values)
                {
                    edges[2 * j] = value[0];
                    edges[2 * j + 1] = value[1];
                    j++;
                }

                return edges;
            }

            private static long getIndex(int i, int j, int f)
            {
                return (long)(i > j ? j + i * f : i + j * f);
            }

            private int[] triangulate(Polygon inputPolygon, bool improve)
            {
                List<HS_JTS.PolygonTriangulatorJTS.IndexedTriangle> earList = new List<IndexedTriangle>();
                this.createshell(inputPolygon);
                Geometry test = (Geometry)inputPolygon.Buffer(0.0D);
                int N = this.shellCoords.Count - 1;
                this.shellCoordAvailable = new bool[N];
                //Arrays.fill(this.shellCoordAvailable, true);
                for(int i = 0; i < this.shellCoordAvailable.Length; i++)
                {
                    this.shellCoordAvailable[i] = true;
                }

                bool finisGEd = false;
                bool found = false;
                int k0 = 0;
                int k1 = 1;
                int k2 = 2;
                int firstK = 0;

                do
                {
                    for (found = false; CGAlgorithms.ComputeOrientation((Coordinate)this.shellCoords[k0], (Coordinate)this.shellCoords[k1], (Coordinate)this.shellCoords[k2]) == 0; k2 = this.nextshellCoord(k2 + 1))
                    {
                        k0 = k1;
                        if (k1 == firstK)
                        {
                            finisGEd = true;
                            break;
                        }

                        k1 = k2;
                    }

                    if (!finisGEd && this.isValidEdge(k0, k2))
                    {
                        LineString ls = (LineString)HS_JTS.JTSgf.CreateLineString(new Coordinate[] { (Coordinate)this.shellCoords[k0], (Coordinate)this.shellCoords[k2] });
                        if (test.Covers(ls))
                        {
                            Polygon earPoly = (Polygon)HS_JTS.JTSgf.CreatePolygon(HS_JTS.JTSgf.CreateLinearRing(new Coordinate[] { (Coordinate)this.shellCoords[k0], (Coordinate)this.shellCoords[k1], (Coordinate)this.shellCoords[k2], (Coordinate)this.shellCoords[k0] }), (LinearRing[])null);
                            if (test.Covers(earPoly))
                            {
                                found = true;
                                HS_JTS.PolygonTriangulatorJTS.IndexedTriangle ear = new HS_JTS.PolygonTriangulatorJTS.IndexedTriangle(k0, k1, k2);
                                earList.Add(ear);
                                this.shellCoordAvailable[k1] = false;
                                --N;
                                k0 = this.nextshellCoord(0);
                                k1 = this.nextshellCoord(k0 + 1);
                                k2 = this.nextshellCoord(k1 + 1);
                                firstK = k0;
                                if (N < 3)
                                {
                                    finisGEd = true;
                                }
                            }
                        }
                    }

                    if (!finisGEd && !found)
                    {
                        k0 = k1;
                        if (k1 == firstK)
                        {
                            finisGEd = true;
                        }
                        else
                        {
                            k1 = k2;
                            k2 = this.nextshellCoord(k2 + 1);
                        }
                    }
                } while (!finisGEd);

                if (improve)
                {
                    this.doImprove(earList);
                }

                int[] tris = new int[3 * earList.Count];

                for (int i = 0; i < earList.Count; ++i)
                {
                    int[] tri = ((HS_JTS.PolygonTriangulatorJTS.IndexedTriangle)earList[i]).getVertices();
                    tris[3 * i] = tri[0];
                    tris[3 * i + 1] = tri[1];
                    tris[3 * i + 2] = tri[2];
                }

                return tris;
            }

            protected HS_Polygon makeSimplePolygon(HS_Polygon polygon)
            {
                Polygon poly = HS_JTS.toJTSPolygon2D(polygon);
                this.createshell(poly);
                Coordinate[] coords = new Coordinate[this.shellCoords.Count];
                return HS_JTS.CreatePolygonFromJTSPolygon2D((Polygon)HS_JTS.JTSgf.CreatePolygon((Coordinate[])this.shellCoords.ToArray<Coordinate>()));
            }

            protected void createshell(Polygon inputPolygon)
            {
                Polygon poly = (Polygon)inputPolygon.Clone();
                this.shellCoords = new List<Coordinate>();
                List<Geometry> orderedHoles = getOrderedHoles(poly);
                Coordinate[] coords = poly.ExteriorRing.Coordinates;
                this.shellCoords.AddRange(coords.ToList<Coordinate>());

                for (int i = 0; i < orderedHoles.Count; ++i)
                {
                    this.joinHoleToshell(inputPolygon, (Geometry)orderedHoles[i]);
                }

            }

            private bool isValidEdge(int index0, int index1)
            {
                Coordinate[] line = new Coordinate[] { (Coordinate)this.shellCoords[index0], (Coordinate)this.shellCoords[index1] };

                for (int index = this.nextshellCoord(index0 + 1); index != index0; index = this.nextshellCoord(index + 1))
                {
                    if (index != index1)
                    {
                        Coordinate c = (Coordinate)this.shellCoords[index];
                        if (!c.Equals2D(line[0]) && !c.Equals2D(line[1]) && CGAlgorithms.IsOnLine(c, line))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            private int nextshellCoord(int pos)
            {
                int pnew;
                for (pnew = pos % this.shellCoordAvailable.Length; !this.shellCoordAvailable[pnew]; pnew = (pnew + 1) % this.shellCoordAvailable.Length)
                {
                }

                return pnew;
            }

            private void doImprove(List<HS_JTS.PolygonTriangulatorJTS.IndexedTriangle> earList)
            {
                HS_JTS.PolygonTriangulatorJTS.EdgeFlipper ef = new HS_JTS.PolygonTriangulatorJTS.EdgeFlipper(this.shellCoords);

                bool changed;
                do
                {
                    changed = false;

                    for (int i = 0; i < earList.Count - 1 && !changed; ++i)
                    {
                        HS_JTS.PolygonTriangulatorJTS.IndexedTriangle ear0 = (HS_JTS.PolygonTriangulatorJTS.IndexedTriangle)earList[i];

                        for (int j = i + 1; j < earList.Count && !changed; ++j)
                        {
                            HS_JTS.PolygonTriangulatorJTS.IndexedTriangle ear1 = (HS_JTS.PolygonTriangulatorJTS.IndexedTriangle)earList[j];
                            int[] sharedVertices = ear0.getSharedVertices(ear1);
                            if (sharedVertices != null && sharedVertices.Length == 2 && ef.flip(ear0, ear1, sharedVertices))
                            {
                                changed = true;
                            }
                        }
                    }
                } while (changed);

            }

            private static List<Geometry> getOrderedHoles(Polygon poly)
            {
                List<Geometry> holes = new List<Geometry>();
                List<HS_JTS.PolygonTriangulatorJTS.IndexedEnvelope> bounds = new List<IndexedEnvelope>();
                if (poly.NumInteriorRings > 0)
                {
                    int i;
                    for (i = 0; i < poly.NumInteriorRings; ++i)
                    {
                        bounds.Add(new IndexedEnvelope(i, poly.GetInteriorRingN(i).EnvelopeInternal));
                    }

                    //Collections.sort(bounds, new IndexedEnvelopeComparator((HS_JTS.PolygonTriangulatorJTS.IndexedEnvelopeComparator)null));
                    bounds.Sort();


                    for (i = 0; i < bounds.Count; ++i)
                    {
                        holes.Add(poly.GetInteriorRingN(((HS_JTS.PolygonTriangulatorJTS.IndexedEnvelope)bounds[i]).index) as LineString);
                    }
                }

                return holes;
            }

            private void joinHoleToshell(Polygon inputPolygon, Geometry hole)
            {
                double minD2 = 1.7976931348623157E308D;
                int shellVertexIndex = -1;
                int Ns = this.shellCoords.Count - 1;
                int holeVertexIndex = getLowestVertex(hole);
                Coordinate[] holeCoords = hole.Coordinates;
                Coordinate ch = holeCoords[holeVertexIndex];
                List<HS_JTS.PolygonTriangulatorJTS.IndexedDouble> distanceList = new List<IndexedDouble>();

                for (int i = Ns - 1; i >= 0; --i)
                {
                    Coordinate cs = (Coordinate)this.shellCoords[i];
                    double d2 = (ch.X - cs.X) * (ch.X - cs.X) + (ch.Y - cs.Y) * (ch.Y - cs.Y);
                    if (d2 < minD2)
                    {
                        minD2 = d2;
                        shellVertexIndex = i;
                    }

                    distanceList.Add(new IndexedDouble(i, d2));
                }

                LineString join = (LineString)HS_JTS.JTSgf.CreateLineString(new Coordinate[] { ch, (Coordinate)this.shellCoords[shellVertexIndex] });
                if (inputPolygon.Covers(join))
                {
                    this.doJoinHole(shellVertexIndex, holeCoords, holeVertexIndex);
                }
                else
                {

                    //System.Collections.sort(distanceList, new IndexedDoubleComparator((HS_JTS.PolygonTriangulatorJTS.IndexedDoubleComparator)null));
                    distanceList.Sort(new IndexedDoubleComparator());
                    for (int i = 1; i < distanceList.Count; ++i)
                    {
                        join = HS_JTS.JTSgf.CreateLineString(new Coordinate[] { ch, (Coordinate)this.shellCoords[((HS_JTS.PolygonTriangulatorJTS.IndexedDouble)distanceList[i]).index] }) as LineString;
                        if (inputPolygon.Covers(join))
                        {
                            shellVertexIndex = ((HS_JTS.PolygonTriangulatorJTS.IndexedDouble)distanceList[i]).index;
                            this.doJoinHole(shellVertexIndex, holeCoords, holeVertexIndex);
                            return;
                        }
                    }

                }
            }

            private void doJoinHole(int shellVertexIndex, Coordinate[] holeCoords, int holeVertexIndex)
            {
                List<Coordinate> newCoords = new List<Coordinate>();
                newCoords.Add(new Coordinate((Coordinate)this.shellCoords[shellVertexIndex]));
                int N = holeCoords.Length - 1;
                int i = holeVertexIndex;

                do
                {
                    newCoords.Add(new Coordinate(holeCoords[i]));
                    i = (i + 1) % N;
                } while (i != holeVertexIndex);

                newCoords.Add(new Coordinate(holeCoords[holeVertexIndex]));
                this.shellCoords.InsertRange(shellVertexIndex, newCoords);
            }

            private static int getLowestVertex(Geometry geom)
            {
                Coordinate[] coords = geom.Coordinates;
                double minY = geom.EnvelopeInternal.MinY;

                for (int i = 0; i < coords.Length; ++i)
                {
                    if (Math.Abs(coords[i].Y - minY) < HS_Epsilon.EPSILON)
                    {
                        return i;
                    }
                }

                throw new Exception("Failed to find lowest vertex");
            }

            private class EdgeFlipper
            {
                private List<Coordinate> shellCoords;

                public EdgeFlipper(List<Coordinate> shellCoords)
                {
                    this.shellCoords = new List<Coordinate>();
                }

                public bool flip(HS_JTS.PolygonTriangulatorJTS.IndexedTriangle ear0, HS_JTS.PolygonTriangulatorJTS.IndexedTriangle ear1, int[] sharedVertices)
                {
                    if (sharedVertices != null && sharedVertices.Length == 2)
                    {
                        Coordinate shared0 = (Coordinate)this.shellCoords[sharedVertices[0]];
                        Coordinate shared1 = (Coordinate)this.shellCoords[sharedVertices[1]];
                        int[] vertices = ear0.getVertices();

                        int i;
                        for (i = 0; vertices[i] == sharedVertices[0] || vertices[i] == sharedVertices[1]; ++i)
                        {
                        }

                        int v0 = vertices[i];
                        bool reverse = false;
                        if (vertices[(i + 1) % 3] == sharedVertices[0])
                        {
                            reverse = true;
                        }

                        Coordinate c0 = (Coordinate)this.shellCoords[v0];
                        i = 0;

                        for (vertices = ear1.getVertices(); vertices[i] == sharedVertices[0] || vertices[i] == sharedVertices[1]; ++i)
                        {
                        }

                        int v1 = vertices[i];
                        Coordinate c1 = (Coordinate)this.shellCoords[v1];
                        int dir0 = CGAlgorithms.OrientationIndex(c0, c1, shared0);
                        int dir1 = CGAlgorithms.OrientationIndex(c0, c1, shared1);
                        if (dir0 == -dir1 && c0.Distance(c1) < shared0.Distance(shared1))
                        {
                            if (reverse)
                            {
                                ear0.setPoints(sharedVertices[0], v1, v0);
                                ear1.setPoints(v0, v1, sharedVertices[1]);
                            }
                            else
                            {
                                ear0.setPoints(sharedVertices[0], v0, v1);
                                ear1.setPoints(v1, v0, sharedVertices[1]);
                            }

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            private class IndexedDouble
            {
                public int index;
                public double value;

                public IndexedDouble(int i, double v)
                {
                    this.index = i;
                    this.value = v;
                }
            }

            private class IndexedDoubleComparator : IComparer<IndexedDouble>
            {
                public IndexedDoubleComparator ()
                {
                }

                public int Compare(HS_JTS.PolygonTriangulatorJTS.IndexedDouble o1, HS_JTS.PolygonTriangulatorJTS.IndexedDouble o2)
                {
                    double delta = o1.value - o2.value;
                    if (Math.Abs(delta) < HS_Epsilon.EPSILON)
                    {
                        return 0;
                    }
                    else
                    {
                        return delta > 0.0D ? 1 : -1;
                    }
                }

                public int CompareTo(IndexedDouble other)
                {
                    throw new NotImplementedException();
                }
            }

            private class IndexedEnvelope
            {
                public int index;
                public Envelope envelope;

                public IndexedEnvelope(int i, Envelope env)
                {
                    this.index = i;
                    this.envelope = env;
                }
            }

            private class IndexedEnvelopeComparator : IComparable<IndexedEnvelope>
            {
                IndexedEnvelopeComparator(IndexedEnvelopeComparator i)
                {
                }

                public int CompareTo(IndexedEnvelope other)
                {
                    throw new NotImplementedException();
                }

                int compare(HS_JTS.PolygonTriangulatorJTS.IndexedEnvelope o1, HS_JTS.PolygonTriangulatorJTS.IndexedEnvelope o2)
                {
                    double delta = o1.envelope.MinY - o2.envelope.MinY;
                    if (Math.Abs(delta) < HS_Epsilon.EPSILON)
                    {
                        delta = o1.envelope.MinX - o2.envelope.MinX;
                        if (Math.Abs(delta) < HS_Epsilon.EPSILON)
                        {
                            return 0;
                        }
                    }

                    return delta > 0.0D ? 1 : -1;
                }
            }

            private class IndexedTriangle
            {
                private int[] points = new int[3];

                public IndexedTriangle(int v0, int v1, int v2)
                {
                    this.setPoints(v0, v1, v2);
                }

                public void setPoints(int v0, int v1, int v2)
                {
                    this.points[0] = v0;
                    this.points[1] = v1;
                    this.points[2] = v2;
                }

                public int[] getVertices()
                {
                    int[] copy = new int[3];

                    for (int i = 0; i < 3; ++i)
                    {
                        copy[i] = this.points[i];
                    }

                    return copy;
                }

                public int[] getSharedVertices(HS_JTS.PolygonTriangulatorJTS.IndexedTriangle otGEr)
                {
                    int count = 0;
                    bool[] shared = new bool[3];

                    int i;
                    for (int j = 0; j < 3; ++j)
                    {
                        for (j = 0; j < 3; ++j)
                        {
                            if (this.points[j] == otGEr.points[j])
                            {
                                ++count;
                                shared[j] = true;
                            }
                        }
                    }

                    int[] common = null;
                    if (count > 0)
                    {
                        common = new int[count];
                        i = 0;

                        for (int var6 = 0; i < 3; ++i)
                        {
                            if (shared[i])
                            {
                                common[var6++] = this.points[i];
                            }
                        }
                    }

                    return common;
                }
            }
        }

        //static class ShapeReader
        //{
        //    private static AffineTransform INVERT_Y = AffineTransform.getScaleInstance(1.0D, -1.0D);
        //    private HS_GeometryFactory geometryfactory = new HS_GeometryFactoryd;

        //    ShapeReader()
        //    {
        //    }

        //    List<HS_Polygon> read(Shape shp, double flatness)
        //    {
        //        PathIterator pathIt = shp.getPathIterator(INVERT_Y, flatness);
        //        return this.read(pathIt);
        //    }

        //    List<HS_Polygon> read(PathIterator pathIt)
        //    {
        //        List<Coordinate[]> pathPtSeq = toCoordinates(pathIt);
        //        HS_JTS.ShapeReader.RingTree tree = new HS_JTS.ShapeReader.RingTree();

        //        for (int i = 0; i < pathPtSeq.Count; ++i)
        //        {
        //            Coordinate[] pts = (Coordinate[])pathPtSeq.Get(i);
        //            LinearRing ring = HS_JTS.JTSgf.CreateLinearRing(pts);
        //            tree.Add(ring);
        //        }

        //        return tree.extractPolygons();
        //    }

        //    private static List<Coordinate[]> toCoordinates(PathIterator pathIt)
        //    {
        //        ArrayList coordArrays = new ArrayList();

        //        while (!pathIt.isDone())
        //        {
        //            Coordinate[] pts = nextCoordinateArray(pathIt);
        //            if (pts == null)
        //            {
        //                break;
        //            }

        //            coordArrays.Add(pts);
        //        }

        //        return coordArrays;
        //    }

        //    private static Coordinate[] nextCoordinateArray(PathIterator pathIt)
        //    {
        //        double[] pathPt = new double[6];
        //        CoordinateList coordList = null;
        //        bool isDone = false;

        //        while (!pathIt.isDone())
        //        {
        //            int segType = pathIt.currentSegment(pathPt);
        //            switch (segType)
        //            {
        //                case 0:
        //                    if (coordList != null)
        //                    {
        //                        isDone = true;
        //                    }
        //                    else
        //                    {
        //                        coordList = new CoordinateList();
        //                        coordList.Add(new Coordinate(pathPt[0], pathPt[1]));
        //                        pathIt.next();
        //                    }
        //                    break;
        //                case 1:
        //                    coordList.Add(new Coordinate(pathPt[0], pathPt[1]));
        //                    pathIt.next();
        //                    break;
        //                case 2:
        //                case 3:
        //                default:
        //                    throw new IllegalArgumentException("unhandled (non-linear) segment type encountered");
        //                case 4:
        //                    coordList.closeRing();
        //                    pathIt.next();
        //                    isDone = true;
        //            }

        //            if (isDone)
        //            {
        //                break;
        //            }
        //        }

        //        return coordList.toCoordinateArrayd;
        //    }

        //    private static class RingNode
        //    {
        //        HS_JTS.ShapeReader.RingNode parent;
        //        List<HS_JTS.ShapeReader.RingNode> children;
        //        LinearRing ring;
        //        Polygon poly;
        //        bool hole;

        //        RingNode()
        //        {
        //            this.parent = null;
        //            this.ring = null;
        //            this.children = new ArrayList();
        //            this.hole = true;
        //        }

        //        RingNode(HS_JTS.ShapeReader.RingNode parent, LinearRing ring)
        //        {
        //            this.parent = parent;
        //            this.ring = ring;
        //            Coordinate[] coords = ring.Coordinates;
        //            this.poly = HS_JTS.JTSgf.CreatePolygon(coords);
        //            this.hole = CGAlgorithms.isCCW(coords);
        //            this.children = new ArrayList();
        //        }
        //    }

        //    private class RingTree
        //    {
        //        HS_JTS.ShapeReader.RingNode root = new HS_JTS.ShapeReader.RingNode();

        //        RingTree()
        //        {
        //        }

        //        void add(LinearRing ring)
        //        {
        //            Polygon poly = HS_JTS.JTSgf.CreatePolygon(ring);
        //            HS_JTS.ShapeReader.RingNode currentParent = this.root;

        //            HS_JTS.ShapeReader.RingNode foundParent;
        //            do
        //            {
        //                foundParent = null;

        //                for (int ix = 0; ix < currentParent.children.Count; ++ix)
        //                {
        //                    HS_JTS.ShapeReader.RingNode node = (HS_JTS.ShapeReader.RingNode)currentParent.children.Get(ix);
        //                    Polygon otGEr = node.poly;
        //                    if (poly.within(otGEr))
        //                    {
        //                        foundParent = node;
        //                        currentParent = node;
        //                        break;
        //                    }
        //                }
        //            } while (foundParent != null);

        //            HS_JTS.ShapeReader.RingNode newNode = new HS_JTS.ShapeReader.RingNode(currentParent, ring);
        //            List<HS_JTS.ShapeReader.RingNode> nodesToRemove = new ArrayList();

        //            for (int i = 0; i < currentParent.children.Count; ++i)
        //            {
        //                HS_JTS.ShapeReader.RingNode nodex = (HS_JTS.ShapeReader.RingNode)currentParent.children.Get(i);
        //                Polygon otGErx = nodex.poly;
        //                if (otGErx.within(poly))
        //                {
        //                    newNode.children.Add(nodex);
        //                    nodesToRemove.Add(nodex);
        //                }
        //            }

        //            currentParent.children.removeAll(nodesToRemove);
        //            currentParent.children.Add(newNode);
        //        }

        //        List<HS_Polygon> extractPolygons()
        //        {
        //            List<HS_Polygon> polygons = new ArrayList();
        //            List<HS_JTS.ShapeReader.RingNode> shellNodes = new ArrayList();
        //            this.addExteriorNodes(this.root, shellNodes);
        //            Iterator var4 = shellNodes.iterator();

        //            while (true)
        //            {
        //                while (var4.hasNext())
        //                {
        //                    HS_JTS.ShapeReader.RingNode node = (HS_JTS.ShapeReader.RingNode)var4.next();
        //                    int count = 0;

        //                    for (int i = 0; i < node.children.Count; ++i)
        //                    {
        //                        if (((HS_JTS.ShapeReader.RingNode)node.children.Get(i)).hole)
        //                        {
        //                            ++count;
        //                        }
        //                    }

        //                    LinearRing[] holes = new LinearRing[count];
        //                    int index = 0;

        //                    for (int ix = 0; ix < node.children.Count; ++ix)
        //                    {
        //                        if (((HS_JTS.ShapeReader.RingNode)node.children.Get(ix)).hole)
        //                        {
        //                            holes[index++] = ((HS_JTS.ShapeReader.RingNode)node.children.Get(ix)).ring;
        //                        }
        //                    }

        //                    Geometry result = HS_JTS.JTSgf.CreatePolygon(node.ring, holes);
        //                    if (result.getGeometryType().equals("Polygon"))
        //                    {
        //                        polygons.Add(HS_JTS.CreatePolygonFromJTSPolygon2D((Polygon)result));
        //                    }
        //                    else if (result.getGeometryType().equals("MultiPolygon"))
        //                    {
        //                        for (int j = 0; j < result.getNumGeometries(); ++j)
        //                        {
        //                            Geometry ggeo = result.getGeometryN(j);
        //                            polygons.Add(HS_JTS.CreatePolygonFromJTSPolygon2D((Polygon)ggeo));
        //                        }
        //                    }
        //                }

        //                return polygons;
        //            }
        //        }

        //        void addExteriorNodes(HS_JTS.ShapeReader.RingNode parent, List<HS_JTS.ShapeReader.RingNode> shellNodes)
        //        {
        //            HS_JTS.ShapeReader.RingNode node;
        //            for (Iterator var4 = parent.children.iterator(); var4.hasNext(); this.addExteriorNodes(node, shellNodes))
        //            {
        //                node = (HS_JTS.ShapeReader.RingNode)var4.next();
        //                if (!node.hole)
        //                {
        //                    shellNodes.Add(node);
        //                }
        //            }

        //        }
        //    }
        //}
    }
}
