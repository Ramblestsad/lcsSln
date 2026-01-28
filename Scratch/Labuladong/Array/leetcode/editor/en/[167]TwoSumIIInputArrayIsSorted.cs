namespace Scratch.Labuladong.Algorithms.TwoSumIIInputArrayIsSorted;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] TwoSum(int[] numbers, int target)
    {
        var left = 0;
        var right = numbers.Length - 1;

        while (left < right)
        {
            var sum = numbers[left] + numbers[right];
            if (sum == target) return [left + 1, right + 1];
            // 记住numbers是非递减数组，移动左指针，sum就会变大；反之移动右指针，sum就会变小;
            if (sum < target) left++;
            if (sum > target) right--;
        }

        return [-1, -1];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
