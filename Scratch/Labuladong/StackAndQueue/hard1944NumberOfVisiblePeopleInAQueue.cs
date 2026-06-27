namespace Scratch.Labuladong.Algorithms.NumberOfVisiblePeopleInAQueue;

// 1944. Number of Visible People in a Queue (Hard)
//
// There are n people standing in a queue, and they numbered from 0 to n - 1 in left to right
// order. You are given an array heights of distinct integers where heights[i] represents the
// height of the i^th person.
//
// A person can see another person to their right in the queue if everybody in between is shorter
// than both of them. More formally, the i^th person can see the j^th person if i < j and
// min(heights[i], heights[j]) > max(heights[i+1], heights[i+2], ..., heights[j-1]).
//
// Return an array answer of length n where answer[i] is the number of people the i^th person can
// see to their right in the queue.
//
// Example 1:
//
// Input: heights = [10,6,8,5,11,9]
// Output: [3,1,2,1,1,0]
// Explanation:
// Person 0 can see person 1, 2, and 4.
// Person 1 can see person 2.
// Person 2 can see person 3 and 4.
// Person 3 can see person 4.
// Person 4 can see person 5.
// Person 5 can see no one since nobody is to the right of them.
//
// Example 2:
//
// Input: heights = [5,1,2,3,10]
// Output: [4,1,1,1,0]
//
// Constraints:
//
// - n == heights.length
//
// - 1 <= n <= 10^5
//
// - 1 <= heights[i] <= 10^5
//
// - All the values of heights are unique.
//
// Related Topics: Array, Stack, Monotonic Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] CanSeePersonsCount(int[] heights)
    {
        // 不是问你下一个更大元素是多少，而是问你当前元素和下一个更大元素之间的元素个数
        var n = heights.Length;
        var res = new int[n];
        var stk = new Stack<int>();

        for (var i = n - 1; i >= 0; i--)
        {
            // 记录右侧比自己矮的人
            var count = 0;
            while (stk.Count != 0 && stk.Peek() <= heights[i])
            {
                stk.Pop();
                count++;
            }

            // 不仅可以看到比自己矮的人，如果后面存在的第一个比自己更高的的人。
            res[i] = stk.Count == 0 ? count : count + 1;
            stk.Push(heights[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
