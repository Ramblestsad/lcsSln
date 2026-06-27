namespace Scratch.Labuladong.Algorithms.CheckCompletenessOfABinaryTree;

// 958. Check Completeness of a Binary Tree (Medium)
//
// Given the root of a binary tree, determine if it is a complete binary tree.
//
// In a complete binary tree, every level, except possibly the last, is completely filled, and all
// nodes in the last level are as far left as possible. It can have between 1 and 2^h nodes
// inclusive at the last level h.
//
// Example 1:
//
// Input: root = [1,2,3,4,5,6]
// Output: true
// Explanation: Every level before the last is full (ie. levels with node-values {1} and {2, 3}),
// and all nodes in the last level ({4, 5, 6}) are as far left as possible.
//
// Example 2:
//
// Input: root = [1,2,3,4,5,null,7]
// Output: false
// Explanation: The node with value 7 isn't as far left as possible.
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 100].
//
// - 1 <= Node.val <= 1000
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
    public bool IsCompleteTree(TreeNode? root)
    {
        // 完全二叉树在层序遍历中，一旦出现第一个 null（缺口），
        // 后面出队的节点必须全是 null；
        // 否则就说明“缺口后面还有真实节点”，结构不可能是完全二叉树。
        if (root == null) return true;

        var q = new Queue<TreeNode?>();
        q.Enqueue(root);
        // 遍历完所有非空节点时变成 true
        var end = false;

        while (q.Count != 0)
        {
            var _size = q.Count;
            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                if (cur == null)
                {
                    // 第一次遇到 null 时 end 变成 true
                    // 如果之后的所有节点都是 null，则说明是完全二叉树
                    end = true;
                }
                else
                {
                    if (end) return false;
                    // 将下一层节点放入队列，不用判断是否非空
                    q.Enqueue(cur.left);
                    q.Enqueue(cur.right);
                }
            }
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
