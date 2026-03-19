namespace Scratch.Labuladong.Algorithms.SwapNodesInPairs;

//Given a linked list, swap every two adjacent nodes and return its head. You
//must solve the problem without modifying the values in the list's nodes (i.e.,
//only nodes themselves may be changed.)
//
//
// Example 1:
//
//
// Input: head = [1,2,3,4]
//
//
// Output: [2,1,4,3]
//
// Explanation:
//
//
//
// Example 2:
//
//
// Input: head = []
//
//
// Output: []
//
// Example 3:
//
//
// Input: head = [1]
//
//
// Output: [1]
//
// Example 4:
//
//
// Input: head = [1,2,3]
//
//
// Output: [2,1,3]
//
//
// Constraints:
//
//
// The number of nodes in the list is in the range [0, 100].
// 0 <= Node.val <= 100
//
//
// Related TopicsLinked List | Recursion
//
// 👍 13116, 👎 512bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

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
    public ListNode? SwapPairs(ListNode? head)
    {
        if (head is null) return head;
        var a = head;
        var b = head;

        for (int i = 0; i < 2; i++)
        {
            // 不足2个，不需要反转了
            if (b == null) return head;
            b = b.next;
        }

        var newHead = ReverseTwo(a);
        // 此时 b 指向下一组待反转的头结点, a是上一组的尾节点
        // 递归反转后续链表并连接起来
        a.next = SwapPairs(b);

        return newHead;
    }

    // 反正从head开始的2个节点
    ListNode? ReverseTwo(ListNode head)
    {
        if (head == null || head.next == null) return head;

        ListNode? pre = null;
        var cur = head;
        var next = head.next;

        for (int i = 0; i < 2; i++)
        {
            cur?.next = pre;
            pre = cur;
            cur = next;
            if (next != null) next = next.next;
        }

        head.next = cur;

        return pre;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
