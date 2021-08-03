using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TransportProblem.Models;
using TransportProblem.Rools;

namespace TransportProblem.Windows
{
	/// <summary>
	/// Логика взаимодействия для AssignmentProblemWindow.xaml
	/// </summary>
	public partial class AssignmentProblemWindow : Window
	{
		private int numberOfRows;
		private int numberOfColumns;

		private List<List<TextBox>> transportProblemTextBoxes;

		private Window previousWindow;

		public AssignmentProblemWindow(Window previous)
		{
			InitializeComponent();

			previousWindow = previous;
			transportProblemTextBoxes = new List<List<TextBox>>();
		}

		private void GetTransportProblemTableButton_Click(object sender, RoutedEventArgs e)
		{
			if (Rool.IsCorrectUnsignedInt(RowsTextBox.Text, out numberOfRows) &&
				Rool.IsCorrectUnsignedInt(ColumnsTextBox.Text, out numberOfColumns))
			{
				for (int i = 0; i < numberOfRows; ++i)
				{
					var row = new RowDefinition();
					GridLength height = new GridLength(30);
					row.Height = height;
					TransportProblemGrid.RowDefinitions.Add(row);
				}

				for (int j = 0; j < numberOfColumns; ++j)
				{
					var column = new ColumnDefinition();
					GridLength width = new GridLength(50);
					column.Width = width;
					TransportProblemGrid.ColumnDefinitions.Add(column);
				}

				MainGrid.RowDefinitions[1].Height = new GridLength(30 * numberOfRows);

				for (int i = 0; i < numberOfRows; ++i)
				{
					var row = new List<TextBox>();
					for (int j = 0; j < numberOfColumns; ++j)
					{
						row.Add(GetTextBox(i, j));
						TransportProblemGrid.Children.Add(row[j]);
					}
					transportProblemTextBoxes.Add(row);
				}

				CalculateOptimalPlanButton.Visibility = Visibility.Visible;
			}

			RowsTextBox.Text = "";
			ColumnsTextBox.Text = "";
		}

		private void CalculateOptimalPlanButton_Click(object sender, RoutedEventArgs e)
		{
			var tarrifs = new Tarrifs(numberOfRows, numberOfColumns);
			var vacancies = new Warehouses(numberOfRows);
			var candidates = new Clients(numberOfColumns);

			for (int i = 0; i < transportProblemTextBoxes.Count; ++i)
				vacancies[i] = 1;

			for (int j = 0; j < transportProblemTextBoxes[0].Count; ++j)
				candidates[j] = 1;

			for (int i = 0; i < transportProblemTextBoxes.Count; ++i)
			{
				for (int j = 0; j < transportProblemTextBoxes[i].Count; ++j)
				{
					int value;
					if (!Rool.IsCorrectUnsignedInt(transportProblemTextBoxes[i][j].Text, out value))
						return;

					tarrifs[i, j] = new Tarrif(value, i, j);
				}
			}

			var tp = new TransportationProblem(tarrifs, vacancies, candidates);
			var optimalePlan = tp.GetOptimalPlanByMinimalElementsMethod();

			for (int i = 0; i < optimalePlan.NumberOfRows; ++i)
			{
				var row = new RowDefinition();
				GridLength height = new GridLength(30);
				row.Height = height;
				ResultGrid.RowDefinitions.Add(row);
			}

			for (int j = 0; j < optimalePlan.NumberOfColumns; ++j)
			{
				var column = new ColumnDefinition();
				GridLength width = new GridLength(50);
				column.Width = width;
				ResultGrid.ColumnDefinitions.Add(column);
			}

			F.Text = optimalePlan.GetF();

			for (int i = 0; i < optimalePlan.NumberOfRows; ++i)
				for (int j = 0; j < optimalePlan.NumberOfColumns; ++j)
					ResultGrid.Children.Add(GetTextBlock(optimalePlan[i, j].ToString(), i, j));

			ResultText.Text = optimalePlan.ToAssignmentProblemString();

			OptimalePlanLabel.Visibility = Visibility.Visible;
		}

		private TextBox GetTextBox(int row, int column)
		{
			TextBox textBox = new TextBox();
			textBox.Style = (Style)textBox.FindResource("BGThemeTextBox");
			textBox.Width = 50;
			textBox.Height = 30;
			Grid.SetRow(textBox, row);
			Grid.SetColumn(textBox, column);
			return textBox;
		}

		private TextBlock GetTextBlock(string text, int row, int column)
		{
			TextBlock textBlock = new TextBlock();
			textBlock.Style = (Style)textBlock.FindResource("BGThemeTextBlock");
			textBlock.Width = 50;
			textBlock.Height = 30;
			textBlock.Text = text;
			textBlock.TextAlignment = TextAlignment.Center;
			Grid.SetRow(textBlock, row);
			Grid.SetColumn(textBlock, column);
			return textBlock;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			previousWindow.Show();
		}
	}
}
