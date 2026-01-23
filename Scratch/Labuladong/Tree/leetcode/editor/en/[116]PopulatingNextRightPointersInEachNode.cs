namespace Scratch.Labuladong.Algorithms.PopulatingNextRightPointersInEachNode;

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

        Traverse(root.left, root.right);

        return root;
    }

    private void Traverse(Node? node1, Node? node2)
    {
        if (node1 == null || node2 == null) return;

        // 前序位置
        // 将传入的两个节点穿起来
        node1.next = node2;

        // 连接相同父节点的两个子节点
        Traverse(node1.left, node1.right);
        Traverse(node2.left, node2.right);
        // 连接跨越父节点的两个子节点
        Traverse(node1.right, node2.left);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
