using System;
using System.Collections.Generic;
using System.Text;

namespace CryptLib
{
    class Queue<T>
    {
        private List<T> items;
        public int ItemsRemaining
        {
            get { return items.Count; }
        }

        public Queue()
        {
            items = new List<T>();
        }

        public Queue(T item)
        {
            items = new List<T>();
            items.Add(item);
        }
        public Queue(IEnumerable<T> items)
        {
            this.items = new List<T>(items);
        }

        public void Enqueue(T item)
        {
            items.Add(item);
        }

        public void Enqueue(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.items.Add(item);
            }
        }

        public T Dequeue()
        {
            T item = items[0];
            items.RemoveAt(0);
            return item;
        }
    }
}
