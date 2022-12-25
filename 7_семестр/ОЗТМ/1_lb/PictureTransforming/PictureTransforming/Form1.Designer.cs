namespace PictureTransforming;

partial class MainForm
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
			this.RotareTextBox = new System.Windows.Forms.TextBox();
			this.RotareButton = new System.Windows.Forms.Button();
			this.XTextBox = new System.Windows.Forms.TextBox();
			this.YTextBox = new System.Windows.Forms.TextBox();
			this.XLabel = new System.Windows.Forms.Label();
			this.YLabel = new System.Windows.Forms.Label();
			this.MoveButton = new System.Windows.Forms.Button();
			this.ScaleTrackBar = new System.Windows.Forms.TrackBar();
			this.ScaleButton = new System.Windows.Forms.Button();
			this.ResetButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScaleTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(15, 15);
			this.Picture.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(510, 539);
			this.Picture.TabIndex = 0;
			this.Picture.TabStop = false;
			// 
			// RotareTextBox
			// 
			this.RotareTextBox.Location = new System.Drawing.Point(566, 28);
			this.RotareTextBox.Name = "RotareTextBox";
			this.RotareTextBox.Size = new System.Drawing.Size(246, 23);
			this.RotareTextBox.TabIndex = 1;
			this.RotareTextBox.Text = "0";
			// 
			// RotareButton
			// 
			this.RotareButton.Location = new System.Drawing.Point(818, 28);
			this.RotareButton.Name = "RotareButton";
			this.RotareButton.Size = new System.Drawing.Size(75, 23);
			this.RotareButton.TabIndex = 2;
			this.RotareButton.Text = "Rotare";
			this.RotareButton.UseVisualStyleBackColor = true;
			this.RotareButton.Click += new System.EventHandler(this.RotareButton_Click);
			// 
			// XTextBox
			// 
			this.XTextBox.Location = new System.Drawing.Point(586, 69);
			this.XTextBox.Name = "XTextBox";
			this.XTextBox.Size = new System.Drawing.Size(100, 23);
			this.XTextBox.TabIndex = 3;
			this.XTextBox.Text = "0";
			// 
			// YTextBox
			// 
			this.YTextBox.Location = new System.Drawing.Point(712, 69);
			this.YTextBox.Name = "YTextBox";
			this.YTextBox.Size = new System.Drawing.Size(100, 23);
			this.YTextBox.TabIndex = 4;
			this.YTextBox.Text = "0";
			// 
			// XLabel
			// 
			this.XLabel.AutoSize = true;
			this.XLabel.Location = new System.Drawing.Point(566, 72);
			this.XLabel.Name = "XLabel";
			this.XLabel.Size = new System.Drawing.Size(14, 15);
			this.XLabel.TabIndex = 5;
			this.XLabel.Text = "X";
			// 
			// YLabel
			// 
			this.YLabel.AutoSize = true;
			this.YLabel.Location = new System.Drawing.Point(692, 72);
			this.YLabel.Name = "YLabel";
			this.YLabel.Size = new System.Drawing.Size(14, 15);
			this.YLabel.TabIndex = 6;
			this.YLabel.Text = "Y";
			// 
			// MoveButton
			// 
			this.MoveButton.Location = new System.Drawing.Point(818, 68);
			this.MoveButton.Name = "MoveButton";
			this.MoveButton.Size = new System.Drawing.Size(75, 23);
			this.MoveButton.TabIndex = 7;
			this.MoveButton.Text = "Move";
			this.MoveButton.UseVisualStyleBackColor = true;
			this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
			// 
			// ScaleTrackBar
			// 
			this.ScaleTrackBar.Location = new System.Drawing.Point(566, 112);
			this.ScaleTrackBar.Name = "ScaleTrackBar";
			this.ScaleTrackBar.Size = new System.Drawing.Size(246, 45);
			this.ScaleTrackBar.TabIndex = 8;
			this.ScaleTrackBar.Value = 5;
			// 
			// ScaleButton
			// 
			this.ScaleButton.Location = new System.Drawing.Point(818, 112);
			this.ScaleButton.Name = "ScaleButton";
			this.ScaleButton.Size = new System.Drawing.Size(75, 23);
			this.ScaleButton.TabIndex = 9;
			this.ScaleButton.Text = "Scale";
			this.ScaleButton.UseVisualStyleBackColor = true;
			this.ScaleButton.Click += new System.EventHandler(this.ScaleButton_Click);
			// 
			// ResetButton
			// 
			this.ResetButton.Location = new System.Drawing.Point(692, 179);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new System.Drawing.Size(75, 23);
			this.ResetButton.TabIndex = 10;
			this.ResetButton.Text = "Reset";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 561);
			this.Controls.Add(this.ResetButton);
			this.Controls.Add(this.ScaleButton);
			this.Controls.Add(this.ScaleTrackBar);
			this.Controls.Add(this.MoveButton);
			this.Controls.Add(this.YLabel);
			this.Controls.Add(this.XLabel);
			this.Controls.Add(this.YTextBox);
			this.Controls.Add(this.XTextBox);
			this.Controls.Add(this.RotareButton);
			this.Controls.Add(this.RotareTextBox);
			this.Controls.Add(this.Picture);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "MainForm";
			this.Text = "Picture Transforming";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ScaleTrackBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private PictureBox Picture;
	private TextBox RotareTextBox;
	private Button RotareButton;
	private TextBox XTextBox;
	private TextBox YTextBox;
	private Label XLabel;
	private Label YLabel;
	private Button MoveButton;
	private TrackBar ScaleTrackBar;
	private Button ScaleButton;
	private Button ResetButton;
}
