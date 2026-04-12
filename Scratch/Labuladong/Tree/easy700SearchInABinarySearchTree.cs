/*
 * @lc app=leetcode id=700 lang=csharp
 * @lcpr version=30402
 *
 * [700] Search In A Binary Search Tree
 */

namespace Scratch.Labuladong.Algorithms.SearchInABinarySearchTree;

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
    public TreeNode? SearchBST(TreeNode? root, int val)
    {
        if (root == null) return null;

        if (val < root.val) return SearchBST(root.left, val);
        if (val > root.val) return SearchBST(root.right, val);

        return root;
    }
}
// @lc code=end
