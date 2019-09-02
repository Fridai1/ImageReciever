using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageReciever
{
    public partial class Form1 : Form
    {
        private UdpClient udp = new UdpClient(6789);
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, 6789);
        public Form1()
        {
            InitializeComponent();
            Task.Factory.StartNew(Test);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] imagebytearray = udp.Receive(ref ep);
            Image picture = byteArrayToImage(imagebytearray);
            pictureBox1.Image = picture;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void Test()
        {
            while (true)
            {
                byte[] imagebytearray = udp.Receive(ref ep);
                Image picture = byteArrayToImage(imagebytearray);
                pictureBox1.Image = picture;
            }
            
        }
    }
}
