namespace Scratch.Labuladong.Algorithms.ValidateBinarySearchTree;

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
