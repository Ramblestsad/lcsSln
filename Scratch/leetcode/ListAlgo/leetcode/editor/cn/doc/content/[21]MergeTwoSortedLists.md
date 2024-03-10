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

<div>👍 3459, 👎 0<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.gitee.io/article/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

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

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🌈🌈 算法可视化 🌈🌈</strong></summary><div id="data_merge-two-sorted-lists" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
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

