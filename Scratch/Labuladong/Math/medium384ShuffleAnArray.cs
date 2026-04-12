/*
 * @lc app=leetcode id=384 lang=csharp
 * @lcpr version=30402
 *
 * [384] Shuffle An Array
 */

namespace Scratch.Labuladong.Algorithms.ShuffleAnArray;

//Given an integer array nums, design an algorithm to randomly shuffle the
//array. All permutations of the array should be equally likely as a result of the
//shuffling.
//
// Implement the Solution class:
//
//
// Solution(int[] nums) Initializes the object with the integer array nums.
// int[] reset() Resets the array to its original configuration and returns it.
//
// int[] shuffle() Returns a random shuffling of the array.
//
//
//
// Example 1:
//
//
//Input
//["Solution", "shuffle", "reset", "shuffle"]
//[[[1, 2, 3]], [], [], []]
//Output
//[null, [3, 1, 2], [1, 2, 3], [1, 3, 2]]
//
//Explanation
//Solution solution = new Solution([1, 2, 3]);
//solution.shuffle();    // Shuffle the array [1,2,3] and return its result.
//                       // Any permutation of [1,2,3] must be equally likely
//to be returned.
//                       // Example: return [3, 1, 2]
//solution.reset();      // Resets the array back to its original configuration
//[1,2,3]. Return [1, 2, 3]
//solution.shuffle();    // Returns the random shuffling of array [1,2,3].
//Example: return [1, 3, 2]
//
//
//
//
// Constraints:
//
//
// 1 <= nums.length <= 50
// -10⁶ <= nums[i] <= 10⁶
// All the elements of nums are unique.
// At most 10⁴ calls in total will be made to reset and shuffle.
//
//
// Related TopicsArray | Math | Design | Randomized
//
// 👍 1431, 👎 946bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    private int[] nums;
    private Random rand = new();

    public Solution(int[] nums)
    {
        this.nums = nums;
    }

    public int[] Reset()
    {
        return nums;
    }

    public int[] Shuffle()
    {
        var n = nums.Length;
        var copy = new int[n];
        Array.Copy(nums, copy, n);

        for (int i = 0; i < n; i++)
        {
            // 分析洗牌算法正确性的准则：产生的结果必须有 n! 种可能。
            // 因为一个长度为 n 的数组的全排列就有 n! 种，也就是说打乱结果总共有 n! 种。

            // 生成一个 [i, n-1] 区间内的随机数
            var r = i + rand.Next(n - i);
            // 交换 nums[i] 和 nums[r]
            ( copy[i], copy[r] ) = ( copy[r], copy[i] );
        }

        return copy;
    }
}

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(nums);
 * int[] param_1 = obj.Reset();
 * int[] param_2 = obj.Shuffle();
 */
// @lc code=end
