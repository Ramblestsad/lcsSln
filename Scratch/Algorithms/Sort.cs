namespace Scratch.Algorithms;

public static class Sort {
    public static void SelectionSort(int[] nums) {
        var n = nums.Length;
        // 这里i < n-1 是因为，选择排序到最后一轮，n-1位置（即最后一个数组自动是最大的）
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
                    ( nums[j + 1], nums[j] ) = ( nums[j], nums[j + 1] );
                    swap = true;
                }
            }

            if (!swap) break;
        }
    }

    public static void InsertionSort(int[] nums) {
        var n = nums.Length;
        // 维护 [0, sortedIndex) 是有序数组
        var sortedIndex = 0;
        while (sortedIndex < n) {
            // 将 nums[sortedIndex] 插入到有序数组 [0, sortedIndex) 中
            for (var i = sortedIndex; i > 0; i--) {
                if (nums[i] < nums[i - 1]) {
                    ( nums[i], nums[i - 1] ) = ( nums[i - 1], nums[i] );
                } else {
                    break;
                }
            }

            sortedIndex++;
        }
    }

    public static void ShellSort(int[] nums) {
        // 希尔排序，对 h 有序数组进行插入排序
        // 逐渐缩小 h，最后 h=1 时，完成整个数组的排序
        // 确保初始间隔不会太大，至少能将数组分成3个有意义的子序列
        // 这是 Knuth 序列的经验性选择，在实践中表现良好
        // 后续流程： 希尔排序会使用 h = 13 → 4 → 1 的顺序进行多轮插入排序，最终完成整个数组的排序

        var n = nums.Length;
        // 生成函数 (3^k - 1) / 2 即 h = 1, 4, 13, 40, 121, 364...
        var h = 1;
        while (h < n / 3) {
            h = 3 * h + 1;
        }

        // 36 27 20 60 55 7 28 36 67 44 16
        // 0  1  2  3  4  5 6  7  8  9  10
        //             h
        //             |sortedIndex......|
        // i-h         |i................|
        // n = 11 n / 3 = 3
        // initial h = 4, 即分为以下四组进行组内排序
        // 0 4 8
        // 1 5 9
        // 2 6 10
        // 3 7

        // 改动一，把插入排序的主要逻辑套在 h 的 while 循环中
        while (h >= 1) {
            // 改动二，bas 初始化为 h，而不是 1
            var sortedIndex = h;
            while (sortedIndex < n) {
                // 改动三，把比较和交换元素的步长设置为 h，而不是相邻元素
                // i>=h 就可以做到，下一个比较的数是组内的后一个数
                // i 减去一个h，还至少有一个h，也就是组内下一个数
                for (var i = sortedIndex; i >= h; i -= h) {
                    if (nums[i] < nums[i - h]) {
                        ( nums[i], nums[i - h] ) = ( nums[i - h], nums[i] );
                    } else {
                        break;
                    }
                }

                sortedIndex++;
            }

            h /= 3;
        }
    }

    static int Partition(int[] nums, int left, int right) {
        var pivot = nums[left];
        var i = left + 1;
        var j = right;
        while (i <= j) {
            // 移动到第一个大于pivot的数
            while (i <= j && nums[i] <= pivot) {
                i++;
            }

            // 移动到第一个小于pivot的数
            while (j >= i && nums[j] >= pivot) {
                j--;
            }

            // 已经交错，说明本身有序
            if (i >= j) {
                break;
            }

            // 将第一个大于pivot的数和第一个小于pivot的数交换位置
            // 交换之后进入下一轮迭代，持续交换直到边界点
            ( nums[i], nums[j] ) = ( nums[j], nums[i] );
        }

        // pivot与边界线交换
        ( nums[left], nums[j] ) = ( nums[j], nums[left] );
        return j;
    }

    public static void QuickSort(int[] nums, int left, int right) {
        if (left >= right) {
            return;
        }

        // ****** 前序位置 ******
        // 对 nums[left..right] 进行切分，将 nums[p] 排好序
        // 使得 nums[left..p-1] <= nums[p] < nums[p+1..right]
        var p = Partition(nums, left, right);

        // 去左右子数组进行切分
        QuickSort(nums, left, p - 1);
        QuickSort(nums, p + 1, right);
    }

    /// <summary>
    /// 递归深度优化
    /// </summary>
    public static void QuickSortRecursionOpt(int[] nums, int left, int right) {
        // 子数组长度为 1 时终止
        while (left < right) {
            // 哨兵划分操作
            var pivot = Partition(nums, left, right);
            // 对两个子数组中较短的那个执行快速排序
            // 递归调用会占用栈空间，处理较短的子数组可以更快地完成递归，释放栈空间
            // 较长的子数组通过修改边界值在下次循环中处理，避免了额外的递归调用
            if (pivot - left < right - pivot) {
                // 左子数组较短，递归处理它
                QuickSort(nums, left, pivot - 1);
                left = pivot + 1; // 下次循环处理右子数组
            } else {
                // 右子数组较短，递归处理它
                QuickSort(nums, pivot + 1, right);
                right = pivot - 1; // 下次循环处理左子数组
            }
        }
    }

    public static void MergeSort(int[] nums, int left, int right) {
    }

    public static void Merge(int[] nums, int left, int mid, int right) {
    }
}
