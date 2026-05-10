/*
 * @lc app=leetcode id=295 lang=csharp
 * @lcpr version=30403
 *
 * [295] Find Median from Data Stream
 */

namespace Scratch.Labuladong.Algorithms.FindMedianFromDataStream;

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// ["MedianFinder","addNum","addNum","findMedian","addNum","findMedian"]\n[[],[1],[2],[],[3],[]]\n
// @lcpr case=end
 */
