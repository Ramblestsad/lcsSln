namespace Scratch.Labuladong.Algorithms.RemoveNthFromEnd;

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
    public ListNode? RemoveNthFromEnd(ListNode head, int n) {
        var dummy = new ListNode();
        dummy.next = head;

        // find N+1 th from end
        var x = FindFromEnd(dummy, n + 1);
        // why pass in dummy not dummy.next or head?
        // [1,3,2,5,4] n=5
        // Because this fix the corner case n = k(number of list nodes, which is 1st node)
        // 这种情况，x 就是 dummy

        // points to grandson
        x.next = x.next?.next;

        return dummy.next;
    }

    private static ListNode FindFromEnd(ListNode head, int n) {
        var p1 = head;

        // move p1 n steps
        for (var i = 0; i < n; i++) {
            p1 = p1!.next;
        }

        var p2 = head;
        // move p1, p2 (n-k) steps
        // aka p1 = null
        while (p1 != null) {
            p1 = p1.next;
            p2 = p2!.next;
        }

        // now p2 points to reverse nth
        return p2!;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
