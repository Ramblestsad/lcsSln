using System.Text;

namespace Scratch.Labuladong.Algorithms.DecodeString;

//leetcode submit region begin(Prohibit modification and deletion)

public class Solution
{
    /// 用一个栈存储「当前字符串」和「重复次数」的配对。遍历编码串时：
    ///     遇到数字：累积重复次数（注意可能是多位数，如 12[a]）
    ///     遇到 [：把当前积累的字符串和重复次数压栈，然后重置两者
    ///     遇到 ]：从栈中弹出之前的字符串和重复次数，把当前字符串重复若干次后拼接到弹出的字符串后面
    ///     遇到字母：拼接到当前字符串
    public string DecodeString(string s)
    {
        // 栈存储 (之前的字符串, 重复次数) 的配对
        var strStk = new Stack<string>();
        var cntStk = new Stack<int>();

        // 当前正在构建的字符串
        var cur = new StringBuilder();
        // 当前正在构建的数字（可能是多位数）
        var k = 0;

        foreach (var c in s)
        {
            if (char.IsDigit(c))
            {
                // 累积多位数字
                k = k * 10 + ( c - '0' );
            }
            else if (c == '[')
            {
                // 压栈，重置当前状态
                strStk.Push(cur.ToString());
                cntStk.Push(k);
                cur = new StringBuilder();
                k = 0;
            }
            else if (c == ']')
            {
                // 弹栈，把当前串重复 k 次后拼接到之前的串
                var times = cntStk.Pop();
                var prev = strStk.Pop();
                StringBuilder sb = new StringBuilder(prev);
                for (var i = 0; i < times; i++)
                {
                    sb.Append(cur);
                }

                cur = sb;
            }
            else
            {
                // 普通字符直接追加
                cur.Append(c);
            }
        }

        return cur.ToString();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
