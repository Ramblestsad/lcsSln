namespace Scratch.Labuladong.Algorithms.SingleNumber;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int SingleNumber(int[] nums)
    {
        var res = 0;

        foreach (var num in nums)
        {
            res ^= num;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
