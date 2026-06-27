namespace Scratch.Labuladong.Algorithms.PopulatingNextRightPointersInEachNodeII;

public class Node
{
    public int val;
    public Node? left;
    public Node? right;
    public Node? next;

    public Node()
    {
    }

    public Node(int _val)
    {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public Node? Connect(Node? root)
    {
        if (root == null) return null;

        var q = new Queue<Node>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            Node? pre = null;

            for (int i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                // 链接当前层所有节点的 next 指针
                // 记录每一层从开始到结尾的节点为pre，然后连到当前cur节点
                if (pre != null) pre.next = cur;
                pre = cur;

                if (cur.left != null) q.Enqueue(cur.left);
                if (cur.right != null) q.Enqueue(cur.right);
            }
        }

        return root;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
