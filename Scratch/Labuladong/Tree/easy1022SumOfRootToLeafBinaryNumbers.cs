namespace Scratch.Labuladong.Algorithms.SumOfRootToLeafBinaryNumbers;

// 1022. Sum of Root To Leaf Binary Numbers (Easy)
//
// You are given the root of a binary tree where each node has a value 0 or 1. Each root-to-leaf
// path represents a binary number starting with the most significant bit.
//
// - For example, if the path is 0 -> 1 -> 1 -> 0 -> 1, then this could represent 01101 in binary,
// which is 13.
//
// For all leaves in the tree, consider the numbers represented by the path from the root to that
// leaf. Return the sum of these numbers.
//
// The test cases are generated so that the answer fits in a 32-bits integer.
//
// Example 1:
//
// Input: root = [1,0,1,0,1,0,1]
// Output: 22
// Explanation: (100) + (101) + (110) + (111) = 4 + 5 + 6 + 7 = 22
//
// Example 2:
//
// Input: root = [0]
// Output: 0
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 1000].
//
// - Node.val is 0 or 1.
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
    private int path = 0;
    private int res = 0;

    public int SumRootToLeaf(TreeNode root)
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
            res += path << 1 | node.val;
            return;
        }

        path = path << 1 | node.val;
        Traverse(node.left);
        Traverse(node.right);
        path = path >> 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
