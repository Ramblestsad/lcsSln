namespace Scratch.Labuladong.Algorithms.ReverseLinkedListII;

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
    public ListNode? ReverseBetween(ListNode head, int left, int right) {
        // 找到第 left - 1 个节点，然后复用之前实现的 reverseN 函数
        if (left == 1) {
            // identical to reverse n
            return ReverseN(head, right);
        }

        var pre = head;
        for (int i = 1; i < left - 1; i++) {
            pre = pre!.next;
        }
        // 从第left个节点开始反转 n (right - left) 个节点
        pre!.next = ReverseN(pre.next, right - left + 1);

        return head;
    }

    static ListNode? ReverseN(ListNode? head, int n) {
        if (head == null || head.next == null) {
            return head;
        }

        ListNode? pre = null;
        var cur = head;
        var nxt = head.next;
        while (n > 0) {
            cur!.next = pre;
            pre = cur;
            cur = nxt;
            if (nxt != null) nxt = nxt.next;

            n--;
        }

        // 此时的 cur 是第 n + 1 个节点，head 是反转后的尾结点
        head.next = cur;

        return pre;
    }

    public ListNode? ReverseBetweenRecursive(ListNode head, int left, int right) {
        if (left == 1) return ReverseNRecursive(head, right);
        // left right 同步减一，最后right就剩区间长度n

        // 前进到反转的起点触发 base case
        head.next = ReverseBetweenRecursive(head.next!, left - 1, right - 1);
        return head;
    }

    private ListNode? _successor = null;

    ListNode? ReverseNRecursive(ListNode head, int n) {
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
