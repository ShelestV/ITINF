using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PictureTransforming;

public partial class MainForm : Form
{
	private Bitmap? original;

	public MainForm()
	{
		InitializeComponent();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		original = new Bitmap("C:\\Users\\ShelestV\\Pictures\\Saved Pictures\\Traffalgar Lo - ava.jpg");
		Picture.Image = (Image)original.Clone();
	}

	private void RotareButton_Click(object sender, EventArgs e)
	{
		var bitmap = new Bitmap(Picture.Image.Width, Picture.Image.Height); 
		var angel = Convert.ToInt32(RotareTextBox.Text);

		var graphics = Graphics.FromImage(bitmap); 
		graphics.Clear(Color.FromArgb(0, 0, 0, 0));
		graphics.TranslateTransform(bitmap.Width / 2,bitmap.Height / 2); 
		graphics.RotateTransform(angel);
		graphics.TranslateTransform(bitmap.Width / (-2), bitmap.Height / (-2));
		graphics.DrawImage(original!, new Point());

		Picture.Image = bitmap;
	}

	private void MoveButton_Click(object sender, EventArgs e)
	{
		var parsedX = Int32.TryParse(XTextBox.Text, out var x);
		var parsedY = Int32.TryParse(YTextBox.Text, out var y);

		x = parsedX ? x : 0; 
		y = parsedY ? y : 0;

		x += Picture.Location.X;
		y += Picture.Location.Y;

		Picture.Location = new Point(x, y);
	}

	private void ScaleButton_Click(object sender, EventArgs e)
	{
		var scaleValue = ScaleTrackBar.Value;

		if (scaleValue == 5)
			return;

		const double ZeroScaleValue = 5.0;

		var scale = scaleValue switch
		{
			> 5 => 1.0 + ((scaleValue - ZeroScaleValue) / ZeroScaleValue),
			< 5 => scaleValue / ZeroScaleValue
		};

		var width = (int)Math.Round(Picture.Width * scale);
		var height = (int)Math.Round(Picture.Height * scale);

		var bitmap = new Bitmap(Picture.Image, new() 
		{ 
			Width = width, 
			Height = height 
		});

		Picture.Image = bitmap;
	}

	private void ResetButton_Click(object sender, EventArgs e)
	{
		if (original is null)
			return;
		
		Picture.Image = (Image)original.Clone();
		Picture.Location = new Point(15, 15);

		RotareTextBox.Text = "0";
		XTextBox.Text = "0";
		YTextBox.Text = "0";
	}
}
