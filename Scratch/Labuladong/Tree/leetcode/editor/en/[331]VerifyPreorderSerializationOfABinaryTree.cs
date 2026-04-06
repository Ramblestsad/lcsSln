/*
 * @lc app=leetcode id=331 lang=csharp
 * @lcpr version=30402
 *
 * [331] Verify Preorder Serialization Of A Binary Tree
 */

namespace Scratch.Labuladong.Algorithms.VerifyPreorderSerializationOfABinaryTree;

// @lc code=start
public class Solution
{
    public bool IsValidSerialization(string preorder)
    {
        if (preorder is null) return false;

        var nodes = preorder.Split(",").ToList();
        return _deserialize(nodes) && nodes.Count == 0;
    }

    private bool _deserialize(List<string> nodes)
    {
        if (nodes.Count == 0) return false;

        var first = nodes[0];
        nodes.RemoveAt(0);
        if (first == "#") return true;

        return _deserialize(nodes) && _deserialize(nodes);
    }
}
// @lc code=end
