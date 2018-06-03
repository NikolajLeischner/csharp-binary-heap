namespace BinaryHeap {
  /// <summary>
  /// A Priority Queue implemented as a Binary Heap. Enqueue and dequeue both have a complexity of O(log(n)).
  /// 
  /// The code is intended to maximise performance. It intentionally does not include safety checks,
  /// and only offers the minimal API required to use it as the basis for Dijkstra or A* implementations.
  /// </summary>
  /// <typeparam name="K">The key (priority) type.</typeparam>
  /// <typeparam name="V">The value type.</typeparam>
  public class BinaryHeap<K, V> where K : System.IComparable {

    private struct Element {
      public K key;
      public V value;
    }

    private readonly Element[] data;
    private readonly int capacity;
    private readonly K supremum;
    private int size;

    /// <summary>
    /// Create a new <c>BinaryHeap</c>.
    /// </summary>
    /// <param name="capacity">The maximum number of elements that can be inserted.</param>
    /// <param name="infimum">A key guaranteed to be smaller than any key that might be inserted.
    /// Inserting keys which compare as less or equal to the infimum will break the heap.</param>
    /// <param name="supremum">A key guaranteed to be greater than any key that might be inserted.
    /// Inserting keys which compare as greater or equal to the supremum will break the heap.</param>
    public BinaryHeap(int capacity, K infimum, K supremum) {
      data = new Element[capacity + 2];
      data[0].key = infimum;
      this.capacity = capacity;
      this.supremum = supremum;
      data[capacity + 1].key = supremum;
      Clear();
    }

    /// <summary>
    /// Removes all keys and values from the <c>BinaryHeap</c>.
    /// </summary>
    public void Clear() {
      size = 0;
      int cap = capacity;
      for (int i = 1; i <= cap; ++i) {
        data[i].key = supremum;
        data[i].value = default(V);
      }
    }

    /// <summary>
    /// Returns the number of elements contained in the <c>BinaryHeap</c>.
    /// </summary>
    public int Count() {
      return size;
    }

    /// <summary>
    /// Enqueues an element.
    /// 
    /// Throws if the <c>BinaryHeap</c> is already full.
    /// </summary>
    /// <param name="value">The value to insert.</param>
    /// <param name="key">The key (priority) to insert.</param>
    public void Enqueue(V value, K key) {
      ++size;
      Element[] dat = data;
      int hole = size;
      int pred = hole >> 1;
      K predKey = dat[pred].key;
      while (predKey.CompareTo(key) > 0) {
        dat[hole].key = predKey;
        dat[hole].value = dat[pred].value;
        hole = pred;
        pred >>= 1;
        predKey = dat[pred].key;
      }

      dat[hole].key = key;
      dat[hole].value = value;
    }

    /// <summary>
    /// Removes the element with the smallest key (priority), and returns its value.
    /// 
    /// Use <c>Count()</c> to determine the current size of the queue. If the queue is empty this method will return a nonsensical value.
    /// </summary>
    /// <returns>The value that was dequeued.</returns>
    public V Dequeue() {
      V min = data[1].value;

      int hole = 1;
      int succ = 2;
      int sz = size;
      Element[] dat = data;

      while (succ < sz) {
        K key1 = dat[succ].key;
        K key2 = dat[succ + 1].key;
        if (key1.CompareTo(key2) > 0) {
          succ++;
          dat[hole].key = key2;
          dat[hole].value = dat[succ].value;
        } else {
          dat[hole].key = key1;
          dat[hole].value = dat[succ].value;
        }
        hole = succ;
        succ <<= 1;
      }

      K bubble = dat[sz].key;
      int pred = hole >> 1;
      while (dat[pred].key.CompareTo(bubble) > 0) {
        dat[hole] = dat[pred];
        hole = pred;
        pred >>= 1;
      }

      dat[hole].key = bubble;
      dat[hole].value = dat[sz].value;

      dat[size].key = supremum;
      size = sz - 1;

      return min;
    }
  }

}
