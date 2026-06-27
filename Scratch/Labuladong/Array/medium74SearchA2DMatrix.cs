namespace Scratch.Labuladong.Algorithms.Search2DMatrix;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        // 把二维数组映射到一维
        var left = 0;
        var right = m * n;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;
            var midVal = _get(matrix, mid);
            if (midVal > target)
            {
                right = mid;
            }
            else if (midVal < target)
            {
                left = mid + 1;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    private int _get(int[][] matrix, int index)
    {
        var n = matrix[0].Length;

        var i = index / n;
        var j = index % n;

        return matrix[i][j];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
