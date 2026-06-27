using System.Text;

namespace Scratch.Labuladong.Algorithms.LetterCombinationsOfAPhoneNumber;

// 17. Letter Combinations of a Phone Number (Medium)
//
// Given a string containing digits from 2-9 inclusive, return all possible letter combinations
// that the number could represent. Return the answer in any order.
//
// A mapping of digits to letters (just like on the telephone buttons) is given below. Note that 1
// does not map to any letters.
//
// Example 1:
//
// Input: digits = "23"
// Output: ["ad","ae","af","bd","be","bf","cd","ce","cf"]
//
// Example 2:
//
// Input: digits = "2"
// Output: ["a","b","c"]
//
// Constraints:
//
// - 1 <= digits.length <= 4
//
// - digits[i] is a digit in the range ['2', '9'].
//
// Related Topics: Hash Table, String, Backtracking

//leetcode submit region begin(Prohibit modification and deletion)

public class Solution
{
    // 每个数字到字母的映射, 前面留两个占位，输入只包含2-9
    private string[] mapping = ["", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"];
    private List<string> res = [];
    private StringBuilder sb = new();

    public IList<string> LetterCombinations(string digits)
    {
        if (digits.Length == 0) return res;
        // 从 digits[0] 开始进行回溯
        _backtrack(digits, 0);
        return res;
    }

    private void _backtrack(string digits, int start)
    {
        if (sb.Length == digits.Length)
        {
            // 到达回溯树底部
            res.Add(sb.ToString());
            return;
        }

        var digit = digits[start] - '0';
        foreach (var c in mapping[digit])
        {
            // 做选择
            sb.Append(c);
            _backtrack(digits, start + 1);
            // 撤销
            sb.Remove(sb.Length - 1, 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
