using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos52
{
    public class UsuarioAdministrador : Usuario
    {
        protected List<Persona> _persona;

        public List<Persona> Persona
        {
            get { return this._persona; }
            set { this._persona = value; }
        }

        public UsuarioAdministrador(string nombre, List<Persona> persona) : base(nombre)
        {
			this._persona = persona;
        }
		

		public void MenuAdministrador(List<Persona> persona)
		{

			Persona = persona; /* Se reciben las personas del sistema */
			
			int opcion;
			do
			{

				Console.Clear();
				Console.WriteLine(" Bienvenido Usuario: *" + Nombre + "* ");
				opcion = Validador.PedirIntMenu("\n Menú de Registro de nuevas Personas: " +
									   "\n [1] Alta [legajo] [nombre]: da de alta el legajo / nombre en el diccionario." +
									   "\n [2] Baja [legajo]: da de baja el  legajo del diccionario" +
									   "\n [3] Grabar [ruta de archivo]”: graba el diccionario en el archivo indicado." +
									   "\n [4] Leer [ruta de archivo]”: Lee el diccionario a partir del archivo indicado" +
									   "\n [5] Salir del Sistema.", 1, 5);

				switch (opcion)
				{
					case 1:
						DarAltaPersona();
						break;
					case 2:
						DarBajaPersona();
						break;
					case 3:
						GrabarPersonaAgenda();
						break;
					case 4:
						LeerAgenda();
						Validador.VolverMenu();
						break;
					case 5:
						break;
				}
			} while (opcion != 5);
		}

		public int BuscarPersonaDni(int dni)
		{
			for (int i = 0; i < this._persona.Count; i++)
			{
				if (this._persona[i].Dni == dni)
				{
					return i;
				}
			}
			/* si no encuentro el producto retorno una posición invalida */
			return -1;
		}

		Dictionary<int, Persona> personaAgenda = new Dictionary<int, Persona>();
		
		protected override void DarAltaPersona()
		{
			int dni;
			
			string nombre;
			string apellido;
			long telefono;
			DateTime fechaNacimiento;
			string opcion;

			Console.Clear();
			dni = Validador.PedirIntMenu("\n Ingrese el DNI de la Persona a agregar. El valor debe ser entre ", 0, 99999999);
			if (BuscarPersonaDni(dni) == -1)
			{
				VerPersona();
				Console.WriteLine("\n ¡En hora buena! Puede utilizar este DNI para crear una Persona Nueva en su agenda");
				nombre = ValidarStringNoVacioNombre("\n Ingrese el nombre de la Persona");
				apellido = ValidarStringNoVacioNombre("\n Ingrese el apellido de la Persona");
				Console.Clear();
				telefono = Validador.PedirLongMenu("\n Ingrese el télefono de la Persona. El valor debe ser entre ", 1100000000, 1199999999);
				fechaNacimiento = Validador.ValidarFechaIngresada("\n Ingrese la fecha de Nacimiento de la Persona");

				

				opcion = ValidarSioNoPersonaNoCreada("\n Está seguro que desea crear esta Persona? ", dni,nombre,apellido,telefono,fechaNacimiento);

				if (opcion == "SI")
                {
					Persona p = new Persona(dni, nombre, apellido, telefono, fechaNacimiento);
					AddPersona(p);
					personaAgenda.Add(dni,p);
					VerPersona();
					VerPersonaDiccionario();
					Console.WriteLine("\n Persona con DNI *" + dni + " y nombre *" + nombre + "* agregado exitósamente");
					Validador.VolverMenu();
				}
				else
				{
					VerPersona();
					Console.WriteLine("\n Como puede verificar no se creo ninguna Persona");
					Validador.VolverMenu();

				}

			}
			else
            {
				VerPersona();
				Console.WriteLine("\n Usted digitó el DNI *" + dni + "*");
				Console.WriteLine("\n Ya existe una persona con ese DNI");
				Console.WriteLine("\n Será direccionado nuevamente al Menú para que lo realice correctamente");
				Validador.VolverMenu();

			}

		}

		public void AddPersona(Persona persona)
		{
			this._persona.Add(persona);
		}

		public void RemoverPersona(int pos)
		{
			this._persona.RemoveAt(pos);
		}

		protected override void DarBajaPersona()
		{
			int dni;
			string confirmacion;

			VerPersona();
			dni = Validador.PedirIntMenu("\n Ingrese el DNI de la Persona a agregar. El valor debe ser entre ", 0, 99999999);

			
			if (BuscarPersonaDni(dni) != -1)
			{

				VerPersona();
				
				confirmacion = ValidarSioNoPersonaCreada("\n\n Está seguro que desea eliminar esta Persona?", dni);

				if (confirmacion == "SI")
				{
					personaAgenda.Remove(dni);
					RemoverPersona(BuscarPersonaDni(dni));
					VerPersona();
					
					VerPersonaDiccionario();
					Console.WriteLine("\n Persona eliminada exitósamente");
					Validador.VolverMenu();
				}
				else
				{
					VerPersona();
					Console.WriteLine("\n Como puede apreciar la Persona no ha sido eliminada");
					Validador.VolverMenu();
				}

			}
			else
			{
				Console.Clear();
				VerPersona();
				Console.WriteLine("\n No existe una Persona con este Dni *" + dni + "*. " +
								  "\n\n Vuelva a intentarlo ingresando el valor de uno de los DNI que ve en la lista " +
								  "la siguiente vez");
				Validador.VolverMenu();
			}

		}

		protected override void GrabarPersonaAgenda()
		{
			using (var archivoAgenda = new FileStream("archivoAgenda.txt",FileMode.Create))
            {
				using (var archivoEscrituraAgenda = new StreamWriter(archivoAgenda))
                {
					foreach (var persona in personaAgenda.Values)
                    {
						
						var linea = "\n Dni de la Persona: " + persona.Dni + 
									"\n Nombre de la Persona: " + persona.Nombre;
						archivoEscrituraAgenda.WriteLine(linea);
						
					}
					
                }
            }
			VerPersona();
			Console.WriteLine("Se ha grabado los datos de las personas en la Agenda correctamente");
			Validador.VolverMenu();

		}

		protected override void LeerAgenda()
		{

		}

		protected string ValidarSioNoPersonaCreada(string mensaje, int dni)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Si esta seguro de ello escriba *" + "si" + "* sin los asteriscos" +
									  "\n De lo contrario escriba " + "*" + "no" + "* sin los asteriscos";
			string mensajeError = "\n Por favor ingrese el valor solicitado y que no sea vacio. ";

			do
			{
				VerPersona();
				if (BuscarPersonaDni(dni) != -1)
				{
					Console.WriteLine("\n Dni de la Persona : " + Persona[BuscarPersonaDni(dni)].Dni +
									  "\n Nombre de la Persona : " + Persona[BuscarPersonaDni(dni)].Nombre +
									  "\n Apellido de la Persona : " + Persona[BuscarPersonaDni(dni)].Apellido +
									  "\n Teléfono de la Persona : " + Persona[BuscarPersonaDni(dni)].Telefono +
									  "\n Fecha de Nacimiento de la Persona: " + Persona[BuscarPersonaDni(dni)].FechaNacimiento);
				}
				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeError);
				Console.WriteLine(mensajeValidador);
				opcion = Console.ReadLine().ToUpper();
				string opcionC = "SI";
				string opcionD = "NO";

				if (opcion == "" || (opcion != opcionC) & (opcion != opcionD))
				{
					Console.WriteLine("\n");

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}

		protected string ValidarSioNoPersonaNoCreada(string mensaje,int dni, string nombre, string apellido, long telefono, DateTime fecha)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Si esta seguro de ello escriba *" + "si" + "* sin los asteriscos" +
									  "\n De lo contrario escriba " + "*" + "no" + "* sin los asteriscos";
			string mensajeError = "\n Por favor ingrese el valor solicitado y que no sea vacio. ";

			do
			{
				VerPersona();

				Console.WriteLine("\n DNI de la Persona a Crear: " + dni +
								  "\n Nombre de la Persona a Crear: " + nombre +
								  "\n Apellido de la Persona a Crear: " + apellido +
								  "\n Teléfono de la Persona a Crear: " + telefono +
								  "\n Fecha de Nacimiento de la Persona a Crear: " + fecha);

				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeError);
				Console.WriteLine(mensajeValidador);
				opcion = Console.ReadLine().ToUpper();
				string opcionC = "SI";
				string opcionD = "NO";

				if (opcion == "" || (opcion != opcionC) & (opcion != opcionD))
				{
					continue;

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}


		protected string ValidarStringNoVacioNombre(string mensaje)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Por favor ingrese el valor solicitado y que no sea vacio.";


			do
			{
				VerPersona();
				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeValidador);

				opcion = Console.ReadLine().ToUpper();

				if (opcion == "")
				{

					Console.Clear();
					Console.WriteLine("\n");
					Console.WriteLine(mensajeValidador);

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}

		public void VerPersona()
		{
			Console.Clear();
			Console.WriteLine("\n Personas en Agenda");
			Console.WriteLine(" #\tDNI.\t\tNombre.\t\tApellido.\t\tTelefono.\t\tFechaNacimiento.");
			for (int i = 0; i < Persona.Count; i++)
			{
				Console.Write(" " + (i + 1));
				Console.Write("\t");
				Console.Write(Persona[i].Dni);
				Console.Write("\t\t");
				Console.Write(Persona[i].Nombre);
				Console.Write("\t\t");
				Console.Write(Persona[i].Apellido);
				Console.Write("\t\t");
				Console.Write(Persona[i].Telefono);
				Console.Write("\t\t");
				Console.Write(Persona[i].FechaNacimiento);
				Console.Write("\n");
			}

		}

		public void VerPersonaDiccionario()
        {
			Console.WriteLine("\n Personas en el Diccionario");
			for (int i = 0; i < personaAgenda.Count; i++)
            {
				KeyValuePair<int, Persona> persona = personaAgenda.ElementAt(i);
				
				Console.WriteLine("\n Dni: " + persona.Key);
				Persona personaValor = persona.Value;

				Console.WriteLine(" Nombre Persona: " + personaValor.Nombre);
				Console.WriteLine(" Apellido Persona: " + personaValor.Apellido);
				Console.WriteLine(" Telefono Persona: " + personaValor.Telefono);
				Console.WriteLine(" Fecha de Nacimiento Persona: " + personaValor.FechaNacimiento);

			}

			
        }

        



    }
}