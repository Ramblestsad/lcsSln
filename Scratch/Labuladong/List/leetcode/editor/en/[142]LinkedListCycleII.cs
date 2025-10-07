namespace Scratch.Labuladong.Algorithms.DetectCycle;

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
public class Solution {
    public ListNode? DetectCycle(ListNode head) {
        var slow = head;
        var fast = head;

        // 先找到相遇点
        while (fast != null && fast.next != null) {
            slow = slow?.next;
            fast = fast.next.next;
            if (fast == slow) break;
        }
        // slow 走k布，fast一定走2k步
        // 差值 k 即为 fast在环里绕的步数
        // 设 m 为 环起始点到相遇点的距离
        // 那么从head到环起点的距离为：k-m,因为slow走到相遇点为k
        // 从相遇点到环起点的距离同样为：k-m, 因为fast在环里绕k步到相遇点，那么减掉m即为环起点

        // fast 为空或这到达list尽头说明无环
        if (fast == null || fast?.next == null) return null;

        slow = head;
        // now, slow and fast move at the same pace
        // 因为从头节点和从相遇点再走相同的步数，一定是环的起点。
        while (slow != fast) {
            fast = fast!.next;
            slow = slow?.next;
        }

        return slow;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
