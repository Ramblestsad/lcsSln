using Scratch.Algorithms;
using Scratch.DataStructure;

namespace Scratch.Tests;

public class AlgorithmsTests: IDisposable {
    public void Dispose() {
        // Do some clean-up for every test
    }

    [Fact]
    public void TestQuickSort() {
        // Arrange
        var input = new int[] { 64, 34, 25, 12, 22, 11, 90 };
        var expected = new int[] { 11, 12, 22, 25, 34, 64, 90 };

        // Act
        var left = 0;
        var right = input.Length - 1;
        Sort.QuickSort(input, left, right);

        // Assert
        Assert.Equal(expected, input);

        // 测试边界情况
        var emptyNums = new int[] { };
        Sort.QuickSort(emptyNums, 0, 0);
        Assert.Equal(new int[] { }, emptyNums);

        var oneElemNums = new int[] { 1 };
        Sort.QuickSort(oneElemNums, 0, 0);
        Assert.Equal(new int[] { 1 }, oneElemNums);

        var twoElemNums = new int[] { 2, 1 };
        Sort.QuickSort(twoElemNums, 0 , 1);
        Assert.Equal(new int[] { 1, 2 }, twoElemNums);
    }
}
