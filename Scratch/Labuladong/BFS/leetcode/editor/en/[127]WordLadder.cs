namespace Scratch.Labuladong.Algorithms.WordLadder;

//A transformation sequence from word beginWord to word endWord using a
//dictionary wordList is a sequence of words beginWord -> s1 -> s2 -> ... -> sk such that:
//
//
//
// Every adjacent pair of words differs by a single letter.
// Every si for 1 <= i <= k is in wordList. Note that beginWord does not need
//to be in wordList.
// sk == endWord
//
//
// Given two words, beginWord and endWord, and a dictionary wordList, return
//the number of words in the shortest transformation sequence from beginWord to
//endWord, or 0 if no such sequence exists.
//
//
// Example 1:
//
//
//Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog",
//"lot","log","cog"]
//Output: 5
//Explanation: One shortest transformation sequence is "hit" -> "hot" -> "dot" -
//> "dog" -> cog", which is 5 words long.
//
//
// Example 2:
//
//
//Input: beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog",
//"lot","log"]
//Output: 0
//Explanation: The endWord "cog" is not in wordList, therefore there is no
//valid transformation sequence.
//
//
//
// Constraints:
//
//
// 1 <= beginWord.length <= 10
// endWord.length == beginWord.length
// 1 <= wordList.length <= 5000
// wordList[i].length == beginWord.length
// beginWord, endWord, and wordList[i] consist of lowercase English letters.
// beginWord != endWord
// All the words in wordList are unique.
//
//
// Related TopicsHash Table | String | Breadth-First Search
//
// 👍 13437, 👎 1961bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        // 将 wordList 转换为 HashSet，加速查找
        var wordSet = new HashSet<string>(wordList);
        if (!wordSet.Contains(endWord)) return 0;
        wordSet.Remove(beginWord);

        var q = new Queue<string>();
        var visited = new HashSet<string>();
        q.Enqueue(beginWord);
        visited.Add(beginWord);

        var step = 1;
        while (q.Count > 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++)
            {
                // 穷举 curWord 修改一个字符能得到的单词
                // 即对每个字符，穷举 26 个字母
                var curWord = q.Dequeue();
                var chars = curWord.ToCharArray();
                // 开始穷举每一位字符 curWord[j]
                for (int j = 0; j < chars.Length; j++)
                {
                    var originChar = chars[j];
                    // 对每一位穷举 26 个字母
                    for (int c = 'a'; c <= 'z'; c++)
                    {
                        if (c == originChar) continue;
                        chars[j] = (char)c;
                        // 如果构成的新单词在 wordSet 中，就是找到了一个可行的下一步
                        var newWord = new string(chars);
                        if (wordSet.Contains(newWord) && !visited.Contains(newWord))
                        {
                            if (newWord == endWord) return step + 1;
                            q.Enqueue(newWord);
                            visited.Add(newWord);
                        }
                    }

                    // 最后别忘了把 curWord[j] 恢复
                    chars[j] = originChar;
                }
            }

            step++;
        }

        return 0;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
