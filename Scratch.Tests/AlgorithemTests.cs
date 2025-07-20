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
        Sort.QuickSort(input, 0, input.Length - 1);

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

    [Fact]
    public void TestMergeSort() {
        // Arrange
        var input = new int[] { 64, 34, 25, 12, 22, 11, 90 };
        var expected = new int[] { 11, 12, 22, 25, 34, 64, 90 };

        // Act
        Sort.MergeSort(input, 0, input.Length - 1);

        // Assert
        Assert.Equal(expected, input);

        // 测试边界情况
        var emptyNums = new int[] { };
        Sort.MergeSort(emptyNums, 0, -1);
        Assert.Equal(new int[] { }, emptyNums);

        var oneElemNums = new int[] { 1 };
        Sort.MergeSort(oneElemNums, 0, 0);
        Assert.Equal(new int[] { 1 }, oneElemNums);

        var twoElemNums = new int[] { 2, 1 };
        Sort.MergeSort(twoElemNums, 0, 1);
        Assert.Equal(new int[] { 1, 2 }, twoElemNums);
    }

    [Fact]
    public void TestHeapSort() {
        // Arrange
        var input = new int[] { 64, 34, 25, 12, 22, 11, 90 };
        var expected = new int[] { 11, 12, 22, 25, 34, 64, 90 };

        // Act
        Sort.HeapSort(input);

        // Assert
        Assert.Equal(expected, input);

        // 测试边界情况
        var emptyNums = new int[] { };
        Sort.HeapSort(emptyNums);
        Assert.Equal(new int[] { }, emptyNums);

        var oneElemNums = new int[] { 1 };
        Sort.HeapSort(oneElemNums);
        Assert.Equal(new int[] { 1 }, oneElemNums);

        var twoElemNums = new int[] { 2, 1 };
        Sort.HeapSort(twoElemNums);
        Assert.Equal(new int[] { 1, 2 }, twoElemNums);
    }

    [Fact]
    public void TestBucketSort() {
        // Arrange - 生成1000个随机的0-1之间的浮点数
        var random = new Random(23);
        var input = new float[1000];
        for (var i = 0; i < input.Length; i++) {
            input[i] = (float)random.NextDouble();
        }

        // 创建期望结果（排序后的数组）
        var expected = new float[input.Length];
        Array.Copy(input, expected, input.Length);
        Array.Sort(expected);

        // Act
        Sort.BucketSort(input);

        // Assert
        Assert.Equal(expected, input);

        // 测试边界情况
        var emptyNums = new float[] { };
        Sort.BucketSort(emptyNums);
        Assert.Equal(new float[] { }, emptyNums);

        var oneElemNums = new float[] { 0.5f };
        Sort.BucketSort(oneElemNums);
        Assert.Equal(new float[] { 0.5f }, oneElemNums);

        var twoElemNums = new float[] { 0.8f, 0.2f };
        Sort.BucketSort(twoElemNums);
        Assert.Equal(new float[] { 0.2f, 0.8f }, twoElemNums);

        // 测试包含重复元素的情况
        var duplicateNums = new float[] { 0.1f, 0.5f, 0.1f, 0.9f, 0.5f };
        var expectedDuplicate = new float[] { 0.1f, 0.1f, 0.5f, 0.5f, 0.9f };
        Sort.BucketSort(duplicateNums);
        Assert.Equal(expectedDuplicate, duplicateNums);
    }
}
