namespace Scratch.Labuladong.Algorithms.SumOfRootToLeafBinaryNumbers;

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
    private int path = 0;
    private int res = 0;

    public int SumRootToLeaf(TreeNode root)
    {
        Traverse(root);

        return res;
    }

    private void Traverse(TreeNode? node)
    {
        if (node == null) return;

        if (node.left == null && node.right == null)
        {
            // 叶子节点
            res += path << 1 | node.val;
            return;
        }

        path = path << 1 | node.val;
        Traverse(node.left);
        Traverse(node.right);
        path = path >> 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
