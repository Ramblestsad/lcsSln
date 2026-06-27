using System.Text;

namespace Scratch.Labuladong.Algorithms.ImplTrie;

//leetcode submit region begin(Prohibit modification and deletion)

public class Trie
{
    TrieSet set = new();

    public void Insert(string word)
    {
        set.Add(word);
    }

    public bool Search(string word)
    {
        return set.Contains(word);
    }

    public bool StartsWith(string prefix)
    {
        return set.HasKeyWithPrefix(prefix);
    }
}

public class TrieMap<V>
{
    // ASCII 码个数
    private const int R = 256;

    // 当前存在 Map 中的键值对个数
    public int Size { get; set; } = 0;

    private class TrieNode
    {
        public V? val = default;
        public bool hasValue = false;
        public TrieNode?[] children = new TrieNode?[R];
    }

    // Trie 树的根节点
    private TrieNode? root = new();

    // 从节点 node 开始搜索 key，如果存在返回对应节点，否则返回 null
    private static TrieNode? _getNode(TrieNode? node, string key)
    {
        var p = node;
        // 从节点 node 开始搜索 key
        foreach (var c in key)
        {
            if (p == null)
                // 无法向下搜索
                return null;

            // 向下搜索
            p = p.children[c];
        }

        return p;
    }

    // 搜索 key 对应的值，不存在则返回 null
    public V? Get(string key)
    {
        // 从 root 开始搜索 key
        var x = _getNode(root, key);
        if (x == null || !x.hasValue)
        {
            // x 为空或 x 的 val 字段为空都说明 key 没有对应的值
            return default;
        }

        return x.val;
    }

    // 判断 key 是否存在在 Map 中
    public bool ContainsKey(string key)
    {
        var x = _getNode(root, key);
        return x?.hasValue == true;
    }

    // 判断是和否存在前缀为 prefix 的键
    public bool HasKeyWithPrefix(string prefix)
    {
        // 只要能找到一个节点，就是存在前缀
        return _getNode(root, prefix) != null;
    }

    // 在所有键中寻找 query 的最短前缀
    public string ShortestPrefixOf(string query)
    {
        var p = root;
        for (var i = 0; i < query.Length; i++)
        {
            if (p == null) return "";
            if (p.hasValue)
                return query[..i];

            p = p.children[query[i]];
        }

        if (p?.hasValue == true)
            return query;

        return "";
    }

    // 在所有键中寻找 query 的最长前缀
    public string LongestPrefixOf(string query)
    {
        var p = root;
        // 记录前缀的最大长度
        var maxLen = 0;

        // 从节点 node 开始搜索 key
        for (int i = 0; i < query.Length; i++)
        {
            if (p == null)
                // 无法向下搜索
                break;

            if (p.hasValue)
            {
                // 找到一个键是 query 的前缀，更新前缀的最大长度
                maxLen = i;
            }

            // 向下搜索
            p = p.children[query[i]];
        }

        if (p?.hasValue == true)
        {
            // 如果 query 本身就是一个键
            return query;
        }

        return query[..maxLen];
    }


    public List<string> KeysWithPrefix(string prefix)
    {
        List<string> res = [];
        // 找到匹配 prefix 在 Trie 树中的那个节点
        var x = _getNode(root, prefix);
        if (x == null)
        {
            return res;
        }

        // DFS 遍历以 x 为根的这棵 Trie 树
        _traverseAllKeys(x, new StringBuilder(prefix), res);
        return res;
    }

    // 遍历以 node 节点为根的 Trie 树，找到所有键
    private static void _traverseAllKeys(TrieNode? node, StringBuilder path, List<string> res)
    {
        if (node == null)
            // 到达 Trie 树底部叶子结点
            return;

        if (node.hasValue)
            // 找到一个 key，添加到结果列表中
            res.Add(path.ToString());

        // 回溯算法遍历框架
        for (var c = 0; c < R; c++)
        {
            // 做选择
            path.Append((char)c);
            _traverseAllKeys(node.children[c], path, res);
            // 撤销选择
            path.Remove(path.Length - 1, 1);
        }
    }

    // 通配符 . 匹配任意字符
    public List<string> KeysWithPattern(string pattern)
    {
        List<string> res = [];
        _traverseAllKeysWithPattern(root, new StringBuilder(), pattern, 0, res);
        return res;
    }

    // 遍历函数，尝试在「以 node 为根的 Trie 树中」匹配 pattern[i..]
    private void _traverseAllKeysWithPattern(
        TrieNode? node,
        StringBuilder path,
        string pattern,
        int i,
        List<string> res)
    {
        if (node == null)
            // 树枝不存在，即字符 pattern[i-1] 匹配失败
            return;

        if (i == pattern.Length)
        {
            // pattern 匹配完成
            if (node.hasValue)
                // 如果这个节点存储着 val，则找到一个匹配的键
                res.Add(path.ToString());

            return;
        }

        var c = pattern[i];
        if (c == '.')
        {
            // pattern[i] 是通配符，可以变化成任意字符
            // 多叉树（回溯算法）遍历框架
            for (var j = 0; j < R; j++)
            {
                path.Append((char)j);
                _traverseAllKeysWithPattern(node.children[j], path, pattern, i + 1, res);
                path.Remove(path.Length - 1, 1);
            }
        }
        else
        {
            // pattern[i] 是普通字符 c
            path.Append(c);
            _traverseAllKeysWithPattern(node.children[c], path, pattern, i + 1, res);
            path.Remove(path.Length - 1, 1);
        }
    }

    // 判断是和否存在前缀为 prefix 的键
    public bool HasKeyWithPattern(string pattern)
    {
        // 一个偷懒的实现
        // return keysWithPattern(pattern).Count != 0;

        // 从 root 节点开始匹配 pattern[0..]
        return _hasKeyWithPattern(root, pattern, 0);
    }

    // 函数定义：从 node 节点开始匹配 pattern[i..]，返回是否成功匹配
    private bool _hasKeyWithPattern(TrieNode? node, string pattern, int i)
    {
        // 找到一个可行解就提前结束递归
        if (node == null)
            // 树枝不存在，即匹配失败
            return false;

        if (i == pattern.Length)
            // 模式串走到头了，看看匹配到的是否是一个键
            return node.hasValue;

        var c = pattern[i];
        // 没有遇到通配符
        if (c != '.')
        {
            // 从 node.children[c] 节点开始匹配 pattern[i+1..]
            return _hasKeyWithPattern(node.children[c], pattern, i + 1);
        }

        // 遇到通配符
        for (int j = 0; j < R; j++)
        {
            // pattern[i] 可以变化成任意字符，尝试所有可能，只要遇到一个匹配成功就返回
            if (_hasKeyWithPattern(node.children[j], pattern, i + 1))
            {
                return true;
            }
        }

        // 都没有匹配
        return false;
    }

    // 在 map 中添加或修改键值对
    public void Put(string key, V val)
    {
        if (!ContainsKey(key))
        {
            // 新增键值对
            Size++;
        }

        // 需要一个额外的辅助函数，并接收其返回值
        root = _put(root, key, val, 0);
    }

    // 定义：向以 node 为根的 Trie 树中插入 key[i..]，返回插入完成后的根节点
    private static TrieNode _put(TrieNode? node, string key, V val, int i)
    {
        if (node == null)
        {
            // 如果树枝不存在，新建
            node = new();
        }

        if (i == key.Length)
        {
            // key 的路径已插入完成，将值 val 存入节点
            node.val = val;
            node.hasValue = true;
            return node;
        }

        var c = key[i];
        // 递归插入子节点，并接收返回值
        node.children[c] = _put(node.children[c], key, val, i + 1);
        return node;
    }

    // 在 Map 中删除 key
    public void Remove(string key)
    {
        if (!ContainsKey(key))
        {
            return;
        }

        // 递归修改数据结构要接收函数的返回值
        root = _remove(root, key, 0);
        Size--;
    }

    // 定义：在以 node 为根的 Trie 树中删除 key[i..]，返回删除后的根节点
    private TrieNode? _remove(TrieNode? node, string key, int i)
    {
        // 一个节点如何知道自己是否需要被删除
        //     主要看自己的 val 字段是否为空
        //     自己的 children 数组是否全都是空指针
        if (node == null)
        {
            return null;
        }

        if (i == key.Length)
        {
            // 找到了 key 对应的 TrieNode，删除 val
            node.val = default;
            node.hasValue = false;
        }
        else
        {
            var c = key[i];
            // 递归去子树进行删除
            node.children[c] = _remove(node.children[c], key, i + 1);
        }

        // 一个节点要先递归处理子树，然后在后序位置检查自己的 val 字段和 children 列表，判断自己是否需要被删除
        // 后序位置，递归路径上的节点可能需要被清理
        if (node.hasValue)
            // 如果该 TireNode 存储着 val，不需要被清理
            return node;

        // 检查该 TrieNode 是否还有后缀
        for (int c = 0; c < R; c++)
        {
            if (node.children[c] != null)
                // 只要存在一个子节点（后缀树枝），就不需要被清理
                return node;
        }

        // 既没有存储 val，也没有后缀树枝，则该节点需要被清理
        return null;
    }
}

public class TrieSet
{
    // 底层用一个 TrieMap，键就是 TrieSet，值仅仅起到占位的作用
    // 值的类型可以随便设置，我参考 Java 标准库设置成 Object
    private readonly TrieMap<object> map = new();

    // **** 增 ****

    // 在集合中添加元素 key
    public void Add(string key)
    {
        map.Put(key, new Object());
    }

    // **** 删 ****

    // 从集合中删除元素 key
    public void Remove(string key)
    {
        map.Remove(key);
    }

    // **** 查 ****

    // 判断元素 key 是否存在集合中
    public bool Contains(string key)
    {
        return map.ContainsKey(key);
    }

    // 在集合中寻找 query 的最短前缀
    public string ShortestPrefixOf(string query)
    {
        return map.ShortestPrefixOf(query);
    }

    // 在集合中寻找 query 的最长前缀
    public string LongestPrefixOf(string query)
    {
        return map.LongestPrefixOf(query);
    }

    // 在集合中搜索前缀为 prefix 的所有元素
    public List<string> KeysWithPrefix(string prefix)
    {
        return map.KeysWithPrefix(prefix);
    }

    // 判断集合中是否存在前缀为 prefix 的元素
    public bool HasKeyWithPrefix(string prefix)
    {
        return map.HasKeyWithPrefix(prefix);
    }

    // 通配符 . 匹配任意字符，返回集合中匹配 pattern 的所有元素
    public List<string> KeysWithPattern(string pattern)
    {
        return map.KeysWithPattern(pattern);
    }

    // 通配符 . 匹配任意字符，判断集合中是否存在匹配 pattern 的元素
    public bool HasKeyWithPattern(string pattern)
    {
        return map.HasKeyWithPattern(pattern);
    }

    // 返回集合中元素的个数
    public int Size()
    {
        return map.Size;
    }
}

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */
//leetcode submit region end(Prohibit modification and deletion)
