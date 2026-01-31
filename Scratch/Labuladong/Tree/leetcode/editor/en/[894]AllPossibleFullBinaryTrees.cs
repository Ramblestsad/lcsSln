namespace Scratch.Labuladong.Algorithms.AllPossibleFullBinaryTrees;

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
    // 备忘录，记录 n 个节点能够组合成的所有可能二叉树
    List<TreeNode>[] memo = null!;

    // 想生成一棵 n 个节点的满二叉树，首先要固定根节点
    // 然后组装左右子树
    // 根节点加上左右子树节点之和应该等于 n
    public IList<TreeNode> AllPossibleFBT(int n)
    {
        if (n % 2 == 0) return new List<TreeNode>();

        memo = new List<TreeNode>[n + 1];

        return _build(n);
    }

    private List<TreeNode> _build(int n)
    {
        var res = new List<TreeNode>();

        // base case
        if (n == 1)
        {
            res.Add(new TreeNode(0));

            return res;
        }

        if (memo[n] != null)
        {
            // 避免冗余计算
            return memo[n];
        }

        // 递归生成所有符合条件的左右子树
        // 要构造一棵有 (n) 个节点的满二叉树：
        // 根节点占 1 个节点
        // 剩下 (n - 1) 个节点必须分给左子树和右子树
        // 设左子树用 (i) 个节点，则右子树用 (j = n - 1 - i) 个节点
        // 因为左右子树本身也必须是满二叉树，所以它们的节点数也必须是奇数。
        // 所以 (i) 只枚举奇数：`for (i = 1; i < n; i += 2)`
        for (int i = 1; i < n; i += 2)
        {
            // i, j 最终会在 n-1 的空间内，各自等于不同的奇数（对应不同情况的子树组合）
            var j = n - i - 1;
            // 生成左右子树
            var leftSubTrees = _build(i);
            var rightSubTrees = _build(j);
            // 左右子树的不同排列也能构成不同的二叉树
            foreach (var left in leftSubTrees)
            {
                foreach (var right in rightSubTrees)
                {
                    // 生成根节点
                    var root = new TreeNode(0);
                    // 组装出一种可能的二叉树形状
                    root.left = Clone(left);
                    root.right = Clone(right);
                    // 加入结果列表
                    res.Add(root);
                }
            }
        }

        memo[n] = res;

        return res;
    }

    private TreeNode? Clone(TreeNode? node)
    {
        if (node == null) return null;

        return new TreeNode(node.val) { left = Clone(node.left), right = Clone(node.right) };
    }
}
//leetcode submit region end(Prohibit modification and deletion)
