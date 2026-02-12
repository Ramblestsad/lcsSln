namespace Scratch.Algorithms;

public class Lru
{
    // key -> Node(key, val)
    private Dictionary<int, Node> map;

    // Node(k1, v1) <-> Node(k2, v2)...
    private DoubleList cache;

    // 最大容量
    private int cap;

    public Lru(int capacity)
    {
        cap = capacity;
        map = new();
        cache = new DoubleList();
    }

    #region helper fn

    // 将某个 key 提升为最近使用的
    private void _makeRecently(int key)
    {
        if (!map.TryGetValue(key, out var x)) return;
        // 先从链表中删除这个节点
        cache.Remove(x);
        // 重新插到队尾
        cache.AddLast(x);
    }

    // 添加最近使用的元素
    private void _addRecently(int key, int val)
    {
        Node x = new Node(key, val);
        // 链表尾部就是最近使用的元素
        cache.AddLast(x);
        // 别忘了在 map 中添加 key 的映射
        map[key] = x;
    }

    // 删除某一个 key
    private void _deleteKey(int key)
    {
        if (!map.TryGetValue(key, out var x)) return;
        // 从链表中删除
        cache.Remove(x);
        // 从 map 中删除
        map.Remove(key);
    }

    // 删除最久未使用的元素
    private void _removeLeastRecently()
    {
        // 链表头部的第一个元素就是最久未使用的
        var deletedNode = cache.RemoveFirst();
        if (deletedNode == null) return;
        // 同时别忘了从 map 中删除它的 key
        var deletedKey = deletedNode.key;
        map.Remove(deletedKey);
    }

    #endregion

    #region Lru API

    public int Get(int key)
    {
        // 将该数据提升为最近使用的
        map.TryGetValue(key, out var node);
        if (node == null) return -1;
        _makeRecently(key);

        return node.val;
    }

    public void Put(int key, int val)
    {
        // key已存在
        if (map.TryGetValue(key, out var node))
        {
            // 删除旧数据
            _deleteKey(key);
            // 新插入的数据为最近使用的数据
            _addRecently(key, val);
            return;
        }

        // key未存在
        if (cap == cache.Size())
        {
            // 但是缓存满了，先删
            _removeLeastRecently();
        }

        // 添加
        _addRecently(key, val);
    }

    #endregion
}

class Node
{
    public int key, val;
    public Node? next, prev;

    public Node(int k, int v)
    {
        key = k;
        val = v;
    }
}

class DoubleList
{
    // 头尾虚节点
    private Node head, tail;

    // 链表元素数
    private int _size;

    public DoubleList()
    {
        // 初始化双向链表的数据
        head = new Node(0, 0);
        tail = new Node(0, 0);
        head.next = tail;
        tail.prev = head;
        _size = 0;
    }

    // 在链表尾部添加节点 x，时间 O(1)
    public void AddLast(Node x)
    {
        x.prev = tail.prev;
        x.next = tail;
        tail.prev!.next = x;
        tail.prev = x;
        _size++;
    }

    // 删除链表中的 x 节点（x 一定存在）
    // 由于是双链表且给的是目标 Node 节点，时间 O(1)
    public void Remove(Node x)
    {
        x.prev?.next = x.next;
        x.next?.prev = x.prev;
        _size--;
    }

    // 删除链表中第一个节点，并返回该节点，时间 O(1)
    public Node? RemoveFirst()
    {
        if (head.next == tail)
            return null;
        var first = head.next!;
        Remove(first);
        return first;
    }

    // 返回链表长度，时间 O(1)
    public int Size()
    {
        return _size;
    }
}
