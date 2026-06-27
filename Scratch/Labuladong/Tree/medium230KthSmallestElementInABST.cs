namespace Scratch.Labuladong.Algorithms.KthSmallestElementInABST;

// 230. Kth Smallest Element in a BST (Medium)
//
// Given the root of a binary search tree, and an integer k, return the k^th smallest value
// (1-indexed) of all the values of the nodes in the tree.
//
// Example 1:
//
// Input: root = [3,1,4,null,2], k = 1
// Output: 1
//
// Example 2:
//
// Input: root = [5,3,6,2,4,null,null,1], k = 3
// Output: 3
//
// Constraints:
//
// - The number of nodes in the tree is n.
//
// - 1 <= k <= n <= 10^4
//
// - 0 <= Node.val <= 10^4
//
// Follow up: If the BST is modified often (i.e., we can do insert and delete operations) and you
// need to find the kth smallest frequently, how would you optimize?
//
// Related Topics: Tree, Depth-First Search, Binary Search Tree, Binary Tree

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
    public int KthSmallest(TreeNode? root, int k)
    {
        // 利用 BST 的中序遍历特性
        Traverse(root, k);
        return res;
    }

    private int res = 0;
    private int rank = 0;

    private void Traverse(TreeNode? root, int k)
    {
        if (root == null) return;

        Traverse(root.left, k);
        // 中序位置
        rank++;
        if (rank == k)
        {
            res = root.val;
            return;
        }

        Traverse(root.right, k);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
