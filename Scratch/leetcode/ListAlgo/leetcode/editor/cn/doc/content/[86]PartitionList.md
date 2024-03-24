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

<div>👍 825, 👎 0<span style='float: right;'><span style='color: gray;'><a href='https://github.com/labuladong/fucking-algorithm/discussions/939' target='_blank' style='color: lightgray;text-decoration: underline;'>bug 反馈</a> | <a href='https://labuladong.gitee.io/article/fname.html?fname=jb插件简介' target='_blank' style='color: lightgray;text-decoration: underline;'>使用指南</a> | <a href='https://labuladong.online/algo/images/others/%E5%85%A8%E5%AE%B6%E6%A1%B6.jpg' target='_blank' style='color: lightgray;text-decoration: underline;'>更多配套插件</a></span></span></div>

<div id="labuladong"><hr>

**通知：[数据结构精品课](https://labuladong.online/algo/ds-class/)
和 [递归算法专题课](https://labuladong.online/algo/tree-class/)
限时附赠网站会员；算法可视化编辑器上线，[点击体验](https://labuladong.online/algo-visualize/)！**



<p><strong><a href="https://labuladong.online/algo/slug.html?slug=partition-list" target="_blank">⭐️labuladong 题解</a></strong></p>
<details><summary><strong>labuladong 思路</strong></summary>

## 基本思路

> 本文有视频版：[链表双指针技巧全面汇总](https://www.bilibili.com/video/BV1q94y1X7vy)

这道题很像 [21. 合并两个有序链表](/problems/merge-two-sorted-lists)，21 题让你合二为一，这里需要分解让你把原链表一分为二。

具体来说，我们可以把原链表分成两个小链表，一个链表中的元素大小都小于 `x`
，另一个链表中的元素都大于等于 `x`，最后再把这两条链表接到一起，就得到了题目想要的结果。细节看代码吧，注意虚拟头结点的运用。

*
*详细题解：[双指针技巧秒杀七道链表题目](https://labuladong.online/algo/ds-class/shu-zu-lia-39fd9/lian-biao--f8c8f)
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

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🥳🥳 算法可视化 🥳🥳</strong></summary><div id="data_partition-list" data="G7VEIxH2UW46KIoKyoYoysXkbUAtE2xjWk/5HTRijZJi10bxFDsamxKDC2LpZU88/eofebRCH8fHSDy3RVi6+vxN999X/VZieMh2+8Usgxy4JAQiQOTPgwi2meoRxatJhARRfGIfCbHALDdL2Qu+SefdQ2lQrmquDBIB2fEnBhY06Qb3hh/o4EgTh/Y9anBeyHWn6P+3NxfJMpQtniqjmlEJA13meGpVuzNvcimtqVLKy8vMD6W0Daq1L1SEI8ZiBBLrwle7bg9KceI/rqk8NUBH/KrM2cqxAorwS376VMmOUF0rJ8ugurlNEZjIZhy+F6UlUoNJAy+ELscNLtkfBKFiOrmL7z6LvB3/rPfn2YREoc2P7jcMXLXp9X0WHMaTjfUVfBZbYtbzUUO4uS/96mCX/oZxCJT4Zz1J9dLzJLZrbGxqi2rrbnS0uaNVPK16VS6B5WE0N76KGVgyfXFYZ+mmoY6cA1rKHCCn+20Mux6cvnYiMTmXLw4LOyIMM5Vvg60x53PcAp+MqxENLmMgb9WqmEdcFfmsrfN54wt0/EwW5DAm59VfFkx2VSX5jCRjnvOtSUPW7RnOf1ak3/rUNAt1eR3W1InCI7pNUr60f2MmGEJIgJNM0Nr4hp2ijCWDvK3411O9M4hwoj7RX7yx11hdfSd+zSxixq51vVUvZrbO84V1/NAOJuSJLSjpBFX367EGyJjGtzyINg92wKRNIFPaNaYrY9a/lkI3PrgFS5Oa+5UFf+ykTK/DUCM/mHSQ8RQ7zF5KhOvT3Q+0p1i0p8LdwvD8AAuywateKqzvtZ2YCjUQSaVAgjaVUDt8C5IWN31eNh3tfAiBRTshhcHyMIvBqy0UqS0PyIUUV92eDEFen4AhmutWAFvxQM9NFTKNunOc3kZ/Rq7iMHtQGPxTG9fcR26Qo1fswRSxgYP8YzBeq6M4vDLgn4oRMqfRaek4rPzw82mnt1OVdaOoVOpxNmp5brhj1h3GPiZh+2s+tu2q2qYnY8h0wI2Nuf4nZOBWvkx7OpAeCAlMI+8XlL1JP4cGco13Ug5VKOFl6lGjxo/w5fLSxARWHLz/wXWiHoLdPvmRvE4pfp8DJijqBQ3PhugCAAkeNHjS9UwU/WWKY3ckVDkQvFWWyHFMTUmobBaQB8t1wisD/qmRC7QbxIK8YSsCRrlO7+KrX2j8U7Z/Nym2+7K4rNRASAnI5ezVGwmCyh0wIRQlAEn2qkmo6gUNdfsc6f3YpivlAYRixAU6yAVBRQ4o2vxfS9gNRJLgrz1o+ONjhBuzv0K9rNJASBGaRHgkFUqXDUJRApBk0BRU9YIGu4HQM255Gwt7jvrKKYIx4ltZQC4IBHWG/ecBXit46ASRJPhrDxr++Bi7IlmW6mU1DYQUoSmER1KhdNUgFCUASQZNQ1Uv6KRakP7oRNtN70p7NQjFiFuQCwNdhlfHHyX8byCSCF2Chj8mRSZjJWW1GwgpQtMIj6RC6bpBKEoAkgyagarR4184YCod7tcgICNuQS4MdBleHX9UMDhAJhGaBI1ghZn//aF/0wAZxVJdMwiMpEKppoFE4sswDy+6wOW/juxFtO7tmr5WoJ0VAE8iEd9r2KQXIH0azv+6Iew49MCP2h1hSFk/+jtpRP+YUvkBMFzxj99zaz1cNmWjWwtb7xco6RRo9DBpTFwu4R1vPTimVnu6bUFiWZHbpmOx6HtIsSwJogdnBfEd/EFy71CQ7rEE2VXRIF8mj/o+nDv6b6R5occ9HRFRvpNkk1qWFbt3nLpyT1whJ3nlpjhHvuQlHa+ZpaJHLrETMJ+loMcusxMwjlLQAxfYCZgVUxB8Bvtt5lBmaViA1V/tmZg1lZzKY3okV0ro4qtYhY3rjDhmWWdum3C/YSRjS2VaZNos3AS21Ne+d/LC8KAseEytSzvy0aq6bm1BVV258KqMDcKq8ncm6bseLa1znKk6YlytqrXKfVHl2g2HsrnvybhZKbDzu3QudQsjVa4/5rTNNBvzcGv5Nt+wj34a6fRLDFHwrVqgxL+5rdq3FaxyZAbJ9MZRb3pkOTY0+unkO/VdD8i30dEvE4WTUK4B+RI3lHnjqEvsa5G65C71gNhiP2o9O34L9ypTUzcetqITBzRA4yV1k9giCghHwGAxJxyX96OxscwZIS6blJpVbBHXvcOmstrzz2JWzAY2xCOpOFivQNnDj3KOoNKE67gfs9SSc1DltiY5ajtbFAEUa+vKHJF2kMUe7Z4XZJZYLwfwZwt70mMxhpnZA8rKNW17p/GEC50hadveadwx3WMAYyg6zQC7OvxglcU0rZfDfk+av5CKOt9w+NN5XZkt3Thlp7M88etPX/bYm9WdrsVHJ6RyYqctijsoDxfj3JepxFcTJXROL/bntU5OHDi3PdzaPPl5MPsMS62k7/waUpGJDQlk15X/0j2uA9ywchssBl+88H1k8BAv16bCsx9KfFBqd2YAqnBiMgk87eu2dFZ9+RHslffqcAr/5+Oo5gWsP8xWJnv0UfCXtIRim5C78lThyx4OxvvURsN1RUg6e1GCs9FavCTLTrxJonTEtQaSm36GGVGNwmvz7SnE4i26NbmmrBe7cBmvHFh8RuTUmm9lz1qC3a3PPQvss5BURVF71g/J0XmvBN3Zjv0EDn6sF020YLKFX3/60n2XYy/B0ao8PWKN+gpDkIjtM9CVZCYCd6ySSK1TzjiNOWK7mpyRJ90ezdBKfBhH/XYU9s+2Ej3gBbo+csvNjRZzaiuw25q8dIWyLRI5W4WMmRb11ud69jto2/fiQypsmhe2pX9vNpa9P8M28oiR7Yh/QLCCuG/X6rfQz6siFgzpEGsKh+VAghWDwwIhwXrAofxLUO35I6LB8yMUdQ59nOO1m0O75lCiOXRljldiDs2XQ8Hl0GM5Xlc5tFIO5ZNDx+R4leTQGDkUQ4b3P1j2tNbb3zRQ2TNTctmZY7P8L6EHGCQ2bmwo+ejyAeevAd1ZCUwhHSiJCSSrcVwYP6IacZQQkiOBOKoRRwkhORqIoxpxlBCSY4E4qhFHCSE5HoijGnGUEJITgTiqEUcJIbkuEKd3IRv/XbYEdjnUmCnADXHclq859u8vO1X+v/+k8wC2zdmZUX9GYW9+Dv42esrfqNRGa9lhAtLVRxG+1tv93grVh511A3MYEvN7oS/+uKpPPJMWL9aOYqHhdSyugXhcLHEyaf2Efrk2q0nRFAlxupTubnEJO1p5SdgxsnQQ1c0Hcb+XnNV8QKjDf1mnZ98XkTwtzsXpb6PnoEtxC/qcdODpL5Gjg4bS7z9n9Yhb8aDhXEi8RRJSpCRIWuhNTDVLRv/NC3sEJQztbyP+7IcVD+c+L1abIlHIYbBSvF/+evEQ8Z+MC/lh7jhPPIHaELo93Ubcwve42JAh8ylkvKXT7asRzh8g8t3Gf1nEHNKa8753d/vadl2eID+QWig8Xc8dO311iVGvjQLZWbSYQ/SOF0QnkS74IDNC9Fv4WQFfi4x9LEpMoElRYu7BkhnS0sXEC3M0q5g1m1+WsU8XdKhYKfJd+CFZgiNdnDkc8tAXL3nyZWQl+11twqy/9HnWof/7rZ84gCEQAR/VH1W3FM4iiC077MKaW00lzJAeODMmpsz5k2MieoyzBTi/WY/i0MDUpgmMTCfTNfLymg2ioZruQsj1oZD1lV6eLvPxsb1BfN6jpxs="></div><div class="resizable aspect-ratio-container" style="height: 100%;">
<div id="iframe_partition-list"></div></div>
</details><hr /><br />

如果你不断开原链表中的每个节点的 `next` 指针就会出错，因为结果链表中会包含一个环：

<hr /><details open hint-container details><summary style="font-size: medium"><strong>🍭🍭 算法可视化 🍭🍭</strong></summary><div id="data_mydata-partition-list" data="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"></div><div class="resizable aspect-ratio-container" style="height: 100%;">
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

