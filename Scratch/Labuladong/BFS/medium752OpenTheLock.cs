namespace Scratch.Labuladong.Algorithms.OpenTheLock;

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
