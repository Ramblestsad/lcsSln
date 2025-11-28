namespace Scratch.Labuladong.Algorithms.SortColors;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public void SortColors(int[] nums)
    {
        int currentIndex = 0, zeros = 0, ones = 0, twos = 0;

        foreach (var num in nums)
        {
            if (num == 0) zeros++;
            if (num == 1) ones++;
            if (num == 2) twos++;
        }

        for (; zeros > 0; zeros--)
        {
            nums[currentIndex] = 0;
            currentIndex++;
        }

        for (; ones > 0; ones--)
        {
            nums[currentIndex] = 1;
            currentIndex++;
        }

        for (; twos > 0; twos--)
        {
            nums[currentIndex] = 2;
            currentIndex++;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
