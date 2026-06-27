namespace Scratch.Labuladong.Algorithms.ReorderList;

// 143. Reorder List (Medium)
//
// You are given the head of a singly linked-list. The list can be represented as:
//
// L_0 → L_1 → … → L_n - 1 → L_n
//
// Reorder the list to be on the following form:
//
// L_0 → L_n → L_1 → L_n - 1 → L_2 → L_n - 2 → …
//
// You may not modify the values in the list's nodes. Only nodes themselves may be changed.
//
// Example 1:
//
// Input: head = [1,2,3,4]
// Output: [1,4,2,3]
//
// Example 2:
//
// Input: head = [1,2,3,4,5]
// Output: [1,5,2,4,3]
//
// Constraints:
//
// - The number of nodes in the list is in the range [1, 5 * 10^4].
//
// - 1 <= Node.val <= 1000
//
// Related Topics: Linked List, Two Pointers, Stack, Recursion

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
    public void ReorderList(ListNode head)
    {
        var stk = new Stack<ListNode>();
        // 先把所有节点装进栈里，得到倒序结果
        var p = head;
        while (p != null)
        {
            stk.Push(p);
            p = p.next;
        }

        p = head; // p 重新指向头节点
        while (p != null)
        {
            // 下一个尾巴节点
            var lastNode = stk.Pop();
            var nextNode = p.next;

            // **结束条件，链表节点数为奇数或偶数时均适用**
            if (lastNode == nextNode || lastNode.next == nextNode)
            {
                lastNode.next = null;
                break;
            }

            p.next = lastNode;
            lastNode.next = nextNode;
            p = nextNode;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
