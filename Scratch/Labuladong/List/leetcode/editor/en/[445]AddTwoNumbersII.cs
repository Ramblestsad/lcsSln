namespace Scratch.Labuladong.Algorithms.AddTwoNumbersII;

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
    public ListNode? AddTwoNumbers(ListNode? l1, ListNode? l2)
    {
        var stk1 = new Stack<int>();
        while (l1 != null)
        {
            stk1.Push(l1.val);
            l1 = l1.next;
        }

        var stk2 = new Stack<int>();
        while (l2 != null)
        {
            stk2.Push(l2.val);
            l2 = l2.next;
        }

        var dummy = new ListNode();
        var carry = 0;

        while (stk1.Count > 0 || stk2.Count > 0 || carry > 0)
        {
            var val = carry;
            if (stk1.Count > 0) val += stk1.Pop();
            if (stk2.Count > 0) val += stk2.Pop();

            carry = val / 10;
            val = val % 10;
            // 头插法构建结果链表:新节点插到头部
            var newNode = new ListNode(val);
            newNode.next = dummy.next;
            dummy.next = newNode;
        }

        return dummy.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
