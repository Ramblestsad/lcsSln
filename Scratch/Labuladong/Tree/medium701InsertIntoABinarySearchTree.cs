/*
 * @lc app=leetcode id=701 lang=csharp
 * @lcpr version=30402
 *
 * [701] Insert Into A Binary Search Tree
 */

namespace Scratch.Labuladong.Algorithms.InsertIntoABinarySearchTree;

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
    public TreeNode InsertIntoBST(TreeNode? root, int val)
    {
        if (root == null) return new TreeNode(val);

        if (val < root.val) root.left = InsertIntoBST(root.left, val);
        if (val > root.val) root.right = InsertIntoBST(root.right, val);

        return root;
    }
}
// @lc code=end
