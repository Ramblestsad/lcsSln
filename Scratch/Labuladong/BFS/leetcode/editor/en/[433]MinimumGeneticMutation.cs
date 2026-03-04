namespace Scratch.Labuladong.Algorithms.MinimumGeneticMutation;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinMutation(string startGene, string endGene, string[] bank)
    {
        var bankSet = new HashSet<string>(bank);
        if (!bankSet.Contains(endGene)) return -1;
        char[] AGCT = ['A', 'G', 'C', 'T'];

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
