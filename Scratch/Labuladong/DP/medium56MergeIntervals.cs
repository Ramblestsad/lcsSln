namespace Scratch.Labuladong.Algorithms.MergeIntervals;

// 56. Merge Intervals (Medium)
//
// Given an array of intervals where intervals[i] = [start_i, end_i], merge all overlapping
// intervals, and return an array of the non-overlapping intervals that cover all the intervals in
// the input.
//
// Example 1:
//
// Input: intervals = [[1,3],[2,6],[8,10],[15,18]]
// Output: [[1,6],[8,10],[15,18]]
// Explanation: Since intervals [1,3] and [2,6] overlap, merge them into [1,6].
//
// Example 2:
//
// Input: intervals = [[1,4],[4,5]]
// Output: [[1,5]]
// Explanation: Intervals [1,4] and [4,5] are considered overlapping.
//
// Example 3:
//
// Input: intervals = [[4,7],[1,4]]
// Output: [[1,7]]
// Explanation: Intervals [1,4] and [4,7] are considered overlapping.
//
// Constraints:
//
// - 1 <= intervals.length <= 10^4
//
// - intervals[i].length == 2
//
// - 0 <= start_i <= end_i <= 10^4
//
// Related Topics: Array, Sorting

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
