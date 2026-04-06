/*
 * @lc app=leetcode id=160 lang=csharp
 * @lcpr version=30402
 *
 * [160] Intersection Of Two Linked Lists
 */

namespace Scratch.Labuladong.Algorithms.GetIntersectionNode;

// @lc code=start
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode? GetIntersectionNode(ListNode headA, ListNode headB)
    {
        // p1 指向 A 链表头结点，p2 指向 B 链表头结点
        ListNode? p1 = headA, p2 = headB;
        while (p1 != p2)
        {
            // p1 走一步，如果走到 A 链表末尾，转到 B 链表
            if (p1 == null) p1 = headB;
            else p1 = p1.next;
            // p2 走一步，如果走到 B 链表末尾，转到 A 链表
            if (p2 == null) p2 = headA;
            else p2 = p2.next;
        }

        return p1;
    }
}
// @lc code=end
