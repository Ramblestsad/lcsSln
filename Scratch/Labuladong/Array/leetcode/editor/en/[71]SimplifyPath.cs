namespace Scratch.Labuladong.Algorithms.SimplifyPath;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string SimplifyPath(string path)
    {
        var parts = path.Split("/");
        var stk = new Stack<string>();

        // "a/./b/..//c/
        // parts: ["a", ".", "b", "..", "", "c", ""]
        // “b” 与 ".." 抵消
        // res = "/a/c
        foreach (var part in parts)
        {
            if (part.Length == 0 || part == ".") continue;
            if (part == "..")
            {
                // ".." 碰到上级目录就跟栈里已经压入的“抵消”
                // 栈里没有就忽略，说明还是在根目录“/”
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
