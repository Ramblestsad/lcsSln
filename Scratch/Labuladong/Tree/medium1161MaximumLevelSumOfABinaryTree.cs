namespace Scratch.Labuladong.Algorithms.MaximumLevelSumOfABinaryTree;

// 1161. Maximum Level Sum of a Binary Tree (Medium)
//
// Given the root of a binary tree, the level of its root is 1, the level of its children is 2, and
// so on.
//
// Return the smallest level x such that the sum of all the values of nodes at level x is maximal.
//
// Example 1:
//
// Input: root = [1,7,0,7,-8,null,null]
// Output: 2
// Explanation:
// Level 1 sum = 1.
// Level 2 sum = 7 + 0 = 7.
// Level 3 sum = 7 + -8 = -1.
// So we return the level with the maximum sum which is level 2.
//
// Example 2:
//
// Input: root = [989,null,10250,98693,-89388,null,null,null,-32127]
// Output: 2
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^4].
//
// - -10^5 <= Node.val <= 10^5
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
    public int MaxLevelSum(TreeNode? root)
    {
        if (root == null) return 0;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        // 记录 BFS 走到的层数
        var depth = 1;
        // 记录元素和最大的那一行和最大元素和
        int res = 0, maxSum = int.MinValue;

        while (q.Count != 0)
        {
            var _size = q.Count;
            var levelSum = 0;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                levelSum += cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            if (levelSum > maxSum)
            {
                res = depth;
                maxSum = levelSum;
            }

            depth++;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
