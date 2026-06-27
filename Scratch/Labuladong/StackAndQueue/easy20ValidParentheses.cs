namespace Scratch.Labuladong.Algorithms.ValidParentheses;

// 20. Valid Parentheses (Easy)
//
// Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if
// the input string is valid.
//
// An input string is valid if:
//
// - Open brackets must be closed by the same type of brackets.
//
// - Open brackets must be closed in the correct order.
//
// - Every close bracket has a corresponding open bracket of the same type.
//
// Example 1:
//
// Input: s = "()"
//
// Output: true
//
// Example 2:
//
// Input: s = "()[]{}"
//
// Output: true
//
// Example 3:
//
// Input: s = "(]"
//
// Output: false
//
// Example 4:
//
// Input: s = "([])"
//
// Output: true
//
// Example 5:
//
// Input: s = "([)]"
//
// Output: false
//
// Constraints:
//
// - 1 <= s.length <= 10^4
//
// - s consists of parentheses only '()[]{}'.
//
// Related Topics: String, Stack

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
