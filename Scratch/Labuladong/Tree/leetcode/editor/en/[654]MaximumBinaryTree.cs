namespace Scratch.Labuladong.Algorithms.MaximumBinaryTree;

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
    public TreeNode? ConstructMaximumBinaryTree(int[] nums)
    {
        return Build(nums, 0, nums.Length - 1);
    }

    // 定义：将 nums[lo..hi] 构造成符合条件的树，返回根节点
    TreeNode? Build(int[] nums, int lo, int hi)
    {
        // base case
        if (lo > hi)
        {
            return null;
        }

        // 前序位置
        // 找到数组中的最大值和对应的索引
        var index = -1;
        var maxVal = int.MinValue;
        for (int i = lo; i <= hi; i++)
        {
            if (nums[i] > maxVal)
            {
                index = i;
                maxVal = nums[i];
            }
        }

        // 先构造出根节点
        var root = new TreeNode(maxVal);
        // 递归调用构造左右子树
        root.left = Build(nums, lo, index - 1);
        root.right = Build(nums, index + 1, hi);

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
