namespace Scratch.Labuladong.Algorithms.TopKFreqElements;

// 347. Top K Frequent Elements (Medium)
//
// Given an integer array nums and an integer k, return the k most frequent elements. You may
// return the answer in any order.
//
// Example 1:
//
// Input: nums = [1,1,1,2,2,3], k = 2
//
// Output: [1,2]
//
// Example 2:
//
// Input: nums = [1], k = 1
//
// Output: [1]
//
// Example 3:
//
// Input: nums = [1,2,1,2,1,2,3,1,3,2], k = 2
//
// Output: [1,2]
//
// Constraints:
//
// - 1 <= nums.length <= 10^5
//
// - -10^4 <= nums[i] <= 10^4
//
// - k is in the range [1, the number of unique elements in the array].
//
// - It is guaranteed that the answer is unique.
//
// Follow up: Your algorithm's time complexity must be better than O(n log n), where n is the
// array's size.
//
// Related Topics: Array, Hash Table, Divide and Conquer, Sorting, Heap (Priority Queue), Bucket Sort, Counting, Quickselect

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] TopKFrequent(int[] nums, int k)
    {
        var valToFreq = new Dictionary<int, int>();

        foreach (var num in nums)
        {
            valToFreq[num] = valToFreq.GetValueOrDefault(num) + 1;
        }

        var pq = new PriorityQueue<int, int>();

        foreach (var (num, freq) in valToFreq)
        {
            pq.Enqueue(num, freq);
            if (pq.Count > k) pq.Dequeue();
        }

        var res = new int[k];
        for (int i = 0; i < k; i++)
        {
            res[i] = pq.Dequeue();
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
