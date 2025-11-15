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

    private void reverse(int[] arr)
    {
        int i = 0, j = arr.Length - 1;
        while (j > i) {
            // swap(arr[i], arr[j]);
            ( arr[i], arr[j] ) = ( arr[j], arr[i] );
            i++;
            j--;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
