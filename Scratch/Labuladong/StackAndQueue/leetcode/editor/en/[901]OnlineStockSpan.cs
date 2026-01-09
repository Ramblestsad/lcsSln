namespace Scratch.Labuladong.Algorithms.OnlineStockSpan;

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
