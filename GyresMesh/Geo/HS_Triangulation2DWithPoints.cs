using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Triangulation2DWithPoints:HS_Triangulation2D
    {
        private List<HS_Coord> _points;
        public HS_Triangulation2DWithPoints()
        {

        }

        public HS_Triangulation2DWithPoints(int[] T, int[] E, List<HS_Coord> P):base(T,E)
        {
            this._points = P;

        }

        public HS_Triangulation2DWithPoints(int[] T, int[] E, HS_Coord[] P) : base(T, E)
        {
            this._points = P.ToList<HS_Coord>();

        }
        public HS_Triangulation2DWithPoints(int[] T, List<HS_Coord> P):base(T)
        {
            this._points = P;
        }
        public HS_Triangulation2DWithPoints(int[] T, HS_Coord[] P) : base(T)
        {
            this._points = P.ToList<HS_Coord>();
        }
        protected HS_Triangulation2DWithPoints(HS_Triangulation2D tri):base(tri.getTriangles(),tri.getEdges())
        {
            this._points = null;
        }

        public List<HS_Coord> getPoints()
        {
            return this._points;
        }

    }
}
