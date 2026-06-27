using System.Text;

namespace Scratch.Labuladong.Algorithms.SlidingPuzzle;

// 773. Sliding Puzzle (Hard)
//
// On an 2 x 3 board, there are five tiles labeled from 1 to 5, and an empty square represented by
// 0. A move consists of choosing 0 and a 4-directionally adjacent number and swapping it.
//
// The state of the board is solved if and only if the board is [[1,2,3],[4,5,0]].
//
// Given the puzzle board board, return the least number of moves required so that the state of the
// board is solved. If it is impossible for the state of the board to be solved, return -1.
//
// Example 1:
//
// Input: board = [[1,2,3],[4,0,5]]
// Output: 1
// Explanation: Swap the 0 and the 5 in one move.
//
// Example 2:
//
// Input: board = [[1,2,3],[5,4,0]]
// Output: -1
// Explanation: No number of moves will make the board solved.
//
// Example 3:
//
// Input: board = [[4,1,2],[5,0,3]]
// Output: 5
// Explanation: 5 is the smallest number of moves that solves the board.
// An example path:
// After move 0: [[4,1,2],[5,0,3]]
// After move 1: [[4,1,2],[0,5,3]]
// After move 2: [[0,1,2],[4,5,3]]
// After move 3: [[1,0,2],[4,5,3]]
// After move 4: [[1,2,0],[4,5,3]]
// After move 5: [[1,2,3],[4,5,0]]
//
// Constraints:
//
// - board.length == 2
//
// - board[i].length == 3
//
// - 0 <= board[i][j] <= 5
//
// - Each value board[i][j] is unique.
//
// Related Topics: Array, Dynamic Programming, Backtracking, Breadth-First Search, Memoization, Matrix

//leetcode submit region begin(Prohibit modification and deletion)

public class Solution
{
    public int SlidingPuzzle(int[][] board)
    {
        var m = 2;
        var n = 3;
        var sb = new StringBuilder();
        var target = "123450";

        // 将 2x3 的数组转化成字符串作为 BFS 的起点
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sb.Append(board[i][j]);
            }
        }

        var start = sb.ToString();
        // 记录一维字符串的相邻索引
        var neighbor = new int[][] { [1, 3], [0, 4, 2], [1, 5], [0, 4], [3, 1, 5], [4, 2] };

        var q = new Queue<string>();
        var visited = new HashSet<string>();
        q.Enqueue(start);
        visited.Add(start);

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++) // each layer
            {
                var cur = q.Dequeue();
                if (target == cur) return step;
                // 找到数字 0 的索引
                var zIdx = 0;
                for (; cur[zIdx] != '0'; zIdx++) ;
                // 将数字 0 和相邻的数字交换位置
                foreach (var adj in neighbor[zIdx])
                {
                    var chars = cur.ToCharArray();
                    ( chars[adj], chars[zIdx] ) = ( chars[zIdx], chars[adj] );
                    var newBoard = new string(chars);
                    // 防止走回头路
                    if (!visited.Contains(newBoard))
                    {
                        q.Enqueue(newBoard);
                        visited.Add(newBoard);
                    }
                }
            }

            step++;
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
