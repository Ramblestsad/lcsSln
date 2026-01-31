namespace Scratch.Labuladong.Algorithms.MaximumBinaryTreeII;

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
    public TreeNode InsertIntoMaxTree(TreeNode? root, int val)
    {
        if (root == null)
        {
            return new TreeNode(val);
        }

        if (root.val < val)
        {
            // 如果 val 是整棵树最大的，那么原来的这棵树应该是 val 节点的左子树，
            // 因为 val 节点是接在原始数组 a 的最后一个元素
            var tmp = root;
            root = new TreeNode(val);
            root.left = tmp;
        }
        else
        {
            // 如果 val 不是最大的，那么就应该在右子树上，
            // 因为 val 节点是接在原始数组 a 的最后一个元素
            root.right = InsertIntoMaxTree(root.right, val);
        }

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
