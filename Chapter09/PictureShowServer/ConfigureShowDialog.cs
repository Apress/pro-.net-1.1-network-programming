using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// Summary description for ConfigureShowDialog.
	/// </summary>
	public class ConfigureShowDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TrackBar trackBarTimeInterval;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigureShowDialog()
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public int TimeInterval
		{
			get
			{
				return trackBarTimeInterval.Value;
			}
			set
			{
				trackBarTimeInterval.Value = value;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.trackBarTimeInterval = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTimeInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBarTimeInterval
			// 
			this.trackBarTimeInterval.AccessibleName = "";
			this.trackBarTimeInterval.LargeChange = 10;
			this.trackBarTimeInterval.Location = new System.Drawing.Point(32, 64);
			this.trackBarTimeInterval.Maximum = 60;
			this.trackBarTimeInterval.Minimum = 1;
			this.trackBarTimeInterval.Name = "trackBarTimeInterval";
			this.trackBarTimeInterval.Size = new System.Drawing.Size(216, 45);
			this.trackBarTimeInterval.TabIndex = 0;
			this.trackBarTimeInterval.Tag = "";
			this.trackBarTimeInterval.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarTimeInterval.Value = 30;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Set the time interval for pictures to show:";
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOK.Location = new System.Drawing.Point(64, 128);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.Location = new System.Drawing.Point(160, 128);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			// 
			// ConfigureShowDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(292, 174);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonCancel,
																		  this.buttonOK,
																		  this.label1,
																		  this.trackBarTimeInterval});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigureShowDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "PictureShow Configuration";
			((System.ComponentModel.ISupportInitialize)(this.trackBarTimeInterval)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
