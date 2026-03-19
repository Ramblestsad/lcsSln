namespace Scratch.Labuladong.Algorithms.FindTheDuplicateNumber;

//Given an array of integers nums containing n + 1 integers where each integer
//is in the range [1, n] inclusive.
//
// There is only one repeated number in nums, return this repeated number.
//
// You must solve the problem without modifying the array nums and using only
//constant extra space.
//
//
// Example 1:
//
//
//Input: nums = [1,3,4,2,2]
//Output: 2
//
//
// Example 2:
//
//
//Input: nums = [3,1,3,4,2]
//Output: 3
//
//
// Example 3:
//
//
//Input: nums = [3,3,3,3,3]
//Output: 3
//
//
// Constraints:
//
//
// 1 <= n <= 10⁵
// nums.length == n + 1
// 1 <= nums[i] <= n
// All the integers in nums appear only once except for precisely one integer
//which appears two or more times.
//
//
//
// Follow up:
//
//
// How can we prove that at least one duplicate number must exist in nums?
// Can you solve the problem in linear runtime complexity?
//
//
// Related TopicsArray | Two Pointers | Binary Search | Bit Manipulation
//
// 👍 25413, 👎 5783bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int FindDuplicate(int[] nums)
    {
        // n + 1 elements
        // [1, n]

        var fast = 0;
        var slow = 0;

        while (true)
        {
            fast = nums[nums[fast]];
            slow = nums[slow];
            if (fast == slow) break;
        }

        slow = 0;
        // 快慢指针同步前进，相交点就是环入口，即重复数字
        while (slow != fast)
        {
            fast = nums[fast];
            slow = nums[slow];
        }

        return slow;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
