namespace Scratch.Labuladong.Algorithms.Combinations;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new List<IList<int>>();
    public List<int> track = new List<int>();

    public IList<IList<int>> Combine(int n, int k)
    {
        _backtrack(1, n, k);
        return res;
    }

    void _backtrack(int start, int n, int k)
    {
        // base case
        if (k == track.Count)
        {
            // 遍历到了第 k 层，收集当前节点的值
            res.Add(new List<int>(track));
            return;
        }

        for (int i = start; i <= n; i++)
        {
            // 选择
            track.Add(i);
            // 通过 start 参数控制树枝的遍历，避免产生重复的子集
            _backtrack(i + 1, n, k);
            //撤销选择
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
