namespace Scratch.Labuladong.Algorithms.LowestCommonAncestorOfABinarySearchTree;

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
