namespace Scratch.Labuladong.Algorithms.Permutations;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    // track 中的元素会被标记为 true
    public bool[] used = null!;

    public IList<IList<int>> Permute(int[] nums)
    {
        used = new bool[nums.Length];
        _backtrack(nums);
        return res;
    }

    void _backtrack(int[] nums)
    {
        // base case，到达叶子节点
        if (track.Count == nums.Length)
        {
            res.Add(new List<int>(track));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // 已经存在 track 中的元素，不能重复选择
            if (used[i]) continue;

            // 做选择
            used[i] = true;
            track.Add(nums[i]);
            // 进入下一层
            _backtrack(nums);
            // 撤销选择
            track.RemoveAt(track.Count - 1);
            used[i] = false;
        }
    }

    void _K_backtrack(int[] nums, int k)
    {
        // base case，到达第 k 层，收集节点的值
        if (track.Count == k)
        {
            // 第 k 层节点的值就是大小为 k 的排列
            res.Add(new List<int>(track));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // 已经存在 track 中的元素，不能重复选择
            if (used[i]) continue;

            // 做选择
            used[i] = true;
            track.Add(nums[i]);
            // 进入下一层
            _backtrack(nums);
            // 撤销选择
            track.RemoveAt(track.Count - 1);
            used[i] = false;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
