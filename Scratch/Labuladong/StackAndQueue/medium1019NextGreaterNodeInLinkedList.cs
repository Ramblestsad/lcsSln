/*
 * @lc app=leetcode id=1019 lang=csharp
 * @lcpr version=30402
 *
 * [1019] Next Greater Node In Linked List
 */

namespace Scratch.Labuladong.Algorithms.NextGreaterNodeInLinkedList;

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
// @lc code=end
