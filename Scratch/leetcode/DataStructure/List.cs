namespace Scratch.leetcode.DataStructure;

public class ListNode(int val = 0, ListNode? next = null)
{
    public int val = val;
    public ListNode? next = next ?? new ListNode();
}
