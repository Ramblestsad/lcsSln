namespace Scratch.Labuladong.Algorithms.LongestConsecutiveSeq;

// 128. Longest Consecutive Sequence (Medium)
//
// Given an unsorted array of integers nums, return the length of the longest consecutive elements
// sequence.
//
// You must write an algorithm that runs in O(n) time.
//
// Example 1:
//
// Input: nums = [100,4,200,1,3,2]
// Output: 4
// Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is
// 4.
//
// Example 2:
//
// Input: nums = [0,3,7,2,5,8,4,6,0,1]
// Output: 9
//
// Example 3:
//
// Input: nums = [1,0,1,2]
// Output: 3
//
// Constraints:
//
// - 0 <= nums.length <= 10^5
//
// - -10^9 <= nums[i] <= 10^9
//
// Related Topics: Array, Hash Table, Union-Find

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        var set = new HashSet<int>();

        foreach (var num in nums)
        {
            set.Add(num);
        }

        var res = 0;

        foreach (var num in set)
        {
            // num 不是子序列的第一个（最小）
            if (set.Contains(num - 1)) continue;

            // num 是子序列的第一个
            var curNum = num;
            var curLen = 1;

            while (set.Contains(curNum + 1))
            {
                curNum += 1;
                curLen += 1;
            }

            // update res
            res = Math.Max(res, curLen);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
