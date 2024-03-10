namespace Scratch.DataStructure;

public class ArrayUtils
{
    /* 随机访问元素 */
    public static int RandomAccess(int[] nums)
    {
        Random random = new();
        // 在区间 [0, nums.Length) 中随机抽取一个数字
        int randomIndex = random.Next(nums.Length);
        // 获取并返回随机元素
        int randomNum = nums[randomIndex];
        return randomNum;
    }

    /* 在数组的索引 index 处插入元素 num */
    public static void Insert(int[] nums, int num, int index)
    {
        // 把索引 index 以及之后的所有元素向后移动一位
        for (int i = nums.Length - 1; i > index; i--)
        {
            nums[i] = nums[i - 1];
        }
        // 将 num 赋给 index 处的元素
        nums[index] = num;
    }

    /* 删除索引 index 处的元素 */
    public static void Remove(int[] nums, int index)
    {
        // 把索引 index 之后的所有元素向前移动一位
        for (int i = index; i < nums.Length - 1; i++)
        {
            nums[i] = nums[i + 1];
        }
    }

    /* 遍历数组 */
    public static void Traverse(int[] nums)
    {
        int count = 0;
        // 通过索引遍历数组
        for (int i = 0; i < nums.Length; i++)
        {
            count += nums[i];
        }
        // 直接遍历数组元素
        foreach (int num in nums)
        {
            count += num;
        }
    }

    /* 在数组中查找指定元素 */
    public static int Find(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == target)
                return i;
        }
        return -1;
    }

    /* 扩展数组长度 */
    public static int[] Extend(int[] nums, int enlarge)
    {
        // 初始化一个扩展长度后的数组
        int[] res = new int[nums.Length + enlarge];
        // 将原数组中的所有元素复制到新数组
        for (int i = 0; i < nums.Length; i++)
        {
            res[i] = nums[i];
        }
        // 返回扩展后的新数组
        return res;
    }
}
