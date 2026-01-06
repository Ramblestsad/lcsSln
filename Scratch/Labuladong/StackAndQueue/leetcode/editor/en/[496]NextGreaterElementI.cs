namespace Scratch.Labuladong.Algorithms.NextGreaterElementI;

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
