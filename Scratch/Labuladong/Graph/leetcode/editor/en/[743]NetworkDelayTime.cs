/*
 * @lc app=leetcode id=743 lang=csharp
 * @lcpr version=30402
 *
 * [743] Network Delay Time
 */

namespace Scratch.Labuladong.Algorithms.NetworkDelayTime;

// @lc code=start
public class Solution
{
    class State
    {
        // 当前节点 ID
        public int node;

        // 从起点 s 到当前 node 节点的最小路径权重和
        public int distFromStart;

        public State(int node, int distFromStart)
        {
            this.node = node;
            this.distFromStart = distFromStart;
        }
    }

    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        // 节点编号是从 1 开始的，所以要一个大小为 n + 1 的邻接表
        var graph = new List<int[]>[n + 1];
        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = [];
        }

        // build graph
        foreach (var edge in times)
        {
            var from = edge[0];
            var to = edge[1];
            var weight = edge[2];
            // from -> List<(to, weight)>
            // 邻接表存储图结构，同时存储权重信息
            graph[from].Add([to, weight]);
        }

        var distTo = Dijkstra(graph, k);

        // 找到最长的那条最短路径
        var res = 0;
        for (int i = 1; i < distTo.Length; i++)
        {
            if (distTo[i] == int.MaxValue) return -1;
            res = Math.Max(res, distTo[i]);
        }

        return res;
    }

    // 输入不包含负权重边的加权图 graph 和起点 src
    // graph[i] 是节点 i 的邻接表，每个元素 int[] { 邻接节点, 权重 }
    // numNodes 为图的节点总数
    // 返回从起点 src 到其他节点的最小路径权重和
    public static int[] Dijkstra(List<int[]>[] graph, int src)
    {
        // 记录从起点 src 到其他节点的最小路径权重和
        // distTo[i] 表示从起点 src 到节点 i 的最小路径权重和
        var distTo = new int[graph.Length];
        // 都初始化为正无穷，表示未计算
        Array.Fill(distTo, int.MaxValue);
        // 优先级队列，distFromStart 较小的节点排在前面
        var pq = new PriorityQueue<State, int>();

        // 从起点 src 开始进行 BFS
        var initState = new State(src, 0);
        pq.Enqueue(initState, initState.distFromStart);
        distTo[src] = 0;

        // BFS
        while (pq.Count > 0)
        {
            var state = pq.Dequeue();
            var curNode = state.node;
            var curDistFromStart = state.distFromStart;

            // 在 Dijkstra 算法中，队列中可能存在重复的节点 state
            // 所以要在元素出队时进行判断，去除较差的重复节点
            if (distTo[curNode] < curDistFromStart) continue;

            // 遍历 curNode 的所有邻接节点
            foreach (var neighbor in graph[curNode])
            {
                var nextNode = neighbor[0];
                var weight = neighbor[1];
                var nextDistFromStart = curDistFromStart + weight;

                if (distTo[nextNode] <= nextDistFromStart) continue;

                // 将 nextNode 节点加入优先级队列
                pq.Enqueue(new State(nextNode, nextDistFromStart), nextDistFromStart);
                // 记录 nextNode 节点到起点的最小路径权重和
                distTo[nextNode] = nextDistFromStart;
            }
        }

        return distTo;
    }
}
// @lc code=end
