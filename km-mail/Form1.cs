using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
namespace km_mail
{
    public partial class Form1 : Form
    {
        string host;
        
        public Form1()
        {
            InitializeComponent();


            
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (IntPtr.Size == 4)
            {
                label2.Text = "CPU: 32 bit";
            }
            else if (IntPtr.Size == 8)
            {
                label2.Text = "CPU: 64 bit";
            }
            label4.Text = "Name: " + System.Environment.MachineName;
            label10.Text = "Time: " + DateTime.Now.ToString("h:mm:ss tt");
            label13.Text = "Current Directory: " + System.Environment.CurrentDirectory;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (groupBox3.Enabled == false && listBox1.Items.Count == 0)
            {
                groupBox3.Enabled = true;
                button3.Enabled = false;
            }
             else
            {
                groupBox3.Enabled = false;
                button3.Enabled = true;
                listBox1.Enabled = true;
                listBox1.Items.Clear();
                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    listBox1.Items.AddRange(textBox1.Lines);
                    //listBox1.Items.Add("end");
                }
            }
        }

       

        private void listBox1_DisplayMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.SelectedIndex += 1;
            int port = Int32.Parse(textBox6.Text);
            var fromAddress = new MailAddress(textBox4.Text, textBox3.Text);
            var toAddress = new MailAddress(listBox1.GetItemText(listBox1.SelectedItem));
            string fromPassword = textBox5.Text;
            string subject = textBox2.Text;
            string body = richTextBox1.Text;
            if (listBox1.SelectedIndex != listBox1.Items.Count - 1)
            {

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
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
                    if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
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
            else if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
            {
                groupBox3.Enabled = true;
                timer1.Enabled = false;   
                listBox1.Items.Clear();
                MessageBox.Show("Your: ("+label1.Text+") emails have been sent!");   
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
                listBox1.Items.Clear();
                
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "number of emails : " + textBox1.Lines.Length;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Gmail")
            {
                textBox6.Text = "587";
                host = "smtp.gmail.com";
            }
            else if (comboBox1.Text == "Outlook")
            {
                textBox6.Text = "587";
                host = "smtp.live.com";
            }
            else if (comboBox1.Text == "Office365")
            {
                textBox6.Text = "587";
                host = "smtp.office365.com";
            }
            else if (comboBox1.Text == "Yahoo Mail")
            {
                textBox6.Text = "465";
                host = "smtp.mail.yahoo.com";
            }
            else if (comboBox1.Text == "Orange")
            {
                textBox6.Text = "25";
                host = "smtp.orange.net";
            }
            else if (comboBox1.Text == "Hotmail")
            {
                textBox6.Text = "465";
                host = "smtp.live.com";
            }
            else if (comboBox1.Text == "zoho Mail")
            {
                textBox6.Text = "465";
                host = "smtp.zoho.com";
            }
            else if (comboBox1.Text == "Mail.com")
            {
                textBox6.Text = "587";
                host = "smtp.mail.com";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }
    }
}
