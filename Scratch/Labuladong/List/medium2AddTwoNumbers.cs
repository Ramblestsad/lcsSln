namespace Scratch.Labuladong.Algorithms.AddTwoNumbers;

// 2. Add Two Numbers (Medium)
//
// You are given two non-empty linked lists representing two non-negative integers. The digits are
// stored in reverse order, and each of their nodes contains a single digit. Add the two numbers
// and return the sum as a linked list.
//
// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
//
// Example 1:
//
// Input: l1 = [2,4,3], l2 = [5,6,4]
// Output: [7,0,8]
// Explanation: 342 + 465 = 807.
//
// Example 2:
//
// Input: l1 = [0], l2 = [0]
// Output: [0]
//
// Example 3:
//
// Input: l1 = [9,9,9,9,9,9,9], l2 = [9,9,9,9]
// Output: [8,9,9,9,0,0,0,1]
//
// Constraints:
//
// - The number of nodes in each linked list is in the range [1, 100].
//
// - 0 <= Node.val <= 9
//
// - It is guaranteed that the list represents a number that does not have leading zeros.
//
// Related Topics: Linked List, Math, Recursion

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
    public ListNode? AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var dummy = new ListNode();
        var p = dummy;
        var p1 = l1;
        var p2 = l2;
        var carry = 0;

        while (p1 != null || p2 != null || carry > 0)
        {
            var val = carry;
            if (p1 != null)
            {
                val += p1.val;
                p1 = p1.next;
            }

            if (p2 != null)
            {
                val += p2.val;
                p2 = p2.next;
            }

            carry = val / 10;
            p.next = new ListNode(val % 10);
            p = p.next;
        }

        return dummy.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
