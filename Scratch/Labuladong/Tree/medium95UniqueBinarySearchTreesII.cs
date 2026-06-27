namespace Scratch.Labuladong.Algorithms.UniqueBinarySearchTreesII;

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
    public IList<TreeNode?> GenerateTrees(int n)
    {
        if (n == 0) return new List<TreeNode?>();

        return _build(1, n);
    }

    // 构造闭区间 [lo, hi] 组成的 BST
    private List<TreeNode?> _build(int lo, int hi)
    {
        var res = new List<TreeNode?>();
        if (lo > hi)
        {
            res.Add(null);
            return res;
        }

        // 1、穷举 root 节点的所有可能。
        for (int i = lo; i <= hi; i++)
        {
            // 2、递归构造出左右子树的所有合法 BST。
            var leftTree = _build(lo, i - 1);
            var rightTree = _build(i + 1, hi);

            // 3、给 root 节点穷举所有左右子树的组合。
            foreach (var left in leftTree)
            {
                foreach (var right in rightTree)
                {
                    // i 作为根节点 root 的值
                    var root = new TreeNode(i);
                    root.left = left;
                    root.right = right;
                    res.Add(root);
                }
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
