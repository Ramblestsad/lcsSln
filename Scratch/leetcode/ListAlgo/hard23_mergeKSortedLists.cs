/*
 * @lc app=leetcode.cn id=23 lang=csharp
 *
 * [23] Merge k Sorted Lists
 */

using Algorithm.DataStructure;

namespace VSC.Leetcode.ListALgorithm;
// @lc code=start
public partial class Solution
{
    public ListNode? MergeKLists(ListNode?[] lists)
    {
        var pq = new PriorityQueue<ListNode, int>();
        foreach (ListNode? node in lists)
        {
            if (node != null)
            {
                pq.Enqueue(node, node.Val);
            }
        }

        ListNode ans = new ListNode(), d = ans;
        while (pq.Count > 0)
        {
            ListNode t = pq.Dequeue();
            d.Next = t;
            d = d.Next;
            if (t.Next != null)
            {
                pq.Enqueue(t.Next, t.Next.Val);
            }
        }

        return ans.Next;
    }
}
// @lc code=end
