using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace X509
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmX509 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnView;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmX509()
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
			this.btnView = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnView
			// 
			this.btnView.Location = new System.Drawing.Point(8, 8);
			this.btnView.Name = "btnView";
			this.btnView.Size = new System.Drawing.Size(272, 88);
			this.btnView.TabIndex = 0;
			this.btnView.Text = "&View Certificate";
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			// 
			// frmX509
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(288, 101);
			this.Controls.Add(this.btnView);
			this.Name = "frmX509";
			this.Text = "Reading X509 Certificate";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmX509());
		}

		private void btnView_Click(object sender, System.EventArgs e)
		{
			//Read the client certificate from the file 
			//into the object variable of the type X509Certificate
			X509Certificate objClientCerti = X509Certificate.CreateFromCertFile("test.cer");
			
			StringBuilder sb = new StringBuilder();

			sb.Append("Issuer Name: " + objClientCerti.GetIssuerName() + "\n");
			sb.Append("Public Key String: " + objClientCerti.GetPublicKeyString() + "\n");
			sb.Append("Key Algorithm: " + objClientCerti.GetKeyAlgorithm().ToString() + "\n");
			sb.Append("Serial Number: " + objClientCerti.GetSerialNumberString() + "\n");
			sb.Append("Effective Date: " + objClientCerti.GetEffectiveDateString().ToString() + "\n");
			sb.Append("Expiration Date: " + objClientCerti.GetExpirationDateString().ToString() + "\n");
			MessageBox.Show(this, sb.ToString());
		}
	}
}
