namespace Scratch.Labuladong.Algorithms.InvertBinaryTree;

// 226. Invert Binary Tree (Easy)
//
// Given the root of a binary tree, invert the tree, and return its root.
//
// Example 1:
//
// Input: root = [4,2,7,1,3,6,9]
// Output: [4,7,2,9,6,3,1]
//
// Example 2:
//
// Input: root = [2,1,3]
// Output: [2,3,1]
//
// Example 3:
//
// Input: root = []
// Output: []
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 100].
//
// - -100 <= Node.val <= 100
//
// Related Topics: Tree, Depth-First Search, Breadth-First Search, Binary Tree

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
    public TreeNode InvertTree(TreeNode root)
    {
        Traverse(root);
        return root;
    }

    private void Traverse(TreeNode? root)
    {
        if (root == null) return;

        // 前序位置
        // 每一个节点需要做的事就是交换它的左右子节点
        ( root.left, root.right ) = ( root.right, root.left );

        Traverse(root.left);
        Traverse(root.right);
    }

    public TreeNode? RecurInvertTree(TreeNode? root)
    {
        if (root == null) return null;

        // 利用函数定义，先翻转左右子树
        var left = RecurInvertTree(root.left);
        var right = RecurInvertTree(root.right);

        // 然后交换左右子节点
        root.left = right;
        root.right = left;

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
