namespace Scratch.Labuladong.Algorithms.IsGraphBipartite;

// 785. Is Graph Bipartite? (Medium)
//
// There is an undirected graph with n nodes, where each node is numbered between 0 and n - 1. You
// are given a 2D array graph, where graph[u] is an array of nodes that node u is adjacent to. More
// formally, for each v in graph[u], there is an undirected edge between node u and node v. The
// graph has the following properties:
//
// - There are no self-edges (graph[u] does not contain u).
//
// - There are no parallel edges (graph[u] does not contain duplicate values).
//
// - If v is in graph[u], then u is in graph[v] (the graph is undirected).
//
// - The graph may not be connected, meaning there may be two nodes u and v such that there is no
// path between them.
//
// A graph is bipartite if the nodes can be partitioned into two independent sets A and B such that
// every edge in the graph connects a node in set A and a node in set B.
//
// Return true if and only if it is bipartite.
//
// Example 1:
//
// Input: graph = [[1,2,3],[0,2],[0,1,3],[0,2]]
// Output: false
// Explanation: There is no way to partition the nodes into two independent sets such that every
// edge connects a node in one and a node in the other.
//
// Example 2:
//
// Input: graph = [[1,3],[0,2],[1,3],[0,2]]
// Output: true
// Explanation: We can partition the nodes into two sets: {0, 2} and {1, 3}.
//
// Constraints:
//
// - graph.length == n
//
// - 1 <= n <= 100
//
// - 0 <= graph[u].length < n
//
// - 0 <= graph[u][i] <= n - 1
//
// - graph[u] does not contain u.
//
// - All the values of graph[u] are unique.
//
// - If graph[u] contains v, then graph[v] contains u.
//
// Related Topics: Depth-First Search, Breadth-First Search, Union-Find, Graph Theory

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 记录图是否符合二分图性质
    private bool Ok = true;

    // 记录图中节点的颜色，false 和 true 代表两种不同颜色
    private bool[] color = null!;

    // 记录图中节点是否被访问过
    private bool[] visited = null!;

    public bool IsBipartite(int[][] graph)
    {
        var n = graph.Length;
        color = new bool[n];
        visited = new bool[n];

        // 因为图不一定是联通的，可能存在多个子图
        // 所以要把每个节点都作为起点进行一次遍历
        // 如果发现任何一个子图不是二分图，整幅图都不算二分图
        for (int i = 0; i < n; i++)
        {
            if (!visited[i]) _traverse(graph, i);
        }

        return Ok;
    }

    private void _traverse(int[][] graph, int v)
    {
        // 如果已经确定不是二分图了，就不用浪费时间再递归遍历了
        if (!Ok) return;

        visited[v] = true;
        foreach (var w in graph[v])
        {
            if (!visited[w])
            {
                // 相邻节点 w 没有被访问过
                // 那么应该给节点 w 涂上和节点 v 不同的颜色
                color[w] = !color[v];
                _traverse(graph, w);
            }
            else
            {
                // 相邻节点 w 已经被访问过
                // 根据 v 和 w 的颜色判断是否是二分图
                if (color[w] == color[v]) Ok = false;
            }
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
