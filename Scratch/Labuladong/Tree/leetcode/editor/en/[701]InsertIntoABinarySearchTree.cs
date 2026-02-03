namespace Scratch.Labuladong.Algorithms.InsertIntoABinarySearchTree;

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
    public TreeNode InsertIntoBST(TreeNode? root, int val)
    {
        if (root == null) return new TreeNode(val);

        if (val < root.val) root.left = InsertIntoBST(root.left, val);
        if (val > root.val) root.right = InsertIntoBST(root.right, val);

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
