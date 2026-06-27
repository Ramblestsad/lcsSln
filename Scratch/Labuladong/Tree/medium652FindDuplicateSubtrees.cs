namespace Scratch.Labuladong.Algorithms.FindDuplicateSubtrees;

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
    // 记录所有子树
    private Dictionary<string, int> memo = new();

    // 记录重复的子树根节点
    private List<TreeNode> res = new();

    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        Traverse(root);

        return res;
    }

    string? Traverse(TreeNode? root)
    {
        if (root == null) return null;

        var leftString = Traverse(root.left);
        var rightString = Traverse(root.right);
        // 后序位置
        var subTree = leftString + "," + rightString + "," + root.val;

        memo.TryGetValue(subTree, out var freq);
        // 多次重复也只会被加入结果集一次
        if (freq == 1) res.Add(root);
        // 给子树对应的出现次数加一
        memo[subTree] = freq + 1;

        return subTree;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
