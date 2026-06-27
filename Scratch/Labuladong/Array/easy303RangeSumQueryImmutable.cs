namespace Scratch.Labuladong.Algorithms.RangeSumQueryImmutable;

// 303. Range Sum Query - Immutable (Easy)
//
// Given an integer array nums, handle multiple queries of the following type:
//
// - Calculate the sum of the elements of nums between indices left and right inclusive where left
// <= right.
//
// Implement the NumArray class:
//
// - NumArray(int[] nums) Initializes the object with the integer array nums.
//
// - int sumRange(int left, int right) Returns the sum of the elements of nums between indices left
// and right inclusive (i.e. nums[left] + nums[left + 1] + ... + nums[right]).
//
// Example 1:
//
// Input
// ["NumArray", "sumRange", "sumRange", "sumRange"]
// [[[-2, 0, 3, -5, 2, -1]], [0, 2], [2, 5], [0, 5]]
// Output
// [null, 1, -1, -3]
//
// Explanation
// NumArray numArray = new NumArray([-2, 0, 3, -5, 2, -1]);
// numArray.sumRange(0, 2); // return (-2) + 0 + 3 = 1
// numArray.sumRange(2, 5); // return 3 + (-5) + 2 + (-1) = -1
// numArray.sumRange(0, 5); // return (-2) + 0 + 3 + (-5) + 2 + (-1) = -3
//
// Constraints:
//
// - 1 <= nums.length <= 10^4
//
// - -10^5 <= nums[i] <= 10^5
//
// - 0 <= left <= right < nums.length
//
// - At most 10^4 calls will be made to sumRange.
//
// Related Topics: Array, Design, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class NumArray
{
    private int[] PreSum { get; set; }

    public NumArray(int[] nums)
    {
        // preSum[0] = 0，便于计算累加和
        PreSum = new int[nums.Length + 1];

        // 计算 nums 的累加和
        for (int i = 1; i < PreSum.Length; i++)
        {
            PreSum[i] = PreSum[i - 1] + nums[i - 1];
        }
    }

    public int SumRange(int left, int right)
    {
        return PreSum[right + 1] - PreSum[left];
    }
}

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * int param_1 = obj.SumRange(left,right);
 */
//leetcode submit region end(Prohibit modification and deletion)
