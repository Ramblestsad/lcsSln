namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedArrayII;

// 80. Remove Duplicates from Sorted Array II (Medium)
//
// Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place such
// that each unique element appears at most twice. The relative order of the elements should be
// kept the same.
//
// Since it is impossible to change the length of the array in some languages, you must instead
// have the result be placed in the first part of the array nums. More formally, if there are k
// elements after removing the duplicates, then the first k elements of nums should hold the final
// result. It does not matter what you leave beyond the first k elements.
//
// Return k after placing the final result in the first k slots of nums.
//
// Do not allocate extra space for another array. You must do this by modifying the input array
// in-place with O(1) extra memory.
//
// Custom Judge:
//
// The judge will test your solution with the following code:
//
// int[] nums = [...]; // Input array
// int[] expectedNums = [...]; // The expected answer with correct length
//
// int k = removeDuplicates(nums); // Calls your implementation
//
// assert k == expectedNums.length;
// for (int i = 0; i < k; i++) {
// assert nums[i] == expectedNums[i];
// }
//
// If all assertions pass, then your solution will be accepted.
//
// Example 1:
//
// Input: nums = [1,1,1,2,2,3]
// Output: 5, nums = [1,1,2,2,3,_]
// Explanation: Your function should return k = 5, with the first five elements of nums being 1, 1,
// 2, 2 and 3 respectively.
// It does not matter what you leave beyond the returned k (hence they are underscores).
//
// Example 2:
//
// Input: nums = [0,0,1,1,1,1,2,3,3]
// Output: 7, nums = [0,0,1,1,2,3,3,_,_]
// Explanation: Your function should return k = 7, with the first seven elements of nums being 0,
// 0, 1, 1, 2, 3 and 3 respectively.
// It does not matter what you leave beyond the returned k (hence they are underscores).
//
// Constraints:
//
// - 1 <= nums.length <= 3 * 10^4
//
// - -10^4 <= nums[i] <= 10^4
//
// - nums is sorted in non-decreasing order.
//
// Related Topics: Array, Two Pointers

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int RemoveDuplicates(int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        int slow = 0, fast = 0, count = 0;

        while (fast < n)
        {
            if (nums[fast] != nums[slow])
            {
                // 此时，对于 nums[0..slow] 来说，nums[fast] 是一个新的元素，加进来
                slow++;
                nums[slow] = nums[fast];
            }
            else if (slow < fast && count < 2)
            {
                // 此时，对于 nums[0..slow] 来说，nums[fast] 重复次数小于 2，也加进来
                slow++;
                nums[slow] = nums[fast];
            }

            fast++;
            // 以fast为准，相当于用fast遍历数组，fast前进了，count就+
            count++;
            if (fast < n && nums[fast] != nums[fast - 1])
            {
                // fast 遇到新的不同的元素时，重置 count
                count = 0;
            }
        }

        return slow + 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
