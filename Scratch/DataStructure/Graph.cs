namespace Scratch.DataStructure;

public class GraphAdjMatrix
{
    // 邻接矩阵，行列索引对应“顶点索引”
    List<List<int>> adjMatrix;

    // 顶点列表，元素代表“顶点值”，索引代表“顶点索引”
    List<int> vertices;

    public GraphAdjMatrix(int[] inVertices, int[][] edges)
    {
        vertices = [];
        adjMatrix = [];
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
        foreach (var row in adjMatrix)
        {
            row.Add(0);
        }

        // 最后再attach新行
        adjMatrix.Add(newRow);
    }

    public void RemoveVertex(int index)
    {
        if (index >= vertices.Count)
            throw new IndexOutOfRangeException();
        // 在顶点列表中移除索引 index 的顶点
        vertices.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的行
        adjMatrix.RemoveAt(index);
        // 在邻接矩阵中删除索引 index 的列
        foreach (var row in adjMatrix)
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
        adjMatrix[i][j] = 1;
        adjMatrix[j][i] = 1;
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
        adjMatrix[i][j] = 0;
        adjMatrix[j][i] = 0;
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
    public Dictionary<Vertex, List<Vertex>> adjList;

    public GraphAdjList(Vertex[][] edges)
    {
        adjList = [];
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
        return adjList.Count;
    }

    public void AddEdge(Vertex vet1, Vertex vet2)
    {
        if (!adjList.ContainsKey(vet1) || !adjList.ContainsKey(vet2) || vet1 == vet2)
            throw new InvalidOperationException();
        // 添加边 vet1 - vet2
        adjList[vet1].Add(vet2);
        adjList[vet2].Add(vet1);
    }

    public void RemoveEdge(Vertex vet1, Vertex vet2)
    {
        if (!adjList.ContainsKey(vet1) || !adjList.ContainsKey(vet2) || vet1 == vet2)
            throw new InvalidOperationException();
        // 删除边 vet1 - vet2
        adjList[vet1].Remove(vet2);
        adjList[vet2].Remove(vet1);
    }

    public void AddVertex(Vertex vet)
    {
        if (adjList.ContainsKey(vet))
            return;

        adjList.Add(vet, []);
    }

    public void RemoveVertex(Vertex vet)
    {
        if (!adjList.ContainsKey(vet))
            throw new InvalidOperationException();
        // 在邻接表中删除顶点 vet 对应的链表
        adjList.Remove(vet);
        // 遍历其他顶点的链表，删除所有包含 vet 的边
        foreach (List<Vertex> list in adjList.Values)
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
        visited.Add(startVet);
        // 队列用于实现 BFS
        Queue<Vertex> que = new();
        que.Enqueue(startVet);
        // 以顶点 vet 为起点，循环直至访问完所有顶点
        while (que.Count > 0)
        {
            var vet = que.Dequeue(); // 队首顶点出队
            res.Add(vet); // 记录访问顶点
            foreach (var adjVet in graph.adjList[vet])
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
        GraphTraverse.DFS(graph, visited, res, startVet);
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
        foreach (Vertex adjVet in graph.adjList[vet])
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
