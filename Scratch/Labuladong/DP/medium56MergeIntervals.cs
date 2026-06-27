namespace Scratch.Labuladong.Algorithms.MergeIntervals;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        var res = new List<int[]>();

        // 按区间的 start 升序排列
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        res.Add(intervals[0]);

        for (var i = 1; i < intervals.Length; i++)
        {
            var curr = intervals[i];
            // res 中最后一个元素的引用
            var last = res[^1];
            if (curr[0] <= last[1])
            {
                last[1] = Math.Max(last[1], curr[1]);
            }
            else
            {
                // 处理下一个待合并区间
                res.Add(curr);
            }
        }

        return res.ToArray();
    }
}
//leetcode submit region end(Prohibit modification and deletion)
