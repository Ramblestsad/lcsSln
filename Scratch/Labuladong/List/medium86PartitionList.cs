/*
 * @lc app=leetcode id=86 lang=csharp
 * @lcpr version=30203
 *
 * [86] Partition List
 */

namespace Scratch.Labuladong.Algorithms;

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
public partial class Solution {
    public ListNode? Partition(ListNode? head, int x) {
        var less = new ListNode();
        var greater = new ListNode();
        var p1 = less;
        var p2 = greater;
        var p = head;

        while (p != null) {
            if (p.val < x) {
                p1.next = p;
                p1 = p1.next;
            } else {
                p2.next = p;
                p2 = p2.next;
            }

            var temp = p.next;
            p.next = null;
            p = temp;
        }

        p1.next = greater.next;
        return less.next;
    }
}
// @lc code=end

/*
@lcpr case=start
[1,4,3,2,5,2]\n3\n
@lcpr case=end

@lcpr case=start
[2,1]\n2\n
@lcpr case=end
 */
