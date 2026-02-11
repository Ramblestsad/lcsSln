namespace Scratch.Labuladong.Algorithms.CourseSchedule;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        // 什么时候无法修完所有课程？当存在循环依赖的时候
        // 看到依赖问题，首先想到的就是把问题转化成「有向图」这种数据结构
        // 只要图中存在环，那就说明存在循环依赖

        // 把课程看成「有向图」中的节点，节点编号分别是 0, 1, ..., numCourses-1
        // 把课程之间的依赖关系看做节点之间的有向边

        // 首先要把输入转化成一幅有向图，然后再判断图中是否存在环
        var g = _buildG(numCourses, prerequisites);
        // 构建入度数组
        var indegree = new int[numCourses];
        foreach (var edge in prerequisites)
        {
            var from = edge[0];
            var to = edge[1];
            // 节点 to 的入度加一
            indegree[to]++;
        }

        // 根据入度初始化队列中的节点
        Queue<int> q = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (indegree[i] == 0)
            {
                // 节点 i 没有入度，即没有依赖的节点
                // 可以作为拓扑排序的起点，加入队列
                q.Enqueue(i);
            }
        }

        // 记录遍历的节点个数
        var count = 0;
        while (q.Count > 0)
        {
            // 弹出节点 cur，并将它指向的节点的入度减一
            var cur = q.Dequeue();
            count++;
            foreach (var next in g[cur])
            {
                indegree[next]--;
                if (indegree[next] == 0)
                {
                    // 如果入度变为 0，说明 next 依赖的节点都已被遍历
                    q.Enqueue(next);
                }
            }
        }

        // 如果所有节点都被遍历过，说明不成环
        return count == numCourses;

        // 不仅要判断是否存在环，还要返回这个环具体有哪些节点，怎么办？
        // 最简单直接的解法是，在 bool[] onPath 数组的基础上，再使用一个 Stack<int> path 栈，把遍历过程中经过的节点顺序也保存下来。
        // 比如path 从栈底到栈顶的元素是 [0,4,5,9,8,7,6]。此时又一次遇到了节点 5，那么就可以知道 [5,9,8,7,6] 这部分是环了
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
            var from = edge[0];
            var to = edge[1];
            graph[from].Add(to);
        }

        return graph;
    }

    // 记录一次递归堆栈中的节点
    bool[] onPath = null!;

    // 记录节点是否被遍历过
    bool[] visited = null!;

    // 记录图中是否有环
    bool hasCycle = false;

    public bool CanFinishDfs(int numCourses, int[][] prerequisites)
    {
        var g = _buildG(numCourses, prerequisites);
        onPath = new bool[numCourses];
        visited = new bool[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            // 遍历图中的所有节点, 因为图不一定都相连
            _dfs(g, i);
        }

        return false;
    }

    void _dfs(List<int>[] graph, int s)
    {
        // 如果已经找到了环，也不用再遍历了
        if (hasCycle) return;

        if (onPath[s])
        {
            // s 已经在递归路径上，说明成环了
            hasCycle = true;
            return;
        }

        // 不用再重复遍历已遍历过的节点
        if (visited[s]) return;

        // 前序代码位置
        visited[s] = true;
        onPath[s] = true;
        foreach (var t in graph[s])
        {
            _dfs(graph, t);
        }

        // 后序代码位置
        onPath[s] = false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
