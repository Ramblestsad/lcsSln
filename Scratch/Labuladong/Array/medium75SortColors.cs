namespace Scratch.Labuladong.Algorithms.SortColors;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public void SortColors(int[] nums)
    {
        // 注意区间的开闭，初始化时区间内应该没有元素
        // 所以我们定义 [0，p0) 是元素 0 的区间，(p2, nums.length - 1] 是 2 的区间
        int p0 = 0, p2 = nums.Length - 1, p = 0;
        // 由于 p2 是开区间，所以 p <= p2
        while (p <= p2)
        {
            if (nums[p] == 0)
            {
                ( nums[p0], nums[p] ) = ( nums[p], nums[p0] );
                p0++;
            }
            else if (nums[p] == 2)
            {
                ( nums[p2], nums[p] ) = ( nums[p], nums[p2] );
                p2--;
            }
            else if (nums[p] == 1)
            {
                p++;
            }

            // 因为小于 p0 都是 0，所以 p 不要小于 p0
            if (p < p0)
            {
                p = p0;
            }
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
