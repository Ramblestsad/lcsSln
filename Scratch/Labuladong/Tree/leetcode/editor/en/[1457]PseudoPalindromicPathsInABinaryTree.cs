namespace Scratch.Labuladong.Algorithms.PseudoPalindromicPathsInABinaryTree;

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
    // 计数数组，题目说了 1 <= root.val <= 9
    int[] count = new int[10];
    int res = 0;

    public int PseudoPalindromicPaths(TreeNode root)
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
            count[node.val]++;
            // 如果路径上出现奇数次的数字个数大于 1，
            // 则不可能组成回文串，反之则可以组成回文串
            var odd = 0;
            foreach (var n in count)
            {
                if (n % 2 == 1) odd++;
            }

            if (odd <= 1) res++;

            count[node.val]--;
            return;
        }

        count[node.val]++;
        Traverse(node.left);
        Traverse(node.right);
        count[node.val]--;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
