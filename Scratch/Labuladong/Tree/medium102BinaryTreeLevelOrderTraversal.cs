namespace Scratch.Labuladong.Algorithms.BinaryTreeLevelOrderTraversal;

// 102. Binary Tree Level Order Traversal (Medium)
//
// Given the root of a binary tree, return the level order traversal of its nodes' values. (i.e.,
// from left to right, level by level).
//
// Example 1:
//
// Input: root = [3,9,20,null,null,15,7]
// Output: [[3],[9,20],[15,7]]
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
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var res = new List<IList<int>>();
        if (root == null)
        {
            return res;
        }

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            // 记录这一层的节点值
            var level = new List<int>();
            for (int i = 0; i < _size; i++)
            {
                var curNode = q.Dequeue();
                level.Add(curNode.val);
                if (curNode.left != null) q.Enqueue(curNode.left);
                if (curNode.right != null) q.Enqueue(curNode.right);
            }

            res.Add(level);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
