using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;

namespace Apress.ProfessionalNetworking
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOpenFile;
		private System.Windows.Forms.TextBox textData;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textFileOpen;
		private System.Windows.Forms.TextBox textFileSave;
		private System.Windows.Forms.Button buttonSaveFile;
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
			this.buttonOpenFile = new System.Windows.Forms.Button();
			this.textData = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textFileOpen = new System.Windows.Forms.TextBox();
			this.textFileSave = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSaveFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonOpenFile
			// 
			this.buttonOpenFile.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOpenFile.Location = new System.Drawing.Point(232, 16);
			this.buttonOpenFile.Name = "buttonOpenFile";
			this.buttonOpenFile.Size = new System.Drawing.Size(96, 24);
			this.buttonOpenFile.TabIndex = 2;
			this.buttonOpenFile.Text = "Open";
			this.buttonOpenFile.Click += new System.EventHandler(this.OnFileOpen);
			// 
			// textData
			// 
			this.textData.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textData.Location = new System.Drawing.Point(0, 96);
			this.textData.Multiline = true;
			this.textData.Name = "textData";
			this.textData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textData.Size = new System.Drawing.Size(344, 256);
			this.textData.TabIndex = 3;
			this.textData.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Open File:";
			// 
			// textFileOpen
			// 
			this.textFileOpen.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textFileOpen.Location = new System.Drawing.Point(96, 16);
			this.textFileOpen.Name = "textFileOpen";
			this.textFileOpen.Size = new System.Drawing.Size(120, 20);
			this.textFileOpen.TabIndex = 1;
			this.textFileOpen.Text = "";
			// 
			// textFileSave
			// 
			this.textFileSave.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textFileSave.Location = new System.Drawing.Point(96, 56);
			this.textFileSave.Name = "textFileSave";
			this.textFileSave.Size = new System.Drawing.Size(120, 20);
			this.textFileSave.TabIndex = 4;
			this.textFileSave.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Save as:";
			// 
			// buttonSaveFile
			// 
			this.buttonSaveFile.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSaveFile.Location = new System.Drawing.Point(232, 56);
			this.buttonSaveFile.Name = "buttonSaveFile";
			this.buttonSaveFile.Size = new System.Drawing.Size(96, 23);
			this.buttonSaveFile.TabIndex = 6;
			this.buttonSaveFile.Text = "Save";
			this.buttonSaveFile.Click += new System.EventHandler(this.OnFileSave);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 350);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSaveFile,
																		  this.label2,
																		  this.textFileSave,
																		  this.textFileOpen,
																		  this.label1,
																		  this.textData,
																		  this.buttonOpenFile});
			this.Name = "Form1";
			this.Text = "WebRequest, WebResponse Demo";
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

		private void OnFileOpen(object sender, System.EventArgs e)
		{
			string fileName = textFileOpen.Text;
			// FileWebRequest request = (FileWebRequest)WebRequest.Create(fileName);
			WebRequest request = WebRequest.Create(fileName);
			Stream stream = request.GetResponse().GetResponseStream();

			StreamReader reader = new StreamReader(stream);
			textData.Text = reader.ReadToEnd();
			reader.Close();
		}

		private void OnFileSave(object sender, System.EventArgs e)
		{
			string fileName = textFileSave.Text;
			FileWebRequest request = (FileWebRequest)WebRequest.Create("file://" + fileName);
			request.Method = "PUT";
			Stream stream = request.GetRequestStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(textData.Text);
			writer.Close();			
		}
	}
}
