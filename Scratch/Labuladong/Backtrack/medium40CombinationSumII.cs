namespace Scratch.Labuladong.Algorithms.CombinationSumII;

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
