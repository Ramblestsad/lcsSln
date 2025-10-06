namespace Scratch.Labuladong.Algorithms;

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public partial class Solution {
    public ListNode? GetIntersectionNode(ListNode headA, ListNode headB) {
        int lenA = 0, lenB = 0;
        // 首先计算两条链表长度
        for (var p1 = headA; p1 != null; p1 = p1.next) {
            lenA++;
        }

        for (var p2 = headB; p2 != null; p2 = p2.next) {
            lenB++;
        }

        // 抹平长度差值：让更长的链表先走差值步数
        // 这样两条链表在之后的循环可以同时结束
        var pa = headA;
        var pb = headB;
        if (lenA > lenB) {
            for (var i = 0; i < lenA - lenB; i++) {
                pa = pa?.next;
            }
        } else {
            for (var i = 0; i < lenB - lenA; i++) {
                pb = pb?.next;
            }
        }

        // 看两个指针是否会相同，p1 == p2 时有两种情况：
        // 1、要么是两条链表不相交，他俩同时走到尾部空指针
        // 2、要么是两条链表相交，他俩走到两条链表的相交点
        while (pa != pb) {
            pa = pa?.next;
            pb = pb?.next;
        }

        return pa;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
