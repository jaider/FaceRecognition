using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FaceRecognitionApp
{
    public partial class Form1 : Form
    {
        private VideoCapture cap;
        private CascadeClassifier haar;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Mat nextFrame = cap.QueryFrame())
            {
                if (nextFrame != null)
                {
                    var grayframe = nextFrame.ToImage<Gray, byte>();  //CvInvoke.CvtColor(nextFrame, ???, ColorConversion.Bgr2Gray);
                    var faces = haar.DetectMultiScale(grayframe, 1.4, 4, new Size(grayframe.Width / 8, grayframe.Height / 8));
                    var img = nextFrame.ToImage<Bgr, byte>();
                    foreach (var face in faces)
                    {
                        img.Draw(face, new Bgr(0, 255, 0), 3);
                    }
                    pictureBox1.Image = img.ToBitmap();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // passing 0 gets zeroth webcam
            cap = new VideoCapture(0);
            // adjust path to find your xml
            haar = new CascadeClassifier("haarcascade_frontalface_default.xml");
        }
    }
}
