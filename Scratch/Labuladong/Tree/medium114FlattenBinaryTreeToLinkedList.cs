namespace Scratch.Labuladong.Algorithms.FlattenBinaryTreeToLinkedList;

// 114. Flatten Binary Tree to Linked List (Medium)
//
// Given the root of a binary tree, flatten the tree into a "linked list":
//
// - The "linked list" should use the same TreeNode class where the right child pointer points to
// the next node in the list and the left child pointer is always null.
//
// - The "linked list" should be in the same order as a pre-order traversal of the binary tree.
//
// Example 1:
//
// Input: root = [1,2,5,3,4,null,6]
// Output: [1,null,2,null,3,null,4,null,5,null,6]
//
// Example 2:
//
// Input: root = []
// Output: []
//
// Example 3:
//
// Input: root = [0]
// Output: [0]
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 2000].
//
// - -100 <= Node.val <= 100
//
// Follow up: Can you flatten the tree in-place (with O(1) extra space)?
//
// Related Topics: Linked List, Stack, Tree, Depth-First Search, Binary Tree

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
    public void Flatten(TreeNode? root)
    {
        // base case
        if (root == null) return;

        // 把左右子树拉平
        Flatten(root.left);
        Flatten(root.right);

        // 后序位置
        // 此时的root已经是左右子树的上一层父节点
        // 1、左右子树已经被拉平成一条链表
        var l = root.left;
        var r = root.right;

        // 2、将左子树作为右子树
        root.left = null;
        root.right = l;

        // 3、将原先的右子树接到当前右子树的末端
        var p = root;
        while (p.right != null)
        {
            p = p.right;
        }

        p.right = r;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
