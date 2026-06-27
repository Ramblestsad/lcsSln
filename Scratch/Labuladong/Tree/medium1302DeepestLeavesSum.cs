namespace Scratch.Labuladong.Algorithms.DeepestLeavesSum;

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
