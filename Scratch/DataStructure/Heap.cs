namespace Scratch.DataStructure;

public class MaxHeap {
    List<int> maxHeap;

    public MaxHeap() {
        maxHeap = [];
    }

    public MaxHeap(IEnumerable<int> nums) {
        // 将列表元素原封不动添加进堆
        maxHeap = new List<int>(nums);
        // 堆化除叶节点以外的其他所有节点
        var size = Parent(this.Size() - 1);
        for (var i = size; i >= 0; i--) {
            SiftDown(i);
        }
    }

    private int Left(int i) {
        return 2 * i + 1;
    }

    private int Right(int i) {
        return 2 * i + 2;
    }

    private int Parent(int i) {
        return ( i - 1 ) / 2; // 向下整除
    }

    public int Peek() {
        return maxHeap[0];
    }

    public int Size() {
        return maxHeap.Count;
    }

    public bool IsEmpty() {
        return Size() == 0;
    }

    /*
     * 设节点总数为 n，则树的高度为 O(logn)。
     * 由此可知，堆化操作的循环轮数最多为O(logn)，元素入堆操作的时间复杂度为O(logn)。
     */

    void Push(int val) {
        maxHeap.Add(val);
        // 从底至顶堆化
        SiftUp(Size() - 1);
    }

    int Pop() {
        if (IsEmpty())
            throw new IndexOutOfRangeException();
        // 交换根节点与最右叶节点（交换首元素与尾元素）
        Swap(0, Size() - 1);
        // 删除节点
        var val = maxHeap.Last();
        maxHeap.RemoveAt(Size() - 1);
        // 从顶至底堆化
        SiftDown(0);
        // 返回堆顶元素
        return val;
    }

    void Swap(int i, int p) {
        ( maxHeap[i], maxHeap[p] ) = ( maxHeap[p], maxHeap[i] );
    }

    /// <summary>
    /// 从节点 i 开始，从底至顶堆化
    /// </summary>
    /// <param name="i"></param>
    void SiftUp(int i) {
        while (true) {
            var p = Parent(i);
            // 若“越过根节点”或“节点无须修复”，则结束堆化
            if (p < 0 || maxHeap[i] <= maxHeap[p])
                break;
            // 交换两节点
            Swap(i, p);
            // 循环向上堆化
            i = p;
        }
    }

    /// <summary>
    /// 从节点 i 开始，从顶至底堆化
    /// </summary>
    /// <param name="i"></param>
    void SiftDown(int i) {
        while (true) {
            // 判断节点 i, l, r 中值最大的节点，记为 ma
            int l = Left(i), r = Right(i), ma = i;
            if (l < Size() && maxHeap[l] > maxHeap[ma])
                ma = l;
            if (r < Size() && maxHeap[r] > maxHeap[ma])
                ma = r;
            // 若“节点 i 最大”或“越过叶节点”，则结束堆化
            if (ma == i) break;
            // 交换两节点
            Swap(i, ma);
            // 循环向下堆化
            i = ma;
        }
    }
}
