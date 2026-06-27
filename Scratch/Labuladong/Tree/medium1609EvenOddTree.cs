namespace Scratch.Labuladong.Algorithms.EvenOddTree;

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
    public bool IsEvenOddTree(TreeNode? root)
    {
        if (root == null) return true;

        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        // 层数从0开始，即偶数层，判断奇数特征
        var even = true;
        while (q.Count != 0)
        {
            var _size = q.Count;
            // 记录前一个节点，便于判断是否递增/递减
            var prev = even ? int.MinValue : int.MaxValue;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                if (even)
                {
                    // 偶数层，判断奇数特征
                    if (prev >= cur.val || cur.val % 2 == 0) return false;
                }
                else
                {
                    if (prev <= cur.val || cur.val % 2 != 0) return false;
                }

                prev = cur.val;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }

            // 下一层，切换奇偶层
            even = !even;
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
