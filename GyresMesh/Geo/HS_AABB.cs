using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_AABB
    {
        public double[] _min;
        public double[] _max;
        int _id;

        public HS_AABB(HS_Coord p)
        {
            init();
            setToNull();
            expandToInclude(p);
        }

        public HS_AABB(HS_Coord[] points)
        {
            if (points == null)
            {
                throw new NullReferenceException("Array not initialized.");

            }
            if (points.Length == 0)
            {
                throw new ArgumentException("Array has zero size.");
            }
            HS_Coord point = points[0];
            if (point == null)
            {
                throw new NullReferenceException("Array point not initialized.");
            }
            init();
            for (int i = -0; i < points.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    point = points[i];
                    if (point == null)
                    {
                        throw new NullReferenceException("Array point not initialized");

                    }
                    if (_min[j] > point.getd(j))
                    {
                        _min[j] = point.getd(j);
                    }
                    if (_max[j] < point.getd(j))
                    {
                        _max[j] = point.getd(j);
                    }
                }
            }
        }

        public HS_AABB(List<HS_Coord> points)
        {
            if (points == null)
            {
                throw new NullReferenceException("List not initialized.");

            }
            if (points.Count == 0)
            {
                throw new ArgumentException("List has zero size.");
            }
            HS_Coord fpoint = points[0];
            if (fpoint == null)
            {
                throw new NullReferenceException("List point not initialized.");
            }
            init();
            foreach (HS_Coord point in points)
            {

                if (point == null)
                {
                    throw new NullReferenceException("Array point not initialized");
                }

                for (int j = 0; j < 3; j++)
                {
                    if (_min[j] > point.getd(j))
                    {
                        _min[j] = point.getd(j);
                    }
                    if (_max[j] < point.getd(j))
                    {
                        _max[j] = point.getd(j);
                    }
                }
            }

        }

        public HS_AABB(double[] min, double[] max)
        {
            init();
            if (min.Length == 3 && max.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (min[i] < max[i])
                    {
                        _min[i] = min[i];
                        _max[i] = max[i];
                    }
                    else
                    {
                        _min[i] = max[i];
                        _max[i] = min[i];
                    }
                }
            } else if (min.Length == 2 && max.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (min[i] < max[i])
                    {
                        _min[i] = min[i];
                        _max[i] = max[i];
                    }
                    else
                    {
                        _min[i] = max[i];
                        _max[i] = min[i];
                    }
                }
                _min[2] = _max[2] = 0;
            }
            else
            {
                throw new ArgumentException();
            }


        }
        private void init()
        {
            _min = new double[3];
            _max = new double[3];
            for (int i = 0; i < 3; i++)
            {
                _min[i] = double.PositiveInfinity;
                _max[i] = double.NegativeInfinity;
            }
        }

        public HS_AABB get()
        {
            return new HS_AABB(_min, _max);
        }

        public double getMinX()
        {
            return _min[0];
        }

        public double getMinY()
        {
            return _min[1];
        }

        public double getMinZ()
        {
            return _min[2];
        }

        public double getMaxX()
        {
            return _max[0];
        }
        public double getMaxY()
        {
            return _max[1];
        }
        public double getMaxZ()
        {
            return _max[2];
        }

        public double getCenterX()
        {
            return 0.5D * (_min[0] + _max[0]);
        }

        public double getCenterY()
        {
            return 0.5D * (_min[1] + _max[1]);
        }
        public double getCenterZ()
        {
            return 0.5D * (_min[2] + _max[2]);
        }

        public double getArea()
        {
            return 2.0D*(get)
        }

        public double getSize(int i)
        {
            if (isNull())
            {
                return 0;
            }
            return _max[i] - _min[i];
        }

        public double getWidth()
        {
            return getSize(0);
        }
        public double getHeight()
        {
            return getSize(1);
        }
        public double getDepth()
        {
            return getSize(2);
        }


        public void setId(int id)
        {
            this._id = id;
        }
        public void setToNull()
        {
            for (int i = 0; i < 3; i++)
            {
                _min[i] = double.PositiveInfinity;
                _max[i] = double.NegativeInfinity;
            }
        }
        public bool isNull()
        {
            return _max[0] < _min[0];
        }
        public void expandToInclude(HS_Coord p)
        {
            expandToInclude(p.xd, p.yd, p.zd);
        }
        public void expandToInclude(double x, double y, double z)
        {
            if (isNull())
            {
                _min[0] = x;
                _max[0] = x;
                _min[1] = y;
                _max[1] = y;
                _min[2] = z;
                _max[2] = z;
            }
            else
            {
                if (x < _min[0])
                {
                    _min[0] = x;
                }
                if (x > _max[0])
                {
                    _max[0] = 0;
                }
                if (y < _min[1])
                {
                    _min[1] = y;
                }
                if (y > _max[1])
                {
                    _max[1] = y;
                }
                if (x < _min[2])
                {
                    _min[2] = z;
                }
                if (x > _max[2])
                {
                    _max[2] = z;
                }
            }
        }
    }
}

