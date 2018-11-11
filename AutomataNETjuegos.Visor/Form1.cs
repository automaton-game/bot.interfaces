using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Fabrica;
using AutomataNETjuegos.JugadorManual;
using AutomataNETjuegos.Logica;
using AutomataNETjuegos.Visor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomataNETjuegos.Visor
{
    public partial class Form1 : Form, IRobotInput
    {
        private const int margen = 1;
        private const int dimensionImg = 50;
        private bool juegoActivo = false;
        private IList<IRobot> jugadores;
        private IJuego2v2 juego;

        public Form1()
        {
            InitializeComponent();
            juego = new FabricaJuego(this).Crear();
            jugadores = juego.GetJugadores().ToArray();
            juego.Iniciar();
            Dibujar();
        }

        public void Dibujar()
        {
            var tablero = juego.Tablero;
            this.flowLayoutPanel1.Controls.Clear();

            var pictures = tablero.Filas.SelectMany(f => f.Casilleros.Select(CreatePictureBox)).ToArray();
            this.flowLayoutPanel1.Controls.AddRange(pictures);
            this.flowLayoutPanel1.Width = (dimensionImg + margen) * tablero.Filas.First().Casilleros.Count + margen +10;
            this.flowLayoutPanel1.Height = (dimensionImg + margen) * tablero.Filas.Count + margen + 10;
        }

        public void Consola(string msg)
        {
            this.listBox1.Items.Add(msg);
        }

        private PicCasillero CreatePictureBox(Casillero casillero)
        {
            var image = ConversorImagen(casillero);
            //image = ChangeColor(image);
            var pic =  new PicCasillero()
            {
                Margin = new Padding(1),
                Casillero = casillero,
                Image = image,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Width = 50,
                Height = 50
            };

            if(image == null)
            {
                pic.BackColor = Color.White;
            }

            return pic;
        }

        public static Bitmap ChangeColor(Bitmap scrBitmap)
        {
            if(scrBitmap == null)
            {
                return scrBitmap;
            }

            //You can change your new color here. Red,Green,LawnGreen any..
            Color newColor = Color.Green;
            Color actualColor;
            //make an empty bitmap the same size as scrBitmap
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    actualColor = scrBitmap.GetPixel(i, j);
                    // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor.A > 150)
                        newBitmap.SetPixel(i, j, newColor);
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                }
            }
            return newBitmap;
        }

        private Bitmap ConversorImagen(Casillero casillero)
        {
            if (casillero.Robot == jugadores[0] && casillero.Muralla == jugadores[0])
            {
                return Resources.robotMurallaAzul;
            }

            if (casillero.Robot == jugadores[1] && casillero.Muralla == jugadores[1])
            {
                return Resources.robotRojoMuralla;
            }

            if (casillero.Robot == jugadores[0] && casillero.Muralla == null)
            {
                return Resources.robotAzul;
            }

            if (casillero.Robot == jugadores[1] && casillero.Muralla == null)
            {
                return Resources.robotRojo;
            }

            if (casillero.Robot == null && casillero.Muralla == jugadores[0])
            {
                return Resources.murallaAzul;
            }

            if (casillero.Robot == null && casillero.Muralla == jugadores[1])
            {
                return Resources.murallaRoja;
            }

            return null;
        }
       
        public string Leer()
        {
            var letra = textBox1.Text;
            textBox1.Text = string.Empty;
            return letra;
        }

        private class PicCasillero : PictureBox
        {
            public Casillero Casillero { get; set; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = ((Control)sender).Text;
                button1.Enabled = !juego.JugarTurno();
            }
            catch (Exception ex)
            {
                Consola(ex.Message);
            }

            pictureBox1.Image = juego.ObtenerRobotTurnoActual() == jugadores.First() ? Resources.robotAzul : Resources.robotRojo;
            Dibujar();
        }
    }
}
