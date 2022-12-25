namespace _3_lb;

partial class Form1
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.Picture = new System.Windows.Forms.PictureBox();
			this.FractalIterationsTextBox = new System.Windows.Forms.TextBox();
			this.DrawFractalsButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.SuspendLayout();
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(0, 57);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(800, 568);
			this.Picture.TabIndex = 0;
			this.Picture.TabStop = false;
			// 
			// FractalIterationsTextBox
			// 
			this.FractalIterationsTextBox.Location = new System.Drawing.Point(12, 12);
			this.FractalIterationsTextBox.Name = "FractalIterationsTextBox";
			this.FractalIterationsTextBox.Size = new System.Drawing.Size(125, 27);
			this.FractalIterationsTextBox.TabIndex = 1;
			this.FractalIterationsTextBox.Text = "1";
			// 
			// DrawFractalsButton
			// 
			this.DrawFractalsButton.Location = new System.Drawing.Point(153, 11);
			this.DrawFractalsButton.Name = "DrawFractalsButton";
			this.DrawFractalsButton.Size = new System.Drawing.Size(94, 29);
			this.DrawFractalsButton.TabIndex = 2;
			this.DrawFractalsButton.Text = "Draw";
			this.DrawFractalsButton.UseVisualStyleBackColor = true;
			this.DrawFractalsButton.Click += new System.EventHandler(this.DrawFractalsButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(922, 739);
			this.Controls.Add(this.DrawFractalsButton);
			this.Controls.Add(this.FractalIterationsTextBox);
			this.Controls.Add(this.Picture);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private PictureBox Picture;
	private TextBox FractalIterationsTextBox;
	private Button DrawFractalsButton;
}
