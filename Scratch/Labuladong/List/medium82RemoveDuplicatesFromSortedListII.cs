namespace Scratch.Labuladong.Algorithms.DeleteDuplicates;

// 82. Remove Duplicates from Sorted List II (Medium)
//
// Given the head of a sorted linked list, delete all nodes that have duplicate numbers, leaving
// only distinct numbers from the original list. Return the linked list sorted as well.
//
// Example 1:
//
// Input: head = [1,2,3,3,4,4,5]
// Output: [1,2,5]
//
// Example 2:
//
// Input: head = [1,1,1,2,3]
// Output: [2,3]
//
// Constraints:
//
// - The number of nodes in the list is in the range [0, 300].
//
// - -100 <= Node.val <= 100
//
// - The list is guaranteed to be sorted in ascending order.
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
    public ListNode? DeleteDuplicates(ListNode head)
    {
        // use 101 as dummy so it won't affect remove logic.
        var dummyUniq = new ListNode(101);
        var dummyDup = new ListNode(101);
        var pU = dummyUniq;
        var pD = dummyDup;
        var p = head;

        while (p != null)
        {
            if (( p.next != null && p.val == p.next.val ) || p.val == pD.val)
            {
                // 发现重复节点，接到重复链表后面。因为链表是升序的，所以采取以下逻辑：
                // p.next != null && p.val == p.next.val：当前节点和下一个节点值相同，说明当前节点是重复节点。
                // p.val == pD.val：当前节点的值和已记录的重复节点值相同，说明它也是重复节点。
                pD.next = p;
                pD = pD.next;
            }
            else
            {
                // 不是重复节点，接到不重复链表后面
                pU.next = p;
                pU = pU.next;
            }

            p = p.next;
            // 将原链表和新链表断开，避免形成环
            pU.next = null;
            pD.next = null;
        }

        return dummyUniq.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
