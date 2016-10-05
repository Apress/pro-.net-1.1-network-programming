using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
namespace CryptImplemntation
{
	public class frmCrypto : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnEncrypt;
		private System.Windows.Forms.Button btnDecrypt;
		private System.Windows.Forms.OpenFileDialog sourceFileDialog;
		private System.Windows.Forms.GroupBox grpFile;
		private System.Windows.Forms.Button btnBrowse1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox decFile;
		private System.Windows.Forms.TextBox encFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox strKey;
		private System.Windows.Forms.Label label3;

		private System.ComponentModel.Container components = null;
		public frmCrypto()
		{
			InitializeComponent();
		}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCrypto));
			this.btnEncrypt = new System.Windows.Forms.Button();
			this.btnDecrypt = new System.Windows.Forms.Button();
			this.sourceFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.grpFile = new System.Windows.Forms.GroupBox();
			this.strKey = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnBrowse1 = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.decFile = new System.Windows.Forms.TextBox();
			this.encFile = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpFile.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnEncrypt
			// 
			this.btnEncrypt.Enabled = false;
			this.btnEncrypt.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnEncrypt.Location = new System.Drawing.Point(152, 152);
			this.btnEncrypt.Name = "btnEncrypt";
			this.btnEncrypt.Size = new System.Drawing.Size(88, 32);
			this.btnEncrypt.TabIndex = 2;
			this.btnEncrypt.Text = "&Encrypt";
			this.btnEncrypt.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnDecrypt
			// 
			this.btnDecrypt.Enabled = false;
			this.btnDecrypt.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDecrypt.Location = new System.Drawing.Point(248, 152);
			this.btnDecrypt.Name = "btnDecrypt";
			this.btnDecrypt.Size = new System.Drawing.Size(88, 32);
			this.btnDecrypt.TabIndex = 2;
			this.btnDecrypt.Text = "&Decrypt";
			this.btnDecrypt.Click += new System.EventHandler(this.button2_Click);
			// 
			// grpFile
			// 
			this.grpFile.Controls.Add(this.strKey);
			this.grpFile.Controls.Add(this.label3);
			this.grpFile.Controls.Add(this.btnBrowse1);
			this.grpFile.Controls.Add(this.btnBrowse);
			this.grpFile.Controls.Add(this.decFile);
			this.grpFile.Controls.Add(this.encFile);
			this.grpFile.Controls.Add(this.label2);
			this.grpFile.Controls.Add(this.label1);
			this.grpFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpFile.Location = new System.Drawing.Point(8, 8);
			this.grpFile.Name = "grpFile";
			this.grpFile.Size = new System.Drawing.Size(424, 136);
			this.grpFile.TabIndex = 5;
			this.grpFile.TabStop = false;
			this.grpFile.Text = "File Information:";
			// 
			// strKey
			// 
			this.strKey.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.strKey.Location = new System.Drawing.Point(120, 101);
			this.strKey.MaxLength = 512;
			this.strKey.Name = "strKey";
			this.strKey.Size = new System.Drawing.Size(256, 23);
			this.strKey.TabIndex = 12;
			this.strKey.Text = "MySecret";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(79, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 18);
			this.label3.TabIndex = 11;
			this.label3.Text = "Key:";
			// 
			// btnBrowse1
			// 
			this.btnBrowse1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnBrowse1.Location = new System.Drawing.Point(384, 64);
			this.btnBrowse1.Name = "btnBrowse1";
			this.btnBrowse1.Size = new System.Drawing.Size(24, 24);
			this.btnBrowse1.TabIndex = 10;
			this.btnBrowse1.Text = "...";
			this.btnBrowse1.Click += new System.EventHandler(this.btnBrowse1_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnBrowse.Location = new System.Drawing.Point(384, 24);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(24, 24);
			this.btnBrowse.TabIndex = 9;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click_1);
			// 
			// decFile
			// 
			this.decFile.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.decFile.Location = new System.Drawing.Point(120, 64);
			this.decFile.Name = "decFile";
			this.decFile.Size = new System.Drawing.Size(256, 23);
			this.decFile.TabIndex = 8;
			this.decFile.Text = "";
			// 
			// encFile
			// 
			this.encFile.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.encFile.Location = new System.Drawing.Point(120, 24);
			this.encFile.Name = "encFile";
			this.encFile.Size = new System.Drawing.Size(256, 23);
			this.encFile.TabIndex = 7;
			this.encFile.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "File To Decrypt:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 18);
			this.label1.TabIndex = 6;
			this.label1.Text = "File to encrypt:";
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnClose.Location = new System.Drawing.Point(344, 152);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 32);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmCrypto
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 198);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.grpFile);
			this.Controls.Add(this.btnDecrypt);
			this.Controls.Add(this.btnEncrypt);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCrypto";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Crypter 1.0 - DES Algorithm Version";
			this.Load += new System.EventHandler(this.frmCrypto_Load);
			this.grpFile.ResumeLayout(false);
			this.ResumeLayout(false);

		}
            #endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new frmCrypto()); 
		}

		private byte[] iv;

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (EncryptData(encFile.Text, encFile.Text + ".enc" , strKey.Text) == true)
				MessageBox.Show(this,"Done!","Encryption Status",MessageBoxButtons.OK,MessageBoxIcon.Information);
			else
				MessageBox.Show(this,"The encryption process failed!","Fatal Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
		}
		private void button2_Click(object sender, System.EventArgs e)
		{
			string decFileName = decFile.Text.Replace(".enc","");
			if (DecryptData(decFile.Text, decFileName, strKey.Text) == true )
				MessageBox.Show(this,"Done!","Decryption Status",MessageBoxButtons.OK,MessageBoxIcon.Information);
			else
				MessageBox.Show(this,"The decryption process failed!","Fatal Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
		}

		// The EncryptData method will encrypt the given file using the DES algorithm
		public bool EncryptData(string sourceFile, string destinationFile, string cryptoKey)
		{
			try
			{
				//Create the DES Service Provider object and assign the key and vector to it
				DESCryptoServiceProvider  DESProvider = new DESCryptoServiceProvider();
				DESProvider.Key = ASCIIEncoding.ASCII.GetBytes(cryptoKey);
				//Initialize the initialization vector
				DESProvider.IV=this.iv;
				ICryptoTransform DESEncrypt = DESProvider.CreateEncryptor();

				//Open the source and destination file using the file stream object
				FileStream inFileStream = new FileStream(sourceFile, FileMode.Open,FileAccess.Read);
				FileStream outFileStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write);

				//Create a CrytoStream class and write the encrypted out
				CryptoStream cryptoStream = new CryptoStream(outFileStream, DESEncrypt, CryptoStreamMode.Write);

				//Declare the byte array of the length of the input file
				byte[] bytearrayinput = new byte[inFileStream.Length];

				//Read the input file stream in to the byte array and write 
				//in back in the CrytoStream
				inFileStream.Read(bytearrayinput, 0, bytearrayinput.Length);
				cryptoStream.Write(bytearrayinput, 0,bytearrayinput.Length);

				//Close the stream handlers
				cryptoStream.Close();
				inFileStream.Close();
				outFileStream.Close();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(this,e.ToString(),"Encryption Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return false;
			}
		}
		
		// The DecryptData method will decrypt the given file using the DES algorithm
		public bool DecryptData(string sourceFile, string destinationFile, string cryptoKey)
		{
			try
			{
				//Create the DES Service Provider object and assign the key and vector to it
				DESCryptoServiceProvider DESProvider = new DESCryptoServiceProvider();
				DESProvider.Key = ASCIIEncoding.ASCII.GetBytes(cryptoKey);
				//Initialize the initialization vector
				DESProvider.IV=this.iv;

				FileStream DecryptedFile= new FileStream(sourceFile, FileMode.Open,FileAccess.Read);
				ICryptoTransform desDecrypt= DESProvider.CreateDecryptor();
	      
				CryptoStream cryptostreamDecr = new CryptoStream(DecryptedFile, desDecrypt, CryptoStreamMode.Read);
				StreamWriter DecryptedOutput = new StreamWriter(destinationFile);
				DecryptedOutput.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
				DecryptedOutput.Flush();
				DecryptedOutput.Close();
				DecryptedFile.Close();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(this,e.ToString(),"Decryption Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return false;
			}
		}

		private void btnBrowse_Click_1(object sender, System.EventArgs e)
		{
			// Open file to Encrypt
			sourceFileDialog.Title="Select the source file to encrypt";
			sourceFileDialog.Filter="All files (*.*)|*.*";
			
			if(sourceFileDialog.ShowDialog() == DialogResult.OK)
			{
				encFile.Text=sourceFileDialog.FileName;
				btnEncrypt.Enabled=true;
			}
		}

		private void btnBrowse1_Click(object sender, System.EventArgs e)
		{
			// Open file to Encrypt
			sourceFileDialog.Title="Select the source file to decrypt";
			sourceFileDialog.Filter="Encrypted Files (*.enc)|*.enc";
			
			if(sourceFileDialog.ShowDialog() == DialogResult.OK)
			{
				decFile.Text=sourceFileDialog.FileName;
				btnDecrypt.Enabled=true;
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void frmCrypto_Load(object sender, System.EventArgs e)
		{
			//Create an initialization vector used for encryption and decryption.
			this.iv=new byte[]{1,2,3,4,5,6,7,8};
		}
	}
}
