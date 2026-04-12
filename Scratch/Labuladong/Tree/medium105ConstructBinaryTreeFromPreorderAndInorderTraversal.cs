/*
 * @lc app=leetcode id=105 lang=csharp
 * @lcpr version=30402
 *
 * [105] Construct Binary Tree From Preorder And Inorder Traversal
 */

namespace Scratch.Labuladong.Algorithms.ConstructBinaryTreeFromPreorderAndInorderTraversal;

// @lc code=start
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
    // 存储 inorder 中值到索引的映射
    Dictionary<int, int> valToIndex = new();

    public TreeNode? BuildTree(int[] preorder, int[] inorder)
    {
        for (int i = 0; i < inorder.Length; i++)
        {
            valToIndex[inorder[i]] = i;
        }

        return Build(preorder, 0, preorder.Length - 1,
                     inorder, 0, inorder.Length - 1);
    }

    TreeNode? Build(int[] preorder, int preStart, int preEnd,
        int[] inorder, int inStart, int inEnd)
    {
        if (preStart > preEnd)
        {
            return null;
        }

        // root 节点对应的值就是前序遍历数组的第一个元素
        var rootVal = preorder[preStart];
        // rootVal 在中序遍历数组中的索引
        valToIndex.TryGetValue(rootVal, out var index);

        var lefSize = index - inStart;

        // 构造root
        var root = new TreeNode(rootVal);
        // 递归构造左右子树
        root.left = Build(preorder, preStart + 1, preStart + lefSize,
                          inorder, inStart, index - 1);
        root.right = Build(preorder, preStart + lefSize + 1, preEnd,
                           inorder, index + 1, inEnd);

        return root;
    }
}
// @lc code=end
