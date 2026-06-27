namespace Scratch.Labuladong.Algorithms.CompleteBinaryTreeInserter;

// 919. Complete Binary Tree Inserter (Medium)
//
// A complete binary tree is a binary tree in which every level, except possibly the last, is
// completely filled, and all nodes are as far left as possible.
//
// Design an algorithm to insert a new node to a complete binary tree keeping it complete after the
// insertion.
//
// Implement the CBTInserter class:
//
// - CBTInserter(TreeNode root) Initializes the data structure with the root of the complete binary
// tree.
//
// - int insert(int v) Inserts a TreeNode into the tree with value Node.val == val so that the tree
// remains complete, and returns the value of the parent of the inserted TreeNode.
//
// - TreeNode get_root() Returns the root node of the tree.
//
// Example 1:
//
// Input
// ["CBTInserter", "insert", "insert", "get_root"]
// [[[1, 2]], [3], [4], []]
// Output
// [null, 1, 2, [1, 2, 3, 4]]
//
// Explanation
// CBTInserter cBTInserter = new CBTInserter([1, 2]);
// cBTInserter.insert(3); // return 1
// cBTInserter.insert(4); // return 2
// cBTInserter.get_root(); // return [1, 2, 3, 4]
//
// Constraints:
//
// - The number of nodes in the tree will be in the range [1, 1000].
//
// - 0 <= Node.val <= 5000
//
// - root is a complete binary tree.
//
// - 0 <= val <= 5000
//
// - At most 10^4 calls will be made to insert and get_root.
//
// Related Topics: Tree, Breadth-First Search, Design, Binary Tree

//leetcode submit region begin(Prohibit modification and deletion)
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class CBTInserter
{
    // 这个队列只记录完全二叉树底部可以进行插入的节点
    private Queue<TreeNode> q = new();
    private TreeNode root;

    public CBTInserter(TreeNode root)
    {
        this.root = root;
        // 进行普通的 BFS，目的是找到底部可插入的节点
        var temp = new Queue<TreeNode>();
        temp.Enqueue(root);
        while (temp.Count > 0)
        {
            var cur = temp.Dequeue();
            if (cur.left != null) temp.Enqueue(cur.left);
            if (cur.right != null) temp.Enqueue(cur.right);
            // 找到完全二叉树底部可以进行插入的节点
            if (cur.right == null || cur.left == null) q.Enqueue(cur);
        }
    }

    public int Insert(int val)
    {
        var node = new TreeNode(val);
        var cur = q.Peek();

        if (cur.left == null)
        {
            cur.left = node;
        }
        else if (cur.right == null)
        {
            // 右孩子也填上了，cur 已满，从队列移除
            cur.right = node;
            q.Dequeue();
        }

        // 新节点的左右节点也是可以插入的
        q.Enqueue(node);
        return cur.val;
    }

    public TreeNode Get_root()
    {
        return root;
    }
}

/**
 * Your CBTInserter object will be instantiated and called as such:
 * CBTInserter obj = new CBTInserter(root);
 * int param_1 = obj.Insert(val);
 * TreeNode param_2 = obj.Get_root();
 */
//leetcode submit region end(Prohibit modification and deletion)
