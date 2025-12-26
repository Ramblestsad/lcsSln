namespace Scratch.Labuladong.Algorithms.SplitArrayLargestSum;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int SplitArray(int[] nums, int k)
    {
        int left = 0, right = 0;

        foreach (var num in nums)
        {
            left = Math.Max(left, num);
            right += num;
        }

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (F(nums, mid) < k) right = mid - 1;
            else if (F(nums, mid) > k) left = mid + 1;
            else right = mid - 1; // F(x) == k
        }

        return left;
    }

    static int F(int[] nums, int x)
    {
        var days = 0;

        for (int i = 0; i < nums.Length;)
        {
            var cap = x;
            while (i < nums.Length)
            {
                if (cap < nums[i]) break;
                cap -= nums[i];
                i++;
            }

            days++;
        }

        return days;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
