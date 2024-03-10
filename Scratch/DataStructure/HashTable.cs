/* 键值对 int->string */
public class Pair(int key, string val)
{
    public int key = key;
    public string val = val;
}

/* 基于数组实现的哈希表 */
public class ArrayHashMap
{
    List<Pair?> buckets;
    public ArrayHashMap()
    {
        // 初始化数组，包含 100 个桶
        buckets = [];
        for (int i = 0; i < 100; i++)
        {
            buckets.Add(null);
        }
    }

    /* 哈希函数 */
    int HashFunc(int key)
    {
        int index = key % 100;
        return index;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        int index = HashFunc(key);
        Pair? pair = buckets[index];
        if (pair == null) return null;
        return pair.val;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        Pair pair = new(key, val);
        int index = HashFunc(key);
        buckets[index] = pair;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        int index = HashFunc(key);
        // 置为 null ，代表删除
        buckets[index] = null;
    }

    /* 获取所有键值对 */
    public List<Pair> PairSet()
    {
        List<Pair> pairSet = [];
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                pairSet.Add(pair);
        }
        return pairSet;
    }

    /* 获取所有键 */
    public List<int> KeySet()
    {
        List<int> keySet = [];
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                keySet.Add(pair.key);
        }
        return keySet;
    }

    /* 获取所有值 */
    public List<string> ValueSet()
    {
        List<string> valueSet = [];
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                valueSet.Add(pair.val);
        }
        return valueSet;
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (Pair kv in PairSet())
        {
            Console.WriteLine(kv.key + " -> " + kv.val);
        }
    }
}

/* 链式地址哈希表 */
public class HashMapChaining
{
    int size; // 键值对数量
    int capacity; // 哈希表容量
    double loadThres; // 触发扩容的负载因子阈值
    int extendRatio; // 扩容倍数
    List<List<Pair>> buckets; // 桶数组

    /* 构造方法 */
    public HashMapChaining()
    {
        size = 0;
        capacity = 4;
        loadThres = 2.0 / 3.0;
        extendRatio = 2;
        buckets = new List<List<Pair>>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            buckets.Add([]);
        }
    }

    /* 哈希函数 */
    int HashFunc(int key)
    {
        return key % capacity;
    }

    /* 负载因子 */
    double LoadFactor()
    {
        return (double)size / capacity;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        int index = HashFunc(key);
        // 遍历桶，若找到 key ，则返回对应 val
        foreach (Pair pair in buckets[index])
        {
            if (pair.key == key)
            {
                return pair.val;
            }
        }
        // 若未找到 key ，则返回 null
        return null;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        // 当负载因子超过阈值时，执行扩容
        if (LoadFactor() > loadThres)
        {
            Extend();
        }
        int index = HashFunc(key);
        // 遍历桶，若遇到指定 key ，则更新对应 val 并返回
        foreach (Pair pair in buckets[index])
        {
            if (pair.key == key)
            {
                pair.val = val;
                return;
            }
        }
        // 若无该 key ，则将键值对添加至尾部
        buckets[index].Add(new Pair(key, val));
        size++;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        int index = HashFunc(key);
        // 遍历桶，从中删除键值对
        foreach (Pair pair in buckets[index].ToList())
        {
            if (pair.key == key)
            {
                buckets[index].Remove(pair);
                size--;
                break;
            }
        }
    }

    /* 扩容哈希表 */
    void Extend()
    {
        // 暂存原哈希表
        List<List<Pair>> bucketsTmp = buckets;
        // 初始化扩容后的新哈希表
        capacity *= extendRatio;
        buckets = new List<List<Pair>>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            buckets.Add([]);
        }
        size = 0;
        // 将键值对从原哈希表搬运至新哈希表
        foreach (List<Pair> bucket in bucketsTmp)
        {
            foreach (Pair pair in bucket)
            {
                Put(pair.key, pair.val);
            }
        }
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (List<Pair> bucket in buckets)
        {
            List<string> res = [];
            foreach (Pair pair in bucket)
            {
                res.Add(pair.key + " -> " + pair.val);
            }
            foreach (string kv in res)
            {
                Console.WriteLine(kv);
            }
        }
    }
}

/* 开放寻址哈希表 */
public class HashMapOpenAddressing
{
    int size; // 键值对数量
    int capacity = 4; // 哈希表容量
    double loadThres = 2.0 / 3.0; // 触发扩容的负载因子阈值
    int extendRatio = 2; // 扩容倍数
    Pair[] buckets; // 桶数组
    Pair TOMBSTONE = new(-1, "-1"); // 删除标记

    /* 构造方法 */
    public HashMapOpenAddressing()
    {
        size = 0;
        buckets = new Pair[capacity];
    }

    /* 哈希函数 */
    int HashFunc(int key)
    {
        return key % capacity;
    }

    /* 负载因子 */
    double LoadFactor()
    {
        return (double)size / capacity;
    }

    /* 搜索 key 对应的桶索引 */
    int FindBucket(int key)
    {
        int index = HashFunc(key);
        int firstTombstone = -1;
        // 线性探测，当遇到空桶时跳出
        while (buckets[index] != null)
        {
            // 若遇到 key ，返回对应的桶索引
            if (buckets[index].key == key)
            {
                // 若之前遇到了删除标记，则将键值对移动至该索引处
                if (firstTombstone != -1)
                {
                    buckets[firstTombstone] = buckets[index];
                    buckets[index] = TOMBSTONE;
                    return firstTombstone; // 返回移动后的桶索引
                }
                return index; // 返回桶索引
            }
            // 记录遇到的首个删除标记
            if (firstTombstone == -1 && buckets[index] == TOMBSTONE)
            {
                firstTombstone = index;
            }
            // 计算桶索引，越过尾部则返回头部
            index = ( index + 1 ) % capacity;
        }
        // 若 key 不存在，则返回添加点的索引
        return firstTombstone == -1 ? index : firstTombstone;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        // 搜索 key 对应的桶索引
        int index = FindBucket(key);
        // 若找到键值对，则返回对应 val
        if (buckets[index] != null && buckets[index] != TOMBSTONE)
        {
            return buckets[index].val;
        }
        // 若键值对不存在，则返回 null
        return null;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        // 当负载因子超过阈值时，执行扩容
        if (LoadFactor() > loadThres)
        {
            Extend();
        }
        // 搜索 key 对应的桶索引
        int index = FindBucket(key);
        // 若找到键值对，则覆盖 val 并返回
        if (buckets[index] != null && buckets[index] != TOMBSTONE)
        {
            buckets[index].val = val;
            return;
        }
        // 若键值对不存在，则添加该键值对
        buckets[index] = new Pair(key, val);
        size++;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        // 搜索 key 对应的桶索引
        int index = FindBucket(key);
        // 若找到键值对，则用删除标记覆盖它
        if (buckets[index] != null && buckets[index] != TOMBSTONE)
        {
            buckets[index] = TOMBSTONE;
            size--;
        }
    }

    /* 扩容哈希表 */
    void Extend()
    {
        // 暂存原哈希表
        Pair[] bucketsTmp = buckets;
        // 初始化扩容后的新哈希表
        capacity *= extendRatio;
        buckets = new Pair[capacity];
        size = 0;
        // 将键值对从原哈希表搬运至新哈希表
        foreach (Pair pair in bucketsTmp)
        {
            if (pair != null && pair != TOMBSTONE)
            {
                Put(pair.key, pair.val);
            }
        }
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (Pair pair in buckets)
        {
            if (pair == null)
            {
                Console.WriteLine("null");
            }
            else if (pair == TOMBSTONE)
            {
                Console.WriteLine("TOMBSTONE");
            }
            else
            {
                Console.WriteLine(pair.key + " -> " + pair.val);
            }
        }
    }
}

/*  平方探测
    平方探测与线性探测类似，都是开放寻址的常见策略之一。
    当发生冲突时，平方探测不是简单地跳过一个固定的步数，而是跳过“探测次数的平方”的步数，
    即1、4、9、16...步。
*/

/* 多次哈希
   顾名思义，多次哈希方法使用多个哈希函数`f1(x)` `f2(x)` `f3(x)`...进行探测。

   插入元素：若哈希函数f1(x)出现冲突，则尝试f2(2)，以此类推，直到找到空位后插入元素。
   查找元素：在相同的哈希函数顺序下进行查找，直到找到目标元素时返回；
            若遇到空位或已尝试所有哈希函数，说明哈希表中不存在该元素，则返回 None 。
   与线性探测相比，多次哈希方法不易产生聚集，但多个哈希函数会带来额外的计算量。
*/

/*
    几个简单的哈希算法。
    使用大质数作为模数，可以最大化地保证哈希值的均匀分布。
 */
/* 加法哈希 */
public class SimpleHashAlgorithms
{
    public int AddHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = ( hash + c ) % MODULUS;
        }
        return (int)hash;
    }

    /* 乘法哈希 */
    public int MulHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = ( 31 * hash + c ) % MODULUS;
        }
        return (int)hash;
    }

    /* 异或哈希 */
    public int XorHash(string key)
    {
        int hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash ^= c;
        }
        return hash & MODULUS;
    }

    /* 旋转哈希 */
    public int RotHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = ( ( hash << 4 ) ^ ( hash >> 28 ) ^ c ) % MODULUS;
        }
        return (int)hash;
    }
}
