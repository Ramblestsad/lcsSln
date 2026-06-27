namespace Scratch.Labuladong.Algorithms.ConstructBinaryTreeFromInorderAndPostorderTraversal;

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
    // 存储 inorder 中值到索引的映射
    Dictionary<int, int> valToIndex = new();

    public TreeNode? BuildTree(int[] inorder, int[] postorder)
    {
        for (int i = 0; i < inorder.Length; i++)
        {
            valToIndex[inorder[i]] = i;
        }

        return Build(inorder, 0, inorder.Length - 1,
                     postorder, 0, postorder.Length - 1);
    }

    // build 函数的定义：
    // 后序遍历数组为 postorder[postStart..postEnd]，
    // 中序遍历数组为 inorder[inStart..inEnd]，
    // 构造二叉树，返回该二叉树的根节点
    TreeNode? Build(int[] inorder, int inStart, int inEnd,
        int[] postorder, int postStart, int postEnd)
    {
        if (inStart > inEnd) return null;

        // root 节点对应的值就是后序遍历数组的最后一个元素
        var rootVal = postorder[postEnd];
        // rootVal 在中序遍历数组中的索引
        valToIndex.TryGetValue(rootVal, out var index);

        // 左子树个数：通过inorder特性算出
        var leftSize = index - inStart;
        TreeNode root = new TreeNode(rootVal);

        // 递归构造左右子树
        root.left = Build(inorder, inStart, index - 1,
                          postorder, postStart, postStart + leftSize - 1);
        root.right = Build(inorder, index + 1, inEnd,
                           postorder, postStart + leftSize, postEnd - 1);

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
