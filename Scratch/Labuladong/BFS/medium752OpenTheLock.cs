namespace Scratch.Labuladong.Algorithms.OpenTheLock;

// 752. Open the Lock (Medium)
//
// You have a lock in front of you with 4 circular wheels. Each wheel has 10 slots: '0', '1', '2',
// '3', '4', '5', '6', '7', '8', '9'. The wheels can rotate freely and wrap around: for example we
// can turn '9' to be '0', or '0' to be '9'. Each move consists of turning one wheel one slot.
//
// The lock initially starts at '0000', a string representing the state of the 4 wheels.
//
// You are given a list of deadends dead ends, meaning if the lock displays any of these codes, the
// wheels of the lock will stop turning and you will be unable to open it.
//
// Given a target representing the value of the wheels that will unlock the lock, return the
// minimum total number of turns required to open the lock, or -1 if it is impossible.
//
// Example 1:
//
// Input: deadends = ["0201","0101","0102","1212","2002"], target = "0202"
// Output: 6
// Explanation:
// A sequence of valid moves would be "0000" -> "1000" -> "1100" -> "1200" -> "1201" -> "1202" ->
// "0202".
// Note that a sequence like "0000" -> "0001" -> "0002" -> "0102" -> "0202" would be invalid,
// because the wheels of the lock become stuck after the display becomes the dead end "0102".
//
// Example 2:
//
// Input: deadends = ["8888"], target = "0009"
// Output: 1
// Explanation: We can turn the last wheel in reverse to move from "0000" -> "0009".
//
// Example 3:
//
// Input: deadends = ["8887","8889","8878","8898","8788","8988","7888","9888"], target = "8888"
// Output: -1
// Explanation: We cannot reach the target without getting stuck.
//
// Constraints:
//
// - 1 <= deadends.length <= 500
//
// - deadends[i].length == 4
//
// - target.length == 4
//
// - target will not be in the list deadends.
//
// - target and deadends[i] consist of digits only.
//
// Related Topics: Array, Hash Table, String, Breadth-First Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int OpenLock(string[] deadends, string target)
    {
        // 记录需要跳过的死亡密码
        var deads = new HashSet<string>();
        foreach (var deadend in deadends)
        {
            deads.Add(deadend);
        }

        if (deads.Contains("0000")) return -1;

        var visited = new HashSet<string>();
        var q = new Queue<string>();
        q.Enqueue("0000");
        visited.Add("0000");

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++)
            {
                var cur = q.Dequeue();
                if (cur == target) return step;
                // 将一个节点的合法相邻节点加入队列
                foreach (var neighbor in _getNeighbors(cur))
                {
                    if (!visited.Contains(neighbor) && !deads.Contains(neighbor))
                    {
                        q.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }

            step++;
        }

        return -1;
    }

    // 将 s[j] 向上拨动一次
    string plusOne(string s, int j)
    {
        var ch = s.ToCharArray();
        if (ch[j] == '9')
            ch[j] = '0';
        else
            ch[j] = (char)( ch[j] + 1 );
        return new string(ch);
    }

    // 将 s[i] 向下拨动一次
    string minusOne(string s, int j)
    {
        var ch = s.ToCharArray();
        if (ch[j] == '0')
            ch[j] = '9';
        else
            ch[j] = (char)( ch[j] - 1 );
        return new string(ch);
    }

    // 将 s 的每一位向上拨动一次或向下拨动一次，8 种相邻密码
    List<string> _getNeighbors(string s)
    {
        List<string> neighbors = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            neighbors.Add(plusOne(s, i));
            neighbors.Add(minusOne(s, i));
        }

        return neighbors;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
