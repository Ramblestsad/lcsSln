namespace Scratch.Labuladong.Algorithms.Shift2DGrid;

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
