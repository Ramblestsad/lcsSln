namespace Scratch.Labuladong.Algorithms.LongestRepeatingCharacterReplacement;

// 424. Longest Repeating Character Replacement (Medium)
//
// You are given a string s and an integer k. You can choose any character of the string and change
// it to any other uppercase English character. You can perform this operation at most k times.
//
// Return the length of the longest substring containing the same letter you can get after
// performing the above operations.
//
// Example 1:
//
// Input: s = "ABAB", k = 2
// Output: 4
// Explanation: Replace the two 'A's with two 'B's or vice versa.
//
// Example 2:
//
// Input: s = "AABABBA", k = 1
// Output: 4
// Explanation: Replace the one 'A' in the middle with 'B' and form "AABBBBA".
// The substring "BBBB" has the longest repeating letters, which is 4.
// There may exists other ways to achieve this answer too.
//
// Constraints:
//
// - 1 <= s.length <= 10^5
//
// - s consists of only uppercase English letters.
//
// - 0 <= k <= s.length
//
// Related Topics: Hash Table, String, Sliding Window

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int CharacterReplacement(string s, int k)
    {
        /*
         * 1、什么时候应该扩大窗口？
         * 2、什么时候应该缩小窗口？
         * 3、什么时候得到一个合法的答案？
         * 针对本题，以上三个问题的答案是：
         * 1、right - left - windowMaxCount <= k 时，
         *      所有进入窗口的字符都可以被替换成出现次数最多的字符 x，使得窗口内的所有元素都是重复的。
         * 2、当 right - left - windowMaxCount > k 时，
         *      必须缩小窗口。因为此时窗口已经不合法，用尽 k 次替换机会也会有一个字符无法替换成 x。
         * 3、只要可替换次数 k 大于等于 0，窗口中的字符串都可以全部替换成相同的，想求的是一个最大窗口长度。
         */

        int left = 0, right = 0;
        // 统计窗口中每个字符的出现次数
        var windowCharCount = new int[26];
        // 记录窗口中字符的最多重复次数
        // 记录这个值的意义在于，最划算的替换方法肯定是把其他字符替换成出现次数最多的那个字符
        var windowMaxCount = 0;
        // 记录结果长度
        var res = 0;

        while (right < s.Length)
        {
            var c = s[right] - 'A';
            windowCharCount[c]++;
            windowMaxCount = Math.Max(windowMaxCount, windowCharCount[c]);
            right++;

            while (right - left - windowMaxCount > k)
            {
                // 杂牌字符数量 right - left - windowMaxCount 多于 k
                // 此时，k 次替换已经无法把窗口内的字符都替换成相同字符了
                // 必须缩小窗口
                windowCharCount[s[left] - 'A']--;
                left++;
            }

            // 经过收缩后，此时一定是一个合法的窗口
            res = Math.Max(res, right - left);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
