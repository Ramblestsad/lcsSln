namespace Scratch.Labuladong.Algorithms.BinaryTreeRightSideView;

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
