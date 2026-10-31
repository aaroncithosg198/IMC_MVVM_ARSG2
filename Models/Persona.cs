using System;
using System.Collections.Generic;
using System.Text;

namespace IMC_MVVM_ARSG.Models
{
	internal class Persona
	{
		//Datos númericos necesarios para el cálculo

		//Variables
		public double Peso { get; set; }
		public double Estatura { get; set; }

		//Método que devuelve el IMC utilizando los datos de persona
		public double CalcularIMC()
		{
			return Peso / (Estatura * Estatura);
		}
	}
}