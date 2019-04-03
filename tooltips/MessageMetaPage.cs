using SharpShell.SharpPropertySheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tooltips
{
	public partial class MessageMetaPage : SharpPropertyPage
	{
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.TextBox textBox;
		public MessageMetaPage()
		{
			InitializeComponent();
			PageTitle = "Message Meta";
		}
		private void InitializeComponent()
		{
			this.label = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.clearButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.Location = new System.Drawing.Point(3, 0);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(45, 15);
			this.label.TabIndex = 0;
			this.label.Text = "memo";
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(3, 18);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(400, 24);
			this.textBox.TabIndex = 1;
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(411, 18);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(23, 23);
			this.clearButton.TabIndex = 2;
			this.clearButton.Text = "X";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// MessageMetaPage
			// 
			this.CausesValidation = false;
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.label);
			this.Controls.Add(this.textBox);
			this.Name = "MessageMetaPage";
			this.Size = new System.Drawing.Size(438, 390);
			this.Load += new System.EventHandler(this.MessageMetaPage_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			textBox.Text = "";
		}

		private void MessageMetaPage_Load(object sender, EventArgs e)
		{
			this.ActiveControl = this.textBox;
		}
	}
}
