/*
 * @lc app=leetcode id=344 lang=csharp
 * @lcpr version=30402
 *
 * [344] Reverse String
 */

namespace Scratch.Labuladong.Algorithms.ReverseString;

// @lc code=start
public class Solution
{
    public void ReverseString(char[] s)
    {
        var left = 0;
        var right = s.Length - 1;

        while (left < right)
        {
            ( s[left], s[right] ) = ( s[right], s[left] );
            left++;
            right--;
        }
    }
}
// @lc code=end
