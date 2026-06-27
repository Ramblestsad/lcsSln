namespace Scratch.Labuladong.Algorithms.CombinationSum;

// 39. Combination Sum (Medium)
//
// Given an array of distinct integers candidates and a target integer target, return a list of all
// unique combinations of candidates where the chosen numbers sum to target. You may return the
// combinations in any order.
//
// The same number may be chosen from candidates an unlimited number of times. Two combinations are
// unique if the frequency of at least one of the chosen numbers is different.
//
// The test cases are generated such that the number of unique combinations that sum up to target
// is less than 150 combinations for the given input.
//
// Example 1:
//
// Input: candidates = [2,3,6,7], target = 7
// Output: [[2,2,3],[7]]
// Explanation:
// 2 and 3 are candidates, and 2 + 2 + 3 = 7. Note that 2 can be used multiple times.
// 7 is a candidate, and 7 = 7.
// These are the only two combinations.
//
// Example 2:
//
// Input: candidates = [2,3,5], target = 8
// Output: [[2,2,2,2],[2,3,3],[3,5]]
//
// Example 3:
//
// Input: candidates = [2], target = 1
// Output: []
//
// Constraints:
//
// - 1 <= candidates.length <= 30
//
// - 2 <= candidates[i] <= 40
//
// - All elements of candidates are distinct.
//
// - 1 <= target <= 40
//
// Related Topics: Array, Backtracking

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = [];

    private int trackSum;

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        if (candidates.Length == 0) return res;

        _backtrack(candidates, 0, target);
        return res;
    }

    void _backtrack(int[] nums, int start, int target)
    {
        // base case，找到目标和，记录结果
        if (trackSum == target)
        {
            res.Add(new List<int>(track));
            return;
        }

        // base case，超过目标和，停止向下遍历
        if (trackSum > target) return;

        for (int i = start; i < nums.Length; i++)
        {
            track.Add(nums[i]);
            trackSum += nums[i];

            // 重点:
            // 这个 i 从 start 开始，那么下一层回溯树就是从 start + 1 开始，从而保证 nums[start] 这个元素不会被重复使用
            // 那么反过来，如果想让每个元素被重复使用，只要把 i + 1 改成 i 即可
            _backtrack(nums, i, target);

            trackSum -= nums[i];
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
