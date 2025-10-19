namespace Scratch.Labuladong.Algorithms.MergeKLists;

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
public class Solution {
    public ListNode? MergeKLists(ListNode[] lists) {
        if (lists.Length == 0) {
            return null!;
        }

        var dummy = new ListNode();
        var p = dummy;
        // construct minHeap
        var pq = new PriorityQueue<ListNode, int>();
        // push heads of k lists to pa
        foreach (var node in lists) {
            pq.Enqueue(node, node.val);
        }

        while (pq.Count > 0) {
            var minNode = pq.Dequeue();
            p.next = minNode;
            if (minNode.next != null) pq.Enqueue(minNode.next, minNode.next.val);
            p = p.next;
        }

        return dummy.next;
    }

    /* time complexity:
     * The most number elements in pa is k, time complexity for every Enqueue or Dequeue is O(logk)
     * Every nodes in k lists will be enqueued and dequeued, so O(Nlogk)
     */
}
//leetcode submit region end(Prohibit modification and deletion)
