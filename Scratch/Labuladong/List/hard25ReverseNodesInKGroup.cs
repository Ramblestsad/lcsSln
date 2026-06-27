namespace Scratch.Labuladong.Algorithms.ReverseNodesInKGroup;

// 25. Reverse Nodes in k-Group (Hard)
//
// Given the head of a linked list, reverse the nodes of the list k at a time, and return the
// modified list.
//
// k is a positive integer and is less than or equal to the length of the linked list. If the
// number of nodes is not a multiple of k then left-out nodes, in the end, should remain as it is.
//
// You may not alter the values in the list's nodes, only nodes themselves may be changed.
//
// Example 1:
//
// Input: head = [1,2,3,4,5], k = 2
// Output: [2,1,4,3,5]
//
// Example 2:
//
// Input: head = [1,2,3,4,5], k = 3
// Output: [3,2,1,4,5]
//
// Constraints:
//
// - The number of nodes in the list is n.
//
// - 1 <= k <= n <= 5000
//
// - 0 <= Node.val <= 1000
//
// Follow-up: Can you solve the problem in O(1) extra memory space?
//
// Related Topics: Linked List, Recursion

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
