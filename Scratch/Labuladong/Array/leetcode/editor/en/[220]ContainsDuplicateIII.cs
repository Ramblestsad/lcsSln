namespace Scratch.Labuladong.Algorithms.ContainsDuplicateIII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff)
    {
        /*
         * 1、什么时候应该扩大窗口？
         * 2、什么时候应该缩小窗口？
         * 3、什么时候得到一个合法的答案？
         * 针对本题，以上三个问题的答案是：
         * 1、当窗口大小 <= indexDiff 时，扩大窗口，包含更多元素。
         * 2、当窗口大小 > indexDiff 时，缩小窗口，减少窗口元素。
         * 3、窗口大小  <= indexDiff，且窗口中存在两个不同元素之差小于 valueDiff 时，找到一个答案。
         */

        /*
         * 如何在窗口 [left, right) 中快速判断是否有元素之差小于 t 的两个元素?
         * 需要使用到 TreeSet 利用二叉搜索树结构寻找「地板元素」和「天花板元素」的特性
         */

        if (indexDiff <= 0 || valueDiff < 0 || nums.Length < 2) return false;

        var window = new SortedSet<long>();
        int left = 0, right = 0;

        while (right < nums.Length)
        {
            // 为了防止 i == j，所以在扩大窗口之前先判断是否有符合题意的索引对 (i, j)
            // 对每个 x = nums[right]
            // 在窗口内快速判断是否存在某个 y 使 (|x - y| <= valueDiff)
            var x = nums[right];
            var low = x - (long)valueDiff;
            var high = x + (long)valueDiff;
            // 窗口内只要存在任意元素落在 [low, high] 即满足 |nums[i] - nums[j]| <= valueDiff
            if (window.GetViewBetween(low, high).Count > 0)
                return true;

            //扩大窗口
            window.Add(x);
            right++;

            if (right - left > indexDiff)
            {
                // 缩小窗口
                window.Remove(nums[left]);
                left++;
            }
        }

        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
