namespace Scratch.Labuladong.Algorithms.MaximumSumCircularSubarray;

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
