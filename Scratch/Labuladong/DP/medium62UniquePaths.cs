namespace Scratch.Labuladong.Algorithms.UniquePaths;

// 62. Unique Paths (Medium)
//
// There is a robot on an m x n grid. The robot is initially located at the top-left corner (i.e.,
// grid[0][0]). The robot tries to move to the bottom-right corner (i.e., grid[m - 1][n - 1]). The
// robot can only move either down or right at any point in time.
//
// Given the two integers m and n, return the number of possible unique paths that the robot can
// take to reach the bottom-right corner.
//
// The test cases are generated so that the answer will be less than or equal to 2 * 10^9.
//
// Example 1:
//
// Input: m = 3, n = 7
// Output: 28
//
// Example 2:
//
// Input: m = 3, n = 2
// Output: 3
// Explanation: From the top-left corner, there are a total of 3 ways to reach the bottom-right
// corner:
// 1. Right -> Down -> Down
// 2. Down -> Down -> Right
// 3. Down -> Right -> Down
//
// Constraints:
//
// - 1 <= m, n <= 100
//
// Related Topics: Math, Dynamic Programming, Combinatorics

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
