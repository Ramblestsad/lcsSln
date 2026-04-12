/*
 * @lc app=leetcode id=787 lang=csharp
 * @lcpr version=30402
 *
 * [787] Cheapest Flights Within K Stops
 */

namespace Scratch.Labuladong.Algorithms.CheapestFlightsWithinKStops;

// @lc code=start
public class Solution
{
    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
    {
        var graph = new List<int[]>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = [];
        }

        foreach (var edge in flights)
        {
            var from = edge[0];
            var to = edge[1];
            var price = edge[2];

            graph[from].Add([to, price]);
        }

        // 允许经过最多 K 个节点，就相当于允许经过最多 K + 1 条边，所以在执行 dijkstra 函数之前，需要将 K 转换为 K + 1。
        return Dijkstra(graph, src, dst, k + 1);
    }

    class State(int node, int distFromStart, int edgesFromStart)
    {
        public int node = node;
        public int distFromStart = distFromStart;
        public int edgesFromStart = edgesFromStart;
    }

    int Dijkstra(List<int[]>[] graph, int src, int dst, int k)
    {
        var distTo = new int[graph.Length][];
        for (int i = 0; i < graph.Length; i++)
        {
            // edgesFromStart 可能是 0（起点）到 k（最大边数）一共 k+1 种状态
            distTo[i] = new int[k + 1];
            Array.Fill(distTo[i], int.MaxValue);
        }

        var pq = new PriorityQueue<State, int>();
        pq.Enqueue(new State(src, 0, 0), 0);
        distTo[src][0] = 0;

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var curNode = s.node;
            var curDistFromStart = s.distFromStart;
            var curEdgesFromStart = s.edgesFromStart;

            if (distTo[curNode][curEdgesFromStart] < curDistFromStart) continue;

            if (curNode == dst) return distTo[dst][curEdgesFromStart];

            foreach (var e in graph[curNode])
            {
                var nextNode = e[0];
                var nextDistFromStart = curDistFromStart + e[1];
                var nextEdgesFromStart = curEdgesFromStart + 1;

                if (nextEdgesFromStart > k ||
                    distTo[nextNode][nextEdgesFromStart] <= nextDistFromStart)
                    continue;

                distTo[nextNode][nextEdgesFromStart] = nextDistFromStart;
                pq.Enqueue(new State(nextNode, nextDistFromStart, nextEdgesFromStart),
                           nextDistFromStart);
            }
        }

        return -1;
    }
}
// @lc code=end
