namespace Scratch.Labuladong.Algorithms.SeatchInsertPos;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        var i = 0;
        var j = nums.Length;

        while (i < j)
        {
            var mid = i + ( j - i ) / 2;
            if (nums[mid] < target)
            {
                i = mid + 1;
            }
            else if (nums[mid] > target)
            {
                j = mid;
            }
            else
            {
                j = mid;
            }
        }

        return i;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
