using System.Windows;
using TransportProblem.Windows;

namespace TransportProblem
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void PotentialMethodButton_Click(object sender, RoutedEventArgs e)
		{
			var pmMethod = new PotentialMethodWindow(this);
			pmMethod.Show();
			this.Hide();
		}

		private void AssignmentProblemButton_Click(object sender, RoutedEventArgs e)
		{
			var aWindow = new AssignmentProblemWindow(this);
			aWindow.Show();
			this.Hide();
		}
	}
}
