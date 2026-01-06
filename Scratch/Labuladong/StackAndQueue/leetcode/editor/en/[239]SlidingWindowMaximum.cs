namespace Scratch.Labuladong.Algorithms.SlidingWindowMaximum;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        var window = new MonotonicQueue();
        var res = new List<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            if (i < k - 1)
            {
                // 先把窗口的前 k - 1 填满
                window.Push(nums[i]);
            }
            else
            {
                // 窗口开始向前滑动
                // 移入新元素，右
                window.Push(nums[i]);
                // 将当前窗口中的最大元素记入结果
                res.Add(window.Max());
                // 移出最后的元素，最左
                window.Pop(nums[i - k + 1]);
            }
        }

        return res.ToArray();
    }

    class MonotonicQueue
    {
        // 双链表，支持快速在头部和尾部增删元素
        // 维护其中的元素自尾部到头部单调递增
        private LinkedList<int> maxQ = new();

        // 在尾部添加一个元素 n，维护 maxQ 的单调性质
        public void Push(int n)
        {
            // 将前面小于自己的元素都删除
            while (maxQ.Count != 0 && maxQ.Last!.Value < n)
            {
                maxQ.RemoveLast();
            }

            maxQ.AddLast(n);
        }

        // 返回当前队列中的最大值
        public int Max()
        {
            // 队头就是最大的，队列单调递减
            return maxQ.First!.Value;
        }

        // 队头元素如果是 n，删除它
        public void Pop(int n)
        {
            // pop 方法之所以要判断 n == maxQ.First.Value，
            // 是因为要删除的队头元素 n 可能已经在 push 的过程中被remove了，可能已经不存在了，这种情况就不用删除了
            if (n == maxQ.First!.Value)
            {
                maxQ.RemoveFirst();
            }
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
