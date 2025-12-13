namespace Scratch.Labuladong.Algorithms.SortTheMatrixDiagonally;

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

        // 从数组末尾删除元素效率较高，所以把 ArrayList 倒序排序
        foreach (var diagonal in diagonals.Values)
        {
            diagonal.Sort();
            diagonal.Reverse();
        }

        // 把排序结果回填二维矩阵
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
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
