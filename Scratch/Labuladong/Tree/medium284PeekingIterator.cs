namespace Scratch.Labuladong.Algorithms.PeekingIterator;

//leetcode submit region begin(Prohibit modification and deletion)
// C# IEnumerator interface reference:
// https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=netframework-4.8

class PeekingIterator
{
    private IEnumerator<int> e;
    private bool hasNext;

    // iterators refers to the first element of the array.
    public PeekingIterator(IEnumerator<int> iterator)
    {
        // initialize any member here.
        e = iterator;
        hasNext = true;
    }

    // Returns the next element in the iteration without advancing the iterator.
    public int Peek()
    {
        return e.Current;
    }

    // Returns the next element in the iteration and advances the iterator.
    public int Next()
    {
        var cur = e.Current;
        hasNext = e.MoveNext();

        return cur;
    }

    // Returns false if the iterator is referring to the end of the array of true otherwise.
    public bool HasNext()
    {
        return hasNext;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
