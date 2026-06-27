namespace Scratch.Labuladong.Algorithms.ConvertBSTToGreaterTree;

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
