namespace PaymentSystem.Administration;

partial class SaveEmployeeForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
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
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			System.Windows.Forms.Label paymentLabel;
			System.Windows.Forms.Label dollarSignLabel;
			System.Windows.Forms.Label nameLabel;
			this.isHourlyPayedEmployeeCheckBox = new System.Windows.Forms.CheckBox();
			this.paymentUpDown = new System.Windows.Forms.NumericUpDown();
			this.saveButton = new System.Windows.Forms.Button();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.workedHoursLabel = new System.Windows.Forms.Label();
			this.workedHoursUpDown = new System.Windows.Forms.NumericUpDown();
			this.hoursSymbolLabel = new System.Windows.Forms.Label();
			paymentLabel = new System.Windows.Forms.Label();
			dollarSignLabel = new System.Windows.Forms.Label();
			nameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.paymentUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.workedHoursUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// paymentLabel
			// 
			paymentLabel.AutoSize = true;
			paymentLabel.Location = new System.Drawing.Point(12, 76);
			paymentLabel.Name = "paymentLabel";
			paymentLabel.Size = new System.Drawing.Size(65, 20);
			paymentLabel.TabIndex = 2;
			paymentLabel.Text = "Payment";
			// 
			// dollarSignLabel
			// 
			dollarSignLabel.AutoSize = true;
			dollarSignLabel.Location = new System.Drawing.Point(196, 73);
			dollarSignLabel.Name = "dollarSignLabel";
			dollarSignLabel.Size = new System.Drawing.Size(17, 20);
			dollarSignLabel.TabIndex = 4;
			dollarSignLabel.Text = "$";
			// 
			// nameLabel
			// 
			nameLabel.AutoSize = true;
			nameLabel.Location = new System.Drawing.Point(12, 9);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(49, 20);
			nameLabel.TabIndex = 6;
			nameLabel.Text = "Name";
			// 
			// isHourlyPayedEmployeeCheckBox
			// 
			this.isHourlyPayedEmployeeCheckBox.AutoSize = true;
			this.isHourlyPayedEmployeeCheckBox.Location = new System.Drawing.Point(12, 39);
			this.isHourlyPayedEmployeeCheckBox.Name = "isHourlyPayedEmployeeCheckBox";
			this.isHourlyPayedEmployeeCheckBox.Size = new System.Drawing.Size(201, 24);
			this.isHourlyPayedEmployeeCheckBox.TabIndex = 0;
			this.isHourlyPayedEmployeeCheckBox.Text = "Is hourly payed employee";
			this.isHourlyPayedEmployeeCheckBox.UseVisualStyleBackColor = true;
			this.isHourlyPayedEmployeeCheckBox.CheckedChanged += new System.EventHandler(this.IsHourlyPayedCheckedChanged);
			// 
			// paymentUpDown
			// 
			this.paymentUpDown.DecimalPlaces = 2;
			this.paymentUpDown.Location = new System.Drawing.Point(83, 71);
			this.paymentUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.paymentUpDown.Name = "paymentUpDown";
			this.paymentUpDown.Size = new System.Drawing.Size(107, 27);
			this.paymentUpDown.TabIndex = 3;
			this.paymentUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(113, 161);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(100, 30);
			this.saveButton.TabIndex = 5;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(65, 6);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(148, 27);
			this.nameTextBox.TabIndex = 7;
			// 
			// workedHoursLabel
			// 
			this.workedHoursLabel.AutoSize = true;
			this.workedHoursLabel.Location = new System.Drawing.Point(12, 106);
			this.workedHoursLabel.Name = "workedHoursLabel";
			this.workedHoursLabel.Size = new System.Drawing.Size(60, 20);
			this.workedHoursLabel.TabIndex = 8;
			this.workedHoursLabel.Text = "Worked";
			// 
			// workedHoursUpDown
			// 
			this.workedHoursUpDown.Location = new System.Drawing.Point(83, 104);
			this.workedHoursUpDown.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
			this.workedHoursUpDown.Name = "workedHoursUpDown";
			this.workedHoursUpDown.Size = new System.Drawing.Size(107, 27);
			this.workedHoursUpDown.TabIndex = 9;
			this.workedHoursUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// hoursSymbolLabel
			// 
			this.hoursSymbolLabel.AutoSize = true;
			this.hoursSymbolLabel.Location = new System.Drawing.Point(196, 106);
			this.hoursSymbolLabel.Name = "hoursSymbolLabel";
			this.hoursSymbolLabel.Size = new System.Drawing.Size(45, 20);
			this.hoursSymbolLabel.TabIndex = 10;
			this.hoursSymbolLabel.Text = "hours";
			// 
			// SaveEmployeeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 203);
			this.Controls.Add(this.hoursSymbolLabel);
			this.Controls.Add(this.workedHoursUpDown);
			this.Controls.Add(this.workedHoursLabel);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(nameLabel);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(dollarSignLabel);
			this.Controls.Add(this.paymentUpDown);
			this.Controls.Add(paymentLabel);
			this.Controls.Add(this.isHourlyPayedEmployeeCheckBox);
			this.Name = "SaveEmployeeForm";
			this.Text = "Add Employee";
			((System.ComponentModel.ISupportInitialize)(this.paymentUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.workedHoursUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private CheckBox isHourlyPayedEmployeeCheckBox;
	private NumericUpDown paymentUpDown;
	private Button saveButton;
	private TextBox nameTextBox;
	private Label workedHoursLabel;
	private NumericUpDown workedHoursUpDown;
	private Label hoursSymbolLabel;
}