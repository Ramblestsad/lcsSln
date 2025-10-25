namespace Scratch.Labuladong.Algorithms.LongestPalindromicSubstring;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution {
    public string LongestPalindrome(string s)
    {
        // for 0 <= i < len(s):
        //     找到以 s[i] 为中心的回文串
        //     找到以 s[i] 和 s[i+1] 为中心的回文串
        //     更新答案
        var res = "";
        for (var i = 0; i < s.Length; i++)
        {
            // 以 s[i] 为中心的最长回文子串
            var s1 = Palindrome(s, i, i);
            // 以 s[i] 和 s[i+1] 为中心的最长回文子串
            var s2 = Palindrome(s, i, i + 1);
            // res = longest(res, s1, s2)
            res = res.Length > s1.Length ? res : s1;
            res = res.Length > s2.Length ? res : s2;
        }

        return res;
    }

    private static string Palindrome(string s, int l, int r)
    {
        while (l >= 0 && r < s.Length)
        {
            if (s[l] != s[r])
            {
                break;
            }

            // 从中心向两端扩散的双指针
            l--;
            r++;
        }

        // 此时 s[l+1..r-1] 就是最长回文串
        // 初始就不相等的情况会返回"" (s[x..x])
        return s[( l + 1 )..r];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
