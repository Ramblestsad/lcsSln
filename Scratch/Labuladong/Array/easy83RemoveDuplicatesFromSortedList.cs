namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedList;

// 83. Remove Duplicates from Sorted List (Easy)
//
// Given the head of a sorted linked list, delete all duplicates such that each element appears
// only once. Return the linked list sorted as well.
//
// Example 1:
//
// Input: head = [1,1,2]
// Output: [1,2]
//
// Example 2:
//
// Input: head = [1,1,2,3,3]
// Output: [1,2,3]
//
// Constraints:
//
// - The number of nodes in the list is in the range [0, 300].
//
// - -100 <= Node.val <= 100
//
// - The list is guaranteed to be sorted in ascending order.
//
// Related Topics: Linked List

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
    public ListNode? DeleteDuplicates(ListNode? head)
    {
        if (head == null) return null;

        var fast = head.next;
        var slow = head;

        while (fast != null)
        {
            if (fast.val != slow.val)
            {
                slow.next = fast;
                slow = slow.next;
            }

            fast = fast.next;
        }

        // 记得断开与后面重复元素的连接
        slow.next = null;

        return head;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
