using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Cache
{
    class Linea
    {
        private int numero;
        private string contenido;
        public Linea(int numero, string contenido)
        {
            this.numero = numero;
            this.contenido = contenido;
        }
        public int getNumero()
        {
            return this.numero;
        }
        public string getContenido()
        {
            return this.contenido;
        }
        public void setNumero(int numero)
        {
            this.numero = numero;
        }
        public void setContenido(string contenido)
        {
            this.contenido = contenido;
        }

    }
}
