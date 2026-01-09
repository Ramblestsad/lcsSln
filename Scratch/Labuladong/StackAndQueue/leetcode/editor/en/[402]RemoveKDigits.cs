namespace Scratch.Labuladong.Algorithms.RemoveKDigits;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string RemoveKdigits(string num, int k)
    {
        /*
         * 1、先删除 num 中的若干数字，使得 num 从左到右每一位都单调递增。
         *      比如 14329 转化成 129。
         * 2、num 中的每一位变成单调递增的之后，如果 k 还大于 0（还可以继续删除）的话，则删除尾部的数字。
         *      比如 129 删除成 12。
         */

        var stk = new Stack<char>();
        foreach (var ch in num)
        {
            // 907559 3
            while (stk.Count != 0 && stk.Peek() > ch && k > 0)
            {
                stk.Pop();
                k--;
            }

            // 防止 0 作为数字的开头
            if (stk.Count == 0 && ch == '0') continue;
            stk.Push(ch);
        }

        // 此时栈中元素单调递增，若 k 还没用完的话删掉栈顶元素
        while (stk.Count != 0 && k > 0)
        {
            stk.Pop();
            k--;
        }

        // 若最后没剩下数字，就是 0
        if (stk.Count == 0) return "0";

        // 将栈中字符转化成字符串
        var arr = stk.ToArray();
        Array.Reverse(arr);

        return new string(arr);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
