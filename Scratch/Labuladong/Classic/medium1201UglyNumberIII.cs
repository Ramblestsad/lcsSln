/*
 * @lc app=leetcode id=1201 lang=csharp
 * @lcpr version=30402
 *
 * [1201] Ugly Number III
 */

namespace Scratch.Labuladong.Algorithms.UglyNumberIII;

//An ugly number is a positive integer that is divisible by a, b, or c.
//
// Given four integers n, a, b, and c, return the nᵗʰ ugly number.
//
//
// Example 1:
//
//
//Input: n = 3, a = 2, b = 3, c = 5
//Output: 4
//Explanation: The ugly numbers are 2, 3, 4, 5, 6, 8, 9, 10... The 3ʳᵈ is 4.
//
//
// Example 2:
//
//
//Input: n = 4, a = 2, b = 3, c = 4
//Output: 6
//Explanation: The ugly numbers are 2, 3, 4, 6, 8, 9, 10, 12... The 4ᵗʰ is 6.
//
//
// Example 3:
//
//
//Input: n = 5, a = 2, b = 11, c = 13
//Output: 10
//Explanation: The ugly numbers are 2, 4, 6, 8, 10, 11, 12, 13... The 5ᵗʰ is 10.
//
//
//
//
// Constraints:
//
//
// 1 <= n, a, b, c <= 10⁹
// 1 <= a * b * c <= 10¹⁸
// It is guaranteed that the result will be in range [1, 2 * 10⁹].
//
//
// Related TopicsMath | Binary Search | Combinatorics | Number Theory
//
// 👍 1318, 👎 518bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int NthUglyNumber(int n, int a, int b, int c)
    {
        // 题目说本题结果在 [1, 2 * 10^9] 范围内，
        // 所以就按照这个范围初始化两端都闭的搜索区间
        int left = 1, right = (int)2e9;
        // 搜索左侧边界的二分搜索
        while (left <= right)
        {
            var mid = left + ( right - left ) / 2;
            if (f(mid, a, b, c) < n)
            {
                // [1..mid] 中符合条件的元素个数不足 n，所以目标在右半边
                left = mid + 1;
            }
            else
            {
                // [1..mid] 中符合条件的元素个数大于 n，所以目标在左半边
                right = mid - 1;
            }
        }

        return left;
    }

    // 函数 f 是一个单调函数
    // 计算 [1..num] 之间有多少个能够被 a 或 b 或 c 整除的数字
    private long f(int num, int a, int b, int c)
    {
        long setA = num / a, setB = num / b, setC = num / c;
        var setAB = num / lcm(a, b);
        var setAC = num / lcm(a, c);
        var setBC = num / lcm(b, c);
        var setABC = num / lcm(lcm(a, b), c);

        // 集合论定理：A + B + C - A ∩ B - A ∩ C - B ∩ C + A ∩ B ∩ C
        return setA + setB + setC - setAB - setAC - setBC + setABC;
    }

    // 最小公倍数
    private long lcm(long a, long b)
    {
        // 最小公倍数就是乘积除以最大公因数
        return a * b / gcd(a, b);
    }

    // 计算最大公因数（辗转相除/欧几里得算法）
    private long gcd(long a, long b)
    {
        if (a < b)
        {
            // 保证 a > b
            return gcd(b, a);
        }

        if (b == 0)
        {
            return a;
        }

        return gcd(b, a % b);
    }
}
// @lc code=end
