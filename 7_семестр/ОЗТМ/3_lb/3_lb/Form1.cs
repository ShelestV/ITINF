namespace _3_lb;

public partial class Form1 : Form
{
	private readonly FractalsHelper fractalsHelper;

	public Form1()
	{
		InitializeComponent();

		fractalsHelper = new FractalsHelper(Picture.Size);
	}

	private void DrawFractalsButton_Click(object sender, EventArgs e)
	{
		var bitmap = new Bitmap(Picture.Width, Picture.Height);
		var graphics = Graphics.FromImage(bitmap);

		var iterations = Convert.ToInt32(FractalIterationsTextBox.Text);
		this.fractalsHelper.Draw(graphics, iterations);

		Picture.Image = bitmap;
	}
}
