namespace Scratch.Labuladong.Algorithms.MyCalendarI;

//leetcode submit region begin(Prohibit modification and deletion)
public class MyCalendar
{
    // 什么情况下一个日程会有冲突？有两种情况:
    // 1、之前的那个日程还没结束，这个日程就开始了
    // 2、这个日程还没结束的时候，下个日程就开始了

    private SortedSet<(int start, int end)> events = new();

    public bool Book(int startTime, int endTime)
    {
        var cur = ( start: startTime, end: endTime );

        // 找到 start <= startTime 的最近一个（earlier）
        var earlierView = events.GetViewBetween(( int.MinValue, int.MinValue ), cur);
        if (earlierView.Count > 0)
        {
            var earlier = earlierView.Max;
            // 1、之前的那个日程还没结束，这个日程就开始了
            if (startTime < earlier.end) return false;
        }

        // 找到 start >= startTime 的最近一个（later）
        var laterView = events.GetViewBetween(cur, ( int.MaxValue, int.MaxValue ));
        if (laterView.Count > 0)
        {
            var later = laterView.Min;
            // 2、这个日程还没结束的时候，下个日程就开始了
            if (endTime > later.start) return false;
        }

        events.Add(cur);
        return true;
    }
}

/**
 * Your MyCalendar object will be instantiated and called as such:
 * MyCalendar obj = new MyCalendar();
 * bool param_1 = obj.Book(startTime,endTime);
 */
//leetcode submit region end(Prohibit modification and deletion)
