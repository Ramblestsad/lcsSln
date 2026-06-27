namespace Scratch.Labuladong.Algorithms.HouseRobber;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private int[] memo = [];

    public int Rob(int[] nums)
    {
        memo = new int[nums.Length];
        Array.Fill(memo, -1);

        // 强盗从第 0 间房子开始抢劫
        return dp(nums, 0);
    }

    // 返回 dp[start..] 能抢到的最大值
    private int dp(int[] nums, int start)
    {
        if (start >= nums.Length)
        {
            return 0;
        }

        // 避免重复计算
        if (memo[start] != -1) return memo[start];

        var res = Math.Max(
            dp(nums, start + 1),
            nums[start] + dp(nums, start + 2)
        );
        // 记入备忘录
        memo[start] = res;
        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
