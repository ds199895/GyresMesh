using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_HalfedgeEnumerator : IEnumerator<GE_Halfedge>
    {

        IEnumerator<GE_Halfedge> _etr;
        public GE_HalfedgeEnumerator(List<GE_Halfedge> hes)
        {
            _etr = hes.GetEnumerator();
        }
        public GE_Halfedge Current => _etr.Current;

        object IEnumerator.Current => _etr.Current;

        public void Dispose()
        {
           _etr.Dispose();
        }

        public bool MoveNext()
        {
           return _etr.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
