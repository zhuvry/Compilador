using Compilador.Analisis_Lexico;
using Compilador.Cache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        private ProgramaFuente programaFuente = ProgramaFuente.obtenerInstancia();
        private AnalizadorLexico analizadorLexico = new AnalizadorLexico();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxPrevisualizacion.Clear();
            if (programaFuente.getLineas() == null)
            {
                return;
            }
            for(int i = 1; i <= programaFuente.getLineas().Count; i++)
            {
                Linea lineaActual = programaFuente.getLinea(i);
                textBoxPrevisualizacion.AppendText(i + ".");
                textBoxPrevisualizacion.AppendText(lineaActual.getContenido());
                if (i != programaFuente.getLineas().Count-1)
                {
                    textBoxPrevisualizacion.AppendText("\n");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
                System.IO.StreamReader sr = new                
                System.IO.StreamReader(textBox3.Text);
                int linea = 0;
                List<Linea> listaLineas = new List<Linea>();
        
                while (!sr.EndOfStream)
                {
                    Linea lineaLeida = new Linea(linea,sr.ReadLine());
                    listaLineas.Add(lineaLeida);
                    linea++;
                }
                //Cerramos el archivo y guardamos en cache
                sr.Close();
                programaFuente.setLineas(listaLineas);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] lineas = textBoxConsole.Lines;
            List<Linea> listaLineas = new List<Linea>();
            for(int i = 0; i < lineas.Count(); i++)
            {
                Linea lineaLeida = new Linea(i, lineas[i]);
                listaLineas.Add(lineaLeida);
            }
            programaFuente.setLineas(listaLineas);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionado = comboBox1.SelectedItem.ToString();
            if (seleccionado == "Consola")
            {
                mostrarElementosConsola();
                ocultarElementosArchivo();
            }
            if(seleccionado == "Archivo")
            {
                mostrarElementosArchivo();
                ocultarElementosConsola();
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void mostrarElementosArchivo()
        {
            label2.Visible = true;
            textBox3.Visible = true;
            button2.Visible = true;
        }
        private void ocultarElementosArchivo()
        {
            label2.Visible = false;
            textBox3.Visible = false;
            button2.Visible = false;
        }
        private void mostrarElementosConsola()
        {
            label4.Visible = true;
            textBoxConsole.Visible = true;
            button3.Visible = true;
        }
        private void ocultarElementosConsola()
        {
            label4.Visible = false;
            textBoxConsole.Visible = false;
            button3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComponenteLexico comp = analizadorLexico.analizar();
            MessageBox.Show(comp.lexema);
            
        }
    }
}
