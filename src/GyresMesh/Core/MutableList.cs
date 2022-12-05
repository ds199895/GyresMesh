using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Core
{
    public abstract class MutableList<T> : List<T>
    {
        public abstract new int Count{ get; }

        public abstract new bool Add(T item);
}
}
