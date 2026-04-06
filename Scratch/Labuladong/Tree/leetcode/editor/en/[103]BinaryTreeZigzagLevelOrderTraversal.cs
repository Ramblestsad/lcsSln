/*
 * @lc app=leetcode id=103 lang=csharp
 * @lcpr version=30402
 *
 * [103] Binary Tree Zigzag Level Order Traversal
 */

namespace Scratch.Labuladong.Algorithms.BinaryTreeZigzagLevelOrderTraversal;

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
public class Solution
{
    public IList<IList<int>> ZigzagLevelOrder(TreeNode? root)
    {
        var res = new List<IList<int>>();
        if (root == null) return res;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        // 为 true 时向右，false 时向左
        var flag = true;


        while (q.Count != 0)
        {
            var _size = q.Count;
            var level = new LinkedList<int>();
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                // z iterate
                if (flag)
                {
                    level.AddLast(cur.val);
                }
                else
                {
                    level.AddFirst(cur.val);
                }

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            flag = !flag;
            res.Add(level.ToList());
        }

        return res;
    }
}
// @lc code=end
