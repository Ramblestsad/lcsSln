namespace Scratch.Labuladong.Algorithms.SortTheMatrixDiagonally;

// 1329. Sort the Matrix Diagonally (Medium)
//
// A matrix diagonal is a diagonal line of cells starting from some cell in either the topmost row
// or leftmost column and going in the bottom-right direction until reaching the matrix's end. For
// example, the matrix diagonal starting from mat[2][0], where mat is a 6 x 3 matrix, includes
// cells mat[2][0], mat[3][1], and mat[4][2].
//
// Given an m x n matrix mat of integers, sort each matrix diagonal in ascending order and return
// the resulting matrix.
//
// Example 1:
//
// Input: mat = [[3,3,1,1],[2,2,1,2],[1,1,1,2]]
// Output: [[1,1,1,1],[1,2,2,2],[1,2,3,3]]
//
// Example 2:
//
// Input: mat =
// [[11,25,66,1,69,7],[23,55,17,45,15,52],[75,31,36,44,58,8],[22,27,33,25,68,4],[84,28,14,11,5,50]]
// Output:
// [[5,17,4,1,52,7],[11,11,25,45,8,69],[14,23,25,44,58,15],[22,27,31,36,50,66],[84,28,75,33,55,68]]
//
// Constraints:
//
// - m == mat.length
//
// - n == mat[i].length
//
// - 1 <= m, n <= 100
//
// - 1 <= mat[i][j] <= 100
//
// Related Topics: Array, Sorting, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[][] DiagonalSort(int[][] mat)
    {
        // 如何快速判断两个元素坐标是否在同一个对角线上？
        // 在同一个对角线上的元素，其横纵坐标之差是相同的
        int m = mat.Length, n = mat[0].Length;
        var diagonals = new Dictionary<int, List<int>>();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 横纵坐标之差可以作为一条对角线的 ID
                var diagonalId = i - j;
                if (!diagonals.TryGetValue(diagonalId, out var list))
                {
                    list = new List<int>();
                    diagonals[diagonalId] = list;
                }

                list.Add(mat[i][j]);
            }
        }

        // 排序每条对角线
        foreach (var diagonal in diagonals.Values)
        {
            diagonal.Sort();
            // 从数组末尾删除元素效率较高，所以把 ArrayList 倒序排序
            diagonal.Reverse();
        }

        // 把排序结果回填二维矩阵
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // 现在每条对角线是倒序的，所以从末尾取最小元素
                var diagonal = diagonals[i - j];
                var lastIndex = diagonal.Count - 1;
                mat[i][j] = diagonal[lastIndex];
                diagonal.RemoveAt(lastIndex);
            }
        }

        return mat;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
