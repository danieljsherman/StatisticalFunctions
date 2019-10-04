using Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace StatisticsTests
{
	public class SampleDataTests
	{
		[Fact]
		public void SampleListInitialized()
		{
			SampleData data = new SampleData(100);
		}

		[Fact]
		public void SampleListAccessible()
		{
			SampleData data = new SampleData(100);
			List<int> sampleList = data.Sample;
		}

		[Fact]
		public void SampleIsCorrectSize()
		{
			SampleData data = new SampleData(100);
			List<int> sample = data.Sample;
			Assert.Equal(100, sample.Count);
		}

		[Fact]
		public void HasArray()
		{
			SampleData data = new SampleData(100);
			int[] sampleListArray = data.SampleArray;
		}

		[Fact]
		public void ArrayHasSameValuesAsList()
		{
			SampleData data = new SampleData(100);
			
			for(int i=0; i < 100; i++)
			{
				Assert.Equal(data.Sample[i], data.SampleArray[i]);
			}
		}

		[Fact]
		public void HasSortedList()
		{
			SampleData data = new SampleData(100);
			int[] sortedSample = data.SortedSample;
		}

		[Fact]
		public void SortListHasSameElements()
		{
			SampleData data = new SampleData(100);
			int[] sortedSample = data.SortedSample;
			int[] sampleListArray = data.SampleArray;

			int[] sortedArray = new int[sampleListArray.Length];
			sampleListArray.CopyTo(sortedArray, 0);
			Array.Sort(sortedArray);

			for(int i = 0; i < sampleListArray.Length; i++)
			{
				Assert.Equal(sortedSample, sortedArray);
			}
		}

		[Theory]
		[InlineData(new int[] { 1, 2, 3, 4, 5, 6})]
		public void TestMedianWithEvenNumberOfElements(int[] values)
		{
			double ret = values.Median();
			Assert.Equal(3.5, ret);
		}

		[Theory]
		[InlineData(new int[] { 1, 2, 3, 4, 5 })]
		public void TestMedianWithOddNumberOfElements(int[] values)
		{
			double ret = values.Median();
			Assert.Equal(3.0, ret);
		}

		[Fact]
		public void TestTruncatedMeanWithNumber()
		{
			int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			int numberToDiscard = 2;
			double expectedResult = 5.5;

			double ret = values.TruncatedMean(numberToDiscard);
			Assert.Equal(expectedResult, ret);
		}

		[Fact]
		public void TestTruncatedMeanWithPercent()
		{
			int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			double percentToDiscard = 0.2;
			double expectedResult = 5.5;

			double ret = values.TruncatedMean(percentToDiscard);
			Assert.Equal(expectedResult, ret);
		}

		[Fact]
		public void TestStDevSample()
		{
			int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			double result = values.StDev(true);
			Assert.Equal(3.028, Math.Round(result, 3));
		}

		[Fact]
		public void TestModeSize()
		{
			int[] values = new int[] { 1, 1, 2, 3, 4, 5 };
			List<int> modes = values.Modes();

			Assert.Single(modes);
		}

		[Fact]
		public void TestSingleMode()
		{
			int[] values = new int[] { 1, 1, 2, 3, 4, 5 };
			List<int> modes = values.Modes();
			Assert.Equal(1, modes[0]);
		}
	}
}
