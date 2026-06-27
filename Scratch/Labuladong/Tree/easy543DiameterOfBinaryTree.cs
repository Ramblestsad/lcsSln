namespace Scratch.Labuladong.Algorithms.DiameterOfBinaryTree;

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
    private int maxDiameter = 0;

    public int DiameterOfBinaryTree(TreeNode root)
    {
        // 每一条二叉树的「Diameter」长度，就是一个节点的左右子树的最大深度之和。
        MaxDepth(root);

        return maxDiameter;
    }

    // 计算二叉树的最大深度
    private int MaxDepth(TreeNode? root)
    {
        if (root == null) return 0;

        var leftMax = MaxDepth(root.left);
        var rightMax = MaxDepth(root.right);

        // 后序位置，顺便计算最大直径
        var myDiameter = leftMax + rightMax;
        maxDiameter = Math.Max(maxDiameter, myDiameter);

        return 1 + Math.Max(leftMax, rightMax);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
