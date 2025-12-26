namespace Scratch.Labuladong.Algorithms.BinarySearch;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int Search(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;

        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;

            if (nums[mid] == target) return mid;
            else if (nums[mid] < target) left = mid + 1;
            else if (nums[mid] > target) right = mid - 1;
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
