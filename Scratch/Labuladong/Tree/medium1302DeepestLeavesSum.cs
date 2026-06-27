namespace Scratch.Labuladong.Algorithms.DeepestLeavesSum;

// 1302. Deepest Leaves Sum (Medium)
//
// Given the root of a binary tree, return the sum of values of its deepest leaves.
//
// Example 1:
//
// Input: root = [1,2,3,4,5,null,6,7,null,null,null,null,8]
// Output: 15
//
// Example 2:
//
// Input: root = [6,7,8,2,7,1,3,9,null,1,4,null,null,null,5]
// Output: 19
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 10^4].
//
// - 1 <= Node.val <= 100
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
    public int DeepestLeavesSum(TreeNode? root)
    {
        if (root == null) return 0;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        var sum = 0;
        while (q.Count != 0)
        {
            sum = 0;
            var _size = q.Count;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                sum += cur.val;
                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }
        }

        return sum;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
