using System.Text;

namespace Scratch.Labuladong.Algorithms.SumRootToLeafNumbers;

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
    StringBuilder path = new StringBuilder();
    int res = 0;

    public int SumNumbers(TreeNode root)
    {
        Traverse(root);

        return res;
    }

    private void Traverse(TreeNode? root)
    {
        if (root == null) return;

        // 前序遍历位置，记录节点值
        path.Append(root.val);
        if (root.left == null && root.right == null)
        {
            // 到达叶子节点，累加路径和
            res += int.Parse(path.ToString());
        }

        Traverse(root.left);
        Traverse(root.right);

        // 后续遍历位置，撤销节点值
        path.Remove(path.Length - 1, 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
