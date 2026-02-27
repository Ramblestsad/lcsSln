namespace Scratch.Labuladong.Algorithms.CombinationSum;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    private int trackSum = 0;

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
