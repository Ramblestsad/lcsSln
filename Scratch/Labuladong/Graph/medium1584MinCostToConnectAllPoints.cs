namespace Scratch.Labuladong.Algorithms.MinCostToConnectAllPoints;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinCostConnectPoints(int[][] points)
    {
        var n = points.Length;
        // 生成所有边及权重
        var edges = new List<int[]>();
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var weight = Math.Abs(points[i][0] - points[j][0]) + Math.Abs(points[i][1] - points[j][1]);
                edges.Add([i, j, weight]);
            }
        }

        // 按照权重从小到大排序
        edges.Sort((a, b) => a[2].CompareTo(b[2]));

        // Kruskal 算法
        var mst = 0;
        var uf = new UF(n);
        foreach (var edge in edges)
        {
            var u = edge[0];
            var v = edge[1];
            var weight = edge[2];
            // 若这条边会产生环，则不能加入 mst
            if (uf.Connected(u, v)) continue;
            // 否则，将这条边加入 mst，并合并 u 和 v 的连通分量
            uf.Union(u, v);
            mst += weight;
        }

        return mst;
    }

    // Union Find 结构
    class UF
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
}
//leetcode submit region end(Prohibit modification and deletion)
