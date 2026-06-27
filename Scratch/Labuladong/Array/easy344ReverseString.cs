namespace Scratch.Labuladong.Algorithms.ReverseString;

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
