namespace Scratch.Labuladong.Algorithms.PermutationsII;

// 47. Permutations II (Medium)
//
// Given a collection of numbers, nums, that might contain duplicates, return all possible unique
// permutations in any order.
//
// Example 1:
//
// Input: nums = [1,1,2]
// Output:
// [[1,1,2],
// [1,2,1],
// [2,1,1]]
//
// Example 2:
//
// Input: nums = [1,2,3]
// Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
//
// Constraints:
//
// - 1 <= nums.length <= 8
//
// - -10 <= nums[i] <= 10
//
// Related Topics: Array, Backtracking, Sorting

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public List<IList<int>> res = new();

    public List<int> track = new();

    public bool[] used = null!;

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        // 先排序，让相同的元素靠在一起
        Array.Sort(nums);
        used = new bool[nums.Length];
        _backtrack(nums);
        return res;
    }

    void _backtrack(int[] nums)
    {
        if (track.Count == nums.Length)
        {
            res.Add(new List<int>(track));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i]) continue;

            // 新添加的剪枝逻辑，固定相同的元素在排列中的相对位置
            // 当出现重复元素时，比如输入 nums = [1,2,2',2'']，
            // 2' 只有在 2 已经被使用的情况下才会被选择，
            // 同理，2'' 只有在 2' 已经被使用的情况下才会被选择，这就保证了相同元素在排列中的相对位置保证固定。
            if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]) continue;

            track.Add(nums[i]);
            used[i] = true;

            _backtrack(nums);

            used[i] = false;
            track.RemoveAt(track.Count - 1);
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
