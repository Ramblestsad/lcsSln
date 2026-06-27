namespace Scratch.Labuladong.Algorithms.ConstrainedSubsequenceSum;

// 1425. Constrained Subsequence Sum (Hard)
//
// Given an integer array nums and an integer k, return the maximum sum of a non-empty subsequence
// of that array such that for every two consecutive integers in the subsequence, nums[i] and
// nums[j], where i < j, the condition j - i <= k is satisfied.
//
// A subsequence of an array is obtained by deleting some number of elements (can be zero) from the
// array, leaving the remaining elements in their original order.
//
// Example 1:
//
// Input: nums = [10,2,-10,5,20], k = 2
// Output: 37
// Explanation: The subsequence is [10, 2, 5, 20].
//
// Example 2:
//
// Input: nums = [-1,-2,-3], k = 1
// Output: -1
// Explanation: The subsequence must be non-empty, so we choose the largest number.
//
// Example 3:
//
// Input: nums = [10,-2,-10,-5,20], k = 2
// Output: 23
// Explanation: The subsequence is [10, -2, -5, 20].
//
// Constraints:
//
// - 1 <= k <= nums.length <= 10^5
//
// - -10^4 <= nums[i] <= 10^4
//
// Related Topics: Array, Dynamic Programming, Queue, Sliding Window, Heap (Priority Queue), Monotonic Queue

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int ConstrainedSubsetSum(int[] nums, int k)
    {
        var n = nums.Length;
        // 定义：dp[i] 表示以 nums[i] 结尾的子序列的最大和
        var dp = new int[n];
        dp[0] = nums[0];
        // 单调队列辅助计算 dp[i-k..i-1] 的最大值
        var window = new MonotonicQueue<int>();
        window.Push(dp[0]);

        for (var i = 1; i < n; i++)
        {
            // 状态转移方程
            dp[i] = Math.Max(nums[i], window.Max() + nums[i]);
            // 维护滑动窗口的大小为 k
            if (window.Size() == k)
            {
                window.Pop();
            }

            window.Push(dp[i]);
        }

        // dp 数组中的最大值就是结果
        var res = int.MinValue;
        for (var i = 0; i < n; i++)
        {
            res = Math.Max(res, dp[i]);
        }

        return res;
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
