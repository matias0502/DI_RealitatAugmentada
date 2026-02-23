using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Windows.Forms;
using System;
using Emgu.CV.Util;


namespace realitatAugmentada
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private bool _detectarAR = false;
        public Form1()
        {
            InitializeComponent();
            _capture = new VideoCapture(0);
            timer1.Interval = 30;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Mat frame = _capture.QueryFrame())
            {
                if (frame != null)
                {
                    if (_detectarAR)
                    {
                        AplicarlogicaAR(frame);
                    }
                    if (pbVideo.Image != null) pbVideo.Image.Dispose();
                    pbVideo.Image = frame.ToBitmap();

                }
            }

        }
        private void AplicarlogicaAR(Mat frame)
        {
            using (QRCodeDetector detector = new QRCodeDetector())
            {
                using (Mat punts = new Mat())
                {
                    bool trobat = detector.Detect(frame, punts);

                    if (trobat && !punts.IsEmpty)
                    {
                        string dades = detector.Decode(frame, punts);

                        if (!string.IsNullOrEmpty(dades))
                        {

                            CvInvoke.PutText(frame, "QR: " + dades,
                                new System.Drawing.Point(50, 50),
                                FontFace.HersheySimplex, 0.8, new MCvScalar(0, 255, 0), 2);
                        }
                    }
                }
            }
        }
        private void btnEscanejar_click(object sender, EventArgs e)
        {
            _detectarAR = !_detectarAR;
            btnEscanejar.Text = _detectarAR ? "Altura" : "Escanejar";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
