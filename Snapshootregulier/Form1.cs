using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Snapshootregulier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double nb = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button1.Text = "Start";
                textBox2.Text = nb.ToString();
            }
            else
            {
                button1.Text = "Stop";
                nb = Convert.ToDouble(textBox2.Text);
                timer1.Interval = Convert.ToInt32(textBox1.Text);
                timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add(Screen.AllScreens.Count() + " Ecran detecter");
            listBox1.Items.Add("Nom de l'ecrant : ");
            listBox1.Items.Add(Screen.PrimaryScreen.DeviceName);
            listBox1.Items.Add("Hight : " + Screen.PrimaryScreen.Bounds.Height.ToString());
            listBox1.Items.Add("Width : " + Screen.PrimaryScreen.Bounds.Width.ToString());
        }

        public void screen(object a)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save("Img"+a+".jpg", ImageFormat.Jpeg);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nb++;
            listBox2.Items.Add(nb.ToString());
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            var aa = new Thread(screen);
            aa.Start(nb);
        }
    }
}
