namespace Scratch.Labuladong.Algorithms.NonDecreasingSubsequences;

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
