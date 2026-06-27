namespace Scratch.Labuladong.Algorithms.NextGreaterElementI;

// 496. Next Greater Element I (Easy)
//
// The next greater element of some element x in an array is the first greater element that is to
// the right of x in the same array.
//
// You are given two distinct 0-indexed integer arrays nums1 and nums2, where nums1 is a subset of
// nums2.
//
// For each 0 <= i < nums1.length, find the index j such that nums1[i] == nums2[j] and determine
// the next greater element of nums2[j] in nums2. If there is no next greater element, then the
// answer for this query is -1.
//
// Return an array ans of length nums1.length such that ans[i] is the next greater element as
// described above.
//
// Example 1:
//
// Input: nums1 = [4,1,2], nums2 = [1,3,4,2]
// Output: [-1,3,-1]
// Explanation: The next greater element for each value of nums1 is as follows:
// - 4 is underlined in nums2 = [1,3,4,2]. There is no next greater element, so the answer is -1.
// - 1 is underlined in nums2 = [1,3,4,2]. The next greater element is 3.
// - 2 is underlined in nums2 = [1,3,4,2]. There is no next greater element, so the answer is -1.
//
// Example 2:
//
// Input: nums1 = [2,4], nums2 = [1,2,3,4]
// Output: [3,-1]
// Explanation: The next greater element for each value of nums1 is as follows:
// - 2 is underlined in nums2 = [1,2,3,4]. The next greater element is 3.
// - 4 is underlined in nums2 = [1,2,3,4]. There is no next greater element, so the answer is -1.
//
// Constraints:
//
// - 1 <= nums1.length <= nums2.length <= 1000
//
// - 0 <= nums1[i], nums2[i] <= 10^4
//
// - All integers in nums1 and nums2 are unique.
//
// - All the integers of nums1 also appear in nums2.
//
// Follow up: Could you find an O(nums1.length + nums2.length) solution?
//
// Related Topics: Array, Hash Table, Stack, Monotonic Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[] NextGreaterElement(int[] nums1, int[] nums2)
    {
        // 记录 nums2 中每个元素的下一个更大元素
        var greater = NextGreaterE(nums2);
        // 转化成映射：元素 x -> x 的下一个最大元素
        var m = new Dictionary<int, int>();
        for (int i = 0; i < nums2.Length; i++)
        {
            m.Add(nums2[i], greater[i]);
        }

        // nums1 是 nums2 的子集，所以根据 greaterMap 可以得到结果
        var res = new int[nums1.Length];
        for (int i = 0; i < nums1.Length; i++)
        {
            if (!m.TryGetValue(nums1[i], out var g))
            {
                res[i] = -1;
            }

            res[i] = g;
        }

        return res;
    }

    // 计算 nums 中每个元素的下一个更大元素
    int[] NextGreaterE(int[] nums)
    {
        var n = nums.Length;
        // 存放答案的数组
        var res = new int[n];
        var s = new Stack<int>();
        // 倒着往栈里放
        for (int i = n - 1; i >= 0; i--)
        {
            // 判定个子高矮
            while (s.Count != 0 && s.Peek() <= nums[i])
            {
                s.Pop();
            }

            // nums[i] 身后的下一个更大元素
            res[i] = s.Count == 0 ? -1 : s.Peek();
            s.Push(nums[i]);
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
