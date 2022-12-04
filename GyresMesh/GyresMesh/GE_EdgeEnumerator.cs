using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_EdgeEnumerator : IEnumerator<GE_Halfedge>
    {
        IEnumerator<GE_Halfedge> _Etr;
        public GE_EdgeEnumerator(List<GE_Halfedge>edges)
        {
            this._Etr = edges.GetEnumerator();

        }
        public GE_Halfedge Current => _Etr.Current;

        object IEnumerator.Current => _Etr.Current;

        public void Dispose()
        {
            _Etr.Dispose();
        }

        public bool MoveNext()
        {
            return _Etr.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
