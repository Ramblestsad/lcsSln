namespace Scratch.Labuladong.Algorithms.PalindromeLinkedList;

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
    public bool IsPalindrome(ListNode head)
    {
        left = head;
        Traverse(head);

        return res;
    }

    // 从左向右移动的指针
    ListNode? left;

    // 记录链表是否为回文
    bool res = true;

    void Traverse(ListNode? right)
    {
        if (right == null) return;

        // 前序遍历位置

        Traverse(right.next);

        // 后续遍历位置
        // 此时的 right 指针指向链表右侧尾部
        // 所以可以和 left 指针比较，判断是否是回文链表
        if (left!.val != right.val) res = false;

        left = left.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
