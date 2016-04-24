using Compilador.Analisis_Lexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Tabla_de_simbolos
{
    class TablaDePalabrasReservadas
    {

        private static TablaDePalabrasReservadas INSTANCIA = new TablaDePalabrasReservadas();
        private Dictionary<String, ComponenteLexico> mapaPalabrasReservadas = new Dictionary<string, ComponenteLexico>();

        private TablaDePalabrasReservadas()
        {
            mapaPalabrasReservadas.Add("ENTERO", new ComponenteLexico("ENTERO", "TIPO DATO NUMERO ENTERO", true));
            mapaPalabrasReservadas.Add("DECIMAL", new ComponenteLexico("DECIMAL", "TIPO DATO NUMERO DECIMAL", true));
            mapaPalabrasReservadas.Add("TEXTO", new ComponenteLexico("TEXTO", "TIPO DATO TEXTO", true));
            mapaPalabrasReservadas.Add("CARACTER", new ComponenteLexico("CARACTER", "TIPO DATO NUMERO CARACTER", true));
            mapaPalabrasReservadas.Add("MIENTRAS", new ComponenteLexico("MIENTRAS", "ENCABEZADO ESTRUCTURA MIENTRAS", true));
            mapaPalabrasReservadas.Add("PARA", new ComponenteLexico("PARA", "ENCABEZADO ESTRUCTURA PARA", true));
        }
        public static TablaDePalabrasReservadas obtenerInstancia()
        {
            return INSTANCIA;
        }
        public ComponenteLexico obtenerPalabraReservada(String clave)
        {
            ComponenteLexico retorno = new ComponenteLexico();
            if (clave != null && clave.Trim() != "" && mapaPalabrasReservadas.ContainsKey(clave))
            {
                retorno = (ComponenteLexico)mapaPalabrasReservadas[clave].Clone();
            }
            return retorno;
        }

    }
}
