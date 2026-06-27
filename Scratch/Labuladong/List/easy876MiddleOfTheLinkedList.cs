namespace Scratch.Labuladong.Algorithms.MiddleNode;

// 876. Middle of the Linked List (Easy)
//
// Given the head of a singly linked list, return the middle node of the linked list.
//
// If there are two middle nodes, return the second middle node.
//
// Example 1:
//
// Input: head = [1,2,3,4,5]
// Output: [3,4,5]
// Explanation: The middle node of the list is node 3.
//
// Example 2:
//
// Input: head = [1,2,3,4,5,6]
// Output: [4,5,6]
// Explanation: Since the list has two middle nodes with values 3 and 4, we return the second one.
//
// Constraints:
//
// - The number of nodes in the list is in the range [1, 100].
//
// - 1 <= Node.val <= 100
//
// Related Topics: Linked List, Two Pointers

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
    public ListNode MiddleNode(ListNode head)
    {
        // 每当慢指针 slow 前进一步，快指针 fast 就前进两步，这样，当 fast 走到链表末尾时，slow 就指向了链表中点。
        var slow = head;
        var fast = head;

        while (fast != null && fast.next != null)
        {
            // Note:
            // 这里需要同时判断 fast != null 和 fast.next != null
            // 如果长度为偶数，fast.next 可能会是 null，此时再访问 fast.next.next 就会抛出异常
            // 如果长度为奇数，fast正好为最后一个节点时，会进入循环，slow会多走一步。
            slow = slow?.next;
            fast = fast.next?.next;
        }

        return slow!;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
