namespace Scratch.Labuladong.Algorithms.CourseScheduleII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 记录后序遍历结果
    private List<int> postorder = new();

    // 记录是否存在环
    private bool hasCycle = false;
    private bool[] visited = null!;
    private bool[] onPath = null!;

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        // 把图结构后序遍历的结果进行反转，就是拓扑排序的结果
        var g = _buildG(numCourses, prerequisites);
        visited = new bool[numCourses];
        onPath = new bool[numCourses];
        for (int i = 0; i < numCourses; i++)
        {
            _traverse(g, i);
        }

        // 先判断一下题目输入的课程依赖是否成环，成环的话是无法进行拓扑排序的
        if (hasCycle) return [];

        // 逆后序遍历结果即为拓扑排序结果
        // postorder.Reverse();

        return postorder.ToArray();
    }

    private List<int>[] _buildG(int numCourses, int[][] prerequisites)
    {
        // 图中共有 numCourses 个节点
        var graph = new List<int>[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var edge in prerequisites)
        {
            var prereq = edge[1];
            var c = edge[0];
            graph[c].Add(prereq);
        }

        return graph;
    }

    void _traverse(List<int>[] graph, int s)
    {
        // 发现环
        if (onPath[s]) hasCycle = true;

        if (visited[s] || hasCycle) return;

        // 前序遍历位置
        onPath[s] = true;
        visited[s] = true;
        foreach (var t in graph[s])
        {
            _traverse(graph, t);
        }

        // 后序遍历位置
        postorder.Add(s);
        onPath[s] = false;
    }

    public int[] FindOrderBfs(int numCourses, int[][] prerequisites)
    {
        var g = _buildG(numCourses, prerequisites);
        // 计算入度
        var indegree = new int[numCourses];
        foreach (var edge in prerequisites)
        {
            // 1 -> 2 = 2 依赖 1，方便获取顺序
            var from = edge[1];
            var to = edge[0];
            indegree[to]++;
        }

        // 根据入度初始化队列中的节点
        var q = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (indegree[i] == 0) q.Enqueue(i);
        }

        // 记录拓扑排序结果
        var res = new int[numCourses];
        // 记录遍历节点的顺序（索引）
        var count = 0;
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            // 弹出节点的顺序即为拓扑排序结果
            res[count] = cur;
            count++;
            foreach (var next in g[cur])
            {
                indegree[next]--;
                if (indegree[next] == 0) q.Enqueue(next);
            }
        }

        // 存在环，拓扑排序不存在
        if (count != numCourses) return [];

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
