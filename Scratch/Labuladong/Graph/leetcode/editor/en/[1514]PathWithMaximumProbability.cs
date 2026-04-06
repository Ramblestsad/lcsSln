/*
 * @lc app=leetcode id=1514 lang=csharp
 * @lcpr version=30402
 *
 * [1514] Path With Maximum Probability
 */

namespace Scratch.Labuladong.Algorithms.PathWithMaximumProbability;

// @lc code=start
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
// @lc code=end
