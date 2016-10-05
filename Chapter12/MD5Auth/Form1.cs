using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
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
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.TextBox txtEmail;
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
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.grpLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpLogin
			// 
			this.grpLogin.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.txtPwd,
																				   this.txtEmail,
																				   this.label2,
																				   this.label1});
			this.grpLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpLogin.Location = new System.Drawing.Point(8, 8);
			this.grpLogin.Name = "grpLogin";
			this.grpLogin.Size = new System.Drawing.Size(360, 104);
			this.grpLogin.TabIndex = 10;
			this.grpLogin.TabStop = false;
			this.grpLogin.Text = "Login Information:";
			// 
			// txtPwd
			// 
			this.txtPwd.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPwd.Location = new System.Drawing.Point(88, 64);
			this.txtPwd.MaxLength = 20;
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PasswordChar = '*';
			this.txtPwd.Size = new System.Drawing.Size(256, 23);
			this.txtPwd.TabIndex = 8;
			this.txtPwd.Text = "";
			// 
			// txtEmail
			// 
			this.txtEmail.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtEmail.Location = new System.Drawing.Point(88, 24);
			this.txtEmail.MaxLength = 50;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(256, 23);
			this.txtEmail.TabIndex = 7;
			this.txtEmail.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Password:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(40, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Login:";
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnClose.Location = new System.Drawing.Point(280, 120);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 32);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnLogin.Location = new System.Drawing.Point(184, 120);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(88, 32);
			this.btnLogin.TabIndex = 8;
			this.btnLogin.Text = "&Login";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 158);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnClose,
																		  this.grpLogin,
																		  this.btnLogin});
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MD5 DB Auth";
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
			if (txtEmail.Text.Trim() != "" && txtPwd.Text.Trim() != "")
				AuthenticateUser();
		}

		private bool AuthenticateUser()
		{
			bool bRtnValue = false;
			string strConn = "PROVIDER=Microsoft.Jet.OLEDB.4.0;" + 
				"DATA SOURCE=DBAuth.mdb;";
			OleDbConnection Conn = new OleDbConnection(strConn) ;
			Conn.Open(); 

			String strSQL = "SELECT Pwd FROM Tbl_MA_Users WHERE Email = '" +
				txtEmail.Text + "'";
			OleDbCommand Cmd = new OleDbCommand(strSQL,Conn);

			//Create a datareader, connection object
			OleDbDataReader Dr = Cmd.ExecuteReader(
				System.Data.CommandBehavior.CloseConnection);

			//Get the first row and check the password.
			if (Dr.Read())
			{
				if (Dr["Pwd"].ToString() == GenerateMD5Hash(txtPwd.Text))
				{
					MessageBox.Show(this,"Password was successful!");
					bRtnValue = true;
				}
				else
				{
					MessageBox.Show(this,"Invalid password.");
				}
			}
			else
			{
				MessageBox.Show(this,"Login name not found.");
			}

			Dr.Close();
			return bRtnValue;
		}

		string GenerateMD5Hash(string sInput)
		{
			//Generate bytes our the input string
			byte[] bInputData = ASCIIEncoding.ASCII.GetBytes(sInput);

			//Compute the MD5 hash
			MD5 objMD5 = new MD5CryptoServiceProvider();
			byte[] bHashResult = objMD5.ComputeHash(bInputData);

			return ASCIIEncoding.ASCII.GetString(bHashResult);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
	}
}
