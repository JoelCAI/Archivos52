using System;
using System.Collections.Generic;

namespace Archivos52
{

	/* Diccionario: nos pide un valor de clave que lo identifica y valor determinado Add(TKey,TValue)
	   No podra existir dos elementos con la misma clave
	   Consume muchos recursos porque trabaja con la base de datos	
	   Foreach no tiene un orden 
	   Para el objeto Diccionary el metodo Add de instancia.Key y value son propiedades de instancia
	   son de instancia porque se aplica a un objeto diccionary en particular 
	
	   Los metodos estaticos se aplican unicamente sobre una instancia de la clase 
	   Console. ya es una unica instancia de ese objeto de una clase estatica, por eso solo habra una 
	
	   La clase surge de los requerimientos no de lo que se define por convencion en una clase ejemplo Persona 
	   no es su DNI nombre altura peso sino lo que me pida el ejercicio
	
	   Cada clase se define en un archivo separado.	
	   
	   Namespace: definir las clases dentro de un espacio de nombre que yo controle 
	   Public: todo el codigo (Namespace) puede acceder a la propiedad (inclye apps que hagan referencia al nuestro) 
	   Internal: todo el codigo (Namespace) puede acceder a la propiedad (no incluye apps de referencia)
	
	   Constructor: Siempre hay uno por defecto que no tiene parametros y no tiene nada
	   Public Persona()
	   {
	   }
	   Es para crear objetos dependiendo de su accesibilidad. Si es privada usar un metodo estatico.
	   Public static Persona CrearPersona()
	   {
			return new Persona(); 
	   }
	   
	   
	     */


	class Program
	{
		static void Main(string[] args)
		{
			Sistema s = new Sistema();
			s.Iniciar();
		}

	}
}