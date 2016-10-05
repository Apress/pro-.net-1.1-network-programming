using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MailClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MailForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnReceive;
		private System.Windows.Forms.TextBox txtPopServer;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage recvTab;
		private System.Windows.Forms.TabPage sendTab;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtSmtpServer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelSubject;
		private System.Windows.Forms.Label labelTo;
		private System.Windows.Forms.Label labelFrom;
		private System.Windows.Forms.Label labelServer;
		private System.Windows.Forms.ListBox lstLog;
		private System.Windows.Forms.ListBox lstLog2;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.TextBox txtEmails;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MailForm()
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.sendTab = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.lstLog = new System.Windows.Forms.ListBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtSmtpServer = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.labelSubject = new System.Windows.Forms.Label();
			this.labelTo = new System.Windows.Forms.Label();
			this.labelFrom = new System.Windows.Forms.Label();
			this.labelServer = new System.Windows.Forms.Label();
			this.recvTab = new System.Windows.Forms.TabPage();
			this.txtEmails = new System.Windows.Forms.TextBox();
			this.lstLog2 = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.txtPopServer = new System.Windows.Forms.TextBox();
			this.btnReceive = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.sendTab.SuspendLayout();
			this.recvTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.sendTab);
			this.tabControl1.Controls.Add(this.recvTab);
			this.tabControl1.Location = new System.Drawing.Point(0, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(408, 360);
			this.tabControl1.TabIndex = 0;
			// 
			// sendTab
			// 
			this.sendTab.Controls.Add(this.label5);
			this.sendTab.Controls.Add(this.lstLog);
			this.sendTab.Controls.Add(this.btnSend);
			this.sendTab.Controls.Add(this.txtMessage);
			this.sendTab.Controls.Add(this.txtSubject);
			this.sendTab.Controls.Add(this.txtTo);
			this.sendTab.Controls.Add(this.txtFrom);
			this.sendTab.Controls.Add(this.txtSmtpServer);
			this.sendTab.Controls.Add(this.label6);
			this.sendTab.Controls.Add(this.labelSubject);
			this.sendTab.Controls.Add(this.labelTo);
			this.sendTab.Controls.Add(this.labelFrom);
			this.sendTab.Controls.Add(this.labelServer);
			this.sendTab.Location = new System.Drawing.Point(4, 22);
			this.sendTab.Name = "sendTab";
			this.sendTab.Size = new System.Drawing.Size(400, 334);
			this.sendTab.TabIndex = 0;
			this.sendTab.Text = "Send";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Status Messages:";
			// 
			// lstLog
			// 
			this.lstLog.Location = new System.Drawing.Point(8, 120);
			this.lstLog.Name = "lstLog";
			this.lstLog.Size = new System.Drawing.Size(376, 82);
			this.lstLog.TabIndex = 11;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(312, 304);
			this.btnSend.Name = "btnSend";
			this.btnSend.TabIndex = 5;
			this.btnSend.Text = "&Send";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(8, 224);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(376, 72);
			this.txtMessage.TabIndex = 4;
			this.txtMessage.Text = "";
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(88, 80);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(296, 20);
			this.txtSubject.TabIndex = 3;
			this.txtSubject.Text = "";
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(88, 56);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(296, 20);
			this.txtTo.TabIndex = 2;
			this.txtTo.Text = "";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(88, 32);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Size = new System.Drawing.Size(296, 20);
			this.txtFrom.TabIndex = 1;
			this.txtFrom.Text = "";
			// 
			// txtSmtpServer
			// 
			this.txtSmtpServer.Location = new System.Drawing.Point(88, 8);
			this.txtSmtpServer.Name = "txtSmtpServer";
			this.txtSmtpServer.Size = new System.Drawing.Size(296, 20);
			this.txtSmtpServer.TabIndex = 0;
			this.txtSmtpServer.Text = "";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 208);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95, 16);
			this.label6.TabIndex = 4;
			this.label6.Text = "Message to Send:";
			// 
			// labelSubject
			// 
			this.labelSubject.AutoSize = true;
			this.labelSubject.Location = new System.Drawing.Point(8, 80);
			this.labelSubject.Name = "labelSubject";
			this.labelSubject.Size = new System.Drawing.Size(45, 16);
			this.labelSubject.TabIndex = 3;
			this.labelSubject.Text = "Subject:";
			// 
			// labelTo
			// 
			this.labelTo.AutoSize = true;
			this.labelTo.Location = new System.Drawing.Point(8, 56);
			this.labelTo.Name = "labelTo";
			this.labelTo.Size = new System.Drawing.Size(21, 16);
			this.labelTo.TabIndex = 2;
			this.labelTo.Text = "To:";
			// 
			// labelFrom
			// 
			this.labelFrom.AutoSize = true;
			this.labelFrom.Location = new System.Drawing.Point(8, 32);
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.Size = new System.Drawing.Size(34, 16);
			this.labelFrom.TabIndex = 1;
			this.labelFrom.Text = "From:";
			// 
			// labelServer
			// 
			this.labelServer.AutoSize = true;
			this.labelServer.Location = new System.Drawing.Point(8, 8);
			this.labelServer.Name = "labelServer";
			this.labelServer.Size = new System.Drawing.Size(70, 16);
			this.labelServer.TabIndex = 0;
			this.labelServer.Text = "Smtp Server:";
			// 
			// recvTab
			// 
			this.recvTab.Controls.Add(this.txtEmails);
			this.recvTab.Controls.Add(this.lstLog2);
			this.recvTab.Controls.Add(this.label4);
			this.recvTab.Controls.Add(this.txtPassword);
			this.recvTab.Controls.Add(this.txtUserName);
			this.recvTab.Controls.Add(this.txtPopServer);
			this.recvTab.Controls.Add(this.btnReceive);
			this.recvTab.Controls.Add(this.label1);
			this.recvTab.Controls.Add(this.label3);
			this.recvTab.Controls.Add(this.label2);
			this.recvTab.Location = new System.Drawing.Point(4, 22);
			this.recvTab.Name = "recvTab";
			this.recvTab.Size = new System.Drawing.Size(400, 334);
			this.recvTab.TabIndex = 1;
			this.recvTab.Text = "Receive";
			// 
			// txtEmails
			// 
			this.txtEmails.Location = new System.Drawing.Point(8, 208);
			this.txtEmails.Multiline = true;
			this.txtEmails.Name = "txtEmails";
			this.txtEmails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtEmails.Size = new System.Drawing.Size(376, 88);
			this.txtEmails.TabIndex = 4;
			this.txtEmails.Text = "";
			// 
			// lstLog2
			// 
			this.lstLog2.Location = new System.Drawing.Point(8, 88);
			this.lstLog2.Name = "lstLog2";
			this.lstLog2.Size = new System.Drawing.Size(376, 95);
			this.lstLog2.TabIndex = 11;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Messages:";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(72, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(312, 20);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.Text = "";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(72, 32);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(312, 20);
			this.txtUserName.TabIndex = 1;
			this.txtUserName.Text = "";
			// 
			// txtPopServer
			// 
			this.txtPopServer.Location = new System.Drawing.Point(72, 8);
			this.txtPopServer.Name = "txtPopServer";
			this.txtPopServer.Size = new System.Drawing.Size(312, 20);
			this.txtPopServer.TabIndex = 0;
			this.txtPopServer.Text = "";
			// 
			// btnReceive
			// 
			this.btnReceive.Location = new System.Drawing.Point(312, 304);
			this.btnReceive.Name = "btnReceive";
			this.btnReceive.TabIndex = 3;
			this.btnReceive.Text = "&Receive";
			this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Password:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "POP Server";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "UserName:";
			// 
			// MailForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 366);
			this.Controls.Add(this.tabControl1);
			this.Name = "MailForm";
			this.Text = "Email Client";
			this.tabControl1.ResumeLayout(false);
			this.sendTab.ResumeLayout(false);
			this.recvTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MailForm());
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{	
			// change the cursor to hourglass
			Cursor.Current = Cursors.WaitCursor;
		
			string sendString;

			byte [] dataToSend;

			string receiveData;

			try
			{
				// creating an instance of the TcpClient class
				TcpClient smtpServer = new TcpClient(txtSmtpServer.Text, 25);
				
				lstLog.Items.Add("Connection Established with " + txtSmtpServer.Text);
				
				// creating stream classes for communication
				NetworkStream writeStream = smtpServer.GetStream();
				StreamReader readStream = new StreamReader(smtpServer.GetStream());
				
				// receiving connection success
				receiveData = readStream.ReadLine();				
				lstLog.Items.Add(receiveData);

				// sending HELO followed by the sending hostname to start the mail session
				sendString = "HELO "+Dns.GetHostName()+"\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);
				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);

				// sending From Email Address
				sendString = "MAIL FROM: " + "<" + txtFrom.Text + ">\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);
                
				// sending To Email Address
				sendString = "RCPT TO: " + "<" + txtTo.Text + ">\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);

				// sending data
				sendString = "DATA " + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);
				
				// sending Message Subject and Text
				sendString = "SUBJECT: " + txtSubject.Text + "\r\n" + txtMessage.Text + "\r\n" + "." + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);

				// sending Disconnect from Server
				sendString = "QUIT " + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog.Items.Add(receiveData);

				// closing all open resources
				writeStream.Close();
				readStream.Close();

				smtpServer.Close();
			}
			catch(SocketException se)
			{
				MessageBox.Show("SocketException:" + se.ToString());
			}
			catch(Exception excep)
			{
					MessageBox.Show("Exception:" + excep.ToString());
			}

		}

		private void btnReceive_Click(object sender, System.EventArgs e)
		{

			// change the cursor to hourglass
			Cursor appCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
		
			string sendString;

			byte [] dataToSend;

			string receiveData;

			try
			{
				TcpClient popServer = new TcpClient(txtPopServer.Text,110);
				
				NetworkStream writeStream = popServer.GetStream();				
				StreamReader readStream = new StreamReader(popServer.GetStream());

				// connect with the server
				receiveData = readStream.ReadLine();
				lstLog2.Items.Add(receiveData);

				// sending username to the server
				sendString = "USER " + txtUserName.Text + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog2.Items.Add(receiveData);

				// sending password to the server
				sendString = "PASS " + txtPassword.Text + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog2.Items.Add(receiveData);

				// getting the number of emails on the server
				sendString = "STAT" + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog2.Items.Add(receiveData);
				
				// parse the returned number into integer
				Match m = Regex.Match(receiveData,@"(\s\d*\s)");
				int nMails = 0;

				if (m.Success)
				{
					nMails = int.Parse(m.ToString());
				}

				// reading emails in a loop
				
				
				for (int index = 1; index <= nMails; index++)
				{
					string szTemp;

					// reading individual email
					MessageBox.Show("Reading Email:" + index.ToString());
					sendString = "RETR " + index.ToString() + "\r\n";
					dataToSend = Encoding.ASCII.GetBytes(sendString);
					writeStream.Write(dataToSend,0,dataToSend.Length);

					receiveData = readStream.ReadLine();
					szTemp = receiveData;
					
					while(receiveData != ".")
					{	
						receiveData = readStream.ReadLine();
						szTemp = szTemp + "\r\n" + receiveData;
					}
					
					txtEmails.Text = txtEmails.Text + 
							index.ToString() + "\r\n" + 
							szTemp + "\r\n";
					//lstMsgList.Items.Add(receiveData);

				}
				


				// Disconnect from the server
				sendString = "QUIT" + "\r\n";
				dataToSend = Encoding.ASCII.GetBytes(sendString);
				writeStream.Write(dataToSend,0,dataToSend.Length);

				receiveData = readStream.ReadLine();
				lstLog2.Items.Add(receiveData);

				// freeing all open resources
				writeStream.Close();
				readStream.Close();
	
				popServer.Close();
			}
			catch(SocketException se)
			{
				MessageBox.Show("SocketException:" + se.ToString());
			}
			catch(Exception excep)
			{
				MessageBox.Show("Exception:" + excep.ToString());
			}

			// restoring the cursor state
			Cursor.Current = appCursor;

		}
	}
}
