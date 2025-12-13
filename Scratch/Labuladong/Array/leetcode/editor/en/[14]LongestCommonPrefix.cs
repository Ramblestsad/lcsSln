namespace Scratch.Labuladong.Algorithms.LongestCommonPrefix;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        // 把字符串列表看成一个二维数组，然后用一个嵌套 for 循环计算这个二维数组前面有多少列的元素完全相同即可
        var m = strs.Length;
        // 以第一行的列数为基准
        var n = strs[0].Length;

        for (int col = 0; col < n; col++)
        {
            for (int row = 1; row < m; row++)
            {
                var thisStr = strs[row];
                var prevStr = strs[row - 1];

                // 判断每个字符串的 col 索引是否都相同
                if (col >= thisStr.Length || col >= prevStr.Length ||
                    thisStr[col] != prevStr[col])
                {
                    // 发现不匹配的字符，只有 strs[row][0..col-1] 是公共前缀
                    return strs[row][0..col];
                }
            }
        }

        return strs[0];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
