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

// 117. Populating Next Right Pointers in Each Node II (Medium)
//
// Given a binary tree
//
// struct Node {
// int val;
// Node *left;
// Node *right;
// Node *next;
// }
//
// Populate each next pointer to point to its next right node. If there is no next right node, the
// next pointer should be set to NULL.
//
// Initially, all next pointers are set to NULL.
//
// Example 1:
//
// Input: root = [1,2,3,4,5,null,7]
// Output: [1,#,2,3,#,4,5,7,#]
// Explanation: Given the above binary tree (Figure A), your function should populate each next
// pointer to point to its next right node, just like in Figure B. The serialized output is in
// level order as connected by the next pointers, with '#' signifying the end of each level.
//
// Example 2:
//
// Input: root = []
// Output: []
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 6000].
//
// - -100 <= Node.val <= 100
//
// Follow-up:
//
// - You may only use constant extra space.
//
// - The recursive approach is fine. You may assume implicit stack space does not count as extra
// space for this problem.
//
// Related Topics: Linked List, Tree, Depth-First Search, Breadth-First Search, Binary Tree

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
