namespace Scratch.Labuladong.Algorithms.DeleteNodesAndReturnForest;

// 1110. Delete Nodes And Return Forest (Medium)
//
// Given the root of a binary tree, each node in the tree has a distinct value.
//
// After deleting all nodes with a value in to_delete, we are left with a forest (a disjoint union
// of trees).
//
// Return the roots of the trees in the remaining forest. You may return the result in any order.
//
// Example 1:
//
// Input: root = [1,2,3,4,5,6,7], to_delete = [3,5]
// Output: [[1,2,null,4],[6],[7]]
//
// Example 2:
//
// Input: root = [1,2,4,null,3], to_delete = [3]
// Output: [[1,2,4]]
//
// Constraints:
//
// - The number of nodes in the given tree is at most 1000.
//
// - Each node has a distinct value between 1 and 1000.
//
// - to_delete.length <= 1000
//
// - to_delete contains distinct values between 1 and 1000.
//
// Related Topics: Array, Hash Table, Tree, Depth-First Search, Binary Tree

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
