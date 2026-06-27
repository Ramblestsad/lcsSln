namespace Scratch.Labuladong.Algorithms.NextGreaterElementII;

// 503. Next Greater Element II (Medium)
//
// Given a circular integer array nums (i.e., the next element of nums[nums.length - 1] is
// nums[0]), return the next greater number for every element in nums.
//
// The next greater number of a number x is the first greater number to its traversing-order next
// in the array, which means you could search circularly to find its next greater number. If it
// doesn't exist, return -1 for this number.
//
// Example 1:
//
// Input: nums = [1,2,1]
// Output: [2,-1,2]
// Explanation: The first 1's next greater number is 2;
// The number 2 can't find next greater number.
// The second 1's next greater number needs to search circularly, which is also 2.
//
// Example 2:
//
// Input: nums = [1,2,3,4,3]
// Output: [2,3,4,-1,4]
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -10^9 <= nums[i] <= 10^9
//
// Related Topics: Array, Stack, Monotonic Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] NextGreaterElements(int[] nums)
    {
        var n = nums.Length;
        var res = new int[n];

        var s = new Stack<int>();

        // 数组长度加倍模拟环形数组
        for (int i = 2 * n - 1; i >= 0; i--)
        {
            // 索引 i 要求模
            while (s.Count != 0 && s.Peek() <= nums[i % n])
            {
                s.Pop();
            }

            res[i % n] = s.Count == 0 ? -1 : s.Peek();
            s.Push(nums[i % n]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
