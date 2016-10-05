using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Security.Cryptography;

namespace RSEnc
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tab1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnSavePri;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnDecrypt;
		private System.Windows.Forms.Button btnEncrypt;
		private System.Windows.Forms.TextBox txtCipherText;
		private System.Windows.Forms.TextBox txtClearText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button btnLoadPub;
		private System.Windows.Forms.Button btnLoadPri;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnDecrypt1;
		private System.Windows.Forms.Button btnEncrypt1;
		private System.Windows.Forms.TextBox txtCipherText1;
		private System.Windows.Forms.TextBox txtClearText1;

		
		//Class level object
		static RSACryptoServiceProvider rsaProvider;
		private System.Windows.Forms.TextBox txtPrvParams;
		private System.Windows.Forms.TextBox txtPubParams;
		private System.Windows.Forms.TextBox txtPriParams1;
		private System.Windows.Forms.TextBox txtPubParams1;
		//static RSACryptoServiceProvider objRSACryptoProvider=new RSACryptoServiceProvider(); 
		
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.tab1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnSavePri = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnDecrypt = new System.Windows.Forms.Button();
			this.btnEncrypt = new System.Windows.Forms.Button();
			this.txtCipherText = new System.Windows.Forms.TextBox();
			this.txtClearText = new System.Windows.Forms.TextBox();
			this.txtPrvParams = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPubParams = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.btnLoadPri = new System.Windows.Forms.Button();
			this.btnLoadPub = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnDecrypt1 = new System.Windows.Forms.Button();
			this.btnEncrypt1 = new System.Windows.Forms.Button();
			this.txtCipherText1 = new System.Windows.Forms.TextBox();
			this.txtClearText1 = new System.Windows.Forms.TextBox();
			this.txtPriParams1 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPubParams1 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.tab1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tab1
			// 
			this.tab1.Controls.Add(this.tabPage1);
			this.tab1.Controls.Add(this.tabPage2);
			this.tab1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab1.Location = new System.Drawing.Point(0, 0);
			this.tab1.Name = "tab1";
			this.tab1.SelectedIndex = 0;
			this.tab1.Size = new System.Drawing.Size(736, 486);
			this.tab1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnSavePri);
			this.tabPage1.Controls.Add(this.btnSave);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.btnDecrypt);
			this.tabPage1.Controls.Add(this.btnEncrypt);
			this.tabPage1.Controls.Add(this.txtCipherText);
			this.tabPage1.Controls.Add(this.txtClearText);
			this.tabPage1.Controls.Add(this.txtPrvParams);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.txtPubParams);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(728, 460);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Auto Keys";
			// 
			// btnSavePri
			// 
			this.btnSavePri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSavePri.Location = new System.Drawing.Point(632, 424);
			this.btnSavePri.Name = "btnSavePri";
			this.btnSavePri.Size = new System.Drawing.Size(88, 23);
			this.btnSavePri.TabIndex = 30;
			this.btnSavePri.Text = "Save As...";
			this.btnSavePri.Click += new System.EventHandler(this.btnSavePri_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(632, 240);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 23);
			this.btnSave.TabIndex = 29;
			this.btnSave.Text = "Save As...";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(4, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(89, 18);
			this.label6.TabIndex = 28;
			this.label6.Text = "Cipher Text:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 18);
			this.label5.TabIndex = 27;
			this.label5.Text = "Clear Text:";
			// 
			// btnDecrypt
			// 
			this.btnDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDecrypt.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDecrypt.Location = new System.Drawing.Point(632, 72);
			this.btnDecrypt.Name = "btnDecrypt";
			this.btnDecrypt.Size = new System.Drawing.Size(88, 32);
			this.btnDecrypt.TabIndex = 26;
			this.btnDecrypt.Text = "&Decrypt";
			this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
			// 
			// btnEncrypt
			// 
			this.btnEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEncrypt.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnEncrypt.Location = new System.Drawing.Point(632, 16);
			this.btnEncrypt.Name = "btnEncrypt";
			this.btnEncrypt.Size = new System.Drawing.Size(88, 32);
			this.btnEncrypt.TabIndex = 25;
			this.btnEncrypt.Text = "&Encrypt";
			this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
			// 
			// txtCipherText
			// 
			this.txtCipherText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCipherText.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCipherText.Location = new System.Drawing.Point(96, 48);
			this.txtCipherText.Multiline = true;
			this.txtCipherText.Name = "txtCipherText";
			this.txtCipherText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCipherText.Size = new System.Drawing.Size(520, 80);
			this.txtCipherText.TabIndex = 24;
			this.txtCipherText.Text = "";
			// 
			// txtClearText
			// 
			this.txtClearText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtClearText.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClearText.Location = new System.Drawing.Point(96, 16);
			this.txtClearText.Name = "txtClearText";
			this.txtClearText.Size = new System.Drawing.Size(520, 23);
			this.txtClearText.TabIndex = 23;
			this.txtClearText.Text = "";
			// 
			// txtPrvParams
			// 
			this.txtPrvParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPrvParams.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPrvParams.Location = new System.Drawing.Point(8, 280);
			this.txtPrvParams.MaxLength = 37000;
			this.txtPrvParams.Multiline = true;
			this.txtPrvParams.Name = "txtPrvParams";
			this.txtPrvParams.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPrvParams.Size = new System.Drawing.Size(712, 144);
			this.txtPrvParams.TabIndex = 22;
			this.txtPrvParams.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 264);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(220, 18);
			this.label4.TabIndex = 21;
			this.label4.Text = "Public and Private Parameters:";
			// 
			// txtPubParams
			// 
			this.txtPubParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPubParams.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPubParams.Location = new System.Drawing.Point(8, 152);
			this.txtPubParams.MaxLength = 37000;
			this.txtPubParams.Multiline = true;
			this.txtPubParams.Name = "txtPubParams";
			this.txtPubParams.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPubParams.Size = new System.Drawing.Size(712, 88);
			this.txtPubParams.TabIndex = 20;
			this.txtPubParams.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 18);
			this.label3.TabIndex = 19;
			this.label3.Text = "Public Parameters:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnLoadPri);
			this.tabPage2.Controls.Add(this.btnLoadPub);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.btnDecrypt1);
			this.tabPage2.Controls.Add(this.btnEncrypt1);
			this.tabPage2.Controls.Add(this.txtCipherText1);
			this.tabPage2.Controls.Add(this.txtClearText1);
			this.tabPage2.Controls.Add(this.txtPriParams1);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.txtPubParams1);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(728, 460);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Keys From XML File";
			this.tabPage2.Visible = false;
			// 
			// btnLoadPri
			// 
			this.btnLoadPri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadPri.Location = new System.Drawing.Point(616, 272);
			this.btnLoadPri.Name = "btnLoadPri";
			this.btnLoadPri.Size = new System.Drawing.Size(104, 23);
			this.btnLoadPri.TabIndex = 40;
			this.btnLoadPri.Text = "Load from XML...";
			this.btnLoadPri.Click += new System.EventHandler(this.btnLoadPri_Click);
			// 
			// btnLoadPub
			// 
			this.btnLoadPub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadPub.Location = new System.Drawing.Point(616, 144);
			this.btnLoadPub.Name = "btnLoadPub";
			this.btnLoadPub.Size = new System.Drawing.Size(104, 23);
			this.btnLoadPub.TabIndex = 39;
			this.btnLoadPub.Text = "Load from XML...";
			this.btnLoadPub.Click += new System.EventHandler(this.btnLoadPub_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 18);
			this.label1.TabIndex = 38;
			this.label1.Text = "Cipher Text:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 18);
			this.label2.TabIndex = 37;
			this.label2.Text = "Clear Text:";
			// 
			// btnDecrypt1
			// 
			this.btnDecrypt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDecrypt1.Enabled = false;
			this.btnDecrypt1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDecrypt1.Location = new System.Drawing.Point(632, 80);
			this.btnDecrypt1.Name = "btnDecrypt1";
			this.btnDecrypt1.Size = new System.Drawing.Size(88, 32);
			this.btnDecrypt1.TabIndex = 36;
			this.btnDecrypt1.Text = "&Decrypt";
			this.btnDecrypt1.Click += new System.EventHandler(this.btnDecrypt1_Click);
			// 
			// btnEncrypt1
			// 
			this.btnEncrypt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEncrypt1.Enabled = false;
			this.btnEncrypt1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnEncrypt1.Location = new System.Drawing.Point(632, 8);
			this.btnEncrypt1.Name = "btnEncrypt1";
			this.btnEncrypt1.Size = new System.Drawing.Size(88, 32);
			this.btnEncrypt1.TabIndex = 35;
			this.btnEncrypt1.Text = "&Encrypt";
			this.btnEncrypt1.Click += new System.EventHandler(this.btnEncrypt1_Click);
			// 
			// txtCipherText1
			// 
			this.txtCipherText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCipherText1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCipherText1.Location = new System.Drawing.Point(104, 40);
			this.txtCipherText1.Multiline = true;
			this.txtCipherText1.Name = "txtCipherText1";
			this.txtCipherText1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCipherText1.Size = new System.Drawing.Size(496, 88);
			this.txtCipherText1.TabIndex = 34;
			this.txtCipherText1.Text = "";
			// 
			// txtClearText1
			// 
			this.txtClearText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtClearText1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClearText1.Location = new System.Drawing.Point(104, 8);
			this.txtClearText1.Name = "txtClearText1";
			this.txtClearText1.Size = new System.Drawing.Size(496, 23);
			this.txtClearText1.TabIndex = 33;
			this.txtClearText1.Text = "";
			// 
			// txtPriParams1
			// 
			this.txtPriParams1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPriParams1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPriParams1.Location = new System.Drawing.Point(8, 296);
			this.txtPriParams1.MaxLength = 37000;
			this.txtPriParams1.Multiline = true;
			this.txtPriParams1.Name = "txtPriParams1";
			this.txtPriParams1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPriParams1.Size = new System.Drawing.Size(712, 152);
			this.txtPriParams1.TabIndex = 32;
			this.txtPriParams1.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 280);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(220, 18);
			this.label7.TabIndex = 31;
			this.label7.Text = "Public and Private Parameters:";
			// 
			// txtPubParams1
			// 
			this.txtPubParams1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPubParams1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPubParams1.Location = new System.Drawing.Point(8, 168);
			this.txtPubParams1.MaxLength = 37000;
			this.txtPubParams1.Multiline = true;
			this.txtPubParams1.Name = "txtPubParams1";
			this.txtPubParams1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPubParams1.Size = new System.Drawing.Size(712, 88);
			this.txtPubParams1.TabIndex = 30;
			this.txtPubParams1.Text = "";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(8, 152);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(136, 18);
			this.label8.TabIndex = 29;
			this.label8.Text = "Public Parameters:";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileName = "doc1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 486);
			this.Controls.Add(this.tab1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Message Crypter 1.0 - RSA Algorithm Version";
			this.tab1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
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

		private void btnEncrypt_Click(object sender, System.EventArgs e)
		{
			if (txtClearText.Text.Trim() != "")
			{
				// Initialize the RSA Cryptography Service Provider (CSP)
				rsaProvider = new RSACryptoServiceProvider();

				UTF8Encoding utf8 = new UTF8Encoding();
				byte[] clearText = utf8.GetBytes(txtClearText.Text.Trim());

				// Encrypting the data recieved
				txtCipherText.Text = Convert.ToBase64String(rsaProvider.Encrypt(clearText,
					false));

				// Show the public and private parameters
				txtPrvParams.Text = rsaProvider.ToXmlString(true);

				// Show the public parameters
				txtPubParams.Text = rsaProvider.ToXmlString(false);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
 
			saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			saveFileDialog1.FilterIndex = 2;
			saveFileDialog1.RestoreDirectory = true;
 
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//Write the content to an XML file
				XmlDocument objXDoc = new XmlDocument();
				objXDoc.LoadXml(this.txtPubParams.Text);

				//Save the document to a file.
				objXDoc.Save(saveFileDialog1.OpenFile());
			}
		}

		private void btnSavePri_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
 
			saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"  ;
			saveFileDialog1.FilterIndex = 2 ;
			saveFileDialog1.RestoreDirectory = true ;
 
			if(saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//Write the content to an XML file
				XmlDocument objXDoc = new XmlDocument();
				objXDoc.LoadXml(this.txtPrvParams.Text);

				//Save the document to a file.
				objXDoc.Save(saveFileDialog1.OpenFile());
			}		
		}

		private void btnDecrypt_Click(object sender, System.EventArgs e)
		{
			if (txtCipherText.Text.Trim() != "")
			{
				//Convert the input string into a byte array
				byte[] bCipherText = Convert.FromBase64String(txtCipherText.Text.Trim());

				//Decrypt the data and convert it back to a string
				string strValue = ASCIIEncoding.ASCII.GetString(rsaProvider.Decrypt(bCipherText, false));
		
				//Display the decrypted string in a MessageBox
				MessageBox.Show(this, strValue , "Decrypted value", MessageBoxButtons.OK,MessageBoxIcon.Information);
			}		
		
		}

		private void btnLoadPub_Click(object sender, System.EventArgs e)
		{
			//Show the open file dialog
			openFileDialog1.Title="Select the Public Parameters file";
			openFileDialog1.Filter="XML Files (*.xml)|*.xml";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string sFileName = openFileDialog1.FileName;
				btnEncrypt1.Enabled=true;

				//Load the the document
				XmlTextReader objXReader = new XmlTextReader(sFileName);
				objXReader.WhitespaceHandling = WhitespaceHandling.None;
				objXReader.Read(); 
				//Assign the public parameters to the textbox
				txtPubParams1.Text = objXReader.ReadOuterXml();
			}
		}

		private void btnEncrypt1_Click(object sender, System.EventArgs e)
		{
			if (txtClearText1.Text.Trim() != "")
			{
				// Initializing the RSA Cryptography Service Provider (CSP)
				RSACryptoServiceProvider rsaProvider1 = new RSACryptoServiceProvider();

				try
				{
					CspParameters cspParam = new CspParameters();
					cspParam.Flags = CspProviderFlags.UseMachineKeyStore;
					cspParam.KeyContainerName = "ApressRSAStore";
					cspParam.ProviderName = "MS Strong Cryptographic Provider";

					// CryptoAPI constant -> PROV_RSA_FULL = 1
					// This provider type supports both digital 
					// signatures and data encryption, and is considered 
					// general purpose. The RSA public-key algorithm 
					// is used for all public-key operations.
					cspParam.ProviderType = 1;

					//Load the public parameters
					rsaProvider1.FromXmlString(txtPubParams1.Text);

					UTF8Encoding utf8 = new UTF8Encoding();
					byte[] bClearText = utf8.GetBytes(txtClearText1.Text);

					//Convert encrypted text to base64 for transportation in SOAP message
					txtCipherText1.Text = Convert.ToBase64String(rsaProvider1.Encrypt(bClearText,false));
				}
				catch (Exception exp) 
				{
					MessageBox.Show(this, exp.ToString());
				}
			}
		}

		private void btnLoadPri_Click(object sender, System.EventArgs e)
		{
			//Show the open file dialog
			openFileDialog1.Title="Select the Private Key file";
			openFileDialog1.Filter="XML Files (*.xml)|*.xml";
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string sFileName = openFileDialog1.FileName;
				this.btnDecrypt1.Enabled=true;

				//Load the the document
				XmlTextReader objXReader = new XmlTextReader(sFileName);
				objXReader.WhitespaceHandling = WhitespaceHandling.None;
				objXReader.Read(); 

				//Assign the public parameters to the textbox
				txtPriParams1.Text = objXReader.ReadOuterXml();
			}
		}

		private void btnDecrypt1_Click(object sender, System.EventArgs e)
		{
			if (txtClearText1.Text.Trim() != "")
			{
				// Initializing the RSA Cryptography Service Provider (CSP)
				RSACryptoServiceProvider rsaProvider1 = new RSACryptoServiceProvider();

				//Set the loaded private parameters
				rsaProvider1.FromXmlString(this.txtPriParams1.Text);

				// Decrypting the data recived
				MessageBox.Show(this,ASCIIEncoding.ASCII.GetString(rsaProvider1.Decrypt(Convert.FromBase64String(txtCipherText1.Text.Trim()), false)), "Decrypted value", MessageBoxButtons.OK,MessageBoxIcon.Information);
			}		
		}

	}
}
