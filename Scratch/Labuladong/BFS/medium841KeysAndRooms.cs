namespace Scratch.Labuladong.Algorithms.KeysAndRooms;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var n = rooms.Count;
        var visited = new bool[n];
        var q = new Queue<int>();
        q.Enqueue(0);
        visited[0] = true;

        while (q.Count > 0)
        {
            var room = q.Dequeue();
            foreach (var nextRoom in rooms[room])
            {
                if (!visited[nextRoom])
                {
                    q.Enqueue(nextRoom);
                    visited[nextRoom] = true;
                }
            }
        }

        // 必须全部room都解锁
        foreach (var b in visited)
        {
            if (!b) return false;
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
