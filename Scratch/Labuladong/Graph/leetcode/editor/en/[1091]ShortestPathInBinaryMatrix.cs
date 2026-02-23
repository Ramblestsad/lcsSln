namespace Scratch.Labuladong.Algorithms.ShortestPathInBinaryMatrix;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 8 个方向
    public int[][] dirs = [[-1, -1], [-1, 0], [-1, 1], [0, -1], [0, 1], [1, -1], [1, 0], [1, 1]];

    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        var n = grid.Length;
        // 起点或终点被堵住
        if (grid[0][0] == 1 || grid[n - 1][n - 1] == 1) return -1;
        if (n == 1) return 1;

        // distTo[i][j] 记录从起点到 (i, j) 的最短距离
        var distTo = new int[n][];
        for (int i = 0; i < n; i++)
        {
            distTo[i] = new int[n];
            Array.Fill(distTo[i], int.MaxValue);
        }

        distTo[0][0] = 1;
        // 优先级队列，按 f = g + h 排序
        // 数组含义：{x, y, g, f}
        var pq = new PriorityQueue<int[], int>();
        var f = 1 + _heuristicFn(0, 0, n);
        pq.Enqueue([0, 0, 1, f], f);

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var x = s[0];
            var y = s[1];
            var g = s[2];

            // 到达终点
            if (x == n - 1 && y == n - 1) return g;

            // 剪枝
            if (distTo[x][y] < g) continue;

            // 遍历 8 个方向
            foreach (var dir in dirs)
            {
                var nx = x + dir[0];
                var ny = y + dir[1];
                // 边界检查和障碍物检查
                if (nx < 0 || nx >= n || ny < 0 || ny >= n || grid[nx][ny] == 1) continue;

                var nextG = g + 1;
                if (distTo[nx][ny] <= nextG) continue;

                distTo[nx][ny] = nextG;
                var nf = nextG + _heuristicFn(nx, ny, n);
                pq.Enqueue([nx, ny, nextG, nf], nf);
            }
        }

        return -1;
    }

    // 启发函数：切比雪夫距离
    int _heuristicFn(int x, int y, int n)
    {
        return Math.Max(Math.Abs(x - ( n - 1 )), Math.Abs(y - ( n - 1 )));
    }
}
//leetcode submit region end(Prohibit modification and deletion)
