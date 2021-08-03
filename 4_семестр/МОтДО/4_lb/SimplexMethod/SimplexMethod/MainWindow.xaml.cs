using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimplexMethod
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int numberOfUndefineds;
		private int numberOfExpressions;

		public MainWindow()
		{
			InitializeComponent();
		}

		private TextBox GetTextBox(double marginWidth, double marginHeight)
		{
			TextBox textBox = new TextBox();
			textBox.Foreground = Brushes.LawnGreen;
			textBox.Background = Brushes.Black;
			textBox.FontSize = 14;
			textBox.TextAlignment = TextAlignment.Right;
			textBox.Width = 20;
			textBox.Height = 20;
			textBox.VerticalAlignment = VerticalAlignment.Top;
			textBox.HorizontalAlignment = HorizontalAlignment.Left;
			textBox.BorderThickness = new Thickness(1);
			textBox.BorderBrush = Brushes.LawnGreen;
			textBox.Margin = new Thickness(marginWidth, marginHeight, 0, 0);
			return textBox;
		}

		private Label GetLabel(string content, double marginLeft, double marginTop)
		{
			Label label = new Label();
			label.Content = content;
			label.FontSize = 16;
			label.VerticalContentAlignment = VerticalAlignment.Center;
			label.HorizontalContentAlignment = HorizontalAlignment.Center;
			label.Foreground = Brushes.LawnGreen;
			label.VerticalAlignment = VerticalAlignment.Top;
			label.HorizontalAlignment = HorizontalAlignment.Left;
			label.Margin = new Thickness(marginLeft, marginTop, 0, 0);
			return label;
		}

		private ComboBox GetComboBox(List<string> content, double width, double marginLeft, double marginTop)
		{
			ComboBox comboBox = new ComboBox();
			comboBox.Foreground = Brushes.Black;
			comboBox.ItemsSource = content;
			comboBox.SelectedItem = content[0];
			comboBox.FontSize = 14;
			comboBox.VerticalContentAlignment = VerticalAlignment.Top;
			comboBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			comboBox.Width = width;
			comboBox.Height = 25;
			comboBox.VerticalAlignment = VerticalAlignment.Top;
			comboBox.HorizontalAlignment = HorizontalAlignment.Left;
			comboBox.Margin = new Thickness(marginLeft, marginTop, 0, 0);
			return comboBox;
		}

		private void GetInputExpressionTableButton_Click(object sender, RoutedEventArgs e)
		{
			if (Rules.IsCorrectUnsignedInt(NumberOfUndefinedsTextBox.Text, out numberOfUndefineds) &&
				Rules.IsCorrectUnsignedInt(NumberOfEqationsTextBox.Text, out numberOfExpressions))
			{
				// Objective function
				FunctionGrid.Children.Add(GetTextBox(5, 5));
				FunctionGrid.Children.Add(GetLabel("x1", 25, 0));
				for (int j = 1; j < numberOfUndefineds; ++j)
				{
					FunctionGrid.Children.Add(GetLabel("+", 60 * j - 15, 0));
					FunctionGrid.Children.Add(GetTextBox(20 + 60 * j - 15, 5));
					FunctionGrid.Children.Add(GetLabel("x" + (j + 1).ToString(), 40 + 60 * j - 15, 0));
				}
				#region Arrow
				FunctionGrid.Children.Add(new Line()
				{
					X1 = 5 + 60 * numberOfUndefineds - 15,
					Y1 = 15,
					X2 = 45 + 60 * numberOfUndefineds - 15,
					Y2 = 15,
					Stroke = Brushes.LawnGreen
				});
				FunctionGrid.Children.Add(new Line()
				{
					X1 = 25 + 60 * numberOfUndefineds - 15,
					Y1 = 10,
					X2 = 45 + 60 * numberOfUndefineds - 15,
					Y2 = 15,
					Stroke = Brushes.LawnGreen
				});
				FunctionGrid.Children.Add(new Line()
				{
					X1 = 25 + 60 * numberOfUndefineds - 15,
					Y1 = 20,
					X2 = 45 + 60 * numberOfUndefineds - 15,
					Y2 = 15,
					Stroke = Brushes.LawnGreen
				});
				#endregion
				FunctionGrid.Children.Add(GetComboBox(new List<string> { "min", "max" }, 70, 50 + 60 * numberOfUndefineds - 15, 0));
				// System of expressions
				for (int i = 0; i < numberOfExpressions; ++i)
				{
					ExpressionsGrid.Children.Add(GetTextBox(5, i * 40));
					ExpressionsGrid.Children.Add(GetLabel("x1", 25, i * 40 - 5));
					for (int j = 1; j < numberOfUndefineds; ++j)
					{
						ExpressionsGrid.Children.Add(GetLabel("+", 60 * j - 15, i * 40 - 5));
						ExpressionsGrid.Children.Add(GetTextBox(20 + 60 * j - 15, i * 40));
						ExpressionsGrid.Children.Add(GetLabel("x" + (j + 1).ToString(), 40 + 60 * j - 15, i * 40 - 5));
					}
					ExpressionsGrid.Children.Add(GetComboBox(new List<string> { "=", "≤", "≥" }, 40, 60 * numberOfUndefineds - 10, i * 40));
					ExpressionsGrid.Children.Add(GetTextBox(50 + 60 * numberOfUndefineds - 15, i * 40));
				}

				CalculateByJordaneGaussMethodButton.Visibility = Visibility.Visible;
				ObjectiveFunctionLabel.Visibility = Visibility.Visible;
			}

			NumberOfUndefinedsTextBox.Text = "";
			NumberOfEqationsTextBox.Text = "";
		}

		private void CalculateByJordaneGaussMethodButton_Click(object sender, RoutedEventArgs e)
		{
			int indexOfFunctionUndefined = 0;
			Objective objective = Objective.Min;
			List<Undefined> functionUndefineds = new List<Undefined>();

			foreach (var component in FunctionGrid.Children)
			{
				if (component.GetType() == NumberOfEqationsTextBox.GetType())
				{
					int number;
					if (!Rules.IsCorrectInt(((TextBox)component).Text, out number))
					{
						MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					functionUndefineds.Add(new Undefined(indexOfFunctionUndefined, number));
					++indexOfFunctionUndefined;
				}
				else if (component.GetType() == GetComboBox(new List<string> { "" }, 0, 0, 0).GetType())
				{
					if (((ComboBox)component).SelectedItem == null ||
						string.IsNullOrEmpty(((ComboBox)component).SelectedItem.ToString()))
					{
						MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					switch (((ComboBox)component).SelectedItem.ToString())
					{
						case "min":
							objective = Objective.Min;
							break;
						case "max":
							objective = Objective.Max;
							break;
						default:
							MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
							return;
					}
				}
			}
			var F = new ObjectiveFunction(functionUndefineds, objective);

			int indexOfUndefined = 0;
			int indexOfExpression = 0;

			List<Expression> expressions = new List<Expression>();
			List<List<Undefined>> undefineds = new List<List<Undefined>>();
			Sign sign = Sign.Equal;

			for (int i = 0; i < numberOfExpressions; ++i)
			{
				undefineds.Add(new List<Undefined>());
			}

			foreach (var component in ExpressionsGrid.Children)
			{
				if (component.GetType() == NumberOfEqationsTextBox.GetType())
				{
					int number;
					if (!Rules.IsCorrectInt(((TextBox)component).Text, out number))
					{
						MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					if (indexOfUndefined != numberOfUndefineds)
					{
						undefineds[indexOfExpression].Add(new Undefined(indexOfUndefined, number));
						++indexOfUndefined;
					}
					else if (indexOfUndefined == numberOfUndefineds)
					{
						expressions.Add(new Expression(undefineds[indexOfExpression], new Free(number), sign));
						indexOfUndefined = 0;
						++indexOfExpression;
					}
				}
				else if (component.GetType() == GetComboBox(new List<string> { "" }, 0, 0, 0).GetType())
				{
					if (((ComboBox)component).SelectedItem == null ||
						string.IsNullOrEmpty(((ComboBox)component).SelectedItem.ToString()))
					{
						MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					switch (((ComboBox)component).SelectedItem.ToString())
					{
						case "=":
							sign = Sign.Equal;
							break;
						case "≤":
							sign = Sign.EqualOrLess;
							break;
						case "≥":
							sign = Sign.EqualOrGreater;
							break;
						default:
							MessageBox.Show("Input correct values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
							return;
					}
				}
			}

			var simplex = new Simplex(expressions, F);
			simplex.Operate();

			var lastIteration = simplex.GetLastIteration();

			int numberOfRows = lastIteration.Count;
			int numberOfColums = lastIteration[0].Count;

			for (int i = 0; i < numberOfRows; ++i)
			{
				var row = new RowDefinition();
				row.Height = new GridLength(35);
				LastIterationGrid.RowDefinitions.Add(row);
			}
			for (int i = 0; i < numberOfColums; ++i)
			{
				var column = new ColumnDefinition();
				column.Width = new GridLength(60);
				LastIterationGrid.ColumnDefinitions.Add(column);
			}
			LastIterationGrid.ColumnDefinitions.Add(new ColumnDefinition()); // Result

			for (int i = 0; i < numberOfRows; ++i)
			{
				for (int j = 0; j < numberOfColums; ++j)
				{
					var label = new Label();
					label.Content = lastIteration[i][j];
					label.FontSize = 16;
					label.BorderThickness = new Thickness(1);
					label.BorderBrush = Brushes.LawnGreen;
					label.HorizontalContentAlignment = HorizontalAlignment.Center;
					label.VerticalContentAlignment = VerticalAlignment.Center;
					label.Background = Brushes.Black;
					label.Foreground = Brushes.LawnGreen;
					Grid.SetRow(label, i);
					Grid.SetColumn(label, j);
					LastIterationGrid.Children.Add(label);
				}
			}

			var resultLabel = new Label();
			resultLabel.Content = simplex.GetSolution();
			resultLabel.FontSize = 16;
			resultLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
			resultLabel.VerticalContentAlignment = VerticalAlignment.Center;
			resultLabel.Background = Brushes.Black;
			resultLabel.Foreground = Brushes.LawnGreen;
			Grid.SetColumn(resultLabel, numberOfColums);
			Grid.SetRow(resultLabel, 0);
			Grid.SetRowSpan(resultLabel, numberOfRows);
			LastIterationGrid.Children.Add(resultLabel);
		}
	}
}
