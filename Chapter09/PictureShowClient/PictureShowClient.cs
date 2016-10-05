using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Configuration;

namespace Apress.Networking.Multicast
{
	public delegate void MethodInvokerInt(int i);
	public delegate void MethodInvokerString(string s);

	/// <summary>
	/// Main form of the Picture Show Client application.
	/// </summary>
	public class PictureClientForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.MenuItem miFileExit;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.MenuItem miFileStart;
		private System.Windows.Forms.MenuItem miFileStop;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.StatusBarPanel statusBarPanelAddress;
		private System.Windows.Forms.StatusBarPanel statusBarPanelPort;
		private System.Windows.Forms.StatusBarPanel statusBarPanelMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private IPAddress groupAddress;	// Multicast group address
		private int groupPort;			// Multicast group port
		private int ttl;
		private UdpClient udpClient;	// Client socket to receive pictures

		private string serverName;	// Hostname of the server
		private int serverInfoPort;	// Port number to request group information

		private bool done = false;	// Flag to end receiving thread

		private UnicodeEncoding encoding = new UnicodeEncoding();

		// Array of all pictures received
		private SortedList pictureArray = new SortedList();

		public PictureClientForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			try
			{
				// Read the application configuration file
				NameValueCollection configuration = 
					ConfigurationSettings.AppSettings;
				serverName = configuration["ServerName"];
				serverInfoPort = int.Parse(configuration["ServerPort"]);
				ttl = int.Parse(configuration["TTL"]);
			}
			catch
			{
				MessageBox.Show("Check the configuration file");
			}

			GetMulticastConfiguration();
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
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.miFile = new System.Windows.Forms.MenuItem();
			this.miFileStart = new System.Windows.Forms.MenuItem();
			this.miFileStop = new System.Windows.Forms.MenuItem();
			this.miFileExit = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusBarPanelMain = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelAddress = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanelPort = new System.Windows.Forms.StatusBarPanel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelAddress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelPort)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.miFile});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miFileStart,
																				   this.miFileStop,
																				   this.miFileExit});
			this.miFile.Text = "&File";
			// 
			// miFileStart
			// 
			this.miFileStart.Index = 0;
			this.miFileStart.Text = "&Start";
			this.miFileStart.Click += new System.EventHandler(this.OnStart);
			// 
			// miFileStop
			// 
			this.miFileStop.Index = 1;
			this.miFileStop.Text = "Sto&p";
			this.miFileStop.Click += new System.EventHandler(this.OnStop);
			// 
			// miFileExit
			// 
			this.miFileExit.Index = 2;
			this.miFileExit.Text = "E&xit";
			this.miFileExit.Click += new System.EventHandler(this.OnExit);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 288);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.statusBarPanelMain,
																						 this.statusBarPanelAddress,
																						 this.statusBarPanelPort});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(432, 22);
			this.statusBar.TabIndex = 1;
			// 
			// statusBarPanelMain
			// 
			this.statusBarPanelMain.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanelMain.Width = 316;
			// 
			// statusBarPanelAddress
			// 
			this.statusBarPanelAddress.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelAddress.ToolTipText = "Multicast Address";
			this.statusBarPanelAddress.Width = 70;
			// 
			// statusBarPanelPort
			// 
			this.statusBarPanelPort.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarPanelPort.ToolTipText = "Multicast Port";
			this.statusBarPanelPort.Width = 30;
			// 
			// pictureBox
			// 
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(432, 288);
			this.pictureBox.TabIndex = 3;
			this.pictureBox.TabStop = false;
			// 
			// PictureClientForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 310);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox,
																		  this.statusBar});
			this.Menu = this.mainMenu;
			this.Name = "PictureClientForm";
			this.Text = "Picture Show Client";
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelAddress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanelPort)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Thread.CurrentThread.Name = "Main";
			Application.Run(new PictureClientForm());
		}

		private void OnExit(object sender, System.EventArgs e)
		{
			Application.Exit();
		}


		private void OnStart(object sender, System.EventArgs e)
		{
			udpClient = new UdpClient(groupPort);
			try
			{
				udpClient.JoinMulticastGroup(groupAddress, ttl);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}		

			Thread t1 = new Thread(new ThreadStart(Listener));
			t1.Name = "Listener";
			t1.IsBackground = true;
			t1.Start();
		}

		private void OnStop(object sender, System.EventArgs e)
		{
			done = true;
			udpClient.DropMulticastGroup(groupAddress);		
		}

		private void DisplayPicture(int id)
		{
			PicturePackage[] packages = (PicturePackage[])pictureArray[id];
			
			Image picture = PicturePackager.GetPicture(packages);

			pictureArray.Remove(id);

			pictureBox.Image = picture;
		}

		public void SetStatusBar(string s)
		{
			statusBarPanelMain.Text = s;
		}

		private void Listener()
		{
			while (!done)
			{
				IPEndPoint ep = null;
				byte[] data = udpClient.Receive(ref ep);

				PicturePackage package = new PicturePackage(encoding.GetString(data));
				
				PicturePackage[] packages;
				if (pictureArray.ContainsKey(package.Id))
				{
					packages = (PicturePackage[])pictureArray[package.Id];
					packages[package.SegmentNumber - 1] = package;
				}
				else
				{
					packages = new PicturePackage[package.NumberOfSegments];
					packages[package.SegmentNumber - 1] = package;
					pictureArray.Add(package.Id, packages);
				}

				string message = "Received picture " + package.Id + " Segment " + package.SegmentNumber;
				Invoke(new MethodInvokerString(SetStatusBar), new object[] {message});

				// Check if all segments of a picture are received
				int segmentCount = 0;
				for (int i=0; i < package.NumberOfSegments; i++)
				{
					if (packages[i] != null)
						segmentCount++;
				}

				// All segments are received, so draw the picture
				if (segmentCount == package.NumberOfSegments)
				{
					this.Invoke(new MethodInvokerInt(DisplayPicture), new object[] {package.Id});
				}
			}
		}	

		private void GetMulticastConfiguration()
		{
			Socket socket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);
			try
			{
				// Get the multicast configuration from the server
				IPHostEntry server = Dns.GetHostByName(serverName);
				socket.Connect(new IPEndPoint(server.AddressList[0], serverInfoPort));
				byte[] buffer = new byte[512];
				int receivedBytes = socket.Receive(buffer);
				if (receivedBytes < 0)
				{
					MessageBox.Show("Error receiving");
					return;
				}
				socket.Shutdown(SocketShutdown.Both);

				string config = encoding.GetString(buffer);
				string[] multicastAddress = config.Split(':');
				groupAddress = IPAddress.Parse(multicastAddress[0]);
				groupPort = int.Parse(multicastAddress[1]);

				this.statusBarPanelAddress.Text = groupAddress.ToString();
				this.statusBarPanelPort.Text = groupPort.ToString();
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode == 10061)
				{
					MessageBox.Show(this, "It seems there is no server running at the host " + 
						serverName + ", Port: " + serverInfoPort, "Error Picture Show",
						MessageBoxButtons.OK, MessageBoxIcon.Error);

				}
				else
				{
					MessageBox.Show(this, ex.Message, "Error Picture Show",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			finally
			{
				socket.Close();
			}
		}
	}
}
