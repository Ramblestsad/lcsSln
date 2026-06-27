namespace Scratch.Labuladong.Algorithms.CountPrimes;

//Given an integer n, return the number of prime numbers that are strictly less
//than n.
//
//
// Example 1:
//
//
//Input: n = 10
//Output: 4
//Explanation: There are 4 prime numbers less than 10, they are 2, 3, 5, 7.
//
//
// Example 2:
//
//
//Input: n = 0
//Output: 0
//
//
// Example 3:
//
//
//Input: n = 1
//Output: 0
//
//
//
// Constraints:
//
//
// 0 <= n <= 5 * 10⁶
//
//
// Related TopicsArray | Math | Enumeration | Number Theory
//
// 👍 8781, 👎 1577bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int CountPrimes(int n)
    {
        // 「素数筛选法」

        var isPrime = new bool[n];
        Array.Fill(isPrime, true);

        for (int i = 2; i * i < n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j < n; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }

        var count = 0;
        for (int i = 2; i < n; i++)
        {
            if (isPrime[i]) count++;
        }

        return count;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
