namespace Scratch.Labuladong.Algorithms.ReverseLinkedList;

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
public partial class Solution {
    public ListNode? ReverseList(ListNode head) {
        if (head == null || head.next == null) {
            return head;
        }

        ListNode? pre = null;
        var cur = head;
        var next = head.next;

        //       1  ->  2  ->  5  ->  4
        // pre  cur   next
        //      pre   cur     next
        while (cur != null) {
            // 逐个节点翻转
            cur.next = pre; // 当前节点的下个节点变前一个
            // update 指针：每个指针都往下进一步
            pre = cur;
            cur = next;
            if (next != null) next = next.next;
        }

        return pre;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
