namespace Scratch.Labuladong.Algorithms.MaximumSumCircularSubarray;

// 918. Maximum Sum Circular Subarray (Medium)
//
// Given a circular integer array nums of length n, return the maximum possible sum of a non-empty
// subarray of nums.
//
// A circular array means the end of the array connects to the beginning of the array. Formally,
// the next element of nums[i] is nums[(i + 1) % n] and the previous element of nums[i] is nums[(i
// - 1 + n) % n].
//
// A subarray may only include each element of the fixed buffer nums at most once. Formally, for a
// subarray nums[i], nums[i + 1], ..., nums[j], there does not exist i <= k1, k2 <= j with k1 % n
// == k2 % n.
//
// Example 1:
//
// Input: nums = [1,-2,3,-2]
// Output: 3
// Explanation: Subarray [3] has maximum sum 3.
//
// Example 2:
//
// Input: nums = [5,-3,5]
// Output: 10
// Explanation: Subarray [5,5] has maximum sum 5 + 5 = 10.
//
// Example 3:
//
// Input: nums = [-3,-2,-3]
// Output: -2
// Explanation: Subarray [-2] has maximum sum -2.
//
// Constraints:
//
// - n == nums.length
//
// - 1 <= n <= 3 * 10^4
//
// - -3 * 10^4 <= nums[i] <= 3 * 10^4
//
// Related Topics: Array, Divide and Conquer, Dynamic Programming, Queue, Monotonic Queue

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MaxSubarraySumCircular(int[] nums)
    {
        /*
         * nums[i] 的下一个元素是 nums[(i + 1) % n]
         * nums[i] 的前一个元素是 nums[(i - 1 + n) % n]
         */

        var n = nums.Length;
        var preSum = new int[2 * n + 1];
        preSum[0] = 0;
        // 计算环状 nums 的前缀和
        for (var i = 1; i < preSum.Length; i++)
        {
            preSum[i] = preSum[i - 1] + nums[( i - 1 ) % n];
        }

        var maxSum = int.MinValue;
        var window = new MonotonicQueue<int>();

        window.Push(0);
        for (var i = 1; i < preSum.Length; i++)
        {
            maxSum = Math.Max(maxSum, preSum[i] - window.Min());
            // 维护窗口的大小为 nums 数组的大小
            if (window.Size() == n) window.Pop();
            window.Push(preSum[i]);
        }

        return maxSum;
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
