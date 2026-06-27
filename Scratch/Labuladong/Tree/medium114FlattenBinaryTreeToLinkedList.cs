namespace Scratch.Labuladong.Algorithms.FlattenBinaryTreeToLinkedList;

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
