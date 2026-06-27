namespace Scratch.Labuladong.Algorithms.Search2DMatrixII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        // 初始化位置在右上角
        var i = 0;
        var j = n - 1;

        while (i < m && j >= 0)
        {
            if (matrix[i][j] < target)
            {
                // 需要大一点，向下移动
                i++;
            }
            else if (matrix[i][j] > target)
            {
                // 需要小一点，向左移动
                j--;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
