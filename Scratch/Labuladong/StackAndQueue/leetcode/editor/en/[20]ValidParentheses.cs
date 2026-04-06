/*
 * @lc app=leetcode id=20 lang=csharp
 * @lcpr version=30402
 *
 * [20] Valid Parentheses
 */

namespace Scratch.Labuladong.Algorithms.ValidParentheses;

// @lc code=start
public class Solution
{
    public bool IsValid(string s)
    {
        var left = new Stack<char>();
        foreach (var paren in s)
        {
            if (paren == '(' || paren == '[' || paren == '{')
            {
                // 字符 c 是左括号，入栈
                left.Push(paren);
            }
            else
            {
                // 字符 c 是右括号
                if (left.Count != 0 && LeftOf(paren) == left.Peek())
                {
                    left.Pop();
                }
                else
                {
                    // 和最近的左括号不匹配
                    return false;
                }
            }
        }

        // 是否所有的左括号都被匹配了
        return left.Count == 0;
    }

    private char LeftOf(char c)
    {
        if (c == '}') return '{';
        if (c == ')') return '(';

        return '[';
    }
}
// @lc code=end
