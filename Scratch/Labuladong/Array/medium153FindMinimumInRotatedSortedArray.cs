namespace Scratch.Labuladong.Algorithms.FindMinRotatedSortedArr;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int FindMin(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;

            if (nums[mid] > nums[right])
            {
                // mid 落在断崖左边，最小值在 [mid+1, right]
                left = mid + 1;
            }
            else
            {
                // mid 落在断崖右边（或本身就是最小值），最小值在 [left, mid]
                right = mid;
            }
        }

        return nums[left];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
