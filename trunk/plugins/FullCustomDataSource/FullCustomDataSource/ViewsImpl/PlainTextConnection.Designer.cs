namespace FullCustomDataSource.ViewsImpl
{
	partial class PlainTextConnection
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.btnSearchFile = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cbFieldDelimiter = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbTextQualifier = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select File:";
			// 
			// txtFileName
			// 
			this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.txtFileName.Location = new System.Drawing.Point(135, 3);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(291, 20);
			this.txtFileName.TabIndex = 1;
			// 
			// btnSearchFile
			// 
			this.btnSearchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchFile.Location = new System.Drawing.Point(432, 1);
			this.btnSearchFile.Name = "btnSearchFile";
			this.btnSearchFile.Size = new System.Drawing.Size(27, 23);
			this.btnSearchFile.TabIndex = 3;
			this.btnSearchFile.Text = "...";
			this.btnSearchFile.UseVisualStyleBackColor = true;
			this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Field Delimiter:";
			// 
			// cbFieldDelimiter
			// 
			this.cbFieldDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFieldDelimiter.FormattingEnabled = true;
			this.cbFieldDelimiter.Location = new System.Drawing.Point(135, 19);
			this.cbFieldDelimiter.Name = "cbFieldDelimiter";
			this.cbFieldDelimiter.Size = new System.Drawing.Size(153, 21);
			this.cbFieldDelimiter.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Text Qualifier:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                              | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.cbTextQualifier);
			this.groupBox1.Controls.Add(this.cbFieldDelimiter);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(0, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(459, 81);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "File Settings";
			// 
			// cbTextQualifier
			// 
			this.cbTextQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTextQualifier.FormattingEnabled = true;
			this.cbTextQualifier.Location = new System.Drawing.Point(135, 46);
			this.cbTextQualifier.Name = "cbTextQualifier";
			this.cbTextQualifier.Size = new System.Drawing.Size(153, 21);
			this.cbTextQualifier.TabIndex = 7;
			// 
			// PlainTextConnection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSearchFile);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.label1);
			this.Name = "PlainTextConnection";
			this.Size = new System.Drawing.Size(462, 163);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnSearchFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbFieldDelimiter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbTextQualifier;
	}
}