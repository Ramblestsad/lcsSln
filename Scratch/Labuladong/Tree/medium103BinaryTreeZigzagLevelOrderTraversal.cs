namespace Scratch.Labuladong.Algorithms.BinaryTreeZigzagLevelOrderTraversal;

// 103. Binary Tree Zigzag Level Order Traversal (Medium)
//
// Given the root of a binary tree, return the zigzag level order traversal of its nodes' values.
// (i.e., from left to right, then right to left for the next level and alternate between).
//
// Example 1:
//
// Input: root = [3,9,20,null,null,15,7]
// Output: [[3],[20,9],[15,7]]
//
// Example 2:
//
// Input: root = [1]
// Output: [[1]]
//
// Example 3:
//
// Input: root = []
// Output: []
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 2000].
//
// - -100 <= Node.val <= 100
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
    public IList<IList<int>> ZigzagLevelOrder(TreeNode? root)
    {
        var res = new List<IList<int>>();
        if (root == null) return res;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        // 为 true 时向右，false 时向左
        var flag = true;


        while (q.Count != 0)
        {
            var _size = q.Count;
            var level = new LinkedList<int>();
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                // z iterate
                if (flag)
                {
                    level.AddLast(cur.val);
                }
                else
                {
                    level.AddFirst(cur.val);
                }

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            flag = !flag;
            res.Add(level.ToList());
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
