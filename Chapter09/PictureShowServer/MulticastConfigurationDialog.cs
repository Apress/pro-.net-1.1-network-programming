using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// Summary description for MulticastConfiguration.
	/// </summary>
	public class MulticastConfigurationDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxIPAddress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxLocalInterface;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MulticastConfigurationDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			string hostname = Dns.GetHostName();
			IPHostEntry entry = Dns.GetHostByName(hostname);
			IPAddress[] addresses = entry.AddressList;

			foreach (IPAddress address in addresses)
			{
				comboBoxLocalInterface.Items.Add(address.ToString());
			}
			comboBoxLocalInterface.SelectedIndex = 0;

			if (addresses.Length > 1)
			{
				comboBoxLocalInterface.Items.Add("Any");
			}
			else
			{
				comboBoxLocalInterface.Enabled = false;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxIPAddress = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxLocalInterface = new System.Windows.Forms.ComboBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter a Multicast IP Address:";
			// 
			// textBoxIPAddress
			// 
			this.textBoxIPAddress.Location = new System.Drawing.Point(208, 32);
			this.textBoxIPAddress.MaxLength = 15;
			this.textBoxIPAddress.Name = "textBoxIPAddress";
			this.textBoxIPAddress.Size = new System.Drawing.Size(144, 20);
			this.textBoxIPAddress.TabIndex = 1;
			this.textBoxIPAddress.Text = "225.1.1.1";
			this.textBoxIPAddress.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidateMulticastAddress);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Port Number:";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(208, 72);
			this.textBoxPort.MaxLength = 4;
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(144, 20);
			this.textBoxPort.TabIndex = 3;
			this.textBoxPort.Text = "8888";
			this.textBoxPort.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidatePort);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 120);
			this.label4.Name = "label4";
			this.label4.TabIndex = 4;
			this.label4.Text = "Local Interface:";
			// 
			// comboBoxLocalInterface
			// 
			this.comboBoxLocalInterface.Location = new System.Drawing.Point(208, 120);
			this.comboBoxLocalInterface.Name = "comboBoxLocalInterface";
			this.comboBoxLocalInterface.Size = new System.Drawing.Size(144, 21);
			this.comboBoxLocalInterface.TabIndex = 5;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOK.Location = new System.Drawing.Point(80, 168);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = "&OK";
			this.buttonOK.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// buttonCancel
			// 
			this.buttonCancel.CausesValidation = false;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.Location = new System.Drawing.Point(200, 168);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "&Cancel";
			// 
			// MulticastConfigurationDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(376, 214);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonCancel,
																		  this.buttonOK,
																		  this.comboBoxLocalInterface,
																		  this.label4,
																		  this.textBoxPort,
																		  this.label2,
																		  this.textBoxIPAddress,
																		  this.label1});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MulticastConfigurationDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "PictureShow - Multicast Configuration";
			this.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			this.ResumeLayout(false);

		}
		#endregion


		private void OnValidating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				IPAddress address = IPAddress.Parse(textBoxIPAddress.Text);
			}
			catch (FormatException ex)
			{
				MessageBox.Show(ex.Message);
				e.Cancel = true;
			}
		}

		private void OnValidateMulticastAddress(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				IPAddress address = IPAddress.Parse(textBoxIPAddress.Text);

				string[] segments = textBoxIPAddress.Text.Split('.');
				int network = int.Parse(segments[0]);
				if ((network < 224) || (network > 239))
					throw new FormatException("Multicast addresses have the range 224.x.x.x to 239.x.x.x");

				if ((network == 224) && (int.Parse(segments[1]) == 0) && (int.Parse(segments[2]) == 0))
					throw new FormatException("The Local Network Control Block cannot be used for multicasting groups");
		}
		catch (FormatException ex)
			{
				MessageBox.Show(ex.Message);
				e.Cancel = true;
			}
		}

		private void OnValidatePort(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				int port = int.Parse(textBoxPort.Text);
				if ((port < 1) || (port > 65535))
					throw new FormatException("The port must be in a range from xx to 65535");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				e.Cancel = true;
			}
		}

		public IPAddress MulticastAddress
		{
			get
			{
				return IPAddress.Parse(textBoxIPAddress.Text);
			}
			set
			{
				textBoxIPAddress.Text = value.ToString();
			}
		}

		public int PortNumber
		{
			get
			{
				return int.Parse(textBoxPort.Text);
			}
			set
			{
				textBoxPort.Text = value.ToString();
			}
		}

		public string LocalInterface
		{
			get
			{
				return comboBoxLocalInterface.Text;
			}
		}
	}
}
