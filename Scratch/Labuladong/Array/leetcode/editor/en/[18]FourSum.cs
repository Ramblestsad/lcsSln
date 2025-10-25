namespace Scratch.Labuladong.Algorithms.FourSum;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        Array.Sort(nums);
        // var n = nums.Length;
        // var res = new List<IList<int>>();
        //
        // // 穷举 fourSum 的第一个数
        // for (var i = 0; i < n; i++)
        // {
        //     // 对 target - nums[i] 计算 threeSum
        //     var triples = ThreeSumTarget(nums, i + 1, target - nums[i]);
        //     // 如果存在满足条件的三元组，再加上 nums[i] 就是结果四元组
        //     foreach (var triple in triples)
        //     {
        //         triple.Add(nums[i]);
        //         res.Add(triple);
        //     }
        //
        //     // fourSum 的第一个数不能重复
        //     while (i < n - 1 && nums[i] == nums[i + 1]) i++;
        // }
        //
        // return res;

        // n 为 4，从 nums[0] 开始计算和为 target 的四元组
        return NSumTarget(nums, 4, 0, target);
    }

    private List<IList<int>> ThreeSumTarget(int[] nums, int start, long target)
    {
        var n = nums.Length;
        var res = new List<IList<int>>();

        // 穷举三元数组的第一个数
        for (var i = start; i < n; i++)
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

    private List<IList<int>> TwoSumTarget(int[] nums, int start, long target)
    {
        // 包list为了动态添加元素
        var res = new List<IList<int>>();
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

    private List<IList<int>> NSumTarget(int[] nums, int n, int start, long target)
    {
        var sz = nums.Length;
        var res = new List<IList<int>>();
        // 至少是 2Sum，且数组大小不应该小于 n
        if (n < 2 || sz < n) return res;

        // 2Sum 是 base case
        if (n == 2)
        {
            // 双指针那一套操作
            var lo = start;
            var hi = sz - 1;
            while (lo < hi)
            {
                var sum = nums[lo] + nums[hi];
                int left = nums[lo], right = nums[hi];
                if (sum < target)
                {
                    while (lo < hi && nums[lo] == left) lo++;
                }
                else if (sum > target)
                {
                    while (lo < hi && nums[hi] == right) hi--;
                }
                else
                {
                    res.Add([left, right]);
                    while (lo < hi && nums[lo] == left) lo++;
                    while (lo < hi && nums[hi] == right) hi--;
                }
            }
        }
        else
        {
            // n > 2 时，递归计算 (n-1)Sum 的结果
            for (var i = start; i < sz; i++)
            {
                var sub = NSumTarget(nums, n - 1, i + 1, target - nums[i]);
                foreach (var s in sub)
                {
                    // (n-1)Sum 加上 nums[i] 就是 nSum
                    s.Add(nums[i]);
                    res.Add(s);
                }

                while (i < sz - 1 && nums[i] == nums[i + 1]) i++;
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
