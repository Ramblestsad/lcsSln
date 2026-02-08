namespace Scratch.Labuladong.Algorithms.NumberOfStudentsUnableToEatLunch;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int CountStudents(int[] students, int[] sandwiches)
    {
        // 1、剩下的所有学生都想吃 1，但栈顶是 0。
        // 2、剩下的所有学生都想吃 0，但栈顶是 1。

        var typeCount = new int[2];
        foreach (var st in students)
        {
            typeCount[st]++;
        }

        foreach (var sanT in sandwiches)
        {
            if (typeCount[sanT] == 0) return typeCount[0] + typeCount[1];
            typeCount[sanT]--;
        }

        return 0;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
