using System;
using System.Collections.Generic;

namespace Data
{
	public class SampleData
	{
		private List<int> _dataValues;

		public SampleData(int sampleSize)
		{
			_dataValues = new List<int>(sampleSize);
			InitializeSample(sampleSize);
		}

		public List<int> Sample => _dataValues;

		public int[] SampleArray => _dataValues.ToArray();

		public int[] SortedSample
		{
			get
			{
				int[] tempArray = new int[_dataValues.Count];
				SampleArray.CopyTo(tempArray, 0);
				Array.Sort(tempArray);
				return tempArray;
			}
		}

		private void InitializeSample(int sampleSize)
		{
			Random rnd = new Random();

			for(int i = 0; i < sampleSize; i++)
			{
				_dataValues.Add(rnd.Next(1, 13));
			}
		}


	}
}
