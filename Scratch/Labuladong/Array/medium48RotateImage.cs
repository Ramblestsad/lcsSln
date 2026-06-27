namespace Scratch.Labuladong.Algorithms.RotateImage;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public void Rotate(int[][] matrix)
    {
        var n = matrix.Length;
        // 沿对角线反转
        for (var i = 0; i < n; i++)
        {
            for (var j = i; j < n; j++)
            {
                // swap(matrix[i][j], matrix[j][i]);
                ( matrix[i][j], matrix[j][i] ) = ( matrix[j][i], matrix[i][j] );
            }
        }

        // 逐行反转
        foreach (var row in matrix)
        {
            reverse(row);
        }
    }

    public void Rotate2(int[][] matrix)
    {
        var n = matrix.Length;
        // 沿左下到右上的对角线镜像对称二维矩阵
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n - i; j++)
            {
                // swap(matrix[i][j], matrix[n-j-1][n-i-1])
                ( matrix[i][j], matrix[n - j - 1][n - i - 1] ) = ( matrix[n - j - 1][n - i - 1], matrix[i][j] );
            }
        }
    }

    private void reverse(int[] arr)
    {
        int i = 0, j = arr.Length - 1;
        while (j > i)
        {
            // swap(arr[i], arr[j]);
            ( arr[i], arr[j] ) = ( arr[j], arr[i] );
            i++;
            j--;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
