using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// Summary description for ConfigurePicturesDialog.
	/// </summary>
	public class ConfigurePicturesDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ListView listViewPictures;
		private System.Windows.Forms.Button buttonClear;
		private System.ComponentModel.IContainer components;

		private string[] fileNames;

		public ConfigurePicturesDialog()
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
			this.components = new System.ComponentModel.Container();
			this.listViewPictures = new System.Windows.Forms.ListView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.buttonSelect = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.buttonClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listViewPictures
			// 
			this.listViewPictures.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listViewPictures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewPictures.LargeImageList = this.imageList;
			this.listViewPictures.Location = new System.Drawing.Point(0, 56);
			this.listViewPictures.Name = "listViewPictures";
			this.listViewPictures.Size = new System.Drawing.Size(312, 168);
			this.listViewPictures.TabIndex = 0;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(96, 96);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// buttonSelect
			// 
			this.buttonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSelect.Location = new System.Drawing.Point(40, 17);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(104, 23);
			this.buttonSelect.TabIndex = 1;
			this.buttonSelect.Text = "Select Pictures...";
			this.buttonSelect.Click += new System.EventHandler(this.OnFileOpen);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOK.Location = new System.Drawing.Point(57, 236);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(87, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.Location = new System.Drawing.Point(169, 236);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(87, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Bitmap Files|*.bmp|JPEG|*.jpg;*.jpeg|GIF|*.gif|All Picture Files|*.gif;*.bmp;*.jp" +
				"g;*.jpeg";
			this.openFileDialog.Multiselect = true;
			// 
			// buttonClear
			// 
			this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonClear.Location = new System.Drawing.Point(168, 16);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(104, 23);
			this.buttonClear.TabIndex = 4;
			this.buttonClear.Text = "Clear";
			this.buttonClear.Click += new System.EventHandler(this.OnClearPictures);
			// 
			// ConfigurePicturesDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(312, 270);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonClear,
																		  this.buttonCancel,
																		  this.buttonOK,
																		  this.buttonSelect,
																		  this.listViewPictures});
			this.MinimizeBox = false;
			this.Name = "ConfigurePicturesDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Configure Pictures";
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFileOpen(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileNames = openFileDialog.FileNames;

				int imageIndex = 0;
				foreach (string fileName in fileNames)
				{
					using (Image image = Image.FromFile(fileName))
					{
						imageList.Images.Add(image);

						listViewPictures.Items.Add(fileName, imageIndex++);
					}
				}
			}
		}

		private void OnClearPictures(object sender, System.EventArgs e)
		{
			listViewPictures.Items.Clear();
		}

		public string[] FileNames
		{
			get
			{
				return fileNames;
			}
		}
	}
}
