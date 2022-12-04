//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hsy.Geo
//{
//    public class Triangulator
//    {
//        public static bool Triangulate(List<HS_Vector> shell,List<List<HS_Vector>>holes, out int[] triangles, out string errorMessage)
//        {
//            triangles = null;
//            errorMessage = string.Empty;
//            if (shell is null)
//            {
//                errorMessage = "The vertex list is null.";
//                return false;
//            }
//            if (shell.Count < 3)
//            {
//                errorMessage = "The length of vertices must large than 3.";
//                return false;
//            }

//            List<int> indexList = new List<int>();
//            for(int i = 0; i < shell.Count; i++)
//            {
//                indexList.Add(i);
//            }
//            int totalTriangleCount = shell.Count - 2;
//            int totalTriangleIndexCount = totalTriangleCount * 3;
            
//            triangles = new int[totalTriangleIndexCount];
//            int triangleIndexCount = 0;
//            while (indexList.Count > 3)
//            {
//                for(int i = 0; i < indexList.Count; i++)
//                {
//                    int prv = (i - 1 + indexList.Count) % indexList.Count;
//                    int nxt = (i + 1) % indexList.Count;

//                    int a = indexList[i];

//                    int b = indexList[prv];
//                    int c = indexList[nxt];

//                    HS_Vector va = shell[a];
//                    HS_Vector vb = shell[b];
//                    HS_Vector vc = shell[c];
        
//                    HS_Vector va_to_b = vb - va;
//                    HS_Vector va_to_c = vc - va;
 
//                    if (va_to_b.cross2D(va_to_c) < 0f)
//                    {
//                        continue;
//                    }
//                    bool isEar = true;
//                    for(int j = 0; j < shell.Length; j++)
//                    {
//                        if (j == a || j == b || j == c)
//                        {
//                            continue;
//                        }
//                        HS_Vector p = shell[j];
//                        if(IsPointInTriangle(p, vb, va, vc))
//                        {
//                            isEar = false;
//                            break;
//                        }
//                    }
//                    if (isEar)
//                    {
//                        triangles[triangleIndexCount++] = b;
//                        triangles[triangleIndexCount++] = a;
//                        triangles[triangleIndexCount++] = c;
//                        indexList.RemoveAt(i);
//                        break; 
//                    }
//                }
//            }
//            triangles[triangleIndexCount++] = indexList[0];
//            triangles[triangleIndexCount++] = indexList[1];
//            triangles[triangleIndexCount++] = indexList[2];
//            return true;
//        }
//        private void ProcessHoles(List<List<HS_Vector>> _holes)
//        {
//            for (int h = 0; h < _holes.Count; h++)
//            {
//                List<Polygon> polygons = new List<Polygon>();
//                polygons.Add(_mainPointList);
//                polygons.AddRange(_holes);
//                ConnectionEdge M, P;
//                GetVisiblePoints(h + 1, polygons, out M, out P);
//                if (M.Origin.Equals(P.Origin))
//                    throw new Exception();

//                var insertionEdge = P;
//                InsertNewEdges(insertionEdge, M);
//                _holes.RemoveAt(h);
//                h--;
//            }
//        }




//        public static bool IsSimplePolygon(HS_Vector[] vertices)
//        {
//            throw new NotFiniteNumberException();

//        }
//        public static bool containsCollinearEdges(HS_Vector[] vertices)
//        {
//            throw new NotImplementedException();
//        }
//        public static bool IsPointInTriangle(HS_Vector p, HS_Vector a, HS_Vector b, HS_Vector c)
//        {
//            HS_Vector ab = b - a;
//            HS_Vector bc = c - b;
//            HS_Vector ca = a - c;

//            HS_Vector ap = p - a;
//            HS_Vector bp = p - b;
//            HS_Vector cp = p - c;
//            double cross1 = ab.cross2D(ap);
//            double cross2 = bc.cross2D(bp);
//            double cross3 = ca.cross2D(cp);
//            if (cross1 > 0d || cross2 > 0d || cross3 > 0d)
//            {
//                return false;
//            }
//            return true;
//        }

//    }
//}
