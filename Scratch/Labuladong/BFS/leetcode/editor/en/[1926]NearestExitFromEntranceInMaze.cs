namespace Scratch.Labuladong.Algorithms.NearestExitFromEntranceInMaze;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int NearestExit(char[][] maze, int[] entrance)
    {
        var m = maze.Length;
        var n = maze[0].Length;
        int[][] dirs = [[0, 1], [0, -1], [1, 0], [-1, 0]];

        var q = new Queue<int[]>();
        var visited = new bool[m][];
        for (int i = 0; i < m; i++)
        {
            visited[i] = new bool[n];
        }

        q.Enqueue(entrance);
        visited[entrance[0]][entrance[1]] = true;

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            step++;
            for (int i = 0; i < sz; i++)
            {
                var cur = q.Dequeue();
                // 每个节点都会尝试向上下左右四个方向扩展一步
                foreach (var dir in dirs)
                {
                    var x = cur[0] + dir[0];
                    var y = cur[1] + dir[1];
                    if (x < 0 || x >= m || y < 0 || y >= n || visited[x][y] || maze[x][y] == '+') continue;
                    // 走到出口了
                    if (x == 0 || x == m - 1 || y == 0 || y == n - 1) return step;

                    q.Enqueue([x, y]);
                    visited[x][y] = true;
                }
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
