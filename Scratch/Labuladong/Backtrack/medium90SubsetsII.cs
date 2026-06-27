namespace Scratch.Labuladong.Algorithms.SubsetsII;

// 90. Subsets II (Medium)
//
// Given an integer array nums that may contain duplicates, return all possible subsets (the power
// set).
//
// The solution set must not contain duplicate subsets. Return the solution in any order.
//
// Example 1:
//
// Input: nums = [1,2,2]
// Output: [[],[1],[1,2],[1,2,2],[2],[2,2]]
// Example 2:
//
// Input: nums = [0]
// Output: [[],[0]]
//
// Constraints:
//
// - 1 <= nums.length <= 10
//
// - -10 <= nums[i] <= 10
//
// Related Topics: Array, Backtracking, Bit Manipulation

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        // 需要先进行排序，让相同的元素靠在一起，如果发现 nums[i] == nums[i-1]，则跳过
        Array.Sort(nums);
        _backtrack(nums, 0);
        return res;
    }

    void _backtrack(int[] nums, int start)
    {
        res.Add(new List<int>(track));

        for (int i = start; i < nums.Length; i++)
        {
            // 剪枝逻辑，值相同的相邻树枝，只遍历第一条
            if (i > start && nums[i] == nums[i - 1]) continue;

            track.Add(nums[i]);
            _backtrack(nums, i + 1);
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
