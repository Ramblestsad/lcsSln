namespace Scratch.Labuladong.Algorithms.CourseSchedule;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        // 什么时候无法修完所有课程？当存在循环依赖的时候
        // 看到依赖问题，首先想到的就是把问题转化成「有向图」这种数据结构
        // 只要图中存在环，那就说明存在循环依赖

        // 把课程看成「有向图」中的节点，节点编号分别是 0, 1, ..., numCourses-1
        // 把课程之间的依赖关系看做节点之间的有向边

        // 首先要把题目的输入转化成一幅有向图，然后再判断图中是否存在环

        return false;
    }

    private List<int>[] _buildG(int numCourses, int[][] prerequisites)
    {
        throw new NotFiniteNumberException();
    }

    public bool CanFinishDfs(int numCourses, int[][] prerequisites)
    {
        return false;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
