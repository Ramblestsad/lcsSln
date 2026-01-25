using System.Text;

namespace Scratch.Labuladong.Algorithms.SerializeAndDeserializeBinaryTree;

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
        TraversePreorder(root, sb);

        return sb.ToString();
    }

    // Decodes your encoded data to tree.
    public TreeNode? deserialize(string data)
    {
        return _deserializePreorder(data.Split(SEP).ToList());
    }

    private void TraversePreorder(TreeNode? root, StringBuilder sb)
    {
        if (root == null)
        {
            sb.Append(NULL).Append(SEP);
            return;
        }

        // 前序位置
        sb.Append(root.val).Append(SEP);

        TraversePreorder(root.left, sb);
        TraversePreorder(root.right, sb);
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
}

// Your Codec object will be instantiated and called as such:
// Codec ser = new Codec();
// Codec deser = new Codec();
// TreeNode ans = deser.deserialize(ser.serialize(root));
//leetcode submit region end(Prohibit modification and deletion)
