namespace Scratch.Labuladong.Algorithms.MinimumGeneticMutation;

// 433. Minimum Genetic Mutation (Medium)
//
// A gene string can be represented by an 8-character long string, with choices from 'A', 'C', 'G',
// and 'T'.
//
// Suppose we need to investigate a mutation from a gene string startGene to a gene string endGene
// where one mutation is defined as one single character changed in the gene string.
//
// - For example, "AACCGGTT" --> "AACCGGTA" is one mutation.
//
// There is also a gene bank bank that records all the valid gene mutations. A gene must be in bank
// to make it a valid gene string.
//
// Given the two gene strings startGene and endGene and the gene bank bank, return the minimum
// number of mutations needed to mutate from startGene to endGene. If there is no such a mutation,
// return -1.
//
// Note that the starting point is assumed to be valid, so it might not be included in the bank.
//
// Example 1:
//
// Input: startGene = "AACCGGTT", endGene = "AACCGGTA", bank = ["AACCGGTA"]
// Output: 1
//
// Example 2:
//
// Input: startGene = "AACCGGTT", endGene = "AAACGGTA", bank = ["AACCGGTA","AACCGCTA","AAACGGTA"]
// Output: 2
//
// Constraints:
//
// - 0 <= bank.length <= 10
//
// - startGene.length == endGene.length == bank[i].length == 8
//
// - startGene, endGene, and bank[i] consist of only the characters ['A', 'C', 'G', 'T'].
//
// Related Topics: Hash Table, String, Breadth-First Search

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinMutation(string startGene, string endGene, string[] bank)
    {
        var bankSet = new HashSet<string>(bank);
        if (!bankSet.Contains(endGene)) return -1;

        var q = new Queue<string>();
        var visited = new HashSet<string>();
        q.Enqueue(startGene);
        visited.Add(startGene);

        var step = 0;
        while (q.Count > 0)
        {
            var sz = q.Count;
            for (int i = 0; i < sz; i++)
            {
                var cur = q.Dequeue();
                if (cur == endGene) return step;
                // 向周围扩散
                foreach (var newGene in _getAllMutation(cur))
                {
                    if (!visited.Contains(newGene) && bankSet.Contains(newGene))
                    {
                        q.Enqueue(newGene);
                        visited.Add(newGene);
                    }
                }
            }

            step++;
        }

        return -1;
    }

    // 当前基因的每个位置都可以变异为 A/G/C/T，穷举所有可能的结构
    private List<string> _getAllMutation(string gene)
    {
        var res = new List<string>();
        var geneChars = gene.ToCharArray();
        for (int i = 0; i < geneChars.Length; i++)
        {
            var oldChar = geneChars[i];
            foreach (char newChar in new[] { 'A', 'G', 'C', 'T' })
            {
                geneChars[i] = newChar;
                res.Add(new string(geneChars));
            }

            geneChars[i] = oldChar;
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
