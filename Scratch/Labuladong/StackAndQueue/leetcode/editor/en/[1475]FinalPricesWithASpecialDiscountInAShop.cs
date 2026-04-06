/*
 * @lc app=leetcode id=1475 lang=csharp
 * @lcpr version=30402
 *
 * [1475] Final Prices With A Special Discount In A Shop
 */

namespace Scratch.Labuladong.Algorithms.FinalPricesWithASpecialDiscountInAShop;

// @lc code=start
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
// @lc code=end
