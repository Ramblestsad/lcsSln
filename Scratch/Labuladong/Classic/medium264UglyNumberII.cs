namespace Scratch.Labuladong.Algorithms.UglyNumberII;

//An ugly number is a positive integer whose prime factors are limited to 2, 3,
//and 5.
//
// Given an integer n, return the nᵗʰ ugly number.
//
//
// Example 1:
//
//
//Input: n = 10
//Output: 12
//Explanation: [1, 2, 3, 4, 5, 6, 8, 9, 10, 12] is the sequence of the first 10
//ugly numbers.
//
//
// Example 2:
//
//
//Input: n = 1
//Output: 1
//Explanation: 1 has no prime factors, therefore all of its prime factors are
//limited to 2, 3, and 5.
//
//
//
// Constraints:
//
//
// 1 <= n <= 1690
//
//
// Related TopicsHash Table | Math | Dynamic Programming | Heap (Priority Queue)
//
//
// 👍 6854, 👎 448bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    // 基于筛数法筛选质数的思路和丑数的定义
    // 不难想到这样一个规律：
    //      如果一个数 x 是丑数，那么 x * 2, x * 3, x * 5 都一定是丑数。

    public int NthUglyNumber(int n)
    {
        // 可以理解为三个指向有序链表头结点的指针
        int p2 = 1, p3 = 1, p5 = 1;
        // 可以理解为三个有序链表的头节点的值
        int product2 = 1, product3 = 1, product5 = 1;
        // 可以理解为最终合并的有序链表（结果链表）
        var ugly = new int[n + 1];
        // 可以理解为结果链表上的指针
        int p = 1;

        while (p <= n)
        {
            // 取三个链表的最小结点
            var min = Math.Min(Math.Min(product2, product3), product5);
            // 接到结果链表上
            ugly[p] = min;
            p++;

            // 前进对应有序链表上的指针
            if (min == product2)
            {
                product2 = 2 * ugly[p2];
                p2++;
            }

            if (min == product3)
            {
                product3 = 3 * ugly[p3];
                p3++;
            }

            if (min == product5)
            {
                product5 = 5 * ugly[p5];
                p5++;
            }
        }

        return ugly[n];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
