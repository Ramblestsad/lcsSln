namespace Scratch.Labuladong.Algorithms.SudokuSolver;

// 37. Sudoku Solver (Hard)
//
// Write a program to solve a Sudoku puzzle by filling the empty cells.
//
// A sudoku solution must satisfy all of the following rules:
//
// - Each of the digits 1-9 must occur exactly once in each row.
//
// - Each of the digits 1-9 must occur exactly once in each column.
//
// - Each of the digits 1-9 must occur exactly once in each of the 9 3x3 sub-boxes of the grid.
//
// The '.' character indicates empty cells.
//
// Example 1:
//
// Input: board =
// [["5","3",".",".","7",".",".",".","."],["6",".",".","1","9","5",".",".","."],[".","9","8",".",".",".",".","6","."],["8",".",".",".","6",".",".",".","3"],["4",".",".","8",".","3",".",".","1"],["7",".",".",".","2",".",".",".","6"],[".","6",".",".",".",".","2","8","."],[".",".",".","4","1","9",".",".","5"],[".",".",".",".","8",".",".","7","9"]]
// Output:
// [["5","3","4","6","7","8","9","1","2"],["6","7","2","1","9","5","3","4","8"],["1","9","8","3","4","2","5","6","7"],["8","5","9","7","6","1","4","2","3"],["4","2","6","8","5","3","7","9","1"],["7","1","3","9","2","4","8","5","6"],["9","6","1","5","3","7","2","8","4"],["2","8","7","4","1","9","6","3","5"],["3","4","5","2","8","6","1","7","9"]]
// Explanation: The input board is shown above and the only valid solution is shown below:
//
// Constraints:
//
// - board.length == 9
//
// - board[i].length == 9
//
// - board[i][j] is a digit or '.'.
//
// - It is guaranteed that the input board has only one solution.
//
// Related Topics: Array, Hash Table, Backtracking, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
