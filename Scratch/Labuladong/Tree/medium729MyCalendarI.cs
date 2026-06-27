namespace Scratch.Labuladong.Algorithms.MyCalendarI;

// 729. My Calendar I (Medium)
//
// You are implementing a program to use as your calendar. We can add a new event if adding the
// event will not cause a double booking.
//
// A double booking happens when two events have some non-empty intersection (i.e., some moment is
// common to both events.).
//
// The event can be represented as a pair of integers startTime and endTime that represents a
// booking on the half-open interval [startTime, endTime), the range of real numbers x such that
// startTime <= x < endTime.
//
// Implement the MyCalendar class:
//
// - MyCalendar() Initializes the calendar object.
//
// - boolean book(int startTime, int endTime) Returns true if the event can be added to the
// calendar successfully without causing a double booking. Otherwise, return false and do not add
// the event to the calendar.
//
// Example 1:
//
// Input
// ["MyCalendar", "book", "book", "book"]
// [[], [10, 20], [15, 25], [20, 30]]
// Output
// [null, true, false, true]
//
// Explanation
// MyCalendar myCalendar = new MyCalendar();
// myCalendar.book(10, 20); // return True
// myCalendar.book(15, 25); // return False, It can not be booked because time 15 is already booked
// by another event.
// myCalendar.book(20, 30); // return True, The event can be booked, as the first event takes every
// time less than 20, but not including 20.
//
// Constraints:
//
// - 0 <= start < end <= 10^9
//
// - At most 1000 calls will be made to book.
//
// Related Topics: Array, Binary Search, Design, Segment Tree, Ordered Set

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
