namespace Scratch.Labuladong.Algorithms.PathWithMinimumEffort;

// 1631. Path With Minimum Effort (Medium)
//
// You are a hiker preparing for an upcoming hike. You are given heights, a 2D array of size rows x
// columns, where heights[row][col] represents the height of cell (row, col). You are situated in
// the top-left cell, (0, 0), and you hope to travel to the bottom-right cell, (rows-1, columns-1)
// (i.e., 0-indexed). You can move up, down, left, or right, and you wish to find a route that
// requires the minimum effort.
//
// A route's effort is the maximum absolute difference in heights between two consecutive cells of
// the route.
//
// Return the minimum effort required to travel from the top-left cell to the bottom-right cell.
//
// Example 1:
//
// Input: heights = [[1,2,2],[3,8,2],[5,3,5]]
// Output: 2
// Explanation: The route of [1,3,5,3,5] has a maximum absolute difference of 2 in consecutive
// cells.
// This is better than the route of [1,2,2,2,5], where the maximum absolute difference is 3.
//
// Example 2:
//
// Input: heights = [[1,2,3],[3,8,4],[5,3,5]]
// Output: 1
// Explanation: The route of [1,2,3,4,5] has a maximum absolute difference of 1 in consecutive
// cells, which is better than route [1,3,5,3,5].
//
// Example 3:
//
// Input: heights = [[1,2,1,1,1],[1,2,1,2,1],[1,2,1,2,1],[1,2,1,2,1],[1,1,1,2,1]]
// Output: 0
// Explanation: This route does not require any effort.
//
// Constraints:
//
// - rows == heights.length
//
// - columns == heights[i].length
//
// - 1 <= rows, columns <= 100
//
// - 1 <= heights[i][j] <= 10^6
//
// Related Topics: Array, Binary Search, Depth-First Search, Breadth-First Search, Union-Find, Heap (Priority Queue), Matrix

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
