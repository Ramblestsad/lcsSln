namespace Scratch.Labuladong.Algorithms.TopKFreqElements;

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
