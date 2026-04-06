/*
 * @lc app=leetcode id=373 lang=csharp
 * @lcpr version=30402
 *
 * [373] Find K Pairs With Smallest Sums
 */

namespace Scratch.Labuladong.Algorithms.FindKPairsWithSmallestSums;

// @lc code=start
public class Solution
{
    public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
    {
        var res = new List<IList<int>>();
        // 元素 = (i, j), 优先级 = nums1[i] + nums2[j]
        var pq = new PriorityQueue<(int, int), int>();
        var limit = Math.Min(nums1.Length, k);
        for (int i = 0; i < limit; i++)
        {
            pq.Enqueue(( i, 0 ), nums1[i] + nums2[0]);
        }

        while (pq.Count > 0 && k > 0)
        {
            var (i, j) = pq.Dequeue();
            res.Add([nums1[i], nums2[j]]);

            k--;

            if (j + 1 < nums2.Length)
            {
                pq.Enqueue(( i, j + 1 ), nums1[i] + nums2[j + 1]);
            }
        }

        return res;
    }
}
// @lc code=end
