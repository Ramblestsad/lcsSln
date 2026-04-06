/*
 * @lc app=leetcode id=365 lang=csharp
 * @lcpr version=30402
 *
 * [365] Water And Jug Problem
 */

namespace Scratch.Labuladong.Algorithms.WaterAndJugProblem;

// You are given two jugs with capacities x liters and y liters. You have an
// infinite water supply. Return whether the total amount of water in both jugs may
// reach target using the following operations:
//
//
// Fill either jug completely with water.
// Completely empty either jug.
// Pour water from one jug into another until the receiving jug is full, or the
// transferring jug is empty.
//
//
//
// Example 1:
//
//
// Input: x = 3, y = 5, target = 4
//
//
// Output: true
//
// Explanation:
//
// Follow these steps to reach a total of 4 liters:
//
//
// Fill the 5-liter jug (0, 5).
// Pour from the 5-liter jug into the 3-liter jug, leaving 2 liters (3, 2).
// Empty the 3-liter jug (0, 2).
// Transfer the 2 liters from the 5-liter jug to the 3-liter jug (2, 0).
// Fill the 5-liter jug again (2, 5).
// Pour from the 5-liter jug into the 3-liter jug until the 3-liter jug is full.
// This leaves 4 liters in the 5-liter jug (3, 4).
// Empty the 3-liter jug. Now, you have exactly 4 liters in the 5-liter jug (0,
//4).
//
//
// Reference: The Die Hard example.
//
// Example 2:
//
//
// Input: x = 2, y = 6, target = 5
//
//
// Output: false
//
// Example 3:
//
//
// Input: x = 1, y = 2, target = 3
//
//
// Output: true
//
// Explanation: Fill both jugs. The total amount of water in both jugs is equal
//to 3 now.
//
//
// Constraints:
//
//
// 1 <= x, y, target <= 10³
//
//
// Related TopicsMath | Depth-First Search | Breadth-First Search
//
// 👍 1676, 👎 1515bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public bool CanMeasureWater(int x, int y, int target)
    {
        var jug1Capacity = x;
        var jug2Capacity = y;
        var targetCapacity = target;
        // BFS 算法的队列
        var q = new Queue<int[]>();
        // 用来记录已经遍历过的状态，把元组转化成数字方便存储哈希集合
        // 转化方式是 (x, y) -> (x * (jug2Capacity + 1) + y)
        // 因为水桶 2 的取值是 [0, jug2Capacity]，所以需要额外加一，类比二维数组坐标转一维坐标
        // 且考虑到题目输入的数据规模较大，相乘可能导致 int 溢出，所以使用 long 类型
        var visited = new HashSet<long>();
        // 添加初始状态，两个桶都没有水
        q.Enqueue([0, 0]);
        visited.Add((long)0 * ( 0 + 1 ) + 0);

        while (q.Count > 0)
        {
            var curState = q.Dequeue();
            if (curState[0] == targetCapacity || curState[1] == targetCapacity
                                              || curState[0] + curState[1] == targetCapacity) return true;

            // 计算出所有可能的下一个状态
            var nextStates = new List<int[]>();
            // 把 1 桶灌满
            nextStates.Add([jug1Capacity, curState[1]]);
            // 把 2 桶灌满
            nextStates.Add([curState[0], jug2Capacity]);
            // 把 1 桶倒空
            nextStates.Add([0, curState[1]]);
            // 把 2 桶倒空
            nextStates.Add([curState[0], 0]);
            // 把 1 桶的水灌进 2 桶，直到 1 桶空了或者 2 桶满了
            // 壶1 倒空了 → 最多倒出 curState[0]
            // 壶2 装满了 → 最多再装 jug2Capacity - curState[1]
            var toPour1To2 = Math.Min(curState[0], jug2Capacity - curState[1]);
            nextStates.Add([
                curState[0] - toPour1To2,
                curState[1] + toPour1To2
            ]);
            // 把 2 桶的水灌进 1 桶，直到 2 桶空了或者 1 桶满了
            var toPour2To1 = Math.Min(curState[1], jug1Capacity - curState[0]);
            nextStates.Add([
                curState[0] + toPour2To1,
                curState[1] - toPour2To1
            ]);

            // 把所有可能的下一个状态都放进队列里
            foreach (var nextState in nextStates)
            {
                // 把二维坐标转化为数字，方便去重
                var hash = (long)nextState[0] * ( jug2Capacity + 1 ) + nextState[1];
                if (visited.Contains(hash)) continue;

                q.Enqueue(nextState);
                visited.Add(hash);
            }
        }

        return false;
    }
}
// @lc code=end
