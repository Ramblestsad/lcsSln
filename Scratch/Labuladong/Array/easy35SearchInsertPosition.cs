namespace Scratch.Labuladong.Algorithms.SeatchInsertPos;

// 35. Search Insert Position (Easy)
//
// Given a sorted array of distinct integers and a target value, return the index if the target is
// found. If not, return the index where it would be if it were inserted in order.
//
// You must write an algorithm with O(log n) runtime complexity.
//
// Example 1:
//
// Input: nums = [1,3,5,6], target = 5
// Output: 2
//
// Example 2:
//
// Input: nums = [1,3,5,6], target = 2
// Output: 1
//
// Example 3:
//
// Input: nums = [1,3,5,6], target = 7
// Output: 4
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -10^4 <= nums[i] <= 10^4
//
// - nums contains distinct values sorted in ascending order.
//
// - -10^4 <= target <= 10^4
//
// Related Topics: Array, Binary Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        var i = 0;
        var j = nums.Length;

        while (i < j)
        {
            var mid = i + ( j - i ) / 2;
            if (nums[mid] < target)
            {
                i = mid + 1;
            }
            else if (nums[mid] > target)
            {
                j = mid;
            }
            else
            {
                j = mid;
            }
        }

        return i;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
