namespace Scratch.Labuladong.Algorithms.ThreeSum;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        var target = 0;
        var n = nums.Length;
        var res = new List<IList<int>>();

        // 穷举三元数组的第一个数
        for (var i = 0; i < n; i++)
        {
            // 对 target = target - nums[i] 计算 twoSum
            var tuples = TwoSumTarget(nums, i + 1, target - nums[i]);
            // 如果存在满足条件的二元组，再加上 nums[i] 就是结果三元组
            foreach (var tuple in tuples)
            {
                tuple.Add(nums[i]);
                res.Add(tuple);
            }

            // 跳过第一个数字重复的情况，否则会出现重复结果
            while (i < n - 1 && nums[i] == nums[i + 1]) i++;
        }

        return res;
    }

    private List<List<int>> TwoSumTarget(int[] nums, int start, int target)
    {
        // 包list为了动态添加元素
        var res = new List<List<int>>();
        var left = start;
        var right = nums.Length - 1;

        while (left < right)
        {
            var lVal = nums[left];
            var rVal = nums[right];
            var sum = nums[left] + nums[right];
            if (sum == target)
            {
                res.Add([nums[left], nums[right]]);
                // 跳过重复元素
                while (left < right && nums[left] == lVal) left++;
                while (left < right && nums[right] == rVal) right--;
            }

            if (sum < target)
            {
                while (left < right && nums[left] == lVal) left++;
            }

            if (sum > target)
            {
                while (left < right && nums[right] == rVal) right--;
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
