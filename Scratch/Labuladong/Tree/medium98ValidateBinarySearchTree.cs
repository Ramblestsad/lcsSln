namespace Scratch.Labuladong.Algorithms.ValidateBinarySearchTree;

// 98. Validate Binary Search Tree (Medium)
//
// Given the root of a binary tree, determine if it is a valid binary search tree (BST).
//
// A valid BST is defined as follows:
//
// - The left subtree of a node contains only nodes with keys strictly less than the node's key.
//
// - The right subtree of a node contains only nodes with keys strictly greater than the node's
// key.
//
// - Both the left and right subtrees must also be binary search trees.
//
// Example 1:
//
// Input: root = [2,1,3]
// Output: true
//
// Example 2:
//
// Input: root = [5,1,4,null,null,3,6]
// Output: false
// Explanation: The root node's value is 5 but its right child's value is 4.
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^4].
//
// - -2^31 <= Node.val <= 2^31 - 1
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
    public bool IsValidBST(TreeNode root)
    {
        return _isValidBST(root, null, null);
    }

    // 限定以 root 为根的子树节点必须满足 max.val > root.val > min.val
    private bool _isValidBST(TreeNode? root, TreeNode? smallTreeNode, TreeNode? bigTreeNode)
    {
        if (root == null) return true;

        // 若 root.val 不符合 max 和 min 的限制，说明不是合法 BST
        if (smallTreeNode != null && root.val <= smallTreeNode.val) return false;
        if (bigTreeNode != null && root.val >= bigTreeNode.val) return false;

        // 限定左子树的最大值是 root.val，右子树的最小值是 root.val
        return _isValidBST(root.left, smallTreeNode, root)
               && _isValidBST(root.right, root, bigTreeNode);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
