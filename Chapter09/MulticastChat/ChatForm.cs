using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Apress.Networking.Multicast
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ChatForm : System.Windows.Forms.Form
	{
		private bool done = true;	// Flag to stop listener thread 
		private UdpClient client;	// Client socket
		private IPAddress groupAddress;	// Multicast group address
		private int localPort;		// Local port to receive messages
		private int remotePort;		// Remote port to send messages
		private int ttl;			

		private IPEndPoint remoteEP;
		private UnicodeEncoding encoding = new UnicodeEncoding();

		private string name;		// User name in chat
		private string message;		// Message to send 

		private System.Windows.Forms.TextBox textMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox textMessages;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textName;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChatForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			try
			{
				// Read the application configuration file
				NameValueCollection configuration = ConfigurationSettings.AppSettings;
				groupAddress = IPAddress.Parse(configuration["GroupAddress"]);
				localPort = int.Parse(configuration["LocalPort"]);
				remotePort = int.Parse(configuration["RemotePort"]);
				ttl = int.Parse(configuration["TTL"]);
			}
			catch
			{
				MessageBox.Show(this, "Check the application configuration file!", 
					"Error MulticastChat", MessageBoxButtons.OK, MessageBoxIcon.Error);
				buttonStart.Enabled = false;
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ChatForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSend = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.textMessages = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.label2,
																				 this.label1,
																				 this.buttonSend,
																				 this.buttonStop,
																				 this.textMessage,
																				 this.textName,
																				 this.buttonStart});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(568, 112);
			this.panel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(312, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Message:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// buttonSend
			// 
			this.buttonSend.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonSend.Enabled = false;
			this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSend.Location = new System.Drawing.Point(464, 16);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.TabIndex = 6;
			this.buttonSend.Text = "Send";
			this.buttonSend.Click += new System.EventHandler(this.OnSend);
			// 
			// buttonStop
			// 
			this.buttonStop.Enabled = false;
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.Location = new System.Drawing.Point(152, 72);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(104, 23);
			this.buttonStop.TabIndex = 3;
			this.buttonStop.Text = "Stop";
			this.buttonStop.Click += new System.EventHandler(this.OnStop);
			// 
			// textMessage
			// 
			this.textMessage.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textMessage.Location = new System.Drawing.Point(304, 56);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.Size = new System.Drawing.Size(240, 40);
			this.textMessage.TabIndex = 5;
			this.textMessage.Text = "";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Location = new System.Drawing.Point(104, 24);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(152, 20);
			this.textName.TabIndex = 1;
			this.textName.Text = "";
			// 
			// buttonStart
			// 
			this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStart.Location = new System.Drawing.Point(32, 72);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(104, 23);
			this.buttonStart.TabIndex = 2;
			this.buttonStart.Text = "Start";
			this.buttonStart.Click += new System.EventHandler(this.OnStart);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 360);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(568, 22);
			this.statusBar.TabIndex = 2;
			// 
			// textMessages
			// 
			this.textMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textMessages.Location = new System.Drawing.Point(0, 112);
			this.textMessages.Multiline = true;
			this.textMessages.Name = "textMessages";
			this.textMessages.ReadOnly = true;
			this.textMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMessages.Size = new System.Drawing.Size(568, 248);
			this.textMessages.TabIndex = 1;
			this.textMessages.Text = "";
			// 
			// ChatForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 382);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textMessages,
																		  this.statusBar,
																		  this.panel1});
			this.Name = "ChatForm";
			this.Text = "Multicast Chat";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
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
			Application.Run(new ChatForm());
		}

		// Main method of the listener thread to receive the data
		private void Listener()
		{
			done = false;
			try
			{
				while (!done)
				{
					IPEndPoint ep = null;

					byte[] buffer = client.Receive(ref ep);
					message = encoding.GetString(buffer);
				
					this.Invoke(new MethodInvoker(DisplayReceivedMessage));
				}
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode == 10004)
				{
					// ignore cancel blocking call error messages
				}
				else
				{
					MessageBox.Show(this, ex.Message, "Error MulticastChat", 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void DisplayReceivedMessage()
		{
			string time = DateTime.Now.ToString("t");
			textMessages.Text = time + "  " + message + "\r\n" + textMessages.Text;
			statusBar.Text = "Received last message at " + time;
		}

		private void OnStart(object sender, System.EventArgs e)
		{
			name = textName.Text;
			textName.ReadOnly = true;

			try
			{
				// Join the multicast group
				client = new UdpClient(localPort);
				client.JoinMulticastGroup(groupAddress, ttl);
				remoteEP = new IPEndPoint(groupAddress, remotePort);

				// Start the receiving thread
				Thread receiver = new Thread(new ThreadStart(Listener));
				receiver.IsBackground = true;
				receiver.Start();

				// Send the first message to the group
				byte[] data = encoding.GetBytes(name + " has joined the chat");
				client.Send(data, data.Length, remoteEP);

				buttonStart.Enabled = false;
				buttonStop.Enabled = true;
				buttonSend.Enabled = true;
			}
			catch (SocketException ex)
			{
				MessageBox.Show(this, ex.Message, "Error MulticastChat", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void StopListener()
		{
			// Send a leaving message to the group
			byte[] data = encoding.GetBytes(name + " has left the chat");
			client.Send(data, data.Length, remoteEP);

			// Leave the group
			client.DropMulticastGroup(groupAddress);
			client.Close();

			// Tell the receiving thread to stop
			done = true;

			buttonStart.Enabled = true;
			buttonStop.Enabled = false;
			buttonSend.Enabled = false;
		}

		private void OnStop(object sender, System.EventArgs e)
		{
			StopListener();
		}

		private void OnSend(object sender, System.EventArgs e)
		{
			try
			{
				// Sends a message to the group
				byte[] data = encoding.GetBytes(name + ": " + textMessage.Text);
				client.Send(data, data.Length, remoteEP);
				textMessage.Clear();
				textMessage.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error MulticastChat", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}			
		}

		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!done)
				StopListener();
		}
	}
}
