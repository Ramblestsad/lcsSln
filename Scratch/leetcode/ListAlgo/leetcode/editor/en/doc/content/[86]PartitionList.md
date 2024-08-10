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

<details><summary><strong>Related Topics</strong></summary>Linked List | Two Pointers</details><br>

<div>👍 7359, 👎 879<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.online/algo/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[新版网站会员](https://labuladong.online/algo/intro/site-vip/) 即将涨价；已支持老用户续费~**



<p><strong><a href="https://labuladong.online/algo/slug.html?slug=partition-list" target="_blank">⭐️labuladong 题解</a></strong></p>
<details><summary><strong>labuladong 思路</strong></summary>

<div id="labuladong_solution_zh">

## 基本思路

> 本文有视频版：[链表双指针技巧全面汇总](https://www.bilibili.com/video/BV1q94y1X7vy)

这道题很像 [21. 合并两个有序链表](/problems/merge-two-sorted-lists)，21 题让你合二为一，这里需要分解让你把原链表一分为二。

具体来说，我们可以把原链表分成两个小链表，一个链表中的元素大小都小于 `x`，另一个链表中的元素都大于等于 `x`，最后再把这两条链表接到一起，就得到了题目想要的结果。细节看代码吧，注意虚拟头结点的运用。

**详细题解：[双指针技巧秒杀七道链表题目](https://labuladong.online/algo/essential-technique/linked-list-skills-summary/)**

</div>

**标签：[数据结构](https://labuladong.online/algo/)，[链表双指针](https://labuladong.online/algo/)**

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

/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode() : val(0), next(nullptr) {}
 *     ListNode(int x) : val(x), next(nullptr) {}
 *     ListNode(int x, ListNode *next) : val(x), next(next) {}
 * };
 */

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
        while (p != nullptr) {
            if (p->val >= x) {
                p2->next = p;
                p2 = p2->next;
            } else {
                p1->next = p;
                p1 = p1->next;
            }
            // 断开原链表中的每个节点的 next 指针
            ListNode* temp = p->next;
            p->next = nullptr;
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

class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

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
    dummy1 := &ListNode{-1, nil}
    // 存放大于等于 x 的链表的虚拟头结点
    dummy2 := &ListNode{-1, nil}
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

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🍭🍭 算法可视化 🍭🍭</strong></summary><div id="data_partition-list" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_partition-list"></div></div>
</details><hr /><br />

如果你不断开原链表中的每个节点的 `next` 指针就会出错，因为结果链表中会包含一个环：

<hr /><details open hint-container details><summary style="font-size: medium"><strong>👾👾 算法可视化 👾👾</strong></summary><div id="data_mydata-partition-list" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_mydata-partition-list"></div></div>
</details><hr /><br />

总的来说，如果我们需要把原链表的节点接到新链表上，而不是 new 新节点来组成新链表的话，那么断开节点和原链表之间的链接可能是必要的。那其实我们可以养成一个好习惯，但凡遇到这种情况，就把原链表的节点断开，这样就不会出错了。

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

</div>

</details>
</div>

