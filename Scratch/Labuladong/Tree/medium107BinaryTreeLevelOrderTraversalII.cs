namespace Scratch.Labuladong.Algorithms.BinaryTreeLevelOrderTraversalII;

// 107. Binary Tree Level Order Traversal II (Medium)
//
// Given the root of a binary tree, return the bottom-up level order traversal of its nodes'
// values. (i.e., from left to right, level by level from leaf to root).
//
// Example 1:
//
// Input: root = [3,9,20,null,null,15,7]
// Output: [[15,7],[9,20],[3]]
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
// - -1000 <= Node.val <= 1000
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
    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        var res = new LinkedList<IList<int>>();
        if (root == null) return res.ToList();

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            // 记录这一层的节点值
            var level = new List<int>();
            // for 循环控制每一层从左向右遍历
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                level.Add(cur.val);
                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            // 把每一层添加到头部，就是自底向上的层序遍历。
            res.AddFirst(level);
        }

        return res.ToList();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
