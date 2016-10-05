using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Wrox_WindowsApp
{
	/// <summary>
	/// Summary description for ViewerControl.
	/// </summary>
	public class frmPreview : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPreview()
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
            this.crystalReportViewer1.Location = new System.Drawing.Point(8, 8);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = null;
            this.crystalReportViewer1.Size = new System.Drawing.Size(568, 392);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            this.crystalReportViewer1.HandleException += new CrystalDecisions.Windows.Forms.ExceptionEventHandler(this.crystalReportViewer1_HandleException);
            // 
            // frmPreview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(808, 542);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmPreview";
            this.Text = "Preview Report";
            this.Load += new System.EventHandler(this.frmPreview_Load);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmPreview());
		}

private void crystalReportViewer1_Load(object sender, System.EventArgs e)
{
//			customer_List1.PrintToPrinter(1,false,1,2);

}

private void frmPreview_Load(object sender, System.EventArgs e)
{
	//Untyped report
	CrystalDecisions.CrystalReports.Engine.ReportDocument myReport;
	myReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
	try
	{
		myReport.Load(@"..\..\Customer List.rpt");
		crystalReportViewer1.ReportSource = myReport;			
	}
	catch(CrystalDecisions.CrystalReports.Engine.EngineException ex)
	{
		MessageBox.Show("Can't create report due to following error: " + ex.ToString());
	}
}

private void button1_Click(object sender, System.EventArgs e)
{
	Customer_List myReport = new Customer_List();
	myReport.PrintToPrinter(1,false,0,0);
}

private void crystalReportViewer1_HandleException(object source, CrystalDecisions.Windows.Forms.ExceptionEventArgs e)
{
	MessageBox.Show("While generating your next report page, the following error occurred: " + e.ToString());
}


	}
}
