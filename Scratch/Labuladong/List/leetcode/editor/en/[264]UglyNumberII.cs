namespace Scratch.Labuladong.Algorithms.UglyNumberII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int NthUglyNumber(int n)
    {
        // 理解为三个指向有序链表头结点的指针
        int p2 = 1, p3 = 1, p5 = 1;
        // 理解为三个有序链表的头节点的值
        int product2 = 1, product3 = 1, product5 = 1;
        // 理解为最终合并的有序链表（结果链表）
        var ugly = new int[n + 1];
        // 理解为结果链表上的指针
        var p = 1;

        // 开始合并三个有序链表
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

    public bool IsUglyNumber(int n)
    {
        if (n <= 0) return false;

        while (n % 2 == 0) n /= 2;
        while (n % 3 == 0) n /= 3;
        while (n % 5 == 0) n /= 5;

        return n == 1;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
