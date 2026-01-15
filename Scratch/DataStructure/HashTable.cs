namespace Scratch.DataStructure;

public class Pair(int key, string val)
{
    public int Key = key;
    public string Val = val;
}

/* Hashtable based on array */
public class ArrayHashMap
{
    List<Pair?> _buckets = [];

    public ArrayHashMap()
    {
        // 初始化数组，包含 100 个桶
        for (var i = 0; i < 100; i++)
        {
            _buckets.Add(null);
        }
    }

    /* 哈希函数 */
    static int HashFunc(int key)
    {
        var index = key % 100;
        return index;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        var index = HashFunc(key);
        Pair? pair = _buckets[index];
        if (pair == null) return null;
        return pair.Val;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        Pair pair = new(key, val);
        var index = HashFunc(key);
        _buckets[index] = pair;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        var index = HashFunc(key);
        // 置为 null ，代表删除
        _buckets[index] = null;
    }

    /* 获取所有键值对 */
    public List<Pair> PairSet()
    {
        List<Pair> pairSet = [];
        foreach (Pair? pair in _buckets)
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
        foreach (Pair? pair in _buckets)
        {
            if (pair != null)
                keySet.Add(pair.Key);
        }

        return keySet;
    }

    /* 获取所有值 */
    public List<string> ValueSet()
    {
        List<string> valueSet = [];
        foreach (Pair? pair in _buckets)
        {
            if (pair != null)
                valueSet.Add(pair.Val);
        }

        return valueSet;
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (Pair kv in PairSet())
        {
            Console.WriteLine(kv.Key + " -> " + kv.Val);
        }
    }
}

/* 链式地址哈希表 */
public class HashMapChaining
{
    List<List<Pair>> _buckets; // 桶数组
    int _capacity; // 哈希表容量
    int _extendRatio; // 扩容倍数
    double _loadThres; // 触发扩容的负载因子阈值
    int _size; // 键值对数量

    /* 构造方法 */
    public HashMapChaining()
    {
        _size = 0;
        _capacity = 4;
        _loadThres = 2.0 / 3.0;
        _extendRatio = 2;
        _buckets = new List<List<Pair>>(_capacity);
        for (var i = 0; i < _capacity; i++)
        {
            _buckets.Add([]);
        }
    }

    /* 哈希函数 */
    int HashFunc(int key)
    {
        return key % _capacity;
    }

    /* 负载因子 */
    double LoadFactor()
    {
        return (double)_size / _capacity;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        var index = HashFunc(key);
        // 遍历桶，若找到 key ，则返回对应 val
        foreach (Pair pair in _buckets[index])
        {
            if (pair.Key == key)
            {
                return pair.Val;
            }
        }

        // 若未找到 key ，则返回 null
        return null;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        // 当负载因子超过阈值时，执行扩容
        if (LoadFactor() > _loadThres)
        {
            Extend();
        }

        var index = HashFunc(key);
        // 遍历桶，若遇到指定 key ，则更新对应 val 并返回
        foreach (Pair pair in _buckets[index])
        {
            if (pair.Key == key)
            {
                pair.Val = val;
                return;
            }
        }

        // 若无该 key ，则将键值对添加至尾部
        _buckets[index].Add(new Pair(key, val));
        _size++;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        var index = HashFunc(key);
        // 遍历桶，从中删除键值对
        foreach (Pair pair in _buckets[index].ToList())
        {
            if (pair.Key == key)
            {
                _buckets[index].Remove(pair);
                _size--;
                break;
            }
        }
    }

    /* 扩容哈希表 */
    void Extend()
    {
        // 暂存原哈希表
        List<List<Pair>> bucketsTmp = _buckets;
        // 初始化扩容后的新哈希表
        _capacity *= _extendRatio;
        _buckets = new List<List<Pair>>(_capacity);
        for (var i = 0; i < _capacity; i++)
        {
            _buckets.Add([]);
        }

        _size = 0;
        // 将键值对从原哈希表搬运至新哈希表
        foreach (List<Pair> bucket in bucketsTmp)
        {
            foreach (Pair pair in bucket)
            {
                Put(pair.Key, pair.Val);
            }
        }
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (List<Pair> bucket in _buckets)
        {
            List<string> res = [];
            foreach (Pair pair in bucket)
            {
                res.Add(pair.Key + " -> " + pair.Val);
            }

            foreach (var kv in res)
            {
                Console.WriteLine(kv);
            }
        }
    }
}

/* 开放寻址哈希表 */
public class HashMapOpenAddressing
{
    Pair[] _buckets; // 桶数组
    int _capacity = 4; // 哈希表容量
    int _extendRatio = 2; // 扩容倍数
    double _loadThres = 2.0 / 3.0; // 触发扩容的负载因子阈值
    int _size; // 键值对数量
    Pair _tombstone = new(-1, "-1"); // 删除标记

    /* 构造方法 */
    public HashMapOpenAddressing()
    {
        _size = 0;
        _buckets = new Pair[_capacity];
    }

    /* 哈希函数 */
    int HashFunc(int key)
    {
        return key % _capacity;
    }

    /* 负载因子 */
    double LoadFactor()
    {
        return (double)_size / _capacity;
    }

    /* 搜索 key 对应的桶索引 */
    int FindBucket(int key)
    {
        var index = HashFunc(key);
        var firstTombstone = -1;
        // 线性探测，当遇到空桶时跳出
        while (_buckets[index] != null)
        {
            // 若遇到 key ，返回对应的桶索引
            if (_buckets[index].Key == key)
            {
                // 若之前遇到了删除标记，则将键值对移动至该索引处
                if (firstTombstone != -1)
                {
                    _buckets[firstTombstone] = _buckets[index];
                    _buckets[index] = _tombstone;
                    return firstTombstone; // 返回移动后的桶索引
                }

                return index; // 返回桶索引
            }

            // 记录遇到的首个删除标记
            if (firstTombstone == -1 && _buckets[index] == _tombstone)
            {
                firstTombstone = index;
            }

            // 计算桶索引，越过尾部则返回头部
            index = ( index + 1 ) % _capacity;
        }

        // 若 key 不存在，则返回添加点的索引
        return firstTombstone == -1 ? index : firstTombstone;
    }

    /* 查询操作 */
    public string? Get(int key)
    {
        // 搜索 key 对应的桶索引
        var index = FindBucket(key);
        // 若找到键值对，则返回对应 val
        if (_buckets[index] != null && _buckets[index] != _tombstone)
        {
            return _buckets[index].Val;
        }

        // 若键值对不存在，则返回 null
        return null;
    }

    /* 添加操作 */
    public void Put(int key, string val)
    {
        // 当负载因子超过阈值时，执行扩容
        if (LoadFactor() > _loadThres)
        {
            Extend();
        }

        // 搜索 key 对应的桶索引
        var index = FindBucket(key);
        // 若找到键值对，则覆盖 val 并返回
        if (_buckets[index] != null && _buckets[index] != _tombstone)
        {
            _buckets[index].Val = val;
            return;
        }

        // 若键值对不存在，则添加该键值对
        _buckets[index] = new Pair(key, val);
        _size++;
    }

    /* 删除操作 */
    public void Remove(int key)
    {
        // 搜索 key 对应的桶索引
        var index = FindBucket(key);
        // 若找到键值对，则用删除标记覆盖它
        if (_buckets[index] != null && _buckets[index] != _tombstone)
        {
            _buckets[index] = _tombstone;
            _size--;
        }
    }

    /* 扩容哈希表 */
    void Extend()
    {
        // 暂存原哈希表
        Pair[] bucketsTmp = _buckets;
        // 初始化扩容后的新哈希表
        _capacity *= _extendRatio;
        _buckets = new Pair[_capacity];
        _size = 0;
        // 将键值对从原哈希表搬运至新哈希表
        foreach (Pair pair in bucketsTmp)
        {
            if (pair != null && pair != _tombstone)
            {
                Put(pair.Key, pair.Val);
            }
        }
    }

    /* 打印哈希表 */
    public void Print()
    {
        foreach (Pair pair in _buckets)
        {
            if (pair == null)
            {
                Console.WriteLine("null");
            }
            else if (pair == _tombstone)
            {
                Console.WriteLine("TOMBSTONE");
            }
            else
            {
                Console.WriteLine(pair.Key + " -> " + pair.Val);
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
public class SimpleHashAlgorithms
{
    /* 加法哈希 */
    public int AddHash(string key)
    {
        long hash = 0;
        const int modulus = 1000000007;
        foreach (var c in key)
        {
            hash = ( hash + c ) % modulus;
        }

        return (int)hash;
    }

    /* 乘法哈希 */
    public int MulHash(string key)
    {
        long hash = 0;
        const int modulus = 1000000007;
        foreach (var c in key)
        {
            hash = ( 31 * hash + c ) % modulus;
        }

        return (int)hash;
    }

    /* 异或哈希 */
    public int XorHash(string key)
    {
        var hash = 0;
        const int modulus = 1000000007;
        foreach (var c in key)
        {
            hash ^= c;
        }

        return hash & modulus;
    }

    /* 旋转哈希 */
    public int RotHash(string key)
    {
        long hash = 0;
        const int modulus = 1000000007;
        foreach (var c in key)
        {
            hash = ( ( hash << 4 ) ^ ( hash >> 28 ) ^ c ) % modulus;
        }

        return (int)hash;
    }
}
