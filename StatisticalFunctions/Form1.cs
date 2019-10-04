using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace StatisticalFunctions
{
	public partial class Form1 : Telerik.WinControls.UI.RadForm
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void BtnGo_Click(object sender, EventArgs e)
		{
			if (!Int32.TryParse(txtNumValues.Text, out int numVals)) throw new Exception("Invalid number of values");

			SampleData data = new SampleData(100);
			lstListItems.DataSource = data.SampleArray;
			lstSortedItems.DataSource = data.SortedSample;

			if (!Double.TryParse(txtDiscardFraction.Text, out double discard)) throw new Exception("Bad discard value.");

			txtMin.Text = data.Sample.Min().ToString("0.00");
			txtMax.Text = data.Sample.Max().ToString("0.00");
			txtMean.Text = data.Sample.Average().ToString("0.00");
			txtMedian.Text = data.Sample.Median().ToString("0.00");
			txtMode.Text = string.Join(", ", data.Sample.Modes().ConvertAll(item => item.ToString()));
			txtTruncMean.Text = data.Sample.TruncatedMean(discard).ToString("0.00");
			txtStDevSample.Text = data.Sample.StDev(true).ToString("0.00");
			txtStdDevPop.Text = data.Sample.StDev(false).ToString("0.00");
			txtMinArray.Text = data.SampleArray.Min().ToString("0.00");
			txtMaxArray.Text = data.SampleArray.Max().ToString("0.00");
			txtMeanArray.Text = data.SampleArray.Average().ToString("0.00");
			txtMedianArray.Text = data.SampleArray.Median().ToString("0.00");
			txtModeArray.Text = string.Join(", ", data.SampleArray.Modes().ConvertAll(item => item.ToString()));
			txtTruncMeanArray.Text = data.SampleArray.TruncatedMean(discard).ToString("0.00");
			txtStdDevSampleArray.Text = data.SampleArray.StDev(true).ToString("0.00");
			txtStdDevPopArray.Text = data.SampleArray.StDev(false).ToString("0.00");

			CreateHistogram(data);
		}

		private void CreateHistogram(SampleData data)
		{
			int[] counts = new int[13];
			for(int i = 0; i < 100; i++)
			{
				counts[data.Sample[i]]++;
			}

			int bottom = lbl1.Bottom;
			int maxHeight = bottom - txtNumValues.Top;
			float scale = maxHeight / (float)counts.Max();

			RadLabel[] labels = { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10, lbl11, lbl12 };
			for(int i = 0; i < 12; i++)
			{
				labels[i].Height = (int)(counts[i + 1] * scale);
				labels[i].Top = bottom - labels[i].Height;
				labels[i].Text = counts[i + 1].ToString();
			}
		}

		private int GenerateRandomNumber(Random rnd)
		{
			return rnd.Next(1, 13);
		}
	}
}
