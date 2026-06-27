namespace Scratch.Labuladong.Algorithms.Combinations;

// 77. Combinations (Medium)
//
// Given two integers n and k, return all possible combinations of k numbers chosen from the range
// [1, n].
//
// You may return the answer in any order.
//
// Example 1:
//
// Input: n = 4, k = 2
// Output: [[1,2],[1,3],[1,4],[2,3],[2,4],[3,4]]
// Explanation: There are 4 choose 2 = 6 total combinations.
// Note that combinations are unordered, i.e., [1,2] and [2,1] are considered to be the same
// combination.
//
// Example 2:
//
// Input: n = 1, k = 1
// Output: [[1]]
// Explanation: There is 1 choose 1 = 1 total combination.
//
// Constraints:
//
// - 1 <= n <= 20
//
// - 1 <= k <= n
//
// Related Topics: Backtracking

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
