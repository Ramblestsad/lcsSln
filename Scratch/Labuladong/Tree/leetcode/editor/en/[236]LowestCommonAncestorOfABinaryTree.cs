namespace Scratch.Labuladong.Algorithms.LowestCommonAncestorOfABinaryTree;

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
