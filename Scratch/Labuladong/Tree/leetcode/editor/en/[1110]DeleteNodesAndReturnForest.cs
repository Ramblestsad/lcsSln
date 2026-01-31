namespace Scratch.Labuladong.Algorithms.DeleteNodesAndReturnForest;

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
    HashSet<int> delSet = [];

    // 记录森林的根节点
    List<TreeNode> res = [];

    public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
    {
        if (root == null) return new List<TreeNode>();
        foreach (var d in to_delete)
        {
            delSet.Add(d);
        }

        _doDel(root, false);

        return res;
    }

    // 定义：输入一棵二叉树，删除 delSet 中的节点，返回删除完成后的根节点
    private TreeNode? _doDel(TreeNode? node, bool hasParent)
    {
        if (node == null) return null;

        var deleted = delSet.Contains(node.val);
        if (!deleted && !hasParent)
        {
            // 没有父节点且不需要被删除，就是一个新的根节点
            res.Add(node);
        }

        // 去左右子树进行删除
        // 这里传 !deleted 的原因：
        // 若当前节点被删（deleted == true），子节点在结果里就没有父节点了，所以传 hasParent = false，子节点若不删会变成新根。
        // 若当前节点不删，子节点仍然有父节点连接着，所以传 hasParent = true。
        node.left = _doDel(node.left, !deleted);
        node.right = _doDel(node.right, !deleted);

        // 如果需要被删除，返回 null 给父节点
        return deleted ? null : node;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
