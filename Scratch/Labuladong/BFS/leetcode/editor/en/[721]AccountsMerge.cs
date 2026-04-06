/*
 * @lc app=leetcode id=721 lang=csharp
 * @lcpr version=30402
 *
 * [721] Accounts Merge
 */

namespace Scratch.Labuladong.Algorithms.AccountsMerge;

//Given a list of accounts where each element accounts[i] is a list of strings,
//where the first element accounts[i][0] is a name, and the rest of the elements
//are emails representing emails of the account.
//
// Now, we would like to merge these accounts. Two accounts definitely belong
//to the same person if there is some common email to both accounts. Note that even
//if two accounts have the same name, they may belong to different people as
//people could have the same name. A person can have any number of accounts initially,
//but all of their accounts definitely have the same name.
//
// After merging the accounts, return the accounts in the following format: the
//first element of each account is the name, and the rest of the elements are
//emails in sorted order. The accounts themselves can be returned in any order.
//
//
// Example 1:
//
//
//Input: accounts = [["John","johnsmith@mail.com","john_newyork@mail.com"],[
//"John","johnsmith@mail.com","john00@mail.com"],["Mary","mary@mail.com"],["John",
//"johnnybravo@mail.com"]]
//Output: [["John","john00@mail.com","john_newyork@mail.com","johnsmith@mail.
//com"],["Mary","mary@mail.com"],["John","johnnybravo@mail.com"]]
//Explanation:
//The first and second John's are the same person as they have the common email
//"johnsmith@mail.com".
//The third John and Mary are different people as none of their email addresses
//are used by other accounts.
//We could return these lists in any order, for example the answer [['Mary',
//'mary@mail.com'], ['John', 'johnnybravo@mail.com'],
//['John', 'john00@mail.com', 'john_newyork@mail.com', 'johnsmith@mail.com']]
//would still be accepted.
//
//
// Example 2:
//
//
//Input: accounts = [["Gabe","Gabe0@m.co","Gabe3@m.co","Gabe1@m.co"],["Kevin",
//"Kevin3@m.co","Kevin5@m.co","Kevin0@m.co"],["Ethan","Ethan5@m.co","Ethan4@m.co",
//"Ethan0@m.co"],["Hanzo","Hanzo3@m.co","Hanzo1@m.co","Hanzo0@m.co"],["Fern","Fern5@
//m.co","Fern1@m.co","Fern0@m.co"]]
//Output: [["Ethan","Ethan0@m.co","Ethan4@m.co","Ethan5@m.co"],["Gabe","Gabe0@m.
//co","Gabe1@m.co","Gabe3@m.co"],["Hanzo","Hanzo0@m.co","Hanzo1@m.co","Hanzo3@m.
//co"],["Kevin","Kevin0@m.co","Kevin3@m.co","Kevin5@m.co"],["Fern","Fern0@m.co",
//"Fern1@m.co","Fern5@m.co"]]
//
//
//
// Constraints:
//
//
// 1 <= accounts.length <= 1000
// 2 <= accounts[i].length <= 10
// 1 <= accounts[i][j].length <= 30
// accounts[i][0] consists of English letters.
// accounts[i][j] (for j > 0) is a valid email.
//
//
// Related TopicsArray | Hash Table | String | Depth-First Search | Breadth-
//First Search | Union-Find | Sorting
//
// 👍 7648, 👎 1305bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        // key: email, value: 出现该 email 的 account 的索引列表
        var emailToIndexes = new Dictionary<string, List<int>>();
        for (int i = 0; i < accounts.Count; i++)
        {
            var account = accounts[i];
            for (int j = 1; j < account.Count; j++)
            {
                var email = account[j];
                if (!emailToIndexes.TryGetValue(email, out var indexes))
                {
                    indexes = [];
                    emailToIndexes[email] = indexes;
                }

                indexes.Add(i);
            }
        }
        // {"john@abc.com": [0, 1], ...}

        // 计算合并后的账户
        List<IList<string>> res = [];
        var visitedEmails = new HashSet<string>();

        foreach (var email in emailToIndexes.Keys)
        {
            if (visitedEmails.Contains(email)) continue;

            // 合并账户，用 BFS 算法穷举所有和 email 相关联的邮箱
            var mergedEmails = new List<string>();
            var q = new Queue<string>();
            q.Enqueue(email);
            visitedEmails.Add(email);

            while (q.Count > 0)
            {
                var curEmail = q.Dequeue();
                mergedEmails.Add(curEmail);
                emailToIndexes.TryGetValue(curEmail, out var indexes);
                foreach (var index in indexes!)
                {
                    var account = accounts[index];
                    for (int j = 1; j < account.Count; j++)
                    {
                        var nextEmail = account[j];
                        if (!visitedEmails.Contains(nextEmail))
                        {
                            q.Enqueue(nextEmail);
                            visitedEmails.Add(nextEmail);
                        }
                    }
                }
            }

            var userName = accounts[emailToIndexes[email][0]][0];
            // mergedEmail 是 userName 的所有邮箱
            mergedEmails.Sort(StringComparer.Ordinal);
            mergedEmails.Insert(0, userName);
            res.Add(mergedEmails);
        }

        return res;
    }
}
// @lc code=end
