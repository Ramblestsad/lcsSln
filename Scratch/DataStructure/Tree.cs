using System.Text;

namespace Scratch.DataStructure;

/// <summary>
/// Binary tree node
/// </summary>
/// <param name="x">node value</param>
public class TreeNode(int? x)
{
    public TreeNode? Left;
    public TreeNode? Right;
    public int? Val = x;
}

/// <summary>
/// BST（Binary Search Tree）
/// </summary>
public class BinarySearchTree
{
    public TreeNode? Root;

    public BinarySearchTree(TreeNode root)
    {
        Root = root;
    }

    public TreeNode? Search(int num)
    {
        TreeNode? cur = Root;
        while (cur != null)
        {
            if (cur.Val < num)
                cur = cur.Right;
            else if (cur.Val > num)
                cur = cur.Left;
            else
                break;
        }

        return cur;
    }

    public void Insert(int num)
    {
        Root ??= new TreeNode(num);

        var cur = Root;
        TreeNode? pre = null;

        // search for the position to insert
        while (cur != null)
        {
            pre = cur;
            if (num < cur.Val)
                cur = cur.Left;
            else if (num > cur.Val)
                cur = cur.Right;
            else
                return; // already exists
        }
        // now `cur` is null, pre is the parent node of the new node

        // insert
        var newNode = new TreeNode(num);
        if (pre != null)
        {
            if (num < pre.Val)
                pre.Left = newNode;
            else
                pre.Right = newNode;
        }
    }

    public void Remove(int num)
    {
        if (Root == null)
            return;
        TreeNode? cur = Root, pre = null;
        // search for the node to delete
        while (cur != null)
        {
            if (cur.Val == num)
                break;
            pre = cur;
            if (cur.Val < num)
                cur = cur.Right;
            else
                cur = cur.Left;
        }

        if (cur == null)
            return;

        // children = 0 / 1
        if (cur.Left == null || cur.Right == null)
        {
            TreeNode? child = cur.Left ?? cur.Right;
            // delete cur: replace cur with child
            if (cur != Root)
            {
                if (pre!.Left == cur)
                    pre.Left = child;
                else
                    pre.Right = child;
            }
            else
            {
                // if it's root, update the root
                Root = child;
            }
        }
        // children = 2
        else
        {
            // get the next node in the inorder traversal (右子树的最小节点）
            TreeNode? tmp = cur.Right;
            while (tmp.Left != null)
            {
                tmp = tmp.Left;
            }

            // delete tmp recursively （先删除、再覆盖原待删除节点的值）
            Remove(tmp.Val!.Value);
            cur.Val = tmp.Val;
        }
    }
}

/// <summary>
/// AVL (Adelson-Velsky and Landis) Tree is a self-balancing binary search tree.
/// The balancing ensures that the tree maintains O(log n) time complexity
/// for insert, delete, and search operations by keeping the height difference
/// between the left and right subtrees of every node to at most one.
/// </summary>
public class AvlTreeNode(int x)
{
    private int _height;
    public AvlTreeNode? Left;
    public AvlTreeNode? Right;
    public int val = x;

    public static int Height(AvlTreeNode? node)
    {
        // 空节点高度为 -1 ，叶节点高度为 0
        return node?._height ?? -1;
    }

    public static void UpdateHeight(AvlTreeNode node)
    {
        // 节点高度等于最高子树高度 + 1
        node._height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    public static int BalanceFactor(AvlTreeNode? node)
    {
        if (node == null) return 0;
        // 节点平衡因子 = 左子树高度 - 右子树高度
        return Height(node.Left) - Height(node.Right);
        // tip: 设平衡因子为f，则一棵 AVL 树的任意节点的平衡因子皆满足 -1<=f<=1。
        //      就是说，AVL 树要求对每个节点，左右子树的高度差 最多为 1。
    }
}

public class AvlTree
{
    public AvlTreeNode? root { get; private set; }

    private static AvlTreeNode? RightRotate(AvlTreeNode? node)
    {
        // 以child为核心！！！
        var child = node?.Left;
        if (child == null || node == null) return child;
        // `if` child has a right child, attach to node's left
        var grandChild = child.Right;

        // 以child为原点右旋
        child.Right = node;
        node.Left = grandChild;

        // update height for node and child
        AvlTreeNode.UpdateHeight(node);
        AvlTreeNode.UpdateHeight(child);

        return child; // return new "balanced" root for the parent node to connect
    }

    private static AvlTreeNode? LeftRotate(AvlTreeNode? node)
    {
        var child = node?.Right;
        if (child == null || node == null) return child;
        // `if` child has a left child, attach to node's right
        var grandChild = child.Left;

        child.Left = node;
        node.Right = grandChild;

        AvlTreeNode.UpdateHeight(node);
        AvlTreeNode.UpdateHeight(child);

        return child;
    }

    private static AvlTreeNode? Rotate(AvlTreeNode? node)
    {
        // 失衡节点的平衡因子	子节点的平衡因子	应采用的旋转方法
        // > 1（左偏树）      >= 0             右旋
        // > 1（左偏树）      < 0              先左旋后右旋
        // < -1（右偏树）     <= 0             左旋
        // < -1（右偏树）     > 0              先右旋后左旋
        // 主要判断平衡因子的正负号来判断所采用的旋转方法

        // 获取节点 node 的平衡因子
        var balanceFactorInt = AvlTreeNode.BalanceFactor(node);
        // 左偏树
        if (balanceFactorInt > 1)
        {
            if (AvlTreeNode.BalanceFactor(node?.Left) >= 0)
            {
                // 右旋
                return RightRotate(node);
            }
            else
            {
                // 先左旋后右旋
                node!.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
        }

        // 右偏树
        if (balanceFactorInt < -1)
        {
            if (AvlTreeNode.BalanceFactor(node?.Right) <= 0)
            {
                // 左旋
                return LeftRotate(node);
            }
            else
            {
                // 先右旋后左旋
                node!.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }
        }

        // 平衡树，无须旋转，直接返回
        return node;
    }

    public void Insert(int num)
    {
        root = InsertHelper(root, num);
    }

    private static AvlTreeNode? InsertHelper(AvlTreeNode? node, int num)
    {
        /*
         * AVL 树的节点插入操作与二叉搜索树在主体上类似。
         * 唯一的区别在于，在 AVL 树中插入节点后，从该节点到根节点的路径上可能会出现一系列失衡节点。
         * 因此，需要从这个节点开始，自底向上执行旋转操作，使所有失衡节点恢复平衡。
         */

        // 每一轮递归的node不一样，直到children为null，然后创建新的node。
        // 每一轮递归的node后续都需要UpdateHeight 和 Rotate，从插入节点的父节点自底向上。
        if (node == null) return new AvlTreeNode(num);

        /* 1. 查找插入位置并插入节点 */
        if (num < node.val)
            node.Left = InsertHelper(node.Left, num);
        else if (num > node.val)
            node.Right = InsertHelper(node.Right, num);
        else
            return node; // 重复节点不插入，直接返回

        AvlTreeNode.UpdateHeight(node); // 更新节点高度

        /* 2. 执行旋转操作，使该子树重新恢复平衡 */
        node = Rotate(node);

        return node;
    }

    public void Remove(int num)
    {
        root = RemoveHelper(root, num);
    }

    private static AvlTreeNode? RemoveHelper(AvlTreeNode? node, int num)
    {
        /*
         * 类似地，在二叉搜索树的删除节点方法的基础上，需要从底至顶执行旋转操作，使所有失衡节点恢复平衡。
         */
        if (node == null) return null;
        /* 1. 查找节点并删除 */
        if (num < node.val)
            node.Left = RemoveHelper(node.Left, num);
        else if (num > node.val)
            node.Right = RemoveHelper(node.Right, num);
        else
        {
            if (node.Left == null || node.Right == null)
            {
                AvlTreeNode? child = node.Left ?? node.Right;
                // 子节点数量 = 0 ，直接删除 node 并返回
                if (child == null)
                    return null;
                // 子节点数量 = 1 ，直接删除 node
                node = child;
            }
            else
            {
                // 子节点数量 = 2 ，则将中序遍历的下个节点(右子树的最小节点)删除，并用该节点替换当前节点
                AvlTreeNode? temp = node.Right;
                while (temp.Left != null)
                {
                    temp = temp.Left;
                }

                node.Right = RemoveHelper(node.Right, temp.val);
                node.val = temp.val;
            }
        }

        AvlTreeNode.UpdateHeight(node); // 更新节点高度

        /* 2. 执行旋转操作，使该子树重新恢复平衡 */
        node = Rotate(node);

        return node;
    }
}

public static class TreeUtils
{
    /// <summary>
    /// 层序遍历
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<int> LevelOrder(TreeNode root)
    {
        // 初始化队列，加入根节点
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);
        // 初始化一个列表，用于保存遍历序列
        List<int> list = [];
        while (queue.Count != 0)
        {
            TreeNode node = queue.Dequeue(); // 队列出队
            list.Add(node.Val!.Value); // 保存节点值
            if (node.Left != null)
                queue.Enqueue(node.Left); // 左子节点入队
            if (node.Right != null)
                queue.Enqueue(node.Right); // 右子节点入队
        }

        return list;
    }

    /// <summary>
    /// 带深度的层序遍历
    /// </summary>
    /// <param name="root"></param>
    public static void LevelOrderTraverse(TreeNode root)
    {
        if (root == null)
        {
            return;
        }

        Queue<TreeNode> q = new();
        q.Enqueue(root);
        // 记录当前遍历到的层数（根节点视为第 1 层）
        var depth = 1;

        while (q.Count != 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++)
            {
                TreeNode cur = q.Dequeue();
                // 访问 cur 节点，同时知道它所在的层数
                Console.WriteLine("depth = " + depth + ", val = " + cur.Val);

                // 把 cur 的左右子节点加入队列
                if (cur.Left != null)
                {
                    q.Enqueue(cur.Left);
                }

                if (cur.Right != null)
                {
                    q.Enqueue(cur.Right);
                }
            }

            depth++;
        }
    }

    struct State(TreeNode node, int depth)
    {
        public TreeNode node = node;
        public int depth = depth;
    }

    public static void LevelOrderTraverseWeight(TreeNode root)
    {
        if (root == null)
        {
            return;
        }

        Queue<State> q = new();
        // 根节点的路径权重和是 1
        q.Enqueue(new State(root, 1));

        while (q.Count != 0)
        {
            State cur = q.Dequeue();
            // 访问 cur 节点，同时知道它的路径权重和
            Console.WriteLine("depth = " + cur.depth + ", val = " + cur.node.Val);

            // 把 cur 的左右子节点加入队列
            if (cur.node.Left != null)
            {
                q.Enqueue(new State(cur.node.Left, cur.depth + 1));
            }

            if (cur.node.Right != null)
            {
                q.Enqueue(new State(cur.node.Right, cur.depth + 1));
            }
        }
    }

    /// <summary>
    /// 前序遍历
    /// </summary>
    /// <param name="root"></param>
    public static void PreOrder(TreeNode? root)
    {
        List<int> list = [];

        if (root == null) return;
        // 访问优先级：根节点 -> 左子树 -> 右子树
        list.Add(root.Val!.Value);
        PreOrder(root.Left);
        PreOrder(root.Right);
    }

    /// <summary>
    /// 中序遍历
    /// </summary>
    /// <param name="root"></param>
    public static void InOrder(TreeNode? root)
    {
        List<int> list = [];

        if (root == null) return;
        // 访问优先级：左子树 -> 根节点 -> 右子树
        InOrder(root.Left);
        list.Add(root.Val!.Value);
        InOrder(root.Right);
    }

    /// <summary>
    /// 后序遍历
    /// </summary>
    /// <param name="root"></param>
    public static void PostOrder(TreeNode? root)
    {
        List<int> list = [];

        if (root == null) return;
        // 访问优先级：左子树 -> 右子树 -> 根节点
        PostOrder(root.Left);
        PostOrder(root.Right);
        list.Add(root.Val!.Value);
    }
}

/// <summary>
/// 数组表示下的二叉树类
/// </summary>
/// <param name="arr"></param>
public class ArrayBinaryTree(List<int?> arr)
{
    private List<int?> _tree = [.. arr];

    /* 列表容量 */
    public int Size()
    {
        return _tree.Count;
    }

    /* 获取索引为 i 节点的值 */
    public int? Val(int i)
    {
        // 若索引越界，则返回 null ，代表空位
        if (i < 0 || i >= Size())
            return null;
        return _tree[i];
    }

    /* 获取索引为 i 节点的左子节点的索引 */
    public int Left(int i)
    {
        return 2 * i + 1;
    }

    /* 获取索引为 i 节点的右子节点的索引 */
    public int Right(int i)
    {
        return 2 * i + 2;
    }

    /* 获取索引为 i 节点的父节点的索引 */
    public int Parent(int i)
    {
        return ( i - 1 ) / 2;
    }

    /* 层序遍历 */
    public List<int> LevelOrder()
    {
        List<int> res = [];
        // 直接遍历数组
        for (var i = 0; i < Size(); i++)
        {
            if (Val(i).HasValue)
                res.Add(Val(i)!.Value);
        }

        return res;
    }

    /* 深度优先遍历 */
    void Dfs(int i, string order, List<int> res)
    {
        // 若为空位，则返回
        if (!Val(i).HasValue)
            return;
        // 前序遍历
        if (order == "pre")
            res.Add(Val(i)!.Value);
        Dfs(Left(i), order, res);
        // 中序遍历
        if (order == "in")
            res.Add(Val(i)!.Value);
        Dfs(Right(i), order, res);
        // 后序遍历
        if (order == "post")
            res.Add(Val(i)!.Value);
    }

    /* 前序遍历 */
    public List<int> PreOrder()
    {
        List<int> res = [];
        Dfs(0, "pre", res);
        return res;
    }

    /* 中序遍历 */
    public List<int> InOrder()
    {
        List<int> res = [];
        Dfs(0, "in", res);
        return res;
    }

    /* 后序遍历 */
    public List<int> PostOrder()
    {
        List<int> res = [];
        Dfs(0, "post", res);
        return res;
    }
}

public class TrieMap<V>
{
    // ASCII 码个数
    private const int R = 256;

    // 当前存在 Map 中的键值对个数
    public int Size { get; set; } = 0;

    private class TrieNode
    {
        public V? val = default;
        public bool hasValue = false;
        public TrieNode?[] children = new TrieNode?[R];
    }

    // Trie 树的根节点
    private TrieNode? root = new();

    // 从节点 node 开始搜索 key，如果存在返回对应节点，否则返回 null
    private static TrieNode? _getNode(TrieNode? node, string key)
    {
        var p = node;
        // 从节点 node 开始搜索 key
        foreach (var c in key)
        {
            if (p == null)
                // 无法向下搜索
                return null;

            // 向下搜索
            p = p.children[c];
        }

        return p;
    }

    // 搜索 key 对应的值，不存在则返回 null
    public V? Get(string key)
    {
        // 从 root 开始搜索 key
        var x = _getNode(root, key);
        if (x == null || !x.hasValue)
        {
            // x 为空或 x 的 val 字段为空都说明 key 没有对应的值
            return default;
        }

        return x.val;
    }

    // 判断 key 是否存在在 Map 中
    public bool ContainsKey(string key)
    {
        var x = _getNode(root, key);
        return x?.hasValue == true;
    }

    // 判断是和否存在前缀为 prefix 的键
    public bool HasKeyWithPrefix(string prefix)
    {
        // 只要能找到一个节点，就是存在前缀
        return _getNode(root, prefix) != null;
    }

    // 在所有键中寻找 query 的最短前缀
    public string ShortestPrefixOf(string query)
    {
        var p = root;
        for (var i = 0; i < query.Length; i++)
        {
            if (p == null) return "";
            if (p.hasValue)
                return query[..i];

            p = p.children[query[i]];
        }

        if (p?.hasValue == true)
            return query;

        return "";
    }

    // 在所有键中寻找 query 的最长前缀
    public string LongestPrefixOf(string query)
    {
        var p = root;
        // 记录前缀的最大长度
        var maxLen = 0;

        // 从节点 node 开始搜索 key
        for (int i = 0; i < query.Length; i++)
        {
            if (p == null)
                // 无法向下搜索
                break;

            if (p.hasValue)
            {
                // 找到一个键是 query 的前缀，更新前缀的最大长度
                maxLen = i;
            }

            // 向下搜索
            p = p.children[query[i]];
        }

        if (p?.hasValue == true)
        {
            // 如果 query 本身就是一个键
            return query;
        }

        return query[..maxLen];
    }


    public List<string> KeysWithPrefix(string prefix)
    {
        List<string> res = [];
        // 找到匹配 prefix 在 Trie 树中的那个节点
        var x = _getNode(root, prefix);
        if (x == null)
        {
            return res;
        }

        // DFS 遍历以 x 为根的这棵 Trie 树
        _traverseAllKeys(x, new StringBuilder(prefix), res);
        return res;
    }

    // 遍历以 node 节点为根的 Trie 树，找到所有键
    private static void _traverseAllKeys(TrieNode? node, StringBuilder path, List<string> res)
    {
        if (node == null)
            // 到达 Trie 树底部叶子结点
            return;

        if (node.hasValue)
            // 找到一个 key，添加到结果列表中
            res.Add(path.ToString());

        // 回溯算法遍历框架
        for (var c = 0; c < R; c++)
        {
            // 做选择
            path.Append((char)c);
            _traverseAllKeys(node.children[c], path, res);
            // 撤销选择
            path.Remove(path.Length - 1, 1);
        }
    }

    // 通配符 . 匹配任意字符
    public List<string> KeysWithPattern(string pattern)
    {
        List<string> res = [];
        _traverseAllKeysWithPattern(root, new StringBuilder(), pattern, 0, res);
        return res;
    }

    // 遍历函数，尝试在「以 node 为根的 Trie 树中」匹配 pattern[i..]
    private void _traverseAllKeysWithPattern(
        TrieNode? node,
        StringBuilder path,
        string pattern,
        int i,
        List<string> res)
    {
        if (node == null)
            // 树枝不存在，即字符 pattern[i-1] 匹配失败
            return;

        if (i == pattern.Length)
        {
            // pattern 匹配完成
            if (node.hasValue)
                // 如果这个节点存储着 val，则找到一个匹配的键
                res.Add(path.ToString());

            return;
        }

        var c = pattern[i];
        if (c == '.')
        {
            // pattern[i] 是通配符，可以变化成任意字符
            // 多叉树（回溯算法）遍历框架
            for (var j = 0; j < R; j++)
            {
                path.Append((char)j);
                _traverseAllKeysWithPattern(node.children[j], path, pattern, i + 1, res);
                path.Remove(path.Length - 1, 1);
            }
        }
        else
        {
            // pattern[i] 是普通字符 c
            path.Append(c);
            _traverseAllKeysWithPattern(node.children[c], path, pattern, i + 1, res);
            path.Remove(path.Length - 1, 1);
        }
    }

    // 判断是和否存在前缀为 prefix 的键
    public bool HasKeyWithPattern(string pattern)
    {
        // 一个偷懒的实现
        // return keysWithPattern(pattern).Count != 0;

        // 从 root 节点开始匹配 pattern[0..]
        return _hasKeyWithPattern(root, pattern, 0);
    }

    // 函数定义：从 node 节点开始匹配 pattern[i..]，返回是否成功匹配
    private bool _hasKeyWithPattern(TrieNode? node, string pattern, int i)
    {
        // 找到一个可行解就提前结束递归
        if (node == null)
            // 树枝不存在，即匹配失败
            return false;

        if (i == pattern.Length)
            // 模式串走到头了，看看匹配到的是否是一个键
            return node.hasValue;

        var c = pattern[i];
        // 没有遇到通配符
        if (c != '.')
        {
            // 从 node.children[c] 节点开始匹配 pattern[i+1..]
            return _hasKeyWithPattern(node.children[c], pattern, i + 1);
        }

        // 遇到通配符
        for (int j = 0; j < R; j++)
        {
            // pattern[i] 可以变化成任意字符，尝试所有可能，只要遇到一个匹配成功就返回
            if (_hasKeyWithPattern(node.children[j], pattern, i + 1))
            {
                return true;
            }
        }

        // 都没有匹配
        return false;
    }

    // 在 map 中添加或修改键值对
    public void Put(string key, V val)
    {
        if (!ContainsKey(key))
        {
            // 新增键值对
            Size++;
        }

        // 需要一个额外的辅助函数，并接收其返回值
        root = _put(root, key, val, 0);
    }

    // 定义：向以 node 为根的 Trie 树中插入 key[i..]，返回插入完成后的根节点
    private static TrieNode _put(TrieNode? node, string key, V val, int i)
    {
        if (node == null)
        {
            // 如果树枝不存在，新建
            node = new();
        }

        if (i == key.Length)
        {
            // key 的路径已插入完成，将值 val 存入节点
            node.val = val;
            node.hasValue = true;
            return node;
        }

        var c = key[i];
        // 递归插入子节点，并接收返回值
        node.children[c] = _put(node.children[c], key, val, i + 1);
        return node;
    }

    // 在 Map 中删除 key
    public void Remove(string key)
    {
        if (!ContainsKey(key))
        {
            return;
        }

        // 递归修改数据结构要接收函数的返回值
        root = _remove(root, key, 0);
        Size--;
    }

    // 定义：在以 node 为根的 Trie 树中删除 key[i..]，返回删除后的根节点
    private TrieNode? _remove(TrieNode? node, string key, int i)
    {
        // 一个节点如何知道自己是否需要被删除
        //     主要看自己的 val 字段是否为空
        //     自己的 children 数组是否全都是空指针
        if (node == null)
        {
            return null;
        }

        if (i == key.Length)
        {
            // 找到了 key 对应的 TrieNode，删除 val
            node.val = default;
            node.hasValue = false;
        }
        else
        {
            var c = key[i];
            // 递归去子树进行删除
            node.children[c] = _remove(node.children[c], key, i + 1);
        }

        // 一个节点要先递归处理子树，然后在后序位置检查自己的 val 字段和 children 列表，判断自己是否需要被删除
        // 后序位置，递归路径上的节点可能需要被清理
        if (node.hasValue)
            // 如果该 TireNode 存储着 val，不需要被清理
            return node;

        // 检查该 TrieNode 是否还有后缀
        for (int c = 0; c < R; c++)
        {
            if (node.children[c] != null)
                // 只要存在一个子节点（后缀树枝），就不需要被清理
                return node;
        }

        // 既没有存储 val，也没有后缀树枝，则该节点需要被清理
        return null;
    }
}

public class TrieSet
{
    // 底层用一个 TrieMap，键就是 TrieSet，值仅仅起到占位的作用
    // 值的类型可以随便设置，我参考 Java 标准库设置成 Object
    private readonly TrieMap<object> map = new();

    // **** 增 ****

    // 在集合中添加元素 key
    public void Add(string key)
    {
        map.Put(key, new object());
    }

    // **** 删 ****

    // 从集合中删除元素 key
    public void Remove(string key)
    {
        map.Remove(key);
    }

    // **** 查 ****

    // 判断元素 key 是否存在集合中
    public bool Contains(string key)
    {
        return map.ContainsKey(key);
    }

    // 在集合中寻找 query 的最短前缀
    public string ShortestPrefixOf(string query)
    {
        return map.ShortestPrefixOf(query);
    }

    // 在集合中寻找 query 的最长前缀
    public string LongestPrefixOf(string query)
    {
        return map.LongestPrefixOf(query);
    }

    // 在集合中搜索前缀为 prefix 的所有元素
    public List<string> KeysWithPrefix(string prefix)
    {
        return map.KeysWithPrefix(prefix);
    }

    // 判断集合中是否存在前缀为 prefix 的元素
    public bool HasKeyWithPrefix(string prefix)
    {
        return map.HasKeyWithPrefix(prefix);
    }

    // 通配符 . 匹配任意字符，返回集合中匹配 pattern 的所有元素
    public List<string> KeysWithPattern(string pattern)
    {
        return map.KeysWithPattern(pattern);
    }

    // 通配符 . 匹配任意字符，判断集合中是否存在匹配 pattern 的元素
    public bool HasKeyWithPattern(string pattern)
    {
        return map.HasKeyWithPattern(pattern);
    }

    // 返回集合中元素的个数
    public int Size()
    {
        return map.Size;
    }
}
