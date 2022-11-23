using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

namespace ProyectoPI
{
    public partial class DeteccionMovimiento : Form
    {

        //AGREGAR REFERENCIAS
        //AGREGAR CNTROLES
        //DISEÑAR FORMULARIO
        //VideoSourcePlayer1 es el control donde se motrara la imagen de la webcam

        public DeteccionMovimiento()
        {
            InitializeComponent();
        }
        //Variable para lista de dispositivos
        private FilterInfoCollection Dispositivos;

        //variable para fuente de  video
        private VideoCaptureDevice FuenteDeVideo;

        //variable ara la deteccion 
        MotionDetector Detector;
        float NivelDeDeteccion;

        private void button1_Click(object sender, EventArgs e)
        {
            //Establecer el dispisitivo seleccionado  como fuente de video
            FuenteDeVideo = new VideoCaptureDevice(Dispositivos[comboBox1.SelectedIndex].MonikerString);
            //Inicializar el control
            videoSourcePlayer1.VideoSource = FuenteDeVideo;
            //Iniciar recepcion de Imagenes
            videoSourcePlayer1.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Detener recepcion de Imagenes
            videoSourcePlayer1.SignalToStop();
        }

        private void DeteccionMovimiento_Load(object sender, EventArgs e)
        {
            Detector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
            NivelDeDeteccion = 0;

            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // cargar todos los disositivos al combo
            foreach  (FilterInfo x in Dispositivos)
                {
                comboBox1.Items.Add(x.Name);            
                }
            comboBox1.SelectedIndex = 0;
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            NivelDeDeteccion = Detector.ProcessFrame(image);    
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // textBox1.Text = NivelDeDeteccion.ToString();
            textBox1.Text = String.Format("{0:00.0000}", NivelDeDeteccion);
        }
    }
}
