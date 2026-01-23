namespace Scratch.Labuladong.Algorithms.InvertBinaryTree;

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
    public TreeNode InvertTree(TreeNode root)
    {
        Traverse(root);
        return root;
    }

    private void Traverse(TreeNode? root)
    {
        if (root == null) return;

        // 前序位置
        // 每一个节点需要做的事就是交换它的左右子节点
        ( root.left, root.right ) = ( root.right, root.left );

        Traverse(root.left);
        Traverse(root.right);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
