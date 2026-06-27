namespace Scratch.Labuladong.Algorithms.SquaresOfASortedArray;

// 977. Squares of a Sorted Array (Easy)
//
// Given an integer array nums sorted in non-decreasing order, return an array of the squares of
// each number sorted in non-decreasing order.
//
// Example 1:
//
// Input: nums = [-4,-1,0,3,10]
// Output: [0,1,9,16,100]
// Explanation: After squaring, the array becomes [16,1,0,9,100].
// After sorting, it becomes [0,1,9,16,100].
//
// Example 2:
//
// Input: nums = [-7,-3,2,3,11]
// Output: [4,9,9,49,121]
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -10^4 <= nums[i] <= 10^4
//
// - nums is sorted in non-decreasing order.
//
// Follow up: Squaring each element and sorting the new array is very trivial, could you find an
// O(n) solution using a different approach?
//
// Related Topics: Array, Two Pointers, Sorting

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] SortedSquares(int[] nums)
    {
        // 直接将双指针分别初始化在 nums 的开头和结尾，相当于合并两个从大到小排序的数组，和 88 题类似
        // 两个指针分别初始化在正负子数组绝对值最大的元素索引
        var n = nums.Length;
        int i = 0, j = n - 1;
        // 得到的有序结果是降序的
        var p = n - 1;
        var res = new int[n];

        while (i <= j)
        {
            if (Math.Abs(nums[i]) > Math.Abs(nums[j]))
            {
                res[p] = nums[i] * nums[i];
                i++;
            }
            else
            {
                res[p] = nums[j] * nums[j];
                j--;
            }

            p--;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
