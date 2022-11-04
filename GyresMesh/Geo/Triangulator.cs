using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class Triangulator
    {
        public static bool Triangulate(HS_Vector[] vertices, out int[] triangles, out string errorMessage)
        {
            triangles = null;
            errorMessage = string.Empty;
            if (vertices is null)
            {
                errorMessage = "The vertex list is null.";
                return false;
            }
            if (vertices.Length < 3)
            {
                errorMessage = "The length of vertices must large than 3.";
                return false;
            }

            List<int> indexList = new List<int>();
            for(int i = 0; i < vertices.Length; i++)
            {
                indexList.Add(i);
            }
            int totalTriangleCount = vertices.Length - 2;
            int totalTriangleIndexCount = totalTriangleCount * 3;
            int triangleIndexCount = 0;
            while (indexList.Count > 3)
            {
                for(int i = 0; i < indexList.Count; i++)
                {
                    int prv = (i - 1 + indexList.Count) % indexList.Count;
                    int nxt = (i + 1) % indexList.Count;
                    int a = indexList[i];

                    int b = indexList[prv];
                    int c = indexList[nxt];

                    HS_Vector va = vertices[a];
                    HS_Vector vb = vertices[b];
                    HS_Vector vc = vertices[c];

                    HS_Vector va_to_b = vb - va;
                    HS_Vector va_to_c = vc - va;
                    if (va_to_b.cross2D(va_to_c) < 0f)
                    {
                        continue;
                    }
                    bool isEar = true;
                    for(int j = 0; j < vertices.Length; j++)
                    {
                        if (j == a || j == b || j == c)
                        {
                            continue;
                        }
                        HS_Vector p = vertices[j];
                        if(IsPointInTriangle(p, vb, va, vc))
                        {
                            isEar = false;
                            break;
                        }
                    }
                    if (isEar)
                    {
                        triangles[triangleIndexCount++] = b;
                        triangles[triangleIndexCount++] = a;
                        triangles[triangleIndexCount++] = c;
                        indexList.RemoveAt(i);
                        break; 
                    }
                }
            }
            triangles[triangleIndexCount++] = indexList[0];
            triangles[triangleIndexCount++] = indexList[1];
            triangles[triangleIndexCount++] = indexList[2];
            return true;
        }
        public static bool IsSimplePolygon(HS_Vector[] vertices)
        {
            throw new NotFiniteNumberException();

        }
        public static bool containsCollinearEdges(HS_Vector[] vertices)
        {
            throw new NotImplementedException();
        }
        public static bool IsPointInTriangle(HS_Vector p, HS_Vector a, HS_Vector b, HS_Vector c)
        {
            HS_Vector ab = b - a;
            HS_Vector bc = c - b;
            HS_Vector ca = a - c;

            HS_Vector ap = p - a;
            HS_Vector bp = p - b;
            HS_Vector cp = p - c;
            double cross1 = ab.cross2D(ap);
            double cross2 = bc.cross2D(bp);
            double cross3 = ca.cross2D(cp);
            if (cross1 > 0d || cross2 > 0d || cross3 > 0d)
            {
                return false;
            }
            return true;
        }

    }
}
