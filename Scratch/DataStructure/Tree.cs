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
    public static TreeNode? Search(TreeNode root, int num)
    {
        TreeNode? cur = root;
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

    public static TreeNode? Insert(TreeNode root, int x)
    {
        throw new NotImplementedException();
    }

    public static TreeNode? Remove(TreeNode root, int x)
    {
        throw new NotImplementedException();
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
