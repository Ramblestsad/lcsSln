namespace Scratch.Labuladong.Algorithms.MajorityElement;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MajorityElement(int[] nums)
    {
        var target = 0;
        var cnt = 0;

        foreach (var num in nums)
        {
            if (cnt == 0)
            {
                // 当cnt为0时，假设num就是众数
                target = num;
                cnt = 1;
            }
            else if (num == target)
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }

        return target;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
