namespace Scratch.Labuladong.Algorithms.DiameterOfBinaryTree;

// 543. Diameter of Binary Tree (Easy)
//
// Given the root of a binary tree, return the length of the diameter of the tree.
//
// The diameter of a binary tree is the length of the longest path between any two nodes in a tree.
// This path may or may not pass through the root.
//
// The length of a path between two nodes is represented by the number of edges between them.
//
// Example 1:
//
// Input: root = [1,2,3,4,5]
// Output: 3
// Explanation: 3 is the length of the path [4,2,1,3] or [5,2,1,3].
//
// Example 2:
//
// Input: root = [1,2]
// Output: 1
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^4].
//
// - -100 <= Node.val <= 100
//
// Related Topics: Tree, Depth-First Search, Binary Tree

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
    private int maxDiameter = 0;

    public int DiameterOfBinaryTree(TreeNode root)
    {
        // 每一条二叉树的「Diameter」长度，就是一个节点的左右子树的最大深度之和。
        MaxDepth(root);

        return maxDiameter;
    }

    // 计算二叉树的最大深度
    private int MaxDepth(TreeNode? root)
    {
        if (root == null) return 0;

        var leftMax = MaxDepth(root.left);
        var rightMax = MaxDepth(root.right);

        // 后序位置，顺便计算最大直径
        var myDiameter = leftMax + rightMax;
        maxDiameter = Math.Max(maxDiameter, myDiameter);

        return 1 + Math.Max(leftMax, rightMax);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
