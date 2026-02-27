namespace Scratch.Labuladong.Algorithms.SubsetsII;

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
