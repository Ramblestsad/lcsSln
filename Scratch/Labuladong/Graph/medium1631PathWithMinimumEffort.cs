namespace Scratch.Labuladong.Algorithms.PathWithMinimumEffort;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinimumEffortPath(int[][] heights)
    {
        return Dijkstra(heights);
    }

    class State
    {
        public int row;
        public int col;
        public int effortFromStart;

        public State(int row, int col, int effortFromStart)
        {
            this.row = row;
            this.col = col;
            this.effortFromStart = effortFromStart;
        }
    }

    int Dijkstra(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        // 记录从起点 (0, 0) 到每个节点的最小体力消耗
        var distTo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            distTo[i] = new int[n];
            Array.Fill(distTo[i], int.MaxValue);
        }

        var pq = new PriorityQueue<State, int>();

        // 从起点 (0, 0) 开始进行 dijkstra 算法
        pq.Enqueue(new State(0, 0, 0), 0);
        distTo[0][0] = 0;

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var curRow = s.row;
            var curCol = s.col;
            var curEffortFromStart = s.effortFromStart;

            // 已经存在更优路径，则跳过
            if (distTo[curRow][curCol] < curEffortFromStart) continue;

            // 判断是否已经到达目标点
            if (curRow == m - 1 && curCol == n - 1) return distTo[curRow][curCol];

            foreach (var neighbor in _adj(matrix, curRow, curCol))
            {
                var nextRow = neighbor[0];
                var nextCol = neighbor[1];
                var nextEffortFromStart = Math.Max(curEffortFromStart,
                                                   Math.Abs(matrix[curRow][curCol] - matrix[nextRow][nextCol]));

                // 已经存在更优路径，则跳过
                if (distTo[nextRow][nextCol] <= nextEffortFromStart) continue;

                pq.Enqueue(new State(nextRow, nextCol, nextEffortFromStart), nextEffortFromStart);
                distTo[nextRow][nextCol] = nextEffortFromStart;
            }
        }

        return -1;
    }

    // 返回坐标 (x, y) 的上下左右相邻坐标
    List<int[]> _adj(int[][] matrix, int x, int y)
    {
        var dirs = new int[][] { [0, 1], [0, -1], [1, 0], [-1, 0] };
        var m = matrix.Length;
        var n = matrix[0].Length;

        // 存储相邻节点
        var neighbors = new List<int[]>();
        foreach (var dir in dirs)
        {
            var nx = x + dir[0];
            var ny = y + dir[1];

            // 索引越界
            if (nx >= m || nx < 0 || ny >= n || ny < 0) continue;

            neighbors.Add([nx, ny]);
        }

        return neighbors;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
