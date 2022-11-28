using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;


namespace ProyectoPI
{
    public partial class Form1 : Form
    {
        //Variable que almacenará los histogramas
        int[,] histoAcumulado;

        public Form1()
        {
            InitializeComponent();
        }
        public int[,] histogramaAcumulado(Bitmap bmp)
        {
            //Creamos una matriz que contendrá el histograma acumulado
            byte Rojo = 0;
            byte Verde = 0;
            byte Azul = 0;
            //Declaramos tres variables que almacenarán los colores
            int[,] matrizAcumulada = new int[3, 256];
            //Recorremos la matriz
            for (int i = 0; i <= bmp.Width - 1; i++)
            {
                for (int j = 0; j <= bmp.Height - 1; j++)
                {
                    Rojo = bmp.GetPixel(i, j).R;
                    //Asignamos el color
                    Verde = bmp.GetPixel(i, j).G;
                    Azul = bmp.GetPixel(i, j).B;
                    //ACumulamos los valores. 
                    matrizAcumulada[0, Rojo] += 1;
                    matrizAcumulada[1, Verde] += 1;
                    matrizAcumulada[2, Azul] += 1;
                }
            }
            return matrizAcumulada;
        }


        //FILTROS

        //ESCALA DE GRISES
        private void Btn_Grises_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Grises(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
        }
        public void Grises(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color bmpColor = bmp.GetPixel(i, j);

                    int red = bmpColor.R;
                    int green = bmpColor.G;
                    int blue = bmpColor.B;

                    int pixel = (red + green + blue) / 3;

                    bmp.SetPixel(i, j, Color.FromArgb(pixel, pixel, pixel));
                }
            }
        }
        //BINARIO
        private void btnBinario_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {

                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Binario(bmp);
                pictureBox1.Image = bmp;

                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }


        }
        public void Binario(Bitmap bmp)
        {
            int aux = trackBar4.Value;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color bmpColor = bmp.GetPixel(i, j);

                    int red = bmpColor.R;
                    int green = bmpColor.G;
                    int blue = bmpColor.B;

                    int gray = (red + green + blue) / 3;

                    if (gray > aux)
                    {
                        gray = 255;
                    }
                    else
                    {
                        gray = 0;
                    }

                    red = gray;
                    green = gray;
                    blue = gray;

                    bmp.SetPixel(i, j, Color.FromArgb(red, green, blue));
                }
            }
        }

        //SEPIA
        private void btnSepia_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Sepia(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
        }
        public void Sepia(Bitmap bmp)
        {            
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    //get pixel value
                    Color bmpColor = bmp.GetPixel(j, i);

                    //extract pixel component ARGB
                    int a = bmpColor.A;
                    int r = bmpColor.R;
                    int g = bmpColor.G;
                    int b = bmpColor.B;

                    //calculate temp value
                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    if (tr > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = tr;
                    }

                    if (tg > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = tg;
                    }

                    if (tb > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = tb;
                    }

                    //set the new RGB value in image pixel
                    bmp.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                }
            }
        }
        //NEGATIVO
        private void btnNegativo_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Negativa(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
        }

        public void Negativa(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color bmpColor = bmp.GetPixel(i, j);

                    int a = bmpColor.A;
                    int r = bmpColor.R;
                    int g = bmpColor.G;
                    int b = bmpColor.B;

                    int Resultpixel = (r + g + b) / 3;

                    Resultpixel = 255 - Resultpixel;

                    bmp.SetPixel(i, j, Color.FromArgb(a, Resultpixel, Resultpixel, Resultpixel));
                }
            }
        }

        //MOSAICO
        private void btnMosaico_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Mosaico(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
           
        }
        public void Mosaico(Bitmap bmp)
        {
            int mosaico = 8;

            int xm = 0;
            int ym = 0;

            Color rColor;
            Color oColor;


            for (int i = 0; i < bmp.Width - mosaico; i += mosaico)
            {
                for (int j = 0; j < bmp.Height - mosaico; j += mosaico)
                {
                    int rs = 0;
                    int gs = 0;
                    int bs = 0;
                    for (xm = i; xm < (i + mosaico); xm++)
                        for (ym = j; ym < (j + mosaico); ym++)
                        {
                            oColor = bmp.GetPixel(xm, ym);
                            rs += oColor.R;
                            gs += oColor.G;
                            bs += oColor.B;
                        }


                    int r = rs / (mosaico * mosaico);
                    int g = gs / (mosaico * mosaico);
                    int b = bs / (mosaico * mosaico);

                    rColor = Color.FromArgb(r, g, b);


                    for (xm = i; xm < (i + mosaico); xm++)
                        for (ym = j; ym < (j + mosaico); ym++)
                        {
                            bmp.SetPixel(xm, ym, rColor);
                        }
                }
            }
        }

        //GRADIANTE DE COLORES
        private void btnGradianteColores_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                Btn_Grises_Click(sender, e);
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                GradianteColores(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
        }
        public void GradianteColores(Bitmap bmp)
        {
            int rojo = trackBar1.Value;
            int verde = trackBar2.Value;
            int azul = trackBar3.Value;

            float r1 = 120;
            float g1 = 120;
            float b1 = 120;

            float r2 = rojo;
            float g2 = verde;
            float b2 = azul;

            int r = 0;
            int g = 0;
            int b = 0;

            float dr = (r2 - r1) / bmp.Width;
            float dg = (g2 - g1) / bmp.Width;
            float db = (b2 - b1) / bmp.Width;

            Color oColor;


            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    oColor = bmp.GetPixel(i, j);

                    r = (int)((r1 / 225.0f) * oColor.R);
                    g = (int)((g1 / 225.0f) * oColor.G);
                    b = (int)((b1 / 225.0f) * oColor.B);

                    if (r > 225) r = 255;
                    else if (r < 0) r = 0;
                    if (g > 225) g = 255;
                    else if (g < 0) g = 0;
                    if (b > 225) b = 255;
                    else if (b < 0) b = 0;



                    bmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
                r1 = (r1 + dr);
                g1 = (g1 + dg);
                b1 = (b1 + db);
            }
        }

        //COLORIZAR
        private void btnColorizar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                Btn_Grises_Click(sender, e);
                Bitmap bmp = new Bitmap((Bitmap)pictureBox1.Image);
                Colorizar(bmp);
                pictureBox1.Image = bmp;
                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
   
        }
        public void Colorizar(Bitmap bmp)
        {
            int rojo = trackBar1.Value;
            int verde = trackBar2.Value;
            int azul = trackBar3.Value;

            double rc = rojo / 255.0;
            double gc = verde / 255.0;
            double bc = azul / 255.0;

            Color miColor = new Color();

            int r = 0;
            int g = 0;
            int b = 0;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    miColor = bmp.GetPixel(i, j);

                    r = (int)(miColor.R * rc);
                    g = (int)(miColor.G * gc);
                    b = (int)(miColor.B * bc);

                    bmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }
        }

        //____________________________________________________________________________________

        private void addPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openphoto = new OpenFileDialog();

            if (openphoto.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openphoto.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = new Bitmap(openphoto.FileName);
            }
        }



        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No hay imagen seleccionada.");
            }
            else
            {
                Image imagen = pictureBox1.Image;
                saveFileDialogSave.ShowDialog();
                imagen.Save(saveFileDialogSave.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No hay imagen seleccionada.");
            }
            else
            {
                Image imagen = pictureBox1.Image;
                saveFileDialogSave.ShowDialog();
                imagen.Save(saveFileDialogSave.FileName);
            }
        }
        //Histograma rojo
         private void btnRojo_Click(object sender, EventArgs e)
        {
            //Borramos el posible contenido del chart
            chart1.Series["Histograma"].Points.Clear();
            //Los ponesmos del colores correspondiente
            chart1.Series["Histograma"].Color = Color.Red;
            for (int i = 0; i <= 255; i++)
            {
                chart1.Series["Histograma"].Points.AddXY(i + 1, histoAcumulado[0, i]);
            }
        }
        //Histograma verde
        private void btnVerde_Click(object sender, EventArgs e)
        {
            //Borramos el posible contenido del chart
            chart2.Series["Histograma"].Points.Clear();
            chart2.Series["Histograma"].Color = Color.Green;
            for (int i = 0; i <= 255; i++)
            {
                chart2.Series["Histograma"].Points.AddXY(i + 1, histoAcumulado[1, i]);
            }
        }
        //Histograma azul
        private void btnAzul_Click(object sender, EventArgs e)
        {
            //Borramos el posible contenido del chart
            chart3.Series["Histograma"].Points.Clear();
            chart3.Series["Histograma"].Color = Color.Blue;
            for (int i = 0; i <= 255; i++)
            {
                chart3.Series["Histograma"].Points.AddXY(i + 1, histoAcumulado[2, i]);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFile.FileName);
                pictureBox2.Image = new Bitmap(openFile.FileName);
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {

            Video form2 = new Video();
            form2.ShowDialog();
        }

        private void btnOriginal_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Debe agregar una imagen.");
                btnAbrir_Click(sender, e);
            }
            else
            {

                Bitmap bmp = new Bitmap((Bitmap)pictureBox2.Image);
                pictureBox1.Image = pictureBox2.Image;


                histoAcumulado = histogramaAcumulado(bmp);
                btnRojo_Click(sender, e);
                btnVerde_Click(sender, e);
                btnAzul_Click(sender, e);
            }
        }

        private void addVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Video form2 = new Video();
            form2.ShowDialog();
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label8.Text ="R: " + trackBar1.Value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label9.Text = "G: " + trackBar2.Value.ToString();
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label10.Text = "B: " + trackBar3.Value.ToString();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label11.Text = trackBar4.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void deteccionMovimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeteccionMovimiento form3 = new DeteccionMovimiento();
            form3.ShowDialog();
        }

        private void deteccionDeRostrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeteccionRostros form4 = new DeteccionRostros();
            form4.ShowDialog();
        }
    }
}
