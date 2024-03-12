using Algorithm.DataStructure;

namespace Leetcode.ListAlgorithm;
//leetcode submit region begin(Prohibit modification and deletion)
public partial class Solution
{
    public ListNode? MergeTwoLists(ListNode list1, ListNode list2)
    {
        var dummy = new ListNode(-1);
        var p = dummy;
        ListNode? p1 = list1, p2 = list2;

        while (p1 != null && p2 != null)
        {
            // 比较 p1 和 p2 两个指针
            // 将值较小的的节点接到 p 指针
            if (p1.Val > p2.Val)
            {
                p.Next = p2;
                p2 = p2.Next;
            }
            else
            {
                p.Next = p1;
                p1 = p1.Next;
            }

            // p 指针不断前进
            p = p.Next;
        }

        if (p1 != null) p.Next = p1;
        if (p2 != null) p.Next = p2;

        return dummy.Next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
