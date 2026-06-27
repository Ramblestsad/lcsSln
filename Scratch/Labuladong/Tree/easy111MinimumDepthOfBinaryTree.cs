namespace Scratch.Labuladong.Algorithms.MinimumDepthOfBinaryTree;

// 111. Minimum Depth of Binary Tree (Easy)
//
// Given a binary tree, find its minimum depth.
//
// The minimum depth is the number of nodes along the shortest path from the root node down to the
// nearest leaf node.
//
// Note: A leaf is a node with no children.
//
// Example 1:
//
// Input: root = [3,9,20,null,null,15,7]
// Output: 2
//
// Example 2:
//
// Input: root = [2,null,3,null,4,null,5,null,6]
// Output: 5
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 10^5].
//
// - -1000 <= Node.val <= 1000
//
// Related Topics: Tree, Depth-First Search, Breadth-First Search, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
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
public class Solution
{
    private int minDepth = int.MaxValue;
    private int curDepth = 0;

    public int MinDepth(TreeNode root)
    {
        // BFS 解法, 第一个叶子节点就可以返回

        if (root == null) return 0;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        // root 本身就是一层，depth 初始化为 1
        var depth = 1;

        while (q.Count != 0)
        {
            int _size = q.Count;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                // 判断是否到达叶子结点
                if (cur.left == null && cur.right == null)
                    return depth;
                // 将下一层节点加入队列
                if (cur.left != null)
                    q.Enqueue(cur.left);
                if (cur.right != null)
                    q.Enqueue(cur.right);
            }

            depth++;
        }

        return depth;
    }

    private void TraverseDFS(TreeNode? root)
    {
        if (root == null)
        {
            return;
        }

        // 前序位置进入节点时增加当前深度
        curDepth++;

        // 如果当前节点是叶子节点，更新最小深度
        if (root.left == null && root.right == null)
        {
            minDepth = Math.Min(minDepth, curDepth);
        }

        TraverseDFS(root.left);
        TraverseDFS(root.right);

        // 后序位置离开节点时减少当前深度
        curDepth--;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
