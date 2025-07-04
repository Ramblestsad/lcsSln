namespace Scratch.Algorithms;

public static class Search {
    public static int? BinarySearch(List<int> nums, int target) {
        // 初始化双闭区间 [0, n-1] ，即 i, j 分别指向数组首元素、尾元素
        int i = 0, j = nums.Count - 1;
        // 循环，当搜索区间为空时跳出（当 i > j 时为空）
        while (i <= j) {
            var m = i + ( j - i ) / 2; // 计算中点索引 m，这么算防止overflow
            if (nums[m] < target)
                i = m + 1;
            else if (nums[m] > target)
                j = m - 1;
            else
                return m;
        }

        return null;
    }

    /* 二分查找插入点（存在重复元素） */
    public static int BinarySearchInsertion(int[] nums, int target) {
        int i = 0, j = nums.Length - 1;
        while (i <= j) {
            var m = i + ( j - i ) / 2;
            if (nums[m] < target) {
                i = m + 1;
            }
            else if (nums[m] > target) {
                j = m - 1;
            }
            else {
                j = m - 1; // 首个小于 target 的元素在区间 [i, m-1] 中
            }
        }

        return i;
    }

    /* 二分查找最左一个 target */
    public static int BinarySearchLeftEdge(int[] nums, int target) {
        // 等价于查找 target 的插入点
        var i = Search.BinarySearchInsertion(nums, target);

        // 未找到 target，返回 -1
        // i == nums.Length 说明i越界，target大于数组所有值
        if (i == nums.Length || nums[i] != target) {
            return -1;
        }

        return i;
    }

    /* 二分查找最右一个 target */
    public static int BinarySearchRightEdge(int[] nums, int target) {
        // 转化为查找最左一个 (target + 1)
        // 因为是有序数组，所以最左一个 （target + 1)就是在最右target的右边一个
        var i = Search.BinarySearchInsertion(nums, target + 1);
        // j 指向最右一个 target ，i 指向首个大于 target 的元素
        var j = i - 1;
        // 未找到 target ，返回 -1
        // j == -1 意味着 i = 0 即target + 1已经是数组最小数，处于索引0。没有target
        if (j == -1 || nums[j] != target) {
            return -1;
        }

        return j;
    }
}
