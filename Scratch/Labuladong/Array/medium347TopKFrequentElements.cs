/*
 * @lc app=leetcode id=347 lang=csharp
 * @lcpr version=30403
 *
 * [347] Top K Frequent Elements
 */

namespace Scratch.Labuladong.Algorithms.TopKFreqElements;

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// [1,1,1,2,2,3]\n2\n
// @lcpr case=end

// @lcpr case=start
// [1]\n1\n
// @lcpr case=end

// @lcpr case=start
// [1,2,1,2,1,2,3,1,3,2]\n2\n
// @lcpr case=end
 */
