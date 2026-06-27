namespace Scratch.Labuladong.Algorithms.NextGreaterNodeInLinkedList;

// 1019. Next Greater Node In Linked List (Medium)
//
// You are given the head of a linked list with n nodes.
//
// For each node in the list, find the value of the next greater node. That is, for each node, find
// the value of the first node that is next to it and has a strictly larger value than it.
//
// Return an integer array answer where answer[i] is the value of the next greater node of the i^th
// node (1-indexed). If the i^th node does not have a next greater node, set answer[i] = 0.
//
// Example 1:
//
// Input: head = [2,1,5]
// Output: [5,5,0]
//
// Example 2:
//
// Input: head = [2,7,4,3,5]
// Output: [7,0,5,5,0]
//
// Constraints:
//
// - The number of nodes in the list is n.
//
// - 1 <= n <= 10^4
//
// - 1 <= Node.val <= 10^9
//
// Related Topics: Array, Linked List, Stack, Monotonic Stack

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
    public int[] NextLargerNodes(ListNode head)
    {
        var nums = new List<int>();
        var p = head;
        while (p != null)
        {
            nums.Add(p.val);
            p = p.next;
        }

        var res = new int[nums.Count];
        var stk = new Stack<int>();
        for (int i = nums.Count - 1; i >= 0; i--)
        {
            while (stk.Count != 0 && stk.Peek() <= nums[i])
            {
                stk.Pop();
            }

            res[i] = stk.Count == 0 ? 0 : stk.Peek();
            stk.Push(nums[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
