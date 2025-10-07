namespace Scratch.Labuladong.Algorithms.AddTwoNumbers;

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
public class Solution {
    public ListNode? AddTwoNumbers(ListNode l1, ListNode l2) {
        var dummy = new ListNode();
        var p = dummy;
        var p1 = l1;
        var p2 = l2;
        var carry = 0;

        while (p1 != null || p2 != null || carry > 0) {
            var val = carry;
            if (p1 != null) {
                val += p1.val;
                p1 = p1.next;
            }
            if (p2 != null) {
                val += p2.val;
                p2 = p2.next;
            }

            carry = val / 10;
            val = val % 10;
            // 构建新节点
            p.next = new ListNode(val);
            p = p.next;
        }

        return dummy.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
