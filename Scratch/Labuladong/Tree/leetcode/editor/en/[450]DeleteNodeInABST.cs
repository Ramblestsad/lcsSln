namespace Scratch.Labuladong.Algorithms.DeleteNodeInABST;

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
    public TreeNode? DeleteNode(TreeNode? root, int key)
    {
        if (root == null) return null;

        if (key == root.val)
        {
            // 情况 1：A 恰好是末端节点，两个子节点都为空，那么它可以直接被删除。
            // 情况 2：A 只有一个非空子节点，那么它要让这个孩子接替自己的位置。
            // 情况 3：A 有两个子节点，为了不破坏 BST 的性质，A 必须找到左子树中最大的那个节点，或者右子树中最小的那个节点来接替自己。

            // 这两个 if 把情况 1 和 2 都正确处理了
            if (root.left == null) return root.right;
            if (root.right == null) return root.left;

            // 处理情况3
            // 获得右子树最小的节点
            var rightMinNode = _getMin(root.right);
            // 删除右子树最小节点
            root.right = DeleteNode(root.right, rightMinNode.val);
            // 用右子树最小的节点替换 root 节点
            rightMinNode.left = root.left;
            rightMinNode.right = root.right;
            root = rightMinNode;
        }
        else if (key < root.val)
        {
            root.left = DeleteNode(root.left, key);
        }
        else if (key > root.val)
        {
            root.right = DeleteNode(root.right, key);
        }

        return root;
    }

    private TreeNode _getMin(TreeNode node)
    {
        // BST 最左边的就是最小的
        while (node.left != null) node = node.left;
        return node;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
