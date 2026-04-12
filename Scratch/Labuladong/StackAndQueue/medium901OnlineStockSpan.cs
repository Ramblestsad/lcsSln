/*
 * @lc app=leetcode id=901 lang=csharp
 * @lcpr version=30402
 *
 * [901] Online Stock Span
 */

namespace Scratch.Labuladong.Algorithms.OnlineStockSpan;

// @lc code=start
public class StockSpanner
{
    private Stack<(int price, int cnt)> stk = new();

    public int Next(int price)
    {
        // 算上当天
        var count = 1;

        while (stk.Count != 0 && stk.Peek().price <= price)
        {
            var prev = stk.Pop();
            count += prev.cnt;
        }

        stk.Push(( price, count ));

        return count;
    }
}

/**
 * Your StockSpanner object will be instantiated and called as such:
 * StockSpanner obj = new StockSpanner();
 * int param_1 = obj.Next(price);
 */
// @lc code=end
