namespace Scratch.DataStructure;

/// <summary>
/// Binary tree node
/// </summary>
/// <param name="x">node value</param>
public class TreeNode(int? x) {
    public int? Val = x;
    public TreeNode? Left;
    public TreeNode? Right;
}

/// <summary>
/// BST（Binary Search Tree）
/// </summary>
public class BinarySearchTree {
    public TreeNode? Root;

    public BinarySearchTree(TreeNode root) {
        this.Root = root;
    }

    public TreeNode? Search(int num) {
        TreeNode? cur = this.Root;
        while (cur != null) {
            if (cur.Val < num)
                cur = cur.Right;
            else if (cur.Val > num)
                cur = cur.Left;
            else
                break;
        }

        return cur;
    }

    public void Insert(int num) {
        Root ??= new TreeNode(num);

        var cur = Root;
        TreeNode? pre = null;

        // search for the position to insert
        while (cur != null) {
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
        if (pre != null) {
            if (num < pre.Val)
                pre.Left = newNode;
            else
                pre.Right = newNode;
        }
    }

    public void Remove(int num) {
        if (Root == null)
            return;
        TreeNode? cur = Root, pre = null;
        // search for the node to delete
        while (cur != null) {
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
        if (cur.Left == null || cur.Right == null) {
            TreeNode? child = cur.Left ?? cur.Right;
            // delete cur: replace cur with child
            if (cur != Root) {
                if (pre!.Left == cur)
                    pre.Left = child;
                else
                    pre.Right = child;
            }
            else {
                // if it's root, update the root
                Root = child;
            }
        }
        // children = 2
        else {
            // get the next node in the inorder traversal (右子树的最小节点）
            TreeNode? tmp = cur.Right;
            while (tmp.Left != null) {
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
public class AvlTreeNode(int x) {
    public int val = x;
    private int _height;
    public AvlTreeNode? Left;
    public AvlTreeNode? Right;

    public static int Height(AvlTreeNode? node) {
        // 空节点高度为 -1 ，叶节点高度为 0
        return node?._height ?? -1;
    }

    public static void UpdateHeight(AvlTreeNode node) {
        // 节点高度等于最高子树高度 + 1
        node._height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    public static int BalanceFactor(AvlTreeNode? node) {
        if (node == null) return 0;
        // 节点平衡因子 = 左子树高度 - 右子树高度
        return Height(node.Left) - Height(node.Right);
        // tip: 设平衡因子为f，则一棵 AVL 树的任意节点的平衡因子皆满足 -1<=f<=1。
        //      就是说，AVL 树要求对每个节点，左右子树的高度差 最多为 1。
    }
}

public class AvlTree {
    public AvlTreeNode? root { get; private set; }

    private static AvlTreeNode? RightRotate(AvlTreeNode? node) {
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

    private static AvlTreeNode? LeftRotate(AvlTreeNode? node) {
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

    private static AvlTreeNode? Rotate(AvlTreeNode? node) {
        // 失衡节点的平衡因子	子节点的平衡因子	应采用的旋转方法
        // > 1（左偏树）      >= 0             右旋
        // > 1（左偏树）      < 0              先左旋后右旋
        // < -1（右偏树）     <= 0             左旋
        // < -1（右偏树）     > 0              先右旋后左旋
        // 主要判断平衡因子的正负号来判断所采用的旋转方法

        // 获取节点 node 的平衡因子
        var balanceFactorInt = AvlTreeNode.BalanceFactor(node);
        // 左偏树
        if (balanceFactorInt > 1) {
            if (AvlTreeNode.BalanceFactor(node?.Left) >= 0) {
                // 右旋
                return RightRotate(node);
            }
            else {
                // 先左旋后右旋
                node!.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
        }

        // 右偏树
        if (balanceFactorInt < -1) {
            if (AvlTreeNode.BalanceFactor(node?.Right) <= 0) {
                // 左旋
                return LeftRotate(node);
            }
            else {
                // 先右旋后左旋
                node!.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }
        }

        // 平衡树，无须旋转，直接返回
        return node;
    }

    public void Insert(int num) {
        root = InsertHelper(root, num);
    }

    private static AvlTreeNode? InsertHelper(AvlTreeNode? node, int num) {
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

    public void Remove(int num) {
        root = RemoveHelper(root, num);
    }

    private static AvlTreeNode? RemoveHelper(AvlTreeNode? node, int num) {
        /*
         * 类似地，在二叉搜索树的删除节点方法的基础上，需要从底至顶执行旋转操作，使所有失衡节点恢复平衡。
         */
        if (node == null) return null;
        /* 1. 查找节点并删除 */
        if (num < node.val)
            node.Left = RemoveHelper(node.Left, num);
        else if (num > node.val)
            node.Right = RemoveHelper(node.Right, num);
        else {
            if (node.Left == null || node.Right == null) {
                AvlTreeNode? child = node.Left ?? node.Right;
                // 子节点数量 = 0 ，直接删除 node 并返回
                if (child == null)
                    return null;
                // 子节点数量 = 1 ，直接删除 node
                node = child;
            }
            else {
                // 子节点数量 = 2 ，则将中序遍历的下个节点(右子树的最小节点)删除，并用该节点替换当前节点
                AvlTreeNode? temp = node.Right;
                while (temp.Left != null) {
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

public static class TreeUtils {
    /// <summary>
    /// 层序遍历
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<int> LevelOrder(TreeNode root) {
        // 初始化队列，加入根节点
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);
        // 初始化一个列表，用于保存遍历序列
        List<int> list = [];
        while (queue.Count != 0) {
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
    /// 前序遍历
    /// </summary>
    /// <param name="root"></param>
    public static void PreOrder(TreeNode? root) {
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
    public static void InOrder(TreeNode? root) {
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
    public static void PostOrder(TreeNode? root) {
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
public class ArrayBinaryTree(List<int?> arr) {
    private List<int?> _tree = [.. arr];

    /* 列表容量 */
    public int Size() {
        return _tree.Count;
    }

    /* 获取索引为 i 节点的值 */
    public int? Val(int i) {
        // 若索引越界，则返回 null ，代表空位
        if (i < 0 || i >= Size())
            return null;
        return _tree[i];
    }

    /* 获取索引为 i 节点的左子节点的索引 */
    public int Left(int i) {
        return 2 * i + 1;
    }

    /* 获取索引为 i 节点的右子节点的索引 */
    public int Right(int i) {
        return 2 * i + 2;
    }

    /* 获取索引为 i 节点的父节点的索引 */
    public int Parent(int i) {
        return ( i - 1 ) / 2;
    }

    /* 层序遍历 */
    public List<int> LevelOrder() {
        List<int> res = [];
        // 直接遍历数组
        for (var i = 0; i < Size(); i++) {
            if (Val(i).HasValue)
                res.Add(Val(i)!.Value);
        }

        return res;
    }

    /* 深度优先遍历 */
    void Dfs(int i, string order, List<int> res) {
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
    public List<int> PreOrder() {
        List<int> res = [];
        Dfs(0, "pre", res);
        return res;
    }

    /* 中序遍历 */
    public List<int> InOrder() {
        List<int> res = [];
        Dfs(0, "in", res);
        return res;
    }

    /* 后序遍历 */
    public List<int> PostOrder() {
        List<int> res = [];
        Dfs(0, "post", res);
        return res;
    }
}
