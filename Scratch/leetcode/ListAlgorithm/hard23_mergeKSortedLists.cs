/*
 * @lc app=leetcode.cn id=23 lang=csharp
 *
 * [23] Merge k Sorted Lists
 */


using Algorithm.DataStructure;

namespace Leetcode.ListAlgorithm;

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
                pq.Enqueue(node, node.val);
            }
        }
        ListNode ans = new ListNode(), d = ans;
        while (pq.Count > 0)
        {
            ListNode t = pq.Dequeue();
            d.next = t;
            d = d.next;
            if (t.next != null)
            {
                pq.Enqueue(t.next, t.next.val);
            }
        }
        return ans.next;
    }
}
// @lc code=end
