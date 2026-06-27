namespace Scratch.Labuladong.Algorithms.RotateList;

// 61. Rotate List (Medium)
//
// Given the head of a linked list, rotate the list to the right by k places.
//
// Example 1:
//
// Input: head = [1,2,3,4,5], k = 2
// Output: [4,5,1,2,3]
//
// Example 2:
//
// Input: head = [0,1,2], k = 4
// Output: [2,0,1]
//
// Constraints:
//
// - The number of nodes in the list is in the range [0, 500].
//
// - -100 <= Node.val <= 100
//
// - 0 <= k <= 2 * 10^9
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
