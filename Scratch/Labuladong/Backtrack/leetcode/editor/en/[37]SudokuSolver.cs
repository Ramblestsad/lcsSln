/*
 * @lc app=leetcode id=37 lang=csharp
 * @lcpr version=30402
 *
 * [37] Sudoku Solver
 */

namespace Scratch.Labuladong.Algorithms.SudokuSolver;

// @lc code=start
public class Solution
{
    // 标记是否已经找到可行解
    private bool found = false;

    public void SolveSudoku(char[][] board)
    {
        _backtrack(board, 0);
    }

    // 路径：board 中小于 index 的位置所填的数字
    // 选择列表：数字 1~9
    // 结束条件：整个 board 都填满数字
    private void _backtrack(char[][] board, int index)
    {
        if (found) return;

        var m = 9;
        var n = 9;
        // 编码：二维坐标 (i, j) → 一维索引 index = i * N + j
        // 解码：一维索引 index → 二维坐标 i = index / N, j = index % N
        var i = index / n;
        var j = index % n;
        if (index == m * n)
        {
            // 格子填完了，找到一个可行解，触发 base case
            found = true;
            return;
        }

        if (board[i][j] != '.')
        {
            // 默认填了数字的情况
            _backtrack(board, index + 1);
            return;
        }

        for (var ch = '1'; ch <= '9'; ch++)
        {
            // 剪枝：如果遇到不合法的数字，就跳过
            if (!_isValid(board, i, j, ch)) continue;

            // 做选择
            board[i][j] = ch;

            _backtrack(board, index + 1);
            // 如果找到一个可行解，立即结束
            // 不要撤销选择，否则 board[i][j] 会被重置为 '.'
            if (found) return;

            // 撤销选择
            board[i][j] = '.';
        }
    }

    // 判断是否可以在 (i, j) 位置放置数字 num
    private bool _isValid(char[][] board, int r, int c, char num)
    {
        for (int i = 0; i < 9; i++)
        {
            // 判断行是否存在重复
            if (board[r][i] == num) return false;
            // 判断列是否存在重复
            if (board[i][c] == num) return false;
            // 判断 3 x 3 方框是否存在重复
            // (r/3)*3 -> [r, c]所在格子的九宫格的起始行
            // i/3 -> 行偏移
            // (c/3)*3 -> [r, c]所在格子的九宫格的起始列
            // i%3 -> 列偏移
            if (board[( r / 3 ) * 3 + i / 3][( c / 3 ) * 3 + i % 3] == num)
                return false;
        }

        return true;
    }
}
// @lc code=end
