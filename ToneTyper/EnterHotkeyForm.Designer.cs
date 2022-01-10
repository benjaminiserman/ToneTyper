namespace ToneTyper;

partial class EnterHotkeyForm : Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterHotkeyForm));
			this.HotkeyTextBox = new System.Windows.Forms.TextBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// HotkeyTextBox
			// 
			this.HotkeyTextBox.Location = new System.Drawing.Point(17, 20);
			this.HotkeyTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.HotkeyTextBox.Name = "HotkeyTextBox";
			this.HotkeyTextBox.Size = new System.Drawing.Size(234, 31);
			this.HotkeyTextBox.TabIndex = 0;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(261, 18);
			this.SubmitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(107, 38);
			this.SubmitButton.TabIndex = 1;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// EnterHotkeyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 77);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.HotkeyTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "EnterHotkeyForm";
			this.Text = "Enter Hotkey";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private TextBox HotkeyTextBox;
	private Button SubmitButton;
}
