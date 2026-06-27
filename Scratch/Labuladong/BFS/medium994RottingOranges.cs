namespace Scratch.Labuladong.Algorithms.RottingOranges;

//You are given an m x n grid where each cell can have one of three values:
//
//
// 0 representing an empty cell,
// 1 representing a fresh orange, or
// 2 representing a rotten orange.
//
//
// Every minute, any fresh orange that is 4-directionally adjacent to a rotten
//orange becomes rotten.
//
// Return the minimum number of minutes that must elapse until no cell has a
//fresh orange. If this is impossible, return -1.
//
//
// Example 1:
//
//
//Input: grid = [[2,1,1],[1,1,0],[0,1,1]]
//Output: 4
//
//
// Example 2:
//
//
//Input: grid = [[2,1,1],[0,1,1],[1,0,1]]
//Output: -1
//Explanation: The orange in the bottom left corner (row 2, column 0) is never
//rotten, because rotting only happens 4-directionally.
//
//
// Example 3:
//
//
//Input: grid = [[0,2]]
//Output: 0
//Explanation: Since there are already no fresh oranges at minute 0, the answer
//is just 0.
//
//
//
// Constraints:
//
//
// m == grid.length
// n == grid[i].length
// 1 <= m, n <= 10
// grid[i][j] is 0, 1, or 2.
//
//
// Related TopicsArray | Breadth-First Search | Matrix
//
// 👍 14994, 👎 472bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//
//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int OrangesRotting(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        var q = new Queue<int[]>();
        // 把所有腐烂的橘子加入队列，作为 BFS 的起点
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 2) q.Enqueue([i, j]);
            }
        }

        int[][] dirs = [[-1, 0], [1, 0], [0, -1], [0, 1]];

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++)
            {
                var cur = q.Dequeue();
                foreach (var dir in dirs)
                {
                    var x = cur[0] + dir[0];
                    var y = cur[1] + dir[1];
                    if (x >= 0 && x < m && y >= 0 && y < n && grid[x][y] == 1)
                    {
                        q.Enqueue([x, y]);
                        grid[x][y] = 2;
                    }
                }
            }

            step++;
        }

        // 检查是否还有新鲜橘子
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 有新鲜橘子，返回 -1
                if (grid[i][j] == 1) return -1;
            }
        }

        // 注意，BFS 扩散的步数需要减一才是最终结果。
        // 因为 step++ 放在了 for 循环之后，而最后一轮 BFS 会多算一次。
        return step == 0 ? 0 : step - 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
