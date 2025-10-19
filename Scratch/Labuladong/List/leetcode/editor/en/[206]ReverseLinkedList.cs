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

    public ListNode? ReverseListRecursive(ListNode? head) {
        if (head == null || head.next == null) {
            return head;
        }

        var last = ReverseListRecursive(head.next); // last 始终就是原list的last node
        // head.next 一定不为null 且就是 正在反转的node
        head.next.next = head; // node下一个就是 (head.next).next
        head.next = null; // 现在head就是相对的新tail，置为null

        return last;
    }

    public ListNode? ReverseN(ListNode? head, int n) {
        if (head == null || head.next == null) {
            return head;
        }

        ListNode? pre = null;
        var cur = head;
        var next = head.next;

        while (n > 0) {
            cur.next = pre;
            pre = cur;
            cur = next;
            if (next.next != null) next = next.next;

            n--;
        }

        // 此时的 cur 是第 n + 1 个节点，head 是反转后的尾结点，所以接上
        head.next = cur;

        return pre;
    }

    private ListNode? _successor = null;

    public ListNode? ReverseNRecursive(ListNode? head, int n) {
        if (head == null) {
            // n > length of list
            return head;
        }
        if (n == 1 || head.next == null) {
            // 记录第 n + 1 个节点
            _successor = head.next;
            return head;
        }

        // 以 head.next 为起点，需要反转前 n - 1 个节点
        var last = ReverseNRecursive(head.next, n - 1);

        head.next.next = head; // 这一步会覆盖之前的head.next = _successor
        // 让反转之后的 head 节点和后面的节点连起来
        head.next = _successor;
        return last;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
