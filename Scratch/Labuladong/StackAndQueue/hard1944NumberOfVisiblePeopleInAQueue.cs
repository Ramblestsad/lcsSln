namespace Scratch.Labuladong.Algorithms.NumberOfVisiblePeopleInAQueue;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] CanSeePersonsCount(int[] heights)
    {
        // 不是问你下一个更大元素是多少，而是问你当前元素和下一个更大元素之间的元素个数
        var n = heights.Length;
        var res = new int[n];
        var stk = new Stack<int>();

        for (var i = n - 1; i >= 0; i--)
        {
            // 记录右侧比自己矮的人
            var count = 0;
            while (stk.Count != 0 && stk.Peek() <= heights[i])
            {
                stk.Pop();
                count++;
            }

            // 不仅可以看到比自己矮的人，如果后面存在的第一个比自己更高的的人。
            res[i] = stk.Count == 0 ? count : count + 1;
            stk.Push(heights[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
