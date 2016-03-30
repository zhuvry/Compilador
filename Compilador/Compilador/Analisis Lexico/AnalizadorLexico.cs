using Compilador.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (ProgramaFuente.obtenerInstancia().getLineas().Count >= numLineaActual)
            {
                ProgramaFuente inst = ProgramaFuente.obtenerInstancia();

                contenidoLineaActual =inst.getLinea(numLineaActual).getContenido();
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
                            lexema += caracterActual;
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
                            lexema += lexema;
                            estadoActual = 1;
                        }
                        else if (caracterActual.Equals(","))
                        {
                            lexema += lexema;
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
                            lexema += lexema;
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
                            lexema += lexema;
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


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 6:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "RESTA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 7:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MULTIPLICACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 8:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIVISION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 9:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MODULO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 10:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARENTESIS ABRE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 11:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARANTESIS CIERRA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

                        continuarAnalisis = false;

                        break;
                    case 12:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "FIN ARCHIVO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos

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

                        ////Pendiente colocar en la tabla de símbolos
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

                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 16:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "IDENTIFICADOR";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 17:
                        continuarAnalisis = false;
                        break;
                    case 18:
                        continuarAnalisis = false;
                        break;
                    case 19:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos
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


                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 24:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MENOR O IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos
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

                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 26:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MAYOR O IGUAL QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos
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

                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 28:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "ASIGNACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                    case 29:
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
                            //Pendienteee !!!
                        }
                        break;
                    case 31:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIFERENTE QUE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;


                        ////Pendiente colocar en la tabla de símbolos
                        continuarAnalisis = false;
                        break;
                }
            }
            return componente;
        }
    }
}