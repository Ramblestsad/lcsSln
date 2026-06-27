namespace Scratch.Labuladong.Algorithms.ShortestUnsortedContinuousSubarray;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int FindUnsortedSubarray(int[] nums)
    {
        // 排序解法
        var sortedNums = new int[nums.Length];
        Array.Copy(nums, sortedNums, nums.Length);
        Array.Sort(sortedNums);

        var left = int.MaxValue;
        var right = int.MinValue;

        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] != sortedNums[i])
            {
                left = i;
                break;
            }
        }

        for (var i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] != sortedNums[i])
            {
                right = i;
                break;
            }
        }

        if (left == int.MaxValue && right == int.MinValue) return 0; // 本来有序

        return right - left + 1;
    }

    public int FindUnsortedSubarrayMonotonicStk(int[] nums)
    {
        var n = nums.Length;
        int left = int.MaxValue, right = int.MinValue;

        // 递增栈，存储元素索引
        var incrStk = new Stack<int>();
        for (var i = 0; i < n; i++)
        {
            while (incrStk.Count != 0 && nums[i] < nums[incrStk.Peek()])
            {
                left = Math.Min(left, incrStk.Pop());
            }

            incrStk.Push(i);
        }

        // 递减栈，存储元素索引
        var decrStk = new Stack<int>();
        for (var i = n - 1; i >= 0; i--)
        {
            while (decrStk.Count != 0 && nums[i] > nums[decrStk.Peek()])
            {
                right = Math.Max(right, decrStk.Pop());
            }

            decrStk.Push(i);
        }

        if (left == int.MaxValue && right == int.MinValue) return 0; // 本来有序

        return right - left + 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
