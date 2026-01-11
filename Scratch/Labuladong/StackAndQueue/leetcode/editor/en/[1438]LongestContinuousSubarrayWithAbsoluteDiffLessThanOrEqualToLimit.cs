namespace Scratch.Labuladong.Algorithms.LongestContinuousSubarrayWithAbsoluteDiffLessThanOrEqualToLimit;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestSubarray(int[] nums, int limit)
    {
        /*
         * 当窗口内绝对值之差不超过 limit 时扩大窗口，
         * 当新加入窗口的元素使得绝对值之差超过 limit 时开始收缩窗口，
         * 窗口的最大宽度即最长子数组的长度。
         */

        var window = new MonotonicQueue<int>();
        var left = 0;
        var right = 0;
        var windowSize = 0;
        var res = 0;

        // sliding window
        while (right < nums.Length)
        {
            // 扩大窗口，更新窗口最值
            window.Push(nums[right]);
            right++;
            windowSize++;

            while (( window.Max() - window.Min() ) > limit)
            {
                // 缩小窗口，更新窗口最值
                window.Pop();
                left++;
                windowSize--;
            }

            // 在窗口收缩判断完之后才更新答案
            res = Math.Max(res, windowSize);
        }

        return res;
    }

    public class MonotonicQueue<T> where T: IComparable
    {
        // 常规队列，存储所有元素
        LinkedList<T> q = new LinkedList<T>();

        // 元素降序排列的单调队列，头部是最大值
        LinkedList<T> maxQ = new LinkedList<T>();

        // 元素升序排列的单调队列，头部是最小值
        LinkedList<T> minQ = new LinkedList<T>();

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
            // minQ 的头部是最大元素
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
