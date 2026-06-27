namespace Scratch.Labuladong.Algorithms.PossibleBipartition;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private bool ok = true;
    private bool[] color = null!;
    private bool[] visited = null!;

    public bool PossibleBipartition(int n, int[][] dislikes)
    {
        // 图节点编号从 1 开始
        color = new bool[n + 1];
        visited = new bool[n + 1];
        var graph = _buildGraph(n, dislikes);

        for (int i = 1; i < n + 1; i++)
        {
            if (!visited[i]) _traverse(graph, i);
        }

        return ok;
    }

    private List<int>[] _buildGraph(int n, int[][] dislikes)
    {
        // 图节点编号为 1...n
        var graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var edge in dislikes)
        {
            var v = edge[1];
            var w = edge[0];
            // 「无向图」相当于「双向图」
            // v -> w
            graph[v].Add(w);
            // w -> v
            graph[w].Add(v);
        }

        return graph;
    }

    private void _traverse(List<int>[] graph, int v)
    {
        if (!ok) return;
        visited[v] = true;

        foreach (var w in graph[v])
        {
            if (!visited[w])
            {
                color[w] = !color[v];
                _traverse(graph, w);
            }
            else
            {
                if (color[w] == color[v]) ok = false;
            }
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
