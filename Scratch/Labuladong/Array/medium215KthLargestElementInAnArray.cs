namespace Scratch.Labuladong.Algorithms.KLargestElement;

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
