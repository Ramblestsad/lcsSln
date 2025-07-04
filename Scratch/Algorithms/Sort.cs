namespace Scratch.Algorithms;

public static class Sort {
    public static void SelectionSort(int[] nums) {
        var n = nums.Length;
        for (var i = 0; i < n - 1; i++) {
            var minIndex = i;
            for (var j = i + 1; j < n; j++) {
                if (nums[j] < nums[minIndex]) minIndex = j;
            }

            ( nums[minIndex], nums[i] ) = ( nums[i], nums[minIndex] );
        }
    }

    public static void BubbleSort(int[] nums) {
        var n = nums.Length;
        // 外循环：未排序区间为 [0, i]
        // 反方向迭代是因为：每次内循环后，最大元素会"冒泡"到未排序区间的末尾
        // i > 0是因为：当 i = 1 时，内循环只比较 nums[0] 和 nums[1],i = 0时，无需比较
        for (var i = n - 1; i > 0; i--) {
            var swap = false;
            for (var j = 0; j < i; j++) {
                if (nums[j] > nums[j + 1]) {
                    (nums[j + 1], nums[j]) = (nums[j], nums[j + 1]);
                    swap = true;
                }
            }
            if (!swap) break;
        }
    }

    public static void InsertionSort(int[] nums) {
        // 外循环：已排序区间为 [0, i-1]
        for (var i = 1; i < nums.Length; i++) {
            int bas = nums[i], j = i - 1;
            // 内循环：将 base 插入到已排序区间 [0, i-1] 中的正确位置
            // 将base值即nums[i]与i之前的元素进行比较，大于base的全部右移
            // 此时j指向第一个<=base的值
            // 最后再替换j+1的值
            while (j >= 0 && nums[j] > bas) {
                nums[j + 1] = nums[j]; // 将 nums[j] 向右移动一位
                j--;
            }
            nums[j + 1] = bas;
        }
    }
}
