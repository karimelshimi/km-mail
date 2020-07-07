using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;

namespace km_mail
{
    public partial class activation : Form
    {
        private bool _dragging = false;
        
        private Point _start_point = new Point(0, 0);
        string key;
        void enq()
        {
            foreach (char km in textBox1.Text)
            {
                char encrypted = (char)(km + 1000);
                key += encrypted.ToString();
                
            }
        }
        public activation()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            key = string.Empty;
            Random kkk = new Random();
            int kmm = kkk.Next(1000000000, 2147483647);
            textBox1.Text = kmm.ToString();
            // to encrypt Key By #KaReeM_MoHaMeD
            Thread trd = new Thread(new ThreadStart(enq));
            trd.IsBackground = true;
            trd.Start();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == key)
            {
                if (!Directory.Exists(@"C:/Windows/System32/KM-Mail"))
                {
                    Directory.CreateDirectory(@"C:/Windows/System32/KM-Mail");
                }
                else
                {
                    MessageBox.Show("Error !");
                }
                string path = @"C:/Windows/System32/KM-Mail/km.txt";
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine("KM-Mailer v1.0 by : Kareem Mohamed");
                        tw.WriteLine("-----------------------------------");
                        tw.WriteLine("Your Activaction code:" + key);
                        tw.WriteLine("-----------------------------------");
                        tw.WriteLine("the time: " + DateTime.Now);
                        tw.Close();
                    }
                }

                else if (File.Exists(path))
                {
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine("Activation failure!");
                        tw.Close();
                    }
                }
                MessageBox.Show("Activation successful :)");
                this.Hide();
                Main a = new Main();
                a.Show();
               
            }
            else
            {
                MessageBox.Show("Please make sure you typed the correct KM-Key!");
                textBox1.Text = string.Empty;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            bool connection = NetworkInterface.GetIsNetworkAvailable();
            if (connection != true)
            {
                MessageBox.Show("The internet is not available");
                Environment.Exit(0);
            }

            string path = @"C:/Windows/System32/KM-Mail/km.txt";
            if (File.Exists(path))
            {
            
                    Main a = new Main();
                    a.Show();
                    this.Close();      
            }
   
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false; 
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
  
    }
}
