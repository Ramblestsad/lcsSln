namespace Scratch.Labuladong.Algorithms.WordSearch;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private bool found = false;

    public bool Exist(char[][] board, string word)
    {
        var m = board.Length;
        var n = board[0].Length;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _dfs(board, i, j, word, 0);
                if (found) return true;
            }
        }

        return false;
    }

    // 从 (i, j) 开始向四周搜索，试图匹配 word[p..]
    private void _dfs(char[][] board, int i, int j, string word, int p)
    {
        if (p == word.Length)
        {
            // 整个 word 已经被匹配完，找到了一个答案
            found = true;
            return;
        }

        if (found) return;

        var m = board.Length;
        var n = board[0].Length;
        if (i < 0 || j < 0 || i >= m || j >= n) return;
        if (board[i][j] != word[p]) return;
        // 添一个负号作为标记，避免走回头路
        board[i][j] = (char)( -board[i][j] );
        // word[p] 被 board[i][j] 匹配，开始向四周搜索 word[p+1..]
        _dfs(board, i + 1, j, word, p + 1);
        _dfs(board, i, j + 1, word, p + 1);
        _dfs(board, i - 1, j, word, p + 1);
        _dfs(board, i, j - 1, word, p + 1);
        board[i][j] = (char)( -board[i][j] );
    }
}
//leetcode submit region end(Prohibit modification and deletion)
