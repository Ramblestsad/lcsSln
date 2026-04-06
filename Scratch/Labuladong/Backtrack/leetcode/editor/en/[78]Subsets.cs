/*
 * @lc app=leetcode id=78 lang=csharp
 * @lcpr version=30402
 *
 * [78] Subsets
 */

namespace Scratch.Labuladong.Algorithms.Subsets;

// @lc code=start
public class Solution
{
    public List<IList<int>> res = new List<IList<int>>();

    // 记录回溯算法的递归路径
    private List<int> track = new List<int>();

    public IList<IList<int>> Subsets(int[] nums)
    {
        _backtrack(nums, 0);

        return res;
    }

    // 回溯算法核心函数，遍历子集问题的回溯树
    void _backtrack(int[] nums, int start)
    {
        // 当 start == nums.Length 时，叶子节点的值会被装入 res，但 for 循环不会执行，也就结束了递归。
        // 前序位置，每个节点的值都是一个子集
        res.Add(new List<int>(track));

        for (int i = start; i < nums.Length; i++)
        {
            // 做选择
            track.Add(nums[i]);
            // 通过 start 参数控制树枝的遍历，避免产生重复的子集
            _backtrack(nums, i + 1);
            // 撤销选择
            track.RemoveAt(track.Count - 1);
        }
    }
}
// @lc code=end
