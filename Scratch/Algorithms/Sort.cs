namespace Scratch.Algorithms;

public static class Sort
{
    public static void SelectionSort(int[] nums)
    {
        var n = nums.Length;
        // 这里i < n-1 是因为，选择排序到最后一轮，n-1位置（即最后一个数组自动是最大的）
        for (var i = 0; i < n - 1; i++)
        {
            var minIndex = i;
            for (var j = i + 1; j < n; j++)
            {
                if (nums[j] < nums[minIndex]) minIndex = j;
            }

            ( nums[minIndex], nums[i] ) = ( nums[i], nums[minIndex] );
        }
    }

    public static void BubbleSort(int[] nums)
    {
        var n = nums.Length;
        // 外循环：未排序区间为 [0, i]
        // 反方向迭代是因为：每次内循环后，最大元素会"冒泡"到未排序区间的末尾
        // i > 0是因为：当 i = 1 时，内循环只比较 nums[0] 和 nums[1],i = 0时，无需比较
        for (var i = n - 1; i > 0; i--)
        {
            var swap = false;
            for (var j = 0; j < i; j++)
            {
                if (nums[j] > nums[j + 1])
                {
                    ( nums[j + 1], nums[j] ) = ( nums[j], nums[j + 1] );
                    swap = true;
                }
            }

            if (!swap) break;
        }
    }

    public static void InsertionSort(int[] nums)
    {
        var n = nums.Length;
        // 维护 [0, sortedIndex) 是有序数组
        var sortedIndex = 0;
        while (sortedIndex < n)
        {
            // 将 nums[sortedIndex] 插入到有序数组 [0, sortedIndex) 中
            for (var i = sortedIndex; i > 0; i--)
            {
                if (nums[i] < nums[i - 1]) ( nums[i], nums[i - 1] ) = ( nums[i - 1], nums[i] );
                else break;
            }

            sortedIndex++;
        }
    }

    public static void ShellSort(int[] nums)
    {
        // 希尔排序，对 h 有序数组进行插入排序
        // 逐渐缩小 h，最后 h=1 时，完成整个数组的排序
        // 确保初始间隔不会太大，至少能将数组分成3个有意义的子序列
        // 这是 Knuth 序列的经验性选择，在实践中表现良好
        // 后续流程： 希尔排序会使用 h = 13 → 4 → 1 的顺序进行多轮插入排序，最终完成整个数组的排序

        var n = nums.Length;
        // 生成函数 (3^k - 1) / 2 即 h = 1, 4, 13, 40, 121, 364...
        var h = 1;
        while (h < n / 3)
        {
            h = 3 * h + 1;
        }

        // 36 27 20 60 55 7 28 36 67 44 16
        // 0  1  2  3  4  5 6  7  8  9  10
        // i-h         h(i)
        //             sortedIndex ---->
        //    i-h         i
        //       i-h        i
        //          i-j        i
        //             i-h        i
        //                i-h        i
        //                  i-h         i
        // initial h = 4, 即分为以下四组进行组内排序
        // 0 4 8:   36 x x  x  55 x  x  x  67 x   x
        // 1 5 9:   x  7 x  x  x  27 x  x  x  44  x
        // 2 6 10:  x  x 16 x  x  x  28 x  x  x   28
        // 3 7:     x  x x  36 x  x  x  60 x  x   x

        // 改动一，把插入排序的主要逻辑套在 h 的 while 循环中
        while (h >= 1)
        {
            // 改动二，bas 初始化为 h，而不是 1
            var sortedIndex = h;
            while (sortedIndex < n)
            {
                // 改动三，把比较和交换元素的步长设置为 h，而不是相邻元素
                // i>=h 就可以做到，下一个比较的数是组内的后一个数
                // i 减去一个h，还至少有一个h，也就是组内下一个数
                for (var i = sortedIndex; i >= h; i -= h)
                {
                    if (nums[i] < nums[i - h])
                    {
                        ( nums[i], nums[i - h] ) = ( nums[i - h], nums[i] );
                    }
                    else
                    {
                        break;
                    }
                }

                sortedIndex++;
            }

            h /= 3;
        }
    }

    public static void QuickSort(int[] nums, int left, int right)
    {
        if (left >= right) return;

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
    public static void QuickSortRecursionOpt(int[] nums, int left, int right)
    {
        // 子数组长度为 1 时终止
        while (left < right)
        {
            // 哨兵划分操作
            var pivot = Partition(nums, left, right);
            // 对两个子数组中较短的那个执行快速排序
            // 递归调用会占用栈空间，处理较短的子数组可以更快地完成递归，释放栈空间
            // 较长的子数组通过修改边界值在下次循环中处理，避免了额外的递归调用
            if (pivot - left < right - pivot)
            {
                // 左子数组较短，递归处理它
                QuickSort(nums, left, pivot - 1);
                left = pivot + 1; // 下次循环处理右子数组
            }
            else
            {
                // 右子数组较短，递归处理它
                QuickSort(nums, pivot + 1, right);
                right = pivot - 1; // 下次循环处理左子数组
            }
        }
    }

    static int Partition(int[] nums, int left, int right)
    {
        var pivot = nums[left];
        var i = left + 1;
        var j = right;
        while (i <= j)
        {
            // 移动到第一个大于pivot的数
            while (i <= j && nums[i] <= pivot)
            {
                i++;
            }

            // 移动到第一个小于pivot的数
            while (j >= i && nums[j] >= pivot)
            {
                j--;
            }

            // 已经交错，说明本身有序
            if (i >= j)
            {
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

    public static void MergeSort(int[] nums, int left, int right)
    {
        // 终止条件：当子数组长度为 1 时终止递归
        if (left >= right) return;
        // 划分阶段
        var mid = left + ( right - left ) / 2; // 计算中点（防止overflow)
        MergeSort(nums, left, mid); // 递归左子数组
        MergeSort(nums, mid + 1, right); // 递归右子数组
        // 合并阶段
        Merge(nums, left, mid, right);
    }

    static void Merge(int[] nums, int left, int mid, int right)
    {
        // 左子数组区间为 [left, mid], 右子数组区间为 [mid+1, right]
        // 创建一个临时数组 tmp ，用于存放合并后的结果
        var tmp = new int[right - left + 1];
        // 初始化左子数组和右子数组的起始索引
        int i = left, j = mid + 1, k = 0;
        // 当左右子数组都还有元素时，进行比较并将较小的元素复制到临时数组中
        while (i <= mid && j <= right)
        {
            if (nums[i] <= nums[j])
            {
                tmp[k] = nums[i];
                i++;
            }
            else
            {
                tmp[k] = nums[j];
                j++;
            }

            k++;
        }

        // 将左子数组和右子数组的剩余元素复制到临时数组中
        while (i <= mid)
        {
            tmp[k] = nums[i];
            i++;
            k++;
        }

        while (j <= right)
        {
            tmp[k] = nums[j];
            j++;
            k++;
        }

        // 将临时数组 tmp 中的元素复制回原数组 nums 的对应区间
        for (k = 0; k < tmp.Length; k++)
        {
            nums[left + k] = tmp[k];
        }
    }

    public static void HeapSort(int[] nums)
    {
        // 建堆操作：堆化除叶节点以外的其他所有节点
        // 节点 i 的父节点：(i - 1) / 2
        // 最后一个元素索引：n - 1
        // 最后一个元素的父节点索引：(n - 1 - 1) / 2 = (n - 2) / 2 = n / 2 - 1
        // 所以 nums.Length / 2 - 1 是数组中最后一个非叶节点的索引
        for (var i = nums.Length / 2 - 1; i >= 0; i--)
        {
            SiftDown(nums, nums.Length, i);
        }

        // 从堆中提取最大元素，循环 n-1 轮
        for (var i = nums.Length - 1; i > 0; i--)
        {
            // 交换根节点与最右叶节点（交换首元素与尾元素）
            ( nums[i], nums[0] ) = ( nums[0], nums[i] );
            // 以根节点为起点，从顶至底进行堆化
            // 这里的 n 是当前堆的长度，逐渐缩小，因为交换后，最大元素已经在正确位置
            SiftDown(nums, i, 0);
        }
    }

    static void SiftDown(int[] nums, int n, int i)
    {
        // 堆的长度为 n ，从节点 i 开始，从顶至底堆化
        while (true)
        {
            // 判断节点 i, l, r 中值最大的节点，记为 ma
            var l = 2 * i + 1;
            var r = 2 * i + 2;
            var ma = i;
            if (l < n && nums[l] > nums[ma])
                ma = l;
            if (r < n && nums[r] > nums[ma])
                ma = r;
            // 若节点 i 最大或索引 l, r 越界，则无须继续堆化，跳出
            if (ma == i)
                break;
            // 交换两节点
            ( nums[ma], nums[i] ) = ( nums[i], nums[ma] );
            // 循环向下堆化
            i = ma;
        }
    }

    public static void BucketSort(float[] nums)
    {
        // 初始化 k = n/2 个桶，预期向每个桶分配 2 个元素
        if (nums.Length < 2) return; // 数组长度小于2时无需排序
        var k = nums.Length / 2;
        List<List<float>> buckets = [];
        for (var i = 0; i < k; i++)
        {
            buckets.Add([]);
        }

        // 1. 将数组元素分配到各个桶中
        foreach (var num in nums)
        {
            // 输入数据范围为 [0, 1)，使用 num * k 映射到索引范围 [0, k-1]
            var i = (int)( num * k );
            // 将 num 添加进桶 i
            buckets[i].Add(num);
        }

        // 2. 对各个桶执行排序
        foreach (List<float> bucket in buckets)
        {
            // 使用内置排序函数，也可以替换成其他排序算法
            bucket.Sort();
        }

        // 3. 遍历桶合并结果
        var j = 0;
        foreach (List<float> bucket in buckets)
        {
            foreach (var num in bucket)
            {
                nums[j] = num;
                j++;
            }
        }
    }

    public static void CountingSortNaive(int[] nums)
    {
        // 1. 统计数组最大元素 m
        var m = 0;
        foreach (var num in nums)
        {
            m = Math.Max(m, num);
        }

        // 2. 统计各数字的出现次数
        // counter[num] 代表 num 的出现次数
        var counter = new int[m + 1];
        foreach (var num in nums)
        {
            counter[num]++;
        }

        // 3. 遍历 counter ，将各元素填入原数组 nums
        var i = 0;
        for (var num = 0; num < m + 1; num++)
        {
            for (var j = 1; j <= counter[num]; j++)
            {
                nums[i] = num;
                i++;
            }
        }
    }

    public static void CountingSort(int[] nums)
    {
        // 1. 统计数组最大元素 m
        var m = 0;
        foreach (var num in nums)
        {
            m = Math.Max(m, num);
        }

        // 2. 统计各数字的出现次数
        // counter[num] 代表 num 的出现次数
        var counter = new int[m + 1];
        foreach (var num in nums)
        {
            counter[num]++;
        }

        // 3. 求 counter 的前缀和，将“出现次数”转换为“尾索引”
        // 即 counter[num]-1 是 num 在 res 中最后一次出现的索引
        for (var i = 0; i < m; i++)
        {
            counter[i + 1] += counter[i];
        }

        // 4. 倒序遍历 nums ，将各元素填入结果数组 res
        // 初始化数组 res 用于记录结果
        var n = nums.Length;
        var res = new int[n];
        for (var i = n - 1; i >= 0; i--)
        {
            var num = nums[i];
            res[counter[num] - 1] = num; // 将 num 放置到对应索引处
            counter[num]--; // 令前缀和自减 1 ，得到下次放置想通 num 的索引
        }

        // 使用结果数组 res 覆盖原数组 nums
        for (var i = 0; i < n; i++)
        {
            nums[i] = res[i];
        }
    }

    static int Digit(int num, int exp)
    {
        // 传入 exp 而非 k 可以避免在此重复执行昂贵的次方计算
        return ( num / exp ) % 10;
    }

    static void CountingSortDigit(int[] nums, int exp)
    {
        // 十进制的位范围为 0~9 ，相当于 m = 9 因此需要长度为 10 = m+1 的桶数组
        var counter = new int[10];
        var n = nums.Length;
        // 统计 0~9 各数字的出现次数
        for (var i = 0; i < n; i++)
        {
            var d = Digit(nums[i], exp); // 获取 nums[i] 第 k 位，记为 d
            counter[d]++; // 统计数字 d 的出现次数
        }

        // 求前缀和，将“出现个数”转换为“数组索引”
        for (var i = 1; i < 10; i++)
        {
            counter[i] += counter[i - 1];
        }

        // 倒序遍历，根据桶内统计结果，将各元素填入 res
        var res = new int[n];
        for (var i = n - 1; i >= 0; i--)
        {
            var d = Digit(nums[i], exp);
            var j = counter[d] - 1; // 获取 d 在结果数组中的索引 j
            res[j] = nums[i]; // 将当前元素填入索引 j
            counter[d]--; // 将 d 的数量减 1
        }

        // 使用结果覆盖原数组 nums
        for (var i = 0; i < n; i++)
        {
            nums[i] = res[i];
        }
    }

    public static void RadixSort(int[] nums)
    {
        // 获取数组的最大元素，用于判断最大位数
        var m = int.MinValue;
        foreach (var num in nums)
        {
            if (num > m) m = num;
        }

        // 按照从低位到高位的顺序遍历
        for (var exp = 1; exp <= m; exp *= 10)
        {
            // 对数组元素的第 k 位执行计数排序
            // k = 1 -> exp = 1
            // k = 2 -> exp = 10
            // 即 exp = 10^(k-1)
            CountingSortDigit(nums, exp);
        }
    }
}
