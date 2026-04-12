/*
 * @lc app=leetcode id=125 lang=csharp
 * @lcpr version=30402
 *
 * [125] Valid Palindrome
 */

namespace Scratch.Labuladong.Algorithms.ValidPalindrome;

// @lc code=start
using System.Text;

public class Solution
{
    public bool IsPalindrome(string s)
    {

        var builder = new StringBuilder(s.Length);
        foreach (var ch in s)
        {
            if (char.IsLetterOrDigit(ch))
            {
                builder.Append(char.ToLowerInvariant(ch));
            }
        }

        s = builder.ToString();

        int left = 0, right = s.Length - 1;
        while (left < right)
        {
            if (s[left] != s[right])
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }
}
// @lc code=end
