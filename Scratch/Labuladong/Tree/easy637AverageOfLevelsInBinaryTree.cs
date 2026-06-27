namespace Scratch.Labuladong.Algorithms.AverageOfLevelsInBinaryTree;

// 637. Average of Levels in Binary Tree (Easy)
//
// Given the root of a binary tree, return the average value of the nodes on each level in the form
// of an array. Answers within 10^-5 of the actual answer will be accepted.
//
// Example 1:
//
// Input: root = [3,9,20,null,null,15,7]
// Output: [3.00000,14.50000,11.00000]
// Explanation: The average value of nodes on level 0 is 3, on level 1 is 14.5, and on level 2 is
// 11.
// Hence return [3, 14.5, 11].
//
// Example 2:
//
// Input: root = [3,9,20,15,7]
// Output: [3.00000,14.50000,11.00000]
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^4].
//
// - -2^31 <= Node.val <= 2^31 - 1
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
    public IList<double> AverageOfLevels(TreeNode? root)
    {
        var res = new List<double>();
        if (root == null) return res;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            var levelSum = 0d;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                levelSum += cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            res.Add(levelSum / _size);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
