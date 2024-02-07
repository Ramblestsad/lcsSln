<p>Given the <code>head</code> of a linked list and a value <code>x</code>, partition it such that all nodes <strong>less than</strong> <code>x</code> come before nodes <strong>greater than or equal</strong> to <code>x</code>.</p>

<p>You should <strong>preserve</strong> the original relative order of the nodes in each of the two partitions.</p>

<p>&nbsp;</p>
<p><strong class="example">Example 1:</strong></p>
<img alt="" src="https://assets.leetcode.com/uploads/2021/01/04/partition.jpg" style="width: 662px; height: 222px;" />
<pre>
<strong>Input:</strong> head = [1,4,3,2,5,2], x = 3
<strong>Output:</strong> [1,2,2,4,3,5]
</pre>

<p><strong class="example">Example 2:</strong></p>

<pre>
<strong>Input:</strong> head = [2,1], x = 2
<strong>Output:</strong> [1,2]
</pre>

<p>&nbsp;</p>
<p><strong>Constraints:</strong></p>

<ul>
 <li>The number of nodes in the list is in the range <code>[0, 200]</code>.</li>
 <li><code>-100 &lt;= Node.val &lt;= 100</code></li>
 <li><code>-200 &lt;= x &lt;= 200</code></li>
</ul>

<details><summary><strong>Related Topics</strong></summary>链表 | 双指针</details><br>

<div>👍 808, 👎 0<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.gitee.io/article/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.github.io/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[数据结构精品课](https://aep.h5.xeknow.com/s/1XJHEO)
和 [递归算法专题课](https://aep.xet.tech/s/3YGcq3)
限时附赠网站会员；算法可视化编辑器上线，[点击体验](https://labuladong.online/algo-visualize/)！**



<p><strong><a href="https://labuladong.github.io/article/slug.html?slug=partition-list" target="_blank">⭐️labuladong 题解</a></strong></p>
<details><summary><strong>labuladong 思路</strong></summary>

## 基本思路

> 本文有视频版：[链表双指针技巧全面汇总](https://www.bilibili.com/video/BV1q94y1X7vy)

这道题很像 [21. 合并两个有序链表](/problems/merge-two-sorted-lists)，21 题让你合二为一，这里需要分解让你把原链表一分为二。

具体来说，我们可以把原链表分成两个小链表，一个链表中的元素大小都小于 `x`
，另一个链表中的元素都大于等于 `x`，最后再把这两条链表接到一起，就得到了题目想要的结果。细节看代码吧，注意虚拟头结点的运用。

*
*详细题解：[双指针技巧秒杀七道链表题目](https://appktavsiei5995.pc.xiaoe-tech.com/detail/i_629e1210e4b01a4852089b26/1)
**

*
*标签：[数据结构](https://mp.weixin.qq.com/mp/appmsgalbum?__biz=MzAxODQxMDM0Mw==&action=getalbum&album_id=1318892385270808576)，[链表双指针](https://mp.weixin.qq.com/mp/appmsgalbum?__biz=MzAxODQxMDM0Mw==&action=getalbum&album_id=2120596033251475465)
**

## 解法代码

提示：🟢 标记的是我写的解法代码，🤖 标记的是 chatGPT
翻译的多语言解法代码。如有错误，可以 [点这里](https://github.com/labuladong/fucking-algorithm/issues/1113)
反馈和修正。

<div class="tab-panel"><div class="tab-nav">
<button data-tab-item="cpp" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">cpp🤖</button>

<button data-tab-item="python" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">
python🤖</button>

<button data-tab-item="java" class="tab-nav-button btn active" data-tab-group="default" onclick="switchTab(this)">
java🟢</button>

<button data-tab-item="go" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">
go🤖</button>

<button data-tab-item="javascript" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">
javascript🤖</button>
</div><div class="tab-content">
<div data-tab-item="cpp" class="tab-item " data-tab-group="default"><div class="highlight">

```cpp
// 注意：cpp 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

class Solution {
public:
    ListNode* partition(ListNode* head, int x) {
        // 存放小于 x 的链表的虚拟头结点
        ListNode* dummy1 = new ListNode(-1);
        // 存放大于等于 x 的链表的虚拟头结点
        ListNode* dummy2 = new ListNode(-1);
        // p1, p2 指针负责生成结果链表
        ListNode* p1 = dummy1, *p2 = dummy2;
        // p 负责遍历原链表，类似合并两个有序链表的逻辑
        // 这里是将一个链表分解成两个链表
        ListNode* p = head;
        while (p != NULL) {
            if (p->val >= x) {
                p2->next = p;
                p2 = p2->next;
            } else {
                p1->next = p;
                p1 = p1->next;
            }
            // 断开原链表中的每个节点的 next 指针
            ListNode* temp = p->next;
            p->next = NULL;
            p = temp;
        }
        // 链接两个链表
        p1->next = dummy2->next;

        return dummy1->next;
    }
};
```

</div></div>

<div data-tab-item="python" class="tab-item " data-tab-group="default"><div class="highlight">

```python
# 注意：python 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
# 本代码已经通过力扣的测试用例，应该可直接成功提交。

class Solution:
    def partition(self, head: ListNode, x: int) -> ListNode:
        # 存放小于 x 的链表的虚拟头结点
        dummy1 = ListNode(-1)
        # 存放大于等于 x 的链表的虚拟头结点
        dummy2 = ListNode(-1)
        # p1, p2 指针负责生成结果链表
        p1, p2 = dummy1, dummy2
        # p 负责遍历原链表，类似合并两个有序链表的逻辑
        # 这里是将一个链表分解成两个链表
        p = head
        while p:
            if p.val >= x:
                p2.next = p
                p2 = p2.next
            else:
                p1.next = p
                p1 = p1.next
            # 断开原链表中的每个节点的 next 指针
            temp = p.next
            p.next = None
            p = temp
        # 链接两个链表
        p1.next = dummy2.next

        return dummy1.next
```

</div></div>

<div data-tab-item="java" class="tab-item active" data-tab-group="default"><div class="highlight">

```java
class Solution {
    public ListNode partition(ListNode head, int x) {
        // 存放小于 x 的链表的虚拟头结点
        ListNode dummy1 = new ListNode(-1);
        // 存放大于等于 x 的链表的虚拟头结点
        ListNode dummy2 = new ListNode(-1);
        // p1, p2 指针负责生成结果链表
        ListNode p1 = dummy1, p2 = dummy2;
        // p 负责遍历原链表，类似合并两个有序链表的逻辑
        // 这里是将一个链表分解成两个链表
        ListNode p = head;
        while (p != null) {
            if (p.val >= x) {
                p2.next = p;
                p2 = p2.next;
            } else {
                p1.next = p;
                p1 = p1.next;
            }
            // 断开原链表中的每个节点的 next 指针
            ListNode temp = p.next;
            p.next = null;
            p = temp;
        }
        // 链接两个链表
        p1.next = dummy2.next;

        return dummy1.next;
    }
}
```

</div></div>

<div data-tab-item="go" class="tab-item " data-tab-group="default"><div class="highlight">

```go
// 注意：go 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

func partition(head *ListNode, x int) *ListNode {
  // 存放小于 x 的链表的虚拟头结点
  dummy1 := &ListNode{}
  // 存放大于等于 x 的链表的虚拟头结点
  dummy2 := &ListNode{}
  // p1, p2 指针负责生成结果链表
  p1, p2 := dummy1, dummy2
  // p 负责遍历原链表，类似合并两个有序链表的逻辑
  // 这里是将一个链表分解成两个链表
  p := head
  for p != nil {
    if p.Val >= x {
      p2.Next = p
      p2 = p2.Next
    } else {
      p1.Next = p
      p1 = p1.Next
    }
    // 断开原链表中的每个节点的 next 指针
    temp := p.Next
    p.Next = nil
    p = temp
  }
  // 链接两个链表
  p1.Next = dummy2.Next
  return dummy1.Next
}
```

</div></div>

<div data-tab-item="javascript" class="tab-item " data-tab-group="default"><div class="highlight">

```javascript
// 注意：javascript 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

/**
 * @param {ListNode} head
 * @param {number} x
 * @return {ListNode}
 */
var partition = function(head, x) {
    // 存放小于 x 的链表的虚拟头结点
    let dummy1 = new ListNode(-1);
    // 存放大于等于 x 的链表的虚拟头结点
    let dummy2 = new ListNode(-1);
    // p1, p2 指针负责生成结果链表
    let p1 = dummy1, p2 = dummy2;
    // p 负责遍历原链表，类似合并两个有序链表的逻辑
    // 这里是将一个链表分解成两个链表
    let p = head;
    while (p !== null) {
        if (p.val >= x) {
            p2.next = p;
            p2 = p2.next;
        } else {
            p1.next = p;
            p1 = p1.next;
        }
        // 断开原链表中的每个节点的 next 指针
        let temp = p.next;
        p.next = null;
        p = temp;
    }
    // 链接两个链表
    p1.next = dummy2.next;

    return dummy1.next;
};
```

</div></div>
</div></div>


肯定有读者对「断开原链表中的每个节点的 `next` 指针」这部分代码有疑问，借助我们的可视化面板就很容易看明白了，首先看下正确的写法：

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🎃🎃 算法可视化 🎃🎃</strong></summary><div id="data_partition-list" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_partition-list"></div></div>
</details><hr /><br />

如果你不断开原链表中的每个节点的 `next` 指针就会出错，因为结果链表中会包含一个环：

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🥳🥳 算法可视化 🥳🥳</strong></summary><div id="data_mydata-partition-list" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_mydata-partition-list"></div></div>
</details><hr /><br />

总的来说，如果我们需要把原链表的节点接到新链表上，而不是 new
新节点来组成新链表的话，那么断开节点和原链表之间的链接可能是必要的。那其实我们可以养成一个好习惯，但凡遇到这种情况，就把原链表的节点断开，这样就不会出错了。

**类似题目**：

- [141. 环形链表 🟢](/problems/linked-list-cycle)
- [142. 环形链表 II 🟠](/problems/linked-list-cycle-ii)
- [160. 相交链表 🟢](/problems/intersection-of-two-linked-lists)
- [19. 删除链表的倒数第 N 个结点 🟠](/problems/remove-nth-node-from-end-of-list)
- [21. 合并两个有序链表 🟢](/problems/merge-two-sorted-lists)
- [23. 合并K个升序链表 🔴](/problems/merge-k-sorted-lists)
- [876. 链表的中间结点 🟢](/problems/middle-of-the-linked-list)
- [剑指 Offer 18. 删除链表的节点 🟢](/problems/shan-chu-lian-biao-de-jie-dian-lcof)
- [剑指 Offer 22. 链表中倒数第k个节点 🟢](/problems/lian-biao-zhong-dao-shu-di-kge-jie-dian-lcof)
- [剑指 Offer 25. 合并两个排序的链表 🟢](/problems/he-bing-liang-ge-pai-xu-de-lian-biao-lcof)
- [剑指 Offer 52. 两个链表的第一个公共节点 🟢](/problems/liang-ge-lian-biao-de-di-yi-ge-gong-gong-jie-dian-lcof)
- [剑指 Offer II 021. 删除链表的倒数第 n 个结点 🟠](/problems/SLwz0R)
- [剑指 Offer II 022. 链表中环的入口节点 🟠](/problems/c32eOV)
- [剑指 Offer II 023. 两个链表的第一个重合节点 🟢](/problems/3u1WK4)
- [剑指 Offer II 078. 合并排序链表 🔴](/problems/vvXgSW)

</details>
</div>

