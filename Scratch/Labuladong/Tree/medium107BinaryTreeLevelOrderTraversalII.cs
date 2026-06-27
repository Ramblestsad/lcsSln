namespace Scratch.Labuladong.Algorithms.BinaryTreeLevelOrderTraversalII;

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
