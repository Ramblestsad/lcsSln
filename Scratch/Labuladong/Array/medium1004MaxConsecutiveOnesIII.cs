namespace Scratch.Labuladong.Algorithms.MaxConsecutiveOnesIII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LongestOnes(int[] nums, int k)
    {
        /*
         * 1、什么时候应该扩大窗口？
         * 2、什么时候应该缩小窗口？
         * 3、什么时候得到一个合法的答案？
         * 针对本题，以上三个问题的答案是：
         * 1、当可替换次数大于等于 0 时，扩大窗口，让进入窗口的 0 都变成 1，使得连续的 1 的长度尽可能大。
         * 2、当可替换次数小于 0 时，缩小窗口，空余出可替换次数，以便继续扩大窗口。
         * 3、只要可替换次数大于等于 0，窗口中的元素都会被替换成 1，也就是连续为 1 的子数组，我们想求的就是最大窗口长度。
         */

        int left = 0, right = 0;
        // 记录窗口中 1 的出现次数
        var windowOneCount = 0;
        var res = 0;

        while (right < nums.Length)
        {
            if (nums[right] == 1) windowOneCount++;
            right++;

            while (right - left - windowOneCount > k)
            {
                // 当窗口中需要替换的 0 的数量大于 k，缩小窗口
                if (nums[left] == 1) windowOneCount--;
                left++;
            }

            // 此时一定是一个合法的窗口，求最大窗口长度
            res = Math.Max(res, right - left);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
