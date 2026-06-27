namespace Scratch.Labuladong.Algorithms.ShortestSubarrayWithSumAtLeastK;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int ShortestSubarray(int[] nums, int k)
    {
        /*
         * 前缀和技巧 预计算一个 preSum 数组，
         * 然后在这个 preSum 数组上施展 滑动窗口算法 寻找一个差值大于等于 k 且宽度最小的「窗口」，
         * 这个窗口的大小就是题目想要的结果。
         *
         * 当滑动窗口扩大时，新进入窗口的元素 preSum[right] 需要知道窗口中最小的那个元素是多少，
         * 和最小的那个元素相减才能得到尽可能大的子数组和
         */

        var n = nums.Length;
        // 看题目的数据范围，前缀和数组中元素可能非常大，所以用 long 类型
        var preSum = new long[n + 1];
        preSum[0] = 0;
        // 计算 nums 的前缀和数组
        for (var i = 1; i <= n; i++)
        {
            preSum[i] = preSum[i - 1] + nums[i - 1];
        }

        // 单调队列结构辅助滑动窗口算法
        var window = new MonotonicQueue<long>();
        var left = 0;
        var right = 0;
        var len = int.MaxValue;

        while (right < preSum.Length)
        {
            // 扩大窗口，元素入队
            window.Push(preSum[right]);
            right++;

            // 若新进入窗口的元素和窗口中的最小值之差大于等于 k，
            // 说明得到了符合条件的子数组，缩小窗口，使子数组长度尽可能小
            while (right < preSum.Length && !window.IsEmpty()
                                         && preSum[right] - window.Min() >= k)
            {
                len = Math.Min(len, right - left); // right++; 之后，right 已经指向**下一个**要加入的前缀和下标
                // 缩小窗口
                window.Pop();
                left++;
            }
        }

        return len == int.MaxValue ? -1 : len;
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
