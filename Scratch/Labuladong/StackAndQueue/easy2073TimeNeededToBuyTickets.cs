namespace Scratch.Labuladong.Algorithms.TimeNeededToBuyTickets;

// 2073. Time Needed to Buy Tickets (Easy)
//
// There are n people in a line queuing to buy tickets, where the 0^th person is at the front of
// the line and the (n - 1)^th person is at the back of the line.
//
// You are given a 0-indexed integer array tickets of length n where the number of tickets that the
// i^th person would like to buy is tickets[i].
//
// Each person takes exactly 1 second to buy a ticket. A person can only buy 1 ticket at a time and
// has to go back to the end of the line (which happens instantaneously) in order to buy more
// tickets. If a person does not have any tickets left to buy, the person will leave the line.
//
// Return the time taken for the person initially at position k (0-indexed) to finish buying
// tickets.
//
// Example 1:
//
// Input: tickets = [2,3,2], k = 2
//
// Output: 6
//
// Explanation:
//
// - The queue starts as [2,3,2], where the kth person is underlined.
//
// - After the person at the front has bought a ticket, the queue becomes [3,2,1] at 1 second.
//
// - Continuing this process, the queue becomes [2,1,2] at 2 seconds.
//
// - Continuing this process, the queue becomes [1,2,1] at 3 seconds.
//
// - Continuing this process, the queue becomes [2,1] at 4 seconds. Note: the person at the front
// left the queue.
//
// - Continuing this process, the queue becomes [1,1] at 5 seconds.
//
// - Continuing this process, the queue becomes [1] at 6 seconds. The kth person has bought all
// their tickets, so return 6.
//
// Example 2:
//
// Input: tickets = [5,1,1,1], k = 0
//
// Output: 8
//
// Explanation:
//
// - The queue starts as [5,1,1,1], where the kth person is underlined.
//
// - After the person at the front has bought a ticket, the queue becomes [1,1,1,4] at 1 second.
//
// - Continuing this process for 3 seconds, the queue becomes [4] at 4 seconds.
//
// - Continuing this process for 4 seconds, the queue becomes [] at 8 seconds. The kth person has
// bought all their tickets, so return 8.
//
// Constraints:
//
// - n == tickets.length
//
// - 1 <= n <= 100
//
// - 1 <= tickets[i] <= 100
//
// - 0 <= k < n
//
// Related Topics: Array, Queue, Simulation

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
