namespace Scratch.Labuladong.Algorithms.PossibleBipartition;

// 886. Possible Bipartition (Medium)
//
// We want to split a group of n people (labeled from 1 to n) into two groups of any size. Each
// person may dislike some other people, and they should not go into the same group.
//
// Given the integer n and the array dislikes where dislikes[i] = [a_i, b_i] indicates that the
// person labeled a_i does not like the person labeled b_i, return true if it is possible to split
// everyone into two groups in this way.
//
// Example 1:
//
// Input: n = 4, dislikes = [[1,2],[1,3],[2,4]]
// Output: true
// Explanation: The first group has [1,4], and the second group has [2,3].
//
// Example 2:
//
// Input: n = 3, dislikes = [[1,2],[1,3],[2,3]]
// Output: false
// Explanation: We need at least 3 groups to divide them. We cannot put them in two groups.
//
// Constraints:
//
// - 1 <= n <= 2000
//
// - 0 <= dislikes.length <= 10^4
//
// - dislikes[i].length == 2
//
// - 1 <= a_i < b_i <= n
//
// - All the pairs of dislikes are unique.
//
// Related Topics: Depth-First Search, Breadth-First Search, Union-Find, Graph Theory

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
