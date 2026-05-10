/*
 * @lc app=leetcode id=215 lang=csharp
 * @lcpr version=30403
 *
 * [215] Kth Largest Element in an Array
 */

namespace Scratch.Labuladong.Algorithms.KLargestElement;

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
