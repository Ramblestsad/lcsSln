namespace Scratch.Labuladong.Algorithms.AddTwoNumbersII;

// 445. Add Two Numbers II (Medium)
//
// You are given two non-empty linked lists representing two non-negative integers. The most
// significant digit comes first and each of their nodes contains a single digit. Add the two
// numbers and return the sum as a linked list.
//
// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
//
// Example 1:
//
// Input: l1 = [7,2,4,3], l2 = [5,6,4]
// Output: [7,8,0,7]
//
// Example 2:
//
// Input: l1 = [2,4,3], l2 = [5,6,4]
// Output: [8,0,7]
//
// Example 3:
//
// Input: l1 = [0], l2 = [0]
// Output: [0]
//
// Constraints:
//
// - The number of nodes in each linked list is in the range [1, 100].
//
// - 0 <= Node.val <= 9
//
// - It is guaranteed that the list represents a number that does not have leading zeros.
//
// Follow up: Could you solve it without reversing the input lists?
//
// Related Topics: Linked List, Math, Stack

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
    public ListNode? AddTwoNumbers(ListNode? l1, ListNode? l2)
    {
        var stk1 = new Stack<int>();
        while (l1 != null)
        {
            stk1.Push(l1.val);
            l1 = l1.next;
        }

        var stk2 = new Stack<int>();
        while (l2 != null)
        {
            stk2.Push(l2.val);
            l2 = l2.next;
        }

        var dummy = new ListNode();
        var carry = 0;

        while (stk1.Count > 0 || stk2.Count > 0 || carry > 0)
        {
            var val = carry;
            if (stk1.Count > 0) val += stk1.Pop();
            if (stk2.Count > 0) val += stk2.Pop();

            carry = val / 10;
            val = val % 10;
            // 头插法构建结果链表:新节点插到头部
            var newNode = new ListNode(val);
            newNode.next = dummy.next;
            dummy.next = newNode;
        }

        return dummy.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
