namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedList;

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode? DeleteDuplicates(ListNode? head)
    {
        if (head == null) return null;

        var fast = head.next;
        var slow = head;

        while (fast != null)
        {
            if (fast.val != slow.val)
            {
                slow.next = fast;
                slow = slow.next;
            }

            fast = fast.next;
        }

        // 记得断开与后面重复元素的连接
        slow.next = null;

        return head;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
