<p>You are given the heads of two sorted linked lists <code>list1</code> and <code>list2</code>.</p>

<p>Merge the two lists into one <strong>sorted</strong> list. The list should be made by splicing together the nodes of the first two lists.</p>

<p>Return <em>the head of the merged linked list</em>.</p>

<p>&nbsp;</p>
<p><strong class="example">Example 1:</strong></p>
<img alt="" src="https://assets.leetcode.com/uploads/2020/10/03/merge_ex1.jpg" style="width: 662px; height: 302px;" />
<pre>
<strong>Input:</strong> list1 = [1,2,4], list2 = [1,3,4]
<strong>Output:</strong> [1,1,2,3,4,4]
</pre>

<p><strong class="example">Example 2:</strong></p>

<pre>
<strong>Input:</strong> list1 = [], list2 = []
<strong>Output:</strong> []
</pre>

<p><strong class="example">Example 3:</strong></p>

<pre>
<strong>Input:</strong> list1 = [], list2 = [0]
<strong>Output:</strong> [0]
</pre>

<p>&nbsp;</p>
<p><strong>Constraints:</strong></p>

<ul>
 <li>The number of nodes in both lists is in the range <code>[0, 50]</code>.</li>
 <li><code>-100 &lt;= Node.val &lt;= 100</code></li>
 <li>Both <code>list1</code> and <code>list2</code> are sorted in <strong>non-decreasing</strong> order.</li>
</ul>

<details><summary><strong>Related Topics</strong></summary>递归 | 链表</details><br>

<div>👍 3472, 👎 0<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.gitee.io/article/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[数据结构精品课](https://labuladong.online/algo/ds-class/)
和 [递归算法专题课](https://labuladong.online/algo/tree-class/)
限时附赠网站会员；算法可视化编辑器上线，[点击体验](https://labuladong.online/algo-visualize/)！**



<p><strong><a href="https://labuladong.online/algo/slug.html?slug=merge-two-sorted-lists" target="_blank">⭐️labuladong 题解</a></strong></p>
<details><summary><strong>labuladong 思路</strong></summary>

## 基本思路

> 本文有视频版：[链表双指针技巧全面汇总](https://www.bilibili.com/video/BV1q94y1X7vy)

经典算法题了，[双指针技巧](https://labuladong.github.io/article/fname.html?fname=链表技巧) 用起来。

![](https://labuladong.github.io/pictures/链表技巧/1.gif)

这个算法的逻辑类似于「拉拉链」，`l1, l2` 类似于拉链两侧的锯齿，指针 `p` 就好像拉链的拉索，将两个有序链表合并。

**代码中还用到一个链表的算法题中是很常见的「虚拟头结点」技巧，也就是 `dummy` 节点**
，它相当于是个占位符，可以避免处理空指针的情况，降低代码的复杂性。

*
*详细题解：[双指针技巧秒杀七道链表题目](https://labuladong.github.io/article/fname.html?fname=链表技巧)
**

*
*标签：[数据结构](https://mp.weixin.qq.com/mp/appmsgalbum?__biz=MzAxODQxMDM0Mw==&action=getalbum&album_id=1318892385270808576)，[链表](https://mp.weixin.qq.com/mp/appmsgalbum?__biz=MzAxODQxMDM0Mw==&action=getalbum&album_id=2120596033251475465)，[链表双指针](https://mp.weixin.qq.com/mp/appmsgalbum?__biz=MzAxODQxMDM0Mw==&action=getalbum&album_id=2120596033251475465)
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
    ListNode* mergeTwoLists(ListNode* l1, ListNode* l2) {
        // 虚拟头结点
        ListNode* dummy = new ListNode(-1), *p = dummy;
        ListNode* p1 = l1, *p2 = l2;

        while (p1 != nullptr && p2 != nullptr) {/**<extend down -200>![](https://labuladong.github.io/pictures/链表技巧/1.gif) */
            // 比较 p1 和 p2 两个指针
            // 将值较小的的节点接到 p 指针
            if (p1->val > p2->val) {
                p->next = p2;
                p2 = p2->next;
            } else {
                p->next = p1;
                p1 = p1->next;
            }
            // p 指针不断前进
            p = p->next;
        }

        if (p1 != nullptr) {
            p->next = p1;
        }

        if (p2 != nullptr) {
            p->next = p2;
        }

        return dummy->next;
    }
};
```

</div></div>

<div data-tab-item="python" class="tab-item " data-tab-group="default"><div class="highlight">

```python
# 注意：python 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
# 本代码已经通过力扣的测试用例，应该可直接成功提交。

class Solution:
    def mergeTwoLists(self, l1: ListNode, l2: ListNode) -> ListNode:
        # 虚拟头结点
        dummy = ListNode(-1)
        p = dummy
        p1 = l1
        p2 = l2

        while p1 and p2: # <extend down -200>![](https://labuladong.github.io/pictures/链表技巧/1.gif) #
            # 比较 p1 和 p2 两个指针
            # 将值较小的的节点接到 p 指针
            if p1.val > p2.val:
                p.next = p2
                p2 = p2.next
            else:
                p.next = p1
                p1 = p1.next
            # p 指针不断前进
            p = p.next

        if p1:
            p.next = p1

        if p2:
            p.next = p2

        return dummy.next
```

</div></div>

<div data-tab-item="java" class="tab-item active" data-tab-group="default"><div class="highlight">

```java
class Solution {
    public ListNode mergeTwoLists(ListNode l1, ListNode l2) {
        // 虚拟头结点
        ListNode dummy = new ListNode(-1), p = dummy;
        ListNode p1 = l1, p2 = l2;

        while (p1 != null && p2 != null) {/**<extend down -200>![](https://labuladong.github.io/pictures/链表技巧/1.gif) */
            // 比较 p1 和 p2 两个指针
            // 将值较小的的节点接到 p 指针
            if (p1.val > p2.val) {
                p.next = p2;
                p2 = p2.next;
            } else {
                p.next = p1;
                p1 = p1.next;
            }
            // p 指针不断前进
            p = p.next;
        }

        if (p1 != null) {
            p.next = p1;
        }

        if (p2 != null) {
            p.next = p2;
        }

        return dummy.next;
    }
}
```

</div></div>

<div data-tab-item="go" class="tab-item " data-tab-group="default"><div class="highlight">

```go
// 注意：go 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

/**
 * Definition for singly-linked list.
 * type ListNode struct {
 *     Val int
 *     Next *ListNode
 * }
 */
func mergeTwoLists(l1 *ListNode, l2 *ListNode) *ListNode {
    // 虚拟头结点
    dummy := &ListNode{-1, nil}
    p := dummy
    p1 := l1
    p2 := l2

    for p1 != nil && p2 != nil {/**<extend down -200>![](https://labuladong.github.io/pictures/链表技巧/1.gif) */
        // 比较 p1 和 p2 两个指针
        // 将值较小的的节点接到 p 指针
        if p1.Val > p2.Val {
            p.Next = p2
            p2 = p2.Next
        } else {
            p.Next = p1
            p1 = p1.Next
        }
        // p 指针不断前进
        p = p.Next
    }

    if p1 != nil {
        p.Next = p1
    }

    if p2 != nil {
        p.Next = p2
    }

    return dummy.Next
}
```

</div></div>

<div data-tab-item="javascript" class="tab-item " data-tab-group="default"><div class="highlight">

```javascript
// 注意：javascript 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

/**
 * @param {ListNode} l1
 * @param {ListNode} l2
 * @return {ListNode}
 */
var mergeTwoLists = function(l1, l2) {
    // 虚拟头结点
    var dummy = new ListNode(-1), p = dummy;
    var p1 = l1, p2 = l2;

    while (p1 !== null && p2 !== null) {/**<extend down -200>![](https://labuladong.github.io/pictures/链表技巧/1.gif) */
        // 比较 p1 和 p2 两个指针
        // 将值较小的的节点接到 p 指针
        if (p1.val > p2.val) {
            p.next = p2;
            p2 = p2.next;
        } else {
            p.next = p1;
            p1 = p1.next;
        }
        // p 指针不断前进
        p = p.next;
    }

    if (p1 !== null) {
        p.next = p1;
    }

    if (p2 !== null) {
        p.next = p2;
    }

    return dummy.next;
};
```

</div></div>
</div></div>

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🍭🍭 算法可视化 🍭🍭</strong></summary><div id="data_merge-two-sorted-lists" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_merge-two-sorted-lists"></div></div>
</details><hr /><br />

**类似题目**：

- [1305. 两棵二叉搜索树中的所有元素 🟠](/problems/all-elements-in-two-binary-search-trees)
- [141. 环形链表 🟢](/problems/linked-list-cycle)
- [142. 环形链表 II 🟠](/problems/linked-list-cycle-ii)
- [160. 相交链表 🟢](/problems/intersection-of-two-linked-lists)
- [19. 删除链表的倒数第 N 个结点 🟠](/problems/remove-nth-node-from-end-of-list)
- [23. 合并K个升序链表 🔴](/problems/merge-k-sorted-lists)
- [264. 丑数 II 🟠](/problems/ugly-number-ii)
- [313. 超级丑数 🟠](/problems/super-ugly-number)
- [86. 分隔链表 🟠](/problems/partition-list)
- [876. 链表的中间结点 🟢](/problems/middle-of-the-linked-list)
- [88. 合并两个有序数组 🟢](/problems/merge-sorted-array)
- [97. 交错字符串 🟠](/problems/interleaving-string)
- [977. 有序数组的平方 🟢](/problems/squares-of-a-sorted-array)
- [剑指 Offer 22. 链表中倒数第k个节点 🟢](/problems/lian-biao-zhong-dao-shu-di-kge-jie-dian-lcof)
- [剑指 Offer 25. 合并两个排序的链表 🟢](/problems/he-bing-liang-ge-pai-xu-de-lian-biao-lcof)
- [剑指 Offer 49. 丑数 🟠](/problems/chou-shu-lcof)
- [剑指 Offer 52. 两个链表的第一个公共节点 🟢](/problems/liang-ge-lian-biao-de-di-yi-ge-gong-gong-jie-dian-lcof)
- [剑指 Offer II 021. 删除链表的倒数第 n 个结点 🟠](/problems/SLwz0R)
- [剑指 Offer II 022. 链表中环的入口节点 🟠](/problems/c32eOV)
- [剑指 Offer II 023. 两个链表的第一个重合节点 🟢](/problems/3u1WK4)
- [剑指 Offer II 078. 合并排序链表 🔴](/problems/vvXgSW)

</details>
</div>

