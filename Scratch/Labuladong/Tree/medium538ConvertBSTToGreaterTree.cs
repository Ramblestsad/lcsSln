namespace Scratch.Labuladong.Algorithms.ConvertBSTToGreaterTree;

// 538. Convert BST to Greater Tree (Medium)
//
// Given the root of a Binary Search Tree (BST), convert it to a Greater Tree such that every key
// of the original BST is changed to the original key plus the sum of all keys greater than the
// original key in BST.
//
// As a reminder, a binary search tree is a tree that satisfies these constraints:
//
// - The left subtree of a node contains only nodes with keys less than the node's key.
//
// - The right subtree of a node contains only nodes with keys greater than the node's key.
//
// - Both the left and right subtrees must also be binary search trees.
//
// Example 1:
//
// Input: root = [4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]
// Output: [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
//
// Example 2:
//
// Input: root = [0,null,1]
// Output: [1,null,1]
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 10^4].
//
// - -10^4 <= Node.val <= 10^4
//
// - All the values in the tree are unique.
//
// - root is guaranteed to be a valid binary search tree.
//
// Note: This question is the same as 1038:
// https://leetcode.com/problems/binary-search-tree-to-greater-sum-tree/
//
// Related Topics: Tree, Depth-First Search, Binary Search Tree, Binary Tree

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
    public TreeNode ConvertBST(TreeNode root)
    {
        Traverse(root);
        return root;
    }

    private int sum = 0;

    private void Traverse(TreeNode? root)
    {
        if (root == null) return;

        // 先递归右子树，中序遍历就是降序
        Traverse(root.right);
        // 中序位置
        sum += root.val;
        root.val = sum;
        Traverse(root.left);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
