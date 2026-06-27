namespace Scratch.Labuladong.Algorithms.JumpGameVI;

// 1696. Jump Game VI (Medium)
//
// You are given a 0-indexed integer array nums and an integer k.
//
// You are initially standing at index 0. In one move, you can jump at most k steps forward without
// going outside the boundaries of the array. That is, you can jump from index i to any index in
// the range [i + 1, min(n - 1, i + k)] inclusive.
//
// You want to reach the last index of the array (index n - 1). Your score is the sum of all
// nums[j] for each index j you visited in the array.
//
// Return the maximum score you can get.
//
// Example 1:
//
// Input: nums = [1,-1,-2,4,-7,3], k = 2
// Output: 7
// Explanation: You can choose your jumps forming the subsequence [1,-1,4,3] (underlined above).
// The sum is 7.
//
// Example 2:
//
// Input: nums = [10,-5,-2,4,0,3], k = 3
// Output: 17
// Explanation: You can choose your jumps forming the subsequence [10,4,3] (underlined above). The
// sum is 17.
//
// Example 3:
//
// Input: nums = [1,-5,-20,4,-1,3,-6,-3], k = 2
// Output: 0
//
// Constraints:
//
// - 1 <= nums.length, k <= 10^5
//
// - -10^4 <= nums[i] <= 10^4
//
// Related Topics: Array, Dynamic Programming, Queue, Heap (Priority Queue), Monotonic Queue

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MaxResult(int[] nums, int k)
    {
        var window = new MonotonicQueue<int>();
        var n = nums.Length;
        // 定义：到达 nums[p] 的最大分数为 dp[p]
        var dp = new int[n];
        Array.Fill(dp, int.MinValue);
        // base case
        dp[0] = nums[0];
        window.Push(dp[0]);

        // 状态转移
        for (var p = 1; p < n; p++)
        {
            dp[p] = window.Max() + nums[p];
            // 维护窗口装着 dp[p-1..p-k]
            if (window.Size() == k) window.Pop();
            window.Push(dp[p]);
        }

        return dp[n - 1];
    }

    class MonotonicQueue<T> where T: IComparable<T>
    {
        // 常规队列，存储所有元素
        private LinkedList<T> q = new();

        // 元素降序排列的单调队列，头部是最大值
        private LinkedList<T> maxQ = new();

        // 元素升序排列的单调队列，头部是最小值
        private LinkedList<T> minQ = new();

        public void Push(T elem)
        {
            // 维护常规队列，直接在队尾插入元素
            q.AddLast(elem);

            // 维护 maxQ，将小于 elem 的元素全部删除
            while (maxQ.Count != 0 && maxQ.Last!.Value.CompareTo(elem) < 0)
            {
                maxQ.RemoveLast();
            }

            maxQ.AddLast(elem);

            // 维护 minQ，将大于 elem 的元素全部删除
            while (minQ.Count != 0 && minQ.Last!.Value.CompareTo(elem) > 0)
            {
                minQ.RemoveLast();
            }

            minQ.AddLast(elem);
        }

        public T Max()
        {
            // maxQ 的头部是最大元素
            return maxQ.First!.Value;
        }

        public T Min()
        {
            // minQ 的头部是最小元素
            return minQ.First!.Value;
        }

        public T Pop()
        {
            // 从标准队列头部弹出需要删除的元素
            var deleteVal = q.First!.Value;
            q.RemoveFirst();

            // 由于 push 的时候会删除元素，deleteVal 可能已经被删掉了
            if (deleteVal.Equals(maxQ.First!.Value))
            {
                maxQ.RemoveFirst();
            }

            if (deleteVal.Equals(minQ.First!.Value))
            {
                minQ.RemoveFirst();
            }

            return deleteVal;
        }

        public int Size()
        {
            // 标准队列的大小即是当前队列的大小
            return q.Count;
        }

        public bool IsEmpty()
        {
            return q.Count == 0;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
