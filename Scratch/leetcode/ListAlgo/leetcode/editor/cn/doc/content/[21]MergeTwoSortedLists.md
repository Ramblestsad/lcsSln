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

<div>👍 3417, 👎 0<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.gitee.io/article/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.github.io/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[数据结构精品课](https://aep.h5.xeknow.com/s/1XJHEO)
和 [递归算法专题课](https://aep.xet.tech/s/3YGcq3)
限时附赠网站会员；算法可视化编辑器上线，[点击体验](https://labuladong.online/algo-visualize/)！**



<p><strong><a href="https://labuladong.github.io/article/slug.html?slug=merge-two-sorted-lists" target="_blank">⭐️labuladong 题解</a></strong></p>
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

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🥳🥳 算法可视化 🥳🥳</strong></summary><div id="data_merge-two-sorted-lists" data="G49GUZQvzuIoKiULoyhTkzcBLQt4Q7SuG4mwQsRndAf333l1qqMTcMzXGXZOqN4i4hjlelse6S+QgJr57oL3wHS/2u487ldLTCY1lzk869/hIMq9utdp/S9bWUBOJqACgCyx704OYFtV9m8iWh07yZPi2Pkh/FowMIKmpkwMyOvy8Xq6VBHbmrABNuBc4uD2/38tTRTQTNBFqKioCD2bIkugXyCbfNhbIJBVe6qA3od5ZVLMwqHxVaLaZc4Ztcot/H+/951sJw3eoFvbOBWjMhjHveewFHTVFUElGU/d99G6RDGoPygGi6HTI9uxIf+Mh1ANGNsStd8zJlg22B6q5fy3gnjY6r0VeAd/2PZzkD4xoPp78xsGrlrxUCnvkIZUqG+Mz9OKmDL8H787zBZ+2H+QOHujiBWZXVf+LFyV/gmYzkr68I+VwLyYKPdUGYna894iCq97DI4ljvrt88KXzk0OFjO/a2HTguzn1QZqb+rv5vXSa3/JTTO2EI3bo754bxepRuNbtwXci2R4E+5x3Rg0mS6L02x0znSLjAhEWyIK7/u7W30lDFMRX93STSNnfqy/CDVKPOboo/vjrgBHusEQftKtjWx5CqPDC6dpROyUB4S0dI5P9LpKH7F51ADHD50HUVavmGAcS5dcwU+JhQjT4xfrXliocxpvHS8Rb92BtAjMZ3Cg68qtjOf3NnauHO9+JEaBMaKC0zAUMX1H2dFVbQm3pXt0ImMXNRyaJvamCnQbOWegF1cg2ebEaFVp+daUJ4T5dx8fvte5HSoWSk/neqoVQfk0CJ/HROibiUgNA4R9EBIZO9rc1JlB/gnbkXCGkKbjodNAqMLvEbetkDk92lG/d9qPOjPAkiAnTzTVfm8BmWIQbPPuM2n+EpjV9ah6Nu224etPennoBye4kzfQbKvy4e5yE39IAMuASZiQTgoLybYYH+Y5QnoGCfVG2dQu9WUD9sPcFIGA76SOBrvIUXxf2YkcBVQ4urND0mBWd3fmDoXdIECzbcfN3a74QyJTAPP8DBB/b5RfZ80YUwYsRlgVyLEANMJRWVWA1Ttwt4M/JDIFMM8VAAn1Rtmo9Cz74EsY4Du8CuRYABrhqKzL7CLswN0O/pDIFMA8P0vOIP7eKL+dNWNLGbAYYVYgxwLQCEdl3fJKuAN3O/hDIksA83whPYOEeqNszC77WTO+hAG+w6tAjgWgEY7KClnpfztwt4M/JDIFMM8VMDiTMwhhEMrvNmwpA/yeWYEcB0AjEPCP7BzB237kD4VsAcz5ATlnIjg2zakOcHm0DLtq4q2rdjmTaUEMW7xAQpemclsS5Kha/IpF8fjfwgezWFdrL8foPyvGIFg30G9djLTXsIytu9xeaUuvM5zmWpYLbxntoI5COMkAOef/6rb6mbVsLaG1/G2pteJ1kLVywavXbx4Y7vEXsqnnxwJa8zePdIjTlJNk8sjMUTWVPZUiL9uR2SMlBF4K8AqKiAmiUcAqt/CppmxyU9ftCoXrts/Hmmo2fqISohuiQ2SkiDax4KSBpRBB1IkGLw28pkAQOWE0GniXiMweZSLRaCDhINj+h3fqoZdXvXynR4waw2mT+KAeUQluypcTqFXP02k2WpuZoAqUbWql3Dxm4t4idZl8sqZ9X7YyWdwgygNn88vqRlXoPNF2kxXTq4o0RClkXEQRBYzv6/WhmzAQRSyXJgpJoxUlMvBF1BZ1qqKmhLSIgr7MbrqhC16UB61QVK1YlLDbEvHpFjz46aVCR1GtAGZRDYLWoiBalkWRoiBELb5kNtNxPEJXaZpEeVr7zQcVcxJHyPdikuUTFOLquZdMpLtmy7/k8/IWtbv/Cl61Qm+0ufmUKNxvF7edMLXVf+SR5lMhhE4l1ylLVgG15pR59EnvRnEtjFfRMQ5yO7uUpb1QAqrWXKkCmkFLyjx6rdWMSt1Kl2XVUNp17b0X//vt6Bqemaxk5fhsmiV1QEb/2rMBydNwMqUUs9zhksoKGPNz8FGnSHXe6qumV3ZV9tp+ACb9S5wpN4kAJV3R217VNa2WyX6itvSBIeWCCK229IG+WhQh9aq3B51qTClSCHSkHis+hsv/RBf+hv5Jk8rSigcOWomA0H7x8d1TbRkj1lu5SLRkTQXf6mhf0udWVGef0gUslNFLk5f6BtyvCof6lERpFb4/auF81MhKpr98bHThhE0jQo0ff6p/IM4bN/NPuOnOKnAYG3jCmobDNlJcrLp6moEejEV0AR+edgDrGbcxaqEWhObg/9kHj2uK5pR9KuXunpZpSGi/CI3e0CeetprdtrVVw96KlqK9d7idZbC+IuuLzIR0RDnItSnuu/eso00ksVWIb8QOYjMi6tp8+4itthj4/k0gnywOfG0HBo3O0jXsHin5ore1HRXv3bI9KzvvR99xlqh5Gi/HFEZMH/dU/xJ9pTFTTz8GNdepLEcSgZ2KNql5IoqLU3OW42XcNSPiwmSkrWkWyVtBa7TkI2d7t50fLSU20x5SmKLKxU3qiSZmUpWNZlBSgkRu/8NspNUAdPTwM8+fJpAmskRs178G/b7YuL69zXJlkNXW7RcQqm5eu+487ssFiuzcIXXYUXbnUGV5eW+nztuno/PNoTk6euHcIa3P0en8DXt148gbmqOIOfqW81oFb0+OkuToQs4rD7zZOAqMo6c4ryPw1uEoF44O4bwqwBqBY/F3vt/D13jHtu5Yyp3v3vAV27FJOxZm53sxrv//Gq3/IUguwY3me8985w+Kr8oDRtIG18bbPwTy8aGZ2jwIwzz51Pls+gT7nOeT3eFxYwDzJJZ7iSNpEklKmJQ4kiYZpIRIiSJpUkFKkJQwKdEkTZqkRJISRUoMSZNJUkKkRJMSS9JkRUqYlBhS4kianEmJIiWWpGkKUmLIZS1itQxD0e/KctefbV65p/Vl/wPgD1/p3qveP2gDwH0zXV6e0tW3NRxNSUkVLzeVhEhq8terX/KI/+JFHWXi3J1ZNnIlmWyiB6vom0n/2zcMTLtWRE3tshaW7fTUWbYyNtMV6ki6u8TU77lbf81Cr6H4GY2XyILcm0pktcXf6F9h0BY4v3ig16OBzbjpiH2Offx52Ozogc0Scr+Kv5n410a0o72aljJE9LEVg77UCl9/H1Nnwh26iqUSpTJ/KFCSLdoiDTnBwW7ftlvPOfvQicJMHclKUGC7tf/FogTSxSZXRcnS//5dP/lOCIX5/Jrs04kJo7G4QU+6ktgLRAQoSr1Kyk0FMyqqqCd7nl3SRcO0Mbs31eJU//V5LBQ/eDgC71HPjEByF6Id6HZ9vj8="></div><div class="resizable aspect-ratio-container" style="height: 100%;">
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

