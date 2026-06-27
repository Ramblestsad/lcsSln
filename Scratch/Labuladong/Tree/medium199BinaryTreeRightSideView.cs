namespace Scratch.Labuladong.Algorithms.BinaryTreeRightSideView;

// 199. Binary Tree Right Side View (Medium)
//
// Given the root of a binary tree, imagine yourself standing on the right side of it, return the
// values of the nodes you can see ordered from top to bottom.
//
// Example 1:
//
// Input: root = [1,2,3,null,5,null,4]
//
// Output: [1,3,4]
//
// Explanation:
//
// Example 2:
//
// Input: root = [1,2,3,4,null,null,null,5]
//
// Output: [1,3,4,5]
//
// Explanation:
//
// Example 3:
//
// Input: root = [1,null,3]
//
// Output: [1,3]
//
// Example 4:
//
// Input: root = []
//
// Output: []
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 100].
//
// - -100 <= Node.val <= 100
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
    public IList<int> RightSideView(TreeNode? root)
    {
        var res = new List<int>();
        if (root == null) return res;

        // BFS 层序遍历，计算右侧视图
        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            // 每一层头部就是最右侧的元素
            var last = q.Peek();
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                // 控制每一层从右向左遍历
                if (cur.right != null) q.Enqueue(cur.right);
                if (cur.left != null) q.Enqueue(cur.left);
            }

            // 每一层的最后一个节点就是二叉树的右侧视图
            res.Add(last.val);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
