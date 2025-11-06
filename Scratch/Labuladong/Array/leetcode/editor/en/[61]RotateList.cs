namespace Scratch.Labuladong.Algorithms.RotateList;

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
    public ListNode? RotateRight(ListNode head, int k)
    {
        if (head == null) return null;

        var tail = head;
        var n = 1;
        while (tail.next != null)
        {
            tail = tail.next;
            n++;
        }

        k %= n;
        if (k == 0) return head; // 相当于绕一圈，返回原链表即可
        tail.next = head; // 原链表接尾、头相接

        var newTailIndex = n - k;
        var newTail = head;
        for (int i = 1; i < newTailIndex; i++)
        {
            newTail = newTail!.next;
        }

        var newHead = newTail!.next;
        newTail.next = null;

        return newHead;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
