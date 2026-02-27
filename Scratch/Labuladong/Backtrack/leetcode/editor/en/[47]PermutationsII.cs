namespace Scratch.Labuladong.Algorithms.PermutationsII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    public bool[] used = null!;

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        // 先排序，让相同的元素靠在一起
        Array.Sort(nums);
        used = new bool[nums.Length];
        _backtrack(nums);
        return res;
    }

    void _backtrack(int[] nums)
    {
        if (track.Count == nums.Length)
        {
            res.Add(new List<int>(track));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i]) continue;

            // 新添加的剪枝逻辑，固定相同的元素在排列中的相对位置
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) continue;

            track.Add(nums[i]);
            used[i] = true;

            _backtrack(nums);

            used[i] = false;
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
