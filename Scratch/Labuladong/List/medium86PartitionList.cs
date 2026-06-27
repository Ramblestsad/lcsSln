namespace Scratch.Labuladong.Algorithms.Partition;

// 86. Partition List (Medium)
//
// Given the head of a linked list and a value x, partition it such that all nodes less than x come
// before nodes greater than or equal to x.
//
// You should preserve the original relative order of the nodes in each of the two partitions.
//
// Example 1:
//
// Input: head = [1,4,3,2,5,2], x = 3
// Output: [1,2,2,4,3,5]
//
// Example 2:
//
// Input: head = [2,1], x = 2
// Output: [1,2]
//
// Constraints:
//
// - The number of nodes in the list is in the range [0, 200].
//
// - -100 <= Node.val <= 100
//
// - -200 <= x <= 200
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
    public ListNode? Partition(ListNode? head, int x)
    {
        var less = new ListNode();
        var greater = new ListNode();
        var p1 = less;
        var p2 = greater;
        var p = head;

        while (p != null)
        {
            if (p.val < x)
            {
                p1.next = p;
                p1 = p1.next;
            }
            else
            {
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
//leetcode submit region end(Prohibit modification and deletion)
