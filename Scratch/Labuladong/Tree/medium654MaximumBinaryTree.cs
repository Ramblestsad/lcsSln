namespace Scratch.Labuladong.Algorithms.MaximumBinaryTree;

// 654. Maximum Binary Tree (Medium)
//
// You are given an integer array nums with no duplicates. A maximum binary tree can be built
// recursively from nums using the following algorithm:
//
// - Create a root node whose value is the maximum value in nums.
//
// - Recursively build the left subtree on the subarray prefix to the left of the maximum value.
//
// - Recursively build the right subtree on the subarray suffix to the right of the maximum value.
//
// Return the maximum binary tree built from nums.
//
// Example 1:
//
// Input: nums = [3,2,1,6,0,5]
// Output: [6,3,5,null,2,0,null,null,1]
// Explanation: The recursive calls are as follow:
// - The largest value in [3,2,1,6,0,5] is 6. Left prefix is [3,2,1] and right suffix is [0,5].
// - The largest value in [3,2,1] is 3. Left prefix is [] and right suffix is [2,1].
// - Empty array, so no child.
// - The largest value in [2,1] is 2. Left prefix is [] and right suffix is [1].
// - Empty array, so no child.
// - Only one element, so child is a node with value 1.
// - The largest value in [0,5] is 5. Left prefix is [0] and right suffix is [].
// - Only one element, so child is a node with value 0.
// - Empty array, so no child.
//
// Example 2:
//
// Input: nums = [3,2,1]
// Output: [3,null,2,null,1]
//
// Constraints:
//
// - 1 <= nums.length <= 1000
//
// - 0 <= nums[i] <= 1000
//
// - All integers in nums are unique.
//
// Related Topics: Array, Divide and Conquer, Stack, Tree, Monotonic Stack, Binary Tree

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
