namespace Scratch.Labuladong.Algorithms.DeleteDuplicates;

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
    public ListNode? DeleteDuplicates(ListNode head)
    {
        // use 101 as dummy so it won't affect remove logic.
        var dummyUniq = new ListNode(101);
        var dummyDup = new ListNode(101);
        var pU = dummyUniq;
        var pD = dummyDup;
        var p = head;

        while (p != null)
        {
            if (( p.next != null && p.val == p.next.val ) || p.val == pD.val)
            {
                // 发现重复节点，接到重复链表后面。因为链表是升序的，所以采取以下逻辑：
                // p.next != null && p.val == p.next.val：当前节点和下一个节点值相同，说明当前节点是重复节点。
                // p.val == pD.val：当前节点的值和已记录的重复节点值相同，说明它也是重复节点。
                pD.next = p;
                pD = pD.next;
            }
            else
            {
                // 不是重复节点，接到不重复链表后面
                pU.next = p;
                pU = pU.next;
            }

            p = p.next;
            // 将原链表和新链表断开，避免形成环
            pU.next = null;
            pD.next = null;
        }

        return dummyUniq.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
