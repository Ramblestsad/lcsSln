namespace Scratch.Labuladong.Algorithms.TimeNeededToBuyTickets;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int TimeRequiredToBuy(int[] tickets, int k)
    {
        var res = 0;
        var kTicket = tickets[k];

        // 算法核心在于 k 满足之时，总共卖了几张票
        // 对任意位置 i 的人，统计“在第 k 个人完成之前，他最多能买几次（几张）”
        for (int i = 0; i < tickets.Length; i++)
        {
            if (i <= k)
            {
                // 在每一轮里，i 会在 k 之前被处理，
                // 因此只要 k 还没买完（共会被处理 t 次），i 就最多也能跟着被处理 t 次
                // 前面的人最多买了 t 张票
                // 但 i 自己可能没那么多票要买, 所以取min
                res += Math.Min(kTicket, tickets[i]);
            }
            else
            {
                // 后面的人最多买了 tickets[k] - 1 张票
                res += Math.Min(kTicket - 1, tickets[i]);
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
