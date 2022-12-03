using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_RAS<T> : ISet<T> where T:GE_Object
    {
        List<T> objects;
        Dictionary<long, int> indices;
        public GE_RAS()
        {
            objects = new List<T>();
            indices = new Dictionary<long, int>();
        }
        public GE_RAS(int n)
        {
            objects = new List<T>(n);
            indices = new Dictionary<long, int>(n);
        }
        public GE_RAS(ICollection<T> items) : this(items.Count)
        {
            foreach(T e in items)
            {
                Add(e);
            }
        }
        public int _size;
        public int Count { get { return _size; } set { _size = value; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public bool Add(T item)
        {
            if (item == null)
            {
                return false;
            }
            if (!indices.ContainsKey(item.GetKey()))
            {
                indices.Add(item.GetKey(), objects.Count);
                objects.Add(item);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                return false;
            }
            // @SuppressWarnings(value = "element-type-mismatch
            int id;
            this.indices.TryGetValue(item.GetKey(), out id);
            if (id == null)
            {
                return false;
            }
            removeAt(id);
            return true;
        }


        public T removeAt(int id)
        {
            if (id >= objects.Count)
            {
                return null;
            }
            T res = objects[id];
            indices.Remove(res.GetKey());
            T last = objects[objects.Count - 1];
            objects.RemoveAt(objects.Count - 1);
            //// skip filling the hole if last is removed
            if (id < objects.Count)
            {
                indices.Add(last.GetKey(), id);
                objects.Insert(id, last);
            }
            return res;
        }
        public T getWithIndex(int i)
        {
            return objects[i];
        }

        public T getWithKey(long key)
        {
            int i;
            indices.TryGetValue(key, out i) ;
            if (i == -1)
            {
                return null;
            }
            return objects[i];
        }

        public int indexOf(T obj)
        {
            int v;
            indices.TryGetValue(obj.GetKey(),out v);


            return v == null ? -1 : v;
        }

        public T pollRandom(Random rnd)
        {
            if (objects.Count==0)
            {
                return null;
            }
            int id = rnd.Next(objects.Count);
            return removeAt(id);
        }

    public int size()
        {
            return objects.Count;
        }

        public bool contains(T obj)
        {
            if (obj == null)
            {
                return false;
            }
            return indices.ContainsKey(obj.GetKey());
        }

        public bool containsKey(long key)
        {
            return indices.ContainsKey(key);
        }

    public IEnumerator<T> Enumerator()
        {
            return objects.GetEnumerator();
        }

        public List<T> getObjects()
        {
            return objects;
        }

        //public T remove(int index)
        //{
        //    T previous = this.objects[index];
        //    int totalOffset = this.Count - index - 1;
        //    if (totalOffset > 0)
        //    {
        //        Array.Copy(this.objects, index + 1, this.objects, index, totalOffset);
        //    }

        //    this.items[--this.Count] = null;
        //    return previous;
        //}

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
