namespace Scratch.Labuladong.Algorithms.PalindromeLinkedList;

// 234. Palindrome Linked List (Easy)
//
// Given the head of a singly linked list, return true if it is a palindrome or false otherwise.
//
// Example 1:
//
// Input: head = [1,2,2,1]
// Output: true
//
// Example 2:
//
// Input: head = [1,2]
// Output: false
//
// Constraints:
//
// - The number of nodes in the list is in the range [1, 10^5].
//
// - 0 <= Node.val <= 9
//
// Follow up: Could you do it in O(n) time and O(1) space?
//
// Related Topics: Linked List, Two Pointers, Stack, Recursion

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
    public bool IsPalindrome(ListNode head)
    {
        left = head;
        Traverse(head);

        return res;
    }

    // 从左向右移动的指针
    ListNode? left;

    // 记录链表是否为回文
    bool res = true;

    void Traverse(ListNode? right)
    {
        if (right == null) return;

        // 前序遍历位置

        Traverse(right.next);

        // 后续遍历位置
        // 此时的 right 指针指向链表右侧尾部
        // 所以可以和 left 指针比较，判断是否是回文链表
        if (left!.val != right.val) res = false;

        left = left.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
