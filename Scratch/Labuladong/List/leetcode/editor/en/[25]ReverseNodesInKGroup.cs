namespace Scratch.Labuladong.Algorithms.ReverseNodesInKGroup;

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
    public ListNode? ReverseKGroup(ListNode head, int k)
    {
        // 检验区间 [a, b) 包含 k 个待反转元素
        // 也是递归 base case
        var a = head;
        var b = head;
        for (var i = 0; i < k; i++)
        {
            // 不足 k 个，不需要反转了
            if (b == null) return head;
            b = b.next;
        }

        var newHead = ReverseN(a, k);
        // 此时 b 指向下一组待反转的头结点, a即为新的tail
        // 递归反转后续链表并连接起来
        a.next = ReverseKGroup(b!, k);

        return newHead;
    }

    static ListNode? ReverseN(ListNode head, int n)
    {
        if (head.next == null) return head;

        ListNode? pre = null;
        var cur = head;
        var nxt = head.next;

        while (n > 0)
        {
            cur.next = pre;
            pre = cur;
            cur = nxt;
            if (nxt.next != null) nxt = nxt.next;

            n--;
        }

        // 此时的 cur 是第 n + 1 个节点，head 是反转后的尾结点
        head.next = cur;

        return pre;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
