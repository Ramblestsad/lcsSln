namespace Scratch.DataStructure;
public class MaxHeap
{
    private int Left(int i)
    {
        return 2 * i + 1;
    }

    private int Right(int i)
    {
        return 2 * i + 2;
    }

    private int Parent(int i)
    {
        return ( i - 1 ) / 2; // 向下整除
    }
}
