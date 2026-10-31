using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using IMC_MVVM_ARSG.Models;

namespace IMC_MVVM_ARSG.ViewModels
{
	public class ImcViewModel : INotifyPropertyChanged
	{
		//Los Entry capturan texto, incluso al escribir números
		public string PesoT { get; set; } = "";
		public string EstaturaT { get; set; } = "";
		private string resultado = "Ingresa tus datos";
		public string Resultado
		{
			get
			{
				return resultado;
			}

			set
			{
				resultado = value;

				//Avisar al label que el resultado cambió
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(nameof(Resultado)));

			}
		}

		//El botón usará este comando mediante binding
		public ICommand CalcularCommand { get; }

		public ImcViewModel()
		{
			//Al ejecutar el comando se llama a calcular
			CalcularCommand = new Command(Calcular);
		}

		//El binding escucha este evento
		public event PropertyChangedEventHandler? PropertyChanged;

		private void Calcular()
		{
			//Validamos cada campo por separado
			double peso;
			bool pesoValido = double.TryParse(PesoT, out peso);

			if (pesoValido == false || !double.IsFinite(peso) || peso <= 0)
			{
				Resultado = "Escribe un peso válido mayor que cero.";
				return;
			}

			double estatura;
			bool estaturaValido = double.TryParse(EstaturaT, out estatura);

			if (estaturaValido == false || !double.IsFinite(estatura)
				|| estatura <= 0)
			{
				Resultado = "Escribe una estatura válida mayor que cero.";
				return;
			}

			//Crear el modelo y le asignamos datos
			Persona persona = new Persona();
			persona.Peso = peso;
			persona.Estatura = estatura;

			//El modelo realiza la operación
			double imc = persona.CalcularIMC();

			//Evitamos mostrar un resultado numérico inválido

			if (!double.IsFinite(imc))
			{
				Resultado = "Revisa el peso y la estatura.";
				return;
			}

			//F2 muestra el número con 2 décimales
			Resultado = "Tu IMC es: " + imc.ToString("F2");

			//Identificamos la clasificación de acuerdo al IMC
			if (imc < 18.5)
			{
				Clasificacion = "Peso bajo";
			}
			else if (imc < 25)
			{
				Clasificacion = "Peso normal";
			}
			else if (imc < 30)
			{
				Clasificacion = "Sobrepeso";
			}
			else
			{
				Clasificacion = "Obesidad";
			}
		}

		private string clasificacion = "Esperando resultado";
		public string Clasificacion
			{
			get
			{
				return clasificacion;
			}

			set
			{
				clasificacion = value;

				//Avisar al label que la clasificación cambió
				PropertyChanged?.Invoke(this,
					new PropertyChangedEventArgs(nameof(Clasificacion)));
			}
		}
	}
}
