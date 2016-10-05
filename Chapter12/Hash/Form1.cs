using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace Hash
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtHash;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCompute;
		private System.Windows.Forms.TextBox txtMD5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtSHA1;
		private System.Windows.Forms.TextBox txtSHA256;
		private System.Windows.Forms.TextBox txtSHA384;
		private System.Windows.Forms.TextBox txtSHA512;
		private System.Windows.Forms.GroupBox grpHash;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCompute = new System.Windows.Forms.Button();
			this.txtHash = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpHash = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMD5 = new System.Windows.Forms.TextBox();
			this.txtSHA1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSHA256 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtSHA384 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtSHA512 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.grpHash.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnCompute,
																					this.txtHash,
																					this.label1});
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(576, 64);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Clear Text Value:";
			// 
			// btnCompute
			// 
			this.btnCompute.Location = new System.Drawing.Point(440, 24);
			this.btnCompute.Name = "btnCompute";
			this.btnCompute.Size = new System.Drawing.Size(128, 23);
			this.btnCompute.TabIndex = 2;
			this.btnCompute.Text = "&Compute Hash";
			this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
			// 
			// txtHash
			// 
			this.txtHash.Location = new System.Drawing.Point(80, 24);
			this.txtHash.Name = "txtHash";
			this.txtHash.Size = new System.Drawing.Size(352, 20);
			this.txtHash.TabIndex = 1;
			this.txtHash.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Value:";
			// 
			// grpHash
			// 
			this.grpHash.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.txtSHA512,
																				  this.label6,
																				  this.txtSHA384,
																				  this.label5,
																				  this.txtSHA256,
																				  this.label4,
																				  this.txtSHA1,
																				  this.label3,
																				  this.txtMD5,
																				  this.label2});
			this.grpHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpHash.Location = new System.Drawing.Point(0, 72);
			this.grpHash.Name = "grpHash";
			this.grpHash.Size = new System.Drawing.Size(576, 152);
			this.grpHash.TabIndex = 1;
			this.grpHash.TabStop = false;
			this.grpHash.Text = "Computed Hash Value:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "MD5:";
			// 
			// txtMD5
			// 
			this.txtMD5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtMD5.Location = new System.Drawing.Point(80, 24);
			this.txtMD5.Name = "txtMD5";
			this.txtMD5.Size = new System.Drawing.Size(488, 20);
			this.txtMD5.TabIndex = 2;
			this.txtMD5.Text = "";
			// 
			// txtSHA1
			// 
			this.txtSHA1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSHA1.Location = new System.Drawing.Point(80, 48);
			this.txtSHA1.Name = "txtSHA1";
			this.txtSHA1.Size = new System.Drawing.Size(488, 20);
			this.txtSHA1.TabIndex = 4;
			this.txtSHA1.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "SHA1:";
			// 
			// txtSHA256
			// 
			this.txtSHA256.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSHA256.Location = new System.Drawing.Point(80, 72);
			this.txtSHA256.Name = "txtSHA256";
			this.txtSHA256.Size = new System.Drawing.Size(488, 20);
			this.txtSHA256.TabIndex = 6;
			this.txtSHA256.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "SHA256:";
			// 
			// txtSHA384
			// 
			this.txtSHA384.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSHA384.Location = new System.Drawing.Point(80, 96);
			this.txtSHA384.Name = "txtSHA384";
			this.txtSHA384.Size = new System.Drawing.Size(488, 20);
			this.txtSHA384.TabIndex = 8;
			this.txtSHA384.Text = "";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "SHA384:";
			// 
			// txtSHA512
			// 
			this.txtSHA512.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSHA512.Location = new System.Drawing.Point(80, 120);
			this.txtSHA512.Name = "txtSHA512";
			this.txtSHA512.Size = new System.Drawing.Size(488, 20);
			this.txtSHA512.TabIndex = 10;
			this.txtSHA512.Text = "";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(51, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "SHA512:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 229);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.grpHash,
																		  this.groupBox1});
			this.Name = "Form1";
			this.Text = "Hashing with .NET";
			this.groupBox1.ResumeLayout(false);
			this.grpHash.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnCompute_Click(object sender, System.EventArgs e)
		{
			if (txtHash.Text.Trim() != "")
			{
				//Generate bytes our the input string
				byte[] bInputData = ASCIIEncoding.ASCII.GetBytes(txtHash.Text);

				//Display the hash value in textbox
				txtMD5.Text = ASCIIEncoding.ASCII.GetString(new MD5CryptoServiceProvider().ComputeHash(bInputData));
				txtSHA1.Text = ASCIIEncoding.ASCII.GetString(new SHA1Managed().ComputeHash(bInputData));
				txtSHA256.Text = ASCIIEncoding.ASCII.GetString(new SHA256Managed().ComputeHash(bInputData));
				txtSHA384.Text = ASCIIEncoding.ASCII.GetString(new SHA384Managed().ComputeHash(bInputData));
				txtSHA512.Text = ASCIIEncoding.ASCII.GetString(new SHA512Managed().ComputeHash(bInputData));
			}
		}
	}
}
