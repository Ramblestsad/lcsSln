namespace Scratch.Labuladong.Algorithms.FindLargestValueInEachTreeRow;

// 515. Find Largest Value in Each Tree Row (Medium)
//
// Given the root of a binary tree, return an array of the largest value in each row of the tree
// (0-indexed).
//
// Example 1:
//
// Input: root = [1,3,2,5,3,null,9]
// Output: [1,3,9]
//
// Example 2:
//
// Input: root = [1,2,3]
// Output: [1,3]
//
// Constraints:
//
// - The number of nodes in the tree will be in the range [0, 10^4].
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
    public IList<int> LargestValues(TreeNode root)
    {
        var res = new List<int>();
        if (root == null) return res;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            var levelMax = int.MinValue;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                if (cur.val > levelMax) levelMax = cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            res.Add(levelMax);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
