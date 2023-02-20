namespace PaymentSystem.Administration;

partial class EmployeesForm
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
			this.employeesDataGridView = new System.Windows.Forms.DataGridView();
			this.addButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.employeesDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// employeesDataGridView
			// 
			this.employeesDataGridView.AllowUserToAddRows = false;
			this.employeesDataGridView.AllowUserToDeleteRows = false;
			this.employeesDataGridView.AllowUserToResizeRows = false;
			this.employeesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.employeesDataGridView.Location = new System.Drawing.Point(12, 44);
			this.employeesDataGridView.MultiSelect = false;
			this.employeesDataGridView.Name = "employeesDataGridView";
			this.employeesDataGridView.ReadOnly = true;
			this.employeesDataGridView.RowHeadersWidth = 51;
			this.employeesDataGridView.RowTemplate.Height = 29;
			this.employeesDataGridView.Size = new System.Drawing.Size(776, 394);
			this.employeesDataGridView.TabIndex = 0;
			this.employeesDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EmployeesDataGridRowClick);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(507, 10);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(81, 26);
			this.addButton.TabIndex = 1;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.AddButtonClick);
			// 
			// updateButton
			// 
			this.updateButton.Location = new System.Drawing.Point(594, 9);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(94, 29);
			this.updateButton.TabIndex = 2;
			this.updateButton.Text = "Update";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.UpdateButtonClick);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(694, 9);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(94, 29);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.DeleteButtonClick);
			// 
			// EmployeesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.employeesDataGridView);
			this.Name = "EmployeesForm";
			this.Text = "Employees";
			this.Load += new System.EventHandler(this.EmployeesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.employeesDataGridView)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion

	private DataGridView employeesDataGridView;
	private Button addButton;
	private Button updateButton;
	private Button deleteButton;
}