using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_KDTreeInteger<T> where T : HS_Coord
    {
        public int _dim;
        public readonly int _maximumBinSize;
        private HS_KDNodeInteger<T> root;

        public HS_KDTreeInteger()
        {
            this._dim = 3;
            this._maximumBinSize = 32;
            this.root = new HS_KDNodeInteger<T>(this);
        }

        public HS_KDTreeInteger(int binsize)
        {
            this._dim = 3;
            this._maximumBinSize = binsize;
            this.root = new HS_KDNodeInteger<T>(this);
        }

        public List<HS_AABB> getLeafBounds()
        {
            List<HS_AABB> leafs = new List<HS_AABB>();
            this.root.addLeafBounds(leafs);
            return leafs;
        }

        public List<HS_AABB> getAllBounds()
        {
            List<HS_AABB> all = new List<HS_AABB>();
            this.root.addBox(all, 0);
            return all;
        }

        public List<HS_AABB> getLeafRegions()
        {
            List<HS_AABB> leafs = new List<HS_AABB>();
            this.root.addLeafRegion(leafs);
            return leafs;
        }

        public List<HS_AABB> getAllRegions()
        {
            List<HS_AABB> all = new List<HS_AABB>();
            this.root.addRegion(all, 0);
            return all;
        }

        public int add(T coord, int val)
        {
            return this.root.add(new HS_KDEntryInteger<T>(coord, val, -1.0D));
        }

        public HS_KDEntryInteger<T>[] getRange(HS_AABB aabb)
        {
            return this.root.range(aabb);
        }

        public HS_KDEntryInteger<T>[] getRange(HS_Coord center, double radius)
        {
            double r2 = radius * radius;
            return this.root.range(center, r2);
        }

        public HS_KDEntryInteger<T>[] getRange(HS_Coord center, double lower, double upper)
        {
            double lower2 = lower * lower;
            double upper2 = upper * upper;
            return this.root.range(center, lower2, upper2);
        }

        public HS_KDEntryInteger<T>[] getNearestNeighbors(HS_Coord coord, int num)
        {
            QueryResultInteger<T> heap = new QueryResultInteger<T>(num);
            this.root.findNearest(heap, coord);
            return heap.entries;
        }

        public HS_KDEntryInteger<T> getNearestNeighbor(HS_Coord coord)
        {
            QueryResultInteger<T> heap = new QueryResultInteger<T>(1);
            this.root.findNearest(heap, coord);
            return heap.entries[0];
        }

        public HS_KDEntryInteger<T> getNearestNeighbor(double x, double y, double z)
        {
            QueryResultInteger<T> heap = new QueryResultInteger<T>(1);
            this.root.findNearest(heap, new HS_Point(x, y, z));
            return heap.entries[0];
        }


        public class QueryResultInteger<T> where T : HS_Coord
        {
            public  HS_KDEntryInteger<T>[] entries;
            private double[] distSqs;
            public int capacity;
            public int size;

            public QueryResultInteger(int capacity)
            {
                this.entries = new HS_KDEntryInteger<T>[capacity];
                this.distSqs = new double[capacity];
                this.capacity = capacity;
                this.size = 0;
            }

            public void tryToAdd(double dist, HS_KDEntryInteger<T> entry)
            {
                int i;
                for (i = this.size; i > 0 && this.distSqs[i - 1] > dist; --i)
                {
                }

                if (i < this.capacity)
                {
                    if (this.size < this.capacity)
                    {
                        ++this.size;
                    }

                    int j = i + 1;
                    //System.arraycopy(this.distSqs, i, this.distSqs, j, this.size - j);
                    Array.Copy(this.distSqs, i, this.distSqs, j, this.size - j);

                    this.distSqs[i] = dist;
                    //System.arraycopy(this.entries, i, this.entries, j, this.size - j);
                    Array.Copy(this.entries, i, this.entries, j, this.size - j);
                    entry.d2 = dist;
                    this.entries[i] = entry;
                }
            }

            public HS_KDEntryInteger<T> getEntry(int i)
            {
                return this.size == 0 ? null : this.entries[i];
            }

            public double getDistanceSquare(int i)
            {
                return this.size == 0 ? 0.0D / 0.0 : this.distSqs[i];
            }

            public int Size()
            {
                return this.size;
            }
        }



        public class HS_KDEntryInteger<T> where T : HS_Coord
        {
            public T coord;
            public int value;
            public double d2;

            public HS_KDEntryInteger(T coord, int value, double d2)
            {
                this.coord = coord;
                this.value = value;
                this.d2 = d2;
            }


        }
       class HS_KDNodeInteger<T> where T : HS_Coord
        {
            private HS_AABB _limits;
            private HS_KDNodeInteger<T> _negative, _positive;
            private HS_AABB _region;
            private HS_KDEntryInteger<T>[] _bin;
            private bool _isLeaf;
            private int _binSize;
            private int _discriminator;
            public double _sliceValue;
            private int _id;
            private HS_KDTreeInteger<T> tree;
            private int _maximumBinSize;

            public HS_KDNodeInteger(HS_KDTreeInteger<T> tree)
            {
                this.tree = tree;
                this._maximumBinSize = tree._maximumBinSize;
                _bin = new HS_KDEntryInteger<T>[this._maximumBinSize];
                _negative = _positive = null;
                _limits = null;
                _isLeaf = true;
                _binSize = 0;
                double[] min = new double[tree._dim];
                for (int i = 0; i < min.Length; i++)
                {
                    min[i] = double.NegativeInfinity;
                }
                double[] max = new double[tree._dim];
                for (int i = 0; i < min.Length; i++)
                {
                    max[i] = double.PositiveInfinity;
                }
                _region = new HS_AABB(min, max);
                _id = 0;
            }

            public void addLeafBounds(List<HS_AABB> leafs)
            {
                if (_isLeaf)
                {
                    HS_AABB box = _limits.get();
                    box.setId(_id);
                    leafs.Add(box);
                }
                else
                {
                    _positive.addLeafBounds(leafs);
                    _negative.addLeafBounds(leafs);
                }
            }

            public void addLeafRegion(List<HS_AABB> leafs)
            {
                if (_isLeaf)
                {
                    HS_AABB box = _region.get();
                    if (box.getMinX() == -1.0D / 0.0)
                    {
                        box._min[0] = this._limits._min[0];
                    }
                    if (box.getMinY() == -1.0D / 0.0)
                    {
                        box._min[1] = this._limits._min[1];
                    }
                    if (box.getMinZ() == -1.0D / 0.0)
                    {
                        box._min[2] = this._limits._min[2];
                    }
                    if (box.getMaxX() == 1.0D / 0.0)
                    {
                        box._max[0] = this._limits._max[0];
                    }
                    if (box.getMaxY() == 1.0D / 0.0)
                    {
                        box._max[1] = this._limits._max[1];
                    }
                    if (box.getMaxZ() == 1.0D / 0.0)
                    {
                        box._max[2] = this._limits._max[2];
                    }
                    box.setId(this._id);
                    leafs.Add(box);
                }
                else
                {
                    this._positive.addLeafRegion(leafs);
                    this._negative.addLeafRegion(leafs);
                }
            }
            public void addBox(List<HS_AABB> leafs, int level)
            {
                HS_AABB box = this._limits.get();
                box.setId(this._id);
                leafs.Add(box);
                if (!this._isLeaf)
                {
                    this._positive.addBox(leafs, level + 1);
                    this._negative.addBox(leafs, level + 1);
                }
            }
            public void addRegion(List<HS_AABB> leafs, int level)
            {
                HS_AABB box = this._region.get();
                if (box.getMinX() == -1.0D / 0.0)
                {
                    box._min[0] = this._limits._min[0];
                }

                if (box.getMinY() == -1.0D / 0.0)
                {
                    box._min[1] = this._limits._min[1];
                }

                if (box.getMinZ() == -1.0D / 0.0)
                {
                    box._min[2] = this._limits._min[2];
                }

                if (box.getMaxX() == 1.0D / 0.0)
                {
                    box._max[0] = this._limits._max[0];
                }

                if (box.getMaxY() == 1.0D / 0.0)
                {
                    box._max[1] = this._limits._max[1];
                }

                if (box.getMaxZ() == 1.0D / 0.0)
                {
                    box._max[2] = this._limits._max[2];
                }

                box.setId(this._id);
                leafs.Add(box);
                if (!this._isLeaf)
                {
                    this._positive.addRegion(leafs, level + 1);
                    this._negative.addRegion(leafs, level + 1);
                }
            }

            public int add(HS_KDEntryInteger<T> entry)
            {
                if (_isLeaf)
                {
                    return addInLeaf(entry);
                }
                else
                {
                    extendBounds(entry.coord);
                    if (entry.coord.getd(_discriminator) > _sliceValue)
                    {
                        return _positive.add(entry);
                    }
                    else
                    {
                        return _negative.add(entry);
                    }
                }
            }

            private int addInLeaf(HS_KDEntryInteger<T> entry)
            {
                int lookup = this.lookup(entry.coord);
                if (lookup == -1)
                {
                    this.extendBounds(entry.coord);
                    if (this._binSize + 1 > this._maximumBinSize)
                    {
                        this.addLevel();
                        this.add(entry);
                        return -1;
                    }
                    else
                    {
                        this._bin[this._binSize] = entry;
                        ++this._binSize;
                        return -1;
                    }
                }
                else
                {
                    return lookup;
                }
                if (this._isLeaf)
                {
                    return this.addInLeaf(entry);
                }
                else
                {
                    this.extendBounds(entry.coord);
                    return entry.coord.getd(this._discriminator) > this._sliceValue ? this._positive.add(entry) : this._negative.add(entry);
                }

            }

            private int lookup(HS_Coord point)
            {
                for (int i = 0; i < this._binSize; i++)
                {
                    if (HS_Epsilon.isZeroSq(HS_CoordOp3D.getSqDistance3D(point, this._bin[i].coord)))
                    {
                        return this._bin[i].value;
                    }
                }
                return -1;
            }

            public void findNearest(QueryResultInteger<T> heap, HS_Coord data)
            {
                if (_binSize == 0)
                {
                    return;
                }
                if (_isLeaf)
                {
                    for (int i = 0; i < _binSize; i++)
                    {
                        double dist = HS_CoordOp3D.getSqDistance3D(_bin[i].coord, data);
                        heap.tryToAdd(dist, _bin[i]);
                    }
                }
                else if (data.getd(_discriminator) > _sliceValue)
                {
                    _positive.findNearest(heap, data);
                    if (_negative._binSize == 0)
                    {
                        return;
                    }
                    if (heap.size < heap.capacity || this._negative._limits.getDistanceSquare(data) < heap.getDistanceSquare(heap.size - 1))
                    {
                        this._negative.findNearest(heap, data);
                    }
                }
                else
                {
                    this._negative.findNearest(heap, data);
                    if (this._positive._binSize == 0)
                    {
                        return;
                    }
                    if (heap.size < heap.capacity || this._positive._limits.getDistanceSquare(data) < heap.getDistanceSquare(heap.size - 1))
                    {
                        this._positive.findNearest(heap, data);
                    }
                }

            }

            public HS_KDEntryInteger<T>[] range(HS_AABB range)
            {
                HS_KDEntryInteger<T>[] tmp;
                HS_KDEntryInteger<T>[] tmp2;
                if (this._bin == null)
                {
                    tmp = new HS_KDEntryInteger<T>[0];
                    HS_KDEntryInteger<T>[] tmpr;
                    if (this._negative._limits.intersects(range))
                    {
                        tmpr = this._negative.range(range);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                    }

                    if (this._positive._limits.intersects(range))
                    {
                        tmpr = this._positive.range(range);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                        else if (tmpr.Length > 0)
                        {
                            tmp2 = new HS_KDEntryInteger<T>[tmp.Length + tmpr.Length];
                            //System.arraycopy(tmp, 0, tmp2, 0, tmp.Length);
                            Array.Copy(tmp, 0, tmp2, 0, tmp.Length);
                            //System.arraycopy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            Array.Copy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            tmp = tmp2;
                        }
                    }

                    return tmp;
                }
                else
                {
                    tmp = new  HS_KDEntryInteger<T>[this._binSize];
                    int n = 0;

                    for (int i = 0; i < this._binSize; ++i)
                    {
                        if (range.contains(this._bin[i].coord))
                        {
                            tmp[n++] = this._bin[i];
                        }
                    }

                    tmp2 = new HS_KDEntryInteger<T>[n];
                    //System.arraycopy(tmp, 0, tmp2, 0, n);
                    Array.Copy(tmp, 0, tmp2, 0, n);
                    return tmp2;
                }
            }

            public HS_KDEntryInteger<T>[] range(HS_Coord center, double r2)
            {
                HS_KDEntryInteger<T>[] tmp;
               HS_KDEntryInteger<T>[] tmp2;
                if (this._bin == null)
                {
                    tmp = new HS_KDEntryInteger<T>[0];
                    HS_KDEntryInteger<T>[] tmpr;
                    if (this._negative._limits.getDistanceSquare(center) <= r2)
                    {
                        tmpr = this._negative.range(center, r2);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                    }

                    if (this._positive._limits.getDistanceSquare(center) <= r2)
                    {
                        tmpr = this._positive.range(center, r2);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                        else if (tmpr.Length > 0)
                        {
                            tmp2 = new  HS_KDEntryInteger<T>[tmp.Length + tmpr.Length];
                            //System.arraycopy(tmp, 0, tmp2, 0, tmp.Length);
                            Array.Copy(tmp, 0, tmp2, 0, tmp.Length);
                            //System.arraycopy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            Array.Copy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            tmp = tmp2;
                        }
                    }

                    return tmp;
                }
                else
                {
                    tmp = new HS_KDEntryInteger<T>[this._binSize];
                    int n = 0;

                    for (int i = 0; i < this._binSize; ++i)
                    {
                        double d2 = HS_CoordOp3D.getSqDistance3D(center, this._bin[i].coord);
                        if (d2 <= r2)
                        {
                            this._bin[i].d2 = d2;
                            tmp[n++] = this._bin[i];
                        }
                    }

                    tmp2 = new HS_KDEntryInteger<T>[n];
                    //System.arraycopy(tmp, 0, tmp2, 0, n);
                    Array.Copy(tmp, 0, tmp2, 0, n);
                    return tmp2;
                }
            }

            public HS_KDEntryInteger<T>[] range(HS_Coord center, double lower2, double upper2)
            {
                HS_KDEntryInteger<T>[] tmp;
                HS_KDEntryInteger<T>[] tmp2;
                if (this._bin == null)
                {
                    tmp = new HS_KDEntryInteger<T>[0];
                   HS_KDEntryInteger<T>[] tmpr;
                    if (this._negative._limits.getDistanceSquare(center) <= upper2)
                    {
                        tmpr = this._negative.range(center, lower2, upper2);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                    }

                    if (this._positive._limits.getDistanceSquare(center) <= upper2)
                    {
                        tmpr = this._positive.range(center, lower2, upper2);
                        if (tmp.Length == 0)
                        {
                            tmp = tmpr;
                        }
                        else if (tmpr.Length > 0)
                        {
                            tmp2 = new HS_KDEntryInteger<T>[tmp.Length + tmpr.Length];
                            //System.arraycopy(tmp, 0, tmp2, 0, tmp.Length);
                            Array.Copy(tmp, 0, tmp2, 0, tmp.Length);
                            //System.arraycopy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            Array.Copy(tmpr, 0, tmp2, tmp.Length, tmpr.Length);
                            tmp = tmp2;
                        }
                    }

                    return tmp;
                }
                else
                {
                    tmp = new HS_KDEntryInteger<T>[this._binSize];
                    int n = 0;

                    for (int i = 0; i < this._binSize; ++i)
                    {
                        double d2 = HS_CoordOp3D.getSqDistance3D(center, this._bin[i].coord);
                        if (d2 <= upper2 && d2 >= lower2)
                        {
                            this._bin[i].d2 = d2;
                            tmp[n++] = this._bin[i];
                        }
                    }

                    tmp2 = new HS_KDEntryInteger<T>[n];
                    //System.arraycopy(tmp, 0, tmp2, 0, n);
                    Array.Copy(tmp, 0, tmp2, 0, n);
                    return tmp2;
                }
            }

            private void addLevel()
            {
                this._discriminator = this._limits.maxOrdinate();
                this._negative = new HS_KDNodeInteger<T>(this.tree);
                this._positive = new HS_KDNodeInteger<T>(this.tree);

                this._negative._id = 2 * this._id;
                this._positive._id = 2 * this._id + 1;
                this._sliceValue = (this._limits.getMax(this._discriminator) + this._limits.getMin(this._discriminator)) * 0.5D;
              
                int i;
                for (i = 0; i < this.tree._dim; ++i) {
                    this._negative._region._min[i] = this._region._min[i];
                    this._positive._region._max[i] = this._region._max[i];
                    if (i == this._discriminator)
                    {
                        this._negative._region._max[i] = this._sliceValue;
                        this._positive._region._min[i] = this._sliceValue;
                    }
                    else
                    {
                        this._negative._region._max[i] = this._region._max[i];
                        this._positive._region._min[i] = this._region._min[i];
                    }
                }

                for (i = 0; i < this._binSize; ++i)
                {
                    if (this._bin[i].coord.getd(this._discriminator) > this._sliceValue)
                    {
                        this._positive.addInLeaf(this._bin[i]);
                    }
                    else
                    {
                        this._negative.addInLeaf(this._bin[i]);
                    }
                }

                this._bin = null;
                this._isLeaf = false;
            }

            private void extendBounds(HS_Coord coord)
            {
                if (this._limits == null)
                {
                    this._limits = new HS_AABB(coord);

                }
                else
                {
                    this._limits.expandToInclude(coord);
                }

            }

        }











    }



}
