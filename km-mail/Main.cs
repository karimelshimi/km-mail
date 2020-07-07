using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace km_mail
{

    public partial class Main : Form
    {
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        string host;
        
        sec1 osa = new sec1();
        
        public Main()
        {
            InitializeComponent();
           

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            bool connection = NetworkInterface.GetIsNetworkAvailable();
           if (connection != true)
           {
               MessageBox.Show("The internet is not available", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
               Environment.Exit(0);
           }

           else
           {
            //   MessageBox.Show("hi " + System.Environment.UserName + " !" + Environment.NewLine + "-----------" + Environment.NewLine + "KM-Mailer" + Environment.NewLine + "-----------" + Environment.NewLine + "Programmer: Kareem Mohamed" + Environment.NewLine + "-----------" + Environment.NewLine + "Version: 1.0.0 " + Environment.NewLine + "-----------", "welcome");         
           }
           add_email_list.AllowDrop = true;
      
           if (IntPtr.Size == 4)
            {
                cpu_label.Text = "CPU: 32 bit";
            }
            else if (IntPtr.Size == 8)
            {
                cpu_label.Text = "CPU: 64 bit";
            }
            username_label.Text = "Machine Name : " + System.Environment.MachineName + "    Username : " + System.Environment.UserName;   
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (your_name.Text == string.Empty || your_email.Text == string.Empty || password.Text == string.Empty || server_.Text == string.Empty )
            {
                MessageBox.Show("Check your info !" + System.Environment.NewLine + "-----------" + System.Environment.NewLine + "1- email or password is incorrect" + System.Environment.NewLine + "2- Choose correct port", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer1.Stop();
                timer1.Enabled = false;
            }
            if (timer1.Enabled == true)
            {
                MessageBox.Show("Sending Now!");
            }
            if (add_email_list.Text == string.Empty && email_list.Items.Count ==0)
            {
                MessageBox.Show("Please type email address to send message to it", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          else            
            timer1.Enabled = true;        
        }

        private void button4_Click(object sender, EventArgs e)
        {
           if (add_email_list_group.Enabled == false && email_list.Items.Count == 0)
            {
                add_email_list_group.Enabled = true;
                send_button.Enabled = false;
            }
            else
            {
                add_email_list_group.Enabled = false;
                send_button.Enabled = true;
                email_list.Enabled = true;
                email_list.Items.Clear();
                if (!String.IsNullOrEmpty(add_email_list.Text))
                {
                    email_list.Items.AddRange(add_email_list.Lines);
                    //listBox1.Items.Add("end");
                }
            }
        }

       

        private void listBox1_DisplayMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            try
            {
                email_list.SelectedIndex += 1;
                // int port = Int32.Parse(port_text.Text);
                var fromAddress = new MailAddress(your_email.Text, your_name.Text);
                var toAddress = new MailAddress(email_list.GetItemText(email_list.SelectedItem));
                string fromPassword = password.Text;
                string subject = subject_text.Text;
                string body = message_text.Text;

                if (email_list.SelectedIndex != email_list.Items.Count - 1)
                {

                    var smtp = new SmtpClient
                    {
                        Host = host,
                        Port = osa.portt,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 20000
                    };
                    
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        if (email_list.SelectedIndex == email_list.Items.Count - 1)
                        {
                            timer1.Enabled = false;
                        }
                        else
                        {
                            smtp.Send(message);
                            timer1.Enabled = true;
                        }

                    }
                }
                else if (email_list.SelectedIndex == email_list.Items.Count - 1)
                {

                    timer1.Interval = 999999999;
                    timer1.Enabled = false;
                    int mm = add_email_list.Lines.Length;
                    MessageBox.Show(mm + " emails have been sent!", "Info !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    add_email_list_group.Enabled = true;
                    email_list.Items.Clear();
                    send_button.Enabled = false;

                }
            }
            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("Check your info !" + System.Environment.NewLine + "-----------" + System.Environment.NewLine + "1- email or password is incorrect" + System.Environment.NewLine + "2- check your email security is off", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            send_button.Enabled = false;
                email_list.Items.Clear();                             
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int mm = add_email_list.Lines.Length;
            label1.Text = "Number of emails : " +mm ;
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (server_.Text == "Gmail")
            {
               
                host = "smtp.gmail.com";
            }
            else if (server_.Text == "Yahoo Mail")
            {
                
                host = "smtp.mail.yahoo.com";
            }
             else if (server_.Text == "Outlook (live - hotmail ...etc)")
             {
                
                 host = "smtp.live.com";
             }
             else if (server_.Text == "Office365")
             {
                
                 host = "smtp.office365.com";
             }
             
             else if (server_.Text == "Orange")
             {
                 
                 host = "smtp.orange.net";
             }
             else if (server_.Text == "zoho Mail")
             {
                 
                 host = "smtp.zoho.com";
             }
             else if (server_.Text == "Mail.com")
             {
                 
                 host = "smtp.mail.com";
             }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            add_email_list.Paste();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            add_email_list.Text = string.Empty;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            about_button.BackColor = Color.SlateGray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            about_button.BackColor = Color.White;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }
        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; 
        }
        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void tabPage1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }
        private void tabPage1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; 
        }
        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; 
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            time_label.Text = "Time: " + DateTime.Now.ToString("h:mm:ss tt");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
            //MessageBox.Show("Bye " + System.Environment.UserName + " :(" + Environment.NewLine + "-----------" + Environment.NewLine + "KM-Mailer" + Environment.NewLine + "-----------" + Environment.NewLine + "Programmer: Kareem Mohamed" + Environment.NewLine + "-----------" + Environment.NewLine + "Version: 1.0.0 " + Environment.NewLine + "-----------");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            message_text.SelectAll();
            message_text.Cut();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(message_text.Text);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            message_text.Paste();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            message_text.Clear();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);
            
            idea_label.ForeColor = randomColor;
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (server_.Text == string.Empty)
            {
                MessageBox.Show("Please choose server ! ex.yahoo-gmail-hotmail", "KM-Mail | INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                your_email.Clear();
            }
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            exit_button.BackColor = Color.DimGray;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            exit_button.BackColor = Color.Black;
        }

        private void wtp_ads_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.way2prof.com/");
        }
        private static void WebBrowserVersionEmulation()
        {
            const string BROWSER_EMULATION_KEY =
            @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            //
            // app.exe and app.vshost.exe
            String appname = Process.GetCurrentProcess().ProcessName + ".exe";
            //
            // Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
            const int browserEmulationMode = 9999;

            RegistryKey browserEmulationKey =
                Registry.CurrentUser.OpenSubKey(BROWSER_EMULATION_KEY, RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                Registry.CurrentUser.CreateSubKey(BROWSER_EMULATION_KEY);

            if (browserEmulationKey != null)
            {
                browserEmulationKey.SetValue(appname, browserEmulationMode, RegistryValueKind.DWord);
                browserEmulationKey.Close();
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url != osa.uri1 && webBrowser1.Url != osa.uri)
            {
                webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
                webBrowser1.Size = new Size(1,1);
                webBrowser1.Visible = false;
                pictureBox1.Visible = false;
                MessageBox.Show("Thank you");
            }

        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string s = File.ReadAllText(theDialog.FileName);
                add_email_list.Text = s;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; 
        }

        

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";//|Excel Files|*.xls;*.xlsx;*.xlsm";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                email_list.Items.Clear();
                string s = File.ReadAllText(theDialog.FileName);

                if (!String.IsNullOrEmpty(s))
                {
                    email_list.Items.AddRange(File.ReadAllLines(theDialog.FileName));
                    add_button.Enabled = false;
                    send_button.Enabled = true;
                    email_list.Enabled = true;
                    //listBox1.Items.Add("end");
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           

        }

        private void title_label_MouseDown(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void title_label_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void title_label_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ouo.io/D72qO0");
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            webBrowser1.Navigate(webBrowser1.StatusText);
            e.Cancel = true;
        }
    }
}
