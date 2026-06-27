namespace Scratch.Labuladong.Algorithms.LongestSubstringWithAtLeastKRepeatingCharacters;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestSubstring(string s, int k)
    {
        /*
         * 1、什么时候应该扩大窗口？窗口中字符种类小于 count 时扩大窗口。
         * 2、什么时候应该缩小窗口？窗口中字符种类大于 count 时缩小窗口。
         * 3、什么时候得到一个合法的答案？窗口中所有字符出现的次数都大于等于 k 时，得到一个合法的子串。
         * 因为 s 中只包含小写字母，所以 count 的取值也就是 1~26
         * 所以最后用一个 for 循环把这些值都输入 LongestKLetterSubstr 计算一遍，求最大值就是题目想要的答案
         */

        var len = 0;
        for (var i = 1; i <= 26; i++)
        {
            len = Math.Max(len, LongestKLetterSubstr(s, k, i));
        }

        return len;
    }

    int LongestKLetterSubstr(string s, int k, int count)
    {
        var res = 0;
        int left = 0, right = 0;
        var windowCount = new int[26];
        // 记录窗口中存在几种不同的字符（字符种类）
        var windowUniqueCount = 0;
        // 记录窗口中有几种字符的出现次数达标（大于等于 k）
        var windowValidCount = 0;

        while (right < s.Length)
        {
            // 移入字符，扩大窗口
            var c = s[right];
            if (windowCount[c - 'a'] == 0) windowUniqueCount++;
            windowCount[c - 'a']++;
            if (windowCount[c - 'a'] == k) windowValidCount++;
            right++;

            // 当窗口中字符种类大于 count 时，缩小窗口
            while (windowUniqueCount > count)
            {
                // 移出字符，缩小窗口
                var d = s[left];
                if (windowCount[d - 'a'] == k) windowValidCount--;
                windowCount[d - 'a']--;
                if (windowCount[d - 'a'] == 0) windowUniqueCount--;
                left++;
            }

            // 当窗口中字符种类为 count 且每个字符出现次数都满足 k 时，更新答案
            if (windowValidCount == count) res = Math.Max(res, right - left);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
