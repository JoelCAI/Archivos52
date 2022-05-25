using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos52
{
    public abstract class Usuario
    {
        protected int _registro;
        protected string _nombre;

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public int Registro
        {
            get { return this._registro; }
            set { this._registro = value; }
        }

        public static int _registroUsuario = 1;

        public Usuario(string nombre)
        {
            this._registro = _registroUsuario;
            _registroUsuario++;
            this._nombre = nombre;

        }

       
        protected abstract void DarAltaPersona();

        protected abstract void DarBajaPersona();

        protected abstract void GrabarPersonaAgenda();

        protected abstract void LeerAgenda();
    }
}