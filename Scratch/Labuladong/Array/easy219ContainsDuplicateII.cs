namespace Scratch.Labuladong.Algorithms.ContainsDuplicateII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        /*
         * 1、什么时候应该扩大窗口？
         * 2、什么时候应该缩小窗口？
         * 3、什么时候得到一个合法的答案？
         * 本题很简单直接，以上三个问题的答案是：
         * 1、当窗口大小小于 k 时，扩大窗口。
         * 2、当窗口大小大于 k 时，缩小窗口。
         * 3、当窗口大小等于 k 且发现窗口中存在重复元素时，返回 true。
         */

        int left = 0, right = 0;
        var window = new HashSet<int>();

        while (right < nums.Length)
        {
            if (window.Contains(nums[right])) return true;
            window.Add(nums[right]);
            right++;

            if (right - left > k)
            {
                window.Remove(nums[left]);
                left++;
            }
        }

        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
