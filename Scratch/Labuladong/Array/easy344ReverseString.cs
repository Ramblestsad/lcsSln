namespace Scratch.Labuladong.Algorithms.ReverseString;

// 344. Reverse String (Easy)
//
// Write a function that reverses a string. The input string is given as an array of characters s.
//
// You must do this by modifying the input array in-place with O(1) extra memory.
//
// Example 1:
//
// Input: s = ["h","e","l","l","o"]
// Output: ["o","l","l","e","h"]
// Example 2:
//
// Input: s = ["H","a","n","n","a","h"]
// Output: ["h","a","n","n","a","H"]
//
// Constraints:
//
// - 1 <= s.length <= 10^5
//
// - s[i] is a printable ascii character.
//
// Related Topics: Two Pointers, String

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
