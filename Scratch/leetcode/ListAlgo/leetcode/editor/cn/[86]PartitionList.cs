using Scratch.leetcode.DataStructure;

namespace Scratch.leetcode;
//leetcode submit region begin(Prohibit modification and deletion)
public partial class Solution
{
    public ListNode? Partition(ListNode? head, int x)
    {
        var lowNode = new ListNode();
        var highNode = new ListNode();
        var lowHead = lowNode;
        var highHead = highNode;
        while (head != null)
        {
            if (head.val < x)
            {
                lowNode.next = head;
                lowNode = lowNode.next;
            }
            else
            {
                highNode.next = head;
                highNode = highNode.next;
            }

            head = head.next;
        }

        highNode.next = null;
        lowNode.next = highHead.next;

        return lowHead.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
