using System.Text;

namespace Scratch.Labuladong.Algorithms.SerializeAndDeserializeBinaryTree;

// 297. Serialize and Deserialize Binary Tree (Hard)
//
// Serialization is the process of converting a data structure or object into a sequence of bits so
// that it can be stored in a file or memory buffer, or transmitted across a network connection
// link to be reconstructed later in the same or another computer environment.
//
// Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how
// your serialization/deserialization algorithm should work. You just need to ensure that a binary
// tree can be serialized to a string and this string can be deserialized to the original tree
// structure.
//
// Clarification: The input/output format is the same as how LeetCode serializes a binary tree. You
// do not necessarily need to follow this format, so please be creative and come up with different
// approaches yourself.
//
// Example 1:
//
// Input: root = [1,2,3,null,null,4,5]
// Output: [1,2,3,null,null,4,5]
//
// Example 2:
//
// Input: root = []
// Output: []
//
// Constraints:
//
// - The number of nodes in the tree is in the range [0, 10^4].
//
// - -1000 <= Node.val <= 1000
//
// Related Topics: String, Tree, Depth-First Search, Breadth-First Search, Design, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Codec
{
    // 代表分隔符的字符
    string SEP = ",";

    // 代表 null 空指针的字符
    string NULL = "#";

    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
        var sb = new StringBuilder();
        _serializePreorder(root, sb);

        return sb.ToString();
    }

    // Decodes your encoded data to tree.
    public TreeNode? deserialize(string data)
    {
        return _deserializePreorder(data.Split(SEP).ToList());
    }

    private void _serializePreorder(TreeNode? root, StringBuilder sb)
    {
        if (root == null)
        {
            sb.Append(NULL).Append(SEP);
            return;
        }

        // 前序位置
        sb.Append(root.val).Append(SEP);

        _serializePreorder(root.left, sb);
        _serializePreorder(root.right, sb);
    }

    private TreeNode? _deserializePreorder(List<string> nodes)
    {
        if (nodes.Count == 0) return null;

        // 前序位置
        var first = nodes[0];
        nodes.RemoveAt(0);

        if (first == NULL) return null;
        var root = new TreeNode(int.Parse(first));

        root.left = _deserializePreorder(nodes);
        root.right = _deserializePreorder(nodes);

        return root;
    }

    private void _serializePostorder(TreeNode? root, StringBuilder sb)
    {
        if (root == null)
        {
            sb.Append(NULL).Append(SEP);
            return;
        }

        _serializePostorder(root.left, sb);
        _serializePostorder(root.right, sb);

        // 后序位置
        sb.Append(root.val).Append(SEP);
    }

    private TreeNode? _deserializePostorder(List<string> nodes)
    {
        if (nodes.Count == 0) return null;

        var last = nodes[^1];
        nodes.RemoveAt(nodes.Count - 1);

        if (last == NULL) return null;
        var root = new TreeNode(int.Parse(last));

        // 先构造右子树，后构造左子树
        root.right = _deserializePostorder(nodes);
        root.left = _deserializePostorder(nodes);

        return root;
    }

    private void _serializeLevel(TreeNode root, StringBuilder sb)
    {
        if (root == null) return;
        var q = new Queue<TreeNode?>();
        q.Enqueue(root);

        while (q.Count != 0)
        {
            var _size = q.Count;
            for (var i = 0; i < _size; i++)
            {
                var cur = q.Dequeue();
                if (cur == null)
                {
                    sb.Append(NULL).Append(SEP);
                    continue;
                }

                sb.Append(cur.val).Append(SEP);
                q.Enqueue(cur.left);
                q.Enqueue(cur.right);
            }
        }
    }

    private TreeNode? _deserializeLevel(List<string> nodes)
    {
        if (nodes.Count == 0) return null;

        // 第一个元素就是 root 的值
        var root = new TreeNode(int.Parse(nodes[0]));
        // 队列 q 记录父节点，将 root 加入队列
        var q = new Queue<TreeNode>();
        q.Enqueue(root);

        // index 变量记录正在序列化的节点在数组中的位置
        var index = 1;
        while (q.Count != 0)
        {
            var _size = q.Count;
            for (int i = 0; i < _size; i++)
            {
                var parent = q.Dequeue();
                // 为父节点构造左侧子节点
                var left = nodes[index++];
                if (left != NULL)
                {
                    parent.left = new TreeNode(int.Parse(left));
                    q.Enqueue(parent.left);
                }

                // 为父节点构造右侧子节点
                var right = nodes[index++];
                if (right != NULL)
                {
                    parent.right = new TreeNode(int.Parse(right));
                    q.Enqueue(parent.right);
                }
            }
        }

        return root;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec ser = new Codec();
// Codec deser = new Codec();
// TreeNode ans = deser.deserialize(ser.serialize(root));
//leetcode submit region end(Prohibit modification and deletion)
