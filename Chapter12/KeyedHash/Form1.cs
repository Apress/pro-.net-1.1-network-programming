using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace MD5Auth
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox grpLogin;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtHMACSHA1;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.TextBox txtClearText;
		private System.Windows.Forms.Button btnCompute;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtMACTripleDES;
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
			this.grpLogin = new System.Windows.Forms.GroupBox();
			this.txtKey = new System.Windows.Forms.TextBox();
			this.txtClearText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnCompute = new System.Windows.Forms.Button();
			this.txtHMACSHA1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMACTripleDES = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.grpLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpLogin
			// 
			this.grpLogin.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.txtKey,
																				   this.txtClearText,
																				   this.label2,
																				   this.label1});
			this.grpLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpLogin.Location = new System.Drawing.Point(8, 8);
			this.grpLogin.Name = "grpLogin";
			this.grpLogin.Size = new System.Drawing.Size(360, 80);
			this.grpLogin.TabIndex = 10;
			this.grpLogin.TabStop = false;
			this.grpLogin.Text = "Clear Text:";
			// 
			// txtKey
			// 
			this.txtKey.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtKey.Location = new System.Drawing.Point(56, 48);
			this.txtKey.MaxLength = 16;
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new System.Drawing.Size(296, 23);
			this.txtKey.TabIndex = 8;
			this.txtKey.Text = "";
			// 
			// txtClearText
			// 
			this.txtClearText.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClearText.Location = new System.Drawing.Point(56, 24);
			this.txtClearText.MaxLength = 500;
			this.txtClearText.Name = "txtClearText";
			this.txtClearText.Size = new System.Drawing.Size(296, 23);
			this.txtClearText.TabIndex = 7;
			this.txtClearText.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Key:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Text:";
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnClose.Location = new System.Drawing.Point(280, 96);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 32);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "C&lose";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnCompute
			// 
			this.btnCompute.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnCompute.Location = new System.Drawing.Point(184, 96);
			this.btnCompute.Name = "btnCompute";
			this.btnCompute.Size = new System.Drawing.Size(88, 32);
			this.btnCompute.TabIndex = 8;
			this.btnCompute.Text = "&Compute";
			this.btnCompute.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// txtHMACSHA1
			// 
			this.txtHMACSHA1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtHMACSHA1.Location = new System.Drawing.Point(8, 144);
			this.txtHMACSHA1.MaxLength = 50;
			this.txtHMACSHA1.Multiline = true;
			this.txtHMACSHA1.Name = "txtHMACSHA1";
			this.txtHMACSHA1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtHMACSHA1.Size = new System.Drawing.Size(360, 48);
			this.txtHMACSHA1.TabIndex = 13;
			this.txtHMACSHA1.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 15);
			this.label3.TabIndex = 12;
			this.label3.Text = "HMACSHA1:";
			// 
			// txtMACTripleDES
			// 
			this.txtMACTripleDES.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtMACTripleDES.Location = new System.Drawing.Point(8, 216);
			this.txtMACTripleDES.MaxLength = 50;
			this.txtMACTripleDES.Multiline = true;
			this.txtMACTripleDES.Name = "txtMACTripleDES";
			this.txtMACTripleDES.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMACTripleDES.Size = new System.Drawing.Size(360, 48);
			this.txtMACTripleDES.TabIndex = 15;
			this.txtMACTripleDES.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 200);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 15);
			this.label4.TabIndex = 14;
			this.label4.Text = "MACTripleDES:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 269);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.txtMACTripleDES,
																		  this.label4,
																		  this.txtHMACSHA1,
																		  this.label3,
																		  this.btnClose,
																		  this.grpLogin,
																		  this.btnCompute});
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Exploring Keyed Hash";
			this.grpLogin.ResumeLayout(false);
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
		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			if (txtClearText.Text.Trim() != "" && txtKey.Text.Trim() != "")
				ProcessKeyedHash(txtClearText.Text, txtKey.Text);
		}

		void ProcessKeyedHash(string sInput, string sKey)
		{
			try
			{
				//Generate bytes our the input string
				byte[] bInputData = ASCIIEncoding.ASCII.GetBytes(sInput);
				byte[] bKey = new byte[16];
				bKey = ASCIIEncoding.ASCII.GetBytes(sKey);

				//Compute HMACSHA1
				HMACSHA1 objHmac = new HMACSHA1(bKey);
				CryptoStream objCs = new CryptoStream(Stream.Null, objHmac, CryptoStreamMode.Write);
				objCs.Write(bInputData, 0, bInputData.Length);
				objCs.Close();

				txtHMACSHA1.Text = ASCIIEncoding.ASCII.GetString(objHmac.Hash);

				//Compute the MACTripleDES
				MACTripleDES objMacTripleDES = new MACTripleDES(bKey);
				txtMACTripleDES.Text = ASCIIEncoding.ASCII.GetString(objMacTripleDES.ComputeHash(bInputData));
			}
			catch (Exception ee)
			{
				MessageBox.Show(this, ee.ToString());
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
	}
}
