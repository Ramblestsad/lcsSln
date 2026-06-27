namespace Scratch.Labuladong.Algorithms.MergeSortedArray;

// 88. Merge Sorted Array (Easy)
//
// You are given two integer arrays nums1 and nums2, sorted in non-decreasing order, and two
// integers m and n, representing the number of elements in nums1 and nums2 respectively.
//
// Merge nums1 and nums2 into a single array sorted in non-decreasing order.
//
// The final sorted array should not be returned by the function, but instead be stored inside the
// array nums1. To accommodate this, nums1 has a length of m + n, where the first m elements denote
// the elements that should be merged, and the last n elements are set to 0 and should be ignored.
// nums2 has a length of n.
//
// Example 1:
//
// Input: nums1 = [1,2,3,0,0,0], m = 3, nums2 = [2,5,6], n = 3
// Output: [1,2,2,3,5,6]
// Explanation: The arrays we are merging are [1,2,3] and [2,5,6].
// The result of the merge is [1,2,2,3,5,6] with the underlined elements coming from nums1.
//
// Example 2:
//
// Input: nums1 = [1], m = 1, nums2 = [], n = 0
// Output: [1]
// Explanation: The arrays we are merging are [1] and [].
// The result of the merge is [1].
//
// Example 3:
//
// Input: nums1 = [0], m = 0, nums2 = [1], n = 1
// Output: [1]
// Explanation: The arrays we are merging are [] and [1].
// The result of the merge is [1].
// Note that because m = 0, there are no elements in nums1. The 0 is only there to ensure the merge
// result can fit in nums1.
//
// Constraints:
//
// - nums1.length == m + n
//
// - nums2.length == n
//
// - 0 <= m, n <= 200
//
// - 1 <= m + n <= 200
//
// - -10^9 <= nums1[i], nums2[j] <= 10^9
//
// Follow up: Can you come up with an algorithm that runs in O(m + n) time?
//
// Related Topics: Array, Two Pointers, Sorting

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // 两个指针分别初始化在两个数组的最后一个元素
        int i = m - 1, j = n - 1;
        var p = nums1.Length - 1;
        // 从后向前生成结果数组，类似合并两个有序链表的逻辑
        while (i >= 0 && j >= 0)
        {
            if (nums1[i] > nums2[j])
            {
                nums1[p] = nums1[i];
                i--;
            }
            else
            {
                nums1[p] = nums2[j];
                j--;
            }

            p--;
        }

        // 可能其中一个数组的指针走到尽头了，而另一个还没走完
        // 因为我们本身就是在往 nums1 中放元素，所以只需考虑 nums2 是否剩元素即可
        while (j >= 0)
        {
            nums1[p] = nums2[j];
            j--;
            p--;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
