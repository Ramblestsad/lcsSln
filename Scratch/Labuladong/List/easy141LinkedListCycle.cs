/*
 * @lc app=leetcode id=141 lang=csharp
 * @lcpr version=30402
 *
 * [141] Linked List Cycle
 */

namespace Scratch.Labuladong.Algorithms.LinkedListCycle;

// @lc code=start
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution
{
    public bool HasCycle(ListNode head)
    {
        ListNode? slow = head, fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow?.next;
            fast = fast.next.next;
            if (slow == fast) return true;
        }

        return false;
    }
}
// @lc code=end
