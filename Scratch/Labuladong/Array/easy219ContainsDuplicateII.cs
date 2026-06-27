namespace Scratch.Labuladong.Algorithms.ContainsDuplicateII;

// 219. Contains Duplicate II (Easy)
//
// Given an integer array nums and an integer k, return true if there are two distinct indices i
// and j in the array such that nums[i] == nums[j] and abs(i - j) <= k.
//
// Example 1:
//
// Input: nums = [1,2,3,1], k = 3
// Output: true
//
// Example 2:
//
// Input: nums = [1,0,1,1], k = 1
// Output: true
//
// Example 3:
//
// Input: nums = [1,2,3,1,2,3], k = 2
// Output: false
//
// Constraints:
//
// - 1 <= nums.length <= 10^5
//
// - -10^9 <= nums[i] <= 10^9
//
// - 0 <= k <= 10^5
//
// Related Topics: Array, Hash Table, Sliding Window

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        /*
         * 1、什么时候应该扩大窗口？
         * 2、什么时候应该缩小窗口？
         * 3、什么时候得到一个合法的答案？
         * 本题很简单直接，以上三个问题的答案是：
         * 1、当窗口大小小于 k 时，扩大窗口。
         * 2、当窗口大小大于 k 时，缩小窗口。
         * 3、当窗口大小等于 k 且发现窗口中存在重复元素时，返回 true。
         */

        int left = 0, right = 0;
        var window = new HashSet<int>();

        while (right < nums.Length)
        {
            if (window.Contains(nums[right])) return true;
            window.Add(nums[right]);
            right++;

            if (right - left > k)
            {
                window.Remove(nums[left]);
                left++;
            }
        }

        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
