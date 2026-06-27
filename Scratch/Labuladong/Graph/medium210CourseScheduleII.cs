namespace Scratch.Labuladong.Algorithms.CourseScheduleII;

// 210. Course Schedule II (Medium)
//
// There are a total of numCourses courses you have to take, labeled from 0 to numCourses - 1. You
// are given an array prerequisites where prerequisites[i] = [a_i, b_i] indicates that you must
// take course b_i first if you want to take course a_i.
//
// - For example, the pair [0, 1], indicates that to take course 0 you have to first take course 1.
//
// Return the ordering of courses you should take to finish all courses. If there are many valid
// answers, return any of them. If it is impossible to finish all courses, return an empty array.
//
// Example 1:
//
// Input: numCourses = 2, prerequisites = [[1,0]]
// Output: [0,1]
// Explanation: There are a total of 2 courses to take. To take course 1 you should have finished
// course 0. So the correct course order is [0,1].
//
// Example 2:
//
// Input: numCourses = 4, prerequisites = [[1,0],[2,0],[3,1],[3,2]]
// Output: [0,2,1,3]
// Explanation: There are a total of 4 courses to take. To take course 3 you should have finished
// both courses 1 and 2. Both courses 1 and 2 should be taken after you finished course 0.
// So one correct course order is [0,1,2,3]. Another correct ordering is [0,2,1,3].
//
// Example 3:
//
// Input: numCourses = 1, prerequisites = []
// Output: [0]
//
// Constraints:
//
// - 1 <= numCourses <= 2000
//
// - 0 <= prerequisites.length <= numCourses * (numCourses - 1)
//
// - prerequisites[i].length == 2
//
// - 0 <= a_i, b_i < numCourses
//
// - a_i != b_i
//
// - All the pairs [a_i, b_i] are distinct.
//
// Related Topics: Depth-First Search, Breadth-First Search, Graph Theory, Topological Sort

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
        postorder.Reverse();

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
            var from = edge[1];
            var to = edge[0];
            graph[from].Add(to);
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
