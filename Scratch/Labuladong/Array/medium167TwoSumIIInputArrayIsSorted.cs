namespace Scratch.Labuladong.Algorithms.TwoSumIIInputArrayIsSorted;

// 167. Two Sum II - Input Array Is Sorted (Medium)
//
// Given a 1-indexed array of integers numbers that is already sorted in non-decreasing order, find
// two numbers such that they add up to a specific target number. Let these two numbers be
// numbers[index_1] and numbers[index_2] where 1 <= index_1 < index_2 <= numbers.length.
//
// Return the indices of the two numbers index_1 and index_2, each incremented by one, as an
// integer array [index_1, index_2] of length 2.
//
// The tests are generated such that there is exactly one solution. You may not use the same
// element twice.
//
// Your solution must use only constant extra space.
//
// Example 1:
//
// Input: numbers = [2,7,11,15], target = 9
// Output: [1,2]
// Explanation: The sum of 2 and 7 is 9. Therefore, index_1 = 1, index_2 = 2. We return [1, 2].
//
// Example 2:
//
// Input: numbers = [2,3,4], target = 6
// Output: [1,3]
// Explanation: The sum of 2 and 4 is 6. Therefore index_1 = 1, index_2 = 3. We return [1, 3].
//
// Example 3:
//
// Input: numbers = [-1,0], target = -1
// Output: [1,2]
// Explanation: The sum of -1 and 0 is -1. Therefore index_1 = 1, index_2 = 2. We return [1, 2].
//
// Constraints:
//
// - 2 <= numbers.length <= 3 * 10^4
//
// - -1000 <= numbers[i] <= 1000
//
// - numbers is sorted in non-decreasing order.
//
// - -1000 <= target <= 1000
//
// - The tests are generated such that there is exactly one solution.
//
// Related Topics: Array, Two Pointers, Binary Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] TwoSum(int[] numbers, int target)
    {
        var left = 0;
        var right = numbers.Length - 1;

        while (left < right)
        {
            var sum = numbers[left] + numbers[right];
            if (sum == target) return [left + 1, right + 1];
            // 记住numbers是非递减数组，移动左指针，sum就会变大；反之移动右指针，sum就会变小;
            if (sum < target) left++;
            if (sum > target) right--;
        }

        return [-1, -1];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
