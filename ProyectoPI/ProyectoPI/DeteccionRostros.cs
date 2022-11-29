using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaceRecognition;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using AForge.Video;
using AForge.Video.DirectShow;
/// <summary>
/// Imports HaarCascadeClassifer
//Imports HaarCascadeClassifer.HaarDetector
/// </summary>
/// 

using HaarCascadeClassifer;







namespace ProyectoPI
{
    public partial class DeteccionRostros : Form
    {
        public DeteccionRostros()
        {
            InitializeComponent();
        }
        //FaceRec faceRec = new FaceRec(); no sirve

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        FilterInfoCollection filter;
        VideoCaptureDevice device;

        




        private void DeteccionRostros_Load(object sender, EventArgs e)
        {
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)
              comboBox1.Items.Add(device.Name);
            comboBox1.SelectedIndex = 0;
            device = new VideoCaptureDevice();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //faceRec.openCamera(pictureBox1, pictureBox2); // nosirve
            using(OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "JPEG, PNG|*.JPG;*.PNG"})
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);
                    Image<Rgb, Byte> grayImage = new Image<Rgb, Byte>(bitmap);
                    Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.4,0);

                    foreach(Rectangle rectangulo in rectangles)
                    {
                         using( Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            using( Pen lapiz = new Pen (Color.Blue, 1))
                            {
                                 graphics.DrawRectangle(lapiz, rectangulo);
                            }
                        }
                    }
                    pictureBox1.Image=bitmap;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(filter[comboBox1.SelectedIndex].MonikerString);
            device.NewFrame += Device_NewFrame;
            device.Start();

        }

        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone(); 
            Image<Bgr, byte> grayImage = new Image<Bgr, byte> (bitmap);
            Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 1); // aqui se detectan los rostros
            foreach (Rectangle rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle (pen, rectangle); //aqui se dibujan los rectangulos
                    }
                }
            }
            pictureBox1.Image = bitmap;
        }

        private void DeteccionRostros_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(device.IsRunning)
                device.Stop();

        }
    }
}
