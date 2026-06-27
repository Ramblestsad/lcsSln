namespace Scratch.Labuladong.Algorithms.NumberOfRecentCalls;

//leetcode submit region begin(Prohibit modification and deletion)
public class RecentCounter
{
    private Queue<int> q = new();

    public int Ping(int t)
    {
        q.Enqueue(t);
        while (q.Peek() < ( t - 3000 ))
        {
            // 按照题目，t是递增的，所以可以从队头删除 3000 毫秒之前的请求
            q.Dequeue();
        }

        return q.Count;
    }
}

/**
 * Your RecentCounter object will be instantiated and called as such:
 * RecentCounter obj = new RecentCounter();
 * int param_1 = obj.Ping(t);
 */
//leetcode submit region end(Prohibit modification and deletion)
