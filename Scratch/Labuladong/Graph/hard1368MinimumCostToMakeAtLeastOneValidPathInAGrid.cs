/*
 * @lc app=leetcode id=1368 lang=csharp
 * @lcpr version=30402
 *
 * [1368] Minimum Cost To Make At Least One Valid Path In A Grid
 */

namespace Scratch.Labuladong.Algorithms.MinimumCostToMakeAtLeastOneValidPathInAGrid;

// @lc code=start
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
// @lc code=end
