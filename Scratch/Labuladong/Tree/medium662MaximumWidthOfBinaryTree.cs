namespace Scratch.Labuladong.Algorithms.MaximumWidthOfBinaryTree;

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
    // 记录节点和对应编号
    internal class Pair
    {
        internal TreeNode node;
        internal int id;

        internal Pair(TreeNode node, int id)
        {
            this.node = node;
            this.id = id;
        }
    }

    public int WidthOfBinaryTree(TreeNode? root)
    {
        if (root == null) return 0;

        var maxWidth = 0;
        var q = new Queue<Pair>();
        q.Enqueue(new Pair(root, 1));

        while (q.Count != 0)
        {
            var _size = q.Count;
            var start = 0;
            var end = 0;

            for (int i = 0; i < _size; i++)
            {
                var curPair = q.Dequeue();
                var curNode = curPair.node;
                var curId = curPair.id;
                // 记录当前层第一个和最后一个节点的编号
                if (i == 0) start = curId;
                if (i == _size - 1) end = curId;

                if (curNode.left != null) q.Enqueue(new Pair(curNode.left, 2 * curId));
                if (curNode.right != null) q.Enqueue(new Pair(curNode.right, 2 * curId + 1));
            }

            // 用当前行的宽度更新最大宽度
            maxWidth = Math.Max(maxWidth, end - start + 1);
        }

        return maxWidth;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
