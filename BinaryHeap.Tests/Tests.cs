using NUnit.Framework;
using System;

namespace BinaryHeap {
  [TestFixture]
  public class Tests {

    BinaryHeap<int, string> queue;

    int queueSize = 10;

    int min = -10;

    int max = 1000;

    [SetUp]
    public void Setup() {
      queue = new BinaryHeap<int, string>(queueSize, min, max);
    }
    [Test]
    public void IsEmptyInitially() => Assert.AreEqual(0, queue.Count());

    [TestCase(new int[] { 0, 3, 6, 2, 5 }, new string[] { "a", "b", "c", "d", "e" }, new string[] { "a", "d", "b", "e", "c" })]
    [TestCase(new int[]{0, 1}, new string[]{"a", "b"}, new string[] { "a", "b" })]
    [TestCase(new int[] { 0 }, new string[] { "a" }, new string[] { "a" })]
    public void CanEnqueueAndDequeue(int[] keys, string[] values, string[] expectedOrdering) {
      for (int i = 0; i < keys.Length; ++i) {
        queue.Enqueue(values[i], keys[i]);
        Assert.AreEqual(i + 1, queue.Count());
      }
      for (int i = 0; i < keys.Length; ++i) {
        string value = queue.Dequeue();
        Assert.AreEqual(keys.Length - (i + 1), queue.Count());
        Assert.AreEqual(expectedOrdering[i], value);
      }
    }

    [Test]
    public void DequeueEmptyReturnsDefaultValue() => Assert.AreEqual(default(string), queue.Dequeue());

    [Test]
    public void EnqueueFullThrows() {
      Assert.Throws<IndexOutOfRangeException>(() => {
        for (int i = 0; i < queueSize * 2; ++i) {
          queue.Enqueue("too much", i);
        }
      });
    }
  }
}
