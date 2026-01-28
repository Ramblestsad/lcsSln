using System.Text;

namespace Scratch.Labuladong.Algorithms.SmallestStringStartingFromLeaf;

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
    // 遍历过程中的路径
    StringBuilder path = new();
    string? res = null;

    public string? SmallestFromLeaf(TreeNode root)
    {
        Traverse(root);

        return res;
    }

    private void Traverse(TreeNode? node)
    {
        if (node == null) return;

        // 如果是叶子节点
        if (node.left == null && node.right == null)
        {
            // 结果字符串是从叶子向根，所以需要反转
            path.Append((char)( 'a' + node.val ));
            var chars = path.ToString().ToCharArray();
            Array.Reverse(chars);
            var candidate = new string(chars);

            if (res == null || res?.CompareTo(candidate) > 0) res = candidate;

            // 恢复，正确维护 path 中的元素
            path.Remove(path.Length - 1, 1);
            return;
        }

        // 前序
        path.Append((char)( 'a' + node.val ));
        Traverse(node.left);
        Traverse(node.right);
        // 后序，回溯
        path.Remove(path.Length - 1, 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
