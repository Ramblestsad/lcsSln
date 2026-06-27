namespace Scratch.Labuladong.Algorithms.RemoveElement;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int RemoveElement(int[] nums, int val)
    {
        var fast = 0;
        var slow = 0;

        while (fast < nums.Length)
        {
            if (nums[fast] != val)
            {
                nums[slow] = nums[fast];
                slow++;
            }

            fast++;
        }

        return slow;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
