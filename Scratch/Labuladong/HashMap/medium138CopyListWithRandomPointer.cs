namespace Scratch.Labuladong.Algorithms.CopyListRandom;

public class Node
{
    public int val;
    public Node? next;
    public Node? random;

    public Node(int _val)
    {
        val = _val;
        next = null;
        random = null;
    }
}

//leetcode submit region begin(Prohibit modification and deletion)
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node next;
    public Node random;

    public Node(int _val) {
        val = _val;
        next = null;
        random = null;
    }
}
*/

public class Solution
{
    // 一个hashmap，两次遍历
    public Node? CopyRandomList(Node? head)
    {
        if (head == null) return null;

        var originToClone = new Dictionary<Node, Node>();

        // 第一次遍历：先把所有节点clone出来
        for (Node? p = head; p != null; p = p.next)
        {
            originToClone[p] = new Node(p.val);
        }

        // 第二次遍历：把克隆节点的结构连接好
        for (Node? p = head; p != null; p = p.next)
        {
            if (p.next != null)
            {
                originToClone.TryGetValue(p, out var cloneNode);
                originToClone.TryGetValue(p.next, out var cloneNodeNext);
                cloneNode!.next = cloneNodeNext;
            }

            if (p.random != null)
            {
                var clone = originToClone[p];
                clone.next = p.next == null ? null : originToClone[p.next];
                clone.random = p.random == null ? null : originToClone[p.random];
            }
        }

        return originToClone[head];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
