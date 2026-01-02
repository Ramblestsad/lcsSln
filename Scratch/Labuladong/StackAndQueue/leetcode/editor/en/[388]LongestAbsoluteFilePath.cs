namespace Scratch.Labuladong.Algorithms.LongestAbsoluteFilePath;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LengthLongestPath(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }

        // stack[depth] = 当前 depth 的“到该层目录为止”的累计长度（包含末尾的 '/'）
        // depth 从 0 开始，stack[0] 作为哨兵 0
        var stk = new int[input.Length + 1];
        stk[0] = 0;
        var max = 0;

        foreach (var line in input.Split("\n"))
        {
            var depth = 0;
            while (line[depth] == '\t')
            {
                depth++;
            }

            var nameLen = line.Length - depth;
            // 父层长度 + 当前名字长度 + '/'（如果是目录）
            var curLen = stk[depth] + nameLen;

            var isFile = line.IndexOf('.', depth) >= 0;
            if (isFile)
            {
                if (curLen > max) max = curLen;
            }
            else
            {
                stk[depth + 1] = curLen + 1;
            }
        }

        return max;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
