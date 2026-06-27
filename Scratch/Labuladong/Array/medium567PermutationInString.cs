namespace Scratch.Labuladong.Algorithms.PermutationInString;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 判断 s2 是否包含 s1 的排列
    public bool CheckInclusion(string s1, string s2)
    {
        var need = new Dictionary<char, int>();
        var window = new Dictionary<char, int>();
        foreach (var c in s1)
        {
            if (need.TryGetValue(c, out var v)) need[c] = v + 1;
            else need[c] = 1;
        }

        int left = 0, right = 0;
        // valid 变量表示窗口中满足 need 条件的字符个数
        // 如果 valid 和 need.size 的大小相同，则说明窗口已满足条件，已经完全覆盖了串 T
        int valid = 0;

        while (right < s2.Length)
        {
            var c = s2[right];
            right++;
            // 进行 window 数据的一系列更新
            if (need.TryGetValue(c, out var cv))
            {
                if (window.TryGetValue(c, out var wv)) window[c] = wv + 1;
                else window[c] = 1;
                if (cv == window[c]) valid++;
            }

            // 判断左侧窗口是否要收缩
            while (( right - left ) >= s1.Length)
            {
                // 判断是否找到了合法的子串
                if (valid == need.Count) return true;
                var d = s2[left];
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

        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
