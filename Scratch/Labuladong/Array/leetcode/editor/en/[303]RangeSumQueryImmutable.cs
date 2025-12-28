namespace Scratch.Labuladong.Algorithms.RangeSumQueryImmutable;

//leetcode submit region begin(Prohibit modification and deletion)
public class NumArray
{
    private int[] PreSum { get; set; }

    public NumArray(int[] nums)
    {
        // preSum[0] = 0，便于计算累加和
        PreSum = new int[nums.Length + 1];

        // 计算 nums 的累加和
        for (int i = 1; i < PreSum.Length; i++)
        {
            PreSum[i] = PreSum[i - 1] + nums[i - 1];
        }
    }

    public int SumRange(int left, int right)
    {
        return PreSum[right + 1] - PreSum[left];
    }
}

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * int param_1 = obj.SumRange(left,right);
 */
//leetcode submit region end(Prohibit modification and deletion)
