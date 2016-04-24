using Compilador.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.Tabla_de_simbolos;
using Compilador.Manejador_de_errores;

namespace Compilador.Analisis_Lexico
{
    public class AnalizadorLexico
    {
        private Boolean esPrimerLlamado = true;
        private int numLineaActual = 0;
        private string contenidoLineaActual;
        private int puntero;
        private string caracterActual;
        private string lexema;
        private int estadoActual;
        private ComponenteLexico componente;

        private void cargarNuevaLinea()
        {
            numLineaActual += 1;
            if (ProgramaFuente.obtenerInstancia().getLineas().Count() >= numLineaActual)
            {
                ProgramaFuente inst = ProgramaFuente.obtenerInstancia();

                contenidoLineaActual = inst.getLinea(numLineaActual).getContenido();
            }
            else {
                contenidoLineaActual = "@EOF@";
            }
            puntero = 1;

        }

        private void leerSiguienteCaracter()
        {

            if (contenidoLineaActual.Equals("@EOF@"))
            {
                caracterActual = "@EOF@";

            }
            else if (contenidoLineaActual.Length >= puntero)
            {

                caracterActual = contenidoLineaActual.Substring(puntero - 1, 1);
            }
            else {
                caracterActual = "@FL@";
            }
            puntero += 1;
        }

        private void devolverPuntero()
        {
            puntero -= 1;
        }

        private void reiniciarVariables()
        {
            componente = null;
            caracterActual = "";
            lexema = "";
            estadoActual = 0;
        }

        public ComponenteLexico analizar()
        {
            if (esPrimerLlamado)
            {
                cargarNuevaLinea();
                esPrimerLlamado = false;
            }

            reiniciarVariables();

            Boolean continuarAnalisis = true;
            while (continuarAnalisis)
            {

                switch (estadoActual)
                {
                    case 0:
                        leerSiguienteCaracter();
                        while (caracterActual.Equals(" "))
                        {
                            leerSiguienteCaracter();
                        }
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else if (Char.IsLetter(caracterActual.ToCharArray()[0]) || caracterActual.Equals("$") || caracterActual.Equals("_"))
                        {
                            lexema += caracterActual;
                            estadoActual = 4;
                        }
                        else if (caracterActual.Equals("+"))
                        {
                            lexema += caracterActual;
                            estadoActual = 5;
                        }
                        else if (caracterActual.Equals("-"))
                        {
                            lexema += caracterActual;
                            estadoActual = 6;
                        }
                        else if (caracterActual.Equals("*"))
                        {
                            lexema += caracterActual;
                            estadoActual = 7;
                        }
                        else if (caracterActual.Equals("/"))
                        {
                            lexema += caracterActual;
                            estadoActual = 8;
                        }
                        else if (caracterActual.Equals("%"))
                        {
                            lexema += caracterActual;
                            estadoActual = 9;
                        }
                        else if (caracterActual.Equals("("))
                        {
                            lexema += caracterActual;
                            estadoActual = 10;
                        }
                        else if (caracterActual.Equals(")"))
                        {
                            lexema += caracterActual;
                            estadoActual = 11;
                        }
                        else if (caracterActual.Equals("@EOF@"))
                        {
                            lexema += caracterActual;
                            estadoActual = 12;
                        }
                        else if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 19;
                        }
                        else if (caracterActual.Equals("<"))
                        {
                            lexema += caracterActual;
                            estadoActual = 20;
                        }
                        else if (caracterActual.Equals(">"))
                        {
                            lexema += caracterActual;
                            estadoActual = 21;
                        }
                        else if (caracterActual.Equals(":"))
                        {
                            lexema += caracterActual;
                            estadoActual = 22;
                        }
                        else if (caracterActual.Equals("!"))
                        {
                            lexema += caracterActual;
                            estadoActual = 30;
                        }
                        else if (caracterActual.Equals("@FL@"))
                        {
                            estadoActual = 13;
                        }
                        else {
                            estadoActual = 18;
                        }
                        break;
                    case 1:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else if (caracterActual.Equals(","))
                        {
                            lexema += caracterActual;
                            estadoActual = 2;
                        }
                        else {
                            estadoActual = 14;
                        }
                        break;
                    case 2:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 3;
                        }
                        else {
                            estadoActual = 17;
                        }
                        break;
                    case 3:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 3;
                        }
                        else {
                            estadoActual = 15;
                        }
                        break;
                    case 4:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 4;
                        }
                        else if (Char.IsLetter(caracterActual.ToCharArray()[0]) || caracterActual.Equals("$") || caracterActual.Equals("_"))
                        {
                            lexema += caracterActual;
                            estadoActual = 4;
                        }
                        else {
                            estadoActual = 16;
                        }
                        break;
                    case 5:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "SUMA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;

                        break;
                    case 6:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "RESTA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;

                        break;
                    case 7:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MULTIPLICACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;

                        break;
                    case 8:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("*"))
                        {
                            lexema = "";
                            estadoActual = 38;
                        }
                        else
                        {

                            estadoActual = 37;
                        }

                        break;
                    case 9:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MODULO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;

                        break;
                    case 10:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARENTESIS ABRE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;

                        break;
                    case 11:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARANTESIS CIERRA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;

                        break;
                    case 12:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "FIN ARCHIVO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;

                        break;
                    case 13:
                        cargarNuevaLinea();
                        estadoActual = 0;
                        break;
                    case 14:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "NUMERO ENTERO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 15:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "NUMERO DECIMAL";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 16:
                        devolverPuntero();
                        componente = TablaDePalabrasReservadas.obtenerInstancia().obtenerPalabraReservada(lexema);
                        if (!componente.esPalabraReservada)
                        {
                            componente.lexema = lexema;
                            componente.categoria = "IDENTIFICADOR";
                        }
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 17:
                        System.Windows.Forms.MessageBox.Show("se esperaba un numero decimal valido");
                        string numeroDEcimalDummie = "999.99";
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = numeroDEcimalDummie;
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        Error errorNumeroDEcimal = new Error();
                        errorNumeroDEcimal.descripcionError = "se esperaba un numero decimal valido";
                        errorNumeroDEcimal.numLinea = numLineaActual;
                        errorNumeroDEcimal.posicionFinal = componente.posicionFinal;
                        errorNumeroDEcimal.posicionFinal = componente.posicionInicial;
                        errorNumeroDEcimal.tipoError = "LEXICO";
                        errorNumeroDEcimal.valorEsperado = "6";
                        errorNumeroDEcimal.valorRecibido = "" + caracterActual;
                        ManejadorErrores.obtenerInstancia().adicionarError(errorNumeroDEcimal);
                        continuarAnalisis = false;
                        break;
                    case 18:
                        System.Windows.Forms.MessageBox.Show("error fatal");
                        continuarAnalisis = false;
                        Error errorFatal = new Error();
                        errorFatal.descripcionError = "ERROR FATAL";
                        errorFatal.numLinea = numLineaActual;
                        errorFatal.posicionInicial = puntero - lexema.Length;
                        errorFatal.posicionFinal = puntero - 1;
                        errorFatal.tipoError = "LEXICO";
                        errorFatal.valorEsperado = "PROGRAMA VÁLIDO";
                        errorFatal.valorRecibido = lexema + caracterActual;//Ojo para que coja todo el error
                        ManejadorErrores.obtenerInstancia().adicionarError(errorFatal);
                        break;
                    case 19:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 20:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals(">"))
                        {
                            lexema += lexema;
                            estadoActual = 23;
                        }
                        else if (caracterActual.Equals("="))
                        {
                            lexema += lexema;
                            estadoActual = 24;
                        }
                        else {
                            estadoActual = 25;
                        }
                        break;
                    case 21:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 26;
                        }
                        else
                        {
                            estadoActual = 27;
                        }
                        break;
                    case 22:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 28;
                        }
                        else
                        {
                            estadoActual = 29;
                        }
                        break;
                    case 23:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIFERENTE QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 24:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MENOR O IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 25:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MENOR QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 26:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MAYOR O IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 27:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MAYOR QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 28:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "ASIGNACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 29:
                        System.Windows.Forms.MessageBox.Show("se esperaba =");
                        devolverPuntero();
                        string asignacionDummie = ":=";
                        componente = new ComponenteLexico();
                        componente.lexema = asignacionDummie;
                        componente.categoria = "ASIGNACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        Error errorAsignacion = new Error();
                        errorAsignacion.descripcionError = "se esperaba =";
                        errorAsignacion.numLinea = numLineaActual;
                        errorAsignacion.posicionFinal = componente.posicionFinal;
                        errorAsignacion.posicionFinal = componente.posicionInicial;
                        errorAsignacion.tipoError = "lEXICO";
                        errorAsignacion.valorEsperado = "=";
                        errorAsignacion.valorRecibido = "" + caracterActual;
                        ManejadorErrores.obtenerInstancia().adicionarError(errorAsignacion);
                        continuarAnalisis = false;
                        break;
                    case 30:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += lexema;
                            estadoActual = 31;
                        }
                        else {
                            estadoActual = 32;
                        }
                        break;
                    case 31:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIFERENTE QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 32:
                        System.Windows.Forms.MessageBox.Show("se esperaba =");
                        string DiferenteQueDummie = "!=";
                        componente = new ComponenteLexico();
                        componente.lexema = DiferenteQueDummie;
                        componente.categoria = "DIFERENTE QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        Error errorDiferenteQue = new Error();
                        errorDiferenteQue.descripcionError = "se esperaba =";
                        errorDiferenteQue.numLinea = numLineaActual;
                        errorDiferenteQue.posicionFinal = componente.posicionFinal;
                        errorDiferenteQue.posicionFinal = componente.posicionInicial;
                        errorDiferenteQue.tipoError = "lEXICO";
                        errorDiferenteQue.valorEsperado = "=";
                        errorDiferenteQue.valorRecibido = "" + caracterActual;
                        ManejadorErrores.obtenerInstancia().adicionarError(errorDiferenteQue);
                        continuarAnalisis = false;
                        break;
                    case 37:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIVISION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 38:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("@EOF@"))
                        {
                            lexema += caracterActual;
                            estadoActual = 40;
                        }
                        else if (caracterActual.Equals("@FL@"))
                        {
                            cargarNuevaLinea();
                            estadoActual = 38;
                        }
                        else if (caracterActual.Equals("*"))
                        {
                            estadoActual = 39;
                        }
                        else
                        {
                            estadoActual = 38;
                        }
                        break;
                    case 39:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("@EOF@"))
                        {
                            lexema += caracterActual;
                            estadoActual = 40;
                        }
                        else if (caracterActual.Equals("@FL@"))
                        {
                            cargarNuevaLinea();
                            estadoActual = 38;
                        }
                        else if (caracterActual.Equals("*"))
                        {
                            estadoActual = 39;
                        }
                        else if (caracterActual.Equals("/"))
                        {
                            estadoActual = 0;
                        }
                        else
                        {
                            estadoActual = 38;
                        }
                        break;
                    case 40:
                        System.Windows.Forms.MessageBox.Show("se esperaba cierre comentario");
                        Error errorComentario = new Error();
                        errorComentario.descripcionError = "se esperaba cierre de comentario";
                        errorComentario.numLinea = numLineaActual;
                        errorComentario.posicionFinal = puntero - lexema.Length;
                        errorComentario.posicionFinal = puntero - 1;
                        errorComentario.tipoError = "LEXICO";
                        errorComentario.valorEsperado = "*/";
                        errorComentario.valorRecibido = "" + caracterActual;
                        ManejadorErrores.obtenerInstancia().adicionarError(errorComentario);
                        continuarAnalisis = false;
                        break;

                }
            }
            if (componente != null)
            {
                TablaDeSimbolos.obtenerInstancia().adicionarSimbolo(componente);
            }
            else {
                componente = new ComponenteLexico();
            }
            return componente;
        }
    }
}