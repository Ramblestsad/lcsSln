namespace Scratch.Labuladong.Algorithms.MergeKLists;

// 23. Merge k Sorted Lists (Hard)
//
// You are given an array of k linked-lists lists, each linked-list is sorted in ascending order.
//
// Merge all the linked-lists into one sorted linked-list and return it.
//
// Example 1:
//
// Input: lists = [[1,4,5],[1,3,4],[2,6]]
// Output: [1,1,2,3,4,4,5,6]
// Explanation: The linked-lists are:
// [
// 1->4->5,
// 1->3->4,
// 2->6
// ]
// merging them into one sorted linked list:
// 1->1->2->3->4->4->5->6
//
// Example 2:
//
// Input: lists = []
// Output: []
//
// Example 3:
//
// Input: lists = [[]]
// Output: []
//
// Constraints:
//
// - k == lists.length
//
// - 0 <= k <= 10^4
//
// - 0 <= lists[i].length <= 500
//
// - -10^4 <= lists[i][j] <= 10^4
//
// - lists[i] is sorted in ascending order.
//
// - The sum of lists[i].length will not exceed 10^4.
//
// Related Topics: Linked List, Divide and Conquer, Heap (Priority Queue), Merge Sort

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
    public ListNode? MergeKLists(ListNode[] lists)
    {
        if (lists.Length == 0)
        {
            return null!;
        }

        var dummy = new ListNode();
        var p = dummy;
        // construct minHeap
        var pq = new PriorityQueue<ListNode, int>();
        // push heads of k lists to pq
        foreach (var node in lists)
        {
            pq.Enqueue(node, node.val);
        }

        while (pq.Count > 0)
        {
            var minNode = pq.Dequeue();
            p.next = minNode;
            if (minNode.next != null) pq.Enqueue(minNode.next, minNode.next.val);
            p = p.next;
        }

        return dummy.next;
    }

    /* time complexity:
     * The most number elements in pq is k, time complexity for every Enqueue or Dequeue is O(logk)
     * Every nodes in k lists will be enqueued and dequeued, so O(Nlogk)
     */
}
//leetcode submit region end(Prohibit modification and deletion)
