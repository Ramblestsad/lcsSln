/*
 * @lc app=leetcode id=919 lang=csharp
 * @lcpr version=30402
 *
 * [919] Complete Binary Tree Inserter
 */

namespace Scratch.Labuladong.Algorithms.CompleteBinaryTreeInserter;

// @lc code=start
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class CBTInserter
{
    // 这个队列只记录完全二叉树底部可以进行插入的节点
    private Queue<TreeNode> q = new();
    private TreeNode root;

    public CBTInserter(TreeNode root)
    {
        this.root = root;
        // 进行普通的 BFS，目的是找到底部可插入的节点
        var temp = new Queue<TreeNode>();
        temp.Enqueue(root);
        while (temp.Count > 0)
        {
            var cur = temp.Dequeue();
            if (cur.left != null) temp.Enqueue(cur.left);
            if (cur.right != null) temp.Enqueue(cur.right);
            // 找到完全二叉树底部可以进行插入的节点
            if (cur.right == null || cur.left == null) q.Enqueue(cur);
        }
    }

    public int Insert(int val)
    {
        var node = new TreeNode(val);
        var cur = q.Peek();

        if (cur.left == null)
        {
            cur.left = node;
        }
        else if (cur.right == null)
        {
            // 右孩子也填上了，cur 已满，从队列移除
            cur.right = node;
            q.Dequeue();
        }

        // 新节点的左右节点也是可以插入的
        q.Enqueue(node);
        return cur.val;
    }

    public TreeNode Get_root()
    {
        return root;
    }
}

/**
 * Your CBTInserter object will be instantiated and called as such:
 * CBTInserter obj = new CBTInserter(root);
 * int param_1 = obj.Insert(val);
 * TreeNode param_2 = obj.Get_root();
 */
// @lc code=end
