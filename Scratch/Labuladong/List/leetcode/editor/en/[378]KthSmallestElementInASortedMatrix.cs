/*
 * @lc app=leetcode id=378 lang=csharp
 * @lcpr version=30402
 *
 * [378] Kth Smallest Element In A Sorted Matrix
 */

namespace Scratch.Labuladong.Algorithms.KthSmallestElementInASortedMatrix;

// @lc code=start
public class Solution
{
    public int KthSmallest(int[][] matrix, int k)
    {
        // 当成合并K个有序链表，用PriorityQueue
        var pq = new PriorityQueue<(int, int), int>();
        // 每行第一个元素的i, j index入堆，比较其值
        for (int i = 0; i < matrix.Length; i++)
        {
            pq.Enqueue(( i, 0 ), matrix[i][0]);
        }

        var res = -1;

        while (pq.Count > 0 && k > 0)
        {
            var (i, j) = pq.Dequeue();
            res = matrix[i][j];

            k--;

            // 判断行内索引走到头没有
            if (j + 1 < matrix[i].Length)
            {
                pq.Enqueue(( i, j + 1 ), matrix[i][j + 1]);
            }
        }

        return res;
    }
}
// @lc code=end
