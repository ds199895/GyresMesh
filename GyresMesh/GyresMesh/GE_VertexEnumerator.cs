using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_VertexEnumerator : IEnumerator<GE_Vertex>
    {
        IEnumerator<GE_Vertex> _etr;
        public GE_VertexEnumerator(List<GE_Vertex> vs)
        {
            _etr = vs.GetEnumerator();
        }

        public GE_Vertex Current => _etr.Current;

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
