namespace Scratch.Labuladong.Algorithms.CombinationSumII;

// 40. Combination Sum II (Medium)
//
// Given a collection of candidate numbers (candidates) and a target number (target), find all
// unique combinations in candidates where the candidate numbers sum to target.
//
// Each number in candidates may only be used once in the combination.
//
// Note: The solution set must not contain duplicate combinations.
//
// Example 1:
//
// Input: candidates = [10,1,2,7,6,1,5], target = 8
// Output:
// [
// [1,1,6],
// [1,2,5],
// [1,7],
// [2,6]
// ]
//
// Example 2:
//
// Input: candidates = [2,5,2,1,2], target = 5
// Output:
// [
// [1,2,2],
// [5]
// ]
//
// Constraints:
//
// - 1 <= candidates.length <= 100
//
// - 1 <= candidates[i] <= 50
//
// - 1 <= target <= 30
//
// Related Topics: Array, Backtracking

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    public long trackSum = 0;

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        if (candidates.Length == 0) return res;

        // 先排序，让相同的元素靠在一起
        Array.Sort(candidates);
        _backtrack(candidates, 0, target);
        return res;
    }

    void _backtrack(int[] nums, int start, int target)
    {
        // base case，达到目标和，找到符合条件的组合
        if (trackSum == target)
        {
            res.Add(new List<int>(track));
            return;
        }

        // base case，超过目标和，直接结束
        if (trackSum > target) return;

        for (int i = start; i < nums.Length; i++)
        {
            // 剪枝逻辑，值相同的树枝，只遍历第一条
            if (i > start && nums[i] == nums[i - 1]) continue;

            track.Add(nums[i]);
            trackSum += nums[i];

            _backtrack(nums, i + 1, target);

            trackSum -= nums[i];
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
