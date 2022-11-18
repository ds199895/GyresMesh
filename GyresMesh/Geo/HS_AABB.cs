using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{

	public class HS_AABB
	{
		protected internal double[] _min;
		protected internal double[] _max;
		int _id;
		/**
		 *
		 *
		 * @param p
		 */
		public HS_AABB(HS_Coord p)
		{
			init();
			setToNull();
			expandToInclude(p);
		}

		/**
		 *
		 */
		public HS_AABB()
		{
			init();
		}

		/**
		 *
		 *
		 * @param points
		 */
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
			for (int i = 0; i < points.Length; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					point = points[i];
					if (point == null)
					{
						throw new NullReferenceException(
								"Array point not initialized.");
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

		/**
		 *
		 *
		 * @param points
		 */
		public HS_AABB(List<HS_Coord>points)
		{
			if (points == null)
			{
				throw new ArgumentException("Collection not initialized.");
			}
			if (points.Count == 0)
			{
				throw new ArgumentException("Collection has zero size.");
			}
			HS_Coord fpoint = points.GetEnumerator().Current;
			if (fpoint == null)
			{
				throw new NullReferenceException("Collection point not initialized.");
			}
			init();
			foreach (HS_Coord point in points)
			{
				if (point == null)
				{
					throw new NullReferenceException(
							"Collection point not initialized.");
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

		/**
		 *
		 *
		 * @param min
		 * @param max
		 */
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
			}
			else if (min.Length == 2 && max.Length == 2)
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

		/**
		 *
		 *
		 * @param min
		 * @param max
		 */
		public HS_AABB(float[] min, float[] max)
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
			}
			else if (min.Length == 2 && max.Length == 2)
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

		/**
		 *
		 *
		 * @param min
		 * @param max
		 */
		public HS_AABB(int[] min, int[] max)
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
			}
			else if (min.Length == 2 && max.Length == 2)
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

		/**
		 *
		 *
		 * @param min
		 * @param max
		 */
		public HS_AABB(HS_Coord min, HS_Coord max)
		{
			init();
			for (int i = 0; i < 3; i++)
			{
				if (min.getd(i) < max.getd(i))
				{
					_min[i] = min.getd(i);
					_max[i] = max.getd(i);
				}
				else
				{
					_min[i] = max.getd(i);
					_max[i] = min.getd(i);
				}
			}
		}

		/**
		 *
		 *
		 * @param minx
		 * @param miny
		 * @param maxx
		 * @param maxy
		 */
		public HS_AABB(double minx, double miny, double maxx,
				 double maxy)
		{
			init();
			expandToInclude(minx, miny, 0);
			expandToInclude(maxx, maxy, 0);
		}

		/**
		 *
		 *
		 * @param minx
		 * @param miny
		 * @param minz
		 * @param maxx
		 * @param maxy
		 * @param maxz
		 */
		public HS_AABB(double minx, double miny, double minz,
				 double maxx, double maxy, double maxz)
		{
			init();
			expandToInclude(minx, miny, minz);
			expandToInclude(maxx, maxy, maxz);
		}

		/**
		 *
		 *
		 * @param values
		 */
		public HS_AABB(double[] values)
		{
			init();
			if (values.Length == 0)
			{
			}
			else if (values.Length == 6)
			{
				for (int i = 0; i < 3; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 3];
				}
			}
			else if (values.Length == 4)
			{
				for (int i = 0; i < 2; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 2];
				}
				_min[2] = _max[2] = 0;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		/**
		 *
		 *
		 * @param values
		 */
		public HS_AABB(int[] values)
		{
			init();
			if (values.Length == 0)
			{
			}
			else if (values.Length == 6)
			{
				for (int i = 0; i < 3; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 3];
				}
			}
			else if (values.Length == 4)
			{
				for (int i = 0; i < 2; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 2];
				}
				_min[2] = _max[2] = 0;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		/**
		 *
		 *
		 * @param values
		 */
		public HS_AABB(float[] values)
		{
			init();
			if (values.Length == 0)
			{
			}
			else if (values.Length == 6)
			{
				for (int i = 0; i < 3; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 3];
				}
			}
			else if (values.Length == 4)
			{
				for (int i = 0; i < 2; i++)
				{
					_min[i] = values[i];
					_max[i] = values[i + 2];
				}
				_min[2] = _max[2] = 0;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		/**
		 *
		 *
		 * @param i
		 * @return
		 */
		public double getSize(int i)
		{
			if (isNull())
			{
				return 0;
			}
			return _max[i] - _min[i];
		}

		/**
		 *
		 *
		 * @return
		 */
		public int minOrdinate()
		{
			if (isNull())
			{
				return 0;
			}
			double res = double.PositiveInfinity;
			int ord = 0;
			for (int i = 0; i < 3; i++)
			{
				double w = getSize(i);
				if (res > w)
				{
					res = w;
					ord = i;
				}
			}
			return ord;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int maxOrdinate()
		{
			if (isNull())
			{
				return 0;
			}
			double res = double.NegativeInfinity;
			int ord = 0;
			for (int i = 0; i < 3; i++)
			{
				double w = getSize(i);
				if (res < w)
				{
					res = w;
					ord = i;
				}
			}
			return ord;
		}

		/**
		 *
		 *
		 * @param p
		 */
		public void expandToInclude(HS_Coord p)
		{
			expandToInclude(p.xd, p.yd, p.zd);
		}

		/**
		 *
		 *
		 * @param p
		 */
		public void add(HS_Coord p)
		{
			expandToInclude(p.xd, p.yd, p.zd);
		}

		/**
		 *
		 *
		 * @param distance
		 */
		public void expandBy(double distance)
		{
			if (isNull())
			{
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				_min[i] -= distance;
				_max[i] += distance;
				if (_min[i] > _max[i])
				{
					setToNull();
					return;
				}
			}
		}

		/**
		 *
		 *
		 * @param delta
		 */
		public void expandBy(double[] delta)
		{
			if (isNull())
			{
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				_min[i] -= delta[i];
				_max[i] += delta[i];
				if (_min[i] > _max[i])
				{
					setToNull();
					return;
				}
			}
		}

		/**
		 *
		 *
		 * @param dx
		 * @param dy
		 * @param dz
		 */
		public void expandBy(double dx, double dy, double dz)
		{
			if (isNull())
			{
				return;
			}
			_min[0] -= dx;
			_max[0] += dx;
			if (_min[0] > _max[0])
			{
				setToNull();
				return;
			}
			_min[1] -= dy;
			_max[1] += dy;
			if (_min[1] > _max[1])
			{
				setToNull();
				return;
			}
			_min[2] -= dz;
			_max[2] += dz;
			if (_min[2] > _max[2])
			{
				setToNull();
				return;
			}
		}

		/**
		 *
		 *
		 * @param p
		 */
		public void expandToInclude(double[] p)
		{
			if (isNull())
			{
				for (int i = 0; i < 3; i++)
				{
					_min[i] = p[i];
					_max[i] = p[i];
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					if (p[i] < _min[i])
					{
						_min[i] = p[i];
					}
					if (p[i] > _max[i])
					{
						_max[i] = p[i];
					}
				}
			}
		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 */
		public void expandToInclude(double x, double y,
				 double z)
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
					_max[0] = x;
				}
				if (y < _min[1])
				{
					_min[1] = y;
				}
				if (y > _max[1])
				{
					_max[1] = y;
				}
				if (z < _min[2])
				{
					_min[2] = z;
				}
				if (z > _max[2])
				{
					_max[2] = z;
				}
			}
		}

		/**
		 *
		 *
		 * @param other
		 */
		public void expandToInclude(HS_AABB other)
		{
			expandToInclude(other._min);
			expandToInclude(other._max);
		}

		/**
		 *
		 *
		 * @param other
		 */
		public void add(HS_AABB other)
		{
			expandToInclude(other);
		}

		/**
		 *
		 *
		 * @param d
		 */
		public void translate(double[] d)
		{
			if (isNull())
			{
				return;
			}
			for (int i = 0; i < 3; i++)
			{
				_min[i] += d[i];
				_max[i] += d[i];
			}
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public bool intersects(HS_AABB other)
		{
			if (isNull() || other.isNull())
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (other._min[i] > _max[i])
				{
					return false;
				}
				if (other._max[i] < _min[i])
				{
					return false;
				}
			}
			return true;
		}

		/**
		 *
		 *
		 * @param p
		 * @return
		 */
		public bool intersects(HS_Coord p)
		{
			return intersects(p.xd, p.yd, p.zd);
		}

		/**
		 *
		 *
		 * @param x
		 * @return
		 */
		public bool intersects(double[] x)
		{
			if (isNull())
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (x[i] > _max[i])
				{
					return false;
				}
				if (x[i] < _min[i])
				{
					return false;
				}
			}
			return true;
		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 * @return
		 */
		public bool intersects(double x, double y, double z)
		{
			if (isNull())
			{
				return false;
			}
			if (x > _max[0])
			{
				return false;
			}
			if (x < _min[0])
			{
				return false;
			}
			if (y > _max[1])
			{
				return false;
			}
			if (y < _min[1])
			{
				return false;
			}
			if (z > _max[2])
			{
				return false;
			}
			if (z < _min[2])
			{
				return false;
			}
			return true;
		}

		//public bool intersects(HS_Sphere sphere)
		//{
		//	HS_Coord c = sphere.getCenter();
		//	double r = sphere.getRadius();
		//	double s, d = 0;
		//	// find the square of the distance
		//	// from the sphere to the box
		//	if (c.xd < _min[0])
		//	{
		//		s = c.xd - _min[0];
		//		d = s * s;
		//	}
		//	else if (c.xd > _max[0])
		//	{
		//		s = c.xd - _max[0];
		//		d += s * s;
		//	}
		//	if (c.yd < _min[1])
		//	{
		//		s = c.yd - _min[1];
		//		d += s * s;
		//	}
		//	else if (c.yd > _max[1])
		//	{
		//		s = c.yd - _max[1];
		//		d += s * s;
		//	}
		//	if (c.zd < _min[2])
		//	{
		//		s = c.zd - _min[2];
		//		d += s * s;
		//	}
		//	else if (c.zd > _max[2])
		//	{
		//		s = c.zd - _max[2];
		//		d += s * s;
		//	}
		//	return d <= r * r;
		//}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public bool contains(HS_AABB other)
		{
			return covers(other);
		}

		/**
		 *
		 *
		 * @param p
		 * @return
		 */
		public bool contains(HS_Coord p)
		{
			return covers(p);
		}

		/**
		 *
		 *
		 * @param x
		 * @return
		 */
		public bool contains(double[] x)
		{
			return covers(x);
		}

		/**
		 *
		 *
		 * @param x
		 * @return
		 */
		public bool covers(double[] x)
		{
			if (isNull())
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (x[i] > _max[i])
				{
					return false;
				}
				if (x[i] < _min[i])
				{
					return false;
				}
			}
			return true;
		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 * @return
		 */
		public bool covers(double x, double y, double z)
		{
			if (isNull())
			{
				return false;
			}
			if (x > _max[0])
			{
				return false;
			}
			if (x < _min[0])
			{
				return false;
			}
			if (y > _max[1])
			{
				return false;
			}
			if (y < _min[1])
			{
				return false;
			}
			if (z > _max[2])
			{
				return false;
			}
			if (z < _min[2])
			{
				return false;
			}
			return true;
		}

		/**
		 *
		 *
		 * @param p
		 * @return
		 */
		public bool covers(HS_Coord p)
		{
			return covers(p.xd, p.yd, p.zd);
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public bool covers(HS_AABB other)
		{
			if (isNull() || other.isNull())
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (other._max[i] > _max[i])
				{
					return false;
				}
				if (other._min[i] < _min[i])
				{
					return false;
				}
			}
			return true;
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public double getDistance(HS_AABB other)
		{
			if (intersects(other))
			{
				return 0;
			}
			double dx = 0;
			double sqr = 0;
			for (int i = 0; i < 3; i++)
			{
				if (_max[i] < other._min[i])
				{
					dx = other._min[i] - _max[i];
				}
				else if (_min[i] > other._max[i])
				{
					dx = _min[i] - other._max[i];
				}
				sqr += dx * dx;
			}
			return Math.Sqrt(sqr);
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public double getDistanceSquare(HS_AABB other)
		{
			if (intersects(other))
			{
				return 0;
			}
			double dx = 0;
			double sqr = 0;
			for (int i = 0; i < 3; i++)
			{
				if (_max[i] < other._min[i])
				{
					dx = other._min[i] - _max[i];
				}
				else if (_min[i] > other._max[i])
				{
					dx = _min[i] - other._max[i];
				}
				sqr += dx * dx;
			}
			return sqr;
		}

		/**
		 *
		 *
		 * @param tuple
		 * @return
		 */
		public double getDistance(HS_Coord tuple)
		{
			double dx = 0;
			double sqr = 0;
			for (int i = 0; i < 3; i++)
			{
				if (_max[i] < tuple.getd(i))
				{
					sqr += (dx = tuple.getd(i) - _max[i]) * dx;
				}
				else if (_min[i] > tuple.getd(i))
				{
					sqr += (dx = _min[i] - tuple.getd(i)) * dx;
				}
			}
			return Math.Sqrt(sqr);
		}

		/**
		 *
		 *
		 * @param tuple
		 * @return
		 */
		public double getDistanceSquare(HS_Coord tuple)
		{
			double dx = 0;
			double sqr = 0;
			for (int i = 0; i < 3; i++)
			{
				if (_max[i] < tuple.getd(i))
				{
					sqr += (dx = tuple.getd(i) - _max[i]) * dx;
				}
				else if (_min[i] > tuple.getd(i))
				{
					sqr += (dx = _min[i] - tuple.getd(i)) * dx;
				}
			}
			return sqr;
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public bool equals(HS_AABB other)
		{
			if (isNull())
			{
				return other.isNull();
			}
			for (int i = 0; i < 3; i++)
			{
				if (other._max[i] != _max[i])
				{
					return false;
				}
				if (other._min[i] != _min[i])
				{
					return false;
				}
			}
			return true;
		}

		/*
		 * (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */

	public String toString()
		{
			String str = "HS_AABB [";
			int i = 0;
			for (i = 0; i < 3 - 1; i++)
			{
				str+= _min[i] + ":" + _max[i] + ", ";
			}
			str += _min[i] + ":" + _max[i] + "]";
			return str;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int numberOfPoints()
		{
			if (isNull())
			{
				return 0;
			}
			return 8;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int numberOfSegments()
		{
			if (isNull())
			{
				return 0;
			}
			return 12;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int numberOfTriangles()
		{
			if (isNull())
			{
				return 0;
			}
			return 12;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int numberOfFaces()
		{
			if (isNull())
			{
				return 0;
			}
			return 6;
		}

		/**
		 *
		 *
		 * @return
		 */
		private List<double[]> getCoords()
		{
			if (isNull())
			{
				return null;
			}
			int n = numberOfPoints();
			List<double[]> points = new List<double[]>(n);
			double[] values;
			for (int i = 0; i < n; i++)
			{
				values = new double[3];
				int disc = 1;
				for (int j = 0; j < 3; j++)
				{
					if (i / disc % 2 == 0)
					{
						values[j] = _min[j];
					}
					else
					{
						values[j] = _max[j];
					}
					disc *= 2;
				}
				points.Add(values);
			}
			return points;
		}

		/**
		 *
		 *
		 * @return
		 */
		public HS_Point[] getCorners()
		{
			if (isNull())
			{
				return null;
			}
			int n = numberOfPoints();
			HS_Point[] points = new HS_Point[n];
			double[] values;
			for (int i = 0; i < n; i++)
			{
				values = new double[3];
				int disc = 1;
				for (int j = 0; j < 3; j++)
				{
					if (i / disc % 2 == 0)
					{
						values[j] = _min[j];
					}
					else
					{
						values[j] = _max[j];
					}
					disc *= 2;
				}
				points[i] = new HS_Point(values);
			}
			return points;
		}

		/**
		 *
		 *
		 * @return
		 */
		public List<int[]> getSegments()
		{
			List<double[]> points = getCoords();
			List<int[]> segments = new List<int[]>(numberOfSegments());
			for (int i = 0; i < points.Count; i++)
			{
				for (int j = i + 1; j < points.Count; j++)
				{
					int comp = 0;
					for (int k = 0; k < 3; k++)
					{
						if (points[i][k] != points[j][k])
						{
							comp++;
						}
						if (comp > 1)
						{
							break;
						}
					}
					if (comp == 1)
					{
						int[] seg = { i, j };
						segments.Add(seg);
					}
				}
			}
			return segments;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int getId()
		{
			return _id;
		}

		/**
		 *
		 *
		 * @param id
		 */
		public void setId(int id)
		{
			_id = id;
		}

		/**
		 *
		 *
		 * @return
		 */
		public bool isDegenerate()
		{
			return getTrueDim() < 3 && getTrueDim() > -1;
		}

		/**
		 *
		 *
		 * @param src
		 */
		public void set(HS_AABB src)
		{
			Array.Copy(src._min, 0, _min, 0, 3);

			Array.Copy(src._max, 0, _max, 0, 3);
			//System.arraycopy(src._min, 0, _min, 0, 3);
			//System.arraycopy(src._max, 0, _max, 0, 3);
		}

		/**
		 *
		 */
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

		/**
		 *
		 *
		 * @return
		 */
		public HS_AABB get()
		{
			return new HS_AABB(_min, _max);
		}

		/**
		 *
		 *
		 * @param aabb
		 * @return
		 */
		public HS_AABB getUnion(HS_AABB aabb)
		{
			double[] newmin = new double[3];
			double[] newmax = new double[3];
			for (int i = 0; i < 3; i++)
			{
				newmin[i] = Math.Min(_min[i], aabb._min[i]);
				newmax[i] = Math.Max(_max[i], aabb._max[i]);
			}
			return new HS_AABB(newmin, newmax);
		}

		/**
		 *
		 *
		 * @param other
		 * @return
		 */
		public HS_AABB getIntersection(HS_AABB other)
		{
			if (isNull() || other.isNull() || !intersects(other))
			{
				return null;
			}
			double[] newmin = new double[3];
			double[] newmax = new double[3];
			for (int i = 0; i < 3; i++)
			{
				newmin[i] = Math.Max(_min[i], other._min[i]);
				newmax[i] = Math.Min(_max[i], other._max[i]);
			}
			return new HS_AABB(newmin, newmax);
		}

		/**
		 *
		 *
		 * @param p1
		 * @param p2
		 * @param q
		 * @return
		 */
		public static bool intersects(HS_Coord p1, HS_Coord p2,
				 HS_Coord q)
		{
			if (q.xd >= (p1.xd < p2.xd ? p1.xd : p2.xd)
					&& q.xd <= (p1.xd > p2.xd ? p1.xd : p2.xd)
					&& q.yd >= (p1.yd < p2.yd ? p1.yd : p2.yd)
					&& q.yd <= (p1.yd > p2.yd ? p1.yd : p2.yd)
					&& q.zd >= (p1.zd < p2.zd ? p1.zd : p2.zd)
					&& q.zd <= (p1.zd > p2.zd ? p1.yd : p2.zd))
			{
				return true;
			}
			return false;
		}

		/**
		 *
		 *
		 * @param p1
		 * @param p2
		 * @param q1
		 * @param q2
		 * @return
		 */
		public static bool intersects(HS_Coord p1, HS_Coord p2,
				 HS_Coord q1, HS_Coord q2)
		{
			double minq = Math.Min(q1.xd, q2.xd);
			double maxq = Math.Max(q1.xd, q2.xd);
			double minp = Math.Min(p1.xd, p2.xd);
			double maxp = Math.Max(p1.xd, p2.xd);
			if (minp > maxq)
			{
				return false;
			}
			if (maxp < minq)
			{
				return false;
			}
			minq = Math.Min(q1.yd, q2.yd);
			maxq = Math.Max(q1.yd, q2.yd);
			minp = Math.Min(p1.yd, p2.yd);
			maxp = Math.Max(p1.yd, p2.yd);
			if (minp > maxq)
			{
				return false;
			}
			if (maxp < minq)
			{
				return false;
			}
			minq = Math.Min(q1.zd, q2.zd);
			maxq = Math.Max(q1.zd, q2.zd);
			minp = Math.Min(p1.zd, p2.zd);
			maxp = Math.Max(p1.zd, p2.zd);
			if (minp > maxq)
			{
				return false;
			}
			if (maxp < minq)
			{
				return false;
			}
			return true;
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getWidth()
		{
			return getSize(0);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getHeight()
		{
			return getSize(1);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getDepth()
		{
			return getSize(2);
		}

		/**
		 *
		 *
		 * @param i
		 * @return
		 */
		public double getMin(int i)
		{
			return _min[i];
		}

		/**
		 *
		 *
		 * @param i
		 * @return
		 */
		public double getMax(int i)
		{
			return _max[i];
		}

		public double getCenter(int i)
		{
			return 0.5 * (_min[i] + _max[i]);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMinX()
		{
			return _min[0];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMaxX()
		{
			return _max[0];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getCenterX()
		{
			return 0.5 * (_min[0] + _max[0]);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMinY()
		{
			return _min[1];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMaxY()
		{
			return _max[1];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getCenterY()
		{
			return 0.5 * (_min[1] + _max[1]);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMinZ()
		{
			return _min[2];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getMaxZ()
		{
			return _max[2];
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getCenterZ()
		{
			return 0.5 * (_min[2] + _max[2]);
		}

		/**
		 *
		 *
		 * @return
		 */
		public double getVolume()
		{
			return getWidth() * getHeight() * getDepth();
		}

		public double getArea()
		{
			return 2.0 * (getWidth() * getHeight() + getWidth() * getDepth()
					+ getDepth() * getHeight());
		}

		/**
		 *
		 *
		 * @return
		 */
		public double minExtent()
		{
			if (isNull())
			{
				return 0.0;
			}
			double w = getWidth();
			double h = getHeight();
			double d = getDepth();
			if (w < h)
			{
				return w < d ? w : d;
			}
			return h < d ? h : d;
		}

		/**
		 *
		 *
		 * @return
		 */
		public double maxExtent()
		{
			if (isNull())
			{
				return 0.0;
			}
			double w = getWidth();
			double h = getHeight();
			double d = getDepth();
			if (w > h)
			{
				return w > d ? w : d;
			}
			return h > d ? h : d;
		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 */
		public void translate(double x, double y, double z)
		{
			if (isNull())
			{
				return;
			}
			_min[0] += x;
			_max[0] += x;
			_min[1] += y;
			_max[1] += y;
			_min[2] += z;
			_max[2] += z;
		}

		/**
		 *
		 *
		 * @return
		 */
		public List<int[]> getTriangles()
		{
			List<int[]> tris = new List<int[]>();
			int[] tri01 = { 4, 5, 6 };
			int[] tri02 = { 5, 7, 6 };
			tris.Add(tri01);
			tris.Add(tri02);
			int[] tri11 = { 0, 2, 1 };
			int[] tri12 = { 2, 3, 1 };
			tris.Add(tri11);
			tris.Add(tri12);
			int[] tri21 = { 0, 1, 4 };
			int[] tri22 = { 1, 5, 4 };
			tris.Add(tri21);
			tris.Add(tri22);
			int[] tri31 = { 3, 2, 7 };
			int[] tri32 = { 2, 6, 7 };
			tris.Add(tri31);
			tris.Add(tri32);
			int[] tri41 = { 0, 4, 2 };
			int[] tri42 = { 4, 6, 2 };
			tris.Add(tri41);
			tris.Add(tri42);
			int[] tri51 = { 1, 3, 5 };
			int[] tri52 = { 3, 7, 5 };
			tris.Add(tri51);
			tris.Add(tri52);
			return tris;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int[][] getFaces()
		{
			int[][] faces = new int[6][];
			faces[0] = new int[] { 4, 5, 7, 6 };
			faces[1] = new int[] { 0, 2, 3, 1 };
			faces[2] = new int[] { 0, 1, 5, 4 };
			faces[3] = new int[] { 3, 2, 6, 7 };
			faces[4] = new int[] { 0, 4, 6, 2 };
			faces[5] = new int[] { 1, 3, 7, 5 };
			return faces;
		}

		/**
		 *
		 *
		 * @return
		 */
		public HS_Point getMin()
		{
			return new HS_Point(_min);
		}

		/**
		 *
		 *
		 * @return
		 */
		public HS_Point getMax()
		{
			return new HS_Point(_max);
		}

		/**
		 *
		 *
		 * @return
		 */
		public HS_Point getCenter()
		{
			double[] center = new double[3];
			for (int i = 0; i < 3; i++)
			{
				center[i] = 0.5 * (_min[i] + _max[i]);
			}
			return new HS_Point(center);
		}

		/**
		 *
		 *
		 * @return
		 */
		public int getDim()
		{
			return 3;
		}

		/**
		 *
		 *
		 * @return
		 */
		public int getTrueDim()
		{
			if (!isValid())
			{
				return -1;
			}
			int dim = 0;
			for (int i = 0; i < 3; i++)
			{
				if (_max[i] - _min[i] >= HS_Epsilon.EPSILON)
				{
					dim++;
				}
			}
			return dim;
		}

		/**
		 *
		 *
		 * @param factor
		 */
		public void pad(double factor)
		{
			HS_Point c = getCenter();
			for (int i = 0; i < 3; i++)
			{
				_min[i] = c.getd(i) + (factor + 1.0) * (_min[i] - c.getd(i));
				_max[i] = c.getd(i) + (factor + 1.0) * (_max[i] - c.getd(i));
			}
		}

		/*
		 * (non-Javadoc)
		 * @see java.lang.Object#hashCode()
		 */

	public int hashCode()
		{
			int result = 17;
			for (int i = 0; i < 3; i++)
			{
				result = 37 * result + hashCode(_min[i]);
				result = 37 * result + hashCode(_max[i]);
			}
			return result;
		}

		/**
		 *
		 *
		 * @param v
		 * @return
		 */
		private unsafe int hashCode(double v)
		{
			long tolong = *(long*)&v;
			long tmp = tolong;
			return (int)(tmp ^ tmp >> 32);
		}

		/**
		 *
		 */
		public void setToNull()
		{
			for (int i = 0; i < 3; i++)
			{
				_min[i] = double.PositiveInfinity;
				
				_max[i] = double.NegativeInfinity;
			}
		}

		/**
		 *
		 *
		 * @return
		 */
		public bool isNull()
		{
			return _max[0] < _min[0];
		}

		/**
		 *
		 *
		 * @return
		 */
		public bool isValid()
		{
			return !isNull();
		}
	}
}

