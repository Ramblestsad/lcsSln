/*
 * @lc app=leetcode id=62 lang=csharp
 * @lcpr version=30403
 *
 * [62] Unique Paths
 */

namespace Scratch.Labuladong.Algorithms.UniquePaths;

// @lc code=start
public class Solution
{
    private int[][] memo = [];

    public int UniquePaths(int m, int n)
    {
        memo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
        }

        return dp(m - 1, n - 1);
    }

    // 定义：从 (0, 0) 到 (x, y) 有 dp(x, y) 条路径
    int dp(int x, int y)
    {
        // base case
        if (x == 0 && y == 0)
        {
            return 1;
        }

        if (x < 0 || y < 0)
        {
            return 0;
        }

        // 避免冗余计算
        if (memo[x][y] > 0)
        {
            return memo[x][y];
        }

        // 状态转移方程：
        // 到达 (x, y) 的路径数等于到达 (x - 1, y) 和 (x, y - 1) 路径数之和
        memo[x][y] = dp(x - 1, y) + dp(x, y - 1);
        return memo[x][y];
    }
}
// @lc code=end

/*
// @lcpr case=start
// 3\n7\n
// @lcpr case=end

// @lcpr case=start
// 3\n2\n
// @lcpr case=end
 */
