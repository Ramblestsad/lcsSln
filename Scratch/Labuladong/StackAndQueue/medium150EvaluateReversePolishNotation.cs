namespace Scratch.Labuladong.Algorithms.EvaluateReversePolishNotation;

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
