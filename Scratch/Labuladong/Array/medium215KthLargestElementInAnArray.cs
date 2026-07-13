/*
 * @lc app=leetcode id=215 lang=csharp
 * @lcpr version=3.4.4
 */

namespace Scratch.Labuladong.Algorithms.KLargestElement;

// 215. Kth Largest Element in an Array (Medium)
//
// Given an integer array nums and an integer k, return the k^th largest element in the array.
//
// Note that it is the k^th largest element in the sorted order, not the k^th distinct element.
//
// Can you solve it without sorting?
//
// Example 1:
//
// Input: nums = [3,2,1,5,6,4], k = 2
// Output: 5
// Example 2:
//
// Input: nums = [3,2,3,1,2,4,5,5,6], k = 4
// Output: 4
//
// Constraints:
//
// - 1 <= k <= nums.length <= 10^5
//
// - -10^4 <= nums[i] <= 10^4
//
// Related Topics: Array, Divide and Conquer, Sorting, Heap (Priority Queue), Quickselect

// @lc code=start
public class Solution
{
    public int FindKthLargest(int[] nums, int k)
    {
        var pq = new PriorityQueue<int, int>();

        foreach (var num in nums)
        {
            pq.Enqueue(num, num);
            if (pq.Count > k) pq.Dequeue();
        }

        return pq.Peek();
    }
}
// @lc code=end

/*
// @lcpr case=start
// [3,2,1,5,6,4]\n2\n
// @lcpr case=end

// @lcpr case=start
// [3,2,3,1,2,4,5,5,6]\n4\n
// @lcpr case=end
 */
