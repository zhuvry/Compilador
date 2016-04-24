using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Manejador_de_errores
{
    public class ManejadorErrores
    {
        private static ManejadorErrores INSTANCIA = new ManejadorErrores();
        private List<Error> erroresLexicos = new List<Error>();
        private List<Error> erroresSintacticos = new List<Error>();
        private List<Error> erroresSemanticos = new List<Error>();
        private ManejadorErrores() { }

        public static ManejadorErrores obtenerInstancia()
        {
            return INSTANCIA;
        }
        public void adicionarError(Error error)
        {
            if (error != null)
            {
                if ("LEXICO".Equals(error.tipoError))
                {
                    erroresLexicos.Add(error);
                }

                if ("SINTACTICO".Equals(error.tipoError))
                {
                    erroresSintacticos.Add(error);
                }
                if ("SEMANTICO".Equals(error.tipoError))
                {
                    erroresSemanticos.Add(error);
                }
            }
        }
        public Boolean existenErroresLexicos()
        {
            return erroresLexicos.Any();
        }
        public Boolean existenErroresSemanticos()
        {
            return erroresSemanticos.Any();
        }
        public Boolean existenErroresSintacticos()
        {
            return erroresSintacticos.Any();
        }
        public List<Error> obtenerErrores(String tipo)
        {
            List<Error> listaRetorno = null;
            if ("LEXICO".Equals(tipo))
            {
                listaRetorno = erroresLexicos;
            }
            else if ("SEMANTICOS".Equals(tipo))
            {
                listaRetorno = erroresSemanticos;
            }
            else if ("SINTACTICOS".Equals(tipo))
            {
                listaRetorno = erroresSemanticos;
            }
            return listaRetorno;

        }
        public Boolean existenErrores()
        {
            return (existenErroresLexicos() || existenErroresSemanticos() || existenErroresSintacticos());
        }
        public List<Error> ObtenerErroresCompletos()
        {
            return erroresLexicos.Union(erroresSemanticos).Union(erroresSintacticos).ToList();
        }

    }
}
