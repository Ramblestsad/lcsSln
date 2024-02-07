using Scratch.leetcode.DataStructure;

namespace Scratch.leetcode;
//leetcode submit region begin(Prohibit modification and deletion)
public partial class Solution
{
    public ListNode? MergeKLists(ListNode?[] lists)
    {
        if (lists.Length == 0)
        {
            return null;
        }

        var dummyHead = new ListNode(0);
        var pq = new PriorityQueue<ListNode, int>();
        foreach (var head in lists)
        {
            if (head != null)
            {
                pq.Enqueue(head, head.val);
            }
        }

        var p = dummyHead;
        while (pq.Count > 0)
        {
            var node = pq.Dequeue();
            p.next = node;
            p = p.next;
            node = node.next;
            if (node != null)
            {
                pq.Enqueue(node, node.val);
            }
        }

        return dummyHead.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
