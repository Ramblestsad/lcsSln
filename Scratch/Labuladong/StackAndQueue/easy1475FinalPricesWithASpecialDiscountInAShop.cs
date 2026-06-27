namespace Scratch.Labuladong.Algorithms.FinalPricesWithASpecialDiscountInAShop;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] FinalPrices(int[] prices)
    {
        var n = prices.Length;
        var res = new int[n];
        var stk = new Stack<int>();

        for (var i = n - 1; i >= 0; i--)
        {
            while (stk.Count != 0 && stk.Peek() > prices[i])
            {
                stk.Pop();
            }

            // calc res price
            res[i] = stk.Count == 0 ? prices[i] : prices[i] - stk.Peek();
            stk.Push(prices[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
