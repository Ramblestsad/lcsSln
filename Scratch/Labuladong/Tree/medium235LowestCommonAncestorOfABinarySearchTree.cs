namespace Scratch.Labuladong.Algorithms.LowestCommonAncestorOfABinarySearchTree;

// 235. Lowest Common Ancestor of a Binary Search Tree (Medium)
//
// Given a binary search tree (BST), find the lowest common ancestor (LCA) node of two given nodes
// in the BST.
//
// According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between
// two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow a
// node to be a descendant of itself).”
//
// Example 1:
//
// Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
// Output: 6
// Explanation: The LCA of nodes 2 and 8 is 6.
//
// Example 2:
//
// Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
// Output: 2
// Explanation: The LCA of nodes 2 and 4 is 2, since a node can be a descendant of itself according
// to the LCA definition.
//
// Example 3:
//
// Input: root = [2,1], p = 2, q = 1
// Output: 2
//
// Constraints:
//
// - The number of nodes in the tree is in the range [2, 10^5].
//
// - -10^9 <= Node.val <= 10^9
//
// - All Node.val are unique.
//
// - p != q
//
// - p and q will exist in the BST.
//
// Related Topics: Tree, Depth-First Search, Binary Search Tree, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode? LowestCommonAncestor(TreeNode? root, TreeNode p, TreeNode q)
    {
        if (root == null) return null;
        if (p.val > q.val)
        {
            // 保证 p.val <= q.val，便于后续情况讨论
            return LowestCommonAncestor(root, q, p);
        }

        if (root.val >= p.val && root.val <= q.val)
        {
            // p <= root <= q
            // 即 p 和 q 分别在 root 的左右子树，那么 root 就是 LCA
            return root;
        }

        if (root.val > q.val)
        {
            // p 和 q 都在 root 的左子树，那么 LCA 在左子树
            return LowestCommonAncestor(root.left, p, q);
        }
        else
        {
            // p 和 q 都在 root 的右子树，那么 LCA 在右子树
            return LowestCommonAncestor(root.right, p, q);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
