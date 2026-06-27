namespace Scratch.Labuladong.Algorithms.UniqueBinarySearchTrees;

// 96. Unique Binary Search Trees (Medium)
//
// Given an integer n, return the number of structurally unique BST's (binary search trees) which
// has exactly n nodes of unique values from 1 to n.
//
// Example 1:
//
// Input: n = 3
// Output: 5
//
// Example 2:
//
// Input: n = 1
// Output: 1
//
// Constraints:
//
// - 1 <= n <= 19
//
// Related Topics: Math, Dynamic Programming, Tree, Binary Search Tree, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 备忘录
    private int[,] memo = null!;

    public int NumTrees(int n)
    {
        // 备忘录的值初始化为 0
        memo = new int[n + 1, n + 1];
        return _count(1, n);
    }

    // 计算闭区间 [lo, hi] 组成的 BST 个数
    private int _count(int lo, int hi)
    {
        // base case
        // 显然当 lo > hi 闭区间 [lo, hi] 肯定是个空区间，也就对应着空节点 null
        // 虽然是空节点，但是也是一种情况，所以要返回 1 而不能返回 0
        if (lo >= hi) return 1;

        if (memo[lo, hi] != 0)
        {
            return memo[lo, hi];
        }

        var res = 0;
        for (int mid = lo; mid <= hi; mid++)
        {
            // i 的值作为根节点 root
            var left = _count(lo, mid - 1);
            var right = _count(mid + 1, hi);
            // 左右子树的组合数乘积是 BST 的总数
            res += left * right;
        }

        memo[lo, hi] = res;

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
