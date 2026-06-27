namespace Scratch.Labuladong.Algorithms.PseudoPalindromicPathsInABinaryTree;

// 1457. Pseudo-Palindromic Paths in a Binary Tree (Medium)
//
// Given a binary tree where node values are digits from 1 to 9. A path in the binary tree is said
// to be pseudo-palindromic if at least one permutation of the node values in the path is a
// palindrome.
//
// Return the number of pseudo-palindromic paths going from the root node to leaf nodes.
//
// Example 1:
//
// Input: root = [2,3,1,3,1,null,1]
// Output: 2
// Explanation: The figure above represents the given binary tree. There are three paths going from
// the root node to leaf nodes: the red path [2,3,3], the green path [2,1,1], and the path [2,3,1].
// Among these paths only red path and green path are pseudo-palindromic paths since the red path
// [2,3,3] can be rearranged in [3,2,3] (palindrome) and the green path [2,1,1] can be rearranged
// in [1,2,1] (palindrome).
//
// Example 2:
//
// Input: root = [2,1,1,1,3,null,null,null,null,null,1]
// Output: 1
// Explanation: The figure above represents the given binary tree. There are three paths going from
// the root node to leaf nodes: the green path [2,1,1], the path [2,1,3,1], and the path [2,1].
// Among these paths only the green path is pseudo-palindromic since [2,1,1] can be rearranged in
// [1,2,1] (palindrome).
//
// Example 3:
//
// Input: root = [9]
// Output: 1
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^5].
//
// - 1 <= Node.val <= 9
//
// Related Topics: Bit Manipulation, Tree, Depth-First Search, Breadth-First Search, Binary Tree

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
    // 计数数组，题目说了 1 <= root.val <= 9
    int[] count = new int[10];
    int res = 0;

    public int PseudoPalindromicPaths(TreeNode root)
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
            count[node.val]++;
            // 如果路径上出现奇数次的数字个数大于 1，
            // 则不可能组成回文串，反之则可以组成回文串
            var odd = 0;
            foreach (var n in count)
            {
                if (n % 2 == 1) odd++;
            }

            if (odd <= 1) res++;

            count[node.val]--;
            return;
        }

        count[node.val]++;
        Traverse(node.left);
        Traverse(node.right);
        count[node.val]--;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
