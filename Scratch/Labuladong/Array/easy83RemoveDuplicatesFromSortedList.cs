/*
 * @lc app=leetcode id=83 lang=csharp
 * @lcpr version=30402
 *
 * [83] Remove Duplicates From Sorted List
 */

namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedList;

// @lc code=start
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
// @lc code=end
