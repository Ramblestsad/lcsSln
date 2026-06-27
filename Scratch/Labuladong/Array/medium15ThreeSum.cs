namespace Scratch.Labuladong.Algorithms.ThreeSum;

// 15. 3Sum (Medium)
//
// Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i !=
// j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
//
// Notice that the solution set must not contain duplicate triplets.
//
// Example 1:
//
// Input: nums = [-1,0,1,2,-1,-4]
// Output: [[-1,-1,2],[-1,0,1]]
// Explanation:
// nums[0] + nums[1] + nums[2] = (-1) + 0 + 1 = 0.
// nums[1] + nums[2] + nums[4] = 0 + 1 + (-1) = 0.
// nums[0] + nums[3] + nums[4] = (-1) + 2 + (-1) = 0.
// The distinct triplets are [-1,0,1] and [-1,-1,2].
// Notice that the order of the output and the order of the triplets does not matter.
//
// Example 2:
//
// Input: nums = [0,1,1]
// Output: []
// Explanation: The only possible triplet does not sum up to 0.
//
// Example 3:
//
// Input: nums = [0,0,0]
// Output: [[0,0,0]]
// Explanation: The only possible triplet sums up to 0.
//
// Constraints:
//
// - 3 <= nums.length <= 3000
//
// - -10^5 <= nums[i] <= 10^5
//
// Related Topics: Array, Two Pointers, Sorting

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
