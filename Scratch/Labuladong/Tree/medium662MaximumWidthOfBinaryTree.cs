namespace Scratch.Labuladong.Algorithms.MaximumWidthOfBinaryTree;

// 662. Maximum Width of Binary Tree (Medium)
//
// Given the root of a binary tree, return the maximum width of the given tree.
//
// The maximum width of a tree is the maximum width among all levels.
//
// The width of one level is defined as the length between the end-nodes (the leftmost and
// rightmost non-null nodes), where the null nodes between the end-nodes that would be present in a
// complete binary tree extending down to that level are also counted into the length calculation.
//
// It is guaranteed that the answer will in the range of a 32-bit signed integer.
//
// Example 1:
//
// Input: root = [1,3,2,5,3,null,9]
// Output: 4
// Explanation: The maximum width exists in the third level with length 4 (5,3,null,9).
//
// Example 2:
//
// Input: root = [1,3,2,5,null,null,9,6,null,7]
// Output: 7
// Explanation: The maximum width exists in the fourth level with length 7
// (6,null,null,null,null,null,7).
//
// Example 3:
//
// Input: root = [1,3,2,5]
// Output: 2
// Explanation: The maximum width exists in the second level with length 2 (3,2).
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 3000].
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
