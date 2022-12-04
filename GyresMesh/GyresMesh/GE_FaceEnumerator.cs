using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Generic.List<Hsy.GyresMesh.GE_Face>;

namespace Hsy.GyresMesh
{
    public class GE_FaceEnumerator : IEnumerator<GE_Face>
    {
        IEnumerator<GE_Face> _etr;
        public GE_FaceEnumerator(List<GE_Face> faces)
        {
            _etr=faces.GetEnumerator();
        }
        public GE_Face Current => _etr.Current;

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
