

namespace Scratch.Labuladong.Algorithms.ValidPalindrome;

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
