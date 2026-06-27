namespace Scratch.Labuladong.Algorithms.SearchInRotatedSortedArr;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int Search(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;
            if (nums[mid] == target) return mid;
            if (nums[mid] >= nums[left])
            {
                // mid 落在断崖左边, nums[left..mid]有序
                if (target >= nums[left] && target < nums[mid])
                {
                    // target 落在 [left..mid)中
                    right = mid;
                }
                else
                {
                    // target 落在 [mid + 1..right]中
                    left = mid + 1;
                }
            }
            else
            {
                // mid 落在断崖右边, nums[mid..right]有序
                if (target <= nums[right - 1] && target > nums[mid])
                {
                    // target 落在 [mid + 1..right]中
                    left = mid + 1;
                }
                else
                {
                    // target 落在 [left..mid)中
                    right = mid;
                }
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
