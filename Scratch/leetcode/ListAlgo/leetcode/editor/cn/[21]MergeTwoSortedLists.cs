using Scratch.leetcode.DataStructure;

namespace Scratch.leetcode;
//leetcode submit region begin(Prohibit modification and deletion)
public partial class Solution
{
    public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
    {
        var preHead = new ListNode();
        var pre = preHead;
        while (list1 != null && list2 != null)
        {
            if (list1.val > list2.val)
            {
                pre.next = list2;
                list2 = list2.next;
            }
            else
            {
                pre.next = list1;
                list1 = list1.next;
            }

            pre = pre.next;
        }

        pre.next = list1 ?? list2!;
        return preHead.next;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
