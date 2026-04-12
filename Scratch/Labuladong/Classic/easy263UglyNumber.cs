/*
 * @lc app=leetcode id=263 lang=csharp
 * @lcpr version=30402
 *
 * [263] Ugly Number
 */

namespace Scratch.Labuladong.Algorithms.UglyNumber;

//An ugly number is a positive integer which does not have a prime factor other
//than 2, 3, and 5.
//
// Given an integer n, return true if n is an ugly number.
//
//
// Example 1:
//
//
//Input: n = 6
//Output: true
//Explanation: 6 = 2 × 3
//
//
// Example 2:
//
//
//Input: n = 1
//Output: true
//Explanation: 1 has no prime factors.
//
//
// Example 3:
//
//
//Input: n = 14
//Output: false
//Explanation: 14 is not ugly since it includes the prime factor 7.
//
//
//
// Constraints:
//
//
// -2³¹ <= n <= 2³¹ - 1
//
//
// Related TopicsMath
//
// 👍 3835, 👎 1794bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    // 正整数唯一分解定理：
    //      任意一个大于 1 的自然数，要么它本身就是质数，要么它可以分解为若干质数的乘积。

    public bool IsUgly(int n)
    {
        if (n <= 0) return false;

        // 如果 n 是丑数，分解因子应该只有 2, 3, 5
        while (n % 2 == 0) n /= 2;
        while (n % 3 == 0) n /= 3;
        while (n % 5 == 0) n /= 5;

        // 如果 n 是丑数，那么它的所有质因数都只能是 2、3、5，除完之后 n 应该变成 1。
        // 如果 n 不是丑数，说明它含有其他质因数（比如 7、11 等），这些因子除不掉，最后 n 会大于 1。
        return n == 1;
    }
}
// @lc code=end
