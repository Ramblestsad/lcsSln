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
//leetcode submit region end(Prohibit modification and deletion)
