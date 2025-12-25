namespace Scratch.Labuladong.Algorithms.FindAllAnagramsInAString;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<int> FindAnagrams(string s, string p)
    {
        var need = new Dictionary<char, int>();
        var window = new Dictionary<char, int>();
        foreach (var c in p)
        {
            if (need.TryGetValue(c, out var v)) need[c] = v + 1;
            else need[c] = 1;
        }

        int left = 0, right = 0, valid = 0;
        // valid 变量表示窗口中满足 need 条件的字符个数
        // 如果 valid 和 need.size 的大小相同，则说明窗口已满足条件，已经完全覆盖了串 T
        List<int> res = [];

        while (right < s.Length)
        {
            var c = s[right];
            right++;

            // 进行 window 数据的一系列更新
            if (need.TryGetValue(c, out var cv))
            {
                if (window.TryGetValue(c, out var wv)) window[c] = wv + 1;
                else window[c] = 1;

                if (cv == window[c]) valid++;
            }

            // 判断左窗口是否要收缩
            while (( right - left ) >= p.Length)
            {
                // 当窗口符合条件时，把起始索引加入 res
                if (valid == need.Count)
                {
                    res.Add(left);
                }

                var d = s[left];
                left++;

                // 进行 window 数据的一系列更新
                if (need.TryGetValue(d, out var ndv))
                {
                    window.TryGetValue(d, out var wdv);
                    if (ndv == wdv) valid--;
                    window[d] = wdv - 1;
                }
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
