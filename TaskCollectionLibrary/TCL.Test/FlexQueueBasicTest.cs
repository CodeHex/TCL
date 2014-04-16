using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCL.Core;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TCL.Test
{
    [TestClass]
    public class FlexQueueBasicTest
    {
        [TestMethod]
        public void EnqueueItemToAFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<int>();

            // Act
            queue.Enqueue(23);
            queue.Enqueue(64);
            queue.Enqueue(100023);
            queue.Enqueue(-50);
        }

        [TestMethod]
        public void GetNextItemFromFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<string>();

            // Act
            queue.Enqueue("DDD");
            queue.Enqueue("EEE");
            queue.Enqueue("FFF");
            queue.Enqueue("GGG");

            var firstItem = queue.Dequeue();
            var secondItem = queue.Dequeue();
            var thirdItem = queue.Dequeue();

            // Assert
            Assert.AreEqual("DDD", firstItem);
            Assert.AreEqual("EEE", secondItem);
            Assert.AreEqual("FFF", thirdItem);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNextItemFromFlexQueueWhenNoItemsAvailable()
        {
            // Arrange
            var queue = new FlexQueue<long>();

            // Act
            queue.Dequeue();
        }

        [TestMethod]
        public void GetNumberOfItemsInFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<object>();
            queue.Enqueue("Hello");
            queue.Enqueue(65);
            queue.Enqueue(new List<string>());
            queue.Enqueue(new Action(() => Console.Write("Adding Action")));

            // Act and Assert
            Assert.AreEqual(4, queue.Count);
            queue.Dequeue();
            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void PeekAtNextItemInFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<Tuple<string,int>>();
            queue.Enqueue(new Tuple<string, int>("Entry1", 11111));
            queue.Enqueue(new Tuple<string, int>("Entry2", 22222));
            queue.Enqueue(new Tuple<string, int>("Entry3", 33333));
            queue.Enqueue(new Tuple<string, int>("Entry4", 44444));
            queue.Enqueue(new Tuple<string, int>("Entry5", 55555));

            // Assert
            Assert.AreEqual(5, queue.Count);
            var nextItem = queue.Peek();
            Assert.AreEqual(5, queue.Count);
            Assert.AreEqual("Entry1", nextItem.Item1);
            Assert.AreEqual(11111, nextItem.Item2);
        }

        [TestMethod]
        public void LookAtItemInTheMiddleOfFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<ushort>();
            var entries = new List<ushort>{10,1,9,2,3,8,50,31,2000,23,2,65,10000};
            
            entries.ForEach(x => queue.Enqueue(x));

            // Assert
            Enumerable.Range(0, entries.Count).ToList().ForEach(x =>
            {
                Assert.AreEqual(entries[x], queue.PeekAt(x));
                Assert.AreEqual(entries.Count, queue.Count);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LookAtItemInMiddleOfFlexQueueThatDoesntExist()
        {
            // Arrange
            var queue = new FlexQueue<ushort>();
            var entries = new List<char> { 'c','/','>','L','s' };

            entries.ForEach(x => queue.Enqueue(x));

            // Assert
            queue.PeekAt(500);
        }

        [TestMethod]
        public void RemoveItemFromMiddleOfFlexQueue()
        {
            // Arrange
            var queue = new FlexQueue<int>();
            var entries = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var expEntriesWith5Removed = new List<int> { 1, 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var expEntriesWith5and15Removed = new List<int> { 1, 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20 };
            var expEntriesWith5and15and1Removed = new List<int> { 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20 };

            entries.ForEach(x => queue.Enqueue(x));

            // Assert
            Assert.AreEqual(20, queue.Count);
            var firstItem = queue.RemoveAt(4);
            Assert.AreEqual(5, firstItem);
            Assert.AreEqual(19, queue.Count);
            Enumerable.Range(0, 19).ToList().ForEach(x => Assert.AreEqual(expEntriesWith5Removed[x], queue.PeekAt(x)));

            var secondItem = queue.RemoveAt(13);
            Assert.AreEqual(15, secondItem);
            Assert.AreEqual(18, queue.Count);
            Enumerable.Range(0, 18).ToList().ForEach(x => Assert.AreEqual(expEntriesWith5and15Removed[x], queue.PeekAt(x)));

            var thirdItem = queue.RemoveAt(0);
            Assert.AreEqual(1, thirdItem);
            Assert.AreEqual(17, queue.Count);
            Enumerable.Range(0, 17).ToList().ForEach(x => Assert.AreEqual(expEntriesWith5and15and1Removed[x], queue.PeekAt(x)));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveItemFromFlexQueueWhenOutOfRange()
        {
            // Arrange
            var queue = new FlexQueue<int>();
            var entries = new List<int> { 1, 2 };
            entries.ForEach(x => queue.Enqueue(x));

            // Act
            queue.RemoveAt(20);
        }


        [TestMethod]
        public void MoveThroughFlexQueueUntilItemIsFound()
        {
            throw new NotImplementedException();
        }
    }
}
