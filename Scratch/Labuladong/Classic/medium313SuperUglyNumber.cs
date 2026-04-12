/*
 * @lc app=leetcode id=313 lang=csharp
 * @lcpr version=30402
 *
 * [313] Super Ugly Number
 */

namespace Scratch.Labuladong.Algorithms.SuperUglyNumber;

//A super ugly number is a positive integer whose prime factors are in the
//array primes.
//
// Given an integer n and an array of integers primes, return the nᵗʰ super
//ugly number.
//
// The nᵗʰ super ugly number is guaranteed to fit in a 32-bit signed integer.
//
//
// Example 1:
//
//
//Input: n = 12, primes = [2,7,13,19]
//Output: 32
//Explanation: [1,2,4,7,8,13,14,16,19,26,28,32] is the sequence of the first 12
//super ugly numbers given primes = [2,7,13,19].
//
//
// Example 2:
//
//
//Input: n = 1, primes = [2,3,5]
//Output: 1
//Explanation: 1 has no prime factors, therefore all of its prime factors are
//in the array primes = [2,3,5].
//
//
//
// Constraints:
//
//
// 1 <= n <= 10⁵
// 1 <= primes.length <= 100
// 2 <= primes[i] <= 1000
// primes[i] is guaranteed to be a prime number.
// All the values of primes are unique and sorted in ascending order.
//
//
// Related TopicsArray | Math | Dynamic Programming
//
// 👍 2286, 👎 408bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int NthSuperUglyNumber(int n, int[] primes)
    {
        // 优先队列中装三元组 int[] {product, prime, pi}
        // 其中 product 代表链表节点的值，prime 是计算下一个节点所需的质数因子，pi 代表链表上的指针
        var pq = new PriorityQueue<(long product, int prime, int pi), long>();

        // 把多条链表的头结点加入优先级队列
        foreach (int t in primes)
        {
            pq.Enqueue(( 1, t, 1 ), 1L);
        }

        // 可以理解为最终合并的有序链表（结果链表）
        var ugly = new int[n + 1];
        // 可以理解为结果链表上的指针
        var p = 1;

        while (p <= n)
        {
            // 取三个链表的最小结点
            var pair = pq.Dequeue();
            var product = pair.product;
            var prime = pair.prime;
            var index = pair.pi;

            // 避免结果链表出现重复元素
            if (product != ugly[p - 1])
            {
                // 接到结果链表上
                ugly[p] = (int)product;
                p++;
            }

            // 生成下一个节点加入优先级队列
            var nextProduct = (long)ugly[index] * prime;
            pq.Enqueue(( nextProduct, prime, index + 1 ), nextProduct);
        }

        return ugly[n];
    }
}
// @lc code=end
