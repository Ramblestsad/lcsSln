namespace Scratch.Labuladong.Algorithms.Shift2DGrid;

// 1260. Shift 2D Grid (Easy)
//
// Given a 2D grid of size m x n and an integer k. You need to shift the grid k times.
//
// In one shift operation:
//
// - Element at grid[i][j] moves to grid[i][j + 1].
//
// - Element at grid[i][n - 1] moves to grid[i + 1][0].
//
// - Element at grid[m - 1][n - 1] moves to grid[0][0].
//
// Return the 2D grid after applying shift operation k times.
//
// Example 1:
//
// Input: grid = [[1,2,3],[4,5,6],[7,8,9]], k = 1
// Output: [[9,1,2],[3,4,5],[6,7,8]]
//
// Example 2:
//
// Input: grid = [[3,8,1,9],[19,7,2,5],[4,6,11,10],[12,0,21,13]], k = 4
// Output: [[12,0,21,13],[3,8,1,9],[19,7,2,5],[4,6,11,10]]
//
// Example 3:
//
// Input: grid = [[1,2,3],[4,5,6],[7,8,9]], k = 9
// Output: [[1,2,3],[4,5,6],[7,8,9]]
//
// Constraints:
//
// - m == grid.length
//
// - n == grid[i].length
//
// - 1 <= m <= 50
//
// - 1 <= n <= 50
//
// - -1000 <= grid[i][j] <= 1000
//
// - 0 <= k <= 100
//
// Related Topics: Array, Matrix, Simulation

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<IList<int>> ShiftGrid(int[][] grid, int k)
    {
        // 把二维 grid 抽象成一维数组
        int m = grid.Length, n = grid[0].Length;
        var mn = m * n;
        k = k % mn;
        // 先把最后 k 个数翻转
        Reverse(grid, mn - k, mn - 1);
        // 然后把前 mn - k 个数翻转
        Reverse(grid, 0, mn - k - 1);
        // 最后把整体翻转
        Reverse(grid, 0, mn - 1);

        var res = new List<IList<int>>();
        foreach (var row in grid)
        {
            var rowList = new List<int>();
            foreach (var num in row)
            {
                rowList.Add(num);
            }

            res.Add(rowList);
        }

        return res;
    }

    // 通过一维数组的索引访问二维数组的元素
    int Get(int[][] grid, int index)
    {
        var n = grid[0].Length;
        int i = index / n, j = index % n;
        return grid[i][j];
    }

    // 通过一维数组的索引修改二维数组的元素
    void Set(int[][] grid, int index, int val)
    {
        var n = grid[0].Length;
        int i = index / n, j = index % n;
        grid[i][j] = val;
    }

    void Reverse(int[][] grid, int i, int j)
    {
        while (i < j)
        {
            var temp = Get(grid, i);
            Set(grid, i, Get(grid, j));
            Set(grid, j, temp);
            i++;
            j--;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
