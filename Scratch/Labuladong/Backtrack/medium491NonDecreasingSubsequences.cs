namespace Scratch.Labuladong.Algorithms.NonDecreasingSubsequences;

// 491. Non-decreasing Subsequences (Medium)
//
// Given an integer array nums, return all the different possible non-decreasing subsequences of
// the given array with at least two elements. You may return the answer in any order.
//
// Example 1:
//
// Input: nums = [4,6,7,7]
// Output: [[4,6],[4,6,7],[4,6,7,7],[4,7],[4,7,7],[6,7],[6,7,7],[7,7]]
//
// Example 2:
//
// Input: nums = [4,4,3,2,1]
// Output: [[4,4]]
//
// Constraints:
//
// - 1 <= nums.length <= 15
//
// - -100 <= nums[i] <= 100
//
// Related Topics: Array, Hash Table, Backtracking, Bit Manipulation

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();
    public List<int> track = new();

    public IList<IList<int>> FindSubsequences(int[] nums)
    {
        if (nums.Length == 0) return res;

        _backtrack(nums, 0);

        return res;
    }

    void _backtrack(int[] nums, int start)
    {
        if (track.Count >= 2)
        {
            // 找到一个合法答案
            res.Add(new List<int>(track));
        }

        // 用哈希集合防止重复选择相同元素
        var used = new HashSet<int>();
        for (int i = start; i < nums.Length; i++)
        {
            // 保证集合中元素都是递增顺序
            if (track.Count > 0 && nums[i] < track[^1]) continue;
            // 保证不要重复使用相同的元素
            if (!used.Add(nums[i])) continue;

            // 做选择
            track.Add(nums[i]);
            _backtrack(nums, i + 1);
            // 撤销
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
