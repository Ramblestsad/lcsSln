namespace Scratch.Labuladong.Algorithms.SquaresOfASortedArray;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] SortedSquares(int[] nums)
    {
        // 直接将双指针分别初始化在 nums 的开头和结尾，相当于合并两个从大到小排序的数组，和 88 题类似
        var n = nums.Length;
        // 两个指针分别初始化在正负子数组绝对值最大的元素索引
        int i = 0, j = n - 1;
        // 得到的有序结果是降序的
        var p = n - 1;
        var res = new int[n];

        while (i <= j)
        {
            if (Math.Abs(nums[i]) > Math.Abs(nums[j]))
            {
                res[p] = nums[i] * nums[i];
                i++;
            }
            else
            {
                res[p] = nums[j] * nums[j];
                j--;
            }

            p--;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
