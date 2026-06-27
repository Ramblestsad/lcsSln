namespace Scratch.Labuladong.Algorithms.LongestConsecutiveSeq;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        var set = new HashSet<int>();

        foreach (var num in nums)
        {
            set.Add(num);
        }

        var res = 0;

        foreach (var num in set)
        {
            // num 不是子序列的第一个（最小）
            if (set.Contains(num - 1)) continue;

            // num 是子序列的第一个
            var curNum = num;
            var curLen = 1;

            while (set.Contains(curNum + 1))
            {
                curNum += 1;
                curLen += 1;
            }

            // update res
            res = Math.Max(res, curLen);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
