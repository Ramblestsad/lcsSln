namespace Scratch.Labuladong.Algorithms.DetectCycle;

// 142. Linked List Cycle II (Medium)
//
// Given the head of a linked list, return the node where the cycle begins. If there is no cycle,
// return null.
//
// There is a cycle in a linked list if there is some node in the list that can be reached again by
// continuously following the next pointer. Internally, pos is used to denote the index of the node
// that tail's next pointer is connected to (0-indexed). It is -1 if there is no cycle. Note that
// pos is not passed as a parameter.
//
// Do not modify the linked list.
//
// Example 1:
//
// Input: head = [3,2,0,-4], pos = 1
// Output: tail connects to node index 1
// Explanation: There is a cycle in the linked list, where tail connects to the second node.
//
// Example 2:
//
// Input: head = [1,2], pos = 0
// Output: tail connects to node index 0
// Explanation: There is a cycle in the linked list, where tail connects to the first node.
//
// Example 3:
//
// Input: head = [1], pos = -1
// Output: no cycle
// Explanation: There is no cycle in the linked list.
//
// Constraints:
//
// - The number of the nodes in the list is in the range [0, 10^4].
//
// - -10^5 <= Node.val <= 10^5
//
// - pos is -1 or a valid index in the linked-list.
//
// Follow up: Can you solve it using O(1) (i.e. constant) memory?
//
// Related Topics: Hash Table, Linked List, Two Pointers

//leetcode submit region begin(Prohibit modification and deletion)
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
    public ListNode? DetectCycle(ListNode head)
    {
        var slow = head;
        var fast = head;

        // 先找到相遇点
        while (fast != null && fast.next != null)
        {
            slow = slow?.next;
            fast = fast.next.next;
            if (fast == slow) break;
        }
        // slow 走k步，fast一定走2k步
        // 差值 k 即为 fast在环里绕的步数
        // 设 m 为 环起始点到相遇点的距离
        // 那么从head到环起点的距离为：k-m,因为slow走到相遇点为k
        // 从相遇点到环起点的距离同样为：k-m, 因为fast在环里绕k步到相遇点，那么减掉m即为环起点

        // fast 为空或这到达list尽头说明无环
        if (fast == null || fast.next == null) return null;

        slow = head;
        // now, slow and fast move at the same pace
        // 因为从头节点和从相遇点再走相同的步数，一定是环的起点。
        while (slow != fast)
        {
            fast = fast!.next;
            slow = slow?.next;
        }

        return slow;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
