namespace Scratch.Labuladong.Algorithms.MinimumCostToMakeAtLeastOneValidPathInAGrid;

// 1368. Minimum Cost to Make at Least One Valid Path in a Grid (Hard)
//
// Given an m x n grid. Each cell of the grid has a sign pointing to the next cell you should visit
// if you are currently in this cell. The sign of grid[i][j] can be:
//
// - 1 which means go to the cell to the right. (i.e go from grid[i][j] to grid[i][j + 1])
//
// - 2 which means go to the cell to the left. (i.e go from grid[i][j] to grid[i][j - 1])
//
// - 3 which means go to the lower cell. (i.e go from grid[i][j] to grid[i + 1][j])
//
// - 4 which means go to the upper cell. (i.e go from grid[i][j] to grid[i - 1][j])
//
// Notice that there could be some signs on the cells of the grid that point outside the grid.
//
// You will initially start at the upper left cell (0, 0). A valid path in the grid is a path that
// starts from the upper left cell (0, 0) and ends at the bottom-right cell (m - 1, n - 1)
// following the signs on the grid. The valid path does not have to be the shortest.
//
// You can modify the sign on a cell with cost = 1. You can modify the sign on a cell one time
// only.
//
// Return the minimum cost to make the grid have at least one valid path.
//
// Example 1:
//
// Input: grid = [[1,1,1,1],[2,2,2,2],[1,1,1,1],[2,2,2,2]]
// Output: 3
// Explanation: You will start at point (0, 0).
// The path to (3, 3) is as follows. (0, 0) --> (0, 1) --> (0, 2) --> (0, 3) change the arrow to
// down with cost = 1 --> (1, 3) --> (1, 2) --> (1, 1) --> (1, 0) change the arrow to down with
// cost = 1 --> (2, 0) --> (2, 1) --> (2, 2) --> (2, 3) change the arrow to down with cost = 1 -->
// (3, 3)
// The total cost = 3.
//
// Example 2:
//
// Input: grid = [[1,1,3],[3,2,2],[1,1,4]]
// Output: 0
// Explanation: You can follow the path from (0, 0) to (2, 2).
//
// Example 3:
//
// Input: grid = [[1,2],[4,3]]
// Output: 1
//
// Constraints:
//
// - m == grid.length
//
// - n == grid[i].length
//
// - 1 <= m, n <= 100
//
// - 1 <= grid[i][j] <= 4
//
// Related Topics: Array, Breadth-First Search, Graph Theory, Heap (Priority Queue), Matrix, Shortest Path

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinCost(int[][] grid)
    {
        return Dijkstra(grid);
    }

    class State(int x, int y, int costFromStart)
    {
        public int x = x, y = y;
        public int costFromStart = costFromStart;
    }

    // 获取x, y需要加的差值
    static int[] _getDelta(int directionId)
    {
        // 1 -> right, 2 -> left, 3 -> down, 4 -> up
        if (directionId == 1)
        {
            return [0, 1];
        }

        if (directionId == 2)
        {
            return [0, -1];
        }

        if (directionId == 3)
        {
            return [1, 0];
        }

        return [-1, 0];
    }

    static int Dijkstra(int[][] graph)
    {
        var m = graph.Length;
        var n = graph[0].Length;

        var distTo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            distTo[i] = new int[n];
            Array.Fill(distTo[i], int.MaxValue);
        }

        var pq = new PriorityQueue<State, int>();
        pq.Enqueue(new State(0, 0, 0), 0);
        distTo[0][0] = 0;

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var curX = s.x;
            var curY = s.y;
            var curCostFromStart = s.costFromStart;

            // 已经存在更优路径，则跳过
            if (distTo[curX][curY] < curCostFromStart) continue;

            // 判断是否已经到达目标点
            if (curX == m - 1 && curY == n - 1) return distTo[curX][curY];

            for (int directionId = 1; directionId <= 4; directionId++)
            {
                var delta = _getDelta(directionId);
                var nextX = curX + delta[0];
                var nextY = curY + delta[1];
                if (nextX < 0 || nextX >= m || nextY < 0 || nextY >= n) continue;

                // 如果当前方向和目标方向不一致，则需要花费 1 的代价
                var nextCostFromStart = curCostFromStart;
                if (graph[curX][curY] != directionId) nextCostFromStart += 1;

                // 已经存在更优路径，则跳过
                if (distTo[nextX][nextY] <= nextCostFromStart) continue;

                pq.Enqueue(new State(nextX, nextY, nextCostFromStart), nextCostFromStart);
                distTo[nextX][nextY] = nextCostFromStart;
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
