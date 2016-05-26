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
        private AnalizadorLexico analizadorLexico;
        private ComponenteLexico preAnalisis;

        private Stack<double> pilaCalculos = new Stack<double>();


 

        public void Analizar()
        {
            try
            {
                analizadorLexico = new AnalizadorLexico();
                preAnalisis = analizadorLexico.analizar();//El primer componente
                A();

                if ("FIN ARCHIVO".Equals(preAnalisis.categoria))
                {
                    if (ManejadorErrores.obtenerInstancia().existenErrores())
                    {
                        MessageBox.Show("El programa tiene errores");
                    }
                    else
                    {
                        MessageBox.Show("El programa está bien escrito");
                    }
                    MessageBox.Show(""+pilaCalculos.Pop());
                }
                else
                {
                    MessageBox.Show("El programa tiene errores");
                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void A()
        {
            B();
            APrima();
        }

        private void APrima()
        {
            if ("SUMA".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                A();
                double operandoDerecho = pilaCalculos.Pop();
                double operandoIzquierdo = pilaCalculos.Pop();
                pilaCalculos.Push(operandoIzquierdo + operandoDerecho);
            }
            else if ("RESTA".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                A();
                double operandoDerecho = pilaCalculos.Pop();
                double operandoIzquierdo = pilaCalculos.Pop();
                pilaCalculos.Push(operandoIzquierdo - operandoDerecho);
            }
            //El epsilon es no hacer nada
        }

        private void B()
        {
            C();
            BPrima();
        }

        private void BPrima()
        {
            if ("MULTIPLICACION".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                B();
                double operandoDerecho = pilaCalculos.Pop();
                double operandoIzquierdo = pilaCalculos.Pop();
                pilaCalculos.Push(operandoIzquierdo * operandoDerecho);
            }
            else if ("DIVISION".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                B();
                double operandoDerecho = pilaCalculos.Pop();
                double operandoIzquierdo = pilaCalculos.Pop();
                if (operandoDerecho.Equals(0))
                {
                    Error error = new Error();
                    error.valorRecibido = "0";
                    error.posicionInicial = preAnalisis.posicionInicial;
                    error.posicionFinal = preAnalisis.posicionFinal;
                    error.numLinea = preAnalisis.numLinea;
                    error.valorEsperado = "CUALQUIER OTRO NUMERO";
                    error.descripcionError = "DIVISION POR CERO";
                    error.tipoError = "SEMANTICO";
                    ManejadorErrores.obtenerInstancia().adicionarError(error);
                    throw new Exception("ERROR FATAL, SEMANTICO");
                }
                else
                {
                    pilaCalculos.Push(operandoIzquierdo / operandoDerecho);
                }
            }
        }

        private void C()
        {
            if ("NUMERO ENTERO".Equals(preAnalisis.categoria))
            {
                pilaCalculos.Push(Double.Parse(preAnalisis.lexema));
                Avanzar(preAnalisis.categoria);

            }
            else if("NUMERO DECIMAL".Equals(preAnalisis.categoria))
            {
                pilaCalculos.Push(Double.Parse(preAnalisis.lexema));
                Avanzar(preAnalisis.categoria);
            }
            else if ("IDENTIFICADOR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else if ("PARENTESIS ABRE".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                A();
                Avanzar("PARENTESIS CIERRA");
            }

            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "NUMERO ENTERO, NUMERO DECIMAL, INDETIFICADOR, PARENTESIS QUE ABRE";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);

                preAnalisis.categoria = "NUMERO ENTERO DUMMY";
                Avanzar(preAnalisis.categoria);

            }
        }

        private void Avanzar(String categoria)
        {
            if (categoria.Equals(preAnalisis.categoria))
            {
                preAnalisis = analizadorLexico.analizar();
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = preAnalisis.categoria;
                error.descripcionError = "ESPERABA " + categoria + " pero reibí " + preAnalisis.categoria;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO");
            }
        }

    }
}
