namespace Scratch.Labuladong.Algorithms.LowestCommonAncestorOfABinaryTree;

// 236. Lowest Common Ancestor of a Binary Tree (Medium)
//
// Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.
//
// According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between
// two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow a
// node to be a descendant of itself).”
//
// Example 1:
//
// Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
// Output: 3
// Explanation: The LCA of nodes 5 and 1 is 3.
//
// Example 2:
//
// Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
// Output: 5
// Explanation: The LCA of nodes 5 and 4 is 5, since a node can be a descendant of itself according
// to the LCA definition.
//
// Example 3:
//
// Input: root = [1,2], p = 1, q = 2
// Output: 1
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
// - p and q will exist in the tree.
//
// Related Topics: Tree, Depth-First Search, Binary Tree

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
    // 用一个外部变量来记录是否已经找到 LCA 节点
    TreeNode? lca = null; // [!code ++]

    /// 如果一个节点能够在它的左右子树中分别找到 p 和 q，则该节点为 LCA 节点。
    public TreeNode? LowestCommonAncestor(TreeNode? root, TreeNode p, TreeNode q)
    {
        // base case
        if (root == null) return null;

        // 如果已经找到 LCA 节点，直接返回
        // [!code ++:4]
        if (lca != null)
        {
            return null;
        }

        if (root == p || root == q) return root;

        var left = LowestCommonAncestor(root.left, p, q);
        var right = LowestCommonAncestor(root.right, p, q);

        if (left != null && right != null)
        {
            // 当前节点是 LCA 节点，记录下来
            lca = root; // [!code ++]
            return root;
        }

        return left ?? right;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
