namespace Scratch.Labuladong.Algorithms.SimplifyPath;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string SimplifyPath(string path)
    {
        var parts = path.Split("/");
        var stk = new Stack<string>();

        foreach (var part in parts)
        {
            if (part.Length == 0 || part == ".") continue;
            if (part == "..")
            {
                if (stk.Count != 0) stk.Pop();
                continue;
            }

            stk.Push(part);
        }

        var res = "";
        while (stk.Count != 0)
        {
            res = "/" + stk.Pop() + res;
        }

        return res.Length == 0 ? "/" : res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
