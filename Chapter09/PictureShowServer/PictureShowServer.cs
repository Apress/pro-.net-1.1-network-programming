using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading;

namespace Apress.Networking.Multicast
{
	public delegate void MethodInvokerInt(int x);
	public delegate void MethodInvokerString(string s);
	public delegate void MethodInvokerBoolean(bool flag);


	/// <summary>
	/// Main form of the multicast Picture Show server
	/// </summary>
	public class PictureServerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.MenuItem miHelp;
		private System.Windows.Forms.MenuItem miHelpAbout;
		private System.Windows.Forms.MenuItem miFileExit;
		private System.Windows.Forms.MenuItem miFileStart;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.MenuItem miConfigure;
		private System.Windows.Forms.MenuItem miConfigureMulticast;
		private System.Windows.Forms.MenuItem miConfigurePictures;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.MenuItem miConfigureShow;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonInit;
		private System.Windows.Forms.MenuItem miFileStop;
		private System.Windows.Forms.MenuItem miFileInit;
		private System.Windows.Forms.Button buttonPictures;
		private System.ComponentModel.IContainer components;

		private string[] fileNames;		// Array of picture filenames
		private object filesLock = new object();	// Lock to synchronize access to filenames
		private UnicodeEncoding encoding = new UnicodeEncoding();

      // Multicast group address, port, and endpoint
		private IPAddress groupAddress = IPAddress.Parse("231.4.5.11");	
		private int groupPort = 8765;	
		private IPEndPoint groupEP;		
		private UdpClient udpClient;	

		private Thread senderThread;	// Thread to send pictures
		private Image currentImage;	// Current image used by sender thread

		private int pictureIntervalSeconds = 3;	// Time between sending pictures

		public PictureServerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			this.components = new System.ComponentModel.Container();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.miFile = new System.Windows.Forms.MenuItem();
			this.miFileInit = new System.Windows.Forms.MenuItem();
			this.miFileStart = new System.Windows.Forms.MenuItem();
			this.miFileStop = new System.Windows.Forms.MenuItem();
			this.miFileExit = new System.Windows.Forms.MenuItem();
			this.miConfigure = new System.Windows.Forms.MenuItem();
			this.miConfigureMulticast = new System.Windows.Forms.MenuItem();
			this.miConfigureShow = new System.Windows.Forms.MenuItem();
			this.miConfigurePictures = new System.Windows.Forms.MenuItem();
			this.miHelp = new System.Windows.Forms.MenuItem();
			this.miHelpAbout = new System.Windows.Forms.MenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonPictures = new System.Windows.Forms.Button();
			this.buttonInit = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonStart = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 283);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(384, 22);
			this.statusBar.TabIndex = 2;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.miFile,
																					 this.miConfigure,
																					 this.miHelp});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miFileInit,
																				   this.miFileStart,
																				   this.miFileStop,
																				   this.miFileExit});
			this.miFile.Text = "&File";
			// 
			// miFileInit
			// 
			this.miFileInit.Index = 0;
			this.miFileInit.Text = "&Init";
			this.miFileInit.Click += new System.EventHandler(this.OnInit);
			// 
			// miFileStart
			// 
			this.miFileStart.Enabled = false;
			this.miFileStart.Index = 1;
			this.miFileStart.Text = "&Start";
			this.miFileStart.Click += new System.EventHandler(this.OnStart);
			// 
			// miFileStop
			// 
			this.miFileStop.Enabled = false;
			this.miFileStop.Index = 2;
			this.miFileStop.Text = "Sto&p";
			this.miFileStop.Click += new System.EventHandler(this.OnStop);
			// 
			// miFileExit
			// 
			this.miFileExit.Index = 3;
			this.miFileExit.Text = "E&xit";
			this.miFileExit.Click += new System.EventHandler(this.OnExit);
			// 
			// miConfigure
			// 
			this.miConfigure.Index = 1;
			this.miConfigure.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.miConfigureMulticast,
																						this.miConfigureShow,
																						this.miConfigurePictures});
			this.miConfigure.Text = "&Configure";
			// 
			// miConfigureMulticast
			// 
			this.miConfigureMulticast.Index = 0;
			this.miConfigureMulticast.Text = "&Multicast Session";
			this.miConfigureMulticast.Click += new System.EventHandler(this.OnConfigureMulticast);
			// 
			// miConfigureShow
			// 
			this.miConfigureShow.Index = 1;
			this.miConfigureShow.Text = "&Show Timings";
			this.miConfigureShow.Click += new System.EventHandler(this.OnConfigureShow);
			// 
			// miConfigurePictures
			// 
			this.miConfigurePictures.Index = 2;
			this.miConfigurePictures.Text = "&Pictures";
			this.miConfigurePictures.Click += new System.EventHandler(this.OnConfigurePictures);
			// 
			// miHelp
			// 
			this.miHelp.Index = 2;
			this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miHelpAbout});
			this.miHelp.Text = "&Help";
			// 
			// miHelpAbout
			// 
			this.miHelpAbout.Index = 0;
			this.miHelpAbout.Text = "&About";
			this.miHelpAbout.Click += new System.EventHandler(this.OnHelpAbout);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// progressBar
			// 
			this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar.Location = new System.Drawing.Point(0, 260);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(384, 23);
			this.progressBar.TabIndex = 1;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.pictureBox.Location = new System.Drawing.Point(0, 48);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(384, 207);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.buttonPictures,
																				 this.buttonInit,
																				 this.buttonStop,
																				 this.buttonStart});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 40);
			this.panel1.TabIndex = 0;
			// 
			// buttonPictures
			// 
			this.buttonPictures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPictures.Location = new System.Drawing.Point(24, 8);
			this.buttonPictures.Name = "buttonPictures";
			this.buttonPictures.TabIndex = 0;
			this.buttonPictures.Text = "Pictures...";
			this.buttonPictures.Click += new System.EventHandler(this.OnConfigurePictures);
			// 
			// buttonInit
			// 
			this.buttonInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonInit.Location = new System.Drawing.Point(112, 8);
			this.buttonInit.Name = "buttonInit";
			this.buttonInit.Size = new System.Drawing.Size(64, 23);
			this.buttonInit.TabIndex = 1;
			this.buttonInit.Text = "Init";
			this.buttonInit.Click += new System.EventHandler(this.OnInit);
			// 
			// buttonStop
			// 
			this.buttonStop.BackColor = System.Drawing.SystemColors.Control;
			this.buttonStop.Enabled = false;
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.Location = new System.Drawing.Point(288, 8);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(72, 23);
			this.buttonStop.TabIndex = 3;
			this.buttonStop.Text = "Stop Show";
			this.buttonStop.Click += new System.EventHandler(this.OnStop);
			// 
			// buttonStart
			// 
			this.buttonStart.BackColor = System.Drawing.SystemColors.Control;
			this.buttonStart.Enabled = false;
			this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStart.Location = new System.Drawing.Point(192, 8);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(80, 24);
			this.buttonStart.TabIndex = 2;
			this.buttonStart.Text = "Start Show";
			this.buttonStart.Click += new System.EventHandler(this.OnStart);
			// 
			// PictureServerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 305);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.pictureBox,
																		  this.progressBar,
																		  this.statusBar});
			this.Menu = this.mainMenu;
			this.Name = "PictureServerForm";
			this.Text = "Picture Show Server";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new PictureServerForm());
		}

		private void OnConfigureMulticast(object sender, System.EventArgs e)
		{
			MulticastConfigurationDialog dialog = new MulticastConfigurationDialog();
			dialog.PortNumber = groupPort;
			dialog.MulticastAddress = groupAddress;
			
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				groupPort = dialog.PortNumber;
				groupAddress = dialog.MulticastAddress;
			}		
		}

		private void OnConfigurePictures(object sender, System.EventArgs e)
		{
			ConfigurePicturesDialog dialog = new ConfigurePicturesDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				lock (filesLock)
				{
					fileNames = dialog.FileNames;
					progressBar.Maximum = fileNames.Length;
				}
			}
		}

		private void OnConfigureShow(object sender, System.EventArgs e)
		{
			ConfigureShowDialog dialog = new ConfigureShowDialog();
			dialog.TimeInterval = pictureIntervalSeconds;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				pictureIntervalSeconds = dialog.TimeInterval;
			}
		}

		private void UIEnableInit(bool flag)
		{
			if (flag)
			{
				buttonInit.Enabled = true;
				miFileInit.Enabled = true;
				miConfigureMulticast.Enabled = true;
			}
			else
			{
				buttonInit.Enabled = false;
				miFileInit.Enabled = false;
				miConfigureMulticast.Enabled = false;
			}
		}

		private void UIEnableStart(bool flag)
		{
			if (flag)
			{
				buttonStart.Enabled = true;
				buttonStart.BackColor = Color.SpringGreen;
				miFileStart.Enabled = true;
			}
			else
			{
				buttonStart.Enabled = false;
				buttonStart.BackColor = Color.LightGray;
				miFileStart.Enabled = false;
			}
		}

		private void UIEnableStop(bool flag)
		{
			if (flag)
			{
				buttonStop.Enabled = true;
				buttonStop.BackColor = Color.Red;
				miFileStop.Enabled = true;
				buttonPictures.Enabled = false;
				miConfigurePictures.Enabled = false;
			}
			else
			{
				buttonStop.Enabled = false;
				buttonStop.BackColor = Color.LightGray;
				miFileStop.Enabled = false;
				buttonPictures.Enabled = true;
				miConfigurePictures.Enabled = true;
			}
		}

		private void OnStart(object sender, System.EventArgs e)
		{
			if (fileNames == null)
			{
				MessageBox.Show("Select pictures before starting the show!");
				return;
			}

			senderThread = new Thread(new ThreadStart(SendPictures));
			senderThread.Name = "Sender";
			senderThread.Priority = ThreadPriority.BelowNormal; 
			senderThread.Start();
			
			UIEnableStart(false);
			UIEnableStop(true);
		}

		private void SendPictures()
		{
			InitializeNetwork();

			lock (filesLock)
			{
				int pictureNumber = 1;
				foreach (string fileName in fileNames)
				{
					currentImage = Image.FromFile(fileName);

					Invoke(new MethodInvoker(SetPictureBoxImage));

					SendPicture(currentImage, fileName, pictureNumber);	

					Invoke(new MethodInvokerInt(IncrementProgressBar), new object[] {1});

					Thread.Sleep(pictureIntervalSeconds * 1000);
					pictureNumber++;
				}
			}

			Invoke(new MethodInvoker(ResetProgress));
			Invoke(new MethodInvokerBoolean(UIEnableStart), new object[] {true});
			Invoke(new MethodInvokerBoolean(UIEnableStop), new object[] {false});
		}

		private void ResetProgress()
		{
			progressBar.Value = 0;
			statusBar.Text = "";
		}

		private void SetPictureBoxImage()
		{
			pictureBox.Image = currentImage;
		}

		private void OnStop(object sender, System.EventArgs e)
		{
			// fileIndex = 0;
			progressBar.Value = 0;
			statusBar.Text = "";

			senderThread.Interrupt();

			UIEnableStart(true);
			UIEnableStop(false);
		}

		protected void InitializeNetwork()
		{
			if (udpClient != null)
				udpClient.Close();

			udpClient = new UdpClient();
			groupEP = new IPEndPoint(groupAddress, groupPort);
		}


		protected void SendPicture(Image image, string name, int index)
		{
			string message = "Sending picture " + name;
			Invoke(new MethodInvokerString(SetStatusBar), new Object[] {message});

			PicturePackage[] packages = 
				PicturePackager.GetPicturePackages(name, index, image, 1024);

         // Send all segments of a single picture to the group
			foreach (PicturePackage package in packages)
			{
				byte[] data = encoding.GetBytes(package.GetXml());
				int sendBytes = udpClient.Send(data, data.Length, groupEP);
				if (sendBytes < 0)
					MessageBox.Show("Error in sending");

				Thread.Sleep(300);
			}
			
			message = "Picture " + name + " sent";
			Invoke(new MethodInvokerString(SetStatusBar), new Object[] {message});
		}
		

		public void IncrementProgressBar(int val)
		{
			progressBar.Increment(val);
		}

		public void SetStatusBar(string text)
		{
			statusBar.Text = text;
		}

		private void OnInit(object sender, System.EventArgs e)
		{
			InfoServer info = new InfoServer(groupAddress, groupPort);
			info.Start();

			UIEnableStart(true);
			UIEnableInit(false);
		}

		private void OnHelpAbout(object sender, System.EventArgs e)
		{
			MessageBox.Show(this, "\nPictureShow 1.1\nby Christian Nagel\n\n(c) Copyright 2002-2004", 
				"About", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void OnExit(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

	}
}
