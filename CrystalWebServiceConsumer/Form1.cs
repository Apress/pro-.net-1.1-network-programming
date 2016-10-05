using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CrystalWebServiceConsumer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
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
			this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// crystalReportViewer1
			// 
			this.crystalReportViewer1.ActiveViewIndex = -1;
			this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
			this.crystalReportViewer1.Name = "crystalReportViewer1";
			this.crystalReportViewer1.ReportSource = null;
			this.crystalReportViewer1.Size = new System.Drawing.Size(640, 462);
			this.crystalReportViewer1.TabIndex = 0;
			this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 462);
			this.Controls.Add(this.crystalReportViewer1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
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

		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
		
		}

private void Form1_Load(object sender, System.EventArgs e)
{
	crystalReportViewer1.ReportSource = new localhost.Customer_ListService();
}
	}
}
