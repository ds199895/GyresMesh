namespace Hsy.Geo
{
     /**
     *
     * HS_Map2D is an interface for classes that transform between 3D coordinates
     * and 2D coordinates through some form of mapping or projection.
     *
     *
     */
    public interface HS_Map2D: HS_Map
    {
        public void unmapPoint2D(HS_Coord p, HS_MutableCoord result);

        public void unmapPoint2D(double x, double y,  HS_MutableCoord result);

        public void unmapVector2D(HS_Coord p, out HS_MutableCoord result);

        public void unmapVector2D(double x, double y,  HS_MutableCoord result);

        public HS_Coord unmapPoint2D(HS_Coord p);

        public HS_Coord unmapPoint2D(double x, double y);

        public HS_Coord unmapVector2D(HS_Coord p);

        public HS_Coord unmapVector2D(double x, double y);
    }
}