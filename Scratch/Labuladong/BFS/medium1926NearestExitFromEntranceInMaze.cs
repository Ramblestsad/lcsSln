namespace Scratch.Labuladong.Algorithms.NearestExitFromEntranceInMaze;

// 1926. Nearest Exit from Entrance in Maze (Medium)
//
// You are given an m x n matrix maze (0-indexed) with empty cells (represented as '.') and walls
// (represented as '+'). You are also given the entrance of the maze, where entrance =
// [entrance_row, entrance_col] denotes the row and column of the cell you are initially standing
// at.
//
// In one step, you can move one cell up, down, left, or right. You cannot step into a cell with a
// wall, and you cannot step outside the maze. Your goal is to find the nearest exit from the
// entrance. An exit is defined as an empty cell that is at the border of the maze. The entrance
// does not count as an exit.
//
// Return the number of steps in the shortest path from the entrance to the nearest exit, or -1 if
// no such path exists.
//
// Example 1:
//
// Input: maze = [["+","+",".","+"],[".",".",".","+"],["+","+","+","."]], entrance = [1,2]
// Output: 1
// Explanation: There are 3 exits in this maze at [1,0], [0,2], and [2,3].
// Initially, you are at the entrance cell [1,2].
// - You can reach [1,0] by moving 2 steps left.
// - You can reach [0,2] by moving 1 step up.
// It is impossible to reach [2,3] from the entrance.
// Thus, the nearest exit is [0,2], which is 1 step away.
//
// Example 2:
//
// Input: maze = [["+","+","+"],[".",".","."],["+","+","+"]], entrance = [1,0]
// Output: 2
// Explanation: There is 1 exit in this maze at [1,2].
// [1,0] does not count as an exit since it is the entrance cell.
// Initially, you are at the entrance cell [1,0].
// - You can reach [1,2] by moving 2 steps right.
// Thus, the nearest exit is [1,2], which is 2 steps away.
//
// Example 3:
//
// Input: maze = [[".","+"]], entrance = [0,0]
// Output: -1
// Explanation: There are no exits in this maze.
//
// Constraints:
//
// - maze.length == m
//
// - maze[i].length == n
//
// - 1 <= m, n <= 100
//
// - maze[i][j] is either '.' or '+'.
//
// - entrance.length == 2
//
// - 0 <= entrance_row < m
//
// - 0 <= entrance_col < n
//
// - entrance will always be an empty cell.
//
// Related Topics: Array, Breadth-First Search, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int NearestExit(char[][] maze, int[] entrance)
    {
        var m = maze.Length;
        var n = maze[0].Length;
        int[][] dirs = [[0, 1], [0, -1], [1, 0], [-1, 0]];

        var q = new Queue<int[]>();
        var visited = new bool[m][];
        for (int i = 0; i < m; i++)
        {
            visited[i] = new bool[n];
        }

        q.Enqueue(entrance);
        visited[entrance[0]][entrance[1]] = true;

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            step++;
            for (int i = 0; i < sz; i++)
            {
                var cur = q.Dequeue();
                // 每个节点都会尝试向上下左右四个方向扩展一步
                foreach (var dir in dirs)
                {
                    var x = cur[0] + dir[0];
                    var y = cur[1] + dir[1];
                    if (x < 0 || x >= m || y < 0 || y >= n || visited[x][y] || maze[x][y] == '+') continue;
                    // 走到出口了
                    if (x == 0 || x == m - 1 || y == 0 || y == n - 1) return step;

                    q.Enqueue([x, y]);
                    visited[x][y] = true;
                }
            }
        }

        return -1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
