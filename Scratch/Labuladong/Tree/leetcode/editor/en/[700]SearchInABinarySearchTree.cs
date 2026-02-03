namespace Scratch.Labuladong.Algorithms.SearchInABinarySearchTree;

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
    public TreeNode? SearchBST(TreeNode? root, int val)
    {
        if (root == null) return null;

        if (val < root.val) return SearchBST(root.left, val);
        if (val > root.val) return SearchBST(root.right, val);

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
