namespace Scratch.Labuladong.Algorithms.CountCompleteTreeNodes;

// 222. Count Complete Tree Nodes (Easy)
//
// Given the root of a complete binary tree, return the number of the nodes in the tree.
//
// According to Wikipedia, every level, except possibly the last, is completely filled in a
// complete binary tree, and all nodes in the last level are as far left as possible. It can have
// between 1 and 2^h nodes inclusive at the last level h.
//
// Design an algorithm that runs in less than O(n) time complexity.
//
// Example 1:
//
// Input: root = [1,2,3,4,5,6]
// Output: 6
//
// Example 2:
//
// Input: root = []
// Output: 0
//
// Example 3:
//
// Input: root = [1]
// Output: 1
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 5 * 10^4].
//
// - 0 <= Node.val <= 5 * 10^4
//
// - The tree is guaranteed to be complete.
//
// Related Topics: Binary Search, Bit Manipulation, Tree, Binary Tree

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
    public int CountNodes(TreeNode? root)
    {
        TreeNode? l = root, r = root;
        // 记录左、右子树的高度
        int hl = 0, hr = 0;
        while (l != null)
        {
            l = l.left;
            hl++;
        }

        while (r != null)
        {
            r = r.right;
            hr++;
        }

        // 如果左右子树的高度相同，则是一棵满二叉树
        if (hl == hr)
        {
            return (int)Math.Pow(2, hl) - 1;
        }

        // 如果左右高度不同，则按照普通二叉树的逻辑计算
        return 1 + CountNodes(root!.left) + CountNodes(root.right);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
