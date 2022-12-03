using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Core
{
    public class FastList<T>: List<T>,ICollection<T>
    {
        private static readonly long serialVersionUID = 1L;
        private static readonly T[] DEFAULT_SIZED_EMPTY_ARRAY = new T[0];
        private static readonly T[] ZERO_SIZED_ARRAY = new T[0];
        private static readonly int MAXIMUM_ARRAY_SIZE = 2147483639;
        protected internal int size;
        protected internal T[] items;

        public int Count => this.size;

        public bool IsReadOnly => false;

        public FastList()
        {
            this.items = DEFAULT_SIZED_EMPTY_ARRAY;
        }
        public T this[int index] { get { return this.get(index); } }

        public FastList(int initialCapacity)
        {
            this.items = DEFAULT_SIZED_EMPTY_ARRAY;
            this.items = initialCapacity == 0 ? ZERO_SIZED_ARRAY : new T[initialCapacity];
        }

        protected FastList(T[] array) : this(array.Length, array)
        {

        }

        protected FastList(int size, T[] array)
        {
            this.items = DEFAULT_SIZED_EMPTY_ARRAY;
            this.size = size;
            this.items = array;
        }

        public FastList(ICollection<T> source)
        {
            this.items = DEFAULT_SIZED_EMPTY_ARRAY;
            this.items = source.ToArray();
            this.size = this.items.Length;
        }

        public static FastList<T> newList(FastList<T>fl)
        {
            return new FastList<T>();
        }

        public static FastList<T> wrapCopy(params T[] array)
        {
            T[] newArray = new T[array.Length];
            Array.Copy(array, 0, newArray, 0, array.Length);
            return new FastList<T>(newArray);
        }

        public static FastList<T> newList(int initialCapacity)
        {
            return new FastList<T>(initialCapacity);
        }

        public static FastList<T> newList(IEnumerable<T> source)
        {
            return newListWith(source.ToArray());
        }

        public static FastList<T> newListWith(params T[] elements)
        {
            return new FastList<T>(elements);
        }

        //public static FastList<T> newWithNValues(int size, Function0 factory)
        //{
        //    FastList<T> newFastList = newList(size);

        //    for (int i = 0; i < size; ++i)
        //    {
        //        newFastList.add(factory.value());
        //    }

        //    return newFastList;
        //}

        public FastList<T> clone()
        {
            FastList<T> result = new FastList<T>();
            if (this.items.Length > 0)
            {
                result.items = (T[])this.items.Clone();
            }

            return result;
        }

        public void Clear()
        {
            Array.Clear(this.items, 0, this.size);
            this.size = 0;
        }

        //public void forEach(int from, int to, Procedure<T> procedure)
        //{
        //    ListIterate.rangeCheck(from, to, this.size);
        //    InternalArrayIterate.forEachWithoutChecks(this.items, from, to, procedure);
        //}

        //public void forEachWithIndex(int from, int to, ObjectIntProcedure<? super T> objectIntProcedure)
        //{
        //    ListIterate.rangeCheck(from, to, this.size);
        //    InternalArrayIterate.forEachWithIndexWithoutChecks(this.items, from, to, objectIntProcedure);
        //}

        //public void batchForEach(Procedure<? super T> procedure, int sectionIndex, int sectionCount)
        //{
        //    InternalArrayIterate.batchForEach(procedure, this.items, this.size, sectionIndex, sectionCount);
        //}

        public int getBatchCount(int batchSize)
        {
            return Math.Max(1, this.size / batchSize);
        }

        public T[] toArray(T[] array, int sourceFromIndex, int sourceToIndex, int destinationIndex)
        {
            Array.Copy(this.items, sourceFromIndex, array, destinationIndex, sourceToIndex - sourceFromIndex + 1);
            return array;
        }

        public T[] toArray(int sourceFromIndex, int sourceToIndex)
        {
            return this.toArray(new T[sourceToIndex - sourceFromIndex + 1], sourceFromIndex, sourceToIndex, 0);
        }

        public FastList<T> sortThis(IComparer<T> comparator)
        {
            Array.Sort(this.items, 0, this.size, comparator);
            return this;
        }

        public FastList<T> sortThis()
        {
            Array.Sort(this.items, 0, this.size);
            return this;
        }

        public FastList<T> reverseThis()
        {
            Array.Reverse(this.items);
            return this;
        }

        public bool addAll(ICollection<T> source)
        {
            if (source.Count == 0)
            {
                return false;
            }
            else
            {
                if (source.GetType() == typeof(FastList<T>))
                {
                    this.addAllFastList((FastList<T>)source);
                }
                //else if (source.GetType() == typeof(ArrayList<T>)) {
                //this.addAllArrayList((ArrayList<T>) source);
                //} else if (source is List<T> && source is RandomAccess<T>) {
                //    this.addAllRandomAccessList((List<T>) source);
                //}
                //else
                //{
                //    this.addAllCollection(source);
                //}

                return true;
            }
        }

        private void addAllFastList(FastList<T> source)
        {
            int newSize = this.ensureCapacityForAddAll(source);
            Array.Copy(source.items, 0, this.items, this.size, source.Count);
            this.size = newSize;
        }

        //private void addAllArrayList(ArrayList<T> source)
        //{
        //    int newSize = this.ensureCapacityForAddAll(source);
        //    ArrayListIterate.toArray(source, this.items, this.size, source.Count);
        //    this.size = newSize;
        //}

        //private void addAllRandomAccessList(List<T> source)
        //{
        //    int newSize = this.ensureCapacityForAddAll(source);
        //    RandomAccessListIterate.toArray(source, this.items, this.size, source.Count);
        //    this.size = newSize;
        //}

        //private void addAllCollection(ICollection<T> source)
        //{
        //    this.ensureCapacity(this.size + source.Count);
        //    Iterate.forEachWith(source, Procedures2.addToCollection(), this);
        //}

        //public bool containsAll(ICollection<T> source)
        //{
        //    return Iterate.allSatisfyWith(source, Predicates2.in (), this);
        //}

        //public bool containsAllArguments(Object...source)
        //{
        //    return ArrayIterate.allSatisfyWith(source, Predicates2.in (), this);
        //}

        public T[] toArray(T[] array)
        {
            if (array.Length < this.size)
            {
                array = new T[this.size];
            }

            Array.Copy(this.items, 0, array, 0, this.size);
            if (array.Length > this.size)
            {
                array[this.size] = default(T);
            }

            return array;
        }

        public T[] ToArray()
        {
            return this.copyItemsWithNewCapacity(this.size);
        }

        //public T[] toTypedArray(Class<T> clazz)
        //{
        //    T[] array = (Array.newInstance(clazz, this.size));
        //    Array.Copy(this.items, 0, array, 0, this.size);
        //    return array;
        //}

        private void throwOutOfBounds(int index)
        {
            throw this.newIndexOutOfBoundsException(index);
        }

        public T set(int index, T element)
        {
            T previous = this.get(index);
            this.items[index] = element;
            return previous;
        }

        public int indexOf(T obj)
        {
            return this.items.ToList().IndexOf(obj);
            //return InternalArrayIterate.indexOf(this.items, this.size, obj);
        }

        //public int lastIndexOf(T obj)
        //{
        //    return InternalArrayIterate.lastIndexOf(this.items, this.size, object);
        //}

        //public Spliterator<T> spliterator()
        //{
        //    return Spliterators.spliterator(this.items, 0, this.size, 16);
        //}

        public void trimToSize()
        {
            if (this.size < this.items.Length)
            {
                this.transferItemsToNewArrayWithCapacity(this.size);
            }

        }

        public bool trimToSizeIfGreaterThanPercent(double loadFactor)
        {
            double excessCapacity = 1.0D - (double)this.size / (double)this.items.Length;
            if (excessCapacity > loadFactor)
            {
                this.trimToSize();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ensureCapacityForAdd()
        {
            if (this.items == DEFAULT_SIZED_EMPTY_ARRAY)
            {
                this.items = (new T[10]);
            }
            else
            {
                this.transferItemsToNewArrayWithCapacity(this.sizePlusFiftyPercent(this.size));
            }

        }

        private int ensureCapacityForAddAll(ICollection<T> source)
        {
            int sourceSize = source.Count;
            int newSize = this.size + sourceSize;
            this.ensureCapacity(newSize);
            return newSize;
        }

        public void ensureCapacity(int minCapacity)
        {
            int oldCapacity = this.items.Length;
            if (minCapacity > oldCapacity)
            {
                int newCapacity = Math.Max(this.sizePlusFiftyPercent(oldCapacity), minCapacity);
                this.transferItemsToNewArrayWithCapacity(newCapacity);
            }

        }

        private void transferItemsToNewArrayWithCapacity(int newCapacity)
        {
            this.items = this.copyItemsWithNewCapacity(newCapacity);
        }

        private T[] copyItemsWithNewCapacity(int newCapacity)
        {
            T[] newItems = new T[newCapacity];
            Array.Copy(this.items, 0, newItems, 0, Math.Min(this.size, newCapacity));
            return newItems;
        }

        public FastList<T> with(T element1, T element2)
        {
            this.add(element1);
            this.add(element2);
            return this;
        }

        public FastList<T> with(T element1, T element2, T element3)
        {
            this.add(element1);
            this.add(element2);
            this.add(element3);
            return this;
        }

        public FastList<T> with(params T[] elements)
        {
            return this.withArrayCopy(elements, 0, elements.Length);
        }

        public FastList<T> withArrayCopy(T[] elements, int begin, int length)
        {
            this.ensureCapacity(this.size + length);
            Array.Copy(elements, begin, this.items, this.size, length);
            this.size += length;
            return this;
        }

        public T getFirst()
        {
            return this.size == 0 ? default(T) : this.items[0];
        }

        public T getLast()
        {
            return this.size == 0 ? default(T) : this.items[this.size - 1];
        }

        //public FastListMultimap<V, T> groupBy(Function<? super T, ? extends V> function)
        //{
        //    return (FastListMultimap)this.groupBy(function, FastListMultimap.newMultimap());
        //}

        //public < V, R extends MutableMultimap<V, T>> R groupBy(Function<? super T, ? extends V> function, R target)
        //{
        //    return InternalArrayIterate.groupBy(this.items, this.size, function, target);
        //}

        //public < V > FastListMultimap<V, T> groupByEach(Function<? super T, ? extends Iterable<V>> function)
        //{
        //    return (FastListMultimap)this.groupByEach(function, FastListMultimap.newMultimap());
        //}

        //public < V, R extends MutableMultimap<V, T>> R groupByEach(Function<? super T, ? extends Iterable<V>> function, R target)
        //{
        //    return InternalArrayIterate.groupByEach(this.items, this.size, function, target);
        //}

        //public < K > MutableMap<K, T> groupByUniqueKey(Function<? super T, ? extends K> function)
        //{
        //    return this.groupByUniqueKey(function, UnifiedMap.newMap());
        //}

        //public < K, R extends MutableMap<K, T>> R groupByUniqueKey(Function<? super T, ? extends K> function, R target)
        //{
        //    return InternalArrayIterate.groupByUniqueKey(this.items, this.size, function, target);
        //}

        //public void appendString(Appendable appendable, String start, String separator, String end)
        //{
        //    InternalArrayIterate.appendString(this, this.items, this.size, appendable, start, separator, end);
        //}

        //public MutableList<T> take(int count)
        //{
        //    return RandomAccessListIterate.take(this, count);
        //}

        //public MutableList<T> drop(int count)
        //{
        //    return RandomAccessListIterate.drop(this, count);
        //}

        //public PartitionFastList<T> partition(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.partition(this.items, this.size, predicate);
        //}

        //public < P > PartitionFastList<T> partitionWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return InternalArrayIterate.partitionWith(this.items, this.size, predicate, parameter);
        //}

        //public void each(Procedure<? super T> procedure)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        procedure.value(this.items[i]);
        //    }

        //}

        //public void forEachIf(Predicate<? super T> predicate, Procedure<? super T> procedure)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        T item = this.items[i];
        //        if (predicate.accept(item))
        //        {
        //            procedure.value(item);
        //        }
        //    }

        //}

        //public void forEachWithIndex(ObjectIntProcedure<? super T> objectIntProcedure)
        //{
        //    InternalArrayIterate.forEachWithIndex(this.items, this.size, objectIntProcedure);
        //}

        //public < P > void forEachWith(Procedure2<? super T, ? super P> procedure, P parameter)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        procedure.value(this.items[i], parameter);
        //    }

        //}

        //public FastList<T> select(Predicate<? super T> predicate)
        //{
        //    return (FastList)this.select(predicate, newList());
        //}

        //public < R extends Collection<T>> R select(Predicate<? super T> predicate, R target)
        //{
        //    return InternalArrayIterate.select(this.items, this.size, predicate, target);
        //}

        //public < P > FastList<T> selectWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return (FastList)this.selectWith(predicate, parameter, newList());
        //}

        //public < P, R extends Collection<T>> R selectWith(Predicate2<? super T, ? super P> predicate, P parameter, R target)
        //{
        //    return InternalArrayIterate.selectWith(this.items, this.size, predicate, parameter, target);
        //}

        //public FastList<T> reject(Predicate<? super T> predicate)
        //{
        //    return (FastList)this.reject(predicate, newList());
        //}

        //public < R extends Collection<T>> R reject(Predicate<? super T> predicate, R target)
        //{
        //    return InternalArrayIterate.reject(this.items, this.size, predicate, target);
        //}

        //public < P > FastList<T> rejectWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return (FastList)this.rejectWith(predicate, parameter, newList());
        //}

        //public < P, R extends Collection<T>> R rejectWith(Predicate2<? super T, ? super P> predicate, P parameter, R target)
        //{
        //    return InternalArrayIterate.rejectWith(this.items, this.size, predicate, parameter, target);
        //}

        //public < P > Twin<MutableList<T>> selectAndRejectWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return InternalArrayIterate.selectAndRejectWith(this.items, this.size, predicate, parameter);
        //}

        //public < S > FastList<S> selectInstancesOf(Class<S> clazz)
        //{
        //    return InternalArrayIterate.selectInstancesOf(this.items, this.size, clazz);
        //}

        //public bool removeIf(Predicate<? super T> predicate)
        //{
        //    int currentFilledIndex = 0;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        T item = this.items[i];
        //        if (!predicate.accept(item))
        //        {
        //            if (currentFilledIndex != i)
        //            {
        //                this.items[currentFilledIndex] = item;
        //            }

        //            ++currentFilledIndex;
        //        }
        //    }

        //    bool changed = currentFilledIndex < this.size;
        //    this.wipeAndResetTheEnd(currentFilledIndex);
        //    return changed;
        //}

        private void wipeAndResetTheEnd(int newCurrentFilledIndex)
        {
            for (int i = newCurrentFilledIndex; i < this.size; ++i)
            {
                this.items[i] = default(T);
            }

            this.size = newCurrentFilledIndex;
        }

        //public < P > bool removeIfWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    int currentFilledIndex = 0;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        T item = this.items[i];
        //        if (!predicate.accept(item, parameter))
        //        {
        //            if (currentFilledIndex != i)
        //            {
        //                this.items[currentFilledIndex] = item;
        //            }

        //            ++currentFilledIndex;
        //        }
        //    }

        //    bool changed = currentFilledIndex < this.size;
        //    this.wipeAndResetTheEnd(currentFilledIndex);
        //    return changed;
        //}

        //public < V > FastList<V> collect(Function<? super T, ? extends V> function)
        //{
        //    return (FastList)this.collect(function, newList(this.Count));
        //}

        //public MutableboolList collectbool(boolFunction<? super T> boolFunction)
        //{
        //    return (MutableboolList)this.collectbool(boolFunction, new boolArrayList(this.size));
        //}

        //public < R extends MutableboolCollection> R collectbool(boolFunction<? super T> boolFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(boolFunction.boolValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableByteList collectByte(ByteFunction<? super T> byteFunction)
        //{
        //    return (MutableByteList)this.collectByte(byteFunction, new ByteArrayList(this.size));
        //}

        //public < R extends MutableByteCollection> R collectByte(ByteFunction<? super T> byteFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(byteFunction.byteValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableCharList collectChar(CharFunction<? super T> charFunction)
        //{
        //    return (MutableCharList)this.collectChar(charFunction, new CharArrayList(this.size));
        //}

        //public < R extends MutableCharCollection> R collectChar(CharFunction<? super T> charFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(charFunction.charValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableDoubleList collectDouble(DoubleFunction<? super T> doubleFunction)
        //{
        //    return (MutableDoubleList)this.collectDouble(doubleFunction, new DoubleArrayList(this.size));
        //}

        //public < R extends MutableDoubleCollection> R collectDouble(DoubleFunction<? super T> doubleFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(doubleFunction.doubleValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableFloatList collectFloat(FloatFunction<? super T> floatFunction)
        //{
        //    return (MutableFloatList)this.collectFloat(floatFunction, new FloatArrayList(this.size));
        //}

        //public < R extends MutableFloatCollection> R collectFloat(FloatFunction<? super T> floatFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(floatFunction.floatValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableIntList collectInt(IntFunction<? super T> intFunction)
        //{
        //    return (MutableIntList)this.collectInt(intFunction, new IntArrayList(this.size));
        //}

        //public < R extends MutableIntCollection> R collectInt(IntFunction<? super T> intFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(intFunction.intValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableLongList collectLong(LongFunction<? super T> longFunction)
        //{
        //    return (MutableLongList)this.collectLong(longFunction, new LongArrayList(this.size));
        //}

        //public < R extends MutableLongCollection> R collectLong(LongFunction<? super T> longFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(longFunction.longValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public MutableShortList collectShort(ShortFunction<? super T> shortFunction)
        //{
        //    return (MutableShortList)this.collectShort(shortFunction, new ShortArrayList(this.size));
        //}

        //public < R extends MutableShortCollection> R collectShort(ShortFunction<? super T> shortFunction, R target)
        //{
        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        target.add(shortFunction.shortValueOf(this.items[i]));
        //    }

        //    return target;
        //}

        //public < V, R extends Collection<V>> R collect(Function<? super T, ? extends V> function, R target)
        //{
        //    return InternalArrayIterate.collect(this.items, this.size, function, target);
        //}

        //public < V > FastList<V> flatCollect(Function<? super T, ? extends Iterable<V>> function)
        //{
        //    return (FastList)this.flatCollect(function, newList(this.Count));
        //}

        //public < V, R extends Collection<V>> R flatCollect(Function<? super T, ? extends Iterable<V>> function, R target)
        //{
        //    return InternalArrayIterate.flatCollect(this.items, this.size, function, target);
        //}

        //public < P, V > FastList<V> collectWith(Function2<? super T, ? super P, ? extends V> function, P parameter)
        //{
        //    return (FastList)this.collectWith(function, parameter, newList(this.Count));
        //}

        //public < P, V, R extends Collection<V>> R collectWith(Function2<? super T, ? super P, ? extends V> function, P parameter, R target)
        //{
        //    return InternalArrayIterate.collectWith(this.items, this.size, function, parameter, target);
        //}

        //public < V > FastList<V> collectIf(Predicate<? super T> predicate, Function<? super T, ? extends V> function)
        //{
        //    return (FastList)this.collectIf(predicate, function, newList());
        //}

        //public < V, R extends Collection<V>> R collectIf(Predicate<? super T> predicate, Function<? super T, ? extends V> function, R target)
        //{
        //    return InternalArrayIterate.collectIf(this.items, this.size, predicate, function, target);
        //}

        //public T detect(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.detect(this.items, this.size, predicate);
        //}

        //public < P > T detectWith(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return InternalArrayIterate.detectWith(this.items, this.size, predicate, parameter);
        //}

        //public Optional<T> detectOptional(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.detectOptional(this.items, this.size, predicate);
        //}

        //public < P > Optional<T> detectWithOptional(Predicate2<? super T, ? super P> predicate, P parameter)
        //{
        //    return InternalArrayIterate.detectWithOptional(this.items, this.size, predicate, parameter);
        //}

        //public int detectIndex(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.detectIndex(this.items, this.size, predicate);
        //}

        //public int detectLastIndex(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.detectLastIndex(this.items, this.size, predicate);
        //}

        //public T min(IComparer<T> comparator)
        //{
        //    return InternalArrayIterate.min(this.items, this.size, comparator);
        //}

        //public T max(Comparator<? super T> comparator)
        //{
        //    return InternalArrayIterate.Max(this.items, this.size, comparator);
        //}

        //public T min()
        //{
        //    return InternalArrayIterate.min(this.items, this.size);
        //}

        //public T max()
        //{
        //    return InternalArrayIterate.Max(this.items, this.size);
        //}

        //public < V extends Comparable<? super V>> T minBy(Function<? super T, ? extends V> function)
        //{
        //    return InternalArrayIterate.minBy(this.items, this.size, function);
        //}

        //public < V extends Comparable<? super V>> T maxBy(Function<? super T, ? extends V> function)
        //{
        //    return InternalArrayIterate.MaxBy(this.items, this.size, function);
        //}

        public T get(int index)
        {
            if (index < this.size)
            {
                return this.items[index];
            }
            else
            {
                throw this.newIndexOutOfBoundsException(index);
            }
        }

        private IndexOutOfRangeException newIndexOutOfBoundsException(int index)
        {
            return new IndexOutOfRangeException("Index: " + index + " Size: " + this.size);
        }

        public bool add(T newItem)
        {
            if (this.items.Length == this.size)
            {
                this.ensureCapacityForAdd();
            }

            this.items[this.size++] = newItem;
            return true;
        }

        public void add(int index, T element)
        {
            if (index > -1 && index < this.size)
            {
                this.addAtIndex(index, element);
            }
            else if (index == this.size)
            {
                this.add(element);
            }
            else
            {
                this.throwOutOfBounds(index);
            }

        }

        private void addAtIndex(int index, T element)
        {
            int oldSize = this.size++;
            if (this.items.Length == oldSize)
            {
                T[] newItems = (new T[this.sizePlusFiftyPercent(oldSize)]);
                if (index > 0)
                {
                    Array.Copy(this.items, 0, newItems, 0, index);
                }

                Array.Copy(this.items, index, newItems, index + 1, oldSize - index);
                this.items = newItems;
            }
            else
            {
                Array.Copy(this.items, index, this.items, index + 1, oldSize - index);
            }

            this.items[index] = element;
        }

        private int sizePlusFiftyPercent(int oldSize)
        {
            int result = oldSize + (oldSize >> 1) + 1;
            return result < oldSize ? 2147483639 : result;
        }

        public T remove(int index)
        {
            T previous = this.get(index);
            int totalOffset = this.size - index - 1;
            if (totalOffset > 0)
            {
                Array.Copy(this.items, index + 1, this.items, index, totalOffset);
            }

            this.items[--this.size] = default(T);
            return previous;
        }

        public bool remove(T obj)
        {
            int index = this.indexOf(obj);
            if (index >= 0)
            {
                this.remove(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        //        public bool addAll(int index, ICollection<T> source)
        //        {
        //            if (index > this.size || index < 0)
        //            {
        //                this.throwOutOfBounds(index);
        //            }

        //            if (source.Count == 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                if (source.getClass() == FastList.class) {
        //    this.addAllFastListAtIndex((FastList) source, index);
        //    } else if (source.getClass() == ArrayList.class) {
        //    this.addAllArrayListAtIndex((ArrayList) source, index);
        //} else if (source is List && source is RandomAccess) {
        //    this.addAllRandomAccessListAtIndex((List) source, index);
        //} else
        //{
        //    this.addAllCollectionAtIndex(source, index);
        //}

        //return true;
        //        }
        //    }

        //    private void addAllFastListAtIndex(FastList<T> source, int index)
        //{
        //    int sourceSize = source.Count;
        //    int newSize = this.size + sourceSize;
        //    this.ensureCapacity(newSize);
        //    this.shiftElementsAtIndex(index, sourceSize);
        //    Array.Copy(source.items, 0, this.items, index, sourceSize);
        //    this.size = newSize;
        //}

        //private void addAllArrayListAtIndex(ArrayList<T> source, int index)
        //{
        //    int sourceSize = source.Count;
        //    int newSize = this.size + sourceSize;
        //    this.ensureCapacity(newSize);
        //    this.shiftElementsAtIndex(index, sourceSize);
        //    ArrayListIterate.toArray(source, this.items, index, sourceSize);
        //    this.size = newSize;
        //}

        //private void addAllRandomAccessListAtIndex(List<T> source, int index)
        //{
        //    int sourceSize = source.Count;
        //    int newSize = this.size + sourceSize;
        //    this.ensureCapacity(newSize);
        //    this.shiftElementsAtIndex(index, sourceSize);
        //    RandomAccessListIterate.toArray(source, this.items, index, sourceSize);
        //    this.size = newSize;
        //}

        //private void addAllCollectionAtIndex(ICollection<T> source, int index)
        //{
        //    Object[] newItems = source.ToArray();
        //    int sourceSize = newItems.Length;
        //    int newSize = this.size + sourceSize;
        //    this.ensureCapacity(newSize);
        //    this.shiftElementsAtIndex(index, sourceSize);
        //    this.size = newSize;
        //    Array.Copy(newItems, 0, this.items, index, sourceSize);
        //}

        //private void shiftElementsAtIndex(int index, int sourceSize)
        //{
        //    int numberToMove = this.size - index;
        //    if (numberToMove > 0)
        //    {
        //        Array.Copy(this.items, index, this.items, index + sourceSize, numberToMove);
        //    }

        //}

        public int Size()
        {
            return this.size;
        }

        //public int count(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.count(this.items, this.size, predicate);
        //}

        //public < P > int countWith(Predicate2 <? super T, ? super P > predicate, P parameter) {
        //    return InternalArrayIterate.countWith(this.items, this.size, predicate, parameter);
        //}

        //public < S > bool corresponds(OrderedIterable < S > other, Predicate2 <? super T, ? super S > predicate) {
        //    return InternalArrayIterate.corresponds(this.items, this.size, other, predicate);
        //}

        //public bool anySatisfy(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.anySatisfy(this.items, this.size, predicate);
        //}

        //public < P > bool anySatisfyWith(Predicate2 <? super T, ? super P > predicate, P parameter) {
        //    return InternalArrayIterate.anySatisfyWith(this.items, this.size, predicate, parameter);
        //}

        //public bool allSatisfy(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.allSatisfy(this.items, this.size, predicate);
        //}

        //public < P > bool allSatisfyWith(Predicate2 <? super T, ? super P > predicate, P parameter) {
        //    return InternalArrayIterate.allSatisfyWith(this.items, this.size, predicate, parameter);
        //}

        //public bool noneSatisfy(Predicate<? super T> predicate)
        //{
        //    return InternalArrayIterate.noneSatisfy(this.items, this.size, predicate);
        //}

        //public < P > bool noneSatisfyWith(Predicate2 <? super T, ? super P > predicate, P parameter) {
        //    return InternalArrayIterate.noneSatisfyWith(this.items, this.size, predicate, parameter);
        //}

        //public < IV > IV injectInto(IV injectedValue, Function2 <? super IV, ? super T, ? extends IV > function) {
        //    IV result = injectedValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.value(result, this.items[i]);
        //    }

        //    return result;
        //}

        //public int injectInto(int injectedValue, IntObjectToIntFunction<? super T> function)
        //{
        //    int result = injectedValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.intValueOf(result, this.items[i]);
        //    }

        //    return result;
        //}

        //public long injectInto(long injectedValue, LongObjectToLongFunction<? super T> function)
        //{
        //    long result = injectedValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.longValueOf(result, this.items[i]);
        //    }

        //    return result;
        //}

        //public double injectInto(double injectedValue, DoubleObjectToDoubleFunction<? super T> function)
        //{
        //    double result = injectedValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.doubleValueOf(result, this.items[i]);
        //    }

        //    return result;
        //}

        //public float injectInto(float injectedValue, FloatObjectToFloatFunction<? super T> function)
        //{
        //    float result = injectedValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.floatValueOf(result, this.items[i]);
        //    }

        //    return result;
        //}

        //public MutableList<T> distinct()
        //{
        //    return InternalArrayIterate.distinct(this.items, this.size);
        //}

        //public MutableList<T> distinct(HashingStrategy<? super T> hashingStrategy)
        //{
        //    return InternalArrayIterate.distinct(this.items, this.size, hashingStrategy);
        //}

        //public IntSummaryStatistics summarizeInt(IntFunction<? super T> function)
        //{
        //    return InternalArrayIterate.summarizeInt(this.items, this.size, function);
        //}

        //public DoubleSummaryStatistics summarizeFloat(FloatFunction<? super T> function)
        //{
        //    return InternalArrayIterate.summarizeFloat(this.items, this.size, function);
        //}

        //public LongSummaryStatistics summarizeLong(LongFunction<? super T> function)
        //{
        //    return InternalArrayIterate.summarizeLong(this.items, this.size, function);
        //}

        //public DoubleSummaryStatistics summarizeDouble(DoubleFunction<? super T> function)
        //{
        //    return InternalArrayIterate.summarizeDouble(this.items, this.size, function);
        //}

        //public Optional<T> reduce(BinaryOperator<T> accumulator)
        //{
        //    return InternalArrayIterate.reduce(this.items, this.size, accumulator);
        //}

        //public < R, A > R reduceInPlace(Collector <? super T, A, R > collector) {
        //    return InternalArrayIterate.reduceInPlace(this.items, this.size, collector);
        //}

        //public < R > R reduceInPlace(Supplier < R > supplier, BiConsumer < R, ? super T > accumulator) {
        //    return InternalArrayIterate.reduceInPlace(this.items, this.size, supplier, accumulator);
        //}

        //public long sumOfInt(IntFunction<? super T> function)
        //{
        //    return InternalArrayIterate.sumOfInt(this.items, this.size, function);
        //}

        //public long sumOfLong(LongFunction<? super T> function)
        //{
        //    return InternalArrayIterate.sumOfLong(this.items, this.size, function);
        //}

        //public double sumOfFloat(FloatFunction<? super T> function)
        //{
        //    return InternalArrayIterate.sumOfFloat(this.items, this.size, function);
        //}

        //public double sumOfDouble(DoubleFunction<? super T> function)
        //{
        //    return InternalArrayIterate.sumOfDouble(this.items, this.size, function);
        //}

        //public < V > MutableObjectLongMap < V > sumByInt(Function <? super T, ? extends V > groupBy, IntFunction <? super T > function) {
        //    return InternalArrayIterate.sumByInt(this.items, this.size, groupBy, function);
        //}

        //public < V > MutableObjectLongMap < V > sumByLong(Function <? super T, ? extends V > groupBy, LongFunction <? super T > function) {
        //    return InternalArrayIterate.sumByLong(this.items, this.size, groupBy, function);
        //}

        //public < V > MutableObjectDoubleMap < V > sumByFloat(Function <? super T, ? extends V > groupBy, FloatFunction <? super T > function) {
        //    return InternalArrayIterate.sumByFloat(this.items, this.size, groupBy, function);
        //}

        //public < V > MutableObjectDoubleMap < V > sumByDouble(Function <? super T, ? extends V > groupBy, DoubleFunction <? super T > function) {
        //    return InternalArrayIterate.sumByDouble(this.items, this.size, groupBy, function);
        //}

        //public < IV, P > IV injectIntoWith(IV injectValue, Function3 <? super IV, ? super T, ? super P, ? extends IV > function, P parameter) {
        //    IV result = injectValue;

        //    for (int i = 0; i < this.size; ++i)
        //    {
        //        result = function.value(result, this.items[i], parameter);
        //    }

        //    return result;
        //}

        public FastList<T> toList()
        {
            return newList(this);
        }

        //public FastList<T> toSortedList()
        //{
        //    return this.toSortedList(IComparer.naturalOrder());
        //}

        //public FastList<T> toSortedList(Comparator<? super T> comparator)
        //{
        //    return newList(this).SortThis(comparator);
        //}

        //public MutableList<T> takeWhile(Predicate<? super T> predicate)
        //{
        //    int endIndex = this.detectNotIndex(predicate);
        //    T[] result = (new Object[endIndex]);
        //    Array.Copy(this.items, 0, result, 0, endIndex);
        //    return newListWith(result);
        //}

        //public MutableList<T> dropWhile(Predicate<? super T> predicate)
        //{
        //    int startIndex = this.detectNotIndex(predicate);
        //    int resultSize = this.Count - startIndex;
        //    T[] result = (new Object[resultSize]);
        //    Array.Copy(this.items, startIndex, result, 0, resultSize);
        //    return newListWith(result);
        //}

        //public PartitionMutableList<T> partitionWhile(Predicate<? super T> predicate)
        //{
        //    PartitionMutableList<T> result = new PartitionFastList();
        //    FastList<T> selected = (FastList)result.getSelected();
        //    FastList<T> rejected = (FastList)result.getRejected();
        //    int partitionIndex = this.detectNotIndex(predicate);
        //    int rejectedSize = this.Count - partitionIndex;
        //    selected.withArrayCopy(this.items, 0, partitionIndex);
        //    rejected.withArrayCopy(this.items, partitionIndex, rejectedSize);
        //    return result;
        //}

        //private int detectNotIndex(Predicate<? super T> predicate)
        //{
        //    for (int index = 0; index < this.size; ++index)
        //    {
        //        if (!predicate.accept(this.items[index]))
        //        {
        //            return index;
        //        }
        //    }

        //    return this.size;
        //}

        //public bool equals(Object that)
        //{
        //    if (that == this)
        //    {
        //        return true;
        //    }
        //    else if (!(that is List<T>)) {
        //    return false;
        //} else
        //{
        //    return that is FastList<T> ? this.fastListEquals((FastList)that) : InternalArrayIterate.arrayEqualsList(this.items, this.size, (List)that);
        //}
        //    }

        //    public bool fastListEquals(FastList<?> that)
        //{
        //    if (this.size != that.size)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < this.size; ++i)
        //        {
        //            if (!Comparators.nullSafeEquals(this.items[i], that.items[i]))
        //            {
        //                return false;
        //            }
        //        }

        //        return true;
        //    }
        //}

        public int hashCode()
        {
            int hashCode = 1;

            for (int i = 0; i < this.size; ++i)
            {
                T item = this.items[i];
                hashCode = 31 * hashCode + (item == null ? 0 : item.GetHashCode());
            }

            return hashCode;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        //public void Clear()
        //{
            
        //    throw new NotImplementedException();
        //}

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            //array = new T[this.items.Length];
            Array.ConstrainedCopy(this.items, 0, array, 0, this.size);
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //        public void writeExternal(ObjectOutput out) throws IOException
        //        {
        //        out.writeInt(this.Count);

        //        for(int i = 0; i< this.size; ++i) {
        //            out.writeObject(this.items[i]);
        //    }
        //}
    }
}

//public void readExternal(ObjectInput in) throws IOException, ClassNotFoundException {
//        this.size = in.readInt();
//this.items = (new Object[this.size]);

//for (int i = 0; i < this.size; ++i)
//{
//    this.items[i] = in.readObject();
//}

//    }

    //}
