namespace Scratch.Labuladong.Algorithms.RotateImage;

// 48. Rotate Image (Medium)
//
// You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees
// (clockwise).
//
// You have to rotate the image in-place, which means you have to modify the input 2D matrix
// directly. DO NOT allocate another 2D matrix and do the rotation.
//
// Example 1:
//
// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
// Output: [[7,4,1],[8,5,2],[9,6,3]]
//
// Example 2:
//
// Input: matrix = [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
// Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]
//
// Constraints:
//
// - n == matrix.length == matrix[i].length
//
// - 1 <= n <= 20
//
// - -1000 <= matrix[i][j] <= 1000
//
// Related Topics: Array, Math, Matrix

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
