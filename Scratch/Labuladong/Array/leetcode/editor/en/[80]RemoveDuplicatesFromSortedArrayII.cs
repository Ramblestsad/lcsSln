namespace Scratch.Labuladong.Algorithms.RemoveDuplicatesFromSortedArrayII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int RemoveDuplicates(int[] nums)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        int slow = 0, fast = 0, count = 0;

        while (fast < n)
        {
            if (nums[fast] != nums[slow])
            {
                // 此时，对于 nums[0..slow] 来说，nums[fast] 是一个新的元素，加进来
                slow++;
                nums[slow] = nums[fast];
            }
            else if (slow < fast && count < 2)
            {
                // 此时，对于 nums[0..slow] 来说，nums[fast] 重复次数小于 2，也加进来
                slow++;
                nums[slow] = nums[fast];
            }

            fast++;
            count++;
            if (fast < n && nums[fast] != nums[fast - 1])
            {
                // fast 遇到新的不同的元素时，重置 count
                count = 0;
            }
        }

        return slow + 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
