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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void DeteccionMovimiento_Load(object sender, EventArgs e)
        {

            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // cargar todos los disositivos al combo
            foreach  (FilterInfo x in Dispositivos)
                {
                comboBox1.Items.Add(x.Name);            
                }
            comboBox1.SelectedIndex = 0;
        }
    }
}
