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

<div>👍 21970, 👎 2144<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.online/algo/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：已完成网站教程、网站习题、配套插件中所有多语言代码的校准，解决了之前 chatGPT 翻译可能出错的问题~**



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

</div>

</details>
</div>

