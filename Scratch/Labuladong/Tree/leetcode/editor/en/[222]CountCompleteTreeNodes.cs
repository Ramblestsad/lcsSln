namespace Scratch.Labuladong.Algorithms.CountCompleteTreeNodes;

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
    public int CountNodes(TreeNode? root)
    {
        TreeNode? l = root, r = root;
        // 记录左、右子树的高度
        int hl = 0, hr = 0;
        while (l != null)
        {
            l = l.left;
            hl++;
        }

        while (r != null)
        {
            r = r.right;
            hr++;
        }

        // 如果左右子树的高度相同，则是一棵满二叉树
        if (hl == hr)
        {
            return (int)Math.Pow(2, hl) - 1;
        }

        // 如果左右高度不同，则按照普通二叉树的逻辑计算
        return 1 + CountNodes(root!.left) + CountNodes(root.right);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
