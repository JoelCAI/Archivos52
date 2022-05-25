using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos52
{
    public class Persona
    {
		protected int _registroPersona;
		private int _dni;
		private string _nombre;
		private string _apellido;
		private long _telefono;
		private DateTime _fechaNacimiento;

		/* Un descriptor de acceso que no tiene set solo puede escribirse una vez por el contructor 
		   Ejemplo
		   public Persona (int dni)
			{
				Dni = dni
			}
		 */
		public int Dni
		{
			get { return this._dni; }
			set { this._dni = value; }
		}
		public string Nombre
		{
			get { return this._nombre; }
			set { this._nombre = value; }
		}

		public string Apellido
		{
			get { return this._apellido; }
			set { this._apellido = value; }
		}
		public long Telefono
		{
			get { return this._telefono; }
			set { this._telefono = value; }
		}
		public DateTime FechaNacimiento
		{
			get { return this._fechaNacimiento; }
			set { this._fechaNacimiento = value; }
		}

		public static int registroPersona = 1;

		public Persona(string linea)
        {
			var datos = linea.Split(';');
			Dni = int.Parse(datos[0]);
			Nombre = datos[1];
			Apellido = datos[2];
			Telefono = int.Parse(datos[3]);
			FechaNacimiento = DateTime.Parse(datos[4]);
		}

		public Persona(int dni, string nombre, string apellido, long telefono, DateTime fechaNacimiento )
		{
			this._dni = dni;
			this._nombre = nombre;
			this._apellido = apellido;
			this._telefono = telefono;
			this._fechaNacimiento = fechaNacimiento;
			this._registroPersona = registroPersona;
			registroPersona++;
		}
	}
}
