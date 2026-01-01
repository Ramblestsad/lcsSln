namespace Scratch.Labuladong.Algorithms.ReorderList;

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
