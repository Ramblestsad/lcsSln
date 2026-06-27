namespace Scratch.Labuladong.Algorithms.RemoveNthFromEnd;

// 19. Remove Nth Node From End of List (Medium)
//
// Given the head of a linked list, remove the n^th node from the end of the list and return its
// head.
//
// Example 1:
//
// Input: head = [1,2,3,4,5], n = 2
// Output: [1,2,3,5]
//
// Example 2:
//
// Input: head = [1], n = 1
// Output: []
//
// Example 3:
//
// Input: head = [1,2], n = 1
// Output: [1]
//
// Constraints:
//
// - The number of nodes in the list is sz.
//
// - 1 <= sz <= 30
//
// - 0 <= Node.val <= 100
//
// - 1 <= n <= sz
//
// Follow up: Could you do this in one pass?
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
    public ListNode? RemoveNthFromEnd(ListNode head, int n)
    {
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

    private static ListNode FindFromEnd(ListNode head, int n)
    {
        var p1 = head;

        // move p1 n steps
        for (var i = 0; i < n; i++)
        {
            p1 = p1!.next;
        }

        var p2 = head;
        // move p1, p2 (n-k) steps
        // aka p1 = null
        while (p1 != null)
        {
            p1 = p1.next;
            p2 = p2!.next;
        }

        // now p2 points to reverse nth
        return p2!;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
