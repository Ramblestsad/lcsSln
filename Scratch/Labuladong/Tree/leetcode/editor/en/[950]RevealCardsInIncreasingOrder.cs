namespace Scratch.Labuladong.Algorithms.RevealCardsInIncreasingOrder;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] DeckRevealedIncreasing(int[] deck)
    {
        if (deck == null || deck.Length == 0) return Array.Empty<int>();
        var n = deck.Length;

        // 链表头部代表牌堆顶，尾部代表牌堆底
        var res = new LinkedList<int>();
        // 升序排列，然后从倒着遍历，就是点数递减
        Array.Sort(deck);
        for (int i = n - 1; i >= 0; i--)
        {
            if (res.Count != 0)
            {
                var last = res.Last!.Value;
                res.AddFirst(last);
                res.RemoveLast();
            }

            res.AddFirst(deck[i]);
        }

        return res.ToArray();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
