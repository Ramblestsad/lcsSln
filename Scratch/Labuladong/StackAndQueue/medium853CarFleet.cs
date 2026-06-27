namespace Scratch.Labuladong.Algorithms.CarFleet;

// 853. Car Fleet (Medium)
//
// There are n cars at given miles away from the starting mile 0, traveling to reach the mile
// target.
//
// You are given two integer arrays position and speed, both of length n, where position[i] is the
// starting mile of the i^th car and speed[i] is the speed of the i^th car in miles per hour.
//
// A car cannot pass another car, but it can catch up and then travel next to it at the speed of
// the slower car.
//
// A car fleet is a single car or a group of cars driving next to each other. The speed of the car
// fleet is the minimum speed of any car in the fleet.
//
// If a car catches up to a car fleet at the mile target, it will still be considered as part of
// the car fleet.
//
// Return the number of car fleets that will arrive at the destination.
//
// Example 1:
//
// Input: target = 12, position = [10,8,0,5,3], speed = [2,4,1,1,3]
//
// Output: 3
//
// Explanation:
//
// - The cars starting at 10 (speed 2) and 8 (speed 4) become a fleet, meeting each other at 12.
// The fleet forms at target.
//
// - The car starting at 0 (speed 1) does not catch up to any other car, so it is a fleet by
// itself.
//
// - The cars starting at 5 (speed 1) and 3 (speed 3) become a fleet, meeting each other at 6. The
// fleet moves at speed 1 until it reaches target.
//
// Example 2:
//
// Input: target = 10, position = [3], speed = [3]
//
// Output: 1
//
// Explanation:
//
// There is only one car, hence there is only one fleet.
//
// Example 3:
//
// Input: target = 100, position = [0,2,4], speed = [4,2,1]
//
// Output: 1
//
// Explanation:
//
// - The cars starting at 0 (speed 4) and 2 (speed 2) become a fleet, meeting each other at 4. The
// car starting at 4 (speed 1) travels to 5.
//
// - Then, the fleet at 4 (speed 2) and the car at position 5 (speed 1) become one fleet, meeting
// each other at 6. The fleet moves at speed 1 until it reaches target.
//
// Constraints:
//
// - n == position.length == speed.length
//
// - 1 <= n <= 10^5
//
// - 0 < target <= 10^6
//
// - 0 <= position[i] < target
//
// - All the values of position are unique.
//
// - 0 < speed[i] <= 10^6
//
// Related Topics: Array, Stack, Sorting, Monotonic Stack

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int CarFleet(int target, int[] position, int[] speed)
    {
        /*
         * 是否能够形成车队，取决于下述规律：
         * 如果车 x 排在 车 y 后面，且 x 到达终点所需时间比 y 少，则 x 必然会被 y 卡住，形成车队。
         *
         * 先根据每辆车的起始位置 position 排序，然后计算出时间数组 time
         * 假设计算出的 time 数组为 [12, 3, 7, 1, 2]，
         * 那么观察数组的单调性变化，最后肯定会形成三个车队，他们到达终点的时间分别是 12, 7, 2。
         */

        var n = position.Length;
        var cars = new (int pos, int speed)[n];
        for (var i = 0; i < n; i++)
        {
            cars[i] = ( position[i], speed[i] );
        }

        // 按照初始位置，从小到大排序
        Array.Sort(cars, (a, b) => a.pos.CompareTo(b.pos));

        // 计算每辆车到达终点的时间
        var time = new double[n];
        for (var i = 0; i < n; i++)
        {
            var car = cars[i];
            time[i] = (double)( target - car.pos ) / car.speed;
        }

        // 使用单调栈计算车队的数量
        // var stk = new Stack<double>();
        // foreach (var t in time)
        // {
        //     while (stk.Count != 0 && stk.Peek() <= t)
        //     {   // 小于t的pop
        //         // stk里只保留慢的车
        //         stk.Pop();
        //     }
        //
        //     stk.Push(t);
        // }
        //
        // return stk.Count;

        // 避免使用栈模拟，倒序遍历取递增序列就是答案
        var res = 0;
        double maxTime = 0;
        for (var i = n - 1; i >= 0; i--)
        {
            if (time[i] > maxTime)
            {
                maxTime = time[i];
                res++;
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
