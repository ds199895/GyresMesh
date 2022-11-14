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
        public  int _dim;
        public int _maximumBinSize;
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

        public class QueryResultInteger<T>where T:HS_Coord {
        private  HS_KDEntryInteger<T>[] entries;
        private  double[] distSqs;
        private  int capacity;
        private int size;

        protected QueryResultInteger(int capacity)
        {
            this.entries = new HS_KDEntryInteger<T>[capacity];
            this.distSqs = new double[capacity];
            this.capacity = capacity;
            this.size = 0;
        }

        protected void tryToAdd(double dist,HS_KDEntryInteger<T> entry)
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
                System.arraycopy(this.distSqs, i, this.distSqs, j, this.size - j);

                this.distSqs[i] = dist;
                System.arraycopy(this.entries, i, this.entries, j, this.size - j);
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
        public class HS_KDNodeInteger<T> where T : HS_Coord
        {
            private HS_AABB _limits;
            private HS_KDNodeInteger<T> _negative, _positive;
            private HS_AABB _region;
            private HS_KDEntryInteger<T>[] _bin;
            private bool _isLeaf;
            private int _binSize;
            private int _discriminator;
            private double _sliveValue;
            private int _id;

            public HS_KDNodeInteger(HS_KDTreeInteger<T> tree)
            {
                _bin = new HS_KDEntryInteger<T>[tree._maximumBinSize];
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

            private void addLeafRegion(List<HS_AABB> leafs)
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
            private void addBox(List<HS_AABB>leafs,int level)
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
            public void addRegion(List<HS_AABB>leafs,int level)
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

            private int add(HS_KDEntryInteger<T> entry)
            {
                if (this._isLeaf)
                {
                    return this.addInLeaf(entry);
                }
                else
                {
                    this.extendBounds(entry.coord);
                    return entry.coord.getd(this._discriminator) > this._sliveValue ? this._positive.add(entry) : this._negative.add(entry);
                }
            }

            private int addInLeaf(HS_KDEntryInteger<T> entry)
            {
                int lookup = this.lookup(entry.coord);
                if (lookup == -1)
                {
                    this.extendBounds(entry.coord);
                    if (this._binSize + 1 > HS_KDTreeInteger<T>.maximumBinSize) {
                        this.addLevel();
                        this.add(entry);
                        return -1;
                    } else
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
                    return entry.coord.getd(this._discriminator) > this._sliveValue ? this._positive.add(entry) : this._negative.add(entry);
                }

            }

            private int lookup(HS_Coord point)
            {
                for(int i = 0; i < this._binSize; i++)
                {
                    if (HS_Epsilon.isZeroSq(HS_CoordOp3D.getSqDistance3D(point, this._bin[i].coord)))
                    {
                        return this._bin[i].value;
                    }
                }
                return -1;
            }

            private void findNearest()

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
