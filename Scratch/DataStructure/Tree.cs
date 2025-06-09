// ReSharper disable InconsistentNaming
using System.Diagnostics.CodeAnalysis;

namespace Algorithm.DataStructure;
/// <summary>
/// Binary tree node
/// </summary>
/// <param name="x">node value</param>
public class TreeNode(int? x)
{
    public int? Val = x;
    public TreeNode? Left;
    public TreeNode? Right;
}

/// <summary>
/// BST（Binary Search Tree）
/// </summary>
public class BinarySearchTree
{
    public TreeNode? Root;

    public BinarySearchTree(TreeNode root)
    {
        this.Root = root;
    }

    public TreeNode? Search(int num)
    {
        TreeNode? cur = this.Root;
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
            // get the next node in the inorder traversal
            TreeNode? tmp = cur.Right;
            while (tmp.Left != null)
            {
                tmp = tmp.Left;
            }

            // delete tmp recursively
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
public class AvlTreeNode(int? x)
{
    public int? val = x;
    private int _height;
    public AvlTreeNode? Left;
    public AvlTreeNode? Right;

    private static int Height(AvlTreeNode? node)
    {
        // 空节点高度为 -1 ，叶节点高度为 0
        return node?._height ?? -1;
    }

    private static void UpdateHeight(AvlTreeNode node)
    {
        // 节点高度等于最高子树高度 + 1
        node._height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private static int BalanceFactor(AvlTreeNode? node)
    {
        if (node == null) return 0;
        // 节点平衡因子 = 左子树高度 - 右子树高度
        return Height(node.Left) - Height(node.Right);
        // tip: 设平衡因子为f，则一棵 AVL 树的任意节点的平衡因子皆满足 -1<=f<=1。
        //      就是说，AVL 树要求对每个节点，左右子树的高度差 最多为 1。
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
    private List<int?> _tree = [..arr];

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
