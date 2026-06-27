namespace Scratch.Labuladong.Algorithms.EvaluateReversePolishNotation;

// 150. Evaluate Reverse Polish Notation (Medium)
//
// You are given an array of strings tokens that represents an arithmetic expression in a Reverse
// Polish Notation.
//
// Evaluate the expression. Return an integer that represents the value of the expression.
//
// Note that:
//
// - The valid operators are '+', '-', '*', and '/'.
//
// - Each operand may be an integer or another expression.
//
// - The division between two integers always truncates toward zero.
//
// - There will not be any division by zero.
//
// - The input represents a valid arithmetic expression in a reverse polish notation.
//
// - The answer and all the intermediate calculations can be represented in a 32-bit integer.
//
// Example 1:
//
// Input: tokens = ["2","1","+","3","*"]
// Output: 9
// Explanation: ((2 + 1) * 3) = 9
//
// Example 2:
//
// Input: tokens = ["4","13","5","/","+"]
// Output: 6
// Explanation: (4 + (13 / 5)) = 6
//
// Example 3:
//
// Input: tokens = ["10","6","9","3","+","-11","*","/","*","17","+","5","+"]
// Output: 22
// Explanation: ((10 * (6 / ((9 + 3) * -11))) + 17) + 5
// = ((10 * (6 / (12 * -11))) + 17) + 5
// = ((10 * (6 / -132)) + 17) + 5
// = ((10 * 0) + 17) + 5
// = (0 + 17) + 5
// = 17 + 5
// = 22
//
// Constraints:
//
// - 1 <= tokens.length <= 10^4
//
// - tokens[i] is either an operator: "+", "-", "*", or "/", or an integer in the range [-200,
// 200].
//
// Related Topics: Array, Math, Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int EvalRPN(string[] tokens)
    {
        var stk = new Stack<int>();

        foreach (var token in tokens)
        {
            if ("+-*/".Contains(token))
            {
                // 是个运算符，从栈顶拿出两个数字进行运算，运算结果入栈
                var a = stk.Pop();
                var b = stk.Pop();
                // - / 的话，b为被除/减数
                var result = token switch
                {
                    "+" => a + b,
                    "-" => b - a,
                    "*" => a * b,
                    "/" => b / a,
                    _ => throw new InvalidOperationException($"Unsupported operator: {token}"),
                };

                stk.Push(result);
            }
            else stk.Push(int.Parse(token));
        }

        // 最后栈中剩下一个数字，即是计算结果
        return stk.Pop();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
