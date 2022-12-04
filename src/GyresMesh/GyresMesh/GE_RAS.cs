using Hsy.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GE_RAS<T> : ISet<T> where T : GE_Object
    {
        
        public FastList<T> objects;
        public Dictionary<long, int> indices;
        public GE_RAS()
        {
            objects = new FastList<T>();
            indices = new Dictionary<long, int>();
        }
        public GE_RAS(int n)
        {
            objects = new FastList<T>(n);
            indices = new Dictionary<long, int>(n);
        }
        public GE_RAS(ICollection<T> items) : this(items.Count)
        {
            foreach (T e in items)
            {
                Add(e);
            }
        }
        public int _size;
        public int Count { get { return this.objects.Count; } }

        public bool IsReadOnly => false;

        public bool Add(T item)
        {
            if (item == null)
            {
                return false;
            }
            if (!indices.ContainsKey(item.GetKey()))
            {
                objects.add(item);
                indices.Add(item.GetKey(), objects.Count-1);
                
                return true;
            }
            return false;
        }

        public void Clear()
        {
            this.objects.Clear();
            this.indices.Clear();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.objects.CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.objects.GetEnumerator();
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
            else
            {
                int id;
                bool outcome = this.indices.TryGetValue(item.GetKey(), out id);
                if (!outcome)
                {
                    return false;
                }
                else
                {
                    //Console.WriteLine("Remove:  " + id);
                    this.removeAt(id);
                    return true;
                }
            }
        }


        public T removeAt(int id)
        {
            if (id >= objects.Count)
            {
                return null;
            }
            else
            {
                T res = this.objects[id];
                this.indices.Remove(res.GetKey());

                T last = this.objects.remove(this.objects.Count- 1);

                if (id < this.objects.size)
                {
                    this.indices.Remove(last.GetKey());
                    this.indices.Add(last.GetKey(), id);
                    this.objects.set(id, last);
                }

                return res;
            }

        }
        //public T remove(FastList<T> l, int index)
        //{
        //    T previous = l[index];
        //    int totalOffset = l.Count - index - 1;

        //    T[] sourceArray = l.ToArray();
        //    T[] destArray = sourceArray;
        //    if (totalOffset > 0)
        //    {
        //        Array.Copy(sourceArray, index + 1, destArray, index, totalOffset);
        //    }
        //    l = destArray.ToList();
        //    int len = l.Count;
        //    l[len-1] = null;
        //    return previous;
        //}


        public T getWithIndex(int i)
        {
            return objects[i];
        }

        public T getWithKey(long key)
        {
            int i;
            bool outcome = indices.TryGetValue(key, out i);
            if (!outcome)
            {
                return null;
            }
            return objects[i];
        }

        public int indexOf(T obj)
        {
            int v;
            bool outcome = indices.TryGetValue(obj.GetKey(), out v);


            return outcome ? -1 : v;
        }

        public T pollRandom(Random rnd)
        {
            if (objects.Count == 0)
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
            return objects.ToList();
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
