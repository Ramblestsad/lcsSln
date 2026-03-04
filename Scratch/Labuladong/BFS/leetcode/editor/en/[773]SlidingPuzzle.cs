using System.Text;

namespace Scratch.Labuladong.Algorithms.SlidingPuzzle;

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
