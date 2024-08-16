using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace MyList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>()
            {
                1,2,3,4,
            };
            MyList<int> values = new MyList<int>(ints);
            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            values.Remove(4);
            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            values.RemoveAt(0);
            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
    public class MyList<T> : IList<T>
    {
        private T[] _list;
        private int _size;
        public int Count => _size;
        public T this[int index] {
            get
            {
                if (index < _list.Length)
                {
                    return _list[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < _list.Length)
                {
                    _list[index] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public MyList()
        {
            _list = new T[4];
            _size = 0;
        }

        public MyList(ICollection<T> list)
        {
            _list = new T[list.Count];
            _size = list.Count;
            list.CopyTo(_list, 0);
        }

        public MyList(int capasity)
        {
            _size = 0;
            _list = new T[capasity];
        }

        public void Add(T item)
        {
            if (_list.Length == _size)
            {
              
                Array.Resize(ref _list, _list.Length * 2);
            }
            _list[_size] = item;
            _size++;
        }

        public void Clear()
        {
            _list = new T[4];
            _size = 0;
        }

        public bool Contains(T item)
        {
            foreach (var MyItem in _list)
            {
                if (MyItem.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        public void CopyTo(T[] values)
        {
            if (values != null)
            {
                if (_list.Length < values.Length)
                {
                    _list.CopyTo(values, 0);
                    _size = values.Length;
                }
                else
                {
                    Array.Resize(ref _list, values.Length * 2);
                    _list.CopyTo(values, 0);
                    _size =_list.Length;

                }
            }
        }
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= _list.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size == _list.Length)
            {
                Array.Resize(ref _list, _list.Length * 2);
            }
            Array.Copy(_list, index, _list, index + 1, _size - index);
            _list[index] = item;
            _size++;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        public bool IsReadOnly => _list.IsReadOnly;
        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            else
            {
                Array.Copy(_list, index + 1, _list, index, _size - index - 1);
                Array.Resize(ref _list, _size - 1);
                _size--;
                return true;
            }

        }

        public int IndexOf(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException();
            }
            for(int i=0;i<_size; i++)
            {
                if (item.Equals(_list[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || array.Length - arrayIndex >= _size)
            {
                throw new ArgumentOutOfRangeException("array");
            }
            else
            {
                if (_list.Length - _size < array.Length - arrayIndex)
                {
                    Array.Resize(ref _list, array.Length - arrayIndex + _size);
                }
                Array.Copy(array, arrayIndex, _list, _size + 1, array.Length - arrayIndex);
                _size += array.Length - arrayIndex;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            Array.Copy(_list, index + 1, _list, index, _size - index - 1);
            _list[_size-1] = default(T);
            _size--;

        }
    }   
}
