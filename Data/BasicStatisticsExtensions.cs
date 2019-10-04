using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public static class BasicStatisticsExtensions
	{
		public static double TruncatedMean<T>(this IEnumerable<T> list, int numberToDiscard)
		{
			IEnumerable<double> doubleList = list.Select(value => Convert.ToDouble(value));
			double[] listArray = doubleList.ToArray();
			Array.Sort(listArray);
			int startIndex = numberToDiscard;
			int howManyElementsToCopy = listArray.Length - (numberToDiscard * 2);
			double[] newArray = new double[howManyElementsToCopy];
			Array.Copy(listArray, startIndex, newArray, 0, howManyElementsToCopy);
			return newArray.Average();
		}

		public static double TruncatedMean<T>(this IEnumerable<T> list, double percentToDiscard)
		{
			int numberToDiscard = (int)(list.Count() * percentToDiscard);

			return TruncatedMean(list, numberToDiscard);
		}

		public static double Median<T>(this IEnumerable<T> list)
		{
			double[] listArray = list.Select(item => Convert.ToDouble(item)).ToArray();
			Array.Sort(listArray);

			bool oddNumberOfValues = (listArray.Length % 2 == 1);
			if (oddNumberOfValues)
			{
				return listArray[listArray.Length / 2];
			}

			int secondIndex = listArray.Length / 2;
			int firstIndex = secondIndex - 1;

			return (listArray[secondIndex] + listArray[firstIndex]) / 2;
		}

		public static double StDev<T>(this IEnumerable<T> list, bool asSample)
		{
			double[] items = list.Select(item => Convert.ToDouble(item)).ToArray();
			double mean = items.Average();
			double tally = 0;

			for(int i = 0; i < items.Length; i++)
			{
				tally += Math.Pow((items[i] - mean), 2);
			}

			if(asSample)
			{
				return Math.Sqrt(tally / (items.Length - 1));
			}

			return Math.Sqrt(tally / items.Length);
		}

		public static List<T> Modes<T>(this IEnumerable<T> list)
		{
			Dictionary<T, int> ocurrences = list.GetBins();

			int maxCount = ocurrences.Values.Max();
			List<T> modes = new List<T>();

			foreach(KeyValuePair<T, int> pair in ocurrences)
			{
				if(pair.Value == maxCount)
				{
					modes.Add(pair.Key);
				}
			}

			return modes;
		}

		public static Dictionary<T, int> GetBins<T>(this IEnumerable<T> list)
		{
			Dictionary<T, int> bins = new Dictionary<T, int>();

			foreach(T item in list)
			{
				if(bins.ContainsKey(item))
				{
					bins[item]++;
				}
				else
				{
					bins.Add(item, 1);
				}
			}

			return bins;
		}
	}
}
