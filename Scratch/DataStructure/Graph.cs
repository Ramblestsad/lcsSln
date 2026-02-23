namespace Scratch.DataStructure;

public class GraphAdjMatrix
{
    // 邻接矩阵，行列索引对应“顶点索引”
    List<List<int>> graph;

    // 顶点列表，元素代表“顶点值”，索引代表“顶点索引”
    List<int> vertices;

    public GraphAdjMatrix(int[] inVertices, int[][] edges)
    {
        vertices = [];
        graph = [];
        // 添加顶点
        foreach (var val in inVertices)
        {
            AddVertex(val);
        }

        // 添加边
        // 请注意，edges 元素代表顶点索引，即对应 vertices 元素索引
        foreach (var e in edges)
        {
            AddEdge(e[0], e[1]);
        }
    }

    public void AddVertex(int val)
    {
        // 向顶点列表中添加新顶点的值
        vertices.Add(val);
        // 在邻接矩阵中添加一行
        var newRow = new List<int>(vertices.Count);
        for (var i = 0; i < vertices.Count; i++)
        {
            newRow.Add(0);
        }

        // 在邻接矩阵中添加一列
        // 原先的矩阵所有行最后加一个0就代表新加了一列
        foreach (var row in graph)
        {
            row.Add(0);
        }

        // 最后再attach新行
        graph.Add(newRow);
    }

    public void RemoveVertex(int index)
    {
        if (index >= vertices.Count)
            throw new IndexOutOfRangeException();
        // 在顶点列表中移除索引 index 的顶点
        vertices.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的行
        graph.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的列
        foreach (var row in graph)
        {
            row.RemoveAt(index);
        }
    }

    /// <summary>
    /// 参数 i, j 对应 vertices 元素索引
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void AddEdge(int i, int j)
    {
        // 索引越界与相等处理
        if (i < 0 || j < 0 || i >= vertices.Count || j >= vertices.Count || i == j)
            throw new IndexOutOfRangeException();
        // 在无向图中，邻接矩阵关于主对角线对称，即满足 (i, j) == (j, i)
        graph[i][j] = 1;
        graph[j][i] = 1;
    }

    /// <summary>
    /// 参数 i, j 对应 vertices 元素索引
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public void RemoveEdge(int i, int j)
    {
        // 索引越界与相等处理
        if (i < 0 || j < 0 || i >= vertices.Count || j >= vertices.Count || i == j)
            throw new IndexOutOfRangeException();
        graph[i][j] = 0;
        graph[j][i] = 0;
    }
}

public class Vertex(int val)
{
    public int val = val;
}

public class GraphAdjList
{
    /*
     * 为了方便添加与删除顶点，以及简化代码，我们使用列表（动态数组）来代替链表。
       使用哈希表来存储邻接表，key 为顶点实例，value 为该顶点的邻接顶点列表（链表）。
     */

    // 邻接表，key：顶点，value：该顶点的所有邻接顶点
    public Dictionary<Vertex, List<Vertex>> graph;

    public GraphAdjList(Vertex[][] edges)
    {
        graph = [];
        // 添加所有顶点和边
        foreach (var edge in edges)
        {
            AddVertex(edge[0]);
            AddVertex(edge[1]);
            AddEdge(edge[0], edge[1]);
        }
    }

    int Size()
    {
        return graph.Count;
    }

    public void AddEdge(Vertex vet1, Vertex vet2)
    {
        if (!graph.ContainsKey(vet1) || !graph.ContainsKey(vet2) || vet1 == vet2)
            throw new InvalidOperationException();
        // 添加边 vet1 - vet2
        graph[vet1].Add(vet2);
        graph[vet2].Add(vet1);
    }

    public void RemoveEdge(Vertex vet1, Vertex vet2)
    {
        if (!graph.ContainsKey(vet1) || !graph.ContainsKey(vet2) || vet1 == vet2)
            throw new InvalidOperationException();
        // 删除边 vet1 - vet2
        graph[vet1].Remove(vet2);
        graph[vet2].Remove(vet1);
    }

    public void AddVertex(Vertex vet)
    {
        if (graph.ContainsKey(vet))
            return;

        graph.Add(vet, []);
    }

    public void RemoveVertex(Vertex vet)
    {
        if (!graph.ContainsKey(vet))
            throw new InvalidOperationException();
        // 在邻接表中删除顶点 vet 对应的链表
        graph.Remove(vet);
        // 遍历其他顶点的链表，删除所有包含 vet 的边
        foreach (var list in graph.Values)
        {
            list.Remove(vet);
        }
    }
}

public static class GraphTraverse
{
    public static List<Vertex> GraphBFS(GraphAdjList graph, Vertex startVet)
    {
        /*
         * 1. 将遍历起始顶点 startVet 加入队列，并开启循环。
         * 2. 在循环的每轮迭代中，弹出队首顶点并记录访问，然后将该顶点的所有邻接顶点加入到队列尾部。
         * 3. 循环步骤 2. ，直到所有顶点被访问完毕后结束。
         */

        // 顶点遍历序列
        List<Vertex> res = [];
        // 哈希集合，用于记录已被访问过的顶点
        HashSet<Vertex> visited = [startVet];
        // 队列用于实现 BFS
        Queue<Vertex> que = new();
        que.Enqueue(startVet);
        // 以顶点 vet 为起点，循环直至访问完所有顶点
        while (que.Count > 0)
        {
            var vet = que.Dequeue(); // 队首顶点出队
            res.Add(vet); // 记录访问顶点
            foreach (var adjVet in graph.graph[vet])
            {
                if (visited.Contains(adjVet))
                {
                    continue; // 跳过已被访问的顶点
                }

                que.Enqueue(adjVet); // 只入队未访问的顶点
                visited.Add(adjVet); // 标记该顶点已被访问
            }
        }

        // 返回顶点遍历序列
        return res;

        /*
         * 广度优先遍历的序列是否唯一？
         * 不唯一。广度优先遍历只要求按“由近及远”的顺序遍历，而多个相同距离的顶点的遍历顺序允许被任意打乱。
         */
    }

    public static List<Vertex> GraphDFS(GraphAdjList graph, Vertex startVet)
    {
        // 顶点遍历序列
        List<Vertex> res = [];
        // 哈希集合，用于记录已被访问过的顶点
        HashSet<Vertex> visited = [];
        DFS(graph, visited, res, startVet);
        return res;

        /*
         * 深度优先遍历的序列是否唯一？
         * 与广度优先遍历类似，深度优先遍历序列的顺序也不是唯一的。
         * 给定某顶点，先往哪个方向探索都可以，即邻接顶点的顺序可以任意打乱，都是深度优先遍历。
         */
    }

    static void DFS(GraphAdjList graph, HashSet<Vertex> visited, List<Vertex> res, Vertex vet)
    {
        res.Add(vet); // 记录访问顶点
        visited.Add(vet); // 标记该顶点已被访问
        // 遍历该顶点的所有邻接顶点
        foreach (Vertex adjVet in graph.graph[vet])
        {
            if (visited.Contains(adjVet))
            {
                continue; // 跳过已被访问的顶点
            }

            // 递归访问邻接顶点
            DFS(graph, visited, res, adjVet);
        }
    }
}

// Union Find 结构
public class UF
{
    // 记录连通分量
    private int count;

    // 节点 x 的父节点是 parent[x]
    private int[] parent;

    // 记录每棵树的「重量」（节点数量）
    private int[] size;

    // 构造函数，n 为图的节点总数
    public UF(int n)
    {
        // 一开始互不连通
        count = n;
        // 父节点指针初始指向自己
        parent = new int[n];
        size = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            size[i] = 1;
        }
    }

    public void Union(int p, int q)
    {
        var rootP = Find(p);
        var rootQ = Find(q);
        if (rootP == rootQ) return;

        // 将两棵树合并为一棵
        // 把小树接到大树下面，更平衡
        if (size[rootP] > size[rootQ])
        {
            parent[rootQ] = rootP;
            size[rootP] += size[rootQ];
        }
        else
        {
            parent[rootP] = rootQ;
            size[rootQ] += size[rootP];
        }

        // 连通分量个数减一
        count--;
    }

    public bool Connected(int p, int q)
    {
        var rootP = Find(p);
        var rootQ = Find(q);
        return rootP == rootQ;
    }

    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);
        }

        return parent[x];

        // 递归版本的路径压缩的迭代表示
        // 先找到根节点
        // int root = x;
        // while (parent[root] != root) {
        //     root = parent[root];
        // }
        // 然后把 x 到根节点之间的所有节点直接接到根节点下面
        // int old_parent = parent[x];
        // while (x != root) {
        //     parent[x] = root;
        //     x = old_parent;
        //     old_parent = parent[old_parent];
        // }
        // return root;
    }

    public int Count()
    {
        return count;
    }

    // 返回节点 x 所在连通分量的节点总数
    public int Size(int x)
    {
        var root = Find(x);

        return size[root];
    }
}

public static class DijkstraAlgorithm
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

public static class AStarAlgorithm
{
    // 对于任意节点 x，我们定义三个量：
    // g(x) 表示从起点 src 到节点 x 实际走过的路径长度。
    // h(x) 是从节点 x 到终点 dst 的预估距离（启发函数估算）。
    // f(x) = g(x) + h(x) 作为优先级队列中节点的排序依据。

    //State 类增加 f 字段，优先级队列按 f 排序。
    // 入队时计算 f = g + h。

    // 接近终点时 f(x) 的增速慢，节点排在优先级队列前面，更容易出队，算法也就会优先向终点的方向进行搜索；
    // 反之，远离终点时 f(x) 的增速快，搜索的优先级就会降低。

    class State(int node, int distFromStart, int f)
    {
        public int node = node;

        // g(x)，从起点到当前节点的实际距离
        public int distFromStart = distFromStart;

        // f(x) = g(x) + h(x)，用于优先级队列排序
        public int f = f;
    }

    // 启发函数，估算从 node 到 dst 的距离。具体实现取决于问题场景
    static int _heuristicFn(int node, int dst)
    {
        // 启发函数必须满足可接受性（Admissibility）条件：
        //      对于所有节点 x，h(x) 不能大于从 x 到终点的实际最短距离（高估）。

        // 曼哈顿距离，适用于只能上下左右 4 个方向移动，每格的代价为 1 的场景：
        var h = Math.Abs(node - dst) + Math.Abs(node - dst);

        // 切比雪夫距离，适用于上、下、左、右、左上、右上、左下、右下 8 个方向移动，且每格移动的代价为 1 的场景：
        // var h = Math.Max(Math.Abs(node - dst), Math.Abs(node - dst));

        // 欧几里得距离，适用于向任何角度、任何方向移动的场景：
        // var h = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

        return h;
    }

    // A* 算法，计算从 src 到 dst 的最短路径
    static int AStar(List<int[]>[] graph, int src, int dst)
    {
        var n = graph.Length;
        var distTo = new int[n];
        Array.Fill(distTo, int.MaxValue);
        distTo[src] = 0;

        // 按 f(x) 从小到大排序
        var pq = new PriorityQueue<State, int>();
        // 起点入队，f = g + h = 0 + h(src)
        var init = new State(src, 0, _heuristicFn(src, dst));
        pq.Enqueue(init, init.f);

        while (pq.Count > 0)
        {
            var s = pq.Dequeue();
            var curNode = s.node;
            var curDist = s.distFromStart;

            if (curNode == dst) return curDist;

            if (distTo[curNode] < curDist) continue;

            foreach (var e in graph[curNode])
            {
                var nextNode = e[0];
                var nextDist = curDist + e[1];

                if (distTo[nextNode] <= nextDist) continue;

                distTo[nextNode] = nextDist;
                // 入队时计算 f = g + h
                var f = nextDist + _heuristicFn(nextNode, dst);
                pq.Enqueue(new State(nextNode, nextDist, f), f);
            }
        }

        return -1;
    }
}
