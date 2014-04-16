using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCL.Core
{
    internal class FlexQueue<T>
    {
        // The head of the queue is the first item
        private List<T> queueStorage;

        internal FlexQueue()
        {
            queueStorage = new List<T>();
        }

        internal void Enqueue(T item)
        {
            queueStorage.Add(item);
        }

        internal T Dequeue()
        {
            var firstValue = queueStorage.First();
            queueStorage.RemoveAt(0);
            return firstValue;
        }

        internal int Count
        {
            get { return queueStorage.Count; }
        }

        internal T Peek()
        {
            return PeekAt(0);
        }

        internal T PeekAt(int index)
        {
            return queueStorage[index];
        }

        internal T RemoveAt(int index)
        {
            var entryAtIndex = queueStorage[index];
            queueStorage.RemoveAt(index);
            return entryAtIndex;
        }
    }
}
