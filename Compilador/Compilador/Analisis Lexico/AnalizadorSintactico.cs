using Compilador.Manejador_de_errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador.Analisis_Lexico
{
    class AnalizadorSintactico
    {
        private AnalizadorLexico analex;
        private ComponenteLexico preanalisis;
        private void A()
        {
            B();
            APrima();
        }
        private void APrima()
        {
            if ("SUMA".Equals(preanalisis.categoria) || "RESTA".Equals(preanalisis.categoria))
            {
                avanzar(preanalisis.categoria);
                A();
            }

        }
        private void B()
        {
            C();
            BPrima();
        }
        private void BPrima()
        {
            if ("MULTIPLICACION".Equals(preanalisis.categoria) || "DIVISION".Equals(preanalisis.categoria))
            {
                avanzar(preanalisis.categoria);
                B();
            }
        }
        private void C()
        {
            if ("NUMERO ENTERO".Equals(preanalisis.categoria) || "NUMERO DECIMA".Equals(preanalisis.categoria) || "IDENTIFICADOR".Equals(preanalisis.categoria))
            {
                avanzar(preanalisis.categoria);
            }
            else if ("PARENTESIS ABRE".Equals(preanalisis.categoria) ){
                avanzar(preanalisis.categoria);
                A();
                avanzar("PARENTESIS QUE CIERRA");
            }
            else
            {
                //Manejador_de_errores;
            }
        }
        private void avanzar(string categoria)
        {
            if (categoria.Equals(preanalisis.categoria))
            {
                preanalisis = analex.analizar();
            }
            else
            {
                ManejadorErrores manejador = ManejadorErrores.obtenerInstancia();
                Error error = new Error();
                error.tipoError = "SINTACTICO";
                error.descripcionError = "Se esperaba " + categoria + " y se recibio " + preanalisis.categoria;
                error.valorRecibido = preanalisis.categoria;
                error.valorEsperado = categoria;
                manejador.adicionarError(error);
            }
        }
        public void analizar()
        {
            analex = new AnalizadorLexico();
            preanalisis = analex.analizar();
            A();
            if ("@EOF@".Equals(preanalisis.categoria))
            {
                MessageBox.Show("El programa esta bien escrito");

            }
            else
            {
                MessageBox.Show("El programa tiene errores");
            }
        }

    }
}
