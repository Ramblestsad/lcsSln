namespace Scratch.Labuladong.Algorithms.LongestValidParen;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestValidParentheses(string s)
    {
        var stk = new Stack<int>();
        // dp[i] 的定义：记录以 s[i-1] 结尾的最长合法括号子串长度
        var dp = new int[s.Length + 1];
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                // 遇到左括号，记录索引
                stk.Push(i);
                // 左括号不可能是合法括号子串的结尾
                dp[i + 1] = 0;
            }
            else
            {
                // 遇到右括号
                if (stk.Count != 0)
                {
                    // 配对的左括号对应索引
                    int leftIndex = stk.Pop();
                    // 以这个右括号结尾的最长子串长度
                    int len = 1 + i - leftIndex + dp[leftIndex];
                    dp[i + 1] = len;
                }
                else
                {
                    // 没有配对的左括号
                    dp[i + 1] = 0;
                }
            }
        }

        // 计算最长子串的长度
        var res = 0;
        for (var i = 0; i < dp.Length; i++)
        {
            res = Math.Max(res, dp[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
