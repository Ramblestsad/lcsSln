namespace Scratch.Labuladong.Algorithms.ConstructBinaryTreeFromPreorderAndInorderTraversal;

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
    // 存储 inorder 中值到索引的映射
    Dictionary<int, int> valToIndex = new();

    public TreeNode? BuildTree(int[] preorder, int[] inorder)
    {
        return new TreeNode();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
