namespace Scratch.Labuladong.Algorithms.CarFleet;

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
