namespace Scratch.Labuladong.Algorithms.PathWithMaximumProbability;

// 1514. Path with Maximum Probability (Medium)
//
// You are given an undirected weighted graph of n nodes (0-indexed), represented by an edge list
// where edges[i] = [a, b] is an undirected edge connecting the nodes a and b with a probability of
// success of traversing that edge succProb[i].
//
// Given two nodes start and end, find the path with the maximum probability of success to go from
// start to end and return its success probability.
//
// If there is no path from start to end, return 0. Your answer will be accepted if it differs from
// the correct answer by at most 1e-5.
//
// Example 1:
//
// Input: n = 3, edges = [[0,1],[1,2],[0,2]], succProb = [0.5,0.5,0.2], start = 0, end = 2
// Output: 0.25000
// Explanation: There are two paths from start to end, one having a probability of success = 0.2
// and the other has 0.5 * 0.5 = 0.25.
//
// Example 2:
//
// Input: n = 3, edges = [[0,1],[1,2],[0,2]], succProb = [0.5,0.5,0.3], start = 0, end = 2
// Output: 0.30000
//
// Example 3:
//
// Input: n = 3, edges = [[0,1]], succProb = [0.5], start = 0, end = 2
// Output: 0.00000
// Explanation: There is no path between 0 and 2.
//
// Constraints:
//
// - 2 <= n <= 10^4
//
// - 0 <= start, end < n
//
// - start != end
//
// - 0 <= a, b < n
//
// - a != b
//
// - 0 <= succProb.length == edges.length <= 2*10^4
//
// - 0 <= succProb[i] <= 1
//
// - There is at most one edge between every two nodes.
//
// Related Topics: Array, Graph Theory, Heap (Priority Queue), Shortest Path

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public double MaxProbability(int n, int[][] edges, double[] succProb, int start_node, int end_node)
    {
        var graph = new List<double[]>[n];
        for (var i = 0; i < n; i++)
        {
            graph[i] = new List<double[]>();
        }

        // 构造无向图
        for (var i = 0; i < edges.Length; i++)
        {
            var from = edges[i][0];
            var to = edges[i][1];
            var weight = succProb[i];

            // 无向图其实就是双向图
            graph[from].Add([to, weight]);
            graph[to].Add([from, weight]);
        }

        var res = Dijkstra(graph, start_node, end_node);

        return Math.Abs(res - ( -1 )) < 0.001 ? 0 : res;
    }

    class State
    {
        // 当前节点 ID
        public int node;

        // 从起点 s 到当前 node 节点的最小路径权重和
        public double probFromStart;

        public State(int node, double probFromStart)
        {
            this.node = node;
            this.probFromStart = probFromStart;
        }
    }

    public static double Dijkstra(List<double[]>[] graph, int src, int dst)
    {
        var probTo = new double[graph.Length];
        // 初始化为最小值
        Array.Fill(probTo, 0.0);

        var pq = new PriorityQueue<State, double>(
            Comparer<double>.Create((a, b) => b.CompareTo(a)));

        pq.Enqueue(new State(src, 1.0), 1.0);
        probTo[src] = 1.0;

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var curNode = s.node;
            var curProbFromStart = s.probFromStart;

            // 已经存在更优路径，则跳过
            // 用 > 而不是 >=，因为如果 probTo[curNode] == curProbFromStart，
            // 说明这就是当前最优的路径，不应该跳过，需要继续处理（展开邻居）。
            if (probTo[curNode] > curProbFromStart) continue;
            // 判断是否已经到达目标点
            if (curNode == dst) return probTo[curNode];

            foreach (var e in graph[curNode])
            {
                var nextNode = (int)e[0];
                var nextProbFromStart = curProbFromStart * e[1]; // 这里是乘法，因为是概率，每过一条边，prob减小

                // 已经存在更优路径，则跳过
                // 用 >= 而不是 >，因为如果概率相等，再入队也不会改善结果，只会浪费时间。
                if (probTo[nextNode] >= nextProbFromStart) continue;

                pq.Enqueue(new State(nextNode, nextProbFromStart), nextProbFromStart);
                probTo[nextNode] = nextProbFromStart;
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
