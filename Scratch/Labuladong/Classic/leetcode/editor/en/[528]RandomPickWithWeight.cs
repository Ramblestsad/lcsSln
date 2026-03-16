namespace Scratch.Labuladong.Algorithms.RandomPickWithWeight;

//You are given a 0-indexed array of positive integers w where w[i] describes
//the weight of the iᵗʰ index.
//
// You need to implement the function pickIndex(), which randomly picks an
//index in the range [0, w.length - 1] (inclusive) and returns it. The probability of
//picking an index i is w[i] / sum(w).
//
//
// For example, if w = [1, 3], the probability of picking index 0 is 1 / (1 + 3)
// = 0.25 (i.e., 25%), and the probability of picking index 1 is 3 / (1 + 3) = 0.7
//5 (i.e., 75%).
//
//
//
// Example 1:
//
//
//Input
//["Solution","pickIndex"]
//[[[1]],[]]
//Output
//[null,0]
//
//Explanation
//Solution solution = new Solution([1]);
//solution.pickIndex(); // return 0. The only option is to return 0 since there
//is only one element in w.
//
//
// Example 2:
//
//
//Input
//["Solution","pickIndex","pickIndex","pickIndex","pickIndex","pickIndex"]
//[[[1,3]],[],[],[],[],[]]
//Output
//[null,1,1,1,1,0]
//
//Explanation
//Solution solution = new Solution([1, 3]);
//solution.pickIndex(); // return 1. It is returning the second element (index =
// 1) that has a probability of 3/4.
//solution.pickIndex(); // return 1
//solution.pickIndex(); // return 1
//solution.pickIndex(); // return 1
//solution.pickIndex(); // return 0. It is returning the first element (index =
//0) that has a probability of 1/4.
//
//Since this is a randomization problem, multiple answers are allowed.
//All of the following outputs can be considered correct:
//[null,1,1,1,1,0]
//[null,1,1,1,1,1]
//[null,1,1,1,0,0]
//[null,1,1,1,0,1]
//[null,1,0,1,0,0]
//......
//and so on.
//
//
//
// Constraints:
//
//
// 1 <= w.length <= 10⁴
// 1 <= w[i] <= 10⁵
// pickIndex will be called at most 10⁴ times.
//
//
// Related TopicsArray | Math | Binary Search | Prefix Sum | Randomized
//
// 👍 2276, 👎 1057bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 前缀和数组
    private int[] preSum;
    private Random rand = new Random();

    public Solution(int[] w)
    {
        var n = w.Length;
        // 构建前缀和数组，偏移一位留给 preSum[0]
        preSum = new int[n + 1];
        preSum[0] = 0;
        // preSum[i] = sum(w[0..i-1])
        for (int i = 1; i <= n; i++)
        {
            preSum[i] = preSum[i - 1] + w[i - 1];
        }
    }

    public int PickIndex()
    {
        var n = preSum.Length;
        // C# 的 Next(n) 方法在 [0, n) 中生成一个随机整数
        // 再加一就是在闭区间 [1, preSum[n - 1]] 中随机选择一个数字
        var target = rand.Next(preSum[n - 1]) + 1;
        // 获取 target 在前缀和数组 preSum 中的索引
        // 别忘了前缀和数组 preSum 和原始数组 w 有一位索引偏移
        return left_bound(preSum, target) - 1;
    }

    // 搜索左侧边界的二分搜索
    private int left_bound(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        int left = 0, right = nums.Length;
        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (nums[mid] == target)
            {
                right = mid - 1;
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else if (nums[mid] > target)
            {
                right = mid - 1;
            }
        }

        return left;
    }
}

/**
 * Your Solution object will be instantiated and called as such:
 * Solution obj = new Solution(w);
 * int param_1 = obj.PickIndex();
 */
//leetcode submit region end(Prohibit modification and deletion)
