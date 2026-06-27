namespace Scratch.Labuladong.Algorithms.NQueens;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<IList<string>> res = new List<IList<string>>();

    public IList<IList<string>> SolveNQueens(int n)
    {
        // '.' 表示空，'Q' 表示皇后，初始化空棋盘。
        List<string> board = new List<string>();
        for (int i = 0; i < n; i++)
        {
            board.Add(new string('.', n));
        }

        _backtrack(board, 0);
        return res;
    }

    // 路径：board 中小于 row 的那些行都已经成功放置了皇后
    // 选择列表：第 row 行的所有列都是放置皇后的选择
    // 结束条件：row 超过 board 的最后一行
    private void _backtrack(List<string> board, int row)
    {
        // tip:
        // 对于 N 皇后问题，我们知道每行必然有且只有一个皇后，
        // 所以如果我们决定在 board[i] 这一行的某一列放置皇后，那么接下来就不用管 board[i] 这一行了，
        // 应该考虑 board[i+1] 这一行的皇后要放在哪里。

        if (row == board.Count) // 最后一行放完了
        {
            res.Add(new List<string>(board));
            return;
        }

        var n = board[row].Length;

        for (int col = 0; col < n; col++)
        {
            // 排除不合法选择
            if (!_canQ(board, row, col))
                continue;

            // 做选择
            var newRow = board[row].ToCharArray();
            newRow[col] = 'Q';
            board[row] = new string(newRow);

            // 进入下一行决策
            _backtrack(board, row + 1);

            // 撤销选择
            newRow[col] = '.';
            board[row] = new string(newRow);
        }
    }

    // 是否可以在 board[row][col] 放置皇后？
    private bool _canQ(List<string> board, int row, int col)
    {
        var n = board.Count;

        // 因为从上到下放置Q
        // 不需要检查下方的行

        // 检查列是否有皇后互相冲突
        // 因为皇后可以攻击同一列
        for (int i = 0; i < row; i++)
        {
            if (board[i][col] == 'Q') return false;
        }

        // 检查右上方是否有皇后互相冲突
        // 因为皇后可以攻击左上对角线
        for (int i = row - 1, j = col + 1; i >= 0 && j < n; i--, j++)
        {
            if (board[i][j] == 'Q') return false;
        }

        // 检查左上方是否有皇后互相冲突
        // 因为皇后可以攻击右上对角线
        for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (board[i][j] == 'Q') return false;
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
