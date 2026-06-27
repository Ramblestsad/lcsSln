namespace Scratch.Labuladong.Algorithms.LongestCommonPrefix;

// 14. Longest Common Prefix (Easy)
//
// Write a function to find the longest common prefix string amongst an array of strings.
//
// If there is no common prefix, return an empty string "".
//
// Example 1:
//
// Input: strs = ["flower","flow","flight"]
// Output: "fl"
//
// Example 2:
//
// Input: strs = ["dog","racecar","car"]
// Output: ""
// Explanation: There is no common prefix among the input strings.
//
// Constraints:
//
// - 1 <= strs.length <= 200
//
// - 0 <= strs[i].length <= 200
//
// - strs[i] consists of only lowercase English letters if it is non-empty.
//
// Related Topics: Array, String, Trie

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        // 把字符串列表看成一个二维数组，
        // 外层循环以第一行（即 strs[0]）的字符数为最大列数 n。
        // 对每一列 col，内层循环从第二行开始与前一行比较该列字符
        var m = strs.Length;
        // 以第一行的列数为基准
        var n = strs[0].Length;

        // ["flower",
        //  "flow",
        //  "flight"]
        for (int col = 0; col < n; col++)
        {
            for (int row = 1; row < m; row++)
            {
                var thisStr = strs[row];
                var prevStr = strs[row - 1];

                // 若某个字符串长度不够
                // 或当前列字符不同
                if (col >= thisStr.Length || col >= prevStr.Length ||
                    thisStr[col] != prevStr[col])
                {
                    // 说明公共前缀在 0..col-1
                    return strs[row][0..col];
                }
            }
        }

        // 若所有列都匹配，则整个第一行即为答案
        return strs[0];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
