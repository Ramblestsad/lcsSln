using Algorithm.DataStructure;

namespace Leetcode.ListAlgorithm;
//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)