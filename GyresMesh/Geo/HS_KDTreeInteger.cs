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
            this.root = new HS_KDNodeInteger<T>(_maximumBinSize);
        }

        public HS_KDTreeInteger(int binsize)
        {
            this._dim = 3;
            this._maximumBinSize = binsize;
            this.root = new HS_KDNodeInteger<T>(_maximumBinSize);
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
                    if()
                }
            }


        }











    }



}
