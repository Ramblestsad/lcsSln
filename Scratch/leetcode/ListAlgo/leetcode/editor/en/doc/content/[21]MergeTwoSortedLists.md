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

<details><summary><strong>Related Topics</strong></summary>Linked List | Recursion</details><br>

<div>👍 21899, 👎 2137<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.online/algo/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[新版网站会员](https://labuladong.online/algo/intro/site-vip/) 即将涨价；已支持老用户续费~**



<p><strong><a href="https://labuladong.online/algo/slug.html?slug=merge-two-sorted-lists" target="_blank">⭐️labuladong 题解</a></strong></p>
<details><summary><strong>labuladong 思路</strong></summary>

<div id="labuladong_solution_zh">

## 基本思路

> 本文有视频版：[链表双指针技巧全面汇总](https://www.bilibili.com/video/BV1q94y1X7vy)

经典算法题了，[双指针技巧](https://labuladong.online/algo/essential-technique/linked-list-skills-summary/) 用起来。

![](https://labuladong.online/algo/images/链表技巧/1.gif)

这个算法的逻辑类似于「拉拉链」，`l1, l2` 类似于拉链两侧的锯齿，指针 `p` 就好像拉链的拉索，将两个有序链表合并。

**代码中还用到一个链表的算法题中是很常见的「虚拟头结点」技巧，也就是 `dummy` 节点**，它相当于是个占位符，可以避免处理空指针的情况，降低代码的复杂性。

**详细题解：[双指针技巧秒杀七道链表题目](https://labuladong.online/algo/essential-technique/linked-list-skills-summary/)**

</div>

**标签：[数据结构](https://labuladong.online/algo/)，[链表](https://labuladong.online/algo/)，[链表双指针](https://labuladong.online/algo/)**

<div id="solution">

## 解法代码



<div class="tab-panel"><div class="tab-nav">
<button data-tab-item="cpp" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">cpp🤖</button>

<button data-tab-item="python" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">python🤖</button>

<button data-tab-item="java" class="tab-nav-button btn active" data-tab-group="default" onclick="switchTab(this)">java🟢</button>

<button data-tab-item="go" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">go🤖</button>

<button data-tab-item="javascript" class="tab-nav-button btn " data-tab-group="default" onclick="switchTab(this)">javascript🤖</button>
</div><div class="tab-content">
<div data-tab-item="cpp" class="tab-item " data-tab-group="default"><div class="highlight">

```cpp
// 注意：cpp 代码由 chatGPT🤖 根据我的 java 代码翻译，旨在帮助不同背景的读者理解算法逻辑。
// 本代码已经通过力扣的测试用例，应该可直接成功提交。

class Solution {
public:
    ListNode* mergeTwoLists(ListNode* l1, ListNode* l2) {
        // 虚拟头结点
        ListNode* dummy = new ListNode(-1);
        ListNode* p = dummy;
        ListNode* p1 = l1;
        ListNode* p2 = l2;

        while (p1 != nullptr && p2 != nullptr) {
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

        while p1 is not None and p2 is not None: # <extend down -200>![](https://labuladong.online/algo/images/链表技巧/1.gif) #
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

        if p1 is not None:
            p.next = p1

        if p2 is not None:
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

        while (p1 != null && p2 != null) {/**<extend down -200>![](https://labuladong.online/algo/images/链表技巧/1.gif) */
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

func mergeTwoLists(l1 *ListNode, l2 *ListNode) *ListNode {
    // 虚拟头结点
    dummy := &ListNode{Val: -1}
    p := dummy
    p1 := l1
    p2 := l2

    for p1 != nil && p2 != nil {
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

var mergeTwoLists = function(l1, l2) {
    // 虚拟头结点
    let dummy = new ListNode(-1), p = dummy;
    let p1 = l1, p2 = l2;

    while (p1 !== null && p2 !== null) {
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

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🥳🥳 算法可视化 🥳🥳</strong></summary><div id="data_merge-two-sorted-lists" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
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

</div>

</details>
</div>

