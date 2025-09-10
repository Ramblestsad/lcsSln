/*
 * @lc app=leetcode id=21 lang=csharp
 * @lcpr version=30203
 *
 * [21] Merge Two Sorted Lists
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
    public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2) {
        // dummy: a placeholder node that points to the head of the merged list
        var dummy = new ListNode();
        // current points the current merging position
        var current = dummy;

        // traver two lists until one of them exhausted
        while (list1 != null && list2 != null) {
            if (list1.val <= list2.val) {
                // append smaller node to merging node
                current.next = list1;
                list1 = list1.next;
            } else {
                current.next = list2;
                list2 = list2.next;
            }

            // move current pointer
            current = current.next;
        }

        // traverse the remaining nodes for one of the lists
        if (list1 != null) {
            current.next = list1;
        }

        if (list2 != null) {
            current.next = list2;
        }

        return dummy.next;
    }
}
// @lc code=end

/*
@lcpr case=start
[1,2,4]\n[1,3,4]\n
@lcpr case=end

@lcpr case=start
[]\n[]\n
@lcpr case=end

@lcpr case=start
[]\n[0]\n
@lcpr case=end
 */
