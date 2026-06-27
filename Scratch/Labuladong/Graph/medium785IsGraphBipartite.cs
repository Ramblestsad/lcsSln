namespace Scratch.Labuladong.Algorithms.IsGraphBipartite;

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
