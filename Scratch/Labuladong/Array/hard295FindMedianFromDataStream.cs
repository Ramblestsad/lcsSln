namespace Scratch.Labuladong.Algorithms.FindMedianFromDataStream;

// 295. Find Median from Data Stream (Hard)
//
// The median is the middle value in an ordered integer list. If the size of the list is even,
// there is no middle value, and the median is the mean of the two middle values.
//
// - For example, for arr = [2,3,4], the median is 3.
//
// - For example, for arr = [2,3], the median is (2 + 3) / 2 = 2.5.
//
// Implement the MedianFinder class:
//
// - MedianFinder() initializes the MedianFinder object.
//
// - void addNum(int num) adds the integer num from the data stream to the data structure.
//
// - double findMedian() returns the median of all elements so far. Answers within 10^-5 of the
// actual answer will be accepted.
//
// Example 1:
//
// Input
// ["MedianFinder", "addNum", "addNum", "findMedian", "addNum", "findMedian"]
// [[], [1], [2], [], [3], []]
// Output
// [null, null, null, 1.5, null, 2.0]
//
// Explanation
// MedianFinder medianFinder = new MedianFinder();
// medianFinder.addNum(1); // arr = [1]
// medianFinder.addNum(2); // arr = [1, 2]
// medianFinder.findMedian(); // return 1.5 (i.e., (1 + 2) / 2)
// medianFinder.addNum(3); // arr[1, 2, 3]
// medianFinder.findMedian(); // return 2.0
//
// Constraints:
//
// - -10^5 <= num <= 10^5
//
// - There will be at least one element in the data structure before calling findMedian.
//
// - At most 5 * 10^4 calls will be made to addNum and findMedian.
//
// Follow up:
//
// - If all integer numbers from the stream are in the range [0, 100], how would you optimize your
// solution?
//
// - If 99% of all integer numbers from the stream are in the range [0, 100], how would you
// optimize your solution?
//
// Related Topics: Two Pointers, Design, Sorting, Heap (Priority Queue), Data Stream

//leetcode submit region begin(Prohibit modification and deletion)
public class MedianFinder
{
    // 右半边，最小堆，堆顶是右半边最小值
    private PriorityQueue<int, int> large;

    // 左半边，最大堆，堆顶是左半边最大值
    private PriorityQueue<int, int> small;

    public MedianFinder()
    {
        // minHeap
        large = new PriorityQueue<int, int>();
        // maxHeap
        small = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    }

    // 保持两件事：
    //  两个堆的数量最多差 1
    //  small 里的数都小于等于 large 里的数
    public void AddNum(int num)
    {
        if (small.Count >= large.Count)
        {
            small.Enqueue(num, num);
            var e = small.Dequeue();
            large.Enqueue(e, e);
        }
        else
        {
            large.Enqueue(num, num);
            var e2 = large.Dequeue();
            small.Enqueue(e2, e2);
        }
    }

    public double FindMedian()
    {
        // 如果元素不一样多，多的那个堆的堆顶就是median
        if (large.Count < small.Count) return small.Peek();
        if (large.Count > small.Count) return large.Peek();

        // 如果元素一样多，两个堆顶的mean就是median
        return ( large.Peek() + small.Peek() ) / 2.0;
    }
}

/**
 * Your MedianFinder object will be instantiated and called as such:
 * MedianFinder obj = new MedianFinder();
 * obj.AddNum(num);
 * double param_2 = obj.FindMedian();
 */
//leetcode submit region end(Prohibit modification and deletion)
