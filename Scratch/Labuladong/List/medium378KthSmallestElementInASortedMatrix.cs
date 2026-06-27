namespace Scratch.Labuladong.Algorithms.KthSmallestElementInASortedMatrix;

// 378. Kth Smallest Element in a Sorted Matrix (Medium)
//
// Given an n x n matrix where each of the rows and columns is sorted in ascending order, return
// the k^th smallest element in the matrix.
//
// Note that it is the k^th smallest element in the sorted order, not the k^th distinct element.
//
// You must find a solution with a memory complexity better than O(n^2).
//
// Example 1:
//
// Input: matrix = [[1,5,9],[10,11,13],[12,13,15]], k = 8
// Output: 13
// Explanation: The elements in the matrix are [1,5,9,10,11,12,13,13,15], and the 8^th smallest
// number is 13
//
// Example 2:
//
// Input: matrix = [[-5]], k = 1
// Output: -5
//
// Constraints:
//
// - n == matrix.length == matrix[i].length
//
// - 1 <= n <= 300
//
// - -10^9 <= matrix[i][j] <= 10^9
//
// - All the rows and columns of matrix are guaranteed to be sorted in non-decreasing order.
//
// - 1 <= k <= n^2
//
// Follow up:
//
// - Could you solve the problem with a constant memory (i.e., O(1) memory complexity)?
//
// - Could you solve the problem in O(n) time complexity? The solution may be too advanced for an
// interview but you may find reading this paper fun.
//
// Related Topics: Array, Binary Search, Sorting, Heap (Priority Queue), Matrix

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
