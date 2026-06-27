namespace Scratch.Labuladong.Algorithms.BinaryTreePaths;

// 257. Binary Tree Paths (Easy)
//
// Given the root of a binary tree, return all root-to-leaf paths in any order.
//
// A leaf is a node with no children.
//
// Example 1:
//
// Input: root = [1,2,3,null,5]
// Output: ["1->2->5","1->3"]
//
// Example 2:
//
// Input: root = [1]
// Output: ["1"]
//
// Constraints:
//
// - The number of nodes in the tree is in the range [1, 100].
//
// - -100 <= Node.val <= 100
//
// Related Topics: String, Backtracking, Tree, Depth-First Search, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    // 记录 traverse 函数递归时的路径
    List<string> path = new();

    // 记录所有从根节点到叶子节点的路径
    List<string> res = new();

    public IList<string> BinaryTreePaths(TreeNode root)
    {
        Traverse(root);

        return res;
    }

    private void Traverse(TreeNode? root)
    {
        if (root == null) return;

        // root 是叶子节点
        if (root.left == null && root.right == null)
        {
            path.Add(root.val.ToString());
            res.Add(string.Join("->", path));
            path.RemoveAt(path.Count - 1);
        }

        // 前序
        path.Add(root.val.ToString());
        Traverse(root.left);
        Traverse(root.right);
        // 后序
        path.RemoveAt(path.Count - 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
