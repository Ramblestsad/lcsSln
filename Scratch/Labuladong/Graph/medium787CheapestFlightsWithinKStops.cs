namespace Scratch.Labuladong.Algorithms.CheapestFlightsWithinKStops;

// 787. Cheapest Flights Within K Stops (Medium)
//
// There are n cities connected by some number of flights. You are given an array flights where
// flights[i] = [from_i, to_i, price_i] indicates that there is a flight from city from_i to city
// to_i with cost price_i.
//
// You are also given three integers src, dst, and k, return the cheapest price from src to dst
// with at most k stops. If there is no such route, return -1.
//
// Example 1:
//
// Input: n = 4, flights = [[0,1,100],[1,2,100],[2,0,100],[1,3,600],[2,3,200]], src = 0, dst = 3, k
// = 1
// Output: 700
// Explanation:
// The graph is shown above.
// The optimal path with at most 1 stop from city 0 to 3 is marked in red and has cost 100 + 600 =
// 700.
// Note that the path through cities [0,1,2,3] is cheaper but is invalid because it uses 2 stops.
//
// Example 2:
//
// Input: n = 3, flights = [[0,1,100],[1,2,100],[0,2,500]], src = 0, dst = 2, k = 1
// Output: 200
// Explanation:
// The graph is shown above.
// The optimal path with at most 1 stop from city 0 to 2 is marked in red and has cost 100 + 100 =
// 200.
//
// Example 3:
//
// Input: n = 3, flights = [[0,1,100],[1,2,100],[0,2,500]], src = 0, dst = 2, k = 0
// Output: 500
// Explanation:
// The graph is shown above.
// The optimal path with no stops from city 0 to 2 is marked in red and has cost 500.
//
// Constraints:
//
// - 2 <= n <= 100
//
// - 0 <= flights.length <= (n * (n - 1) / 2)
//
// - flights[i].length == 3
//
// - 0 <= from_i, to_i < n
//
// - from_i != to_i
//
// - 1 <= price_i <= 10^4
//
// - There will not be any multiple flights between two cities.
//
// - 0 <= src, dst, k < n
//
// - src != dst
//
// Related Topics: Dynamic Programming, Depth-First Search, Breadth-First Search, Graph Theory, Heap (Priority Queue), Shortest Path

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
