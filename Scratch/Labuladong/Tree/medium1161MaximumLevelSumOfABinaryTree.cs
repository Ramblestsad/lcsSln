namespace Scratch.Labuladong.Algorithms.MaximumLevelSumOfABinaryTree;

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
    public int MaxLevelSum(TreeNode? root)
    {
        if (root == null) return 0;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        // 记录 BFS 走到的层数
        var depth = 1;
        // 记录元素和最大的那一行和最大元素和
        int res = 0, maxSum = int.MinValue;

        while (q.Count != 0)
        {
            var _size = q.Count;
            var levelSum = 0;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                levelSum += cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            if (levelSum > maxSum)
            {
                res = depth;
                maxSum = levelSum;
            }

            depth++;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
