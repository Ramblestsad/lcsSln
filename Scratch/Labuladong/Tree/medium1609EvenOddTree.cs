namespace Scratch.Labuladong.Algorithms.EvenOddTree;

// 1609. Even Odd Tree (Medium)
//
// A binary tree is named Even-Odd if it meets the following conditions:
//
// - The root of the binary tree is at level index 0, its children are at level index 1, their
// children are at level index 2, etc.
//
// - For every even-indexed level, all nodes at the level have odd integer values in strictly
// increasing order (from left to right).
//
// - For every odd-indexed level, all nodes at the level have even integer values in strictly
// decreasing order (from left to right).
//
// Given the root of a binary tree, return true if the binary tree is Even-Odd, otherwise return
// false.
//
// Example 1:
//
// Input: root = [1,10,4,3,null,7,9,12,8,6,null,null,2]
// Output: true
// Explanation: The node values on each level are:
// Level 0: [1]
// Level 1: [10,4]
// Level 2: [3,7,9]
// Level 3: [12,8,6,2]
// Since levels 0 and 2 are all odd and increasing and levels 1 and 3 are all even and decreasing,
// the tree is Even-Odd.
//
// Example 2:
//
// Input: root = [5,4,2,3,3,7]
// Output: false
// Explanation: The node values on each level are:
// Level 0: [5]
// Level 1: [4,2]
// Level 2: [3,3,7]
// Node values in level 2 must be in strictly increasing order, so the tree is not Even-Odd.
//
// Example 3:
//
// Input: root = [5,9,1,3,5,7]
// Output: false
// Explanation: Node values in the level 1 should be even integers.
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^5].
//
// - 1 <= Node.val <= 10^6
//
// Related Topics: Tree, Breadth-First Search, Binary Tree

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
    public bool IsEvenOddTree(TreeNode? root)
    {
        if (root == null) return true;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        // 层数从0开始，即偶数层，判断奇数特征
        var even = true;
        while (q.Count != 0)
        {
            var _size = q.Count;
            // 记录前一个节点，便于判断是否递增/递减
            var prev = even ? int.MinValue : int.MaxValue;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                if (even)
                {
                    // 偶数层，判断奇数特征
                    if (prev >= cur.val || cur.val % 2 == 0) return false;
                }
                else
                {
                    if (prev <= cur.val || cur.val % 2 != 0) return false;
                }

                prev = cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            // 下一层，切换奇偶层
            even = !even;
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
