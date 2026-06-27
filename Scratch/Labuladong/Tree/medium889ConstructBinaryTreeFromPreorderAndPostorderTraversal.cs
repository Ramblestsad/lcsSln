namespace Scratch.Labuladong.Algorithms.ConstructBinaryTreeFromPreorderAndPostorderTraversal;

// 889. Construct Binary Tree from Preorder and Postorder Traversal (Medium)
//
// Given two integer arrays, preorder and postorder where preorder is the preorder traversal of a
// binary tree of distinct values and postorder is the postorder traversal of the same tree,
// reconstruct and return the binary tree.
//
// If there exist multiple answers, you can return any of them.
//
// Example 1:
//
// Input: preorder = [1,2,4,5,3,6,7], postorder = [4,5,2,6,7,3,1]
// Output: [1,2,3,4,5,6,7]
//
// Example 2:
//
// Input: preorder = [1], postorder = [1]
// Output: [1]
//
// Constraints:
//
// - 1 <= preorder.length <= 30
//
// - 1 <= preorder[i] <= preorder.length
//
// - All the values of preorder are unique.
//
// - postorder.length == preorder.length
//
// - 1 <= postorder[i] <= postorder.length
//
// - All the values of postorder are unique.
//
// - It is guaranteed that preorder and postorder are the preorder traversal and postorder
// traversal of the same binary tree.
//
// Related Topics: Array, Hash Table, Divide and Conquer, Tree, Binary Tree

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
    // 存储 postorder 中值到索引的映射
    Dictionary<int, int> valToIndex = new();

    public TreeNode? ConstructFromPrePost(int[] preorder, int[] postorder)
    {
        for (int i = 0; i < postorder.Length; i++)
        {
            valToIndex[postorder[i]] = i;
        }

        return Build(preorder, 0, preorder.Length - 1,
                     postorder, 0, postorder.Length - 1);
    }

    // 定义：根据 preorder[preStart..preEnd] 和 postorder[postStart..postEnd]
    // 构建二叉树，并返回根节点。
    TreeNode? Build(int[] preorder, int preStart, int preEnd,
        int[] postorder, int postStart, int postEnd)
    {
        if (preStart > preEnd) return null;
        if (preStart == preEnd) return new TreeNode(preorder[preStart]);

        var rootVal = preorder[preStart];
        var root = new TreeNode(rootVal);

        // root.left 的值是前序遍历第二个元素
        // 通过前序和后序遍历构造二叉树的关键在于通过左子树的根节点
        // 确定 preorder 和 postorder 中左右子树的元素区间
        var leftRootVal = preorder[preStart + 1];
        // leftRootVal 在后序遍历数组中的索引
        valToIndex.TryGetValue(leftRootVal, out var index);
        // 左子树的元素个数
        var leftSize = index - postStart + 1;

        // 递归构造左右子树
        // 根据左子树的根节点索引和元素个数推导左右子树的索引边界
        root.left = Build(preorder, preStart + 1, preStart + leftSize,
                          postorder, postStart, index);
        root.right = Build(preorder, preStart + leftSize + 1, preEnd,
                           postorder, index + 1, postEnd - 1);

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
