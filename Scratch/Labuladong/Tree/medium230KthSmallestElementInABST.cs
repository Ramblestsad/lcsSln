/*
 * @lc app=leetcode id=230 lang=csharp
 * @lcpr version=30402
 *
 * [230] Kth Smallest Element In ABST
 */

namespace Scratch.Labuladong.Algorithms.KthSmallestElementInABST;

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
    public int KthSmallest(TreeNode? root, int k)
    {
        // 利用 BST 的中序遍历特性
        Traverse(root, k);
        return res;
    }

    private int res = 0;
    private int rank = 0;

    private void Traverse(TreeNode? root, int k)
    {
        if (root == null) return;

        Traverse(root.left, k);
        // 中序位置
        rank++;
        if (rank == k)
        {
            res = root.val;
            return;
        }

        Traverse(root.right, k);
    }
}
// @lc code=end
